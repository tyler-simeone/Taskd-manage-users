namespace manage_users.src.models
{
    public class UserList : ResponseBase
    {
        public UserList()
        {
            Users = [];
        }

        public List<User> Users { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}