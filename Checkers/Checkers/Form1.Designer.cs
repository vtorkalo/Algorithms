namespace Checkers
{
    partial class fmCheckersMain
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
            this.pnlField = new System.Windows.Forms.Panel();
            this.btnNewWhite = new System.Windows.Forms.Button();
            this.btnNewBlack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlField
            // 
            this.pnlField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlField.Location = new System.Drawing.Point(82, 48);
            this.pnlField.Name = "pnlField";
            this.pnlField.Size = new System.Drawing.Size(800, 800);
            this.pnlField.TabIndex = 0;
            this.pnlField.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlField_Paint);
            this.pnlField.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlField_MouseClick);
            // 
            // btnNewWhite
            // 
            this.btnNewWhite.AccessibleDescription = "White";
            this.btnNewWhite.Location = new System.Drawing.Point(916, 50);
            this.btnNewWhite.Name = "btnNewWhite";
            this.btnNewWhite.Size = new System.Drawing.Size(100, 45);
            this.btnNewWhite.TabIndex = 3;
            this.btnNewWhite.Text = "White";
            this.btnNewWhite.UseVisualStyleBackColor = true;
            this.btnNewWhite.Click += new System.EventHandler(this.btnNewWhite_Click);
            // 
            // btnNewBlack
            // 
            this.btnNewBlack.Location = new System.Drawing.Point(916, 113);
            this.btnNewBlack.Name = "btnNewBlack";
            this.btnNewBlack.Size = new System.Drawing.Size(100, 44);
            this.btnNewBlack.TabIndex = 4;
            this.btnNewBlack.Text = "Black";
            this.btnNewBlack.UseVisualStyleBackColor = true;
            this.btnNewBlack.Click += new System.EventHandler(this.btnNewBlack_Click);
            // 
            // fmCheckersMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 912);
            this.Controls.Add(this.btnNewBlack);
            this.Controls.Add(this.btnNewWhite);
            this.Controls.Add(this.pnlField);
            this.Name = "fmCheckersMain";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlField;
        private System.Windows.Forms.Button btnNewWhite;
        private System.Windows.Forms.Button btnNewBlack;
    }
}

