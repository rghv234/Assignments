﻿using SimpleAPIDemo.Models;

namespace SimpleAPIDemo.Services
{
    public interface IProductService
    {
        IEnumerable<Models.Product> GetProducts();
        Models.Product? GetProduct(int id);
        Models.Product CreateProduct(Models.Product product);
        void UpdateProduct(int id, Models.Product updatedProduct);
        void DeleteProduct(int id);
        List<Product> GetProductsByPrice(int price);
        List<Product> GetProductsByName(string name);
        IEnumerable<Product>? SearchProducts(ProductSearch search);

    }
}
