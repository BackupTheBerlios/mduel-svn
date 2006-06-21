using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace oltp2olap
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //new heuristics.Classification(@"c:\moody_relations.txt");
        }
    }
}