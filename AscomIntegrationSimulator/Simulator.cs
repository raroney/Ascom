using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using AscomTestHarness;

namespace AscomIntegrationSimulator
{
    public partial class Simulator : Form
    {
        delegate void SetTextCallback(string text);

        private TestHarness _harness = TestHarness.Instance;

        //private System.Threading.Timer _timer;
        private DateTime _started;

        public Simulator()
        {
            InitializeComponent();

            _txtScript.Text = Properties.Settings.Default.FilePath;
            _txtLog.Text = Properties.Settings.Default.LogPath;
            _cmbLogLevel.DataSource = Enum.GetValues(typeof(DebugLevel));
            
            //_harness.PropertyChanged += HarnessPropertyChanged;
        }

        //private void HarnessPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "Running")
        //    {
        //        if (_harness.Running)
        //        {
        //            _btnRun.Enabled = false;
        //            _started = DateTime.Now;
        //            //_timer = new System.Threading.Timer(new TimerCallback(SetText));
        //            //_timer.Change(1000, 1000);
        //        }
        //        else
        //        {
        //            //_timer.Dispose();
        //            _txtTime.Text = DateTime.Now.Subtract(_started).ToString(@"hh\:mm\:ss\.ff");
        //            _btnRun.Enabled = true;
        //        }

        //        this.Refresh();
        //    }
        //}

        //private void UpdateTimer(object sender)
        //{
        //    if (InvokeRequired)
        //    {
        //        BeginInvoke(new Action(() => _txtTime.Text = DateTime.Now.Subtract(_started).ToString(@"hh\:mm\:ss")), null);
        //    }
        //}

        private void SetText(string text)
        {
            if (_txtTime.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                Invoke(d, new object[] { text });
            }
            else
            {
                _txtTime.Text = text;
            }
        }

        private void ScriptClick(object sender, EventArgs e)
        {
            _txtScript.Text = GetFileName(Path.GetDirectoryName(_txtScript.Text), "xml files (*.xml)|*.xml|All files (*.*)|*.*");
        }

        private void LogClick(object sender, EventArgs e)
        {
            _txtLog.Text = GetFileName(Path.GetDirectoryName(_txtLog.Text), "txt files (*.txt)|*.txt|All files (*.*)|*.*");
        }

        private void RunClick(object sender, EventArgs e)
        {
            Properties.Settings.Default.FilePath = _txtScript.Text;
            Properties.Settings.Default.LogPath = _txtLog.Text;
            Properties.Settings.Default.Save();

            _btnRun.Enabled = false;
            _started = DateTime.Now;
            _harness.SetupLog((DebugLevel)_cmbLogLevel.SelectedValue, _txtLog.Text);
            _harness.LoadXml(_txtScript.Text);

            _scriptThread.RunWorkerAsync();
        }

        private string GetFileName(string initDir, string filter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string fileName = "";

            dialog.InitialDirectory = initDir;
            dialog.Filter = filter;
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName;
            }

            return fileName;
        }

        private void RunScript(object sender, DoWorkEventArgs e)
        {
            _harness.RunScript();
        }

        private void ScriptFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            _txtTime.Text = DateTime.Now.Subtract(_started).ToString(@"hh\:mm\:ss\.ff");
            _btnRun.Enabled = true;
        }

        private void UpdateTimer(object sender, EventArgs e)
        {
            if (_txtTime.InvokeRequired)
            {
                BeginInvoke(new Action(() => _txtTime.Text = DateTime.Now.Subtract(_started).ToString(@"hh\:mm\:ss")), null);
            }
        }
    }
}