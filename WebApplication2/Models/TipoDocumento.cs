using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("tipodocumento", Schema = "public")]
    public class TipoDocumento
    {
        public TipoDocumento()
        {
            this.Transaccions = new HashSet<Transaccion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        //Foreign key section
        public int idestado { get; set; }
        public int idcuentacontable { get; set; }

        [ForeignKey("idestado")]
        public virtual Estado Estado { get; set; }

        [ForeignKey("idcuentacontable")]
        public virtual CuentaContable CuentaContable { get; set; }

        public virtual ICollection<Transaccion> Transaccions { get; set; }

    }
}