namespace ShadowSqlBench.Supports;

public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string Content { get; set; }
    /// <summary>
    /// 是否精选
    /// </summary>
    public bool Pick { get; set; }
    public int Hits { get; set; }
}
