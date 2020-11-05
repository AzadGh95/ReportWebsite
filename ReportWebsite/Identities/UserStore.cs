using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dualp.Common.Logger;
using Dualp.Customs.Data.ADO.Mappings;
using Dualp.Customs.Data.IdentityEntities;
using Microsoft.AspNet.Identity;

namespace Dualp.Customs.Data.ADO.Identities
{
    /// <summary>
    ///     EntityFramework based user store implementation that supports IUserStore, IUserLoginStore, IUserClaimStore and
    ///     IUserRoleStore
    /// </summary>
    /// <typeparam name="IdentityUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUserLogin"></typeparam>
    /// <typeparam name="TUserRole"></typeparam>
    /// <typeparam name="TUserClaim"></typeparam>
    public class UserStore : IUserStore<IdentityUser, Guid>, IUserPasswordStore<IdentityUser, Guid>, IUserClaimStore<IdentityUser, Guid>, IUserRoleStore<IdentityUser,Guid>

    {
        //        private readonly IDbSet<TUserLogin> _logins;
        //        private readonly EntityStore<TRole> _roleStore;
        //        private readonly IDbSet<TUserClaim> _userClaims;
        //        private readonly IDbSet<TUserRole> _userRoles;
        //        private bool _disposed;
        //        private EntityStore<IdentityUser> _userStore;

        private readonly SqlConnection _sqlConnection;
        private bool _isOpen;

        /// <summary>
        ///     Constructor which takes a db context and wires up the stores with default instances using the context
        /// </summary>
        /// <param name="context"></param>
        public UserStore(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }


        /// <summary>
        ///     Return the claims for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@UserName", (user.Id == null ? user.UserName : null))
                };


                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetClaims]", param);

                List<Claim> claims = new List<Claim>();

                while (await sqlreader.ReadAsync())
                {
                    claims.Add(sqlreader.ToClaim());
                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return claims;


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Add a claim to a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public virtual async Task AddClaimAsync(IdentityUser user, Claim claim)
        {

            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@claimType", claim.Type),
                    new KeyValuePair<string, object>("@claimValue", claim.Value)
                };


                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[AddClaim]", param);


                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }

        }

        /// <summary>
        ///     Remove a claim from a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public virtual async Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@claimType", claim.Type),
                    new KeyValuePair<string, object>("@claimValue", claim.Value)
                };


                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[RemoveClaim]", param);


                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Returns whether the user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };


                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetEmailConfirmed]", param);

                bool emailconfirmed = false;

                while (await sqlreader.ReadAsync())
                {
                    emailconfirmed = (bool)sqlreader[0];
                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return emailconfirmed;


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Set IsConfirmed on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public virtual async Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@confirmed", confirmed)
                };



                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[SetEmailConfirmed]", param);


                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Set the user email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual async Task SetEmailAsync(IdentityUser user, string email)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@email", email)
                };



                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[SetEmail]", param);


                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Get the user's email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<string> GetEmailAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };


                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetEmail]", param);

                string email = string.Empty;

                while (await sqlreader.ReadAsync())
                {
                    email = sqlreader[0].ToString();
                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return email;


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Find a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual async Task<IdentityUser> FindByEmailAsync(string email)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@email", email) };


                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetEmail]", param);



                IdentityUser user = null;

                while (await sqlreader.ReadAsync())
                {
                    user = sqlreader.ToIdentityUser();
                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return user;


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Returns the DateTimeOffset that represents the end of a user's lockout, any time in the past should be considered
        ///     not locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<DateTimeOffset> GetLockoutEndDateAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetLockoutEndDate]", param);

                var datetime = new DateTimeOffset();

                while (await sqlreader.ReadAsync())
                {
                    var temp = (sqlreader["LockoutEndDateUtc"] == DBNull.Value
                        ? null
                        : (DateTime?)sqlreader["LockoutEndDateUtc"]);
                    if (temp != null)
                        datetime = new DateTimeOffset(DateTime.SpecifyKind((DateTime)temp, DateTimeKind.Utc));

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return datetime;


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }

        }

        /// <summary>
        ///     Locks a user out until the specified end date (set to a past date, to unlock a user)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lockoutEnd"></param>
        /// <returns></returns>
        public virtual async Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset lockoutEnd)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@LockoutEndDateUtc",
                        lockoutEnd == DateTimeOffset.MinValue ? (DateTime?) null : lockoutEnd.UtcDateTime)
                };



                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[SetLockoutEndDate]", param);


                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }

        }

        /// <summary>
        ///     Used to record when an attempt to access the user has failed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<int> IncrementAccessFailedCountAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };

                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[IncrementAccessFailedCount]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);


                return user.AccessFailedCount++;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Used to reset the account access count, typically after the account is successfully accessed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task ResetAccessFailedCountAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };

                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[ResetAccessFailedCount]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                user.AccessFailedCount = 0;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Returns the current number of failed access attempts.  This number usually will be reset whenever the password is
        ///     verified or the account is locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<int> GetAccessFailedCountAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);



                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetAccessFailedCount]", param);

                int access = 0;

                while (await sqlreader.ReadAsync())
                {
                    access = (int)sqlreader["AccessFailedCount"];

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return access;


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Returns whether the user can be locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetLockoutEnabledAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);



                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetLockoutEnabled]", param);

                bool access = false;

                while (await sqlreader.ReadAsync())
                {
                    access = (bool)sqlreader["LockoutEnabled"];

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return access;


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Sets whether the user can be locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public virtual async Task SetLockoutEnabledAsync(IdentityUser user, bool enabled)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@enablelock", enabled)
                };

                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[SetLockoutEnabled]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Find a user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<IdentityUser> FindByIdAsync(Guid userId)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);



                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", userId) };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetUserById]", param);

                IdentityUser user = null;

                while (await sqlreader.ReadAsync())
                {
                    user = sqlreader.ToIdentityUser();

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return user;


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }



        /// <summary>
        ///     Find a user by name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual async Task<IdentityUser> FindByNameAsync(string userName)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);



                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userName", userName) };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[FindByName]", param);

                IdentityUser user = null;

                while (await sqlreader.ReadAsync())
                {
                    user = sqlreader.ToIdentityUser();

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return user;


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Insert an entity
        /// </summary>
        /// <param name="user"></param>
        public virtual async Task CreateAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@Id", user.Id),
                    new KeyValuePair<string, object>("@FirstName", user.FirstName),
                    new KeyValuePair<string, object>("@LastName", user.LastName),
                    new KeyValuePair<string, object>("@BithDate", user.BithDate),
                    new KeyValuePair<string, object>("@CreateDate", user.CreateDate),
                    new KeyValuePair<string, object>("@ModifyDate", user.ModifyDate),
                    new KeyValuePair<string, object>("@Phone", user.Phone),
                    new KeyValuePair<string, object>("@Mobile", user.Mobile),
                    new KeyValuePair<string, object>("@PostalAddress", user.PostalAddress),
                    new KeyValuePair<string, object>("@Country", user.Country),
                    new KeyValuePair<string, object>("@City", user.City),
                    new KeyValuePair<string, object>("@StateOrProvince", user.StateOrProvince),
                    new KeyValuePair<string, object>("@PostalCode", user.PostalCode),
                    new KeyValuePair<string, object>("@ProfileImage", user.ProfileImage),
                    new KeyValuePair<string, object>("@Language", user.Language),
                    new KeyValuePair<string, object>("@TimeZone", user.TimeZone),
                    new KeyValuePair<string, object>("@Email", user.Email),
                    new KeyValuePair<string, object>("@EmailConfirmed", user.EmailConfirmed),
                    new KeyValuePair<string, object>("@PasswordHash", user.PasswordHash),
                    new KeyValuePair<string, object>("@SecurityStamp", user.SecurityStamp),
                    new KeyValuePair<string, object>("@PhoneNumber", user.PhoneNumber),
                    new KeyValuePair<string, object>("@PhoneNumberConfirmed", user.PhoneNumberConfirmed),
                    new KeyValuePair<string, object>("@TwoFactorEnabled", user.TwoFactorEnabled),
                    new KeyValuePair<string, object>("@LockoutEndDateUtc", user.LockoutEndDateUtc),
                    new KeyValuePair<string, object>("@LockoutEnabled", user.LockoutEnabled),
                    new KeyValuePair<string, object>("@AccessFailedCount", user.AccessFailedCount),
                    new KeyValuePair<string, object>("@UserName", user.UserName)
                };


                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[CreateUser]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Mark an entity for deletion
        /// </summary>
        /// <param name="user"></param>
        public virtual async Task DeleteAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@Id", user.Id),
                    new KeyValuePair<string, object>("@UserName", user.UserName)
                };


                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[DeleteUser]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Update an entity
        /// </summary>
        /// <param name="user"></param>
        public virtual async Task UpdateAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@Id", user.Id),
                    new KeyValuePair<string, object>("@FirstName", user.FirstName),
                    new KeyValuePair<string, object>("@LastName", user.LastName),
                    new KeyValuePair<string, object>("@BithDate", user.BithDate),
                    new KeyValuePair<string, object>("@CreateDate", user.CreateDate),
                    new KeyValuePair<string, object>("@ModifyDate", user.ModifyDate),
                    new KeyValuePair<string, object>("@Phone", user.Phone),
                    new KeyValuePair<string, object>("@Mobile", user.Mobile),
                    new KeyValuePair<string, object>("@PostalAddress", user.PostalAddress),
                    new KeyValuePair<string, object>("@Country", user.Country),
                    new KeyValuePair<string, object>("@City", user.City),
                    new KeyValuePair<string, object>("@StateOrProvince", user.StateOrProvince),
                    new KeyValuePair<string, object>("@PostalCode", user.PostalCode),
                    new KeyValuePair<string, object>("@ProfileImage", user.ProfileImage),
                    new KeyValuePair<string, object>("@Language", user.Language),
                    new KeyValuePair<string, object>("@TimeZone", user.TimeZone),
                    new KeyValuePair<string, object>("@Email", user.Email),
                    new KeyValuePair<string, object>("@EmailConfirmed", user.EmailConfirmed),
                    new KeyValuePair<string, object>("@PasswordHash", user.PasswordHash),
                    new KeyValuePair<string, object>("@SecurityStamp", user.SecurityStamp),
                    new KeyValuePair<string, object>("@PhoneNumber", user.PhoneNumber),
                    new KeyValuePair<string, object>("@PhoneNumberConfirmed", user.PhoneNumberConfirmed),
                    new KeyValuePair<string, object>("@TwoFactorEnabled", user.TwoFactorEnabled),
                    new KeyValuePair<string, object>("@LockoutEndDateUtc", user.LockoutEndDateUtc),
                    new KeyValuePair<string, object>("@LockoutEnabled", user.LockoutEnabled),
                    new KeyValuePair<string, object>("@AccessFailedCount", user.AccessFailedCount),
                    new KeyValuePair<string, object>("@UserName", user.UserName)
                };


                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[UpdateUser]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Dispose the store
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // IUserLogin implementation

        /// <summary>
        ///     Returns the user associated with this login
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IdentityUser> FindAsync(UserLoginInfo login)
        {

            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@LoginProvider", login.LoginProvider),
                    new KeyValuePair<string, object>("@ProviderKey", login.ProviderKey)
                };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[FindUser]", param);

                IdentityUser user = null;

                while (await sqlreader.ReadAsync())
                {
                    user = sqlreader.ToIdentityUser();

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return user;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Add a login to the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public virtual async Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);

                var sqlcomm =
                    new SqlCommand("[dbo].[AddLogin]", _sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@LoginProvider", login.LoginProvider),
                    new KeyValuePair<string, object>("@ProviderKey", login.ProviderKey),
                    new KeyValuePair<string, object>("@userId", user.Id)
                };



                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[AddLogin]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Remove a login from a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public virtual async Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@LoginProvider", login.LoginProvider),
                    new KeyValuePair<string, object>("@ProviderKey", login.ProviderKey),
                    new KeyValuePair<string, object>("@userId", user.Id)
                };



                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[RemoveLogin]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Get the logins for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);

                var sqlcomm = new SqlCommand("[dbo].[GetLogins]", _sqlConnection)
                { CommandType = CommandType.StoredProcedure };
                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetLogins]", param);

                List<UserLoginInfo> logins = new List<UserLoginInfo>();

                while (await sqlreader.ReadAsync())
                {
                    logins.Add(sqlreader.ToUserLoginInfo());

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return logins;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Set the password hash for a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public virtual async Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@passwordHash", passwordHash)
                };



                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[SetPasswordHash]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                user.PasswordHash = passwordHash;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Get the password hash for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetPasswordHash]", param);

                string passwordHash = String.Empty;

                while (await sqlreader.ReadAsync())
                {
                    passwordHash = sqlreader["PasswordHash"].ToString();

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return passwordHash;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Returns true if the user has a password set
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<bool> HasPasswordAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);



                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetPasswordHash]", param);

                string passwordHash = String.Empty;

                while (await sqlreader.ReadAsync())
                {
                    passwordHash = sqlreader["PasswordHash"].ToString();

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return !string.IsNullOrWhiteSpace(passwordHash);

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Set the user's phone number
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public virtual async Task SetPhoneNumberAsync(IdentityUser user, string phoneNumber)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);



                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@phoneNumber", phoneNumber)
                };



                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[SetPhoneNumber]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                user.PhoneNumber = phoneNumber;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Get a user's phone number
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<string> GetPhoneNumberAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);



                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetPhoneNumber]", param);

                string phoneNumber = String.Empty;

                while (await sqlreader.ReadAsync())
                {
                    phoneNumber = sqlreader["PhoneNumber"].ToString();

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return phoneNumber;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Returns whether the user phoneNumber is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetPhoneNumberConfirmedAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);



                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetPhoneNumber]", param);

                bool phoneNumberConfirmed = false;

                while (await sqlreader.ReadAsync())
                {
                    phoneNumberConfirmed = (bool)sqlreader["PhoneNumberConfirmed"];

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return phoneNumberConfirmed;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Set PhoneNumberConfirmed on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public virtual async Task SetPhoneNumberConfirmedAsync(IdentityUser user, bool confirmed)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@confirmed", confirmed)
                };



                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[SetPhoneNumberConfirmed]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                user.PhoneNumberConfirmed = confirmed;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Add a user to a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual async Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);




                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@roleName", roleName)
                };



                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[AddUserRoles]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Remove a user from a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual async Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);



                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@roleName", roleName)
                };



                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[RemoveUserRole]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);


            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Get the names of the roles a user is a member of
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);




                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id)
                };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetUserRoles]", param);

                List<string> roles = new List<string>();

                while (await sqlreader.ReadAsync())
                {
                    roles.Add(sqlreader["Name"].ToString());

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return roles;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Returns true if the user is in the named role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@roleName", roleName)
                };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[IsUserInRole]", param);

                bool isrole = false;

                while (await sqlreader.ReadAsync())
                {
                    isrole = true;

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return isrole;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Returns true if the user is in the named role
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public  async Task<bool> IsInRoleAsync(string userName, string roleName)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userName", userName),
                    new KeyValuePair<string, object>("@roleName", roleName)
                };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[IsUserNameInRole]", param);

                bool isrole = false;

                while (await sqlreader.ReadAsync())
                {
                    isrole = true;

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return isrole;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Returns true if the user is in the named role
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool IsInRole(string userName, string roleName)
        {
            try
            {
                _isOpen =  SQLHelper.SQLHelper.OpenConnection(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userName", userName),
                    new KeyValuePair<string, object>("@roleName", roleName)
                };

                var sqlreader =  SQLHelper.SQLHelper.ExecuteAsDataReader(_sqlConnection, "[dbo].[IsUserNameInRole]", param);

                bool isrole = false;

                while ( sqlreader.Read())
                {
                    isrole = true;

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return isrole;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }
        /// <summary>
        ///     Set the security stamp for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stamp"></param>
        /// <returns></returns>
        public virtual async Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@TwoFactorEnabled", null),
                    new KeyValuePair<string, object>("@SecurityStamp", stamp)
                };



                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[SetSecurityStampAndTwoFactorEnabled]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                user.SecurityStamp = stamp;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Get the security stamp for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetSecurityStampAndTwoFactorEnabled]", param);

                string securityStamp = string.Empty;

                while (await sqlreader.ReadAsync())
                {
                    securityStamp = sqlreader["SecurityStamp"].ToString();

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return securityStamp;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }

        /// <summary>
        ///     Set whether two factor authentication is enabled for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public virtual async Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);



                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@userId", user.Id),
                    new KeyValuePair<string, object>("@TwoFactorEnabled", enabled),
                    new KeyValuePair<string, object>("@SecurityStamp", null)
                };



                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[SetSecurityStampAndTwoFactorEnabled]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                user.TwoFactorEnabled = enabled;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);
                throw e;
            }
        }

        /// <summary>
        ///     Gets whether two factor authentication is enabled for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetTwoFactorEnabledAsync(IdentityUser user)
        {
            try
            {
                _isOpen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);



                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@userId", user.Id) };

                var sqlreader = await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetSecurityStampAndTwoFactorEnabled]", param);

                bool twoFactorEnabled = false;

                while (await sqlreader.ReadAsync())
                {
                    twoFactorEnabled = (bool)sqlreader["TwoFactorEnabled"];

                }

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isOpen);

                return twoFactorEnabled;

            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw e;
            }
        }




        /// <summary>
        ///     If disposing, calls dispose on the Context.  Always nulls out the Context
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            _sqlConnection?.Dispose();
        }

        public IQueryable<IdentityUser> Users { get; }
    }
}
