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

app.MapGet("/picturesfromaccount/{accountId}/{page}/{size}", (HttpRequest request) =>
{
    var id = request.RouteValues["accountId"];
    var page = request.RouteValues["page"];
    var pageSize = request.RouteValues["size"];

    var pictures = new List<Picture>();
    try
    {
        //todo: check if values are INT;
    }
    catch
    {
        return pictures;
    }
    //get pictures
    pictures.Add(new Picture("", "nice", "MORE NICE", 10));

    return pictures;
})
    .WithName("GetPictures");

// app.MapPost("/createuser",)

app.MapPost("/uploadpicture/{accountId}", async Task<Results<Ok<string>, BadRequest<string>>>
    ([FromForm] FormFile fileUploadForm, HttpContext context, IAntiforgery antiforgery, HttpRequest request) =>
{
    var idObject = request.RouteValues["accountId"];
    if (idObject == null) return TypedResults.BadRequest("Please provide a valid account Id");
    var id = idObject.ToString();
    if (fileUploadForm == null) return TypedResults.BadRequest("Please provide a file");
    if (fileUploadForm.FileName == null || fileUploadForm.FileName == "") return TypedResults.BadRequest("Please provide a file name");
    await Utils.UploadFile(fileUploadForm, id);

    return TypedResults.Ok($"Uploaded file {fileUploadForm.FileName} successfully");
});

app.Run();

record Picture(string Url, string? Title, string? Description, int Id);
