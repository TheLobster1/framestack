namespace framestackAPI;

public static class Utils
{
    //Change this value to the location you would like your pictures to be stored.
    private const string filePath = "D:\\FrameStackPictures";

    public static async Task<string[]> UploadFile(IFormFile fileUploadForm, string accountId, string description = "")  //Current implementation does not exist for the description to be added here. It was added for the upload to know which file was uploaded and which was not.
    {
        string pathToSave = Path.Combine(filePath, "uploads");  //comabine paths to upload file to specified directory/uploads
        if (!Directory.Exists(pathToSave)) Directory.CreateDirectory(pathToSave); //check if path exists and create if it does not.
        var userPath = Path.Combine(pathToSave, accountId); //create a path for user email
        if (!Directory.Exists(userPath)) Directory.CreateDirectory(userPath);   //create folder if it does not exist
        string fullFilePath = Path.Combine(userPath, Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(fileUploadForm.FileName)));   //generate a random filename and keep the extension from original file.
        while (File.Exists(fullFilePath))   //if for some reason this random file name already exists, create a new one
        {
            fullFilePath = Path.Combine(userPath, Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(fileUploadForm.FileName)));  //new random filename
        }
        await using FileStream stream = new (fullFilePath, FileMode.Create);    //start stream
        await fileUploadForm.CopyToAsync(stream);   //copy file to stream location
        var results = new string[3];
        results[0] = fullFilePath;
        results[1] = fileUploadForm.FileName;
        results[2] = description;
        return results;
    }
}