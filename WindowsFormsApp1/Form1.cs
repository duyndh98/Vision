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
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();

            InitControls();
        }

        private DetoursWrapper _tracer = null;
        private Scanner _scanner = null;

        private Thread _scannerThread = null;

        public void AppendText(ref RichTextBox box, string text, Color color)
        {
            text += "\n";
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        private void InitControls()
        {
            for (int i = 0; i < this.checkedListBoxTrace.Items.Count; i++)
                this.checkedListBoxTrace.SetItemChecked(i, true);

            this.buttonStart.Enabled = true;
            this.buttonTerminate.Enabled = false;

            _tracer = new DetoursWrapper();
            _scanner = new Scanner();

            AppendText(ref this.richTextBoxResult, "Ready", Color.White);
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

                _tracer.Init(this.checkBoxX64.Checked, checkedStatus, this.richTextBox_CommandLine.Text);

                var tracerThread = new Thread(new ThreadStart(_tracer.Trace));
                tracerThread.Start();

                AppendText(ref this.richTextBoxResult, "Trace started", Color.White);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.buttonStart.Enabled = false;
            this.buttonTerminate.Enabled = true;
        }

        private void buttonTerminate_Click(object sender, EventArgs e)
        {
            try
            {                
                var killerThread = new Thread(new ThreadStart(_tracer.Terminate));
                killerThread.Start();

                AppendText(ref this.richTextBoxResult, "Trace terminated", Color.White);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.buttonStart.Enabled = true;
            this.buttonTerminate.Enabled = false;
        }

        private void buttonCleanup_Click(object sender, EventArgs e)
        {
            try
            {
                var cleanerThread = new Thread(new ThreadStart(_tracer.Cleanup));
                cleanerThread.Start();

                AppendText(ref this.richTextBoxResult, "Cleaned up", Color.White);

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonOpenLog_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("notepad.exe", _tracer.GetLogPath());
                AppendText(ref this.richTextBoxResult, "Log is opened", Color.White);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateScannerResult()
        {
            _scannerThread.Join();
            var signatures = _scanner.GetScanResult();
            this.richTextBoxResult.BeginInvoke((MethodInvoker)delegate ()
            {
                if (signatures.Count == 0)
                    AppendText(ref this.richTextBoxResult, "Not detect", Color.LightGreen);
                else
                {
                    foreach (var sign in signatures)
                        AppendText(ref this.richTextBoxResult, "Detected: " + sign.ToString(), Color.Red);
                }
            });
        }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            try
            {
                AppendText(ref this.richTextBoxResult, "Scanning...", Color.White);

                _scanner.SetLogPath(_tracer.GetLogPath());
                _scannerThread = new Thread(new ThreadStart(_scanner.Scan));
                _scannerThread.Start();

                var updateScannerResultThread = new Thread(new ThreadStart(UpdateScannerResult));
                updateScannerResultThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonLoadLog_Click(object sender, EventArgs e)
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog
                {
                    CheckFileExists = true,
                    CheckPathExists = true,
                    FilterIndex = 2,
                    RestoreDirectory = true,
                    ReadOnlyChecked = true,
                    ShowReadOnly = true
                })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        _tracer.SetLogPath(openFileDialog.FileName);
                    }
                }

                AppendText(ref this.richTextBoxResult, "Log is loaded", Color.White);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
