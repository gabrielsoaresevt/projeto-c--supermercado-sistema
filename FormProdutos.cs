using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Supermercado
{
    public partial class FormProdutos : Form
    {
        public FormProdutos()
        {
            InitializeComponent();
            CarregarCategoriasComboBox();
            ListarProdutos();
        }

        private void FormProdutos_Load(object sender, EventArgs e)
        {
            ArredondarBotao(btnCadastrar, 18);
            ArredondarBotao(btnAtualizar, 18);
            ArredondarBotao(btnExcluir, 18);

            ArredondarCampo(txtCodigo, 15);
            ArredondarCampo(txtNome, 15);
            ArredondarCampo(cbCategoria, 15);
            ArredondarCampo(txtQuantidade, 15);
            ArredondarCampo(txtPreco, 15);
        }

        private void CarregarCategoriasComboBox()
        {
            string query = "SELECT id_categoria, nome_categoria FROM categorias ORDER BY nome_categoria ASC";

            using (MySqlConnection conexao = ConexaoBD.ObterConexao())
            {
                if (conexao == null) return;

                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    try
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                        DataTable tabelaCategorias = new DataTable();
                        adapter.Fill(tabelaCategorias);

                        // --- AQUI ESTÁ O TRUQUE ---
                        // Criamos uma linha nova manualmente na tabela de dados temporária
                        DataRow linhaPadrao = tabelaCategorias.NewRow();
                        linhaPadrao["id_categoria"] = DBNull.Value; // Sem ID específico no banco
                        linhaPadrao["nome_categoria"] = "Geral";     // Texto que o usuário vai ver

                        // Inserimos essa linha na posição 0 (no topo da lista do ComboBox)
                        tabelaCategorias.Rows.InsertAt(linhaPadrao, 0);
                        // --------------------------

                        cbCategoria.DataSource = tabelaCategorias;
                        cbCategoria.DisplayMember = "nome_categoria";
                        cbCategoria.ValueMember = "id_categoria";

                        // Define que o "Geral" (posição 0) já vem selecionado por padrão
                        cbCategoria.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar categorias: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ArredondarBotao(Button botao, int raio)
        {
            GraphicsPath pasta = new GraphicsPath();

            pasta.AddArc(0, 0, raio, raio, 180, 90);
            pasta.AddArc(botao.Width - raio, 0, raio, raio, 270, 90);
            pasta.AddArc(botao.Width - raio, botao.Height - raio, raio, raio, 0, 90);
            pasta.AddArc(0, botao.Height - raio, raio, raio, 90, 90);

            pasta.CloseAllFigures();

            botao.Region =
                new System.Drawing.Region(pasta);
        }

        private void ArredondarCampo(Control campo, int raio)
        {
            GraphicsPath pasta = new GraphicsPath();

            pasta.AddArc(0, 0, raio, raio, 180, 90);
            pasta.AddArc(campo.Width - raio, 0, raio, raio, 270, 90);
            pasta.AddArc(campo.Width - raio, campo.Height - raio, raio, raio, 0, 90);
            pasta.AddArc(0, campo.Height - raio, raio, raio, 90, 90);

            pasta.CloseAllFigures();

            campo.Region =
                new System.Drawing.Region(pasta);
        }

        private void SalvarProduto()
        {
            // 1. Validações básicas de preenchimento
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios (Código, Nome e Preço)!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Coleta dos dados da tela
            string codigo = txtCodigo.Text;
            string nome = txtNome.Text;

            // TRATAMENTO DO GERAL: 
            // Se o SelectedValue for nulo (porque clicou em Geral), definimos o ID padrão como 1.
            int idCategoria = 1;
            if (cbCategoria.SelectedValue != null && cbCategoria.SelectedValue != DBNull.Value)
            {
                idCategoria = Convert.ToInt32(cbCategoria.SelectedValue);
            }

            int quantidade = 0;
            if (!int.TryParse(txtQuantidade.Text, out quantidade))
            {
                MessageBox.Show("Por favor, insira uma quantidade válida (número inteiro)!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal preco = 0;
            if (!decimal.TryParse(txtPreco.Text, out preco))
            {
                MessageBox.Show("Por favor, insira um preço válido (ex: 5,50)!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. String SQL CORRIGIDA (Usando id_categoria em vez de categoria)
            string query = "INSERT INTO produtos (codigo, nome, id_categoria, quantidade, preco) VALUES (@codigo, @nome, @idCategoria, @quantidade, @preco)";

            using (MySqlConnection conexao = ConexaoBD.ObterConexao())
            {
                if (conexao == null) return;

                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    // Vinculando os parâmetros corretamente com a nova estrutura
                    comando.Parameters.AddWithValue("@codigo", codigo);
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@idCategoria", idCategoria); // Enviando o ID numérico
                    comando.Parameters.AddWithValue("@quantidade", quantidade);
                    comando.Parameters.AddWithValue("@preco", preco);

                    try
                    {
                        int linhasAfetadas = comando.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimparCampos();
                            ListarProdutos();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao salvar produto no banco: " + ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }

        }

        // Método auxiliar para limpar a tela após o cadastro
        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtNome.Clear();
            txtQuantidade.Clear();
            txtPreco.Clear();
            if (cbCategoria.Items.Count > 0) cbCategoria.SelectedIndex = 0;
            txtCodigo.Focus(); // Coloca o cursor de volta no primeiro campo
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            SalvarProduto();
        }

        private void ListarProdutos()
        {
            // Query que junta a tabela produtos com a tabela categorias para trazer o nome legível
            string query = @"SELECT 
                        p.id_produto AS 'ID',
                        p.codigo AS 'Código',
                        p.nome AS 'Nome',
                        c.nome_categoria AS 'Categoria',
                        p.quantidade AS 'Qtd Estoque',
                        p.preco AS 'Preço (R$)',
                        p.data_cadastro AS 'Data Cadastro'
                     FROM produtos p
                     INNER JOIN categorias c ON p.id_categoria = c.id_categoria
                     ORDER BY p.id_produto DESC"; // Mostra os mais recentes primeiro

            using (MySqlConnection conexao = ConexaoBD.ObterConexao())
            {
                if (conexao == null) return;

                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    try
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                        DataTable tabelaProdutos = new DataTable();
                        adapter.Fill(tabelaProdutos);

                        // Vincula o DataGridView aos dados retornados do banco
                        dgvProdutos.DataSource = tabelaProdutos;

                        // Melhora a usabilidade: ajusta a largura das colunas automaticamente
                        dgvProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao listar produtos: " + ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            SalvarProduto();
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o usuário clicou em uma linha válida (e não no cabeçalho das colunas)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linha = dgvProdutos.Rows[e.RowIndex];

                // Copia os valores da linha da tabela de volta para os seus TextBox
                txtCodigo.Text = linha.Cells["Código"].Value.ToString();
                txtNome.Text = linha.Cells["Nome"].Value.ToString();
                txtQuantidade.Text = linha.Cells["Qtd Estoque"].Value.ToString();
                txtPreco.Text = linha.Cells["Preço (R$)"].Value.ToString();

                // Ajusta o ComboBox para mostrar a categoria correta do produto selecionado
                string nomeCategoria = linha.Cells["Categoria"].Value.ToString();
                cbCategoria.Text = nomeCategoria;
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizarProduto();

        }

        private void AtualizarProduto()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                MessageBox.Show("Selecione um produto na tabela e preencha os campos para atualizar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string codigo = txtCodigo.Text;
            string nome = txtNome.Text;
            int idCategoria = cbCategoria.SelectedValue != null && cbCategoria.SelectedValue != DBNull.Value ? Convert.ToInt32(cbCategoria.SelectedValue) : 1;

            int quantidade;
            int.TryParse(txtQuantidade.Text, out quantidade);

            decimal preco;
            if (!decimal.TryParse(txtPreco.Text, out preco))
            {
                MessageBox.Show("Insira um preço válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Query SQL de UPDATE filtrando pelo Código do produto
            string query = "UPDATE produtos SET nome = @nome, id_categoria = @idCategoria, quantidade = @quantidade, preco = @preco WHERE codigo = @codigo";

            using (MySqlConnection conexao = ConexaoBD.ObterConexao())
            {
                if (conexao == null) return;

                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@codigo", codigo);
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@idCategoria", idCategoria);
                    comando.Parameters.AddWithValue("@quantidade", quantidade);
                    comando.Parameters.AddWithValue("@preco", preco);

                    try
                    {
                        int linhasAfetadas = comando.ExecuteNonQuery();
                        if (linhasAfetadas > 0)
                        {
                            MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimparCampos();
                            ListarProdutos(); // Atualiza a tabela na tela
                        }
                        else
                        {
                            MessageBox.Show("Nenhum produto foi encontrado com esse código para atualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao atualizar produto: " + ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            ExcluirProduto();
        }

        private void ExcluirProduto()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Selecione um produto na tabela ou digite o código para excluir!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Caixa de diálogo perguntando se o usuário tem certeza (Melhoria de Usabilidade)
            DialogResult confirmacao = MessageBox.Show("Tem certeza que deseja excluir este produto de forma permanente?", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                string query = "DELETE FROM produtos WHERE codigo = @codigo";

                using (MySqlConnection conexao = ConexaoBD.ObterConexao())
                {
                    if (conexao == null) return;

                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@codigo", txtCodigo.Text);

                        try
                        {
                            int linhasAfetadas = comando.ExecuteNonQuery();
                            if (linhasAfetadas > 0)
                            {
                                MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimparCampos();
                                ListarProdutos(); // Atualiza a tabela na tela
                            }
                            else
                            {
                                MessageBox.Show("Produto não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao excluir produto. Verifique se ele não está travado em alguma venda ativa: " + ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
