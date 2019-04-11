using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("estado", Schema = "public")]
    public class Estado
    {
        public Estado()
        {
            this.TipoDocumentos = new HashSet<TipoDocumento>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        public virtual ICollection<TipoDocumento> TipoDocumentos { get; set; }
    }
}