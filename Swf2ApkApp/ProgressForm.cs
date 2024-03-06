using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Swf2ApkApp
{
    public partial class ProgressForm : Form
    {
        private bool _completed = false;
        private CancellationTokenSource _source = new CancellationTokenSource();

        public CancellationToken CancellationToken => _source.Token;

        public ProgressForm()
        {
            InitializeComponent();
        }

        public void AppendOutput(string what)
        {
            Invoke(() =>
            {
                richTextBox_Status.SelectionStart = richTextBox_Status.Text.Length;
                richTextBox_Status.SelectionColor = Color.Black;
                richTextBox_Status.AppendText($"{what}\r\n");
                richTextBox_Status.SelectionStart = richTextBox_Status.Text.Length;
            });
        }

        public void AppendError(string what)
        {
            Invoke(() =>
            {
                richTextBox_Status.SelectionStart = richTextBox_Status.Text.Length;
                richTextBox_Status.SelectionColor = Color.Red;
                richTextBox_Status.AppendText($"{what}\r\n");
                richTextBox_Status.SelectionStart = richTextBox_Status.Text.Length;
            });
        }

        public void SetCompleted()
        {
            Invoke(() =>
            {
                _completed = true;
                button_Cancel.Text = "Close";

                Text = "Completed";
            });
        }

        public void SetCompletedByCancel()
        {
            AppendError("Cancelled by user");

            Invoke(() =>
            {
                _completed = true;
                button_Cancel.Text = "Close";

                Text = "Cancelled";
            });
        }

        public void SetCompletedByError(Exception ex)
        {
            AppendError(ex.Message);

            Invoke(() =>
            {
                _completed = true;
                button_Cancel.Text = "Close";

                Text = "Error";
            });
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            if (_completed)
                Close();
            else
                _source.Cancel();
        }
    }
}
