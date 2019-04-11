using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication2.Utils;

namespace WebApplication2.Models
{
    [Table("cliente", Schema ="public")]
    public class Cliente
    {

        public Cliente()
        {
            this.Transaccions = new HashSet<Transaccion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("identificacion")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Debe ser un numero!")]
        [Cedula]
        public string Identificacion { get; set; }
        	
        [Column("nombres")]
        public string Nombres { get; set; }

        [Column("apellidos")]
        public string Apellidos { get; set; }

        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Email invalido")]
        [Column("correo")]
        public string Correo { get; set; }

        [Column("telefono")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Debe ser un numero!")]
        public string Telefono { get; set; }

        [Column("celular")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Debe ser un numero!")]
        public string Celular { get; set; }

        [DataType(DataType.Currency)]
        [Column("limitecredito")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Debe ser un numero!")]
        public float LimiteCredito { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }
        
        public virtual ICollection<Transaccion> Transaccions { get; set; }
    }
}