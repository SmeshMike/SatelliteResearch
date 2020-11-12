using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResearchModel;

namespace SatelliteResearch
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Logger.InitLogger();
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var form = new MainForm();
                Application.Run(form);
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Logger.WriteLine("Main Exception Type=" + ex.GetType());
                Logger.WriteLine("Main Exception Message => " + ex.Message);
                Logger.WriteLine("Main Exception Stack => " + ex.StackTrace);
                int cnt = 1;
                Exception exx = ex.InnerException;
                while (exx != null)
                {
                    Logger.WriteLine("Inner Exception[" + cnt + "] Type=" + exx.GetType());
                    Logger.WriteLine("Inner Exception[" + cnt + "] Message => " + exx.Message);
                    Logger.WriteLine("Inner Exception[" + cnt + "] Stack => " + exx.StackTrace);
                    cnt++;
                    exx = exx.InnerException;
                }
            }

        }
    }
}
