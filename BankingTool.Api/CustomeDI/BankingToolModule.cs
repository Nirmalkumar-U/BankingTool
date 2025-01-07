using System.Reflection;
using System.Text.RegularExpressions;
using Autofac;

namespace BankingTool.Api.CustomeDI
{
    public class BankingToolModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = ContainerBuilderExtensions.GetAssembly();

            foreach (var assembly in assemblies)
            {
                builder.RegisterAssemblyTypes(assembly)
                    .AsImplementedInterfaces()
                    .InstancePerDependency();
            }
        }
    }

    public static class ContainerBuilderExtensions
    {
        public static List<Assembly> GetAssembly()
        {
            string[] assemblyScanerPattern = new[] { @"BankingTool.Repository.*.dll", @"BankingTool.Service.*.dll" };

            // Make sure process paths are sane...
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            // 1. Scan for assemblies containing autofac modules in the bin folder
            List<Assembly> assemblies = new();
            assemblies.AddRange(
                Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.dll", SearchOption.AllDirectories)
                         .Where(filename => assemblyScanerPattern.Any(pattern => Regex.IsMatch(filename, pattern)))
                         .Select(Assembly.LoadFrom)
                );

            return assemblies;
        }
    }
}
