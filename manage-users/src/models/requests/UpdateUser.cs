namespace manage_users.src.models.requests
{
    public class UpdateUser
    {
        public UpdateUser()
        {

        }

        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public int UpdateUserId { get; set; }
    }
}