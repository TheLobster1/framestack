using System.Diagnostics;
using System.Net.Http.Json;
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

    public static async Task<List<Picture>> GetPictures(string email, int page = 0, int pageSize = 20)
    {
        var result = new List<Picture>();
        Uri restUri = new Uri(_restUrl);
        Uri uri = new Uri(restUri, "/picturesfromaccount/" + email + "/" + page + "/" + pageSize);
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
    public static async Task<string> UploadPicture(FileResult fileResult, User user, List<Tag> tags)
    {
        Uri restUri = new Uri(_restUrl);
        Uri uri = new Uri(restUri, "/uploadpicture");
        var stream = await fileResult.OpenReadAsync();
        // FormFile file = new FormFile(stream, 0, stream.Length, fileResult.FileName, fileResult.FileName);
        var content = new MultipartFormDataContent();
        content.Add(new StreamContent(stream), "file", fileResult.FileName);
        string json = JsonSerializer.Serialize(user, _serializerOptions);
        HttpContent jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
        content.Add(jsonContent);
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

    public static async Task<bool> VerifyPassword(User user)
    {
        Uri restUri = new Uri(_restUrl);
        Uri uri = new Uri(restUri, "/checkpassword");
        string json = JsonSerializer.Serialize<User>(user, _serializerOptions);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return false;
    }

    public static async Task<List<string>> CheckUser(User user)
    {
        var result = new List<string>();
        Uri restUri = new Uri(_restUrl);
        Uri uri = new Uri(restUri, "/checkuser");
        string json = JsonSerializer.Serialize<User>(user, _serializerOptions);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var messageContent = await response.Content.ReadFromJsonAsync<List<string>>();
                result = messageContent;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return result;
    }

    public static async Task<User> GetUserDetails(User user)
    {
        Uri restUri = new Uri(_restUrl);
        Uri uri = new Uri(restUri, "/userdetails");
        string json = JsonSerializer.Serialize(user, _serializerOptions);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var messageContent = await response.Content.ReadFromJsonAsync<User>();
                if (messageContent != null) user = messageContent;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return user;
    }
}