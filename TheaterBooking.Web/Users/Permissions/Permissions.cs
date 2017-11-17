using System.Collections.Generic;
using JetBrains.Annotations;
using YamlDotNet.Serialization;

namespace TheaterBooking.Web.Users.Permissions
{
    /// <summary>
    ///     Represents the permissions.yml file
    /// </summary>
    [UsedImplicitly] // YAML Deserialization
    internal class Permissions
    {
        /// <summary>
        ///     Gets a map between role names and their roles
        /// </summary>
        [YamlMember(Alias = "roles")]
        [UsedImplicitly(ImplicitUseKindFlags.Assign)] // YAML Deserialization
        [NotNull]
        public IDictionary<string, Role> Roles { get; internal set; } = new Dictionary<string, Role>();

        /// <summary>
        ///     Represents a role
        /// </summary>
        [UsedImplicitly] // YAML Deserialization
        internal class Role
        {
            /// <summary>
            ///     Gets the permissions that this role provides
            /// </summary>
            [YamlMember(Alias = "permissions")]
            [UsedImplicitly(ImplicitUseKindFlags.Assign)] // YAML Deserialization
            [NotNull]
            public IList<string> Permissions { get; internal set; } = new List<string>();

            /// <summary>
            ///     Gets the name of a role to inherit permissions from
            /// </summary>
            [YamlMember(Alias = "inherit")]
            [UsedImplicitly(ImplicitUseKindFlags.Assign)] // YAML Deserialization
            [CanBeNull]
            public string Inherit { get; internal set; }
        }
    }
}
