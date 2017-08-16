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
    public partial class ChangesInformationForm : Form
    {
        public ChangesInformationForm()
        {
            InitializeComponent();
        }
        public ChangesInformationForm(DataTable dataBuffer)
            : this()
        {
            changesdataGridView.DataSource = dataBuffer;
            changesdataGridView.AutoGenerateColumns = false;
        }
        private void ChangesInfoForm_Shown(object sender, EventArgs e)
        {
            // set window's display location
            int left = Screen.PrimaryScreen.WorkingArea.Right - this.Width - 5;
            int top = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            Point windowLocation = new Point(left, top);
            this.Location = windowLocation;
        }
        private void changesdataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            changesdataGridView.CurrentCell = changesdataGridView.Rows[changesdataGridView.Rows.Count - 1].Cells[0];
        }

    }
}
