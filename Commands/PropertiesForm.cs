using Autodesk.Revit.UI;
using ModelChange;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Revit.SDK.Samples.ParameterUtils.CS
{
    public partial class PropertiesForm : Form
    {        
        private PropertiesForm()
        {
            InitializeComponent();
        }      
        public PropertiesForm(string[] information)
            : this()
        {           
            propertyListView.Columns.Add("Name");
            propertyListView.Columns.Add("Type");
            propertyListView.Columns.Add("Value");                     
            foreach (string row in information)//ÿ�в���
            {
                if (row == null) continue;
                ListViewItem lvi = new ListViewItem(row.Split('\t'));                
                propertyListView.Items.Add(lvi);              
            }          
            propertyListView.GridLines = true;//��ʾ������                             
            propertyListView.Sorting = SortOrder.Ascending;//�����������        
            propertyListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);          
            int span = 40;
            foreach (ColumnHeader ch in propertyListView.Columns)
            {
                ch.Width += span;
            }           
            propertyListView.Columns[propertyListView.Columns.Count - 1].Width = -2;
            ModelChange.Def.propertyListView = propertyListView;
        }

        private void propertyListView_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Button_Click(object sender, EventArgs e)//����
        {                          
            ModelChange.Commands.Form1 frm = new ModelChange.Commands.Form1();
            if (propertyListView.FocusedItem == null)
            {                
                System.Diagnostics.Process.GetCurrentProcess().Kill();//ǿ���˳�����              
            }
            else
            {
                int a = propertyListView.FocusedItem.Index;//��ȡ��ǰѡ��������
                Def.a = a;
                for (int i = 0; i < 3; i++)
                {
                  frm.str[i] = propertyListView.Items[a].SubItems[i].Text;
                }
                //Hide();
                //Form propertiesForm = Def.propertiesForm;
                Def.frm = frm;              
                frm.ShowDialog();                                
                //��α�����������
            } 
                      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)//��һ������
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}