using System;
using System.Reflection;
using Autofac;
using Metaproject.Quiz.Inf.QuizletService;

namespace Metaproject.Quiz.Inf.IoC.Modules
{
    public class QuizletModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetAssembly(typeof(QuizletServiceImpl));
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces();
        }
    }
}
