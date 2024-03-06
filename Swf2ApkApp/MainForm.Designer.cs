namespace Swf2ApkApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            toolStrip1 = new ToolStrip();
            toolStripButton_Convert = new ToolStripButton();
            toolStripButton_SignApk = new ToolStripButton();
            toolStripButton_Settings = new ToolStripButton();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            textBox_ProjectName = new TextBox();
            label2 = new Label();
            textBox_AppName = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBox_VersionName = new TextBox();
            textBox_VersionCode = new TextBox();
            textBox_UUID = new TextBox();
            button_GenerateUUID = new Button();
            tabPage2 = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            listView_Assets = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            tableLayoutPanel3 = new TableLayoutPanel();
            button_ImportFile = new Button();
            button_ImportDir = new Button();
            button_Remove = new Button();
            button_SetAsEntry = new Button();
            tabPage3 = new TabPage();
            tableLayoutPanel4 = new TableLayoutPanel();
            pictureBox_XXXdpi = new PictureBox();
            pictureBox_XXdpi = new PictureBox();
            pictureBox_Xdpi = new PictureBox();
            pictureBox_Hdpi = new PictureBox();
            pictureBox_Mdpi = new PictureBox();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            button_ImportLdpi = new Button();
            button_ImportMdpi = new Button();
            button_ImportHdpi = new Button();
            button_ImportXdpi = new Button();
            button_ImportXXdpi = new Button();
            button_ImportXXXdpi = new Button();
            pictureBox_Ldpi = new PictureBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            saveFileDialog_SaveApk = new SaveFileDialog();
            openFileDialog_OpenAsset = new OpenFileDialog();
            folderBrowserDialog_OpenAssetDir = new FolderBrowserDialog();
            openFileDialog_OpenApk = new OpenFileDialog();
            folderBrowserDialog_OpenApkOutputDir = new FolderBrowserDialog();
            openFileDialog_OpenPNG = new OpenFileDialog();
            toolStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tabPage2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tabPage3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_XXXdpi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_XXdpi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Xdpi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Hdpi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Mdpi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Ldpi).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(32, 32);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton_Convert, toolStripButton_SignApk, toolStripButton_Settings });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton_Convert
            // 
            toolStripButton_Convert.Image = Properties.Resources.Convert;
            resources.ApplyResources(toolStripButton_Convert, "toolStripButton_Convert");
            toolStripButton_Convert.Name = "toolStripButton_Convert";
            toolStripButton_Convert.Click += toolStripButton_Convert_Click;
            // 
            // toolStripButton_SignApk
            // 
            toolStripButton_SignApk.Image = Properties.Resources.Sign;
            resources.ApplyResources(toolStripButton_SignApk, "toolStripButton_SignApk");
            toolStripButton_SignApk.Name = "toolStripButton_SignApk";
            toolStripButton_SignApk.Click += toolStripButton_SignApk_Click;
            // 
            // toolStripButton_Settings
            // 
            toolStripButton_Settings.Image = Properties.Resources.Settings;
            resources.ApplyResources(toolStripButton_Settings, "toolStripButton_Settings");
            toolStripButton_Settings.Name = "toolStripButton_Settings";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel1);
            resources.ApplyResources(tabPage1, "tabPage1");
            tabPage1.Name = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(textBox_ProjectName, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(textBox_AppName, 1, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(label5, 0, 4);
            tableLayoutPanel1.Controls.Add(textBox_VersionName, 1, 2);
            tableLayoutPanel1.Controls.Add(textBox_VersionCode, 1, 3);
            tableLayoutPanel1.Controls.Add(textBox_UUID, 1, 4);
            tableLayoutPanel1.Controls.Add(button_GenerateUUID, 2, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // textBox_ProjectName
            // 
            resources.ApplyResources(textBox_ProjectName, "textBox_ProjectName");
            textBox_ProjectName.Name = "textBox_ProjectName";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // textBox_AppName
            // 
            resources.ApplyResources(textBox_AppName, "textBox_AppName");
            textBox_AppName.Name = "textBox_AppName";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // textBox_VersionName
            // 
            resources.ApplyResources(textBox_VersionName, "textBox_VersionName");
            textBox_VersionName.Name = "textBox_VersionName";
            // 
            // textBox_VersionCode
            // 
            resources.ApplyResources(textBox_VersionCode, "textBox_VersionCode");
            textBox_VersionCode.Name = "textBox_VersionCode";
            // 
            // textBox_UUID
            // 
            resources.ApplyResources(textBox_UUID, "textBox_UUID");
            textBox_UUID.Name = "textBox_UUID";
            // 
            // button_GenerateUUID
            // 
            resources.ApplyResources(button_GenerateUUID, "button_GenerateUUID");
            button_GenerateUUID.Name = "button_GenerateUUID";
            button_GenerateUUID.UseVisualStyleBackColor = true;
            button_GenerateUUID.Click += button_GenerateUUID_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(tableLayoutPanel2);
            resources.ApplyResources(tabPage2, "tabPage2");
            tabPage2.Name = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(tableLayoutPanel2, "tableLayoutPanel2");
            tableLayoutPanel2.Controls.Add(listView_Assets, 0, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // listView_Assets
            // 
            listView_Assets.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            resources.ApplyResources(listView_Assets, "listView_Assets");
            listView_Assets.FullRowSelect = true;
            listView_Assets.GridLines = true;
            listView_Assets.Name = "listView_Assets";
            listView_Assets.UseCompatibleStateImageBehavior = false;
            listView_Assets.View = View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(columnHeader2, "columnHeader2");
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(tableLayoutPanel3, "tableLayoutPanel3");
            tableLayoutPanel3.Controls.Add(button_ImportFile, 0, 0);
            tableLayoutPanel3.Controls.Add(button_ImportDir, 0, 1);
            tableLayoutPanel3.Controls.Add(button_Remove, 0, 2);
            tableLayoutPanel3.Controls.Add(button_SetAsEntry, 0, 4);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // button_ImportFile
            // 
            resources.ApplyResources(button_ImportFile, "button_ImportFile");
            button_ImportFile.Name = "button_ImportFile";
            button_ImportFile.UseVisualStyleBackColor = true;
            button_ImportFile.Click += button_ImportFile_Click;
            // 
            // button_ImportDir
            // 
            resources.ApplyResources(button_ImportDir, "button_ImportDir");
            button_ImportDir.Name = "button_ImportDir";
            button_ImportDir.UseVisualStyleBackColor = true;
            button_ImportDir.Click += button_ImportDir_Click;
            // 
            // button_Remove
            // 
            resources.ApplyResources(button_Remove, "button_Remove");
            button_Remove.Name = "button_Remove";
            button_Remove.UseVisualStyleBackColor = true;
            button_Remove.Click += button_Remove_Click;
            // 
            // button_SetAsEntry
            // 
            resources.ApplyResources(button_SetAsEntry, "button_SetAsEntry");
            button_SetAsEntry.Name = "button_SetAsEntry";
            button_SetAsEntry.UseVisualStyleBackColor = true;
            button_SetAsEntry.Click += button_SetAsEntry_Click;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(tableLayoutPanel4);
            resources.ApplyResources(tabPage3, "tabPage3");
            tabPage3.Name = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(tableLayoutPanel4, "tableLayoutPanel4");
            tableLayoutPanel4.Controls.Add(pictureBox_XXXdpi, 5, 1);
            tableLayoutPanel4.Controls.Add(pictureBox_XXdpi, 4, 1);
            tableLayoutPanel4.Controls.Add(pictureBox_Xdpi, 3, 1);
            tableLayoutPanel4.Controls.Add(pictureBox_Hdpi, 2, 1);
            tableLayoutPanel4.Controls.Add(pictureBox_Mdpi, 1, 1);
            tableLayoutPanel4.Controls.Add(label11, 5, 0);
            tableLayoutPanel4.Controls.Add(label10, 4, 0);
            tableLayoutPanel4.Controls.Add(label9, 3, 0);
            tableLayoutPanel4.Controls.Add(label8, 2, 0);
            tableLayoutPanel4.Controls.Add(label7, 1, 0);
            tableLayoutPanel4.Controls.Add(label6, 0, 0);
            tableLayoutPanel4.Controls.Add(button_ImportLdpi, 0, 2);
            tableLayoutPanel4.Controls.Add(button_ImportMdpi, 1, 2);
            tableLayoutPanel4.Controls.Add(button_ImportHdpi, 2, 2);
            tableLayoutPanel4.Controls.Add(button_ImportXdpi, 3, 2);
            tableLayoutPanel4.Controls.Add(button_ImportXXdpi, 4, 2);
            tableLayoutPanel4.Controls.Add(button_ImportXXXdpi, 5, 2);
            tableLayoutPanel4.Controls.Add(pictureBox_Ldpi, 0, 1);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // pictureBox_XXXdpi
            // 
            resources.ApplyResources(pictureBox_XXXdpi, "pictureBox_XXXdpi");
            pictureBox_XXXdpi.Name = "pictureBox_XXXdpi";
            pictureBox_XXXdpi.TabStop = false;
            // 
            // pictureBox_XXdpi
            // 
            resources.ApplyResources(pictureBox_XXdpi, "pictureBox_XXdpi");
            pictureBox_XXdpi.Name = "pictureBox_XXdpi";
            pictureBox_XXdpi.TabStop = false;
            // 
            // pictureBox_Xdpi
            // 
            resources.ApplyResources(pictureBox_Xdpi, "pictureBox_Xdpi");
            pictureBox_Xdpi.Name = "pictureBox_Xdpi";
            pictureBox_Xdpi.TabStop = false;
            // 
            // pictureBox_Hdpi
            // 
            resources.ApplyResources(pictureBox_Hdpi, "pictureBox_Hdpi");
            pictureBox_Hdpi.Name = "pictureBox_Hdpi";
            pictureBox_Hdpi.TabStop = false;
            // 
            // pictureBox_Mdpi
            // 
            resources.ApplyResources(pictureBox_Mdpi, "pictureBox_Mdpi");
            pictureBox_Mdpi.Name = "pictureBox_Mdpi";
            pictureBox_Mdpi.TabStop = false;
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // button_ImportLdpi
            // 
            resources.ApplyResources(button_ImportLdpi, "button_ImportLdpi");
            button_ImportLdpi.Name = "button_ImportLdpi";
            button_ImportLdpi.UseVisualStyleBackColor = true;
            button_ImportLdpi.Click += button_ImportLdpi_Click;
            // 
            // button_ImportMdpi
            // 
            resources.ApplyResources(button_ImportMdpi, "button_ImportMdpi");
            button_ImportMdpi.Name = "button_ImportMdpi";
            button_ImportMdpi.UseVisualStyleBackColor = true;
            button_ImportMdpi.Click += button_ImportMdpi_Click;
            // 
            // button_ImportHdpi
            // 
            resources.ApplyResources(button_ImportHdpi, "button_ImportHdpi");
            button_ImportHdpi.Name = "button_ImportHdpi";
            button_ImportHdpi.UseVisualStyleBackColor = true;
            button_ImportHdpi.Click += button_ImportHdpi_Click;
            // 
            // button_ImportXdpi
            // 
            resources.ApplyResources(button_ImportXdpi, "button_ImportXdpi");
            button_ImportXdpi.Name = "button_ImportXdpi";
            button_ImportXdpi.UseVisualStyleBackColor = true;
            button_ImportXdpi.Click += button_ImportXdpi_Click;
            // 
            // button_ImportXXdpi
            // 
            resources.ApplyResources(button_ImportXXdpi, "button_ImportXXdpi");
            button_ImportXXdpi.Name = "button_ImportXXdpi";
            button_ImportXXdpi.UseVisualStyleBackColor = true;
            button_ImportXXdpi.Click += button_ImportXXdpi_Click;
            // 
            // button_ImportXXXdpi
            // 
            resources.ApplyResources(button_ImportXXXdpi, "button_ImportXXXdpi");
            button_ImportXXXdpi.Name = "button_ImportXXXdpi";
            button_ImportXXXdpi.UseVisualStyleBackColor = true;
            button_ImportXXXdpi.Click += button_ImportXXXdpi_Click;
            // 
            // pictureBox_Ldpi
            // 
            resources.ApplyResources(pictureBox_Ldpi, "pictureBox_Ldpi");
            pictureBox_Ldpi.Name = "pictureBox_Ldpi";
            pictureBox_Ldpi.TabStop = false;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(32, 32);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.IsLink = true;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // saveFileDialog_SaveApk
            // 
            saveFileDialog_SaveApk.DefaultExt = "apk";
            resources.ApplyResources(saveFileDialog_SaveApk, "saveFileDialog_SaveApk");
            // 
            // openFileDialog_OpenAsset
            // 
            resources.ApplyResources(openFileDialog_OpenAsset, "openFileDialog_OpenAsset");
            openFileDialog_OpenAsset.Multiselect = true;
            // 
            // folderBrowserDialog_OpenAssetDir
            // 
            resources.ApplyResources(folderBrowserDialog_OpenAssetDir, "folderBrowserDialog_OpenAssetDir");
            // 
            // openFileDialog_OpenApk
            // 
            openFileDialog_OpenApk.DefaultExt = "apk";
            resources.ApplyResources(openFileDialog_OpenApk, "openFileDialog_OpenApk");
            // 
            // folderBrowserDialog_OpenApkOutputDir
            // 
            resources.ApplyResources(folderBrowserDialog_OpenApkOutputDir, "folderBrowserDialog_OpenApkOutputDir");
            // 
            // openFileDialog_OpenPNG
            // 
            openFileDialog_OpenPNG.DefaultExt = "png";
            resources.ApplyResources(openFileDialog_OpenPNG, "openFileDialog_OpenPNG");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Name = "MainForm";
            Load += MainForm_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_XXXdpi).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_XXdpi).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Xdpi).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Hdpi).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Mdpi).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Ldpi).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton_Convert;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ToolStripButton toolStripButton_Settings;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private TabPage tabPage3;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TextBox textBox_ProjectName;
        private Label label2;
        private TextBox textBox_AppName;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBox_VersionName;
        private TextBox textBox_VersionCode;
        private TextBox textBox_UUID;
        private Button button_GenerateUUID;
        private SaveFileDialog saveFileDialog_SaveApk;
        private TableLayoutPanel tableLayoutPanel2;
        private ListView listView_Assets;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private TableLayoutPanel tableLayoutPanel3;
        private Button button_ImportFile;
        private Button button_ImportDir;
        private Button button_Remove;
        private Button button_SetAsEntry;
        private OpenFileDialog openFileDialog_OpenAsset;
        private FolderBrowserDialog folderBrowserDialog_OpenAssetDir;
        private ToolStripButton toolStripButton_SignApk;
        private OpenFileDialog openFileDialog_OpenApk;
        private FolderBrowserDialog folderBrowserDialog_OpenApkOutputDir;
        private TableLayoutPanel tableLayoutPanel4;
        private Button button_ImportXXXdpi;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Button button_ImportLdpi;
        private Button button_ImportMdpi;
        private Button button_ImportHdpi;
        private Button button_ImportXdpi;
        private Button button_ImportXXdpi;
        private PictureBox pictureBox_XXXdpi;
        private PictureBox pictureBox_XXdpi;
        private PictureBox pictureBox_Xdpi;
        private PictureBox pictureBox_Hdpi;
        private PictureBox pictureBox_Mdpi;
        private PictureBox pictureBox_Ldpi;
        private OpenFileDialog openFileDialog_OpenPNG;
    }
}
