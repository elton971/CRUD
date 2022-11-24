using System.Text.Json;
using Doiman;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class LoadSeeds
{
    public static void SeedAsync(UserManager<User> userManager)
    {
        try
        {
            if (!userManager.Users.Any())
            {
                var userFile = File.ReadAllText("../Persistence/Seeds/UserSeeds.json");
                var user = JsonSerializer.Deserialize<User>(userFile);
                
                var result2=userManager.CreateAsync(new User
                {
                    Email =user.Email,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber
                }, "P@$$w0rd").GetAwaiter().GetResult();

                if (result2.Errors is not null)
                {
                    throw new Exception("erro "+result2.Errors);
                }

            }
        }
        catch (Exception e)
        { 
            throw new Exception(e.Message);
        }
    }
}