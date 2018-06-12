namespace MorningStart
{
    partial class frmAddEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddEdit));
            this.lstExes = new System.Windows.Forms.ListBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsAdd = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstExes
            // 
            this.lstExes.FormattingEnabled = true;
            this.lstExes.ItemHeight = 16;
            this.lstExes.Location = new System.Drawing.Point(24, 56);
            this.lstExes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstExes.Name = "lstExes";
            this.lstExes.Size = new System.Drawing.Size(811, 260);
            this.lstExes.TabIndex = 0;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(196, 334);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(168, 32);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(520, 336);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(168, 32);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAdd,
            this.tsDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(878, 30);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsAdd
            // 
            this.tsAdd.AutoSize = false;
            this.tsAdd.BackgroundImage = global::MorningStart.Properties.Resources.icons8_plus_filled_50;
            this.tsAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.tsAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsAdd.Image")));
            this.tsAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAdd.Name = "tsAdd";
            this.tsAdd.Size = new System.Drawing.Size(27, 27);
            this.tsAdd.Text = "Click to Add EXE";
            this.tsAdd.Click += new System.EventHandler(this.tsAdd_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.AutoSize = false;
            this.tsDelete.BackgroundImage = global::MorningStart.Properties.Resources.icons8_cancel_filled_50;
            this.tsDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.tsDelete.Image = global::MorningStart.Properties.Resources.icons8_cancel_filled_50;
            this.tsDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(27, 27);
            this.tsDelete.Text = "Click to Delete Record";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // frmAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 392);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.lstExes);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmAddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddEdit";
            this.Load += new System.EventHandler(this.frmAddEdit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstExes;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsAdd;
        private System.Windows.Forms.ToolStripButton tsDelete;
    }
}