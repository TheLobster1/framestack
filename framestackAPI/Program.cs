using System.Net.Mail;
using framestackAPI;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

app.MapGet("/picturesfromaccount/{accountId}/{page}/{size}", async (HttpRequest request) =>
{
    var pictures = new List<Picture>();
    if (!int.TryParse(request.RouteValues["accountId"].ToString(), out int id)) return pictures;
    if (!int.TryParse(request.RouteValues["page"].ToString(), out int page)) return pictures;
    if (!int.TryParse(request.RouteValues["size"].ToString(), out int size)) return pictures;
    var result = await DatabaseConnection.GetPicturesFromAccount(id, page, size);
    return result;
})
    .WithName("GetPictures");

app.MapPost("/createuser", async (HttpRequest request) =>
{
    var test = await request.ReadFromJsonAsync<User>();
    if (!MailAddress.TryCreate(test.Email, out MailAddress mailAddress)) return "Invalid mail address";
    if (test.Password.Length != 60) return "Invalid password";
    var result = await DatabaseConnection.CreateUser(test.Username, test.Password, test.DateOfBirth, test.Email, test.FirstName, test.LastName);
    return result;
})
    .WithName("CreateUser");

app.MapPost("/uploadpicture", async Task<Results<Ok<string>, BadRequest<string>>>
    ([FromForm] FormFile fileUploadForm, HttpContext context) =>
{
    var name = fileUploadForm.FileName;
    var description = fileUploadForm.FileName;
    var accountId = "1";
    if (fileUploadForm == null) return TypedResults.BadRequest("Please provide a file");
    if (string.IsNullOrEmpty(fileUploadForm.FileName)) return TypedResults.BadRequest("Please provide a file name");
    var filePath = await Utils.UploadFile(fileUploadForm, accountId);
    var success = await DatabaseConnection.CreatePicture(name, description, filePath, accountId);
    if (success) return TypedResults.Ok($"Uploaded file {fileUploadForm.FileName} successfully");
    return TypedResults.BadRequest("Failed to add picture to database");
})
    .WithName("UploadPicture");

app.Run();

public record Picture(string Url, string? Title, string? Description, int Id, DateTime DateCreated, int AccountId, List<Tag>? Tags);
public record User(string Username, string Password, DateTime DateOfBirth, string Email, string FirstName, string LastName, List<Picture>? PictureList, List<Album>? Albums);
public record Album(string Name, string? Description, List<Picture>? PictureList);
public record Tag(string Name);
public record Family(string Name, string? Description, List<Picture>? PictureList, List<Album>? Albums);