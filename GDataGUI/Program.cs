using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDataGUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            moveDataClass moveData = new moveDataClass();
            moveData.readMoveData();
            itemDataClass itemData = new itemDataClass();
            itemData.readItemData();
            charDataClass charData = new charDataClass();
            charData.readCharData();
            moveData.writeMoveData();
            itemData.writeItemData();
            charData.writeCharData();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
