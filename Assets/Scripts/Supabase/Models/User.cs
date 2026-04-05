using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System;

[Table("Users")]
public class User : BaseModel
{
    [PrimaryKey("user_id", true)]
    public Guid UserId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("age")]
    public int Age { get; set; }
    [Column("email")]
    public string Email { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
