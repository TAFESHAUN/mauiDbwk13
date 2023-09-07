﻿using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace mauiDbwk13.Data
{
    public class Repository
    {
        #region Connection String


        private string _connectionString;
        public Repository()
        {
            //AZURE
            //_connectionString = "Data Source=mysqlserver1092837465.database.windows.net;Initial Catalog=StoreDb;User ID=azureuser;Password=MyAzure_92;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
           
            //SSMS
            _connectionString = @"Data Source=DESKTOP-TBL2MHJ;Initial Catalog=StoreDB;Integrated Security=True;Trust Server Certificate=True";
        } 
        #endregion

        #region Get Product
        public IEnumerable<Products> GetProduct()
        {
            var productList = new List<Products>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SELECT * FROM Products", connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var aProduct = new Products
                        {
                            Id = reader.GetInt32(0),
                            Product = reader.GetString(1),
                            Price = Convert.ToDouble(reader.GetDecimal(2)),
                            Code = reader.GetString(3).ToUpper()
                        };
                        productList.Add(aProduct);

                    }
                }
            }
            return productList;
        }
        #endregion

        #region Update Product
        public int UpdateProduct(Products p)
        {
            int result = 0;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                command.CommandText = "UPDATE Products SET Product=@Product,Price=@Price,Code=@Code WHERE Id = @id";
                command.Parameters.AddWithValue("@id", p.Id);
                command.Parameters.AddWithValue("@Product", p.Product);
                command.Parameters.AddWithValue("@Price", p.Price);
                command.Parameters.AddWithValue("@Code", p.Code);

                command.Connection = connection;
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            return result;
        }
        #endregion

        #region Insert Product
        public int InsertProduct(Products p)
        {
            int result = 0;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                command.CommandText = "INSERT INTO Products VALUES (@Product,@Price,@Code)";
                //command.Parameters.AddWithValue("@id", p.Id);
                command.Parameters.AddWithValue("@Product", p.Product);
                command.Parameters.AddWithValue("@Price", p.Price);
                command.Parameters.AddWithValue("@Code", p.Code);
                command.Connection = connection;
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            return result;
        }
        #endregion

        #region Delete Product
        public int DeleteProduct(Products p)
        {
            int result = 0;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                command.CommandText = "DELETE FROM Products WHERE Id=@id";
                command.Parameters.AddWithValue("@id", p.Id);
                command.Connection = connection;
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            return result;
        }
        #endregion
    }
}