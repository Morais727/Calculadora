using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration;
using Npgsql;

namespace Trabalho_final.Controller
{
    public class Conexao
    {
        
        public string equacao_history { get; set; }

        //Dados SQL Server do Azure
        string Server = "inf-0999-server.postgres.database.azure.com";
        string Port = "5432";
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
                    "Server=" + Server + 
                    ";Username=" + Username + 
                    ";Database=" + Database + 
                    ";Port=" + Port + 
                    ";Password=" + Password + 
                    ";SSLMode=Prefer";

                NpgsqlConnection conn = new NpgsqlConnection(connectionString);

                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                
                if (comando == "inserir")
                {
                    cmd.ExecuteNonQuery();
                }

                if (comando == "selecionar")
                {
                    NpgsqlDataReader cmd_select = cmd.ExecuteReader();
                    while (cmd_select.Read())
                    {
                        equacao_history = equacao_history + "\n" + cmd_select["data_atu"].ToString() + " | " + (string)cmd_select["equacao"] + " " + cmd_select["resultado"];
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