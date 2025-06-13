using SimpleAPIDemo.Models;

namespace SimpleAPIDemo.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Models.Product> _products = new List<Models.Product>
        {
            new Models.Product { Id = 1, Name = "Product 1", Price = 10.99m },
            new Models.Product { Id = 2, Name = "Product 2", Price = 20.99m },
            new Models.Product { Id = 3, Name = "Product 3", Price = 30.99m }
        };
        public IEnumerable<Models.Product> GetProducts()
        {
            return _products;
        }
        public Models.Product? GetProduct(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }
        public Models.Product CreateProduct(Models.Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
            return product;
        }
        public void UpdateProduct(int id, Models.Product updatedProduct)
        {
            var product = GetProduct(id);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
            }
        }
        public List<Product> GetProductsByPrice(int price)
        {
            var productList = _products.Where(p => p.Price == price).ToList();
            return productList;
        }
        public List<Product> GetProductsByName(string name)
        {
            var productList = _products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return productList;
        }
        public void DeleteProduct(int id)
        {
            var product = GetProduct(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
        public IEnumerable<Product>? SearchProducts(ProductSearch search)
        {
            var filteredProducts = _products.AsEnumerable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                filteredProducts = filteredProducts
                    .Where(p => p.Name.Equals(search.Name, StringComparison.OrdinalIgnoreCase));
            }

            if (search.Price.HasValue)
            {
                filteredProducts = filteredProducts
                    .Where(p => p.Price == search.Price.Value);
            }

            if (!filteredProducts.Any())
            {
                return null;
            }

            return filteredProducts;
        }

    }
}
