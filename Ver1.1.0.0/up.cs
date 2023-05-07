using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace taskshots
{
    public partial class up : Form
    {
        public up()
        {
            InitializeComponent();
            this.Hide();
        }

        private void up_Load(object sender, EventArgs e)
        {
            string url = "http://files.piennu777.jp/updater/taskshots/taskshots.exe"; // ダウンロードするファイルのURL
            string fileName = "taskshots_updater.exe"; // 保存するファイル名
            string tempFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.DoNotVerify), "Temp"); // 保存先のパス（一時フォルダに保存する例）
            string filePath = Path.Combine(tempFolderPath, fileName); // 完全なファイルパス

            try
            {
                using (WebClient wc1 = new WebClient())
                {
                    wc1.DownloadFile(url, filePath); // ファイルをダウンロードして保存する
                    this.Close();
                }

                // ファイルを実行する
                System.Diagnostics.Process.Start(filePath);

            }
            catch (WebException ex)
            {
                MessageBox.Show("ネットに繋がっていないか、サーバーが停止されている可能性があります。",
    "Error 404",
    MessageBoxButtons.OK,
    MessageBoxIcon.Error);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。\r開発者に問い合わせてみてください。\rメールアドレス：pien@piennu777.ml",
    "Error 000",
    MessageBoxButtons.OK,
    MessageBoxIcon.Error);
                this.Close();
            }

        }
    }
}
