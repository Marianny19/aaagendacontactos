using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MiDbContext : DbContext
{
    public DbSet<contacto> Contactos { get; set; }
    public DbSet<agenda> Agendas { get; set; }
    public DbSet<red_social> RedesSociales { get; set; }
    public DbSet<tipo_contacto> TiposContacto { get; set; }
    public DbSet<teléfono> Telefonos { get; set; }
    public DbSet<tipo_red_social> TiposRedSocial { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=contacto_agendaa;Username=user_crud;Password=Marianny19");
    }
}

public class contacto
{
    [Key]
    public int ID_contacto { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; }

    [Required, MaxLength(50)]
    public string Apellido { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public int ID_Tipo_Contacto { get; set; }

    public int ID_Tipo_red_social { get; set; }


    [ForeignKey(nameof(ID_Tipo_Contacto))]
    public tipo_contacto TipoContacto { get; set; }

    [ForeignKey(nameof(ID_Tipo_red_social))]
    public tipo_red_social TipoRedSocial { get; set; }
}

public class agenda
{
    [Key]
    public int ID_agenda { get; set; }

    [Required, MaxLength(100)]
    public string Nombre_agenda { get; set; }

    public string Descripcion_agenda { get; set; }
    public DateTime Fecha_agendada { get; set; }
    public DateTime Hora_agendada { get; set; }

}

public class red_social
{
    [Key]
    public int Id_red_social { get; set; }

    [Required, MaxLength(50)]
    public string Nombre_de_usuario { get; set; }

    public int ID_contacto { get; set; }

   
    [ForeignKey(nameof(ID_contacto))]
    public contacto Contacto { get; set; }
}

public class tipo_contacto
{
    [Key]
    public int ID_tipo_contacto { get; set; }

    public string Nombre_tipo_contacto { get; set; }
}

public class teléfono
{
    [Key]
    public int Id_telefono { get; set; }

    [Required, MaxLength(15)]
    public string Número_de_teléfono { get; set; }

    [MaxLength(20)]
    public string Tipo_teléfono { get; set; }

    public int Id_contacto { get; set; }

    [ForeignKey(nameof(Id_contacto))]
    public contacto Contacto { get; set; }
}

public class tipo_red_social
{
    [Key]
    public int Id_tipo_red_social { get; set; }

    [Required, MaxLength(50)]
    public string Nombre_red_social { get; set; }
}
