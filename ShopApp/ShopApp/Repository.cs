using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp
{
    public class Repository : IRepository
    {
        private readonly Database _database = new();

        public Order[] GetOrders(int customerId)
        {
            AssertValidCustomerId(customerId);

            var orders = from or in _database.Orders
                         where or.CustomerId == customerId
                         select or;

            return orders.ToArray();
        }

        public Order GetOrder(int orderId)
        {
            AssertValidOrderId(orderId);

             return _database.Orders.Where(o => o.Id == orderId).FirstOrDefault();
        }

        public decimal GetMoneySpentBy(int customerId)
        {
            AssertValidCustomerId(customerId);

            var orders = from or in _database.Orders
                         from pr in _database.Products
                         where or.CustomerId == customerId && pr.Id == or.ProductId
                         select new
                         {
                             CustomerId = customerId,
                             Price = pr.Price,
                             protuctId = pr.Id,
                             protuctName = pr.Name
                         };

            return orders.Sum(p => p.Price);
        }

        public Product[] GetAllProductsPurchased(int customerId)
        {
            AssertValidCustomerId(customerId);

            var products = from or in _database.Orders
                           from pr in _database.Products
                           where or.CustomerId == customerId && pr.Id == or.ProductId
                           select pr;

            return products.ToArray();
        }

        public Product[] GetUniqueProductsPurchased(int customerId)
        {
            return GetAllProductsPurchased(customerId).Distinct().ToArray();
        }

        public int GetTotalProductsPurchased(int productId)
        {
            AssertValidProductId(productId);

            var products = from or in _database.Orders
                           from pr in _database.Products
                           where or.ProductId == productId && pr.Id == or.ProductId
                           select pr;

            return products.Count();
        }

        public bool HasEverPurchasedProduct(int customerId, int productId)
        {
            AssertValidCustomerId(customerId);
            AssertValidProductId(productId);

            return GetAllProductsPurchased(customerId).Any(p => p.Id == productId);
        }

        public bool AreAllPurchasesHigherThan(int customerId, decimal targetPrice)
        {
            AssertValidCustomerId(customerId);

            return GetAllProductsPurchased(customerId).All(p => p.Price > targetPrice);
        }

        public bool DidPurchaseAllProducts(int customerId, params int[] productIds)
        {
            AssertValidCustomerId(customerId);

            var products = GetAllProductsPurchased(customerId);

            foreach (var product in productIds)
            {
                AssertValidProductId(product);

                if (!products.Any(p => p.Id == product))
                {
                    return false;
                }
            }

            return true;
        }

        public CustomerOverView GetCustomerOverview(int customerId)
        {
            AssertValidCustomerId(customerId);

            var products = GetAllProductsPurchased(customerId);

            return new CustomerOverView()
            {
                Name = _database.Customers.Where(c => c.Id == customerId).First().Name,
                MaxAmountSpentPerProducts = products.Max(p => p.Price),
                TotalMoneySpent = products.Sum(p => p.Price),
                TotalProductsPurchased = products.Length,
                FavoriteProductName = products.Where(p => p.Price == products.Max(p => p.Price)).First().Name
            };
        }

        public List<(string productName, int numberOfPurchases)> GetProductsPurchased(int customerId)
        {
            AssertValidCustomerId(customerId);

            var products = GetAllProductsPurchased(customerId);
            var uniqueProducts = GetUniqueProductsPurchased(customerId);
            List<(string productName, int numberOfPurchases)> productsPurchased = new();

            foreach (var product in uniqueProducts)
            {
                var productForList = (product.Name, products.Where(p => p.Id == product.Id).Count());

                productsPurchased.Add(productForList);
            }

            return productsPurchased;
        }

        private void AssertValidCustomerId(int customerId)
        {
            if (!_database.Customers.Any(c => c.Id == customerId))
            {
                throw new ArgumentException($"Invalid customerId: {customerId}!");
            }
        }

        private void AssertValidOrderId(int orderId)
        {
            if (!_database.Customers.Any(o => o.Id == orderId))
            {
                throw new ArgumentException($"Invalid orderId: {orderId}!");
            }
        }

        private void AssertValidProductId(int productId)
        {
            if (!_database.Products.Any(p => p.Id == productId))
            {
                throw new ArgumentException($"Invalid productId: {productId}!");
            }
        }
    }
}
