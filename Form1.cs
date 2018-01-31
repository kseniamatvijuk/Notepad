﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Form1 : Form
    {
        string path;
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void abolutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmAbout frm = new frmAbout())
            {
                frm.ShowDialog();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            path = string.Empty;
            textBox.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text Document|*.txt", ValidateNames = true, Multiselect = false })
                {
                     if (ofd.ShowDialog() == DialogResult.OK)
                     {
                        try
                        { 
                            using (StreamReader sr = new StreamReader(ofd.FileName))
                            {
                                path = ofd.FileName;
                                Task<string> text = sr.ReadToEndAsync();
                                textBox.Text = text.Result;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                     }
                  }
            

        }

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(path))
            {
                
                    using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Document|*.txt", ValidateNames = true })
                    {
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                        try
                        {
                            path = sfd.FileName;
                            using (StreamWriter sw = new StreamWriter(sfd.FileName))
                            {
                                await sw.WriteLineAsync(textBox.Text);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message,"Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    }
                
            }
            else
            {
                using(StreamWriter sw = new StreamWriter(path))
                {
                    await sw.WriteLineAsync(textBox.Text);
                }
            }
        }

        private async void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Document|*.txt", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            await sw.WriteLineAsync(textBox.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
