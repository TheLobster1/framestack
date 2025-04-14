namespace framestackAPI;

public static class Utils
{
    //Change this value to the location you would like your pictures to be stored.
    private const string filePath = "D:\\FrameStackPictures";

    public static async Task<string[]> UploadFile(IFormFile fileUploadForm, string accountId, string description = "")
    {
        string pathToSave = Path.Combine(filePath, "uploads");
        if (!Directory.Exists(pathToSave)) Directory.CreateDirectory(pathToSave);
        var userPath = Path.Combine(pathToSave, accountId);
        if (!Directory.Exists(userPath)) Directory.CreateDirectory(userPath);
        string fullFilePath = Path.Combine(userPath, Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(fileUploadForm.FileName)));
        while (File.Exists(fullFilePath))
        {
            fullFilePath = Path.Combine(userPath, Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(fileUploadForm.FileName)));
        }
        await using FileStream stream = new (fullFilePath, FileMode.Create);
        await fileUploadForm.CopyToAsync(stream);
        var results = new string[3];
        results[0] = fullFilePath;
        results[1] = fileUploadForm.FileName;
        results[2] = description;
        return results;
    }
}