using Business;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_Practice
{
    public partial class Form1 : Form
    {
        SaldoBusiness saldoBusiness = new SaldoBusiness();
        private List<SaldoEntity> MiLista = new List<SaldoEntity>();
        public Form1()
        {
            InitializeComponent();
            LlenarGrilla();
        }

        public void LlenarGrilla()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = saldoBusiness.GetSaldoEntities();

            comboEmisor.DataSource = null;
            comboReceptor.DataSource = null;
            comboEmisor.DataSource = saldoBusiness.GetSaldoEntities();
            comboReceptor.DataSource = saldoBusiness.GetSaldoEntities();

            comboEmisor.ValueMember = "DNI";
            comboEmisor.DisplayMember = "NombreApellido";

            comboReceptor.ValueMember = "DNI";
            comboReceptor.DisplayMember = "NombreApellido";
        }
        private void btnCrear_Click(object sender, EventArgs e)
        {
            SaldoEntity saldoEntity = new SaldoEntity();

            saldoEntity.NombreApellido = txtNomApe.Text;
            saldoEntity.Saldo = Convert.ToDecimal(txtSaldo.Text);

            MiLista.Add(saldoEntity);

          //  saldoBusiness.CrearSaldo(saldoEntity);
            LlenarGrilla();
        }

        private void btnTransferir_Click(object sender, EventArgs e)
        {
            try
            {
                saldoBusiness.TransferirSaldo(Convert.ToInt32(comboEmisor.SelectedValue), Convert.ToInt32(comboReceptor.SelectedValue), Convert.ToDecimal(txtMonto.Text));
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LlenarGrilla();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            SaldoEntity saldoEntity = new SaldoEntity();

            saldoEntity.DNI = Convert.ToInt32(txtIDeliminar.Text);

            saldoBusiness.EliminarCuenta(saldoEntity);

            LlenarGrilla();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            SaldoEntity saldoEntity = new SaldoEntity();
            saldoEntity.DNI = Convert.ToInt32(txtIDModif.Text);
            saldoEntity.Saldo = Convert.ToDecimal(txtSaldoNuevo.Text);
            saldoBusiness.ModificarCuenta(saldoEntity);
            LlenarGrilla();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                saldoBusiness.ConfirmarLista(MiLista);
                MiLista.Clear();
                LlenarGrilla();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
