using Zeus.Api.Domain.Integrations.OpenAi.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.OpenAi;

public class OpenAiUser : Entity<OpenAiUserId>
{
    public OpenAiUser(OpenAiUserId id, string obj, string name, string email, string role, DateTime addedAt) : base(id)
    {
        Object = obj;
        Name = name;
        Email = email;
        Role = role;
        AddedAt = addedAt;
    }

    public string Object { get; }
    public string Name { get; }
    public string Email { get; }
    public string Role { get; }
    public DateTime AddedAt { get; }
}
