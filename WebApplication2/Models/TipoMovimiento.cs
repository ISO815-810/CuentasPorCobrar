using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("tipomovimiento", Schema = "public")]
    public class TipoMovimiento
    {
        public TipoMovimiento()
        {
            this.Transaccions = new HashSet<Transaccion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        public virtual ICollection<Transaccion> Transaccions { get; set; }
    }
}