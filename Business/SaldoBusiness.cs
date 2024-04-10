using Datos;
using Entity;
using System.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business
{
    public class SaldoBusiness
    {
        private SaldoDAO saldoDAO = new SaldoDAO();

        public List<SaldoEntity> GetSaldoEntities()
        {

            return saldoDAO.GetSaldos();
        }

        public void CrearSaldo(SaldoEntity saldoEntity)
        {
            saldoDAO.NuevaCuenta(saldoEntity);
        }

        //Transaction Scope, como settear sus caracteristicas y como utilizarlo.
        public void TransferirSaldo(int idEmi, int idRec, decimal monto)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.ReadCommitted;
            options.Timeout = TimeSpan.FromMinutes(5);

            using(TransactionScope trx = new TransactionScope(TransactionScopeOption.Required, options))
            {
                saldoDAO.Transferir(idEmi, idRec, monto);

                if (monto <= 0)
                {
                    throw new Exception("La cantidad de dinero transferido no puede ser 0");
                }

                trx.Complete();
            }
            
        }

        public void EliminarCuenta(SaldoEntity saldoEntity) 
        {
            saldoDAO.EliminarCuenta(saldoEntity);
        }

        public void ModificarCuenta(SaldoEntity saldoEntity)
        {
            saldoDAO.ModificarCuenta(saldoEntity);
        }

        public void ConfirmarLista(List<SaldoEntity> lista)
        {
            using(TransactionScope trx = new TransactionScope())
            {
                foreach(SaldoEntity saldo in lista)
                {
                    CrearSaldo(saldo);
                }

                trx.Complete();
            }
        }
    }
}
