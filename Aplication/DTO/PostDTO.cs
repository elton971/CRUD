namespace Aplication.DTO;

public class PostDTO
{
    
    public int  Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public  DateTimeOffset Creationdate { get; set; }
    public string UserName { get; set; }
}