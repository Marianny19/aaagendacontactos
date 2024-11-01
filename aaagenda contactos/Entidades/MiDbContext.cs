﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;


public class MiDbContext : DbContext
{
    public DbSet<contacto> contactos { get; set; }
    public DbSet<agendaa> agendas { get; set; }
    public DbSet<red_social> red_social { get; set; }
    public DbSet<tipo_contacto> tipos_contacto { get; set; }
    public DbSet<teléfono> telefono { get; set; }
    public DbSet<tipo_red_social> tipos_red_social { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=contacto_agendaa;Username=user_crud;Password=Marianny19");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de relaciones entre entidades
        modelBuilder.Entity<agendaa>()
            .HasOne(a => a.Contacto)
            .WithMany(c => c.Agendas)
            .HasForeignKey(a => a.ID_contacto);

        modelBuilder.Entity<red_social>()
            .HasOne(r => r.Contacto)
            .WithMany(c => c.RedesSociales)
            .HasForeignKey(r => r.ID_contacto);

        modelBuilder.Entity<teléfono>()
            .HasOne(t => t.Contacto)
            .WithMany(c => c.Telefonos)
            .HasForeignKey(t => t.Id_contacto);
    }
}

// Entidad contacto
public class contacto
{
    [Key]
    public int ID_contacto { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    public int Tipo_Contacto { get; set; }
    public int Tipo_red_social { get; set; }

    // Relaciones
    public ICollection<agendaa> Agendas { get; set; }
    public ICollection<red_social> RedesSociales { get; set; }
    public ICollection<teléfono> Telefonos { get; set; }
}

// Entidad agendaa
public class agendaa
{
    [Key]
    public int ID_agenda { get; set; }
    public string Nombre_agenda { get; set; }
    public string Descripcion_agenda { get; set; }
    public DateTime Fecha_agendada { get; set; }
    public DateTime Hora_agendada { get; set; }
    public int ID_contacto { get; set; }

    // Relación
    public contacto Contacto { get; set; }
}

// Entidad red_social
public class red_social
{
    [Key]
    public int Id_red_social { get; set; }
    public string Nombre_de_usuario { get; set; }
    public int ID_contacto { get; set; }

    // Relación
    public contacto Contacto { get; set; }
}

// Entidad tipo_contacto
public class tipo_contacto
{
    [Key]
    public int ID_tipo_contacto { get; set; }
    public string Nombre_tipo_contacto { get; set; }
}

// Entidad teléfono
public class teléfono
{
    [Key]
    public int Id_telefono { get; set; }
    public string Número_de_teléfono { get; set; }
    public string Tipo_teléfono { get; set; }
    public int Id_contacto { get; set; }

    // Relación
    public contacto Contacto { get; set; }
}

// Entidad tipo_red_social
public class tipo_red_social
{
    [Key]
    public int Id_tipo_red_social { get; set; }
    public string Nombre_red_social { get; set; }
}