using System;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace FhictPowerTools.FakeImplementations
{
    public class TypeResolverFake : ITypeResolver
    {
        private readonly IServiceProvider _provider;

            public TypeResolverFake(IServiceProvider provider)
            {
                _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            }

            public object Resolve(Type type)
            {
                return _provider.GetRequiredService(type);
            }

            public void Dispose()
            {
                if (_provider is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }