using System;
using System.Runtime.Hosting;
using System.Windows;
using System.Windows.Forms;

namespace zoom_sdk_demo
{
    static class Program
    {
        static public ZoomDemo zd;

        [STAThread]
        static void Main()
        {
            ConfigReader cr = new ConfigReader(new ConfigReaderXml("config.xml"));
            ZoomDemoConfiguration cfg = cr.ReadConfig();
            zd = new ZoomDemo(cfg);
            zd.Start();

            var f = new Commands();
            f.Show();

            System.Windows.Forms.Application.Run(f);
            //MessageBox.Show("ok to close", "ZOOM DEMO");
            zd.Stop();
        }
    }
}
