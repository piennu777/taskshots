using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace taskshots
{
    public partial class setting : Form
    {
        public setting()
        {
            InitializeComponent();
            //ユーザーがサイズを変更できないようにする
            //最大化、最小化はできる
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //フォームが最大化されないようにする
            this.MaximizeBox = false;
            //フォームが最小化されないようにする
            this.MinimizeBox = false;


            // AppDataのフォルダパスを取得する
            string appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // "ts_icon.txt"ファイルのパスを作成する
            string filePath = Path.Combine(appDataFolderPath, "ts_icon.txt");

            // ファイルが存在する場合には、中身をtextboxに表示する
            if (File.Exists(filePath))
            {
                // ファイルの中身を読み込む
                string text = File.ReadAllText(filePath);

                // textboxに表示する
                textBoxImagePath.Text = text;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ファイル選択ダイアログを表示して画像を選択する
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Icoファイル (*.ico) | *.ico";
            openFileDialog1.Title = "設定したいアイコンを選んでね♡";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // 選択された画像のパスを取得する
                string imagePath = openFileDialog1.FileName;

                // AppDataフォルダにテキストファイルを作成し、画像のパスを書き込む
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fileName = "ts_icon.txt";
                string filePath = Path.Combine(appDataPath, fileName);

                // テキストボックスに画像のパスを表示する
                textBoxImagePath.Text = imagePath;
                // テキストファイルに選択された画像のパスを書き込む
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(imagePath);
                }

                Console.WriteLine("Image path selected: " + imagePath);

                DialogResult result = MessageBox.Show("設定を完了するにはアプリを再起動する必要があります。\rアプリを再起動しますか？",
    "警告",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Exclamation,
    MessageBoxDefaultButton.Button2);

                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //アプリケーションを再起動する
                    Application.Restart();
                }
                else if (result == DialogResult.No)
                {

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void setting_Load(object sender, EventArgs e)
        {
            textBoxImagePath.ReadOnly = true;
            //フォームを画面の中央に配置

            this.SetBounds((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2, this.Width,
                this.Height);
        }
    }
}
