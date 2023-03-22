using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;

public class ProductosDetalles
{
    [Key]
    public int ProductoDetalleId { get; set; }

    public DateTime Fecha { get; set; }

    public string? Concepto { get; set; }

    public int ProductoId { get; set; }

    public string? Descripcion { get; set; }

    public int CantidadProducto { get; set; }
    
    public decimal PesoTotal { get; set; }
}