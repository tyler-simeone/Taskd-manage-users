using System.Data;
using manage_users.src.models;
using manage_users.src.models.requests;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;

namespace manage_users.src.dataservice
{
    public class UsersDataservice : IUsersDataservice
    {
        private IConfiguration _configuration;
        private string _conx;

        public UsersDataservice(IConfiguration configuration)
        {
            _configuration = configuration;
            _conx = _configuration["LocalDBConnection"];
            
            if (_conx.IsNullOrEmpty())
                _conx = _configuration.GetConnectionString("LocalDBConnection");

            Console.WriteLine($"DB Connection: {_conx}");
        }

        public async Task<User> GetUser(int userId)
        {
            using (MySqlConnection connection = new(_conx))
            {
                using (MySqlCommand command = new("taskd_db_dev.UserGetById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@paramUserId", userId);

                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return ExtractUserFromReader(reader);
                            }

                            return new User();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw;
                    }
                }
            }
        }
        
        public async Task<User> GetUser(string email)
        {
            using (MySqlConnection connection = new(_conx))
            {
                using (MySqlCommand command = new("taskd_db_dev.UserGetByEmail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@paramEmail", email);

                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return ExtractUserFromReader(reader);
                            }

                            return new User();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public async Task<UserList> GetUsers()
        {
            using (MySqlConnection connection = new(_conx))
            {
                using (MySqlCommand command = new("taskd_db_dev.UserGetList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            var userList = new UserList();

                            while (reader.Read())
                            {
                                User user = ExtractUserFromReader(reader);
                                userList.Users.Add(user);
                            }

                            return userList;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public async void CreateUser(CreateUser createUserRequest)
        {
            try
            {
                using MySqlConnection connection = new(_conx);
                using MySqlCommand command = new("taskd_db_dev.UserPersist", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@paramEmail", createUserRequest.Email);
                command.Parameters.AddWithValue("@paramFirstName", createUserRequest.FirstName);
                command.Parameters.AddWithValue("@paramLastName", createUserRequest.LastName);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async void UpdateUser(UpdateUser updateUserRequest)
        {
            using (MySqlConnection connection = new(_conx))
            {
                using (MySqlCommand command = new("taskd_db_dev.UserUpdateByUserId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@paramUserId", updateUserRequest.UserId);
                    command.Parameters.AddWithValue("@paramEmail", updateUserRequest.Email);
                    command.Parameters.AddWithValue("@paramFirstName", updateUserRequest.FirstName);
                    command.Parameters.AddWithValue("@paramLastName", updateUserRequest.LastName);
                    command.Parameters.AddWithValue("@paramUpdateUserId", updateUserRequest.UpdateUserId);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public async void DeleteUser(int userId, int updateUserId)
        {
            using (MySqlConnection connection = new(_conx))
            {
                using (MySqlCommand command = new("taskd_db_dev.UserDelete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@paramUserId", userId);
                    command.Parameters.AddWithValue("@paramUpdateUserId", updateUserId);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        #region HELPERS

        private User ExtractUserFromReader(MySqlDataReader reader)
        {
            int id = reader.GetInt32("UserId");
            string email = reader.GetString("Email");
            string firstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? String.Empty : reader.GetString("FirstName");
            string lastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? String.Empty : reader.GetString("LastName");
            DateTime createDatetime = reader.GetDateTime("CreateDatetime");
            DateTime? updateDatetime = reader.IsDBNull(reader.GetOrdinal("UpdateDatetime")) ? null : reader.GetDateTime("UpdateDatetime");
            int? updateUserId = reader.IsDBNull(reader.GetOrdinal("UpdateUserId")) ? null : reader.GetInt32("UpdateUserId");

            return new User
            {
                UserId = id,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                CreateDatetime = createDatetime,
                UpdateDatetime = updateDatetime,
                UpdateUserId = updateUserId
            };
        }

        #endregion
    }
}