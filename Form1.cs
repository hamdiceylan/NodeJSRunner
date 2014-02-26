using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NodeJSRunner
{
    public partial class Form1 : Form
    {
        string path = @"C:\Program Files\nodejs\nodevars.bat";

        public Form1()
        {
            InitializeComponent();
        }

        private void getJSFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = "";
            openFileDialog1.ShowDialog();
            string[] lines = File.ReadAllLines(openFileDialog1.FileName);
            foreach (string line in lines)
            {
                if (line.Contains("listen"))
                {
                    var regex = Regex.Match(line, @"\d+");
                    if (regex.ToString() != "")
                    {
                        result = regex.ToString();
                    }
                }
            }

            string jsfile = openFileDialog1.FileName;
            string query = "start /b /min node ";
            string cmd = "start /b /min node " + jsfile;
            string[] sutunlar = File.ReadAllLines(path);
            foreach (string item in sutunlar)
            {
                if (item.StartsWith(query))
                {
                    break;
                }

                else
                {
                    StreamWriter sw = new StreamWriter(path, true);
                    sw.WriteLine(Environment.NewLine);
                    sw.WriteLine(cmd);
                    sw.Close();
                    sw.Dispose();
                    break;
                }
            }

            string fullPath = @"cmd.exe";
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = fullPath;
            psi.WorkingDirectory = @"C:\Program Files\nodejs";
            psi.Arguments = "/k nodevars.bat";
            Process.Start(psi);
            string urlpath = "http://localhost:" + result + "/";
            Process.Start(urlpath);           
        }

        //Miguel's code from StackOverFlow: http://stackoverflow.com/questions/4264117/c-sharp-how-to-delete-last-line-in-a-text-file
        public void DeleteLastLine(string filepath)
        {
            List<string> lines = File.ReadAllLines(filepath).ToList();
            File.WriteAllLines(filepath, lines.GetRange(0, lines.Count - 1));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeleteLastLine(path);
        }
    }
}