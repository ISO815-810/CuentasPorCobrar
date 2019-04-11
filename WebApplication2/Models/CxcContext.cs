using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class PGDbContext : DbContext
    {
        public PGDbContext() : base(nameOrConnectionString: "DefaultConnectionString") { }

        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<CuentaContable> CuentaContable { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoMovimiento> TipoMovimiento { get; set; }
        public virtual DbSet<Transaccion> Transaccion { get; set; }
    }
}