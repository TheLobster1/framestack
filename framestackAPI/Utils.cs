namespace framestackAPI;

public static class Utils
{
    //TODO: EXPLAIN WHAT THIS DOES
    public static async Task UploadFile(FormFile fileUploadForm, string accountId)
    {
        string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        if (!Directory.Exists(pathToSave)) Directory.CreateDirectory(pathToSave);
        var userPath = Path.Combine(pathToSave, accountId);
        if (!Directory.Exists(userPath)) Directory.CreateDirectory(userPath);
        string fullFilePath = Path.Combine(userPath, Path.GetRandomFileName());
        while (File.Exists(fullFilePath))
        {
            fullFilePath = Path.Combine(userPath, Path.GetRandomFileName());
        }

        await using FileStream stream = new (fullFilePath, FileMode.Create);
        await fileUploadForm.CopyToAsync(stream);
    }
}