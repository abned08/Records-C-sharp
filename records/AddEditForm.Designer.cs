namespace records
{
    partial class AddEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditForm));
            this.cbRecordAE = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOutAE = new System.Windows.Forms.RadioButton();
            this.rbInAE = new System.Windows.Forms.RadioButton();
            this.cbServiceAE = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpRecordAE = new System.Windows.Forms.DateTimePicker();
            this.txtQuantityAE = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.recBtnSaveAE = new System.Windows.Forms.Button();
            this.txtIDAE = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbRecordAE
            // 
            this.cbRecordAE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRecordAE.DisplayMember = "recordName";
            this.cbRecordAE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRecordAE.FormattingEnabled = true;
            this.cbRecordAE.Location = new System.Drawing.Point(24, 39);
            this.cbRecordAE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbRecordAE.Name = "cbRecordAE";
            this.cbRecordAE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbRecordAE.Size = new System.Drawing.Size(392, 27);
            this.cbRecordAE.TabIndex = 45;
            this.cbRecordAE.ValueMember = "codeID";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(439, 196);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label10.Size = new System.Drawing.Size(78, 19);
            this.label10.TabIndex = 44;
            this.label10.Text = "طبيعة العملية";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbOutAE);
            this.groupBox1.Controls.Add(this.rbInAE);
            this.groupBox1.Location = new System.Drawing.Point(217, 176);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(200, 47);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            // 
            // rbOutAE
            // 
            this.rbOutAE.AutoSize = true;
            this.rbOutAE.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbOutAE.Location = new System.Drawing.Point(16, 16);
            this.rbOutAE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbOutAE.Name = "rbOutAE";
            this.rbOutAE.Size = new System.Drawing.Size(58, 23);
            this.rbOutAE.TabIndex = 21;
            this.rbOutAE.Text = "خروج";
            this.rbOutAE.UseVisualStyleBackColor = false;
            // 
            // rbInAE
            // 
            this.rbInAE.AutoSize = true;
            this.rbInAE.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.rbInAE.FlatAppearance.BorderSize = 10;
            this.rbInAE.Location = new System.Drawing.Point(107, 16);
            this.rbInAE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbInAE.Name = "rbInAE";
            this.rbInAE.Size = new System.Drawing.Size(54, 23);
            this.rbInAE.TabIndex = 20;
            this.rbInAE.Text = "دخول";
            this.rbInAE.UseVisualStyleBackColor = true;
            // 
            // cbServiceAE
            // 
            this.cbServiceAE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbServiceAE.DisplayMember = "serviceName";
            this.cbServiceAE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServiceAE.FormattingEnabled = true;
            this.cbServiceAE.Location = new System.Drawing.Point(25, 243);
            this.cbServiceAE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbServiceAE.Name = "cbServiceAE";
            this.cbServiceAE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbServiceAE.Size = new System.Drawing.Size(392, 27);
            this.cbServiceAE.TabIndex = 42;
            this.cbServiceAE.ValueMember = "serviceID";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(439, 251);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(55, 19);
            this.label7.TabIndex = 41;
            this.label7.Text = "المصلحة";
            // 
            // dtpRecordAE
            // 
            this.dtpRecordAE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpRecordAE.CalendarFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.dtpRecordAE.CalendarTrailingForeColor = System.Drawing.SystemColors.Desktop;
            this.dtpRecordAE.CustomFormat = "dd/MM/yyyy";
            this.dtpRecordAE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRecordAE.Location = new System.Drawing.Point(25, 88);
            this.dtpRecordAE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpRecordAE.Name = "dtpRecordAE";
            this.dtpRecordAE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dtpRecordAE.RightToLeftLayout = true;
            this.dtpRecordAE.Size = new System.Drawing.Size(392, 26);
            this.dtpRecordAE.TabIndex = 40;
            // 
            // txtQuantityAE
            // 
            this.txtQuantityAE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuantityAE.Location = new System.Drawing.Point(25, 133);
            this.txtQuantityAE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtQuantityAE.MaxLength = 6;
            this.txtQuantityAE.Name = "txtQuantityAE";
            this.txtQuantityAE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtQuantityAE.Size = new System.Drawing.Size(392, 26);
            this.txtQuantityAE.TabIndex = 39;
            this.txtQuantityAE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantityAE_KeyPress);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(439, 136);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(41, 19);
            this.label6.TabIndex = 38;
            this.label6.Text = "الكمية";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(439, 94);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(45, 19);
            this.label5.TabIndex = 37;
            this.label5.Text = "التاريخ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(439, 47);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(63, 19);
            this.label3.TabIndex = 36;
            this.label3.Text = " المبطوعة";
            // 
            // recBtnSaveAE
            // 
            this.recBtnSaveAE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.recBtnSaveAE.Location = new System.Drawing.Point(180, 315);
            this.recBtnSaveAE.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.recBtnSaveAE.Name = "recBtnSaveAE";
            this.recBtnSaveAE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.recBtnSaveAE.Size = new System.Drawing.Size(168, 38);
            this.recBtnSaveAE.TabIndex = 46;
            this.recBtnSaveAE.Text = "حفظ";
            this.recBtnSaveAE.UseVisualStyleBackColor = true;
            this.recBtnSaveAE.Click += new System.EventHandler(this.recBtnSaveAE_Click);
            // 
            // txtIDAE
            // 
            this.txtIDAE.Location = new System.Drawing.Point(0, 0);
            this.txtIDAE.Name = "txtIDAE";
            this.txtIDAE.Size = new System.Drawing.Size(78, 26);
            this.txtIDAE.TabIndex = 48;
            this.txtIDAE.Visible = false;
            // 
            // AddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 366);
            this.Controls.Add(this.txtIDAE);
            this.Controls.Add(this.recBtnSaveAE);
            this.Controls.Add(this.cbRecordAE);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbServiceAE);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpRecordAE);
            this.Controls.Add(this.txtQuantityAE);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "AddEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إضافة";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddEditForm_FormClosed);
            this.Load += new System.EventHandler(this.AddEditForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ComboBox cbRecordAE;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox cbServiceAE;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.DateTimePicker dtpRecordAE;
        public System.Windows.Forms.TextBox txtQuantityAE;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button recBtnSaveAE;
        public System.Windows.Forms.RadioButton rbOutAE;
        public System.Windows.Forms.RadioButton rbInAE;
        public System.Windows.Forms.TextBox txtIDAE;
    }
}