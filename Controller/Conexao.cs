using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration;

namespace Trabalho_final.Controller
{
    public class Conexao
    {
        
        public string equacao_history { get; set; }

        //Dados SQL Server do Azure
        string Server = "inf-0999-server.database.windows.net";
        string Port = "1433";
        string Username = "Administrador";
        string Password = "inF0999un1c@mp";
        string Database = "PROJ-FINAL-CALC-INF-0999";
        string Timeout = "30";
        
        public void getDBConnection(String query, String comando)
        {
            try
            {
                // String de Conexao
                string connectionString =
                    "Data Source=" + Server + 
                    ", " + Port + 
                    ";Initial Catalog=" + Database + 
                    ";User ID =" + Username + 
                    ";Password=" + Password + 
                    ";Connect Timeout=" + Timeout;

                SqlConnection conn = new SqlConnection(connectionString);

                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                
                if (comando == "inserir")
                {
                    cmd.ExecuteNonQuery();
                }

                if (comando == "selecionar")
                {
                    SqlDataReader cmd_select = cmd.ExecuteReader();
                    while (cmd_select.Read())
                    {
                        //MessageBox.Show((string)cmd_select["equacao"]);
                        equacao_history = equacao_history + "\n" + (string)cmd_select["data_atu"] + " - " + (string)cmd_select["equacao"] + " " + cmd_select["resultado"];
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
                throw;
            }

        }
    }
}