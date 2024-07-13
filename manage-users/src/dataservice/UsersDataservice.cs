using System;
using System.ComponentModel.DataAnnotations;
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
            _conx = _configuration["ProjectBLocalConnection"];
            if (_conx.IsNullOrEmpty())
                _conx = _configuration.GetConnectionString("ProjectBLocalConnection");
        }

        public async Task<User> GetUser(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(_conx))
            {
                string query = $"CALL ProjectB.UserGetById(@paramUserId)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
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
            using (MySqlConnection connection = new MySqlConnection(_conx))
            {
                string query = $"CALL ProjectB.UserGetByEmail(@paramEmail)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
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
            using (MySqlConnection connection = new MySqlConnection(_conx))
            {
                string query = $"CALL ProjectB.UserGetList()";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
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
            using (MySqlConnection connection = new MySqlConnection(_conx))
            {
                string query = $"CALL ProjectB.UserPersist(@paramUserId, @paramFirstName, @paramLastName)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@paramUserId", createUserRequest.Email);
                    command.Parameters.AddWithValue("@paramFirstName", createUserRequest.FirstName);
                    command.Parameters.AddWithValue("@paramLastName", createUserRequest.LastName);

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

        public async void UpdateUser(UpdateUser updateUserRequest)
        {
            using (MySqlConnection connection = new MySqlConnection(_conx))
            {
                string query = $"CALL ProjectB.UserUpdateByUserId(@paramUserId, @paramEmail, @paramFirstName, @paramLastName, @paramUpdateUserId)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
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
            using (MySqlConnection connection = new MySqlConnection(_conx))
            {
                string query = $"CALL ProjectB.UserDelete(@paramUserId, @paramUpdateUserId)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
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

            return new User()
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