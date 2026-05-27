// ============================
// MainForm.cs
// ============================

using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Certifique-se de que essa linha está no topo do arquivo

namespace Sistema_de_Supermercado
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void AbrirFormulario(Form formulario)
        {
            panelConteudo.Controls.Clear();

            formulario.TopLevel = false;

            formulario.Dock =
                DockStyle.Fill;

            panelConteudo.Controls.Add(formulario);

            formulario.Show();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormProdutos());
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormCompras());
        }

        private void btnNota_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormNota(0));
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            AbrirFormulario(new FormProdutos());
        }

    }
}