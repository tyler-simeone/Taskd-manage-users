using manage_users.src.models.requests;

namespace manage_users.src.util
{
    public interface IRequestValidator
    {
        bool ValidateGetUser(int userId);
        
        bool ValidateGetUser(string email);

        bool ValidateGetUsers();

        bool ValidateCreateUser(CreateUser createUserRequest);

        bool ValidateUpdateUser(UpdateUser updateUserRequest);

        bool ValidateDeleteUser(int userId, int updateUserId);
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
        
        public bool ValidateGetUser(string email)
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

        public bool ValidateDeleteUser(int userId, int updateUserId)
        {
            if (userId == 0 || updateUserId == 0)
                return false;

            return true;
        }
    }
}