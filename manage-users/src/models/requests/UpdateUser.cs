namespace manage_users.src.models.requests
{
    public class UpdateUser
    {
        public UpdateUser()
        {

        }

        public int UserId { get; set; }

        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public int UpdateUserId { get; set; }
    }
}