using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Administrador {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var inicioApp = new Inicio();
            if (args.Length > 0) {
                inicioApp.setCurrentUser(args[0]);
            } else {
                inicioApp.setCurrentUser(Environment.UserDomainName + "\\" + Environment.UserName);
            }
            
            Application.Run(inicioApp);
        }
    }
}
