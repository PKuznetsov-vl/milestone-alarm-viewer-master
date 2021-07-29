namespace VLAlarmViewer.Admin
{
    partial class AddToFaceStreamDialog
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
            this.tbEndpoint = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbLists = new System.Windows.Forms.ComboBox();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbUdp = new System.Windows.Forms.CheckBox();
            this.cbOpenPreview = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbEndpoint
            // 
            this.tbEndpoint.Location = new System.Drawing.Point(70, 9);
            this.tbEndpoint.Name = "tbEndpoint";
            this.tbEndpoint.Size = new System.Drawing.Size(382, 20);
            this.tbEndpoint.TabIndex = 7;
            this.tbEndpoint.Text = "http://127.0.0.1:34569/";
            this.tbEndpoint.TextChanged += new System.EventHandler(this.tbEndpoint_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Endpoint:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(267, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Note: Do not append the API version!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(377, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(296, 124);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "Add";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(70, 71);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(382, 20);
            this.tbName.TabIndex = 13;
            this.tbName.Text = "Stream1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(67, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "List:";
            // 
            // cbLists
            // 
            this.cbLists.FormattingEnabled = true;
            this.cbLists.Items.AddRange(new object[] {
            "30ef4720-f8a1-4475-98fd-ae5129c8ad8b"});
            this.cbLists.Location = new System.Drawing.Point(70, 97);
            this.cbLists.Name = "cbLists";
            this.cbLists.Size = new System.Drawing.Size(382, 21);
            this.cbLists.TabIndex = 16;
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(70, 48);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(327, 20);
            this.tbUrl.TabIndex = 18;
            this.tbUrl.Text = "rtsp://127.0.0.1:554";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "URL:";
            // 
            // cbUdp
            // 
            this.cbUdp.AutoSize = true;
            this.cbUdp.Location = new System.Drawing.Point(403, 50);
            this.cbUdp.Name = "cbUdp";
            this.cbUdp.Size = new System.Drawing.Size(49, 17);
            this.cbUdp.TabIndex = 19;
            this.cbUdp.Text = "UDP";
            this.cbUdp.UseVisualStyleBackColor = true;
            // 
            // cbOpenPreview
            // 
            this.cbOpenPreview.AutoSize = true;
            this.cbOpenPreview.Location = new System.Drawing.Point(70, 128);
            this.cbOpenPreview.Name = "cbOpenPreview";
            this.cbOpenPreview.Size = new System.Drawing.Size(170, 17);
            this.cbOpenPreview.TabIndex = 20;
            this.cbOpenPreview.Text = "Open preview page in browser";
            this.cbOpenPreview.UseVisualStyleBackColor = true;
            // 
            // AddToFaceStreamDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(464, 156);
            this.ControlBox = false;
            this.Controls.Add(this.cbOpenPreview);
            this.Controls.Add(this.cbUdp);
            this.Controls.Add(this.tbUrl);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbLists);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbEndpoint);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddToFaceStreamDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Add to FaceStream";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AddToFaceStreamDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbEndpoint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbLists;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbUdp;
        private System.Windows.Forms.CheckBox cbOpenPreview;
    }
}