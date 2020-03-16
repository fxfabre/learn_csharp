using System;
using System.Windows.Forms;
using System.Threading;

namespace BergerMT_UI
{
    internal static class MainBergerUI
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var myDisplay = new GlobalDisplay();
            Application.Run(myDisplay);
        }

    }
}

