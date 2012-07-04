using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RealtickTickerConverter
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
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            // register the event that is fired after the key press.

            Console.ReadLine();
        }
    }
}
