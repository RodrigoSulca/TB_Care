using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System;

[Table("Medicines")]
public class MedicineM : BaseModel
{
    [PrimaryKey("medicine_id", false)]
    public Guid MedicineId { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("notes")]
    public string Notes { get; set; }

    [Column("start_day")]
    public DateTime StartDay { get; set; }

}
