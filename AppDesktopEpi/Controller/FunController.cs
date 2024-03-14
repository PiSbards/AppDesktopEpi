using AppDesktopEpi.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace AppDesktopEpi.Controller
{
    public class FunController
    {
        MySqlConnection conn = new MySqlConnection("server=sql.freedb.tech;port=3306;database=freedb_ControleEPI;user id=freedb_MuriloPietro;password=8*FcGMa*bxUb2Nj;charset=utf8");

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
                func.matricula = (int)dr["matricula"];
                func.nome = dr["nome"].ToString();                
                func.epi = dr["epi"].ToString();
                func.data_entrega = (DateTime)dr["data_entrega"];
                func.data_vencimento = (DateTime)dr["data_vencimento"];
                li.Add(func);
            }
            dr.Close();
            conn.Close();
            return li;
        }
        public List<Funcionario> listaEpiVencida()
        {
            List<Funcionario> li = new List<Funcionario>();
            string sql = "SELECT * FROM funcionario WHERE data_vencimento < CURDATE()";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Funcionario func = new Funcionario();
                func.matricula = (int)dr["matricula"];
                func.nome = dr["nome"].ToString();
                func.epi = dr["epi"].ToString();
                func.data_entrega = (DateTime)dr["data_entrega"];
                func.data_vencimento = (DateTime)dr["data_vencimento"];
                li.Add(func);
            }
            dr.Close();
            conn.Close();
            return li;
        }
        public List<Funcionario> listaEpiAvencer()
        {
            List<Funcionario> li = new List<Funcionario>();
            string sql = "SELECT * FROM funcionario WHERE data_vencimento BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 3 DAY)";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Funcionario func = new Funcionario();
                func.matricula = (int)dr["matricula"];
                func.nome = dr["nome"].ToString();
                func.epi = dr["epi"].ToString();
                func.data_entrega = (DateTime)dr["data_entrega"];
                func.data_vencimento = (DateTime)dr["data_vencimento"];
                li.Add(func);
            }
            dr.Close();
            conn.Close();
            return li;
        }

        public void Inserir(string nome, string epi, DateTime data_entrega, DateTime data_vencimento)
        {            
            string sql = "INSERT INTO funcionario(nome,epi,data_entrega,data_vencimento) VALUES(@nome,@epi,@data_entrega,@data_vencimento)";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;
                cmd.Parameters.Add("@epi", MySqlDbType.VarChar).Value = epi;
                cmd.Parameters.Add("@data_entrega", MySqlDbType.DateTime).Value = data_entrega;
                cmd.Parameters.Add("@data_vencimento",MySqlDbType.DateTime).Value = data_vencimento;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        public void Atualizar(int matricula, string nome,string epi, DateTime data_entrega,DateTime data_vencimento)
        {            
            string sql = "UPDATE funcionario SET nome=@nome,epi=@epi,data_entrega=@data_entrega,data_vencimento=@data_vencimento WHERE matricula=@matricula";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@matricula", MySqlDbType.Int32).Value = matricula;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;
                cmd.Parameters.Add("@epi", MySqlDbType.VarChar).Value = epi;
                cmd.Parameters.Add("@data_entrega", MySqlDbType.DateTime).Value = data_entrega;
                cmd.Parameters.Add("@data_vencimento", MySqlDbType.DateTime).Value = data_vencimento;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        public void Excluir(int matricula)
        {
            string sql = "DELETE FROM funcionario WHERE matricula='" + matricula + "'";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public Funcionario Localizar(int matricula)
        {
            Funcionario funcionario = new Funcionario();
            string sql = "SELECT * FROM funcionario WHERE matricula='"+matricula+"'";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                funcionario.matricula = (int)dr["matricula"];
                funcionario.nome = dr["nome"].ToString();                
                funcionario.epi = dr["epi"].ToString();
                funcionario.data_entrega = (DateTime)dr["data_entrega"];
                funcionario.data_vencimento = (DateTime)dr["data_vencimento"];
            }            
            dr.Close();
            conn.Close();
            return funcionario;
        }

        public bool RegistroRepetido(string nome, int matricula, string epi)
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
