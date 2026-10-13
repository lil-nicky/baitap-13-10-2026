using Bai46;
using System;
using System.Windows.Forms;

namespace Bai46
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}