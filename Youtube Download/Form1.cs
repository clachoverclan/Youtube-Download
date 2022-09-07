using Guna.UI2.WinForms;
using System;
using System.Diagnostics;
using System.IO;

namespace Youtube_Download
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DL.Startup();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text;
            if (url.StartsWith("https://www.youtube.com/watch?v="))
            {
                guna2Button2.FillColor = Color.Green;
                guna2HtmlLabel3.Text = "Loading..";
                DL.DownloadSingle(url);
                guna2HtmlLabel3.Text = "";
                guna2Button2.FillColor = Color.Transparent;
                return;
            }
            guna2Button2.FillColor = Color.Red;
            textBox1.PlaceholderText = "Please enter a valid url.";
            guna2Button2.FillColor = Color.Transparent;

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string url = textBox2.Text;
            if (url.StartsWith("https://www.youtube.com/watch?v=")) 
            {
                guna2Button3.FillColor = Color.Green;
                guna2HtmlLabel4.Text = "Loading.."; 
                DL.DownloadPlaylistAsync(url);
                guna2HtmlLabel4.Text = "";
                guna2Button3.FillColor = Color.Transparent;
                return;
            }
            guna2Button3.FillColor = Color.Red;
            textBox2.PlaceholderText = "Please enter a valid url.";
            guna2Button3.FillColor = Color.Transparent;


        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", @"c:\YoutubeDL");
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}