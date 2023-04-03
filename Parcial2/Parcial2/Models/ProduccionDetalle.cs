using System.ComponentModel.DataAnnotations;

public class ProduccionDetalle
{
    [Key]
    public int DetalleId { get; set; }
    
    public int ProductoId { get; set; }
    public int ProduccionId { get; set; }
    public int Cantidad { get; set; }
}