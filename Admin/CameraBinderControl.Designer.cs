namespace VLAlarmViewer.Admin
{
    partial class CameraBinderControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraBinderControl));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbEndpoint = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.itemPickerUserControl = new VideoOS.Platform.UI.ItemPickerUserControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddToFS = new System.Windows.Forms.Button();
            this.lblCamera = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbEndpoint);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(674, 81);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LUNA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(334, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Note: Do not append the API version!";
            // 
            // tbEndpoint
            // 
            this.tbEndpoint.Location = new System.Drawing.Point(64, 21);
            this.tbEndpoint.Name = "tbEndpoint";
            this.tbEndpoint.Size = new System.Drawing.Size(243, 20);
            this.tbEndpoint.TabIndex = 5;
            this.tbEndpoint.Text = "http://127.0.0.1:5000/";
            this.tbEndpoint.TextChanged += new System.EventHandler(this.tbEndpoint_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Endpoint:";
            // 
            // itemPickerUserControl
            // 
            this.itemPickerUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.itemPickerUserControl.BackColor = System.Drawing.SystemColors.Control;
            this.itemPickerUserControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.itemPickerUserControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.itemPickerUserControl.CategoryUserSelectable = false;
            this.itemPickerUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemPickerUserControl.Font = new System.Drawing.Font("Arial", 9.25F);
            this.itemPickerUserControl.GroupTabVisible = true;
            this.itemPickerUserControl.ItemsSelected = ((System.Collections.Generic.List<VideoOS.Platform.Item>)(resources.GetObject("itemPickerUserControl.ItemsSelected")));
            this.itemPickerUserControl.KindSelected = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.itemPickerUserControl.KindUserSelectable = false;
            this.itemPickerUserControl.Location = new System.Drawing.Point(3, 16);
            this.itemPickerUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.itemPickerUserControl.Name = "itemPickerUserControl";
            this.itemPickerUserControl.ServerTabVisible = true;
            this.itemPickerUserControl.ShowDisabledItems = false;
            this.itemPickerUserControl.SingleSelect = true;
            this.itemPickerUserControl.Size = new System.Drawing.Size(668, 344);
            this.itemPickerUserControl.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.itemPickerUserControl);
            this.groupBox1.Location = new System.Drawing.Point(3, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(674, 363);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera";
            // 
            // btnAddToFS
            // 
            this.btnAddToFS.Location = new System.Drawing.Point(564, 521);
            this.btnAddToFS.Name = "btnAddToFS";
            this.btnAddToFS.Size = new System.Drawing.Size(113, 23);
            this.btnAddToFS.TabIndex = 20;
            this.btnAddToFS.Text = "Add to FaceStream";
            this.btnAddToFS.UseVisualStyleBackColor = true;
            this.btnAddToFS.Click += new System.EventHandler(this.btnAddToFS_Click);
            // 
            // lblCamera
            // 
            this.lblCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCamera.Location = new System.Drawing.Point(112, 492);
            this.lblCamera.Name = "lblCamera";
            this.lblCamera.ReadOnly = true;
            this.lblCamera.Size = new System.Drawing.Size(565, 20);
            this.lblCamera.TabIndex = 19;
            this.lblCamera.Text = "(no camera selected)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 495);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Selected camera:";
            // 
            // CameraBinderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddToFS);
            this.Controls.Add(this.lblCamera);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CameraBinderControl";
            this.Size = new System.Drawing.Size(682, 547);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbEndpoint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private VideoOS.Platform.UI.ItemPickerUserControl itemPickerUserControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddToFS;
        private System.Windows.Forms.TextBox lblCamera;
        private System.Windows.Forms.Label label1;
    }
}
