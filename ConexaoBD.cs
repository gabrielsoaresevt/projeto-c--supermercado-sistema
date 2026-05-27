using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Sistema_de_Supermercado
{
    public class ConexaoBD
    {
        // Linha que define o servidor, banco de dados, usuário e senha.
        private static string strConexao = "Server=localhost;Database=supermercado_db;Uid=admin_supermercado;Pwd=123456;";
        public static MySqlConnection ObterConexao()
        {
            MySqlConnection conexao = new MySqlConnection(strConexao);
            try
            {
                conexao.Open();
                return conexao; // Retorna a conexão aberta pronta para uso
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com o Banco de Dados: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}