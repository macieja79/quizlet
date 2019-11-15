using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Autofac;
using Autofac.Core;
using Metaproject.Quiz.Application.Core;
using Metaproject.Quiz.Domain.Entities;
using Metaproject.Quiz.Presentation.Client.IoC;

namespace Metaproject.Quiz.Presentation.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);


                var builder = new ContainerBuilder();
            builder.RegisterModule(new ClientModule());

            using (var container = builder.Build())
            {
                using (var scope = container.BeginLifetimeScope())
                {

                    if (args.Length == 1)
                    {
                        var repo = scope.Resolve<IWordFilesRepository>();

                        var learnedDoc = "__learnedDoc.docx";

                        var originFilePath = args[0];
                        var originalFileName = Path.GetFileName(originFilePath);
                        var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        var destFilePath = Path.Combine(currentDir, learnedDoc);

                        File.Delete(destFilePath);
                        File.Copy(originFilePath, destFilePath);

                        var doc = repo.GetDocument(destFilePath);
                        var tables = new TypedParameter(typeof(List<QuestionTable>), doc.Tables);
                        var learningForm = scope.Resolve<LearningForm>(tables);
                        learningForm.ShowDialog();

                        File.Delete(destFilePath);
                    }
                    else
                    {
                        var form = scope.Resolve<ClientForm>();
                        form.ShowDialog();
                    }




                }
            }

        }
    }
}
