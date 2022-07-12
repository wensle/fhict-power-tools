using System;
using FhictPowerTools.Cli;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace FhictPowerTools.FakeImplementations
{
    public class TypeRegistrarMock : ITypeRegistrar
    {
        
        private readonly IServiceCollection _builder;
        
        public TypeRegistrarMock()
        {
            _builder = new ServiceCollection();
        }
        
        public void Register(Type service, Type implementation)
        {
            _builder.AddScoped(service, implementation);
        }

        public void RegisterInstance(Type service, object implementation)
        {
            _builder.AddScoped(service, p => implementation);

        }

        public void RegisterLazy(Type service, Func<object> factory)
        {
            throw new NotImplementedException();
        }

        public ITypeResolver Build()
        {
            return new TypeResolver(_builder.BuildServiceProvider());
        }
    }
}