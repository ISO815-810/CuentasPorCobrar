using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("cuentacontable", Schema = "public")]
    public class CuentaContable
    {
        public CuentaContable()
        {
            this.TipoDocumentos = new HashSet<TipoDocumento>();
        }

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("codigo")]
        public int Codigo { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        
        public virtual ICollection<TipoDocumento> TipoDocumentos { get; set; }
    }
}