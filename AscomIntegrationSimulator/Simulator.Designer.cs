namespace AscomIntegrationSimulator
{
    partial class Simulator
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
            this.components = new System.ComponentModel.Container();
            this._txtScript = new System.Windows.Forms.TextBox();
            this._btnScript = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._txtLog = new System.Windows.Forms.TextBox();
            this._btnLog = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._btnRun = new System.Windows.Forms.Button();
            this._lblRunTime = new System.Windows.Forms.Label();
            this._lblLevel = new System.Windows.Forms.Label();
            this._txtTime = new System.Windows.Forms.TextBox();
            this._cmbLogLevel = new System.Windows.Forms.ComboBox();
            this._scriptThread = new System.ComponentModel.BackgroundWorker();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _txtScript
            // 
            this._txtScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtScript.Location = new System.Drawing.Point(176, 26);
            this._txtScript.Margin = new System.Windows.Forms.Padding(6);
            this._txtScript.Name = "_txtScript";
            this._txtScript.Size = new System.Drawing.Size(744, 38);
            this._txtScript.TabIndex = 0;
            // 
            // _btnScript
            // 
            this._btnScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnScript.Location = new System.Drawing.Point(936, 23);
            this._btnScript.Margin = new System.Windows.Forms.Padding(6);
            this._btnScript.Name = "_btnScript";
            this._btnScript.Size = new System.Drawing.Size(150, 44);
            this._btnScript.TabIndex = 1;
            this._btnScript.Text = "Browse...";
            this._btnScript.UseVisualStyleBackColor = true;
            this._btnScript.Click += new System.EventHandler(this.ScriptClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Test Script";
            // 
            // _txtLog
            // 
            this._txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtLog.Location = new System.Drawing.Point(176, 82);
            this._txtLog.Margin = new System.Windows.Forms.Padding(6);
            this._txtLog.Name = "_txtLog";
            this._txtLog.Size = new System.Drawing.Size(744, 38);
            this._txtLog.TabIndex = 0;
            // 
            // _btnLog
            // 
            this._btnLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnLog.Location = new System.Drawing.Point(936, 79);
            this._btnLog.Margin = new System.Windows.Forms.Padding(6);
            this._btnLog.Name = "_btnLog";
            this._btnLog.Size = new System.Drawing.Size(150, 44);
            this._btnLog.TabIndex = 1;
            this._btnLog.Text = "Browse...";
            this._btnLog.UseVisualStyleBackColor = true;
            this._btnLog.Click += new System.EventHandler(this.LogClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 88);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Log File";
            // 
            // _btnRun
            // 
            this._btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnRun.Location = new System.Drawing.Point(936, 135);
            this._btnRun.Margin = new System.Windows.Forms.Padding(6);
            this._btnRun.Name = "_btnRun";
            this._btnRun.Size = new System.Drawing.Size(150, 44);
            this._btnRun.TabIndex = 1;
            this._btnRun.Text = "Run Script";
            this._btnRun.UseVisualStyleBackColor = true;
            this._btnRun.Click += new System.EventHandler(this.RunClick);
            // 
            // _lblRunTime
            // 
            this._lblRunTime.AutoSize = true;
            this._lblRunTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblRunTime.Location = new System.Drawing.Point(411, 144);
            this._lblRunTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._lblRunTime.Name = "_lblRunTime";
            this._lblRunTime.Size = new System.Drawing.Size(147, 26);
            this._lblRunTime.TabIndex = 2;
            this._lblRunTime.Text = "Running Time";
            // 
            // _lblLevel
            // 
            this._lblLevel.AutoSize = true;
            this._lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblLevel.Location = new System.Drawing.Point(19, 144);
            this._lblLevel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._lblLevel.Name = "_lblLevel";
            this._lblLevel.Size = new System.Drawing.Size(147, 26);
            this._lblLevel.TabIndex = 2;
            this._lblLevel.Text = "Logging Level";
            // 
            // _txtTime
            // 
            this._txtTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtTime.Location = new System.Drawing.Point(570, 138);
            this._txtTime.Margin = new System.Windows.Forms.Padding(6);
            this._txtTime.Name = "_txtTime";
            this._txtTime.ReadOnly = true;
            this._txtTime.Size = new System.Drawing.Size(350, 38);
            this._txtTime.TabIndex = 0;
            // 
            // _cmbLogLevel
            // 
            this._cmbLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbLogLevel.FormattingEnabled = true;
            this._cmbLogLevel.Location = new System.Drawing.Point(176, 141);
            this._cmbLogLevel.Name = "_cmbLogLevel";
            this._cmbLogLevel.Size = new System.Drawing.Size(225, 33);
            this._cmbLogLevel.TabIndex = 3;
            // 
            // _scriptThread
            // 
            this._scriptThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.RunScript);
            this._scriptThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ScriptFinished);
            // 
            // _timer
            // 
            this._timer.Enabled = true;
            this._timer.Interval = 1000;
            this._timer.Tick += new System.EventHandler(this.UpdateTimer);
            // 
            // Simulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 198);
            this.Controls.Add(this._cmbLogLevel);
            this.Controls.Add(this._lblLevel);
            this.Controls.Add(this._lblRunTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._btnRun);
            this.Controls.Add(this._btnLog);
            this.Controls.Add(this._txtTime);
            this.Controls.Add(this._txtLog);
            this.Controls.Add(this._btnScript);
            this.Controls.Add(this._txtScript);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Simulator";
            this.Text = "SCUH - Messaging System (Integration Simulator)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _txtScript;
        private System.Windows.Forms.Button _btnScript;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _txtLog;
        private System.Windows.Forms.Button _btnLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _btnRun;
        private System.Windows.Forms.Label _lblRunTime;
        private System.Windows.Forms.Label _lblLevel;
        private System.Windows.Forms.TextBox _txtTime;
        private System.Windows.Forms.ComboBox _cmbLogLevel;
        private System.ComponentModel.BackgroundWorker _scriptThread;
        private System.Windows.Forms.Timer _timer;
    }
}

