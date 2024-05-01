public class User : ResponseBase
{
    public User()
    {
        
    }

    public int UserId { get; set; }

    public string UserEmail { get; set; }

    public DateTime CreateDatetime { get; set; }

    public int CreateUserId { get; set; }
    
    public DateTime UpdateDatetime { get; set; }

    public int UpdateUserId { get; set; }
}