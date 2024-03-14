using AppDesktopEpi.Controller;
using AppDesktopEpi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDesktopEpi
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();            
        }        
        private void btnSair_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;            
            Funcionario func = new Funcionario();
            FunController controller = new FunController();
            List<Funcionario> li = controller.listaFuncionario();
            dgvFunc.DataSource = li;
        }
        private void btnNovaEntrega_Click(object sender, EventArgs e)
        {
            FrmEntrega entrega = new FrmEntrega();
            entrega.Show();
            this.Hide();
        }
        private void btnEpi3dias_Click(object sender, EventArgs e)
        {
            Funcionario func = new Funcionario();
            FunController controller = new FunController();
            List<Funcionario> li = controller.listaEpiAvencer();
            dgvFunc.DataSource = li;
        }

        private void btnEpiVencidas_Click(object sender, EventArgs e)
        {
            Funcionario func = new Funcionario();
            FunController controller = new FunController();
            List<Funcionario> li = controller.listaEpiVencida();
            dgvFunc.DataSource = li;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtMatricula.Text == "" || txtNome.Text == "" || txtEpi.Text == "" || txtDias.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int matricula = Convert.ToInt32(txtMatricula.Text.Trim());
                Funcionario func = new Funcionario();
                FunController controller = new FunController();
                var data_entrega = DateTime.Today;
                var data_vencimento = DateTime.Today.AddDays(Convert.ToInt32(txtDias.Text));
                controller.Atualizar(Convert.ToInt32(txtMatricula.Text), txtNome.Text, txtEpi.Text, data_entrega,data_vencimento);
                MessageBox.Show("Registro de entrega de EPI atualizada com sucesso!!", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Funcionario> funcionario = controller.listaFuncionario();
                dgvFunc.DataSource = funcionario;
                txtMatricula.Text = "";
                txtEpi.Text = "";
                txtDias.Text = "";
                txtNome.Text = "";
                this.txtNome.Focus();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int matricula = Convert.ToInt32(txtMatricula.Text.Trim());                
                FunController controller = new FunController();
                controller.Excluir(matricula);
                MessageBox.Show("Registro de entrega de EPI excluída com sucesso!!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Funcionario> funcionarios = controller.listaFuncionario();
                dgvFunc.DataSource = funcionarios;
                txtMatricula.Text = "";
                txtNome.Text = "";
                txtEpi.Text = "";
                lblDataEntrega.Text = "";
                lblDataVencimento.Text = "";
                this.txtMatricula.Focus();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }     
        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            try
            {
                int matricula = Convert.ToInt32(txtMatricula.Text.Trim());
                
                FunController controller = new FunController();
                Funcionario func = controller.Localizar(matricula);
                txtNome.Text = func.nome;
                txtEpi.Text = func.epi;
                lblDataEntrega.Text = func.data_entrega.ToString("dd/MM/yyyy");
                lblDataVencimento.Text = func.data_vencimento.ToString("dd/MM/yyyy");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvFunc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvFunc.Rows[e.RowIndex];
                this.dgvFunc.Rows[e.RowIndex].Selected = true;
                txtMatricula.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                txtEpi.Text = row.Cells[2].Value.ToString();
                lblDataEntrega.Text = Convert.ToDateTime(row.Cells[3].Value).ToString("dd/MM/yyyy");
                lblDataVencimento.Text = Convert.ToDateTime(row.Cells[4].Value).ToString("dd/MM/yyyy");
                
            }
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }
    }
}

