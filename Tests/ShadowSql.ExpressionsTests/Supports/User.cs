namespace ShadowSql.ExpressionsTests.Supports;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string? Belief;
    public bool Status { get; set; }
}

public class UserParameter
{
    public int Id2 { get; set; }
    public string Name2 { get; set; }
    public int Age2 { get; set; }
    public int[] Items { get; set; }
}

public class UserRole
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
}

public class UserScore
{
    public int UserId { get; set; }
    public int Score { get; set; }
}