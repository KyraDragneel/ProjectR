﻿namespace compiladorR
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.entrada = new System.Windows.Forms.RichTextBox();
            this.areaResultado = new System.Windows.Forms.RichTextBox();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.nomArchivo = new System.Windows.Forms.Label();
            this.menuArchivo = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelizquierdo = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.moverVentana = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panelEstado = new Bunifu.Framework.UI.BunifuGradientPanel();
            this.run = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.minimizar = new System.Windows.Forms.PictureBox();
            this.restaurar = new System.Windows.Forms.PictureBox();
            this.maximizar = new System.Windows.Forms.PictureBox();
            this.salir = new System.Windows.Forms.PictureBox();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelSuperior.SuspendLayout();
            this.menuArchivo.SuspendLayout();
            this.panelizquierdo.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.restaurar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.salir)).BeginInit();
            this.SuspendLayout();
            // 
            // entrada
            // 
            this.entrada.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entrada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(37)))), ((int)(((byte)(44)))));
            this.entrada.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.entrada.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entrada.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.entrada.Location = new System.Drawing.Point(62, 16);
            this.entrada.Name = "entrada";
            this.entrada.Size = new System.Drawing.Size(1198, 493);
            this.entrada.TabIndex = 0;
            this.entrada.Text = "";
            this.entrada.WordWrap = false;
            this.entrada.Click += new System.EventHandler(this.entrada_Click);
            this.entrada.KeyDown += new System.Windows.Forms.KeyEventHandler(this.entrada_KeyDown);
            // 
            // areaResultado
            // 
            this.areaResultado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.areaResultado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(37)))), ((int)(((byte)(44)))));
            this.areaResultado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.areaResultado.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.areaResultado.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.areaResultado.Location = new System.Drawing.Point(21, 522);
            this.areaResultado.Name = "areaResultado";
            this.areaResultado.ReadOnly = true;
            this.areaResultado.Size = new System.Drawing.Size(1239, 187);
            this.areaResultado.TabIndex = 1;
            this.areaResultado.Text = "";
            this.areaResultado.WordWrap = false;
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(25)))));
            this.panelSuperior.Controls.Add(this.nomArchivo);
            this.panelSuperior.Controls.Add(this.pictureBox1);
            this.panelSuperior.Controls.Add(this.minimizar);
            this.panelSuperior.Controls.Add(this.restaurar);
            this.panelSuperior.Controls.Add(this.maximizar);
            this.panelSuperior.Controls.Add(this.salir);
            this.panelSuperior.Controls.Add(this.menuArchivo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1338, 40);
            this.panelSuperior.TabIndex = 3;
            // 
            // nomArchivo
            // 
            this.nomArchivo.AutoSize = true;
            this.nomArchivo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomArchivo.ForeColor = System.Drawing.SystemColors.Control;
            this.nomArchivo.Location = new System.Drawing.Point(634, 11);
            this.nomArchivo.Name = "nomArchivo";
            this.nomArchivo.Size = new System.Drawing.Size(58, 17);
            this.nomArchivo.TabIndex = 6;
            this.nomArchivo.Text = "Sin titulo";
            // 
            // menuArchivo
            // 
            this.menuArchivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(25)))));
            this.menuArchivo.Dock = System.Windows.Forms.DockStyle.None;
            this.menuArchivo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuArchivo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ayudaToolStripMenuItem});
            this.menuArchivo.Location = new System.Drawing.Point(51, 9);
            this.menuArchivo.Name = "menuArchivo";
            this.menuArchivo.Size = new System.Drawing.Size(247, 25);
            this.menuArchivo.TabIndex = 4;
            this.menuArchivo.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.guardarComoToolStripMenuItem});
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(63, 21);
            this.toolStripMenuItem1.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(25)))));
            this.nuevoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(25)))));
            this.abrirToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(25)))));
            this.guardarToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(25)))));
            this.guardarComoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar como...";
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.guardarComoToolStripMenuItem_Click);
            // 
            // panelizquierdo
            // 
            this.panelizquierdo.Controls.Add(this.run);
            this.panelizquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelizquierdo.Location = new System.Drawing.Point(0, 40);
            this.panelizquierdo.Name = "panelizquierdo";
            this.panelizquierdo.Size = new System.Drawing.Size(45, 741);
            this.panelizquierdo.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Controls.Add(this.panelEstado);
            this.panel3.Controls.Add(this.bunifuSeparator1);
            this.panel3.Controls.Add(this.entrada);
            this.panel3.Controls.Add(this.areaResultado);
            this.panel3.Location = new System.Drawing.Point(51, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1284, 741);
            this.panel3.TabIndex = 5;
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(25)))));
            this.bunifuSeparator1.LineThickness = 2;
            this.bunifuSeparator1.Location = new System.Drawing.Point(21, 513);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(1239, 4);
            this.bunifuSeparator1.TabIndex = 1;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // moverVentana
            // 
            this.moverVentana.Fixed = true;
            this.moverVentana.Horizontal = true;
            this.moverVentana.TargetControl = this.panelSuperior;
            this.moverVentana.Vertical = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(21, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 493);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            // 
            // panelEstado
            // 
            this.panelEstado.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelEstado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelEstado.BackgroundImage")));
            this.panelEstado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEstado.GradientBottomLeft = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(169)))), ((int)(((byte)(104)))));
            this.panelEstado.GradientBottomRight = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(169)))), ((int)(((byte)(104)))));
            this.panelEstado.GradientTopLeft = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(169)))), ((int)(((byte)(104)))));
            this.panelEstado.GradientTopRight = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(230)))), ((int)(((byte)(71)))));
            this.panelEstado.Location = new System.Drawing.Point(0, 715);
            this.panelEstado.Name = "panelEstado";
            this.panelEstado.Quality = 10;
            this.panelEstado.Size = new System.Drawing.Size(1284, 26);
            this.panelEstado.TabIndex = 3;
            // 
            // run
            // 
            this.run.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.run.BackgroundImage = global::compiladorR.Properties.Resources.run1;
            this.run.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.run.FlatAppearance.BorderSize = 0;
            this.run.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.run.Location = new System.Drawing.Point(16, 13);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(16, 16);
            this.run.TabIndex = 2;
            this.run.UseVisualStyleBackColor = false;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::compiladorR.Properties.Resources.icon_raccoon;
            this.pictureBox1.Location = new System.Drawing.Point(9, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // minimizar
            // 
            this.minimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizar.Image = global::compiladorR.Properties.Resources.minimizar;
            this.minimizar.Location = new System.Drawing.Point(1252, 5);
            this.minimizar.Name = "minimizar";
            this.minimizar.Size = new System.Drawing.Size(14, 14);
            this.minimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.minimizar.TabIndex = 3;
            this.minimizar.TabStop = false;
            this.minimizar.Click += new System.EventHandler(this.Minimizar_Click);
            // 
            // restaurar
            // 
            this.restaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.restaurar.Image = global::compiladorR.Properties.Resources.restaurar;
            this.restaurar.Location = new System.Drawing.Point(1283, 5);
            this.restaurar.Name = "restaurar";
            this.restaurar.Size = new System.Drawing.Size(14, 14);
            this.restaurar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.restaurar.TabIndex = 2;
            this.restaurar.TabStop = false;
            this.restaurar.Visible = false;
            this.restaurar.Click += new System.EventHandler(this.Restaurar_Click);
            // 
            // maximizar
            // 
            this.maximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximizar.Image = global::compiladorR.Properties.Resources.maximizar;
            this.maximizar.Location = new System.Drawing.Point(1283, 5);
            this.maximizar.Name = "maximizar";
            this.maximizar.Size = new System.Drawing.Size(14, 14);
            this.maximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.maximizar.TabIndex = 1;
            this.maximizar.TabStop = false;
            this.maximizar.Click += new System.EventHandler(this.Maximizar_Click);
            // 
            // salir
            // 
            this.salir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.salir.Image = global::compiladorR.Properties.Resources.cerrar;
            this.salir.Location = new System.Drawing.Point(1312, 5);
            this.salir.Name = "salir";
            this.salir.Size = new System.Drawing.Size(14, 14);
            this.salir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.salir.TabIndex = 0;
            this.salir.TabStop = false;
            this.salir.Click += new System.EventHandler(this.Salir_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem});
            this.ayudaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(25)))));
            this.acercaDeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.acercaDeToolStripMenuItem.Text = "Acerca de...";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1338, 781);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelizquierdo);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuArchivo;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.menuArchivo.ResumeLayout(false);
            this.menuArchivo.PerformLayout();
            this.panelizquierdo.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.restaurar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.salir)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox entrada;
        private System.Windows.Forms.RichTextBox areaResultado;
        private System.Windows.Forms.Button run;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Panel panelizquierdo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox minimizar;
        private System.Windows.Forms.PictureBox restaurar;
        private System.Windows.Forms.PictureBox maximizar;
        private System.Windows.Forms.PictureBox salir;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private Bunifu.Framework.UI.BunifuDragControl moverVentana;
        private Bunifu.Framework.UI.BunifuGradientPanel panelEstado;
        private System.Windows.Forms.MenuStrip menuArchivo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.Label nomArchivo;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
    }
}

