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

app.Run();

record Picture(string Url, string? Title, string? Description, int Id);