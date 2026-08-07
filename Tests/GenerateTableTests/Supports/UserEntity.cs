using Hand.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenerateTableTests.Supports;

public class UserEntity(UserId id, UserName name)
    : IEntity<UserId>
{
    public UserId Id { get; } = id;
    [DefaultValue("")]
    public UserName Name { get; } = name;
}

public record struct UserId(long Original) : IEntityId;
public record struct UserName(string Original) : IEntityProperty<string>;