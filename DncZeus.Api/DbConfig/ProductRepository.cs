using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DncZeus.Api.DbConfig
{

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }


    public interface IProductRepository
    {
        Task<bool> AddAsync(Product prod);
        Task<IEnumerable<Product>> GetAllAsync();

        IEnumerable<Product> GetAll();

        Task<Product> GetByIDAsync(int id);

        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(Product prod);
    }

    public class ProductRepository : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using (IDbConnection conn = DBConfig.GetSqlConnection())
            {
                return await conn.QueryAsync<Product>(@"SELECT Id
                                            ,Name
                                            ,Quantity
                                            ,Price
                                            ,CategoryId
                                        FROM Product");
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection conn = DBConfig.GetSqlConnection())
            {
                return conn.Query<Product>(@"SELECT Id
                                            ,Name
                                            ,Quantity
                                            ,Price
                                            ,CategoryId
                                        FROM Product");
            }
        }

        public async Task<Product> GetByIDAsync(int id)
        {
            using (IDbConnection conn = DBConfig.GetSqlConnection())
            {
                string sql = @"SELECT Id
                                ,Name
                                ,Quantity
                                ,Price 
                                ,CategoryId
                            FROM Product
                            WHERE Id = @Id";
                return await conn.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
            }
        }

        public async Task<bool> AddAsync(Product prod)
        {
            using (IDbConnection conn = DBConfig.GetSqlConnection())
            {
                string sql = @"INSERT INTO Product 
                            (Name
                            ,Quantity
                            ,Price
                            ,CategoryId)
                        VALUES
                            (@Name
                            ,@Quantity
                            ,@Price
                            ,@CategoryId)";
                return await conn.ExecuteAsync(sql, prod) > 0;
            }
        }

        public async Task<bool> UpdateAsync(Product prod)
        {
            using (IDbConnection conn = DBConfig.GetSqlConnection())
            {
                string sql = @"UPDATE Product SET 
                                Name = @Name
                                ,Quantity = @Quantity
                                ,Price= @Price
                                ,CategoryId= @CategoryId
                           WHERE Id = @Id";
                return await conn.ExecuteAsync(sql, prod) > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (IDbConnection conn = DBConfig.GetSqlConnection())
            {
                string sql = @"DELETE FROM Product
                            WHERE Id = @Id";
                return await conn.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }
    }
}
