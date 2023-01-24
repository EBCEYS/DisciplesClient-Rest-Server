using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminAndAuthorClient.UserForms.InfoForm
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
            UpdateCurrentInfo();
        }

        private void UpdateCurrentInfo()
        {
            CurrentInfoTextBox.Text = Program.HttpSender.GetInfo() ?? "Error!";
        }

        private void GetInfoButton_Click(object sender, EventArgs e)
        {
            UpdateCurrentInfo();
        }

        private void UpdateInfoButton_Click(object sender, EventArgs e)
        {
            string text = UpdateInfoTextBox.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Write something plz!");
                return;
            }
            DialogResult dResult = MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dResult != DialogResult.Yes)
            {
                return;
            }
            if (!Program.HttpSender.PostInfo(text))
            {
                return;
            }
            MessageBox.Show("New info will be set!");

        }
    }
}
