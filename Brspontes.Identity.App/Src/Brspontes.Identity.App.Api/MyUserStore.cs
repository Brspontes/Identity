using Brspontes.Identity.App.Api.Config;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Brspontes.Identity.App.Api
{
    public class MyUserStore : IUserStore<MyUser>, IUserPasswordStore<MyUser>
    {
        private readonly IOptions<ConnectionString> options;

        public MyUserStore(IOptions<ConnectionString> options)
        {
            this.options = options;
        }

        public DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(options.Value.DefaultConnection);
            connection.Open();
            return connection;
        }

        public async Task<IdentityResult> CreateAsync(MyUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync(
                    "INSERT INTO Users(" +
                    "[Id], [UserName], [NormalizerUserName], [PasswordHash]) VALUES(" +
                    "@Id, @UserName, @NormalizedUserName, @PasswordHash)", new
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    NormalizedName = user.NormilezedUserName,
                    PasswordHash = user.PasswordHash
                });
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(MyUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new
                {
                    Id = user.Id,
                });
            }

            return IdentityResult.Success;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<MyUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<MyUser>("SELECT * FROM Users WHERE Id = @Id", new { Id = userId });
            }
        }

        public async Task<MyUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<MyUser>("SELECT * FROM Users WHERE NormalizedUser = @normalizedUserName", new { normalizedUserName = normalizedUserName });
            }
        }

        public Task<string> GetNormalizedUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormilezedUserName);
        }

        public Task<string> GetUserIdAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(MyUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormilezedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(MyUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(MyUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync(
                    "UPDATE Users " +
                    "set [Id] = @Id, [UserName] = @UserName," +
                    "[NormalizedUserName] = @NormalizedUserName," +
                    "[PasswordHash] = @PasswordHash" +
                    "Where [Id] = @Id", new
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    NormalizedName = user.NormilezedUserName,
                    PasswordHash = user.PasswordHash
                });
            }

            return IdentityResult.Success;
        }

        public Task SetPasswordHashAsync(MyUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }
    }
}
