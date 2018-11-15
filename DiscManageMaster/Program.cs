using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DiscManageMaster.Core;

namespace DiscManageMaster
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            if (!CommVar.IsSingleProcess()) return;
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}