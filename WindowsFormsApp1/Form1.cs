using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            InitControls();
        }

        private Thread _tracerThread = null;
        private Thread _killerThread = null;
        private DetoursWrapper _detoursWrapper = null;

        private void InitControls()
        {
            for (int i = 0; i < this.checkedListBoxTrace.Items.Count; i++)
                this.checkedListBoxTrace.SetItemChecked(i, true);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.richTextBox_CommandLine.Text))
                    throw new Exception("Command Line is empty");

                var checkedStatus = new bool[this.checkedListBoxTrace.Items.Count];
                for (int iItem = 0; iItem < this.checkedListBoxTrace.Items.Count; iItem++)
                {
                    if (this.checkedListBoxTrace.CheckedItems.Contains(this.checkedListBoxTrace.Items[iItem]))
                        checkedStatus[iItem] = true;
                    else
                        checkedStatus[iItem] = false;
                }

                _detoursWrapper = new DetoursWrapper(this.checkBoxX64.Checked, checkedStatus, this.richTextBox_CommandLine.Text);

                _tracerThread = new Thread(new ThreadStart(_detoursWrapper.Trace));
                _tracerThread.Start();
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonTerminate_Click(object sender, EventArgs e)
        {
            try
            {
                _tracerThread.Abort();

                _killerThread = new Thread(new ThreadStart(_detoursWrapper.Terminate));
                _killerThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
