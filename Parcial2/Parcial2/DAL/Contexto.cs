using Microsoft.EntityFrameworkCore;


public class Contexto : DbContext
{
#nullable disable
    public DbSet<Productos> Productos { get; set; }
    public DbSet<Produccion> Produccion { get; set; }

    public DbSet<ProduccionDetalle> ProduccionDetalles { get; set; }

    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Productos>().HasData(
            new Productos { ProductoId = 5, Descripcion = "Mani", Costo = 10, Precio = 100, Existencia = 20 },
            new Productos { ProductoId = 2, Descripcion = "nuez", Costo = 10, Precio = 100, Existencia = 15 },
            new Productos { ProductoId = 3, Descripcion = "almendra", Costo = 10, Precio = 100, Existencia = 100 },
            new Productos { ProductoId = 4, Descripcion = "chia", Costo = 10, Precio = 100, Existencia = 10 },
            new Productos { ProductoId = 6, Descripcion = "pasas", Costo = 10, Precio = 100, Existencia = 10 }


        );
    }
}