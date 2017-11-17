using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using JetBrains.Annotations;
using YamlDotNet.Serialization;

namespace TheaterBooking.Web.Users.Permissions
{
    /// <summary>
    ///     Provides a model for permissions
    /// </summary>
    [UsedImplicitly]
    internal class PermissionsModel : IPermissionsModel
    {
        private readonly IDictionary<string, ISet<string>> _permissionsByRole = new Dictionary<string, ISet<string>>();

        /// <summary>
        ///     Instantiates a new permissions model
        /// </summary>
        public PermissionsModel()
        {
            var path = HostingEnvironment.MapPath("~/permissions.yml");
            Debug.Assert(path != null, "path != null");

            using (var reader = new StreamReader(path))
            {
                var permissions = new Deserializer().Deserialize<Permissions>(reader);

                foreach (var role in permissions.Roles)
                {
                    var inheritance = new Stack<Permissions.Role>();
                    inheritance.Push(role.Value);

                    var current = role.Value;
                    while (current.Inherit != null)
                    {
                        current = permissions.Roles[current.Inherit];
                        inheritance.Push(current);
                    }

                    _permissionsByRole[role.Key] = new HashSet<string>();
                    while (inheritance.Count != 0)
                    {
                        current = inheritance.Pop();
                        _permissionsByRole[role.Key].UnionWith(current.Permissions.Where(permission => !permission.StartsWith("-")));
                        _permissionsByRole[role.Key].ExceptWith(current.Permissions
                            .Where(permission => permission.StartsWith("-"))
                            .Select(permission => permission.Substring(1)));
                    }
                }
            }
        }

        /// <summary>
        ///     Gets the permissions for the specified role
        /// </summary>
        /// <param name="role">The role to get the permissions of</param>
        /// <returns>The permissions for the specified role</returns>
        public IEnumerable<string> this[string role] => (IReadOnlyCollection<string>) _permissionsByRole[role];

        /// <summary>
        ///     Gets the available roles within the application
        /// </summary>
        public IEnumerable<string> Roles => _permissionsByRole.Keys;
    }
}
