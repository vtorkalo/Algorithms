﻿namespace Checkers
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
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1094, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 45);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSetBlack
            // 
            this.btnSetBlack.Location = new System.Drawing.Point(1094, 252);
            this.btnSetBlack.Name = "btnSetBlack";
            this.btnSetBlack.Size = new System.Drawing.Size(150, 45);
            this.btnSetBlack.TabIndex = 2;
            this.btnSetBlack.Text = "Set black";
            this.btnSetBlack.UseVisualStyleBackColor = true;
            this.btnSetBlack.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSetBlackKing
            // 
            this.btnSetBlackKing.Location = new System.Drawing.Point(1094, 303);
            this.btnSetBlackKing.Name = "btnSetBlackKing";
            this.btnSetBlackKing.Size = new System.Drawing.Size(150, 40);
            this.btnSetBlackKing.TabIndex = 3;
            this.btnSetBlackKing.Text = "Set black king";
            this.btnSetBlackKing.UseVisualStyleBackColor = true;
            this.btnSetBlackKing.Click += new System.EventHandler(this.btnSetBlackKing_Click);
            // 
            // btnSetWhite
            // 
            this.btnSetWhite.Location = new System.Drawing.Point(1094, 351);
            this.btnSetWhite.Name = "btnSetWhite";
            this.btnSetWhite.Size = new System.Drawing.Size(150, 38);
            this.btnSetWhite.TabIndex = 4;
            this.btnSetWhite.Text = "Set white";
            this.btnSetWhite.UseVisualStyleBackColor = true;
            this.btnSetWhite.Click += new System.EventHandler(this.btnSetWhite_Click);
            // 
            // btnSetWhiteKing
            // 
            this.btnSetWhiteKing.Location = new System.Drawing.Point(1094, 395);
            this.btnSetWhiteKing.Name = "btnSetWhiteKing";
            this.btnSetWhiteKing.Size = new System.Drawing.Size(150, 37);
            this.btnSetWhiteKing.TabIndex = 5;
            this.btnSetWhiteKing.Text = "Set white king";
            this.btnSetWhiteKing.UseVisualStyleBackColor = true;
            this.btnSetWhiteKing.Click += new System.EventHandler(this.btnSetWhiteKing_Click);
            // 
            // btnSetEmpty
            // 
            this.btnSetEmpty.Location = new System.Drawing.Point(1094, 209);
            this.btnSetEmpty.Name = "btnSetEmpty";
            this.btnSetEmpty.Size = new System.Drawing.Size(150, 37);
            this.btnSetEmpty.TabIndex = 6;
            this.btnSetEmpty.Text = "Set empty";
            this.btnSetEmpty.UseVisualStyleBackColor = true;
            this.btnSetEmpty.Click += new System.EventHandler(this.btnSetEmpty_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1094, 534);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 45);
            this.button2.TabIndex = 7;
            this.button2.Text = "Calculate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1094, 618);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(140, 45);
            this.button4.TabIndex = 9;
            this.button4.Text = "GetPaths";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1080, 776);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(114, 36);
            this.button5.TabIndex = 10;
            this.button5.Text = "PathToString";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1080, 824);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(114, 36);
            this.button6.TabIndex = 11;
            this.button6.Text = "FieldToString";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 951);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSetEmpty);
            this.Controls.Add(this.btnSetWhiteKing);
            this.Controls.Add(this.btnSetWhite);
            this.Controls.Add(this.btnSetBlackKing);
            this.Controls.Add(this.btnSetBlack);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnlField);
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
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}

