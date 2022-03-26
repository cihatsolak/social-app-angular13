namespace ServerApp.Data
{
    public static class SeedDatabase
    {
        public static async Task Seed(UserManager<User> userManager)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var users = File.ReadAllText("Data/users.json");
                var listOfUsers = JsonSerializer.Deserialize<List<User>>(users);

                foreach (var user in listOfUsers)
                {
                    await userManager.CreateAsync(user, "Password123+-");
                }
            }
        }
    }
}
