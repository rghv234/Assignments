using AuthenticationDemo1.Contexts;
using AuthenticationDemo1.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationDemo1.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly ProductContext _context;

        public ProductRepo(ProductContext context)
        {
            _context = context;
        }
        public string AddProduct(Product product)
        {
            try
            {
                if (product != null)
                {
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    return " Product Added successfully";
                }
                else
                    return "Enter  product details properly";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string DeleteProduct(int id)
        {
            try
            {
                var product = GetProductById(id);
                if (product != null)
                {
                    product.IsActive = false;
                }
                _context.SaveChanges();
                return " Product Deleted successfully";

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                var products = _context.Products.ToList();
                if (products.Count > 0)
                    return products;
                else
                    return null;
            }
            catch (Exception ex)
            {

                throw new Exception($"Exception While GetAllProducts ${ex.Message}");
            }
        }

        public Product GetProductById(int id)
        {
            try
            {

                var product = _context.Products.FirstOrDefault(x => x.Id == id);
                if (product != null)
                    return product;
                else
                    return null;

            }
            catch (Exception ex)
            {

                throw new Exception($"Exception in GetProductById ${ex.Message}");
            }
        }

        public List<Product> GetProductByName(string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var products = _context.Products.Where(p => !string.IsNullOrEmpty(p.Name) &&
                     p.Name.ToLower().Contains(name.ToLower())).ToList();
                    return products;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {

                throw new Exception($" Exception in GetProductByName ${ex.Message}");
            }
        }

        public List<Product> SearchProductsByPrice(int price)
        {
            try
            {

                var products = _context.Products.Where(p => p.Selling_Price >= price).ToList();
                if (products.Count > 0)
                    return products;
                else
                    return null;

            }
            catch (Exception ex)
            {

                throw new Exception($" Exception in SearchProductsByPrice ${ex.Message}");
            }
        }

        public string UpdateProduct(int id, Product product)
        {
            try
            {
                var exisingProduct = GetProductById(id);
                if (exisingProduct == null)
                    return $"Product With the Id {id} Not Found";
                exisingProduct.Name = product.Name;
                exisingProduct.ManufacturedDate = DateTime.Now;
                exisingProduct.Manufacturing_Cost = product.Manufacturing_Cost;
                exisingProduct.Selling_Price = product.Selling_Price;
                exisingProduct.IsActive = product.IsActive;
                _context.SaveChanges();

                return $"Product with Id {id} is updated successfully";
            }
            catch (Exception ex)
            {

                throw new Exception($" Exception in UpdateProduct ${ex.Message}");
            }
        }
    }
    }
