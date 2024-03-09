using AppDesktopEpi.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDesktopEpi.Controller
{
    internal class FunController
    {
        MySqlConnection conn = new MySqlConnection("server=sql.freedb.tech;port=3306;database=freedb_pietro_tds10;user id=freedb_pips96;password=Pv7NP7Y&qB%n!R3;charset=utf8");

        public List<Funcionario> listaFuncionario()
        {
            List<Funcionario> li = new List<Funcionario>();
            string sql = "SELECT * FROM funcionario";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Funcionario func = new Funcionario();
                func.id = (int)dr["id"];
                func.nome = dr["nome"].ToString();
                func.matricula = dr["matricula"].ToString();
                func.epi = dr["epi"].ToString();
                func.data_entrega = (DateTime)dr["data_entrega"];
                func.data_vencimento = (DateTime)dr["data_vencimento"];
                li.Add(func);
            }
            dr.Close();
            conn.Close();
            return li;
        }
        public List<Funcionario> listaEpi()
        {
            List<Funcionario> li = new List<Funcionario>();
            string sql = "SELECT epi,data_entrega,data_vencimento FROM funcionario";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Funcionario func = new Funcionario();
                func.epi = dr["epi"].ToString();
                func.data_entrega = (DateTime)dr["data_entrega"];
                func.data_vencimento = (DateTime)dr["data_vencimento"];
                li.Add(func);
            }
            dr.Close();
            conn.Close();
            return li;
        }

        public void Inserir(string nome, string matricula, string epi, DateTime data_entrega, DateTime data_vencimento, double dias)
        {
            data_entrega = DateTime.Today;
            data_vencimento = DateTime.Today.AddDays(dias);
            string sql = "INSERT INTO funcionario(nome,matricula,epi,data_entrega,data_vencimento) VALUES('" + nome + "','" + matricula + "','"+epi+"','"+data_entrega+"','"+data_vencimento+"')";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Atualizar(Funcionario func, double dias)
        {
            func.data_vencimento = DateTime.Today.AddDays(dias);
            string sql = "UPDATE funcionario SET nome='" + func.nome + "',matricula='" + func.matricula + "',epi='" + func.epi + "',data_entrega='"+func.data_entrega+"',data_vencimento='"+func.data_vencimento+"' WHERE id='" + func.id + "'";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void Excluir(int id)
        {
            string sql = "DELETE FROM funcionario WHERE id='" + id + "'";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void Localizar(int id)
        {
            Funcionario funcionario = new Funcionario();
            string sql = "SELECT * FROM funcionario WHERE id='" + id + "'";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                funcionario.nome = dr["nome"].ToString();
                funcionario.matricula = dr["matricula"].ToString();
                funcionario.epi = dr["epi"].ToString();
                funcionario.data_entrega = (DateTime)dr["data_entrega"];
                funcionario.data_vencimento = (DateTime)dr["data_vencimento"];
            }
            dr.Close();
            conn.Close();

        }

        public bool RegistroRepetido(string nome, string matricula, string epi)
        {
            string sql = "SELECT * FROM funcionario WHERE nome='" + nome + "' AND matricula='" + matricula + "' AND epi='" + epi + "'";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return (int)result > 0;
            }
            conn.Close();
            return false;
        }
    }
}

