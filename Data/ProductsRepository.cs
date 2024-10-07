using System.Data;
using Dapper;

namespace BlazorWebAppWithDapper.Data
{
    public class ProductsRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ProductsRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // Get all products
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<Product>("GetAllProducts", commandType: CommandType.StoredProcedure);
        }

        // Get a single product by Id
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Product>(
                "GetProductById",
                new { ProductId = productId },
                commandType: CommandType.StoredProcedure);
        }

        // Insert a new product
        public async Task<int> InsertProductAsync(Product product)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("ProductName", product.ProductName);
            parameters.Add("Price", product.Price);
            parameters.Add("Quantity", product.Quantity);

            return await connection.ExecuteAsync("InsertProduct", parameters, commandType: CommandType.StoredProcedure);
        }

        // Update an existing product
        public async Task<int> UpdateProductAsync(Product product)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("ProductId", product.ProductId);
            parameters.Add("ProductName", product.ProductName);
            parameters.Add("Price", product.Price);
            parameters.Add("Quantity", product.Quantity);

            return await connection.ExecuteAsync("UpdateProduct", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
