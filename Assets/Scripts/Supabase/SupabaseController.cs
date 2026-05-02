using UnityEngine;
using System.Threading.Tasks;
using Supabase;
using System;
using System.Collections.Generic;
public class SupabaseController : MonoBehaviour
{
    public static Guid userId;
    private static Client supabase;
    public static SupabaseController Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    async void Start()
    {
        var url = "https://gvgtbtktmztkxplsjpqf.supabase.co";
        var key = "sb_publishable_gAA8Z480_O30CW8Gb3GAcQ_ZcWBhMyg";

        supabase = new Client(url, key);
        await supabase.InitializeAsync();

        Debug.Log("✅ Conectado a Supabase correctamente");
    }

    public static async Task<bool> CreateUser(string name, int age, string email, string password, DateTime created_at)
    {
        var session = await supabase.Auth.SignUp(email, password);

        var authUser = session.User;

        if (authUser == null)
        {
            Debug.LogError("❌ Error al registrar en Auth");
            return false;
        }
        userId = Guid.Parse(authUser.Id);
        try
        {
            var user = new User
            {
                UserId = userId,
                Name = name,
                Age = age,
                Email = email,
                CreatedAt = created_at
            };
            
            await supabase.From<User>().Insert(user);
            Debug.Log("🟢 Usuario registrado correctamente: " + name);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("Error al crear usuario: " + e.Message);
            return false;
        }
        
    }

    public static async Task<bool> CreateMedicine(string name, string notes, DateTime startDay, List<ScheduleObj> schedules)
    {
        try
        {
            var medicine = new MedicineM
            {
                UserId = userId,
                Name = name,
                Notes = notes,
                StartDay = startDay
            };
            var medResponse = await supabase.From<MedicineM>().Insert(medicine);
            var createdMed = medResponse.Models[0];
            Debug.Log("Medicine created");
            foreach(ScheduleObj schedule in schedules)
            {
                await CreateSchedule(createdMed.MedicineId,schedule.time,schedule.weekdays);
            }
            return true;
        }
        catch(Exception e)
        {
            Debug.Log("Error: " + e.Message);
            return false;
        }
    }

    public static async Task<bool> CreateSchedule(Guid medicineId, string hour, int[] weekdays)
    {
        try
        {
            var schedule = new Schedule
            {
                MedicineId = medicineId,
                Time = hour,
                Weekday = weekdays
            };

            await supabase.From<Schedule>().Insert(schedule);
            Debug.Log("Schedule created");
            return true;
        }
        catch(Exception e)
        {
            Debug.Log("Error: " + e.Message);
            return false;
        }
    }

    public static async Task<bool> Login(string email, string password)
    {
        try
        {
            var session = await supabase.Auth.SignIn(email, password);

            if (session?.User != null)
            {
                Debug.Log("🟢 Login exitoso: " + session.User.Email);

                // 🔑 ID del usuario autenticado
                Guid authUserId = Guid.Parse(session.User.Id);

                // 👉 Buscar datos en tu tabla "usuario"
                var response = await supabase
                    .From<User>()
                    .Where(x => x.UserId == authUserId)
                    .Get();

                if (response.Models.Count > 0)
                {
                    var userData = response.Models[0];
                    Debug.Log("👤 Usuario cargado: " + userData.Name);
                    userId = Guid.Parse(session.User.Id);
                }
                else
                {
                    Debug.LogWarning("⚠️ Usuario no existe en tabla 'usuario'");
                }

                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            Debug.LogError("❌ Error en login: " + e.Message + $"{email},{password}");
            return false;
        }
    }

    public static async Task<List<Medicine>> GetUserMedicines()
    {
            var medicines = new List<Medicine>();

            // 1. Obtener medicinas del usuario
            var medicineResponse = await supabase
                .From<MedicineM>() // modelo DB
                .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, supabase.Auth.CurrentUser.Id)
                .Get();

            foreach (var med in medicineResponse.Models)
            {
                Medicine medicine = new()
                {
                    name = med.Name,
                    startDay = med.StartDay,
                    schedules = new List<ScheduleObj>()
                };
                Debug.Log(medicine.name);

                // 2. Obtener schedules de esa medicina
                var scheduleResponse = await supabase
                    .From<Schedule>()
                    .Where(s => s.MedicineId == med.MedicineId)
                    .Get();

                foreach (var sch in scheduleResponse.Models)
                {
                    medicine.schedules.Add(new ScheduleObj
                    {
                        time = sch.Time,
                        weekdays = sch.Weekday
                    });
                }

                medicines.Add(medicine);
            }
            return medicines;
    }
}
