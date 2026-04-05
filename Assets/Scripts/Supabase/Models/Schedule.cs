using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System;

[Table("Schedules")]
public class Schedule : BaseModel
{
    [PrimaryKey("schedule_id", false)]
    public Guid ScheduleId { get; set; }

    [Column("medicine_id")]
    public Guid MedicineId { get; set; }

    [Column("time")]
    public TimeSpan Time { get; set; }

    [Column("weekday")]
    public int[] Weekday { get; set; }

}
