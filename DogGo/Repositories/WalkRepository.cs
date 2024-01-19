using DogGo.Models;
using Microsoft.Data.SqlClient;


namespace DogGo.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalkRepository(IConfiguration config)
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

        public List<Walk> GetAllWalks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          Select w.Id, w.Date, w.Duration, w.WalkerId, w.DogId, wa.Id AS ""Walker Id"", wa.ImageUrl, wa.Name AS ""Walker Name"", wa.NeighborhoodId, d.Breed, d.Id as ""Dog Id"", d.Name AS ""Dog Name"", d.OwnerId AS ""Dog Owner ID""
                            FROM Walks w
                            LEFT JOIN Walker wa on w.WalkerId = wa.Id
                            LEFT JOIN Dog d on w.DogId = d.Id;
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walk> walks = new List<Walk>();

                    while (reader.Read())
                    {
                        Walk walk = new Walk
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            Walker = new Walker
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Walker Id")),
                                Name = reader.GetString(reader.GetOrdinal("Walker Name")),
                            },
                            Dog = new Dog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Dog Id")),
                                Name = reader.GetString(reader.GetOrdinal("Dog Name")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("Dog Owner ID")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            }

                        };

                        walks.Add(walk);
                    }

                    reader.Close();

                    return walks;
                }
            }
        }

        public Walk GetWalkById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          Select w.Id, w.Date, w.Duration, w.WalkerId, w.DogId, wa.Id AS ""Walker Id"", wa.ImageUrl, wa.Name AS ""Walker Name"", wa.NeighborhoodId, d.Breed, d.Id as ""Dog Id"", d.Name AS ""Dog Name"", d.OwnerId AS ""Dog Owner ID""
                            FROM Walks w
                            LEFT JOIN Walker wa on w.WalkerId = wa.Id
                            LEFT JOIN Dog d on w.DogId = d.Id
                            WHERE w.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Walk walk = new Walk
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            Walker = new Walker
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Walker Id")),
                                Name = reader.GetString(reader.GetOrdinal("Walker Name")),
                            },
                            Dog = new Dog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Dog Id")),
                                Name = reader.GetString(reader.GetOrdinal("Dog Name")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("Dog Owner ID")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            }
                        };

                        reader.Close();
                        return walk;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }

        public List<Walk> GetWalksByWalkerId(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                 Select w.Id, w.Date, w.Duration, w.WalkerId, w.DogId, wa.Id AS ""Walker Id"", wa.ImageUrl, wa.Name AS ""Walker Name"", wa.NeighborhoodId, d.Breed, d.Id as ""Dog Id"", d.Name AS ""Dog Name"", d.OwnerId AS ""Dog Owner ID""
                            FROM Walks w
                            LEFT JOIN Walker wa on w.WalkerId = wa.Id
                            LEFT JOIN Dog d on w.DogId = d.Id
                            WHERE w.Id = @walkerId; ";

                    cmd.Parameters.AddWithValue("@walkerId", walkerId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walk> walks = new List<Walk>();

                    while (reader.Read())
                    {
                        Walk walk = new Walk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            Walker = new Walker
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Walker Id")),
                                Name = reader.GetString(reader.GetOrdinal("Walker Name")),
                            },
                            Dog = new Dog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Dog Id")),
                                Name = reader.GetString(reader.GetOrdinal("Dog Name")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("Dog Owner ID")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            }
                        };



                        walks.Add(walk);
                    }
                    reader.Close();
                    return walks;
                }
            }
        }

        public void AddWalk(Walk walk)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Walk (Date, Duration, WalkerId, DogId)
                    OUTPUT INSERTED.ID
                    VALUES (@date, @duration, @walkerId, @dogId);
                ";

                    cmd.Parameters.AddWithValue("@date", walk.Date);
                    cmd.Parameters.AddWithValue("@duration", walk.Duration);
                    cmd.Parameters.AddWithValue("@walkerId", walk.WalkerId);
                    cmd.Parameters.AddWithValue("@dogId", walk.DogId);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();

                    walk.Id = newlyCreatedId;
                }
            }
        }

        public void UpdateWalk(Walk walk)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Walk
                            SET 
                                Date = @date,
                                Duration = @duration,
                                WalkerId = @walkerId,
                                DogId = @dogId,
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@date", walk.Date);
                    cmd.Parameters.AddWithValue("@duration", walk.Duration);
                    cmd.Parameters.AddWithValue("@walkerId", walk.WalkerId);
                    cmd.Parameters.AddWithValue("@dogId", walk.DogId);

                    cmd.ExecuteNonQuery();


                }
            }
        }

        public void DeleteWalk(int walkId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Walk
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", walkId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}