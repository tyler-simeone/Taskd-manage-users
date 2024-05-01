public interface IRequestValidator
{
    bool ValidateGetUser(int userId);

    bool ValidateGetUsers();

    bool ValidateCreateUser(CreateUser createUserRequest);

    bool ValidateUpdateUser(UpdateUser updateUserRequest);

    bool ValidateDeleteUser(int userId);
}

public class RequestValidator : IRequestValidator
{
    public RequestValidator()
    {
        
    }

    public bool ValidateGetUser(int userId)
    {
        return true;
    }

    public bool ValidateGetUsers()
    {
        return true;
    }

    public bool ValidateCreateUser(CreateUser createUserRequest)
    {
        return true;
    }
    
    public bool ValidateUpdateUser(UpdateUser updateUserRequest)
    {
        return true;
    }
    
    public bool ValidateDeleteUser(int userId)
    {
        return true;
    }
}