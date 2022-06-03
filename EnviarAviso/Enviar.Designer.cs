namespace SendFTP
{
    partial class Send
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Send));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timerCount = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbSpareParts = new System.Windows.Forms.RadioButton();
            this.rdbDemurrage = new System.Windows.Forms.RadioButton();
            this.rdbFTP = new System.Windows.Forms.RadioButton();
            this.lbData = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.TextBox();
            this.lbAviso = new System.Windows.Forms.Label();
            this.lbtime = new System.Windows.Forms.Label();
            this.btParar = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lbRodape = new System.Windows.Forms.Label();
            this.gridShipped = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rdbSFTP = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridShipped)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timerCount
            // 
            this.timerCount.Interval = 1000;
            this.timerCount.Tick += new System.EventHandler(this.timerCount_Tick);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::EnviarAviso.Properties.Resources.carta;
            this.panel1.Controls.Add(this.rdbSFTP);
            this.panel1.Controls.Add(this.rdbSpareParts);
            this.panel1.Controls.Add(this.rdbDemurrage);
            this.panel1.Controls.Add(this.rdbFTP);
            this.panel1.Controls.Add(this.lbData);
            this.panel1.Controls.Add(this.txtData);
            this.panel1.Controls.Add(this.lbAviso);
            this.panel1.Controls.Add(this.lbtime);
            this.panel1.Controls.Add(this.btParar);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.lbRodape);
            this.panel1.Controls.Add(this.gridShipped);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(629, 510);
            this.panel1.TabIndex = 0;
            // 
            // rdbSpareParts
            // 
            this.rdbSpareParts.AutoSize = true;
            this.rdbSpareParts.BackColor = System.Drawing.Color.Transparent;
            this.rdbSpareParts.Location = new System.Drawing.Point(309, 39);
            this.rdbSpareParts.Name = "rdbSpareParts";
            this.rdbSpareParts.Size = new System.Drawing.Size(100, 17);
            this.rdbSpareParts.TabIndex = 46;
            this.rdbSpareParts.Text = "SPARE PARTS";
            this.rdbSpareParts.UseVisualStyleBackColor = false;
            // 
            // rdbDemurrage
            // 
            this.rdbDemurrage.AutoSize = true;
            this.rdbDemurrage.BackColor = System.Drawing.Color.Transparent;
            this.rdbDemurrage.Location = new System.Drawing.Point(309, 19);
            this.rdbDemurrage.Name = "rdbDemurrage";
            this.rdbDemurrage.Size = new System.Drawing.Size(95, 17);
            this.rdbDemurrage.TabIndex = 45;
            this.rdbDemurrage.Text = "DEMURRAGE";
            this.rdbDemurrage.UseVisualStyleBackColor = false;
            // 
            // rdbFTP
            // 
            this.rdbFTP.AutoSize = true;
            this.rdbFTP.BackColor = System.Drawing.Color.Transparent;
            this.rdbFTP.Checked = true;
            this.rdbFTP.Location = new System.Drawing.Point(309, 1);
            this.rdbFTP.Name = "rdbFTP";
            this.rdbFTP.Size = new System.Drawing.Size(45, 17);
            this.rdbFTP.TabIndex = 44;
            this.rdbFTP.TabStop = true;
            this.rdbFTP.Text = "FTP";
            this.rdbFTP.UseVisualStyleBackColor = false;
            // 
            // lbData
            // 
            this.lbData.AutoSize = true;
            this.lbData.BackColor = System.Drawing.Color.Transparent;
            this.lbData.Location = new System.Drawing.Point(433, 3);
            this.lbData.Name = "lbData";
            this.lbData.Size = new System.Drawing.Size(110, 13);
            this.lbData.TabIndex = 43;
            this.lbData.Text = "DATA  YYYY-MM-DD";
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(436, 19);
            this.txtData.MaxLength = 10;
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(116, 19);
            this.txtData.TabIndex = 42;
            // 
            // lbAviso
            // 
            this.lbAviso.AutoSize = true;
            this.lbAviso.BackColor = System.Drawing.Color.Transparent;
            this.lbAviso.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAviso.ForeColor = System.Drawing.Color.Red;
            this.lbAviso.Location = new System.Drawing.Point(271, 150);
            this.lbAviso.Name = "lbAviso";
            this.lbAviso.Size = new System.Drawing.Size(148, 25);
            this.lbAviso.TabIndex = 41;
            this.lbAviso.Text = "mensagem erro";
            this.lbAviso.Visible = false;
            // 
            // lbtime
            // 
            this.lbtime.AutoSize = true;
            this.lbtime.BackColor = System.Drawing.Color.Transparent;
            this.lbtime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtime.Location = new System.Drawing.Point(134, 14);
            this.lbtime.Name = "lbtime";
            this.lbtime.Size = new System.Drawing.Size(27, 17);
            this.lbtime.TabIndex = 24;
            this.lbtime.Text = "0:s";
            // 
            // btParar
            // 
            this.btParar.BackColor = System.Drawing.Color.Transparent;
            this.btParar.Image = ((System.Drawing.Image)(resources.GetObject("btParar.Image")));
            this.btParar.Location = new System.Drawing.Point(239, 3);
            this.btParar.Name = "btParar";
            this.btParar.Size = new System.Drawing.Size(48, 35);
            this.btParar.TabIndex = 23;
            this.btParar.UseVisualStyleBackColor = false;
            this.btParar.Click += new System.EventHandler(this.btParar_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(185, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 35);
            this.btnRefresh.TabIndex = 22;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lbRodape
            // 
            this.lbRodape.AutoSize = true;
            this.lbRodape.BackColor = System.Drawing.Color.Transparent;
            this.lbRodape.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRodape.ForeColor = System.Drawing.Color.White;
            this.lbRodape.Location = new System.Drawing.Point(3, 487);
            this.lbRodape.Name = "lbRodape";
            this.lbRodape.Size = new System.Drawing.Size(46, 13);
            this.lbRodape.TabIndex = 21;
            this.lbRodape.Text = "rodape";
            // 
            // gridShipped
            // 
            this.gridShipped.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridShipped.BackgroundColor = System.Drawing.Color.White;
            this.gridShipped.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridShipped.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.gridShipped.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.gridShipped.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridShipped.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridShipped.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.gridShipped.Location = new System.Drawing.Point(0, 302);
            this.gridShipped.Name = "gridShipped";
            this.gridShipped.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridShipped.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridShipped.RowHeadersVisible = false;
            this.gridShipped.Size = new System.Drawing.Size(626, 182);
            this.gridShipped.TabIndex = 40;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EnviarAviso.Properties.Resources.miniLogo;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 35);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // rdbSFTP
            // 
            this.rdbSFTP.AutoSize = true;
            this.rdbSFTP.BackColor = System.Drawing.Color.Transparent;
            this.rdbSFTP.Location = new System.Drawing.Point(309, 58);
            this.rdbSFTP.Name = "rdbSFTP";
            this.rdbSFTP.Size = new System.Drawing.Size(52, 17);
            this.rdbSFTP.TabIndex = 47;
            this.rdbSFTP.Text = "SFTP";
            this.rdbSFTP.UseVisualStyleBackColor = false;
            // 
            // Send
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 510);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "Send";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ENVIAR AVISO - V1.0.0.6";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridShipped)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView gridShipped;
        private System.Windows.Forms.Label lbRodape;
        private System.Windows.Forms.Label lbtime;
        private System.Windows.Forms.Button btParar;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Timer timerCount;
        private System.Windows.Forms.Label lbAviso;
        private System.Windows.Forms.Label lbData;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.RadioButton rdbDemurrage;
        private System.Windows.Forms.RadioButton rdbFTP;
        private System.Windows.Forms.RadioButton rdbSpareParts;
        private System.Windows.Forms.RadioButton rdbSFTP;

    }
}

