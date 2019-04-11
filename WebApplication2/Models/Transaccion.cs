using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace  WebApplication2.Models
{
    [Table("transaccion", Schema="public")]
    public class Transaccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("asientocontable")]
  /**/  [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Debe ser un numero!")]
        public int AsientoContable { get; set; }

        [Column("numerodocumento")]
        public string NumeroDocumento { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column("fecharealizada")]
        public DateTime FechaRealizada { get; set; }

        [DataType(DataType.Time)]
        [Column("horarelizada")]
        public string HoraRealizada { get; set; }

        [DataType(DataType.Currency)]
        [Column("montototal")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Debe ser un numero!")]
        public float MontoTotal { get; set; }


        public int idtipodocumento { get; set; }
        public int idcliente { get; set; }
        public int idtipomovimiento { get; set; }

        [ForeignKey("idtipodocumento")]
        public virtual TipoDocumento TipoDocumento { get; set; }

        [ForeignKey("idcliente")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("idtipomovimiento")]
        public virtual TipoMovimiento TipoMovimiento { get; set; }

    }
}