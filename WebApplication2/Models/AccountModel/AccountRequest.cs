using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.AccountModel
{
    public class AccountRequest
    {
        public string auxiliar { get; set; }
        public string moneda { get; set; }
        public string descripcion { get; set; }
        public List<AccountDetalle> detalle { get; set; }
    }
}