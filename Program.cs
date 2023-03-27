using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafPack
{
    static class Program
    {
        /// Grafpack 2D Application
        /// The main entry pointt for the application.
        /// SID:1824482
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GrafPack());
        }
    }
}
