using SpaceInvaders.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;

namespace SpaceInvaders
{
    public partial class Menu : Form
    {
        public Menu()
        {

            InitializeComponent();
        }
        
        private void Menu_Load(object sender, EventArgs e)
        {
            //SetupCustomFont();

            simpleButton.Click += simpleButtonClick;
            endlessButton.Click += endlessButtonClick;
        }

        private void OpenGame()
        {
            var frm = new GameForm();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void simpleButtonClick(object sender, EventArgs e)
        {
            OpenGame();
        }
        private void endlessButtonClick(object sender, EventArgs e)
        {
            OpenGame();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
