using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace cadastrodeatividades
{
    public partial class frmCadastrodeAtividades : Form
    {
        MySqlConnection conexao;
        string data_source = "datasource=localhost; username=root; password=; database=sistemasdetarefas";


        public frmCadastrodeAtividades()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            try

            {
                if (string.IsNullOrEmpty(comboBoxFuncionaria.Text.Trim()) ||
                    string.IsNullOrEmpty(comboBoxAno.Text.Trim()) ||
                    string.IsNullOrEmpty(comboBoxMes.Text.Trim()) ||
                    string.IsNullOrEmpty(comboBoxSemana.Text.Trim()) ||
                    string.IsNullOrEmpty(comboBoxAtividade.Text.Trim()))
                {
                    MessageBox.Show("Todos os campos devem ser preenchidos.",
                                    "Validação",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                // cria a conexão com o banco de dados
                conexao = new MySqlConnection(data_source);
                conexao.Open();

                

                // Comando SQL para inserir um novo cliente no banco de dados
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conexao
                };
                cmd.Prepare();

                cmd.CommandText = "INSERT INTO tarefa (ano, atividade, semana, mes, nomefuncionaria) " +
                    "VALUES (@ano, @atividade, @semana, @mes, @nomefuncionaria)";
                cmd.Parameters.AddWithValue("@ano", comboBoxAno.Text.Trim());
                cmd.Parameters.AddWithValue("@atividade", comboBoxAtividade.Text.Trim());
                cmd.Parameters.AddWithValue("@semana", comboBoxSemana.Text.Trim());
                cmd.Parameters.AddWithValue("@mes", comboBoxMes.Text.Trim());
                cmd.Parameters.AddWithValue("@nomefuncionaria", comboBoxFuncionaria.Text.Trim());

                // Executar o comando de inserção no banco

                cmd.ExecuteNonQuery();

                // Menssagem de sucesso
                MessageBox.Show("Cadastro realizado com sucesso: ",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            catch (MySqlException ex)
            {
                // Trata erros relacionados ao MySQL

                MessageBox.Show("Erro. " + ex.Number + "Ocorreu: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);


            }

            catch (Exception ex)
            {
                //Trata outros tipos de erro

                MessageBox.Show("Ocorreu: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            finally
            {
                // Garante que a conexão com o banco de dados será fechada, mesmo se ocorrer erro

                if (conexao != null && conexao.State == ConnectionState.Open)
                {
                    conexao.Close();

                   
                }
            }


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try

            {

                if ((comboBoxFuncionaria.Text != null) ||
                   (comboBoxAno.Text != null) ||
                   (comboBoxMes.Text != null) ||
                   (comboBoxSemana.Text != null) ||
                   (comboBoxAtividade.Text != null))
                {
                    comboBoxFuncionaria.Text = null;
                    comboBoxAno.Text = null;
                    comboBoxMes.Text = null;
                    comboBoxSemana.Text = null;
                    comboBoxAtividade.Text = null;
                }

                MessageBox.Show("Cancelado com sucesso ",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //Trata outros tipos de erro

                MessageBox.Show("Ocorreu: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

