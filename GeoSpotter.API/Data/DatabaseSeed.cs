using GeoSpotter.API.Entities;

namespace GeoSpotter.API.Data;

public static class DatabaseSeed
{
    public static List<User> GetUsers()
    {
        return
        [
            new User
            {
                Id = 1,
                UserName = "user1",
                Password = "password1"
            },
            new User
            {
                Id = 2,
                UserName = "user2",
                Password = "password2"
            }
        ];
    }
}
