using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DogGo.Models;

namespace DogGo.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public DogRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Dog> GetAllDogs()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Dog.Id, Dog.[Name], OwnerId, Owner.Name AS OwnerName, Breed, ISNULL(Notes, 'N/A') AS Notes, ISNULL(ImageUrl, 'N/A') AS ImageUrl
                        FROM Dog LEFT JOIN Owner ON Dog.OwnerId = Owner.Id
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Dog> dogs = new List<Dog>();
                    while (reader.Read())
                    {
                        Dog dog = new Dog
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"))
                        };

                        dogs.Add(dog);
                    }

                    reader.Close();

                    return dogs;
                }
            }
        }

        public Dog GetDogById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Dog.Id, Dog.[Name], OwnerId, Owner.Name AS OwnerName, Breed, ISNULL(Notes, 'N/A') AS Notes, ISNULL(ImageUrl, 'N/A') AS ImageUrl
                        FROM Dog LEFT JOIN Owner ON Dog.OwnerId = Owner.Id
                        WHERE Dog.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Dog dog = new Dog
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"))
                        };

                        reader.Close();
                        return dog;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }

        public List<Dog> GetDogsByOwnerId(int ownerId)
{
    using (SqlConnection conn = Connection)
    {
        conn.Open();

        using (SqlCommand cmd = conn.CreateCommand())
        {
            cmd.CommandText = @"
                SELECT Id, Name, Breed, Notes, ImageUrl, OwnerId 
                FROM Dog
                WHERE OwnerId = @ownerId
            ";

            cmd.Parameters.AddWithValue("@ownerId", ownerId);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Dog> dogs = new List<Dog>();

            while (reader.Read())
            {
                Dog dog = new Dog()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Breed = reader.GetString(reader.GetOrdinal("Breed")),
                    OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId"))
                };

                // Check if optional columns are null
                if (reader.IsDBNull(reader.GetOrdinal("Notes")) == false)
                {
                    dog.Notes = reader.GetString(reader.GetOrdinal("Notes"));
                }
                if (reader.IsDBNull(reader.GetOrdinal("ImageUrl")) == false)
                {
                    dog.ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"));
                }

                dogs.Add(dog);
            }
            reader.Close();
            return dogs;
        }
    }
}

    }
}
