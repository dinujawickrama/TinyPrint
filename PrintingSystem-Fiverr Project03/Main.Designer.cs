namespace PrintingSystem_Fiverr_Project03
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.fileDrop = new System.Windows.Forms.ListBox();
            this.link = new System.Windows.Forms.LinkLabel();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drop Your File Here";
            // 
            // fileDrop
            // 
            this.fileDrop.AllowDrop = true;
            this.fileDrop.FormattingEnabled = true;
            this.fileDrop.Location = new System.Drawing.Point(15, 60);
            this.fileDrop.Name = "fileDrop";
            this.fileDrop.Size = new System.Drawing.Size(542, 329);
            this.fileDrop.TabIndex = 1;
            this.fileDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.fileDrop_DragDrop);
            this.fileDrop.DragOver += new System.Windows.Forms.DragEventHandler(this.fileDrop_DragOver);
            // 
            // link
            // 
            this.link.AutoSize = true;
            this.link.Location = new System.Drawing.Point(269, 456);
            this.link.Name = "link";
            this.link.Size = new System.Drawing.Size(291, 13);
            this.link.TabIndex = 2;
            this.link.TabStop = true;
            this.link.Text = "Copyright © 2017 | All Right Reserved | Mr.Source (PVT) Ltd";
            this.link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
            // 
            // btn_print
            // 
            this.btn_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print.Location = new System.Drawing.Point(429, 395);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(128, 40);
            this.btn_print.TabIndex = 3;
            this.btn_print.Text = "Print";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(295, 395);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(128, 40);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 491);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.link);
            this.Controls.Add(this.fileDrop);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "TinyPrint";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox fileDrop;
        private System.Windows.Forms.LinkLabel link;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_cancel;
    }
}

