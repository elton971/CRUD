namespace Aplication.Dtos;

public class PostDto
{
    public int  Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public  DateTimeOffset Creationdate { get; set; }
    public string UserName { get; set; }
    public string Description { get; set; }
}