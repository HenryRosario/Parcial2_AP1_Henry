using Microsoft.EntityFrameworkCore;


public class Contexto: DbContext
{
    public DbSet<Productos> productos  { get; set; }
   
    public Contexto (DbContextOptions <Contexto> options): base(options) 
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Productos>().HasData(
            new Productos { ProductoId = 5, Descripcion = "Mani", Costo = 10,Precio =100, Existencia =0 },
            new Productos { ProductoId = 2, Descripcion = "nuez", Costo = 10,Precio =100, Existencia =0 },
            new Productos { ProductoId = 3, Descripcion = "almendra", Costo = 10,Precio =100, Existencia =0 },
            new Productos { ProductoId = 4, Descripcion = "chia", Costo = 10,Precio =100, Existencia =0 }
     
       
        );
    }
}