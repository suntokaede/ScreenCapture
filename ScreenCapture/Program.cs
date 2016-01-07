using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using System.Configuration;

namespace ScreenCapture
{
    class Program
    {
        /// <summary>
        /// 画像保存後に画像の表示をするかの値をコンフィグから取得
        /// </summary>
        private static bool IsShowAfterSave {
            get
            {
                bool flag;
                if (Boolean.TryParse(ConfigurationManager.AppSettings["isShowAfterSave"], out flag))
                {
                    return flag;
                }
                else
                {
                    return false;
                }
            }
        }

        static void Main(string[] args)
        {
            const string SCREENSHOTS_DIR_PATH = "screenshots";

            // screenshotsフォルダがなければ作成
            if (!Directory.Exists(SCREENSHOTS_DIR_PATH))
            {
                Directory.CreateDirectory(SCREENSHOTS_DIR_PATH);
            }

            // ファイル名決定
            string filepath = Path.Combine(SCREENSHOTS_DIR_PATH, string.Format("screenshot{0:yyyyMMddHHmmss}.png", DateTime.Now));
            
            // スクリーンを取得して画像として保存
            var bounds = Screen.PrimaryScreen.Bounds;
            using (Bitmap bmp = new Bitmap(bounds.Width, bounds.Height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), bmp.Size);
                bmp.Save(filepath, ImageFormat.Png);
            }

            // 保存した画像の表示
            if (IsShowAfterSave)
            {
                Process.Start(filepath);
            }
        }
    }
}
