using AppDesktopEpi.Controller;
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
    public partial class FrmEntrega : Form
    {
        public FrmEntrega()
        {
            InitializeComponent();
        }

        private void btnNovoItem_Click(object sender, EventArgs e)
        {
            if (txtDias.Text == "" || txtEpi.Text == "" || txtNome.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    item.Cells[0].Value = txtNome.Text;
                    item.Cells[1].Value = txtEpi.Text;
                    item.Cells[2].Value = DateTime.Today.ToString();
                    item.Cells[3].Value = DateTime.Today.AddDays(Convert.ToInt32(txtDias.Text)).ToString();
                    dgvFunc.Rows.Add(item);
                    txtEpi.Text = "";
                    txtDias.Text = "";
                }
                else
                {
                    MessageBox.Show("EPI já Cadastrada!!", "EPI Repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmEntrega_Load(object sender, EventArgs e)
        {
            btnEditarItem.Enabled = false;
            btnExcluirItem.Enabled = false;            
            dgvFunc.Columns.Clear();
            dgvFunc.Columns.Add("Matricula", "matricula");
            dgvFunc.Columns.Add("Nome", "nome");
            dgvFunc.Columns.Add("EPI", "epi");
            dgvFunc.Columns.Add("Data Entrega", "data_entrega");
            dgvFunc.Columns.Add("Data Vencimento", "data_vencimento");
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtEpi.Text == "" || txtDias.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int linha = dgvFunc.CurrentRow.Index;
                dgvFunc.Rows.RemoveAt(linha);
                dgvFunc.Refresh();                
                txtEpi.Text = "";
                txtDias.Text = "";                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Edição", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditarItem_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtEpi.Text == "" || txtDias.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int linha = dgvFunc.CurrentRow.Index;                
                dgvFunc.Rows[linha].Cells[0].Value = txtNome.Text;
                dgvFunc.Rows[linha].Cells[1].Value = txtEpi.Text;
                dgvFunc.Rows[linha].Cells[2].Value = DateTime.Today.ToString();
                dgvFunc.Rows[linha].Cells[3].Value = DateTime.Today.AddDays(Convert.ToDouble(txtDias.Text)).ToString();
                txtEpi.Text = "";
                txtDias.Text = ""; 
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Edição", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {            
            FrmPrincipal principal = new FrmPrincipal();
            principal.Show();
            this.Close();
        }

        private void btnFinalizarEntrega_Click(object sender, EventArgs e)
        {
            foreach (DataGridView dr in dgvFunc.Rows)
            {
                FunController controller = new FunController();
                int linha = dgvFunc.CurrentRow.Index;
                int matricula = Convert.ToInt32(dgvFunc[linha, 0]);
                string nome = Convert.ToString(dgvFunc[linha, 1]);
                string epi = Convert.ToString(dgvFunc[linha, 2]);
                var data_entrega = Convert.ToDateTime(dgvFunc[linha, 3]);
                var data_vencimento = Convert.ToDateTime(dgvFunc[linha, 4]);
                controller.Inserir(matricula, nome, epi, data_entrega, data_vencimento);
            }
            MessageBox.Show("Entrega efetuada com Sucesso!!", "Entrega EPI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnEditarItem.Enabled = false;
            btnExcluirItem.Enabled = false;
            btnFinalizarEntrega.Enabled = false;
            btnNovoItem.Enabled = false;
            FrmPrincipal principal = new FrmPrincipal();
            principal.Show();
            Environment.Exit(0);
        }

        private void dgvFunc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvFunc.Rows[e.RowIndex];
                this.dgvFunc.Rows[e.RowIndex].Selected = true;
                
                txtNome.Text = row.Cells[0].Value.ToString();
                txtEpi.Text = row.Cells[1].Value.ToString();
                

            }
            btnEditarItem.Enabled = true;
            btnExcluirItem.Enabled = true;
        }
    }
}
