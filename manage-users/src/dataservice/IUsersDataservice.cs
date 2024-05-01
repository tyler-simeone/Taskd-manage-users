public interface IUsersDataservice 
{

    Task<User> GetUser(int userId);

    Task<UserList> GetUsers();
    
    void CreateUser(CreateUser createUserRequest);

    void UpdateUser(UpdateUser updateUserRequest);

    void DeleteUser(int userId, int updateUserId);
}