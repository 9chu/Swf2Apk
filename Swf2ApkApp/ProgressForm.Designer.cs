namespace Swf2ApkApp
{
    partial class ProgressForm
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
            richTextBox_Status = new RichTextBox();
            button_Cancel = new Button();
            SuspendLayout();
            // 
            // richTextBox_Status
            // 
            richTextBox_Status.Dock = DockStyle.Fill;
            richTextBox_Status.Location = new Point(0, 0);
            richTextBox_Status.Name = "richTextBox_Status";
            richTextBox_Status.ReadOnly = true;
            richTextBox_Status.Size = new Size(492, 256);
            richTextBox_Status.TabIndex = 0;
            richTextBox_Status.Text = "";
            // 
            // button_Cancel
            // 
            button_Cancel.Dock = DockStyle.Bottom;
            button_Cancel.Location = new Point(0, 256);
            button_Cancel.Name = "button_Cancel";
            button_Cancel.Size = new Size(492, 23);
            button_Cancel.TabIndex = 1;
            button_Cancel.Text = "Cancel";
            button_Cancel.UseVisualStyleBackColor = true;
            button_Cancel.Click += button_Cancel_Click;
            // 
            // ProgressForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(492, 279);
            ControlBox = false;
            Controls.Add(richTextBox_Status);
            Controls.Add(button_Cancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProgressForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Converting in progress";
            Load += ProgressForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox_Status;
        private Button button_Cancel;
    }
}