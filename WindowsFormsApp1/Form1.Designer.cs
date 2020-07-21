namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_CommandLine = new System.Windows.Forms.Label();
            this.richTextBox_CommandLine = new System.Windows.Forms.RichTextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.checkBoxX64 = new System.Windows.Forms.CheckBox();
            this.buttonTerminate = new System.Windows.Forms.Button();
            this.checkedListBoxTrace = new System.Windows.Forms.CheckedListBox();
            this.labelTrace = new System.Windows.Forms.Label();
            this.richTextBoxResult = new System.Windows.Forms.RichTextBox();
            this.labelResult = new System.Windows.Forms.Label();
            this.buttonCleanup = new System.Windows.Forms.Button();
            this.buttonOpenLog = new System.Windows.Forms.Button();
            this.buttonScan = new System.Windows.Forms.Button();
            this.buttonLoadLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_CommandLine
            // 
            this.label_CommandLine.AutoSize = true;
            this.label_CommandLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CommandLine.Location = new System.Drawing.Point(13, 36);
            this.label_CommandLine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_CommandLine.Name = "label_CommandLine";
            this.label_CommandLine.Size = new System.Drawing.Size(127, 20);
            this.label_CommandLine.TabIndex = 0;
            this.label_CommandLine.Text = "Command Line:";
            // 
            // richTextBox_CommandLine
            // 
            this.richTextBox_CommandLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_CommandLine.Location = new System.Drawing.Point(13, 69);
            this.richTextBox_CommandLine.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox_CommandLine.Name = "richTextBox_CommandLine";
            this.richTextBox_CommandLine.Size = new System.Drawing.Size(626, 104);
            this.richTextBox_CommandLine.TabIndex = 1;
            this.richTextBox_CommandLine.Text = "";
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.Location = new System.Drawing.Point(863, 227);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(116, 32);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // checkBoxX64
            // 
            this.checkBoxX64.AutoSize = true;
            this.checkBoxX64.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxX64.Location = new System.Drawing.Point(17, 181);
            this.checkBoxX64.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxX64.Name = "checkBoxX64";
            this.checkBoxX64.Size = new System.Drawing.Size(209, 24);
            this.checkBoxX64.TabIndex = 3;
            this.checkBoxX64.Text = "Trace as 64-bit process";
            this.checkBoxX64.UseVisualStyleBackColor = true;
            // 
            // buttonTerminate
            // 
            this.buttonTerminate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTerminate.Location = new System.Drawing.Point(739, 227);
            this.buttonTerminate.Margin = new System.Windows.Forms.Padding(4);
            this.buttonTerminate.Name = "buttonTerminate";
            this.buttonTerminate.Size = new System.Drawing.Size(116, 32);
            this.buttonTerminate.TabIndex = 4;
            this.buttonTerminate.Text = "Terminate";
            this.buttonTerminate.UseVisualStyleBackColor = true;
            this.buttonTerminate.Click += new System.EventHandler(this.buttonTerminate_Click);
            // 
            // checkedListBoxTrace
            // 
            this.checkedListBoxTrace.FormattingEnabled = true;
            this.checkedListBoxTrace.Items.AddRange(new object[] {
            "Win32 API functions",
            "Windows dynamic linking APIs",
            "HeapAlloc API",
            "Registry APIs",
            "Serial ports (com1 or com2)",
            "WinSock TCP APIs"});
            this.checkedListBoxTrace.Location = new System.Drawing.Point(646, 69);
            this.checkedListBoxTrace.Name = "checkedListBoxTrace";
            this.checkedListBoxTrace.Size = new System.Drawing.Size(333, 136);
            this.checkedListBoxTrace.TabIndex = 5;
            // 
            // labelTrace
            // 
            this.labelTrace.AutoSize = true;
            this.labelTrace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTrace.Location = new System.Drawing.Point(642, 36);
            this.labelTrace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTrace.Name = "labelTrace";
            this.labelTrace.Size = new System.Drawing.Size(147, 20);
            this.labelTrace.TabIndex = 6;
            this.labelTrace.Text = "Trace activities of:";
            // 
            // richTextBoxResult
            // 
            this.richTextBoxResult.BackColor = System.Drawing.Color.Black;
            this.richTextBoxResult.ForeColor = System.Drawing.Color.LawnGreen;
            this.richTextBoxResult.Location = new System.Drawing.Point(17, 286);
            this.richTextBoxResult.Name = "richTextBoxResult";
            this.richTextBoxResult.ReadOnly = true;
            this.richTextBoxResult.Size = new System.Drawing.Size(962, 252);
            this.richTextBoxResult.TabIndex = 7;
            this.richTextBoxResult.Text = "";
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(13, 249);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(62, 20);
            this.labelResult.TabIndex = 8;
            this.labelResult.Text = "Result:";
            // 
            // buttonCleanup
            // 
            this.buttonCleanup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCleanup.Location = new System.Drawing.Point(615, 227);
            this.buttonCleanup.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCleanup.Name = "buttonCleanup";
            this.buttonCleanup.Size = new System.Drawing.Size(116, 32);
            this.buttonCleanup.TabIndex = 9;
            this.buttonCleanup.Text = "Cleanup";
            this.buttonCleanup.UseVisualStyleBackColor = true;
            this.buttonCleanup.Click += new System.EventHandler(this.buttonCleanup_Click);
            // 
            // buttonOpenLog
            // 
            this.buttonOpenLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenLog.Location = new System.Drawing.Point(491, 227);
            this.buttonOpenLog.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOpenLog.Name = "buttonOpenLog";
            this.buttonOpenLog.Size = new System.Drawing.Size(116, 32);
            this.buttonOpenLog.TabIndex = 10;
            this.buttonOpenLog.Text = "Open log";
            this.buttonOpenLog.UseVisualStyleBackColor = true;
            this.buttonOpenLog.Click += new System.EventHandler(this.buttonOpenLog_Click);
            // 
            // buttonScan
            // 
            this.buttonScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonScan.Location = new System.Drawing.Point(243, 227);
            this.buttonScan.Margin = new System.Windows.Forms.Padding(4);
            this.buttonScan.Name = "buttonScan";
            this.buttonScan.Size = new System.Drawing.Size(116, 32);
            this.buttonScan.TabIndex = 11;
            this.buttonScan.Text = "Scan";
            this.buttonScan.UseVisualStyleBackColor = true;
            this.buttonScan.Click += new System.EventHandler(this.buttonScan_Click);
            // 
            // buttonLoadLog
            // 
            this.buttonLoadLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadLog.Location = new System.Drawing.Point(367, 227);
            this.buttonLoadLog.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLoadLog.Name = "buttonLoadLog";
            this.buttonLoadLog.Size = new System.Drawing.Size(116, 32);
            this.buttonLoadLog.TabIndex = 12;
            this.buttonLoadLog.Text = "Load log";
            this.buttonLoadLog.UseVisualStyleBackColor = true;
            this.buttonLoadLog.Click += new System.EventHandler(this.buttonLoadLog_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 562);
            this.Controls.Add(this.buttonLoadLog);
            this.Controls.Add(this.buttonScan);
            this.Controls.Add(this.buttonOpenLog);
            this.Controls.Add(this.buttonCleanup);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.richTextBoxResult);
            this.Controls.Add(this.labelTrace);
            this.Controls.Add(this.checkedListBoxTrace);
            this.Controls.Add(this.buttonTerminate);
            this.Controls.Add(this.checkBoxX64);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.richTextBox_CommandLine);
            this.Controls.Add(this.label_CommandLine);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Vision";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_CommandLine;
        private System.Windows.Forms.RichTextBox richTextBox_CommandLine;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.CheckBox checkBoxX64;
        private System.Windows.Forms.Button buttonTerminate;
        private System.Windows.Forms.CheckedListBox checkedListBoxTrace;
        private System.Windows.Forms.Label labelTrace;
        private System.Windows.Forms.RichTextBox richTextBoxResult;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Button buttonCleanup;
        private System.Windows.Forms.Button buttonOpenLog;
        private System.Windows.Forms.Button buttonScan;
        private System.Windows.Forms.Button buttonLoadLog;
    }
}

