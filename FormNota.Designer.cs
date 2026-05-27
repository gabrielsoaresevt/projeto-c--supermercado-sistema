

namespace Sistema_de_Supermercado
{
    partial class FormNota
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing &&
                (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTopo = new System.Windows.Forms.Panel();
            this.lblIcone = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblNumeroNota = new System.Windows.Forms.Label();
            this.lblNumeroNotaValor = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.lblDataValor = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblClienteValor = new System.Windows.Forms.Label();
            this.lblPagamento = new System.Windows.Forms.Label();
            this.lblPagamentoValor = new System.Windows.Forms.Label();
            this.panelItens = new System.Windows.Forms.Panel();
            this.dgvItens = new System.Windows.Forms.DataGridView();
            this.panelResumo = new System.Windows.Forms.Panel();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblDesconto = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSalvarPdf = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.panelTopo.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.panelItens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).BeginInit();
            this.panelResumo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTopo
            // 
            this.panelTopo.BackColor = System.Drawing.Color.White;
            this.panelTopo.Controls.Add(this.lblIcone);
            this.panelTopo.Controls.Add(this.lblTitulo);
            this.panelTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopo.Location = new System.Drawing.Point(0, 0);
            this.panelTopo.Name = "panelTopo";
            this.panelTopo.Size = new System.Drawing.Size(1370, 90);
            this.panelTopo.TabIndex = 0;
            // 
            // lblIcone
            // 
            this.lblIcone.AutoSize = true;
            this.lblIcone.Font = new System.Drawing.Font("Segoe UI Emoji", 28F);
            this.lblIcone.Location = new System.Drawing.Point(25, 15);
            this.lblIcone.Name = "lblIcone";
            this.lblIcone.Size = new System.Drawing.Size(74, 51);
            this.lblIcone.TabIndex = 0;
            this.lblIcone.Text = "🧾";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(95, 22);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(173, 41);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Nota Fiscal";
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.White;
            this.panelInfo.Controls.Add(this.lblNumeroNota);
            this.panelInfo.Controls.Add(this.lblNumeroNotaValor);
            this.panelInfo.Controls.Add(this.lblData);
            this.panelInfo.Controls.Add(this.lblDataValor);
            this.panelInfo.Controls.Add(this.lblCliente);
            this.panelInfo.Controls.Add(this.lblClienteValor);
            this.panelInfo.Controls.Add(this.lblPagamento);
            this.panelInfo.Controls.Add(this.lblPagamentoValor);
            this.panelInfo.Location = new System.Drawing.Point(12, 120);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(320, 280);
            this.panelInfo.TabIndex = 1;
            // 
            // lblNumeroNota
            // 
            this.lblNumeroNota.AutoSize = true;
            this.lblNumeroNota.Location = new System.Drawing.Point(25, 40);
            this.lblNumeroNota.Name = "lblNumeroNota";
            this.lblNumeroNota.Size = new System.Drawing.Size(47, 13);
            this.lblNumeroNota.TabIndex = 0;
            this.lblNumeroNota.Text = "Número:";
            // 
            // lblNumeroNotaValor
            // 
            this.lblNumeroNotaValor.AutoSize = true;
            this.lblNumeroNotaValor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNumeroNotaValor.Location = new System.Drawing.Point(150, 40);
            this.lblNumeroNotaValor.Name = "lblNumeroNotaValor";
            this.lblNumeroNotaValor.Size = new System.Drawing.Size(0, 19);
            this.lblNumeroNotaValor.TabIndex = 1;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(25, 90);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(33, 13);
            this.lblData.TabIndex = 2;
            this.lblData.Text = "Data:";
            // 
            // lblDataValor
            // 
            this.lblDataValor.AutoSize = true;
            this.lblDataValor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDataValor.Location = new System.Drawing.Point(150, 90);
            this.lblDataValor.Name = "lblDataValor";
            this.lblDataValor.Size = new System.Drawing.Size(0, 19);
            this.lblDataValor.TabIndex = 3;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(25, 140);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(42, 13);
            this.lblCliente.TabIndex = 4;
            this.lblCliente.Text = "Cliente:";
            // 
            // lblClienteValor
            // 
            this.lblClienteValor.AutoSize = true;
            this.lblClienteValor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblClienteValor.Location = new System.Drawing.Point(150, 140);
            this.lblClienteValor.Name = "lblClienteValor";
            this.lblClienteValor.Size = new System.Drawing.Size(0, 19);
            this.lblClienteValor.TabIndex = 5;
            // 
            // lblPagamento
            // 
            this.lblPagamento.AutoSize = true;
            this.lblPagamento.Location = new System.Drawing.Point(25, 190);
            this.lblPagamento.Name = "lblPagamento";
            this.lblPagamento.Size = new System.Drawing.Size(64, 13);
            this.lblPagamento.TabIndex = 6;
            this.lblPagamento.Text = "Pagamento:";
            // 
            // lblPagamentoValor
            // 
            this.lblPagamentoValor.AutoSize = true;
            this.lblPagamentoValor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPagamentoValor.Location = new System.Drawing.Point(150, 190);
            this.lblPagamentoValor.Name = "lblPagamentoValor";
            this.lblPagamentoValor.Size = new System.Drawing.Size(0, 19);
            this.lblPagamentoValor.TabIndex = 7;
            // 
            // panelItens
            // 
            this.panelItens.BackColor = System.Drawing.Color.White;
            this.panelItens.Controls.Add(this.dgvItens);
            this.panelItens.Location = new System.Drawing.Point(338, 109);
            this.panelItens.Name = "panelItens";
            this.panelItens.Size = new System.Drawing.Size(580, 500);
            this.panelItens.TabIndex = 2;
            // 
            // dgvItens
            // 
            this.dgvItens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItens.BackgroundColor = System.Drawing.Color.White;
            this.dgvItens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItens.Location = new System.Drawing.Point(0, 0);
            this.dgvItens.Name = "dgvItens";
            this.dgvItens.RowTemplate.Height = 35;
            this.dgvItens.Size = new System.Drawing.Size(580, 500);
            this.dgvItens.TabIndex = 0;
            // 
            // panelResumo
            // 
            this.panelResumo.BackColor = System.Drawing.Color.White;
            this.panelResumo.Controls.Add(this.lblSubtotal);
            this.panelResumo.Controls.Add(this.lblDesconto);
            this.panelResumo.Controls.Add(this.lblTotal);
            this.panelResumo.Controls.Add(this.btnImprimir);
            this.panelResumo.Controls.Add(this.btnSalvarPdf);
            this.panelResumo.Controls.Add(this.btnFechar);
            this.panelResumo.Location = new System.Drawing.Point(924, 96);
            this.panelResumo.Name = "panelResumo";
            this.panelResumo.Size = new System.Drawing.Size(350, 545);
            this.panelResumo.TabIndex = 3;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSubtotal.Location = new System.Drawing.Point(25, 60);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(79, 21);
            this.lblSubtotal.TabIndex = 0;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // lblDesconto
            // 
            this.lblDesconto.AutoSize = true;
            this.lblDesconto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDesconto.Location = new System.Drawing.Point(25, 120);
            this.lblDesconto.Name = "lblDesconto";
            this.lblDesconto.Size = new System.Drawing.Size(86, 21);
            this.lblDesconto.TabIndex = 1;
            this.lblDesconto.Text = "Desconto:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblTotal.Location = new System.Drawing.Point(25, 220);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(96, 41);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "Total:";
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(25, 340);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(190, 45);
            this.btnImprimir.TabIndex = 3;
            this.btnImprimir.Text = "🖨️ Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click_1);
            // 
            // btnSalvarPdf
            // 
            this.btnSalvarPdf.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnSalvarPdf.FlatAppearance.BorderSize = 0;
            this.btnSalvarPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarPdf.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalvarPdf.ForeColor = System.Drawing.Color.White;
            this.btnSalvarPdf.Location = new System.Drawing.Point(25, 410);
            this.btnSalvarPdf.Name = "btnSalvarPdf";
            this.btnSalvarPdf.Size = new System.Drawing.Size(190, 45);
            this.btnSalvarPdf.TabIndex = 4;
            this.btnSalvarPdf.Text = "📄 Salvar PDF";
            this.btnSalvarPdf.UseVisualStyleBackColor = false;
            this.btnSalvarPdf.Click += new System.EventHandler(this.btnSalvarPdf_Click_1);
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.Firebrick;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(25, 480);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(190, 45);
            this.btnFechar.TabIndex = 5;
            this.btnFechar.Text = "❌ Fechar";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click_1);
            // 
            // FormNota
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1370, 677);
            this.Controls.Add(this.panelTopo);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.panelItens);
            this.Controls.Add(this.panelResumo);
            this.Name = "FormNota";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nota Fiscal";
            this.Load += new System.EventHandler(this.FormNota_Load);
            this.panelTopo.ResumeLayout(false);
            this.panelTopo.PerformLayout();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.panelItens.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).EndInit();
            this.panelResumo.ResumeLayout(false);
            this.panelResumo.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelTopo;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Panel panelItens;
        private System.Windows.Forms.Panel panelResumo;

        private System.Windows.Forms.Label lblIcone;
        private System.Windows.Forms.Label lblTitulo;

        private System.Windows.Forms.Label lblNumeroNota;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblPagamento;

        public System.Windows.Forms.Label lblNumeroNotaValor;
        public System.Windows.Forms.Label lblDataValor;
        public System.Windows.Forms.Label lblClienteValor;
        public System.Windows.Forms.Label lblPagamentoValor;

        public System.Windows.Forms.DataGridView dgvItens;

        public System.Windows.Forms.Label lblSubtotal;
        public System.Windows.Forms.Label lblDesconto;
        public System.Windows.Forms.Label lblTotal;

        public System.Windows.Forms.Button btnImprimir;
        public System.Windows.Forms.Button btnSalvarPdf;
        public System.Windows.Forms.Button btnFechar;
    }
}