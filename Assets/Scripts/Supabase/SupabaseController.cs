using UnityEngine;
using System.Threading.Tasks;
using Supabase;
using System;
public class SupabaseController : MonoBehaviour
{
    private static Client supabase;

    async void Start()
    {
        var url = "https://glvdcltimepfnwwchjvr.supabase.co";
        var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImdsdmRjbHRpbWVwZm53d2NoanZyIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjIxMjg3OTcsImV4cCI6MjA3NzcwNDc5N30.BQ7qAsYxYdjUdvK9YgzHbnsxlllkNRSj8A-GU7toy60";

        supabase = new Client(url, key);
        await supabase.InitializeAsync();

        Debug.Log("✅ Conectado a Supabase correctamente");
    }

    public static async Task<bool> CreateUser(string nombre, int edad, string email)
    {
        try
        {
            var user = new User
            {
                Name = nombre,
                Age = edad,
                Email = email,
                Coins = 0,
                PetPoints = 0
            };

            await supabase.From<User>().Insert(user);
            Debug.Log("🟢 Usuario registrado correctamente: " + nombre);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("Error al crear usuario: " + e.Message);
            return false;
        }
        
    }
}
