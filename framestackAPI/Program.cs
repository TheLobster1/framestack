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
    if (!MailAddress.TryCreate(request.RouteValues["email"].ToString(), out MailAddress mailAddress)) return pictures;
    if (!int.TryParse(request.RouteValues["page"].ToString(), out int page)) return pictures;
    if (!int.TryParse(request.RouteValues["size"].ToString(), out int size)) return pictures;
    var result = await DatabaseConnection.GetPicturesFromAccount(mailAddress.Address, page, size);
    return result;
})
    .WithName("GetPictures");

app.MapPost("/createuser", async (HttpRequest request) =>
{
    var user = await request.ReadFromJsonAsync<User>();
    if (!MailAddress.TryCreate(user.Email, out MailAddress mailAddress)) return "Invalid mail address";
    if (user.Password.Length != 60) return "Invalid password";
    var result = await DatabaseConnection.CreateUser(user.Username, user.Password, user.DateOfBirth, user.Email, user.FirstName, user.LastName);
    return result;
})
    .WithName("CreateUser");

app.MapPost("/uploadpicture", async Task<Results<Ok<string>, BadRequest<string>>>
    (HttpRequest request) =>
{
    if (!request.HasFormContentType) return TypedResults.BadRequest("Invalid request");
    var form = await request.ReadFormAsync();
    if (form.Files.Count != 1) return TypedResults.BadRequest("Invalid request");
    var file = form.Files[0];
    var name = file.FileName;
    var description = file.FileName;
    if (!form.TryGetValue("user", out var userValues) || userValues.Count == 0 || string.IsNullOrEmpty(userValues[0]))
    {
        return TypedResults.BadRequest("Required 'user' data field is missing or empty.");
    }
    var userJson = userValues[0];
    
    var user = JsonSerializer.Deserialize<User>(userJson, new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    });
    if (file == null) return TypedResults.BadRequest("Please provide a file");
    if (string.IsNullOrEmpty(file.FileName)) return TypedResults.BadRequest("Please provide a file name");
    var filePath = await Utils.UploadFile(file, user.Email);
    var success = await DatabaseConnection.CreatePicture(name, description, filePath[0], user.Email);
    if (success) return TypedResults.Ok($"Uploaded file {file.FileName} successfully");
    return TypedResults.BadRequest("Failed to add picture to database");
})
    .WithName("UploadPicture");

app.MapPost("/uploadpictures", async Task<Results<Ok<string>, BadRequest<string>>> (HttpRequest request) =>
    {
        if (!request.HasFormContentType) return TypedResults.BadRequest("Invalid request");
        var form = await request.ReadFormAsync();
        if (form.Files.Count < 1) return TypedResults.BadRequest("Invalid request");
        if (!form.TryGetValue("user", out var userValues) || userValues.Count == 0 || string.IsNullOrEmpty(userValues[0]))
        {
            return TypedResults.BadRequest("Required 'user' data field is missing or empty.");
        }
        var userJson = userValues[0];
        var user = JsonSerializer.Deserialize<User>(userJson, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
        List<Task<string[]>> tasks = new List<Task<string[]>>();
        foreach (var file in form.Files)
        {
            if (file == null) continue;
            if (string.IsNullOrEmpty(file.FileName)) continue;
            tasks.Add(Utils.UploadFile(file, user.Email));
        }
        await Task.WhenAll(tasks);
        List<Task<bool>> uploadTasks = new List<Task<bool>>();
        foreach (var task in tasks)
        {
            var filePath = task.Result[0];
            var name = task.Result[1];
            var description = task.Result[2];
            uploadTasks.Add(DatabaseConnection.CreatePicture(name, description, filePath, user.Email));
        }
        await Task.WhenAll(uploadTasks);
        int failedUploads = 0;
        foreach (var task in uploadTasks)
        {
            if (!task.Result)
            {
                failedUploads++;
            }
        }

        return TypedResults.Ok($"{failedUploads} uploads failed");
    })
    .WithName("UploadPictures");

app.MapPost("/checkpassword", async Task<Results<Ok<string>, BadRequest<string>>> (HttpRequest request) =>
{
    var user = await request.ReadFromJsonAsync<User>();
    if (user == null) return TypedResults.BadRequest("Invalid request");
    var hash = await DatabaseConnection.GetPasswordHash(user.Email);
    var correct = BCrypt.Net.BCrypt.EnhancedVerify(user.Password, hash);
    if (correct) return TypedResults.Ok("Password verified");
    return TypedResults.BadRequest("Invalid password");
})
    .WithName("VerifyPassword");

app.MapPost("/checkuser", async Task<Results<Ok<List<string>>, BadRequest<string>>> (HttpRequest request) =>
{
    var user = await request.ReadFromJsonAsync<User>();
    if (user == null) return TypedResults.BadRequest("Invalid request");

    var result = await DatabaseConnection.CheckUser(user.Username, user.Email);
    return TypedResults.Ok(result);
})
    .WithName("CheckUser");

app.MapPost("/userdetails", async (HttpRequest request) =>
{
    var user = await request.ReadFromJsonAsync<User>();
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