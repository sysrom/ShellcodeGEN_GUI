using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace EXE2SHELLCODE
{
    /*
    ____           _______  _______ ____  ____  __  ___
   / __ )__  __   / ___/\ \/ / ___// __ \/ __ \/  |/  /
  / __  / / / /   \__ \  \  /\__ \/ /_/ / / / / /|_/ / 
 / /_/ / /_/ /   ___/ /  / /___/ / _, _/ /_/ / /  / /  
/_____/\__, /   /____/  /_//____/_/ |_|\____/_/  /_/   
      /____/                                           
    */
    public partial class Form1 : Form
    {
        public String needcovPath;
        public String OutputPath;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog nt = new OpenFileDialog()) {
                nt.Title = "选择要转换的文件";
                nt.Multiselect = false;
                if (nt.ShowDialog() == DialogResult.OK) {
                    needcovPath = nt.FileName;
                    textBox1.Text = nt.FileName;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog op = new SaveFileDialog())
            {
                op.Title = "输出到的文件";
                op.Filter = ".bin (*.bin)|*.bin";
                op.FileName = "Client";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    OutputPath = op.FileName;
                    textBox2.Text = op.FileName;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (needcovPath == null || OutputPath == null|| needcovPath ==""||OutputPath=="") {
                MessageBox.Show("傻逼,请选择转换的文件或者转换到的文件");
                return;
            }
            if (radioButton1.Checked) {
                // MessageBox.Show("Donut方式");
                string Donutpath = Path.Combine(Application.StartupPath, @"donut.exe");
                if (!File.Exists(Donutpath))
                {
                    File.WriteAllBytes(Donutpath, Properties.Resources.donut);
                }
                
                Process Process = new Process();
                Process.StartInfo.FileName = Donutpath;
                Process.StartInfo.CreateNoWindow = true;
                Process.StartInfo.Arguments = "-f \"" + needcovPath + "\" -o \"" + OutputPath + "\"";
                Process.Start();
                Process.WaitForExit();
                Process.Close();
                File.Delete(Donutpath);
                MessageBox.Show("生成成功!    方式:Donut\n"+"被转换的文件:"+needcovPath+"\n到文件:"+OutputPath);

            } else if (radioButton2.Checked) {
                // MessageBox.Show("pe2shc方式");
                string pe2shcpath = Path.Combine(Application.StartupPath, @"pe2shc.exe");
                if (!File.Exists(pe2shcpath))
                {
                    File.WriteAllBytes(pe2shcpath, Properties.Resources.pe2shc);
                }

                Process Process = new Process();
                Process.StartInfo.FileName = pe2shcpath;
                Process.StartInfo.CreateNoWindow = true;
                Process.StartInfo.Arguments = " \"" + needcovPath + "\" \"" + OutputPath + "\"";
                Process.Start();
                Process.WaitForExit();
                Process.Close();
                File.Delete(pe2shcpath);
                MessageBox.Show("生成成功!    方式:pe2shc\n" + "被转换的文件:" + needcovPath + "\n到文件:" + OutputPath);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("恭喜你是个傻逼");
        }
    }
}
