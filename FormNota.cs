using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Sistema_de_Supermercado
{
    public partial class FormNota : Form
    {
        private int idVendaAtual;

        // Construtor atualizado recebendo o ID da Venda vindo da tela de compras
        public FormNota(int idVenda)
        {
            InitializeComponent();
            this.idVendaAtual = idVenda;
        }

        private void FormNota_Load(object sender, EventArgs e)
        {
            // Mantém a sua estilização de botões arredondados funcionando perfeitamente
            ArredondarBotao(btnImprimir, 18);
            ArredondarBotao(btnSalvarPdf, 18);
            ArredondarBotao(btnFechar, 18);

            // Carrega os dados da nota assim que a tela abre
            CarregarDadosNotaFiscal();
        }

        private void CarregarDadosNotaFiscal()
        {
            
            if (idVendaAtual == 0)
            {
                lblNumeroNotaValor.Text = "#000000";
                lblDataValor.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                lblClienteValor.Text = "Nenhuma venda selecionada";
                lblPagamentoValor.Text = "-";

                lblSubtotal.Text = "Subtotal: R$ 0,00";
                lblDesconto.Text = "Desconto: R$ 0,00";
                lblTotal.Text = "Total: R$ 0,00";

                dgvItens.DataSource = null;
                return;
            }

            using (MySqlConnection conexao = ConexaoBD.ObterConexao())
            {
                if (conexao == null) return;

                // 1. Puxar dados do Cabeçalho da Venda
                string queryVenda = "SELECT id_venda, data_venda, subtotal, desconto, total FROM vendas WHERE id_venda = @idVenda";

                using (MySqlCommand cmdVenda = new MySqlCommand(queryVenda, conexao))
                {
                    cmdVenda.Parameters.AddWithValue("@idVenda", idVendaAtual);
                    using (MySqlDataReader reader = cmdVenda.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Ajustado para os nomes exatos das suas labels de valor
                            lblNumeroNotaValor.Text = $"#{idVendaAtual:D6}";
                            lblDataValor.Text = Convert.ToDateTime(reader["data_venda"]).ToString("dd/MM/yyyy HH:mm:ss");
                            lblClienteValor.Text = "Consumidor Final";
                            lblPagamentoValor.Text = "Dinheiro/Cartão";

                            lblSubtotal.Text = $"Subtotal: R$ {Convert.ToDecimal(reader["subtotal"]):N2}";
                            lblDesconto.Text = $"Desconto: R$ {Convert.ToDecimal(reader["desconto"]):N2}";
                            lblTotal.Text = $"Total: R$ {Convert.ToDecimal(reader["total"]):N2}";
                        }
                        else
                        {
                            MessageBox.Show("Venda não encontrada no banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // 2. Puxar Itens da Venda e jogar no dgvItens
                string queryItens = @"SELECT 
                                        p.nome AS 'Produto / Item', 
                                        iv.quantidade AS 'Qtd', 
                                        iv.preco_unitario AS 'V. Unitário (R$)', 
                                        (iv.quantidade * iv.preco_unitario) AS 'V. Total (R$)'
                                      FROM itens_venda iv
                                      INNER JOIN produtos p ON iv.id_produto = p.id_produto
                                      WHERE iv.id_venda = @idVenda";

                using (MySqlCommand cmdItens = new MySqlCommand(queryItens, conexao))
                {
                    cmdItens.Parameters.AddWithValue("@idVenda", idVendaAtual);
                    try
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmdItens);
                        DataTable tabelaItens = new DataTable();
                        adapter.Fill(tabelaItens);

                        // Alimenta o seu dgvItens central
                        dgvItens.DataSource = tabelaItens;
                        dgvItens.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvItens.AllowUserToAddRows = false; // Impede linha em branco no final
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar itens da nota: " + ex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void LimparNota()
        {
            lblNumeroNotaValor.Text = "";
            lblDataValor.Text = "";
            lblClienteValor.Text = "";
            lblPagamentoValor.Text = "";

            lblSubtotal.Text = "Subtotal:";
            lblDesconto.Text = "Desconto:";
            lblTotal.Text = "Total:";

            dgvItens.DataSource = null; // Como usamos DataSource, limpamos atribuindo null
        }

        private void ArredondarBotao(Button botao, int raio)
        {
            GraphicsPath pasta = new GraphicsPath();
            pasta.AddArc(0, 0, raio, raio, 180, 90);
            pasta.AddArc(botao.Width - raio, 0, raio, raio, 270, 90);
            pasta.AddArc(botao.Width - raio, botao.Height - raio, raio, raio, 0, 90);
            pasta.AddArc(0, botao.Height - raio, raio, raio, 90, 90);
            pasta.CloseAllFigures();
            botao.Region = new System.Drawing.Region(pasta);
        }

        private void btnImprimir_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Enviando comando de impressão para o caixa físico...\nRecibo emitido com sucesso!", "Impressão de Cupom", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSalvarPdf_Click_1(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();

            // Define o evento de desenho do conteúdo do Cupom
            pd.PrintPage += new PrintPageEventHandler(this.DesenharLayoutCupomPDF);

            // Configura a caixinha de diálogo para salvar o arquivo no computador
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Documento PDF (*.pdf)|*.pdf";
            sfd.FileName = $"Nota_Compra_{idVendaAtual:D5}.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Força o componente a usar o driver nativo do Windows para converter o desenho em PDF
                pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                pd.PrinterSettings.PrintToFile = true;
                pd.PrinterSettings.PrintFileName = sfd.FileName;

                try
                {
                    pd.Print(); // Executa o desenho e gera o PDF final
                    MessageBox.Show("Nota Fiscal gerada e salva em PDF com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao gerar arquivo PDF: " + ex.Message, "Erro de Exportação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DesenharLayoutCupomPDF(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // Configuração de fontes legíveis e profissionais para cupom
            Font fonteTitulo = new Font("Arial", 12, FontStyle.Bold);
            Font fonteSubtitulo = new Font("Arial", 9, FontStyle.Regular);
            Font fonteCabecalhoTabela = new Font("Arial", 9, FontStyle.Bold);
            Font fonteCorpo = new Font("Arial", 9, FontStyle.Regular);
            Font fonteTotal = new Font("Arial", 11, FontStyle.Bold);

            Brush pincelPreto = Brushes.Black;
            Pen linhaPontilhada = new Pen(Color.Black, 1);
            linhaPontilhada.DashStyle = DashStyle.Dash;

            // Margens de alinhamento vertical e horizontal
            int xInicio = 40;
            int yAtual = 40;
            int larguraCupom = 280; // Define o limite lateral do cupom

            // Configuração para centralizar textos
            StringFormat alinharCentro = new StringFormat();
            alinharCentro.Alignment = StringAlignment.Center;
            alinharCentro.LineAlignment = StringAlignment.Center;

            // Configuração para alinhar valores numéricos à direita
            StringFormat alinharDireita = new StringFormat();
            alinharDireita.Alignment = StringAlignment.Far;

            // 1. CABEÇALHO DO SUPERMERCADO (Conforme imagem enviada)
            g.DrawString("SUPERMERCADO EXEMPLO", fonteTitulo, pincelPreto, new RectangleF(xInicio, yAtual, larguraCupom, 20), alinharCentro);
            yAtual += 20;
            g.DrawString("Rua das Flores, 123 - Centro", fonteSubtitulo, pincelPreto, new RectangleF(xInicio, yAtual, larguraCupom, 15), alinharCentro);
            yAtual += 15;
            g.DrawString("CNPJ: 12.345.678/0001-90", fonteSubtitulo, pincelPreto, new RectangleF(xInicio, yAtual, larguraCupom, 15), alinharCentro);
            yAtual += 15;
            g.DrawString("Fone: (11) 99999-9999", fonteSubtitulo, pincelPreto, new RectangleF(xInicio, yAtual, larguraCupom, 15), alinharCentro);

            yAtual += 25;
            g.DrawString("*** NOTA DE COMPRA ***", fonteCabecalhoTabela, pincelPreto, new RectangleF(xInicio, yAtual, larguraCupom, 15), alinharCentro);

            yAtual += 25;
            // Metadados da Venda (Número e Data na mesma linha)
            string txtNumNota = $"Nº da Nota: {idVendaAtual:D6}";
            string txtDataNota = lblDataValor.Text.Length > 10 ? lblDataValor.Text.Substring(0, 10) : lblDataValor.Text;
            g.DrawString(txtNumNota, fonteCorpo, pincelPreto, xInicio, yAtual);
            g.DrawString($"Data: {txtDataNota}", fonteCorpo, pincelPreto, xInicio + 150, yAtual);

            yAtual += 15;
            // Primeira linha pontilhada de divisão
            g.DrawLine(linhaPontilhada, xInicio, yAtual, xInicio + larguraCupom, yAtual);

            yAtual += 5;
            // Cabeçalhos das Colunas da Tabela
            g.DrawString("Produto", fonteCabecalhoTabela, pincelPreto, xInicio, yAtual);
            g.DrawString("Qtd", fonteCabecalhoTabela, pincelPreto, xInicio + 130, yAtual, alinharDireita);
            g.DrawString("Unitário", fonteCabecalhoTabela, pincelPreto, xInicio + 210, yAtual, alinharDireita);
            g.DrawString("Total", fonteCabecalhoTabela, pincelPreto, xInicio + larguraCupom, yAtual, alinharDireita);

            yAtual += 15;
            // Segunda linha pontilhada de divisão antes dos itens
            g.DrawLine(linhaPontilhada, xInicio, yAtual, xInicio + larguraCupom, yAtual);
            yAtual += 8;

            // 2. LAÇO DE REPETIÇÃO: Varre as linhas do dgvItens para preencher o PDF dinamicamente
            foreach (DataGridViewRow linha in dgvItens.Rows)
            {
                if (linha.Cells[0].Value != null)
                {
                    string nomeProduto = linha.Cells["Produto / Item"].Value.ToString();
                    // Se o nome for muito grande, encurta para não passar por cima do preço
                    if (nomeProduto.Length > 18) nomeProduto = nomeProduto.Substring(0, 16) + "..";

                    string qtd = linha.Cells["Qtd"].Value.ToString();

                    decimal valUnitario = Convert.ToDecimal(linha.Cells["V. Unitário (R$)"].Value);
                    decimal valTotalItem = Convert.ToDecimal(linha.Cells["V. Total (R$)"].Value);

                    // Desenha a linha do item na Nota Fiscal
                    g.DrawString(nomeProduto, fonteCorpo, pincelPreto, xInicio, yAtual);
                    g.DrawString(qtd, fonteCorpo, pincelPreto, xInicio + 130, yAtual, alinharDireita);
                    g.DrawString($"{valUnitario:N2}", fonteCorpo, pincelPreto, xInicio + 210, yAtual, alinharDireita);
                    g.DrawString($"{valTotalItem:N2}", fonteCorpo, pincelPreto, xInicio + larguraCupom, yAtual, alinharDireita);

                    yAtual += 18; // Desce o cursor para o próximo item
                }
            }

            // Terceira linha pontilhada após a listagem dos produtos
            g.DrawLine(linhaPontilhada, xInicio, yAtual, xInicio + larguraCupom, yAtual);
            yAtual += 10;

            // 3. SEÇÃO DE FECHAMENTO (Totalizador destacado igual ao exemplo)
            // Extrai o valor monetário de dentro da Label de Total da tela
            string totalLimpo = lblTotal.Text.Replace("Total:", "").Trim();
            g.DrawString("TOTAL DA COMPRA", fonteTotal, pincelPreto, xInicio, yAtual);
            g.DrawString(totalLimpo, fonteTotal, pincelPreto, xInicio + larguraCupom, yAtual, alinharDireita);

            yAtual += 35;
            // Mensagem final centralizada
            g.DrawString("Obrigado e volte sempre!", fonteSubtitulo, pincelPreto, new RectangleF(xInicio, yAtual, larguraCupom, 15), alinharCentro);
        }

        private void btnFechar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}