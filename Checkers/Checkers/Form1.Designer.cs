namespace Checkers
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnSetBlack = new System.Windows.Forms.Button();
            this.btnSetBlackKing = new System.Windows.Forms.Button();
            this.btnSetWhite = new System.Windows.Forms.Button();
            this.btnSetWhiteKing = new System.Windows.Forms.Button();
            this.btnSetEmpty = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlField
            // 
            this.pnlField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlField.Location = new System.Drawing.Point(55, 31);
            this.pnlField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlField.Name = "pnlField";
            this.pnlField.Size = new System.Drawing.Size(534, 521);
            this.pnlField.TabIndex = 0;
            this.pnlField.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlField_Paint);
            this.pnlField.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlField_MouseClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1069, 11);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSetBlack
            // 
            this.btnSetBlack.Location = new System.Drawing.Point(1069, 123);
            this.btnSetBlack.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSetBlack.Name = "btnSetBlack";
            this.btnSetBlack.Size = new System.Drawing.Size(100, 29);
            this.btnSetBlack.TabIndex = 2;
            this.btnSetBlack.Text = "Set black";
            this.btnSetBlack.UseVisualStyleBackColor = true;
            this.btnSetBlack.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSetBlackKing
            // 
            this.btnSetBlackKing.Location = new System.Drawing.Point(1069, 156);
            this.btnSetBlackKing.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSetBlackKing.Name = "btnSetBlackKing";
            this.btnSetBlackKing.Size = new System.Drawing.Size(100, 26);
            this.btnSetBlackKing.TabIndex = 3;
            this.btnSetBlackKing.Text = "Set black king";
            this.btnSetBlackKing.UseVisualStyleBackColor = true;
            this.btnSetBlackKing.Click += new System.EventHandler(this.btnSetBlackKing_Click);
            // 
            // btnSetWhite
            // 
            this.btnSetWhite.Location = new System.Drawing.Point(1069, 187);
            this.btnSetWhite.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSetWhite.Name = "btnSetWhite";
            this.btnSetWhite.Size = new System.Drawing.Size(100, 25);
            this.btnSetWhite.TabIndex = 4;
            this.btnSetWhite.Text = "Set white";
            this.btnSetWhite.UseVisualStyleBackColor = true;
            this.btnSetWhite.Click += new System.EventHandler(this.btnSetWhite_Click);
            // 
            // btnSetWhiteKing
            // 
            this.btnSetWhiteKing.Location = new System.Drawing.Point(1069, 216);
            this.btnSetWhiteKing.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSetWhiteKing.Name = "btnSetWhiteKing";
            this.btnSetWhiteKing.Size = new System.Drawing.Size(100, 24);
            this.btnSetWhiteKing.TabIndex = 5;
            this.btnSetWhiteKing.Text = "Set white king";
            this.btnSetWhiteKing.UseVisualStyleBackColor = true;
            this.btnSetWhiteKing.Click += new System.EventHandler(this.btnSetWhiteKing_Click);
            // 
            // btnSetEmpty
            // 
            this.btnSetEmpty.Location = new System.Drawing.Point(1069, 95);
            this.btnSetEmpty.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSetEmpty.Name = "btnSetEmpty";
            this.btnSetEmpty.Size = new System.Drawing.Size(100, 24);
            this.btnSetEmpty.TabIndex = 6;
            this.btnSetEmpty.Text = "Set empty";
            this.btnSetEmpty.UseVisualStyleBackColor = true;
            this.btnSetEmpty.Click += new System.EventHandler(this.btnSetEmpty_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1077, 312);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 29);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1069, 44);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(67, 29);
            this.button3.TabIndex = 8;
            this.button3.Text = "Clear";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1069, 361);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 29);
            this.button4.TabIndex = 9;
            this.button4.Text = "GetPaths";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1197, 688);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSetEmpty);
            this.Controls.Add(this.btnSetWhiteKing);
            this.Controls.Add(this.btnSetWhite);
            this.Controls.Add(this.btnSetBlackKing);
            this.Controls.Add(this.btnSetBlack);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnlField);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlField;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSetBlack;
        private System.Windows.Forms.Button btnSetBlackKing;
        private System.Windows.Forms.Button btnSetWhite;
        private System.Windows.Forms.Button btnSetWhiteKing;
        private System.Windows.Forms.Button btnSetEmpty;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

