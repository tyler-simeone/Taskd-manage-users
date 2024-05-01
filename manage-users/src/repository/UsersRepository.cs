public class UsersRepository : IUsersRepository
{
    IUsersDataservice _usersDataservice;

    public UsersRepository(IUsersDataservice usersDataservice)
    {
        _usersDataservice = usersDataservice;
    }

    public async Task<User> GetUser(int userId)
    {
        try
        {
            User user = await _usersDataservice.GetUser(userId);
            return user;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<UserList> GetUsers()
    {
        try
        {
            UserList userList = await _usersDataservice.GetUsers();
            return userList;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
    
    public void CreateUser(CreateUser createUserRequest)
    {
        try
        {
            _usersDataservice.CreateUser(createUserRequest);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public void UpdateUser(UpdateUser updateUserRequest)
    {
        try
        {
            _usersDataservice.UpdateUser(updateUserRequest);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public void DeleteUser(int userId, int updateUserId)
    {
        try
        {
            _usersDataservice.DeleteUser(userId, updateUserId);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
}