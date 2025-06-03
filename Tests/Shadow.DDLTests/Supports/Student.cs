namespace Shadow.DDLTests.Supports;

public class Student
{
    public Student()
    {
    }
    public Student(int id, string name, int age, int score)
    {
        Id = id;
        Name = name;
        Age = age;
        Score = score;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Score { get; set; }
}
