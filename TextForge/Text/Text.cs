namespace TextForge.Text;

public class Text(string content)
{
    public int Id { get; set; }
    public string Content { get; set; } = content;
}
