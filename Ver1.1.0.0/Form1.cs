﻿using System;
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
using Windows.UI.Notifications;


namespace taskshots
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ts_icon.txt";
            if (File.Exists(filePath))
            {
                string iconPath = File.ReadAllText(filePath);
                if (File.Exists(iconPath))
                {
                    notifyIcon1.Icon = new System.Drawing.Icon(iconPath);
                }
                else
                {

                }
            }
            else
            {

            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 画像のサイズを指定し、Bitmapオブジェクトのインスタンスを作成
            Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            // Bitmap bm = new Bitmap(500, 300);   // 幅500ピクセル × 高さ300ピクセルの場合

            // Graphicsオブジェクトのインスタンスを作成
            Graphics gr = Graphics.FromImage(bm);
            // 画面全体をコピー
            gr.CopyFromScreen(new Point(0, 0), new Point(0, 0), bm.Size);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JPGファイル(*.jpg)|*.jpg;*.jpeg|bmpファイル(*.bmp)|*.bmp;";
            dialog.Title = "保存してあ♡げ♡る";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //File.WriteAllText(dialog.FileName, txt_memo.Text);
                bm.Save(dialog.FileName + "", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            properties pro = new properties();
            pro.Show();
        }

        private void 終了ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //アプリケーションを終了する
            Application.Exit();
        }

        private void アップデート確認ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadString("http://files.piennu777.jp/updater/taskshots/key");
                wc.Dispose();
                //アクセス可能な場合の処理
                if (Password("http://files.piennu777.jp/updater/taskshots/key", "1.0.0.1"))
                {
                    MessageBox.Show("最新のバージョンです。\r\nいやぁいいですねぇ...",
                        "👍",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
                }
                else
                {
                    DialogResult result = MessageBox.Show("新しいバージョンが見つかりました。\r\n新しいバージョンをインストールしますか？",
                        "🙌",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Asterisk,
                        MessageBoxDefaultButton.Button2);

                    //何が選択されたか調べる
                    if (result == DialogResult.Yes)
                    {
                        up up = new up();
                        up.Show();
                    }
                    else if (result == DialogResult.No)
                    {

                    }
                }
            }
            catch (WebException)
            {
                //アクセスできない場合の処理
                MessageBox.Show("ネットに繋がっていないか、サーバーが停止されている可能性があります。",
    "Error 404",
    MessageBoxButtons.OK,
    MessageBoxIcon.Error);
            }
        }

        private bool Password(string url, string password)
        {

            WebClient client = new WebClient();
            string webps = client.DownloadString(url);

            byte[] input = Encoding.ASCII.GetBytes(password);
            System.Security.Cryptography.SHA256 sha = new System.Security.Cryptography.SHA256CryptoServiceProvider();
            byte[] hash_sha256 = sha.ComputeHash(input);

            string hash = "";

            for (int i = 0; i < hash_sha256.Length; i++)
            {
                hash = hash + string.Format("{0:X2}", hash_sha256[i]);
            }
            if (webps == hash)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //setting set = new setting();
            //set.Show();
            //アクセスできない場合の処理
            MessageBox.Show("現在この機能は使えません。",
"Error 403",
MessageBoxButtons.OK,
MessageBoxIcon.Error);
        }

        private void tToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "おしらせ";
            notifyIcon1.BalloonTipText = "おしらせのメッセージ";
            notifyIcon1.ShowBalloonTip(3000);
        }

        private void 設定ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            setting setting = new setting();
            setting.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
