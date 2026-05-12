namespace MiApp.Domain.Entities;

public class Cliente
{
    // ID principal para la base de datos
    public int Id { get; set; }

    // Datos personales
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    
    // Datos de contacto
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;

    // Estado del cliente (para saber si fue dado de baja o no)
    public bool Activo { get; set; } = true; // Por defecto, cuando se crea, está activo
}