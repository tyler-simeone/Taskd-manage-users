using manage_users.src.models;
using manage_users.src.models.requests;

namespace manage_users.src.repository
{
    public interface IUsersRepository
    {
        Task<User> GetUser(int userId);
        
        Task<User> GetUser(string email);

        Task<UserList> GetUsers();

        void CreateUser(CreateUser createUserRequest);

        void UpdateUser(UpdateUser updateUserRequest);

        void DeleteUser(int userId, int updateUserId);
    }
}