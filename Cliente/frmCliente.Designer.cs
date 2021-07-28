namespace Cliente
{
    partial class frmCliente
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.btnVerificar = new System.Windows.Forms.Button();
            this.btnSolicitar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRespuesta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTipoRespuesta = new System.Windows.Forms.TextBox();
            this.txtIndicador = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPlaca
            // 
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaca.Location = new System.Drawing.Point(12, 48);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(284, 32);
            this.txtPlaca.TabIndex = 3;
            // 
            // btnVerificar
            // 
            this.btnVerificar.Location = new System.Drawing.Point(12, 87);
            this.btnVerificar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.Size = new System.Drawing.Size(141, 38);
            this.btnVerificar.TabIndex = 8;
            this.btnVerificar.Text = "Verificar Dias";
            this.btnVerificar.UseVisualStyleBackColor = true;
            this.btnVerificar.Click += new System.EventHandler(this.btnVerificar_Click);
            // 
            // btnSolicitar
            // 
            this.btnSolicitar.Location = new System.Drawing.Point(158, 87);
            this.btnSolicitar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSolicitar.Name = "btnSolicitar";
            this.btnSolicitar.Size = new System.Drawing.Size(137, 38);
            this.btnSolicitar.TabIndex = 9;
            this.btnSolicitar.Text = "Solicitar Salvoconducto";
            this.btnSolicitar.UseVisualStyleBackColor = true;
            this.btnSolicitar.Click += new System.EventHandler(this.btnSolicitar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "INGRESE SU PLACA (Ej: PRR-0329)";
            // 
            // txtRespuesta
            // 
            this.txtRespuesta.Enabled = false;
            this.txtRespuesta.Location = new System.Drawing.Point(328, 154);
            this.txtRespuesta.Multiline = true;
            this.txtRespuesta.Name = "txtRespuesta";
            this.txtRespuesta.Size = new System.Drawing.Size(254, 32);
            this.txtRespuesta.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(325, 138);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "RESPUESTA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "TIPO RESPUESTA";
            // 
            // txtTipoRespuesta
            // 
            this.txtTipoRespuesta.Enabled = false;
            this.txtTipoRespuesta.Location = new System.Drawing.Point(328, 93);
            this.txtTipoRespuesta.Multiline = true;
            this.txtTipoRespuesta.Name = "txtTipoRespuesta";
            this.txtTipoRespuesta.Size = new System.Drawing.Size(254, 32);
            this.txtTipoRespuesta.TabIndex = 14;
            // 
            // txtIndicador
            // 
            this.txtIndicador.Enabled = false;
            this.txtIndicador.Location = new System.Drawing.Point(465, 38);
            this.txtIndicador.Name = "txtIndicador";
            this.txtIndicador.Size = new System.Drawing.Size(117, 20);
            this.txtIndicador.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(325, 41);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "INDICADOR RESPUESTA";
            // 
            // frmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 234);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtIndicador);
            this.Controls.Add(this.txtTipoRespuesta);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRespuesta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSolicitar);
            this.Controls.Add(this.btnVerificar);
            this.Controls.Add(this.txtPlaca);
            this.Name = "frmCliente";
            this.Text = "Cliente";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClienteForm_FormClosing);
            this.Load += new System.EventHandler(this.ClienteForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Button btnVerificar;
        private System.Windows.Forms.Button btnSolicitar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRespuesta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTipoRespuesta;
        private System.Windows.Forms.TextBox txtIndicador;
        private System.Windows.Forms.Label label4;
    }
}

