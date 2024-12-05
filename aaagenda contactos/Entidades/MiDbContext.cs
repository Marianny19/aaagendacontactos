using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public partial class MiDbContext : DbContext
{
    public DbSet<contacto> contactos { get; set; }
    public DbSet<agenda> agendas { get; set; }
    public DbSet<red_social> red_sociall { get; set; }
    public DbSet<tipo_contacto> tipos_contacto { get; set; }
    public DbSet<teléfono> telefono { get; set; }
    public DbSet<tipo_red_social> tipos_red_social { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=contacto_agendaa;Username=user_crud;Password=miguelencristo01");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<contacto>()
            .HasMany(c => c.Teléfonos)
            .WithOne(t => t.Contacto)
            .HasForeignKey(t => t.Id_contacto);

    }

    public class contacto
    {
        [Key]
        public int ID_contacto { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int Tipo_Contacto { get; set; }

        [ForeignKey(nameof(Tipo_Contacto))]
        public tipo_contacto TipoContacto { get; set; }

        public int Tipo_red_social { get; set; }

        [ForeignKey(nameof(Tipo_red_social))]
        public tipo_red_social TipoRedSocial { get; set; }

        public ICollection<teléfono> Teléfonos { get; set; }

        [NotMapped]
        public string NúmerosDeTeléfono
        {
            get
            {
                return Teléfonos != null && Teléfonos.Any()
                    ? string.Join(", ", Teléfonos.Select(t => t.Número_de_teléfono))
                    : "Sin teléfonos";
            }
        }
    }

    public class agenda
    {
        [Key]
        public int ID_agenda { get; set; }
        public string Nombre_agenda { get; set; }
        public string Descripcion_agenda { get; set; }
        public DateTime Fecha_agendada { get; set; }
        public int ID_contacto { get; set; }

        [ForeignKey(nameof(ID_contacto))]
        public contacto IDContacto { get; set; }
    }

    public class red_social
    {
        [Key]
        public int Id_red_social { get; set; }
        public string Nombre_de_usuario { get; set; }
        public int ID_contacto { get; set; }
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
        public string Número_de_teléfono { get; set; }
        public string Tipo_teléfono { get; set; }
        public int Id_contacto { get; set; }

        [ForeignKey(nameof(Id_contacto))]
        public contacto Contacto { get; set; }
    }

    public class tipo_red_social
    {
        [Key]
        public int Id_tipo_red_social { get; set; }
        public string Nombre_red_social { get; set; }
    }
}
