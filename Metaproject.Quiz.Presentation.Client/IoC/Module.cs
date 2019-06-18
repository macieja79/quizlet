using Autofac;
using Metaproject.Quiz.Inf.IoC;
using Metaproject.Quiz.Inf.IoC.Modules;

namespace Metaproject.Quiz.Presentation.Client.IoC
{
    public class ClientModule  : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new QuizletModule());
            builder.RegisterType<ClientForm>().AsSelf();
        }
    }
}