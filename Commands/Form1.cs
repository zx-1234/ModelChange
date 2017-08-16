using Revit.SDK.Samples.ParameterUtils.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelChange.Commands
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string[] str = new string[3];
        ListView propertyListView = Def.propertyListView;
        Autodesk.Revit.DB.ParameterSet parameters = Def.parameters;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = str[0];
            label5.Text = str[1];
            textBox3.Text = str[2];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //取消修正，直接关闭，等效没有操作
            //textBox1.Text = str[0];
            //textBox2.Text = str[1];
            //textBox3.Text = str[2];
            Form1 frm = Def.frm;
            frm.Hide();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //保存修正，参数传递，返回上个窗体
            int a = Def.a;
            //获取数据进行赋值操作
            //Form1 frm = Def.frm;
            
            //propertyListView.Items[a].SubItems[0].Text = textBox1.Text;
            propertyListView.Items[a].SubItems[1].Text = label2.Text;
            propertyListView.Items[a].SubItems[2].Text = textBox3.Text;                     
            propertyListView.Refresh();

            /*
            foreach (Autodesk.Revit.DB.Parameter param in parameters)
            {
               if (param == null) continue;  
               else
               {
                    if (param.Definition.Name == label4.Text)
                    {
                        //param.Set. =
                        param.SetValueString(textBox3.Text);//属性修改
                    }
               }
               
            }*/
            //propertyListView.
            //Form propertiesForm = new Form();
            //propertiesForm = Def.propertiesForm;
            //propertiesForm = Def.propertyListView
            //propertiesForm.Show();
            //在关闭选项卡将变化的参数返回给实体
            this.Close();
            //希望直接进入上个选项卡，点击确定退出程序。同时将程序作用于实体。
            //关闭修正选项卡，返回参数部分，如何点击确定保存所作修改是关键部分。
            //关闭这个选项卡，同时进入操作上个选项卡。
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }
    }
}
