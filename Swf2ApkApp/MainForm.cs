using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using Scriban.Parsing;
using Swf2Apk;

namespace Swf2ApkApp
{
    public partial class MainForm : Form
    {
        private string _javaExecutablePath = String.Empty;
        private readonly PictureBox[] _iconBox = new PictureBox[6];
        private readonly int[] _iconSize = [36,48,72,96,144,192];
        private readonly string[] _iconName = ["ldpi", "mdpi", "hdpi", "xhdpi", "xxhdpi", "xxxhdpi"];

        public MainForm()
        {
            InitializeComponent();

            _iconBox[0] = pictureBox_Ldpi;
            _iconBox[1] = pictureBox_Mdpi;
            _iconBox[2] = pictureBox_Hdpi;
            _iconBox[3] = pictureBox_Xdpi;
            _iconBox[4] = pictureBox_XXdpi;
            _iconBox[5] = pictureBox_XXXdpi;
        }

        private void LoadConfig()
        {
            try
            {
                string javaExecutablePath = ConfigurationManager.AppSettings["JavaExecutablePath"] ?? String.Empty;
                if (String.IsNullOrEmpty(javaExecutablePath))
                    _javaExecutablePath = JavaUtils.GetJavaExecutable() ?? "";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            Debug.WriteLine($"Java executable: {_javaExecutablePath}");
        }

        private static async Task DoConvert(string javaExecutablePath, string projectName, string appName, string versionName,
            string versionCode, string uuid, string appEntry, IDictionary<string, string> assets, IDictionary<string, Image> icons,
            string outputFilename, Action<string> onStdOutput, Action<string> onStdError, CancellationToken token)
        {
            projectName = projectName?.Trim() ?? String.Empty;
            appName = appName?.Trim() ?? String.Empty;
            versionName = versionName?.Trim() ?? String.Empty;
            versionCode = versionCode?.Trim() ?? String.Empty;
            uuid = uuid?.Trim() ?? String.Empty;
            appEntry = appEntry?.Trim() ?? String.Empty;

            // Validate input
            if (!Regex.IsMatch(projectName, @"^[a-zA-Z_][a-zA-Z_0-9]*$"))
                throw new ArgumentException("Invalid project name.");
            if (String.IsNullOrEmpty(appName))
                throw new ArgumentException("App name is empty.");
            if (String.IsNullOrEmpty(versionName))
                throw new ArgumentException("Version name is empty.");
            if (!Regex.IsMatch(versionCode, @"^\d+$"))
                throw new ArgumentException("Invalid version code.");
            if (!Regex.IsMatch(uuid, @"^[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}$"))
                throw new ArgumentException("Invalid UUID.");
            if (String.IsNullOrEmpty(appEntry) || !assets.ContainsKey(appEntry))
                throw new ArgumentException("Invalid app entry.");

            // Essential files
            string exeDir = Path.GetDirectoryName(Application.ExecutablePath) ?? String.Empty;
            string apkToolJarPath = Path.Combine(exeDir, "res", "apktool.jar");
            string templatePath = Path.Combine(exeDir, "res", "air_template", "template");
            string configPath = Path.Combine(exeDir, "res", "air_template", "config.json");

            // Check path
            if (!File.Exists(javaExecutablePath))
                throw new IOException($"Java executable is not found.");
            if (!File.Exists(apkToolJarPath))
                throw new IOException($"res/apktool.jar is not found.");
            if (!File.Exists(configPath))
                throw new IOException($"res/config.json is not found.");
            if (!Directory.Exists(templatePath))
                throw new IOException($"res/template is not found.");

            // META-INF should not in assets
            foreach (var path in assets)
            {
                if (path.Key.StartsWith("META-INF", StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("META-INF should not be in assets.");
            }

            // Create temporary directory
            string tempDir = Path.Combine(Path.GetTempPath(), "Swf2Apk", projectName);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
            Directory.CreateDirectory(tempDir);

            string apkTempDir = Path.Combine(tempDir, "apk");
            Directory.CreateDirectory(apkTempDir);

            // Write icons
            Dictionary<string, string> iconPaths = new Dictionary<string, string>();
            foreach (var icon in icons)
            {
                string iconPath = Path.Combine(tempDir, $"icon-{icon.Key}.png");
                icon.Value.Save(iconPath, ImageFormat.Png);
                iconPaths.Add(icon.Key, iconPath);
            }

            try
            {
                // Create apktool object
                var apkTool = new ApkTool(javaExecutablePath, apkToolJarPath);

                // Create swf to apk converter object
                var converter = new Converter(templatePath, configPath);

                // Setup converter settings
                var converterSettings = new Converter.ConverterSettings
                {
                    Variables = new Dictionary<string, string>
                    {
                        { "ProjectName", projectName },
                        { "AppTitle", appName },
                        { "VersionName", versionName },
                        { "VersionCode", versionCode },
                        { "AppUUID", uuid },
                        { "AppEntry", appEntry }
                    },
                    Assets = assets,
                    Icons = iconPaths,
                    WorkingDirectory = apkTempDir,
                    OutputFilename = outputFilename,
                    OnStdOutput = onStdOutput,
                    OnStdError = onStdError
                };

                // Invoke converter
                await converter.Convert(apkTool, converterSettings, token);
            }
            finally
            {
                // Delete temporary directory
                Directory.Delete(tempDir, true);
            }
        }

        private static async Task DoSign(string javaExecutablePath, string apkPath, string outputDir,
            Action<string> onStdOutput, Action<string> onStdError, CancellationToken token)
        {
            // Essential files
            string exeDir = Path.GetDirectoryName(Application.ExecutablePath) ?? String.Empty;
            string apkSignToolJarPath = Path.Combine(exeDir, "res", "uber-apk-signer.jar");

            // Check path
            if (!File.Exists(javaExecutablePath))
                throw new IOException($"Java executable is not found.");
            if (!File.Exists(apkSignToolJarPath))
                throw new IOException($"res/uber-apk-signer.jar is not found.");

            ApkSignTool tool = new ApkSignTool(javaExecutablePath, apkSignToolJarPath);
            await tool.DebugSignApk(apkPath, outputDir, onStdOutput, onStdError);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void toolStripButton_Convert_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == saveFileDialog_SaveApk.ShowDialog())
            {
                // Prepare assets
                string entryPoint = String.Empty;
                var assets = new Dictionary<string, string>();
                foreach (ListViewItem item in listView_Assets.Items)
                {
                    assets.Add(item.Text, item.SubItems[1].Text);
                    if (item.Font.Bold)
                        entryPoint = item.Text;
                }

                if (String.IsNullOrEmpty(entryPoint))
                {
                    MessageBox.Show("Please set an entry point.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Prepare icons
                var icons = new Dictionary<string, Image>();
                for (int i = 0; i < _iconBox.Length; i++)
                {
                    if (_iconBox[i].Image != null)
                        icons.Add(_iconName[i], _iconBox[i].Image);
                }

                // Open progress form
                using var progressForm = new ProgressForm();

                // Force the form to be created
                // https://stackoverflow.com/questions/66921/load-a-form-without-showing-it
                // FIXME: This is really tricky, is there any better way?
                IntPtr dummy = progressForm.Handle;

                var t = DoConvert(_javaExecutablePath,
                    textBox_ProjectName.Text,
                    textBox_AppName.Text,
                    textBox_VersionName.Text,
                    textBox_VersionCode.Text,
                    textBox_UUID.Text,
                    entryPoint,
                    assets,
                    icons,
                    saveFileDialog_SaveApk.FileName,
                    (text) => { progressForm.AppendOutput(text); },
                    (text) => { progressForm.AppendError(text); },
                    progressForm.CancellationToken);

                t.GetAwaiter().OnCompleted(() =>
                {
                    if (t.IsFaulted)
                        progressForm.SetCompletedByError(t.Exception);
                    else if (t.IsCanceled)
                        progressForm.SetCompletedByCancel();
                    else
                        progressForm.SetCompleted();
                });

                progressForm.ShowDialog(this);
            }
        }

        private void toolStripButton_SignApk_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog_OpenApk.ShowDialog())
            {
                if (DialogResult.OK == folderBrowserDialog_OpenApkOutputDir.ShowDialog())
                {
                    // Open progress form
                    using var progressForm = new ProgressForm();

                    // Force the form to be created
                    // https://stackoverflow.com/questions/66921/load-a-form-without-showing-it
                    // FIXME: This is really tricky, is there any better way?
                    IntPtr dummy = progressForm.Handle;

                    var t = DoSign(_javaExecutablePath,
                        openFileDialog_OpenApk.FileName,
                        folderBrowserDialog_OpenApkOutputDir.SelectedPath,
                        (text) => { progressForm.AppendOutput(text); },
                        (text) => { progressForm.AppendError(text); },
                        progressForm.CancellationToken);

                    t.GetAwaiter().OnCompleted(() =>
                    {
                        if (t.IsFaulted)
                            progressForm.SetCompletedByError(t.Exception);
                        else if (t.IsCanceled)
                            progressForm.SetCompletedByCancel();
                        else
                            progressForm.SetCompleted();
                    });

                    progressForm.ShowDialog(this);
                }
            }
        }

        private void button_GenerateUUID_Click(object sender, EventArgs e)
        {
            textBox_UUID.Text = Guid.NewGuid().ToString();
        }

        private void AddAssetItem(string assetPath, string fullPath)
        {
            if (!listView_Assets.Items.ContainsKey(assetPath))
            {
                var item = new ListViewItem(assetPath)
                {
                    Name = assetPath,
                    SubItems =
                    {
                        new ListViewItem.ListViewSubItem(null, fullPath)
                    }
                };
                listView_Assets.Items.Add(item);
            }
        }

        private void AddDirRecursive(string rootPath, string dirPath)
        {
            var dir = new DirectoryInfo(dirPath);
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            foreach (FileInfo file in dir.GetFiles())
            {
                string fullPath = Path.Combine(dirPath, file.Name);
                string relativePath = Path.GetRelativePath(rootPath, fullPath);
                AddAssetItem(relativePath, fullPath);
            }

            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                string fullPath = Path.Combine(dirPath, subDir.Name);
                AddDirRecursive(rootPath, fullPath);
            }
        }

        private void button_ImportFile_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog_OpenAsset.ShowDialog())
            {
                foreach (var fullPath in openFileDialog_OpenAsset.FileNames)
                {
                    var filename = Path.GetFileName(fullPath);
                    AddAssetItem(filename, fullPath);
                }
                listView_Assets.Sort();
            }
        }

        private void button_ImportDir_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog_OpenAssetDir.ShowDialog())
            {
                AddDirRecursive(folderBrowserDialog_OpenAssetDir.SelectedPath,
                    folderBrowserDialog_OpenAssetDir.SelectedPath);
                listView_Assets.Sort();
            }
        }

        private void button_Remove_Click(object sender, EventArgs e)
        {
            if (listView_Assets.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listView_Assets.SelectedItems)
                    listView_Assets.Items.Remove(item);
            }
        }

        private void button_SetAsEntry_Click(object sender, EventArgs e)
        {
            if (listView_Assets.SelectedItems.Count > 0)
            {
                // Clear current entry item
                foreach (ListViewItem item in listView_Assets.Items)
                {
                    if (item.Font.Bold)
                        item.Font = new Font(item.Font, FontStyle.Regular);
                }

                // Bold the selected item
                listView_Assets.SelectedItems[0].Font = new Font(listView_Assets.SelectedItems[0].Font,
                    FontStyle.Bold);
            }
        }

        // https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.MakeTransparent();
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using var graphics = Graphics.FromImage(destImage);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);

            return destImage;
        }

        private void ImportIconImage(int index)
        {
            if (DialogResult.OK == openFileDialog_OpenPNG.ShowDialog())
            {
                using var img = Image.FromFile(openFileDialog_OpenPNG.FileName);

                // Fill images based on current image
                for (int i = 0; i < _iconBox.Length; i++)
                {
                    // If already filled, skip
                    if (i != index && _iconBox[i].Image != null)
                        continue;

                    // Resize image to target size
                    var targetSize = _iconSize[i];
                    if (img.Width != targetSize || img.Height != targetSize)
                    {
                        var resizedImg = ResizeImage(img, targetSize, targetSize);
                        _iconBox[i].Image = resizedImg;
                    }
                    else
                    {
                        _iconBox[i].Image = (Image)img.Clone();
                    }
                }
            }
        }

        private void button_ImportLdpi_Click(object sender, EventArgs e)
        {
            ImportIconImage(0);
        }

        private void button_ImportMdpi_Click(object sender, EventArgs e)
        {
            ImportIconImage(1);
        }

        private void button_ImportHdpi_Click(object sender, EventArgs e)
        {
            ImportIconImage(2);
        }

        private void button_ImportXdpi_Click(object sender, EventArgs e)
        {
            ImportIconImage(3);
        }

        private void button_ImportXXdpi_Click(object sender, EventArgs e)
        {
            ImportIconImage(4);
        }

        private void button_ImportXXXdpi_Click(object sender, EventArgs e)
        {
            ImportIconImage(5);
        }
    }
}
