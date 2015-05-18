using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGuiTest
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        [STAThread]
        static void Main(string[] args)
        {
            AttachConsole(ATTACH_PARENT_PROCESS);

            if (args.Length > 0)
            {
                Console.WriteLine("Created commandline tool");

                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                ApplicationException.Exit();
            }
        }
    }
}
