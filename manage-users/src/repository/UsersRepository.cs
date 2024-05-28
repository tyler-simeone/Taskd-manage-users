using manage_users.src.dataservice;
using manage_users.src.models;
using manage_users.src.models.requests;

namespace manage_users.src.repository
{
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        
        public async Task<User> GetUser(string email)
        {
            try
            {
                User user = await _usersDataservice.GetUser(email);
                return user;
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}