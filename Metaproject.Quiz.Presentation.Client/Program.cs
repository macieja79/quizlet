using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Metaproject.Quiz.Presentation.Client.IoC;

namespace Metaproject.Quiz.Presentation.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();
            builder.RegisterModule(new ClientModule());

            using (var container = builder.Build())
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var form = scope.Resolve<ClientForm>();
                    form.ShowDialog();
                }



            }

        }
    }
}
