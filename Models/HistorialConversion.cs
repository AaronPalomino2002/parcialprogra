using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransaccionesApp.Models
{
    [Table("historialconversiones")]
    
    public class HistorialConversion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
    public int Id { get; set; }
    public decimal MontoUSD { get; set; }
    public decimal MontoBTC { get; set; }
    public decimal TasaConversion { get; set; }
    public DateTime Fecha { get; set; }
    }
}