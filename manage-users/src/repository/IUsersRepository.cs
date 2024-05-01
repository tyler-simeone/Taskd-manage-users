public interface IUsersRepository 
{
    Task<User> GetUser(int userId);

    Task<UserList> GetUsers();
    
    void CreateUser(CreateUser createUserRequest);

    void UpdateUser(UpdateUser updateUserRequest);

    void DeleteUser(int userId, int updateUserId);
}