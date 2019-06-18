using System;
using System.Reflection;
using Autofac;
using Metaproject.Quiz.Inf.WordDocsRepository;

namespace Metaproject.Quiz.Inf.IoC
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetAssembly(typeof(WordFilesRepository));
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces();
        }
    }
}
