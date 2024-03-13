using AppDesktopEpi.Controller;
using AppDesktopEpi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            btnFinalizarEntrega.Enabled = false;
            btnNovoItem.Enabled = false;
            btnEditarItem.Enabled = false;
            btnExcluirItem.Enabled = false;
            Funcionario func = new Funcionario();
            FunController controller = new FunController();
            List<Funcionario> li = controller.listaFuncionario();
            dgvFunc.DataSource = li;
        }

        private void btnNovaEntrega_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnFinalizarEntrega.Enabled = true;
            btnNovoItem.Enabled = true;            
            dgvFunc.Columns.Clear();
            dgvFunc.Columns.Add("Matricula", "matricula");
            dgvFunc.Columns.Add("Nome", "nome");
            dgvFunc.Columns.Add("EPI", "epi");
            dgvFunc.Columns.Add("Data Entrega", "data_entrega");
            dgvFunc.Columns.Add("Data Vencimento", "data_vencimento");
        }

        private void btnNovoItem_Click(object sender, EventArgs e)
        {
            if (txtDias.Text == "" || txtEpi.Text == "" || txtNome.Text == "")
            {
                MessageBox.Show("Por favor, preencha campos 'NOME','TIPO EPI','VALIDADE'!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var repetido = false;
                foreach (DataGridViewRow dr in dgvFunc.Rows)
                {
                    if (txtEpi.Text == Convert.ToString(dr.Cells[0].Value))
                    {
                        repetido = true;
                    }
                }
                if (repetido == false)
                {
                    DataGridViewRow item = new DataGridViewRow();
                    item.CreateCells(dgvFunc);
                    item.Cells[0].Value = txtMatricula.Text;
                    item.Cells[1].Value = txtNome.Text;
                    item.Cells[2].Value = txtEpi.Text;
                    item.Cells[3].Value = DateTime.Today.ToString();
                    item.Cells[4].Value = DateTime.Today.AddDays(Convert.ToInt32(txtDias.Text)).ToString();
                    dgvFunc.Rows.Add(item);                    
                    txtEpi.Text = "";
                    txtDias.Text = "";
                }
                else
                {
                    MessageBox.Show("EPI já Cadastrada!!", "EPI Repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {

                throw;
            }
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
            try
            {
                int matricula = Convert.ToInt32(txtMatricula.Text.Trim());
                Funcionario func = new Funcionario();
                FunController controller = new FunController();
                var data_entrega = DateTime.Today;
                var data_vencimento = DateTime.Today.AddDays(Convert.ToDouble(txtDias.Text));
                controller.Atualizar(Convert.ToInt32(txtMatricula.Text), txtNome.Text, txtEpi.Text, data_entrega,data_vencimento);
                MessageBox.Show("Registro de entrega de EPI atualizada com sucesso!!", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Funcionario> funcionario = controller.listaFuncionario();
                dgvFunc.DataSource = funcionario;
                txtMatricula.Text = "";
                txtEpi.Text = "";
                txtDias.Text = "";
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

        private void btnEditarItem_Click(object sender, EventArgs e)
        {
            if (txtMatricula.Text == "" || txtNome.Text == "" || txtEpi.Text == "" || txtDias.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int linha = dgvFunc.CurrentRow.Index;
                dgvFunc.Rows[linha].Cells[0].Value = txtMatricula.Text;
                dgvFunc.Rows[linha].Cells[1].Value = txtNome.Text;
                dgvFunc.Rows[linha].Cells[2].Value = txtEpi.Text;
                dgvFunc.Rows[linha].Cells[3].Value = DateTime.Today.ToString();
                dgvFunc.Rows[linha].Cells[4].Value = DateTime.Today.AddDays(Convert.ToDouble(txtDias.Text)).ToString();
                txtMatricula.Text = "";
                txtNome.Text = "";
                txtEpi.Text = "";
                txtDias.Text = "";
                lblDataEntrega.Text = "";
                lblDataVencimento.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Edição", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (txtMatricula.Text == "" || txtNome.Text == "" || txtEpi.Text == "" || txtDias.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int linha = dgvFunc.CurrentRow.Index;
                dgvFunc.Rows.RemoveAt(linha);
                dgvFunc.Refresh();
                txtMatricula.Text = "";
                txtEpi.Text = "";
                txtDias.Text = "";
                lblDataEntrega.Text = "";
                lblDataVencimento.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Edição", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFinalizarEntrega_Click(object sender, EventArgs e)
        {
            foreach (DataGridView dr in dgvFunc.Rows )
            {
                FunController controller = new FunController();
                int linha = dgvFunc.CurrentRow.Index;
                int matricula = Convert.ToInt32(dgvFunc[linha,0]);
                string nome = Convert.ToString(dgvFunc[linha,1]);
                string epi = Convert.ToString(dgvFunc[linha, 2]);
                var data_entrega = Convert.ToDateTime(dgvFunc[linha,3]);
                var data_vencimento = Convert.ToDateTime(dgvFunc[linha, 4]);
                controller.Inserir(matricula,nome,epi,data_entrega,data_vencimento);
            }
            MessageBox.Show("Entrega efetuada com Sucesso!!", "Entrega EPI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnFinalizarEntrega.Enabled = false;
            btnNovoItem.Enabled = false;
            FunController controll = new FunController();
            List<Funcionario> li = controll.listaFuncionario();
            dgvFunc.DataSource = li;
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            try
            {
                int matricula = Convert.ToInt32(txtMatricula.Text.Trim());
                Funcionario func = new Funcionario();
                FunController controller = new FunController();
                controller.Localizar(matricula);
                txtNome.Text = func.nome;
                txtEpi.Text = func.epi;
                lblDataEntrega.Text = func.data_entrega.ToString();
                lblDataVencimento.Text = func.data_vencimento.ToString();
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
                lblDataEntrega.Text = row.Cells[3].Value.ToString();
                lblDataVencimento.Text = row.Cells[4].Value.ToString();
                String.Format("", lblDataEntrega);
                String.Format("", lblDataVencimento);
            }
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }
    }
}

