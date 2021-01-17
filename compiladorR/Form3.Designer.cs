namespace compiladorR
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mensaje = new System.Windows.Forms.Label();
            this.si = new System.Windows.Forms.Button();
            this.no = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mensaje
            // 
            this.mensaje.AutoSize = true;
            this.mensaje.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.mensaje.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.mensaje.Location = new System.Drawing.Point(104, 105);
            this.mensaje.Name = "mensaje";
            this.mensaje.Size = new System.Drawing.Size(291, 20);
            this.mensaje.TabIndex = 0;
            this.mensaje.Text = "¿Desea guardar los cambios en el archivo?";
            // 
            // si
            // 
            this.si.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.si.FlatAppearance.BorderSize = 0;
            this.si.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.si.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.si.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.si.Location = new System.Drawing.Point(214, 213);
            this.si.Name = "si";
            this.si.Size = new System.Drawing.Size(125, 25);
            this.si.TabIndex = 1;
            this.si.Text = "Si";
            this.si.UseVisualStyleBackColor = false;
            this.si.Click += new System.EventHandler(this.si_Click);
            // 
            // no
            // 
            this.no.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.no.FlatAppearance.BorderSize = 0;
            this.no.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.no.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.no.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.no.Location = new System.Drawing.Point(363, 213);
            this.no.Name = "no";
            this.no.Size = new System.Drawing.Size(125, 25);
            this.no.TabIndex = 2;
            this.no.Text = "No";
            this.no.UseVisualStyleBackColor = false;
            this.no.Click += new System.EventHandler(this.no_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::compiladorR.Properties.Resources.cerrar;
            this.pictureBox1.Location = new System.Drawing.Point(468, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(500, 250);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.no);
            this.Controls.Add(this.si);
            this.Controls.Add(this.mensaje);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form3";
            this.Text = "Form3";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form3_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mensaje;
        private System.Windows.Forms.Button si;
        private System.Windows.Forms.Button no;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}