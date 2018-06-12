namespace MorningStart
{
    partial class frmMorningStart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMorningStart));
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmdCloseAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(80, 48);
            this.cmdStart.Margin = new System.Windows.Forms.Padding(4);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(259, 39);
            this.cmdStart.TabIndex = 0;
            this.cmdStart.Text = "Start Morning Applications";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(80, 144);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(259, 39);
            this.cmdAdd.TabIndex = 1;
            this.cmdAdd.Text = "Add/Edit Exes To Start";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmdCloseAll
            // 
            this.cmdCloseAll.Location = new System.Drawing.Point(80, 96);
            this.cmdCloseAll.Margin = new System.Windows.Forms.Padding(4);
            this.cmdCloseAll.Name = "cmdCloseAll";
            this.cmdCloseAll.Size = new System.Drawing.Size(259, 39);
            this.cmdCloseAll.TabIndex = 2;
            this.cmdCloseAll.Text = "Close All Applications";
            this.cmdCloseAll.UseVisualStyleBackColor = true;
            this.cmdCloseAll.Click += new System.EventHandler(this.cmdCloseAll_Click);
            // 
            // frmMorningStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 208);
            this.Controls.Add(this.cmdCloseAll);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.cmdStart);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMorningStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Morning Startup";
            this.Load += new System.EventHandler(this.frmMorningStart_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button cmdCloseAll;
    }
}

