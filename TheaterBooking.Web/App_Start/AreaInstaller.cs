using System.IO;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.Optimization;
using System.Web.Routing;

namespace TheaterBooking.Web
{
    /// <summary>
    ///     Installs MVC Areas for the entire application
    /// </summary>
    [UsedImplicitly]
    public class AreaInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            foreach (var area in RouteTable.Routes.OfType<Route>()
                .Where(route => route.DataTokens?.ContainsKey("area") == true)
                .Select(route => route.DataTokens["area"]))
            {
                InitializeBundle(new ScriptBundle($"~/Content/js-{area}"), "js", $"~/Areas/{area}/Templates/",
                    $"~/Areas/{area}/Content/");
                InitializeBundle(new LessBundle($"~/Content/less-{area}"), "less",
                    $"~/Areas/{area}/Content/", $"~/Areas/{area}/Templates/");
            }
        }

        private static void InitializeBundle(Bundle bundle, string extension, [NotNull] params string[] directories)
        {
            var paths = directories.Where(directory => HostingEnvironment.VirtualPathProvider.DirectoryExists(directory)).ToArray();
            if (paths.Length == 0)
            {
                return;
            }

            foreach (var path in paths)
            {
                AddDirectory(bundle, extension, path);
            }

            BundleTable.Bundles.Add(bundle);
        }

        private static void AddDirectory([NotNull] Bundle bundle, [NotNull] string extension, [NotNull] string directory)
        {
            // Read the include.rules file in this directory, and add each included directory
            var includes = HostingEnvironment.VirtualPathProvider.GetFile(
                HostingEnvironment.VirtualPathProvider.CombineVirtualPaths(directory, "include.rules"));
            if (HostingEnvironment.VirtualPathProvider.FileExists(includes.VirtualPath))
            {
                using (var reader = new StreamReader(includes.Open(), Encoding.UTF8))
                {
                    string include;
                    while ((include = reader.ReadLine()) != null)
                    {
                        if (include.StartsWith("~/"))
                        {
                            AddDirectory(bundle, extension, include);
                        }
                    }
                }
            }

            bundle.Include($"{directory}*.{extension}");
            foreach (var virtualDirectory in HostingEnvironment.VirtualPathProvider.GetDirectory(directory)
                .Directories.Cast<VirtualDirectory>())
            {
                AddDirectory(bundle, extension, $"~{virtualDirectory.VirtualPath}/");
            }
        }
    }
}
