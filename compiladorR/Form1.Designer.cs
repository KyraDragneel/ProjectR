namespace compiladorR
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.entrada = new System.Windows.Forms.RichTextBox();
            this.areaResultado = new System.Windows.Forms.RichTextBox();
            this.run = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // entrada
            // 
            this.entrada.Location = new System.Drawing.Point(12, 12);
            this.entrada.Name = "entrada";
            this.entrada.Size = new System.Drawing.Size(675, 385);
            this.entrada.TabIndex = 0;
            this.entrada.Text = "";
            // 
            // areaResultado
            // 
            this.areaResultado.Location = new System.Drawing.Point(12, 403);
            this.areaResultado.Name = "areaResultado";
            this.areaResultado.Size = new System.Drawing.Size(675, 184);
            this.areaResultado.TabIndex = 1;
            this.areaResultado.Text = "";
            // 
            // run
            // 
            this.run.Location = new System.Drawing.Point(762, 12);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(75, 23);
            this.run.TabIndex = 2;
            this.run.Text = "Run";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 599);
            this.Controls.Add(this.run);
            this.Controls.Add(this.areaResultado);
            this.Controls.Add(this.entrada);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox entrada;
        private System.Windows.Forms.RichTextBox areaResultado;
        private System.Windows.Forms.Button run;
    }
}

