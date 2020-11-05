using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dualp.Common.Logger;
using Dualp.Customs.Data.ADO.Mappings;
using Dualp.Customs.Data.IdentityEntities;
using Microsoft.AspNet.Identity;
using Dualp.SQLHelper;

namespace Dualp.Customs.Data.ADO.Identities
{
    public class RoleStore : IRoleStore<IdentityRole, Guid>
    {
        private SqlConnection _sqlConnection;
        private bool _isopen;

        public RoleStore(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public void Dispose()
        {
            _sqlConnection?.Dispose();
        }

        public async Task CreateAsync(IdentityRole role)
        {

            try
            {

                _isopen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@roleId", role.Id),
                    new KeyValuePair<string, object>("@name", role.Name)
                };

                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[CreateRole]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isopen);
            }

            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isopen);
                throw ex;
            }

        }

        public async Task UpdateAsync(IdentityRole role)
        {
            try
            {

                _isopen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@roleId", role.Id),
                    new KeyValuePair<string, object>("@name", role.Name),
                };

                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[UpdateRole]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isopen);
            }

            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isopen);
                throw ex;
            }
        }

        public async Task DeleteAsync(IdentityRole role)
        {
            try
            {

                _isopen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param =
                    new List<KeyValuePair<string, object>> {new KeyValuePair<string, object>("@roleId", role.Id)};

                await SQLHelper.SQLHelper.ExecuteNonQueryAsync(_sqlConnection, "[dbo].[DeleteRole]", param);

                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isopen);
            }

            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isopen);
                throw ex;
            }
        }

        public async Task<IdentityRole> FindByIdAsync(Guid roleId)
        {
            try
            {

                _isopen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);


                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@roleId", roleId),
                    new KeyValuePair<string, object>("@rolename", null),
                };

                var sqlread =
                    await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetRole]", param);

                IdentityRole role = null;

                while (await sqlread.ReadAsync())
                {
                    role = sqlread.ToIdentityRole();
                }


                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isopen);

                return role;
            }

            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isopen);
                throw ex;
            }
        }

        public async Task<IdentityRole> FindByNameAsync(string roleName)
        {
            try
            {

                _isopen = await SQLHelper.SQLHelper.OpenConnectionAsync(_sqlConnection);

                var sqlcomm =
                    new SqlCommand("[dbo].[GetRole]", _sqlConnection) {CommandType = CommandType.StoredProcedure};
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@roleId", null),
                    new KeyValuePair<string, object>("@rolename", roleName)
                };

                var sqlread =
                    await SQLHelper.SQLHelper.ExecuteAsDataReaderAsync(_sqlConnection, "[dbo].[GetRole]", param);

                IdentityRole role = null;

                while (await sqlread.ReadAsync())
                {
                    role = sqlread.ToIdentityRole();
                }


                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isopen);

                return role;
            }

            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isopen);
                throw ex;
            }
        }

        public IQueryable<IdentityRole> Roles
        {
            get
            {
                try
                {

                    _isopen = SQLHelper.SQLHelper.OpenConnection(_sqlConnection);

                    var sqlcomm =
                        new SqlCommand("[dbo].[GetRole]", _sqlConnection) {CommandType = CommandType.StoredProcedure};
                    List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>("@roleId", null),
                        new KeyValuePair<string, object>("@rolename", null)
                    };

                    var sqlread = SQLHelper.SQLHelper.ExecuteAsDataReader(_sqlConnection, "[dbo].[GetRoles]", param);

                    var roles = new List<IdentityRole>();

                    while (sqlread.Read())
                    {
                        roles.Add(sqlread.ToIdentityRole());
                    }


                    SQLHelper.SQLHelper.CloseConnection(_sqlConnection, _isopen);

                    return roles.AsQueryable();
                }
                catch (Exception e)
                {
                    this.Log().Fatal(e.Message);
                    throw e;
                }
            }
        }
    }
}
