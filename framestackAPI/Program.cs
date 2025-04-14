using System.Net.Mail;
using System.Text.Json;
using framestackAPI;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/picturesfromaccount/{email}/{page}/{size}", async (HttpRequest request) =>
{
    var pictures = new List<Picture>();
    if (!MailAddress.TryCreate(request.RouteValues["email"].ToString(), out MailAddress mailAddress)) return pictures;  //check if email address is valid
    if (!int.TryParse(request.RouteValues["page"].ToString(), out int page)) return pictures;   //get page from URL
    if (!int.TryParse(request.RouteValues["size"].ToString(), out int size)) return pictures;   //get size from URL
    var result = await DatabaseConnection.GetPicturesFromAccount(mailAddress.Address, page, size);  //get pictures from account
    return result;
})
    .WithName("GetPictures");

app.MapPost("/createuser", async (HttpRequest request) =>
{
    var user = await request.ReadFromJsonAsync<User>();
    if (!MailAddress.TryCreate(user.Email, out MailAddress mailAddress)) return "Invalid mail address";
    if (user.Password.Length != 60) return "Invalid password"; //since password is hashed before sending to database, it has to be of size 60 at this point.
    var result = await DatabaseConnection.CreateUser(user.Username, user.Password, user.DateOfBirth, user.Email, user.FirstName, user.LastName);
    return result;
})
    .WithName("CreateUser");

app.MapPost("/uploadpicture", async Task<Results<Ok<string>, BadRequest<string>>>
    (HttpRequest request) =>
{
    if (!request.HasFormContentType) return TypedResults.BadRequest("Invalid request"); //check if request has a form
    var form = await request.ReadFormAsync();
    if (form.Files.Count != 1) return TypedResults.BadRequest("Invalid request");   //check if there is one file since we only expect one for this method
    var file = form.Files[0]; //get the first file
    var name = file.FileName;
    var description = file.FileName;
    if (!form.TryGetValue("user", out var userValues) || userValues.Count == 0 || string.IsNullOrEmpty(userValues[0]))  //check if the form has a user value and check if it has an entry
    {
        return TypedResults.BadRequest("Required 'user' data field is missing or empty.");
    }
    var userJson = userValues[0];
    
    var user = JsonSerializer.Deserialize<User>(userJson, new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    });
    if (file == null) return TypedResults.BadRequest("Please provide a file");  //if there is no file return an error
    if (string.IsNullOrEmpty(file.FileName)) return TypedResults.BadRequest("Please provide a file name");  //check if file has a filename
    var filePath = await Utils.UploadFile(file, user.Email);    //uploads file to specified location in Utils
    var success = await DatabaseConnection.CreatePicture(name, description, filePath[0], user.Email); //upload picture details to database after filepath has been generated
    if (success) return TypedResults.Ok($"Uploaded file {file.FileName} successfully");
    return TypedResults.BadRequest("Failed to add picture to database");
})
    .WithName("UploadPicture");

app.MapPost("/uploadpictures", async Task<Results<Ok<string>, BadRequest<string>>> (HttpRequest request) =>
    {
        if (!request.HasFormContentType) return TypedResults.BadRequest("Invalid request"); //check if request has a form
        var form = await request.ReadFormAsync();
        if (form.Files.Count < 1) return TypedResults.BadRequest("Invalid request");    //check if there are some files
        if (!form.TryGetValue("user", out var userValues) || userValues.Count == 0 || string.IsNullOrEmpty(userValues[0])) //check if user has a value
        {
            return TypedResults.BadRequest("Required 'user' data field is missing or empty.");
        }
        var userJson = userValues[0];   //select the first and only user entry
        var user = JsonSerializer.Deserialize<User>(userJson, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
        List<Task<string[]>> tasks = new List<Task<string[]>>();    //create a list of tasks to keep track of
        foreach (var file in form.Files)        //loop through every file
        {
            if (file == null) continue; //if the file does not exist continue (should not happen)
            if (string.IsNullOrEmpty(file.FileName)) continue;  //check for filename to exist
            tasks.Add(Utils.UploadFile(file, user.Email));      //start task to upload the file
        }
        await Task.WhenAll(tasks);      //wait for all upload tasks to complete
        List<Task<bool>> uploadTasks = new List<Task<bool>>();      //list of database upload tasks
        foreach (var task in tasks)     //loop through every task to upload to the database
        {
            var filePath = task.Result[0];  //from the task get the first value which is the filepath
            var name = task.Result[1];      //second value is the original name of the file
            var description = task.Result[2]; //third value is the description of the file
            uploadTasks.Add(DatabaseConnection.CreatePicture(name, description, filePath, user.Email)); //start task to add picture to the database
        }
        await Task.WhenAll(uploadTasks);    //wait for all upload to database tasks to complete
        int failedUploads = 0;
        foreach (var task in uploadTasks)   //loop through tasks to check if they succeeded
        {
            if (!task.Result) //if task failed to upload to database
            {
                failedUploads++; //increment failure counter
            }
        }

        return TypedResults.Ok($"{failedUploads} uploads failed");  //return string with information regarding how many uploads failed.
    })
    .WithName("UploadPictures");

app.MapPost("/checkpassword", async Task<Results<Ok<string>, BadRequest<string>>> (HttpRequest request) =>
{
    var user = await request.ReadFromJsonAsync<User>();     //get user json from request
    if (user == null) return TypedResults.BadRequest("Invalid request"); //check if user returned null
    var hash = await DatabaseConnection.GetPasswordHash(user.Email);    //get password hash from database
    var correct = BCrypt.Net.BCrypt.EnhancedVerify(user.Password, hash); //check if password matches hash
    if (correct) return TypedResults.Ok("Password verified");
    return TypedResults.BadRequest("Invalid password");
})
    .WithName("VerifyPassword");

app.MapPost("/checkuser", async Task<Results<Ok<List<string>>, BadRequest<string>>> (HttpRequest request) =>
{
    var user = await request.ReadFromJsonAsync<User>(); //get user json from request
    if (user == null) return TypedResults.BadRequest("Invalid request");    //check if user exists in the request

    var result = await DatabaseConnection.CheckUser(user.Username, user.Email); //check if user exists in database
    return TypedResults.Ok(result); //return data with all messages from api call
})
    .WithName("CheckUser");

app.MapPost("/userdetails", async (HttpRequest request) =>
{
    var user = await request.ReadFromJsonAsync<User>(); //get user json from request
    if (user == null) return null;
    var result = await DatabaseConnection.GetUserDetails(user.Email);
    return result;
})
    .WithName("GetUserDetails");

app.Run();

public record Picture(string FilePath, string? Name, string? Description, int Id, DateTime DateCreated, int AccountId, List<Tag>? Tags);
public record User(string Username, string Password, DateTime DateOfBirth, string Email, string FirstName, string LastName, List<Picture>? PictureList, List<Album>? Albums);
public record Album(string Name, string? Description, List<Picture>? PictureList);
public record Tag(string Name);
public record Family(string Name, string? Description, List<Picture>? PictureList, List<Album>? Albums);