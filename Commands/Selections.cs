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

namespace ModelChange.Commands
{
    public partial class Selections : Form
    {
        public Selections()
        {
            InitializeComponent();
            ShowDialog();
        }
        private void settings_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //打开本地文件，进行选择
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.*)|*.*";
            ofd.RestoreDirectory = true;//保存当前文件的路径
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                //DirectoryInfo dir = Directory.GetParent(ofd.FileName);
                //textBox1.Text = dir.ToString() + "\\" + ofd.FileName.ToString();//用于窗体显示
                //textBox1.Text = dir.ToString() + "\\" + ofd.FileName.ToString();//用于窗体显示
                string str = ofd.FileName.ToString();
                textBox1.Text = str;//本身就可以直接付字符串

                //获取路径中的名字
                string path = str;
                string str1 = Path.GetFileNameWithoutExtension(path);
                //str1 += Path.GetFileNameWithoutExtension(path);
                Def.name = str1;


                /*非常重要
                string path = @str;
                int index = path.LastIndexOf(@"\");
                if(index >0)
                {
                    string name = path.Substring(index + 1, path.Length - index - 1);
                    Def.name = name;
                }*/
                Def.str = str;//用于获取族实体地址
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {           
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
