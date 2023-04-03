using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Produccion
{
    [Key]
    public int ProduccionId { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Today;
    public string Concepto { get; set; } = string.Empty;

    [ForeignKey("ProduccionId")]
    public virtual List<ProduccionDetalle> ProduccionDetalle { get; set; } = new List<ProduccionDetalle>();
    public int Cantidad { get; set; }
    public int ProductoId { get; set; }
}