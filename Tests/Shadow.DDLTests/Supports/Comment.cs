namespace Shadow.DDLTests.Supports;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string Content { get; set; }
    /// <summary>
    /// 是否精选
    /// </summary>
    public bool Pick { get; set; }
}
