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
    //TODO: check if username or email are taken and password size is correct.
    var username = request.Query["username"];
    var password = request.Query["password"];
    var email = request.Query["email"];
    var firstName = request.Query["firstName"];
    var lastName = request.Query["lastName"];
    if (!DateTime.TryParse(request.Query["dateOfBirth"], out DateTime dateOfBirth)) return "Invalid date of birth";
    if (!MailAddress.TryCreate(email, out MailAddress mailAddress)) return "Invalid mail address";
    if (password.Count != 60) return "Invalid password";
    var result = await DatabaseConnection.CreateUser(username, password, dateOfBirth, email, firstName, lastName);
    return result;
});

app.MapPost("/uploadpicture", async Task<Results<Ok<string>, BadRequest<string>>>
    ([FromForm] FormFile fileUploadForm, HttpContext context) =>
{
    var id = "0";
    if (fileUploadForm == null) return TypedResults.BadRequest("Please provide a file");
    if (fileUploadForm.FileName == null || fileUploadForm.FileName == "") return TypedResults.BadRequest("Please provide a file name");
    await Utils.UploadFile(fileUploadForm, id);

    return TypedResults.Ok($"Uploaded file {fileUploadForm.FileName} successfully");
})
    .WithName("UploadPicture");

app.Run();

public record Picture(string Url, string? Title, string? Description, int Id, DateTime DateCreated, int AccountId);
