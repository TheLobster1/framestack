using framestack.Models;

namespace framestack.Services;

public class LocalUserStorage
{
    public LocalUserStorage()
    {
        
    }
    public User User { get; set; }
    public List<Picture> Pictures { get; set; }
    public List<Album> Albums { get; set; }
    public List<Tag> Tags { get; set; }
    public List<User> Users { get; set; }
}