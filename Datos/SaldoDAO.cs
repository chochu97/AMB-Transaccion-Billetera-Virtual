using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class SaldoDAO
    {

        // Consultar la base de datos con Entity Framework
        public List<SaldoEntity> GetSaldos()
        {
            SaldoContext context = new SaldoContext();

            List<Saldo> saldos = context.Saldo.ToList();

            return saldos.Select(saldo => new SaldoEntity
            {
                DNI = saldo.dni,
                NombreApellido = saldo.nombre_apellido,
                Saldo = saldo.saldo1,
            }).ToList();

        }

        // Crear un objeto en la base de datos con Entity Framework
        public void NuevaCuenta(SaldoEntity saldito)
        {
            Saldo saldo = new Saldo();

            saldo.dni = saldito.DNI;
            saldo.nombre_apellido = saldito.NombreApellido;
            saldo.saldo1 = Convert.ToDecimal(saldito.Saldo);

            using(SaldoContext context = new SaldoContext())
            {
                context.Saldo.Add(saldo);
                context.SaveChanges();
            }
        }

        // Transferencia con Entity Framework. Transferencia con Transactions.
        public void Transferir(int idEmisor, int idReceptor, decimal monto)
        {

            using(SaldoContext context = new SaldoContext())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        Saldo saldoEmisor = context.Saldo.FirstOrDefault(saldo => saldo.dni == idEmisor);
                        saldoEmisor.saldo1 = (saldoEmisor.saldo1 - monto);

                        Saldo saldoReceptor = context.Saldo.FirstOrDefault(saldo => saldo.dni == idReceptor);
                        saldoReceptor.saldo1 = (saldoReceptor.saldo1 + monto);

                        context.SaveChanges();
                        transaction.Commit();

                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        // Eliminar un objeto de la base de datos con Entity Framework
        public void EliminarCuenta(SaldoEntity saldoEntity)
        {
            using(SaldoContext context = new SaldoContext())
            {
                Saldo saldito = context.Saldo.FirstOrDefault(saldo => saldo.dni == saldoEntity.DNI);
                context.Saldo.Remove(saldito);
                context.SaveChanges();

            }
        }

        // Modificar un objeto de la base de datos con Entity Framework
        public void ModificarCuenta(SaldoEntity saldoEntity)
        {
            using(SaldoContext context = new SaldoContext())
            {
                Saldo saldito = context.Saldo.FirstOrDefault(saldo => saldo.dni == saldoEntity.DNI);
                saldito.saldo1 = saldoEntity.Saldo;
                context.SaveChanges();
            }
        }
    }
}
