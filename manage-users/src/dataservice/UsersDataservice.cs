using System;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

public class UsersDataservice : IUsersDataservice
{
    private IConfiguration _configuration;
    
    public UsersDataservice(IConfiguration configuration)
    {
         _configuration = configuration;
    }

    public async Task<User> GetUser(int userId)
    {
        var connectionString = _configuration.GetConnectionString("ProjectBLocalConnection");

        using (MySqlConnection connection = new MySqlConnection(connectionString))
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
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
        }
    }

    public async Task<UserList> GetUsers()
    {
        var connectionString = _configuration.GetConnectionString("ProjectBLocalConnection");

        using (MySqlConnection connection = new MySqlConnection(connectionString))
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
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
        }
    }
    
    public async void CreateUser(CreateUser createUserRequest)
    {
        var connectionString = _configuration.GetConnectionString("ProjectBLocalConnection");

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = $"CALL ProjectB.UserPersist(@paramUserId, @paramCreateUserId)";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@paramUserId", createUserRequest.UserEmail);
                command.Parameters.AddWithValue("@paramCreateUserId", createUserRequest.CreateUserId);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
        }
    }

    public async void UpdateUser(UpdateUser updateUserRequest)
    {
        
        var connectionString = _configuration.GetConnectionString("ProjectBLocalConnection");

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = $"CALL ProjectB.UserUpdateByUserId(@paramUserId, @paramUserEmail, @paramUpdateUserId)";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@paramUserId", updateUserRequest.UserId);
                command.Parameters.AddWithValue("@paramUserEmail", updateUserRequest.UserEmail);
                command.Parameters.AddWithValue("@paramUpdateUserId", updateUserRequest.UpdateUserId);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
        }
    }

    public async void DeleteUser(int userId, int updateUserId)
    {
        var connectionString = _configuration.GetConnectionString("ProjectBLocalConnection");

        using (MySqlConnection connection = new MySqlConnection(connectionString))
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
                catch (System.Exception ex)
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
        DateTime createDatetime = reader.GetDateTime("CreateDatetime"); 
        int createUserId = reader.GetInt32("CreateUserId"); 
        DateTime updateDatetime = reader.GetDateTime("UpdateDatetime"); 
        int updateUserId = reader.GetInt32("UpdateUserId");                             

        return new User()
        {
            UserId = id,
            UserEmail = email,
            CreateDatetime = createDatetime,
            CreateUserId = createUserId,
            UpdateDatetime = updateDatetime,
            UpdateUserId = updateUserId
        };
    }

    #endregion
}