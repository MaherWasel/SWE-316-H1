﻿using System;
using System.Windows.Forms;

namespace ConsoleApp1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm()); // Run the MainForm
        }
    }
}
