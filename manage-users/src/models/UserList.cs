public class UserList : ResponseBase
{
    public UserList()
    {
        Users = new List<User>();
    }

    public List<User> Users { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}