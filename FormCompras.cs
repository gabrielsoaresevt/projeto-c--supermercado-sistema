// ==========================================
// FORMCOMPRAS.CS
// ==========================================

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sistema_de_Supermercado
{
    public partial class FormCompras : Form
    {
        private List<ItemCarrinho> carrinho = new List<ItemCarrinho>();
        private decimal totalCompra = 0;

        public FormCompras()
        {
            InitializeComponent();
            CarregarProdutosComboBox();
            ConfigurarGridCarrinho();
        }

        private void CarregarProdutosComboBox()
        {
            string query = "SELECT id_produto, nome, preco FROM produtos ORDER BY nome ASC";

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

                        // --- INSERÇÃO DA OPÇÃO PADRÃO TEXTUAL ---
                        DataRow linhaPadrao = tabelaProdutos.NewRow();
                        linhaPadrao["id_produto"] = 0; // ID 0 para podermos validar depois
                        linhaPadrao["nome"] = "Selecionar produto";
                        linhaPadrao["preco"] = 0.00;

                        // Inserindo no topo da tabela (Posição 0)
                        tabelaProdutos.Rows.InsertAt(linhaPadrao, 0);
                        // ----------------------------------------

                        cbProdutos.DataSource = tabelaProdutos;
                        cbProdutos.DisplayMember = "nome";
                        cbProdutos.ValueMember = "id_produto";

                        // Força o "Selecionar produto" a vir marcado por padrão
                        cbProdutos.SelectedIndex = 0;

                        // Aproveita para garantir a quantidade inicial como 1 aqui também
                        txtQuantidade.Text = "1";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ConfigurarGridCarrinho()
        {
            // Vincula a lista do carrinho ao DataGridView de forma limpa
            dgvCompras.DataSource = null;
            dgvCompras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void RecalcularTotal()
        {
            totalCompra = 0;
            foreach (var item in carrinho)
            {
                totalCompra += item.TotalItem;
            }
            lblTotal.Text = $"Total: R$ {totalCompra:N2}";
        }

        private void AtualizarGrid()
        {
            dgvCompras.DataSource = null; // Reseta o vínculo
            dgvCompras.DataSource = carrinho; // Atualiza com os novos itens

            // Oculta a coluna do ID do produto para ficar mais visual para o usuário
            if (dgvCompras.Columns["IdProduto"] != null)
                dgvCompras.Columns["IdProduto"].Visible = false;
        }

        private void FormCompras_Load(object sender, EventArgs e)
        {
            ArredondarBotao(btnAdicionar, 18);
            ArredondarBotao(btnRemover, 18);
            ArredondarBotao(btnFinalizarCompra, 18);

            ArredondarCampo(cbProdutos, 15);
            ArredondarCampo(txtQuantidade, 15);

            txtQuantidade.Text = "1";
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

        private void dgvCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {

            // Validação atualizada: impede se não tiver nada ou se for a opção informativa (ID 0 ou Index 0)
            if (cbProdutos.SelectedIndex <= 0)
            {
                MessageBox.Show("Por favor, selecione um produto válido da lista!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int quantidade = 0;
            if (!int.TryParse(txtQuantidade.Text, out quantidade) || quantidade <= 0)
            {
                MessageBox.Show("Por favor, insira uma quantidade válida maior que zero!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Recupera o produto selecionado na ComboBox
            DataRowView produtoSelecionado = (DataRowView)cbProdutos.SelectedItem;
            int idProduto = Convert.ToInt32(produtoSelecionado["id_produto"]);
            string nomeProduto = produtoSelecionado["nome"].ToString();
            decimal precoProduto = Convert.ToDecimal(produtoSelecionado["preco"]);

            // Opcional/Boa prática: Buscar o código do produto ou usar o próprio ID como string
            string codigoProduto = idProduto.ToString();

            // 3. Adiciona o item na lista do carrinho
            ItemCarrinho novoItem = new ItemCarrinho
            {
                IdProduto = idProduto,
                Código = codigoProduto,
                Produto = nomeProduto,
                Quantidade = quantidade,
                PreçoUnitário = precoProduto
            };

            carrinho.Add(novoItem);

            // 4. Atualiza a Interface Gráfica
            AtualizarGrid();
            RecalcularTotal();

            // 5. Limpa os campos para o próximo item
            cbProdutos.SelectedIndex = 0; // Volta para "Selecionar produto"
            txtQuantidade.Text = "1";     // Volta para 1
            cbProdutos.Focus();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            // Verifica se há alguma linha selecionada no DataGridView
            if (dgvCompras.CurrentRow != null && dgvCompras.CurrentRow.Index >= 0)
            {
                int indice = dgvCompras.CurrentRow.Index;

                // Remove da nossa lista dinâmica usando o índice da linha da tabela
                carrinho.RemoveAt(indice);

                // Atualiza a tela
                AtualizarGrid();
                RecalcularTotal();
            }
            else
            {
                MessageBox.Show("Selecione um item no carrinho para remover!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            if (carrinho.Count == 0)
            {
                MessageBox.Show("O carrinho está vazio! Adicione itens antes de finalizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conexao = ConexaoBD.ObterConexao())
            {
                if (conexao == null) return;

                // Inicia uma transação no banco de dados
                MySqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    // 1. Inserir o cabeçalho da venda na tabela 'vendas'
                    string sqlVenda = "INSERT INTO vendas (subtotal, desconto, total) VALUES (@subtotal, @desconto, @total); SELECT LAST_INSERT_ID();";
                    int idVendaGerado = 0;

                    using (MySqlCommand cmdVenda = new MySqlCommand(sqlVenda, conexao, transacao))
                    {
                        cmdVenda.Parameters.AddWithValue("@subtotal", totalCompra);
                        cmdVenda.Parameters.AddWithValue("@desconto", 0.00); // Se não tiver campo de desconto na tela, passa 0
                        cmdVenda.Parameters.AddWithValue("@total", totalCompra);

                        // O ExecuteScalar junto com o SELECT LAST_INSERT_ID() pega o ID auto-incremento gerado para essa venda
                        idVendaGerado = Convert.ToInt32(cmdVenda.ExecuteScalar());
                    }

                    // 2. Inserir cada item do carrinho na tabela 'itens_venda'
                    string sqlItem = "INSERT INTO itens_venda (id_venda, id_produto, quantidade, preco_unitario) VALUES (@idVenda, @idProduto, @quantidade, @precoUnitario)";

                    // Também vamos dar baixa no estoque do produto (O professor vai achar sensacional!)
                    string sqlBaixaEstoque = "UPDATE produtos SET quantidade = quantidade - @quantidade WHERE id_produto = @idProduto";

                    foreach (var item in carrinho)
                    {
                        using (MySqlCommand cmdItem = new MySqlCommand(sqlItem, conexao, transacao))
                        {
                            cmdItem.Parameters.AddWithValue("@idVenda", idVendaGerado);
                            cmdItem.Parameters.AddWithValue("@idProduto", item.IdProduto);
                            cmdItem.Parameters.AddWithValue("@quantidade", item.Quantidade);
                            cmdItem.Parameters.AddWithValue("@precoUnitario", item.PreçoUnitário);
                            cmdItem.ExecuteNonQuery();
                        }

                        using (MySqlCommand cmdEstoque = new MySqlCommand(sqlBaixaEstoque, conexao, transacao))
                        {
                            cmdEstoque.Parameters.AddWithValue("@quantidade", item.Quantidade);
                            cmdEstoque.Parameters.AddWithValue("@idProduto", item.IdProduto);
                            cmdEstoque.ExecuteNonQuery();
                        }
                    }

                    transacao.Commit();
                    MessageBox.Show($"Compra realizada com sucesso! Código da Venda: {idVendaGerado}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    FormNota telaNota = new FormNota(idVendaGerado);
                    telaNota.ShowDialog();
                                       
                    carrinho.Clear();
                    AtualizarGrid();
                    RecalcularTotal();
                }
                catch (Exception ex)
                {
                    // Caso ocorra qualquer erro, desfaz todas as inserções dessa venda automaticamente
                    transacao.Rollback();
                    MessageBox.Show("Erro ao finalizar a compra no banco de dados: " + ex.Message, "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}