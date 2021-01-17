
namespace compiladorR
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.aceptar = new System.Windows.Forms.Button();
            this.cerrar = new System.Windows.Forms.PictureBox();
            this.nomVar = new System.Windows.Forms.Label();
            this.tipoVar = new System.Windows.Forms.Label();
            this.nombreVar = new System.Windows.Forms.Label();
            this.tipoVariable = new System.Windows.Forms.Label();
            this.moverVentana = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(37)))), ((int)(((byte)(44)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(12, 115);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(476, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // aceptar
            // 
            this.aceptar.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.aceptar.FlatAppearance.BorderSize = 0;
            this.aceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aceptar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aceptar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.aceptar.Location = new System.Drawing.Point(363, 213);
            this.aceptar.Name = "aceptar";
            this.aceptar.Size = new System.Drawing.Size(125, 25);
            this.aceptar.TabIndex = 1;
            this.aceptar.Text = "Aceptar";
            this.aceptar.UseVisualStyleBackColor = false;
            this.aceptar.Click += new System.EventHandler(this.aceptar_Click);
            // 
            // cerrar
            // 
            this.cerrar.Image = global::compiladorR.Properties.Resources.cerrar;
            this.cerrar.Location = new System.Drawing.Point(468, 12);
            this.cerrar.Name = "cerrar";
            this.cerrar.Size = new System.Drawing.Size(20, 20);
            this.cerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cerrar.TabIndex = 2;
            this.cerrar.TabStop = false;
            this.cerrar.Click += new System.EventHandler(this.cerrar_Click);
            // 
            // nomVar
            // 
            this.nomVar.AutoSize = true;
            this.nomVar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.nomVar.Location = new System.Drawing.Point(12, 54);
            this.nomVar.Name = "nomVar";
            this.nomVar.Size = new System.Drawing.Size(141, 17);
            this.nomVar.TabIndex = 3;
            this.nomVar.Text = "Nombre de la Variable";
            // 
            // tipoVar
            // 
            this.tipoVar.AutoSize = true;
            this.tipoVar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipoVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.tipoVar.Location = new System.Drawing.Point(168, 54);
            this.tipoVar.Name = "tipoVar";
            this.tipoVar.Size = new System.Drawing.Size(104, 17);
            this.tipoVar.TabIndex = 4;
            this.tipoVar.Text = "Tipo de Variable";
            // 
            // nombreVar
            // 
            this.nombreVar.AutoSize = true;
            this.nombreVar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombreVar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.nombreVar.Location = new System.Drawing.Point(12, 80);
            this.nombreVar.Name = "nombreVar";
            this.nombreVar.Size = new System.Drawing.Size(50, 20);
            this.nombreVar.TabIndex = 5;
            this.nombreVar.Text = "label1";
            // 
            // tipoVariable
            // 
            this.tipoVariable.AutoSize = true;
            this.tipoVariable.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipoVariable.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tipoVariable.Location = new System.Drawing.Point(168, 80);
            this.tipoVariable.Name = "tipoVariable";
            this.tipoVariable.Size = new System.Drawing.Size(50, 20);
            this.tipoVariable.TabIndex = 6;
            this.tipoVariable.Text = "label1";
            // 
            // moverVentana
            // 
            this.moverVentana.Fixed = true;
            this.moverVentana.Horizontal = true;
            this.moverVentana.TargetControl = this;
            this.moverVentana.Vertical = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(500, 250);
            this.Controls.Add(this.tipoVariable);
            this.Controls.Add(this.nombreVar);
            this.Controls.Add(this.tipoVar);
            this.Controls.Add(this.nomVar);
            this.Controls.Add(this.cerrar);
            this.Controls.Add(this.aceptar);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.cerrar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button aceptar;
        private System.Windows.Forms.PictureBox cerrar;
        private System.Windows.Forms.Label nomVar;
        private System.Windows.Forms.Label tipoVar;
        private System.Windows.Forms.Label nombreVar;
        private System.Windows.Forms.Label tipoVariable;
        private Bunifu.Framework.UI.BunifuDragControl moverVentana;
    }
}