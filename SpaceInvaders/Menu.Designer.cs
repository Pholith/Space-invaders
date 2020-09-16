using System.Windows.Forms;

namespace SpaceInvaders
{
    partial class Menu
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
            this.simpleButton = new System.Windows.Forms.Button();
            this.endlessButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // simpleButton
            // 
            this.simpleButton.BackColor = System.Drawing.Color.Transparent;
            this.simpleButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.simpleButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.simpleButton.FlatAppearance.BorderSize = 3;
            this.simpleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.simpleButton.Font = new System.Drawing.Font("Mongolian Baiti", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton.Location = new System.Drawing.Point(97, 68);
            this.simpleButton.Name = "simpleButton";
            this.simpleButton.Size = new System.Drawing.Size(198, 47);
            this.simpleButton.TabIndex = 0;
            this.simpleButton.Text = "simple game";
            this.simpleButton.UseCompatibleTextRendering = true;
            this.simpleButton.UseVisualStyleBackColor = false;
            this.simpleButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // endlessButton
            // 
            this.endlessButton.BackColor = System.Drawing.Color.Transparent;
            this.endlessButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.endlessButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.endlessButton.FlatAppearance.BorderSize = 3;
            this.endlessButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.endlessButton.Font = new System.Drawing.Font("Mongolian Baiti", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endlessButton.Location = new System.Drawing.Point(97, 155);
            this.endlessButton.Name = "endlessButton";
            this.endlessButton.Size = new System.Drawing.Size(198, 47);
            this.endlessButton.TabIndex = 1;
            this.endlessButton.Text = "endless game";
            this.endlessButton.UseCompatibleTextRendering = true;
            this.endlessButton.UseVisualStyleBackColor = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(392, 301);
            this.Controls.Add(this.simpleButton);
            this.Controls.Add(this.endlessButton);
            this.Name = "Menu";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button simpleButton;
        private Button endlessButton;
    }
}