namespace manage_users.src.models.requests
{
    public class CreateUser
    {
        public CreateUser()
        {

        }

        public string UserEmail { get; set; }

        public int CreateUserId { get; set; }
    }
}