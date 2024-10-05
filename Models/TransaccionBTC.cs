using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransaccionesApp.Models
{
    [Table("transaccionesbtc")]
    public class TransaccionBTC
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
    public int Id { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }
    }
}