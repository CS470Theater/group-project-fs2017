using System;
using System.Diagnostics;
using TheaterBooking.Core.Users.Permissions;
using JetBrains.Annotations;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TheaterBooking.Core.Users.Claims
{
    /// <summary>
    ///     Provides custom claims on the claims identity created for the current user
    /// </summary>
    [UsedImplicitly]
    internal class ClaimsIdentityFactory : ClaimsIdentityFactory<User, string>
    {
        private readonly HttpContext _httpContext;
        private readonly IPermissionsModel _permissionsModel;

        /// <summary>
        ///     Instantiates a new claims identity factory
        /// </summary>
        /// <param name="permissionsModel">The permissions model to use</param>
        public ClaimsIdentityFactory(IPermissionsModel permissionsModel)
        {
            _httpContext = HttpContext.Current;
            _permissionsModel = permissionsModel;
        }

        /// <summary>
        ///     Create a ClaimsIdentity from a user
        /// </summary>
        /// <param name="manager">The user manager to use</param>
        /// <param name="user">The user to create the claims identity for</param>
        /// <param name="authenticationType">The type of authentication being performed</param>
        /// <returns></returns>
        public override Task<ClaimsIdentity> CreateAsync([NotNull] UserManager<User, string> manager, [NotNull] User user,
            string authenticationType)
        {
            if (HttpContext.Current == null)
            {
                var executionContext = ExecutionContext.Capture();
                var taskCompletionSource = new TaskCompletionSource<Task<ClaimsIdentity>>();
                ExecutionContext.Run(executionContext, state =>
                {
                    HttpContext.Current = _httpContext;
                    taskCompletionSource.SetResult(Run());
                }, null);
                return taskCompletionSource.Task.Result;
            }

            Debug.Assert(_httpContext == HttpContext.Current, "_httpContext == HttpContext.Current");
            return Run();

            async Task<ClaimsIdentity> Run()
            {
                var displayName = user.Email;
                var identity = await base.CreateAsync(manager, user, authenticationType);
                identity.AddClaim(new Claim(ClaimTypes.GivenName, displayName));
                identity.AddClaim(new Claim(ClaimTypes.Hash, GenerateRandomString()));

                foreach (var permission in _permissionsModel["Default"])
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, permission));
                }

                foreach (var role in await manager.GetRolesAsync(user.Id))
                {
                    identity.RemoveClaim(identity.Claims.First(claim => claim.Type == ClaimTypes.Role && claim.Value == role));
                    foreach (var permission in _permissionsModel[role])
                    {
                        if (!identity.HasClaim(ClaimTypes.Role, permission))
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, permission));
                        }
                    }
                }

                return identity;
            }
        }

        /// <summary>
        ///     Generates a new random string
        /// </summary>
        /// <returns>A new random string</returns>
        private static string GenerateRandomString()
        {
            using (var rnd = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[17];
                rnd.GetBytes(buffer);
                return Convert.ToBase64String(buffer).Substring(0, 22);
            }
        }
    }
}
