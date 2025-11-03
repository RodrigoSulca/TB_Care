using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System;

[Table("usuario")]
public class User : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("nombre")]
    public string Name { get; set; }

    [Column("edad")]
    public int Age { get; set; }
    [Column("email")]
    public string Email { get; set; }

    [Column("monedas")]
    public int Coins { get; set; }

    [Column("puntos_mascota")]
    public int PetPoints { get; set; }
}
