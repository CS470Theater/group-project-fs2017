using System;
using System.Collections.Generic;
using System.Linq;
using TheaterBooking.Web.Utilities;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using System.Web.Mvc;
using System.Web.UI;
using TheaterBooking.Web.Areas.Error.Controllers;

namespace TheaterBooking.Web
{
    /// <summary>
    ///     Installs global request filters for the entire application
    /// </summary>
    [UsedImplicitly]
    public class FilterInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            GlobalFilters.Filters.Add(new ErrorAttribute(container));
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            GlobalFilters.Filters.Add(new RequireHttpsAttribute());
            GlobalFilters.Filters.Add(new TransactionAttribute(container));
            GlobalFilters.Filters.Add(new OutputCacheAttribute {Duration = 0, Location = OutputCacheLocation.Client, NoStore = true});

            var providers = FilterProviders.Providers.ToArray();
            FilterProviders.Providers.Clear();
            FilterProviders.Providers.Add(new ExcludeFilterProvider(providers));
        }

        private class ExcludeFilterProvider : IFilterProvider
        {
            private readonly FilterProviderCollection _filterProviders;

            public ExcludeFilterProvider(IList<IFilterProvider> filters)
            {
                _filterProviders = new FilterProviderCollection(filters);
            }

            public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
            {
                var filters = _filterProviders.GetFilters(controllerContext, actionDescriptor).ToList();
                var excludeFilters = new HashSet<Type>(filters.Select(filter => filter.Instance)
                    .OfType<ExcludeFilterAttribute>()
                    .SelectMany(filter => filter.FilterTypes));
                return excludeFilters.Any() ? filters.Where(filter => !excludeFilters.Contains(filter.Instance.GetType())) : filters;
            }
        }
    }

    /// <summary>
    ///     Prevents the specified filter type from affecting an action method
    /// </summary>
    internal class ExcludeFilterAttribute : FilterAttribute
    {
        public ExcludeFilterAttribute(params Type[] filterTypes)
        {
            FilterTypes = filterTypes;
        }

        internal Type[] FilterTypes { get; }
    }
}
