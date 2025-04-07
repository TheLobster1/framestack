using System.Diagnostics;
using System.Text;
using System.Text.Json;
using framestack.Models;
using Microsoft.AspNetCore.Http;

namespace framestack.Services;

public static class RestService
{
    private static string _restUrl = "http://localhost:5151/";
    private static HttpClient _client;
    private static JsonSerializerOptions _serializerOptions;

    static RestService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public static async Task<List<Picture>> GetPictures(int accountId, int page = 0, int pageSize = 20)
    {
        var result = new List<Picture>();
        Uri restUri = new Uri(_restUrl);
        Uri uri = new Uri(restUri, "/picturesfromaccount/" + accountId.ToString() + "/" + page.ToString() + "/" + pageSize.ToString());
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<List<Picture>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return result ??= [];
    }

    public static async Task<List<User>> GetUsers()
    {
        return new List<User>();
    }

    public static async Task<string> CreateUser(User user)
    {
        var result = "";
        Uri restUri = new Uri(_restUrl);
        Uri uri = new Uri(restUri, "/createuser");
        string json = JsonSerializer.Serialize<User>(user, _serializerOptions);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return "Success";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return "Something went wrong";
    }
//TODO: TEST THIS!!!
    public static async Task<string> UploadPicture(FileResult fileResult, string accountId, List<Tag> tags)
    {
        var result = "";
        Uri restUri = new Uri(_restUrl);
        Uri uri = new Uri(restUri, "/createuser");
        var stream = await fileResult.OpenReadAsync();
        // FormFile file = new FormFile(stream, 0, stream.Length, fileResult.FileName, fileResult.FileName);
        var content = new MultipartFormDataContent();
        content.Add(new StreamContent(stream), "file", fileResult.FileName);
        try
        {
            HttpResponseMessage response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return "Success";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return "Something went wrong";
    }
}