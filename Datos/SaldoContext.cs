using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Datos
{
    public partial class SaldoContext : DbContext
    {
        public SaldoContext()
            : base("name=SaldoContext")
        {
        }

        public virtual DbSet<Saldo> Saldo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Saldo>()
                .Property(e => e.nombre_apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Saldo>()
                .Property(e => e.saldo1)
                .HasPrecision(18, 0);
        }
    }
}
