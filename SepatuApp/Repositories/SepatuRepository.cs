using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SepatuApp.Models;

namespace SepatuApp.Repositories
{
    public class SepatuRepository
    {
        private readonly Database db = new Database();

        public List<Sepatu> GetAll()
        {
            var list = new List<Sepatu>();
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM sepatu";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Sepatu
                    {
                        Merk = reader["merk"].ToString(),
                        Warna = reader["warna"].ToString(),
                        Ukuran = reader["ukuran"].ToString(),
                        Stok = int.Parse(reader["stok"].ToString())
                    });
                }
            }
            return list;
        }

        public void Add(Sepatu sepatu)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO sepatu (merk, warna, ukuran, stok) VALUES (@merk, @warna, @ukuran, @stok)";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@merk", sepatu.Merk);
                cmd.Parameters.AddWithValue("@warna", sepatu.Warna);
                cmd.Parameters.AddWithValue("@ukuran", sepatu.Ukuran);
                cmd.Parameters.AddWithValue("@stok", sepatu.Stok);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(string oldMerk, Sepatu sepatu)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "UPDATE sepatu SET merk=@merk, warna=@warna, ukuran=@ukuran, stok=@stok WHERE merk=@oldMerk";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@merk", sepatu.Merk);
                cmd.Parameters.AddWithValue("@warna", sepatu.Warna);
                cmd.Parameters.AddWithValue("@ukuran", sepatu.Ukuran);
                cmd.Parameters.AddWithValue("@stok", sepatu.Stok);
                cmd.Parameters.AddWithValue("@oldMerk", oldMerk);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string merk)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM sepatu WHERE merk=@merk";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@merk", merk);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
