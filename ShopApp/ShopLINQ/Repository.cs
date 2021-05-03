using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp
{
    public class Repository : IRepository
    {
        private readonly Database _db;

        public Repository()
        {
            _db = new();
        }

        public Repository(Database db)
        {
            _db = db;
        }

        public Order[] GetOrders(int customerId)
        {
            return _db.Orders
                   .Where(order => order.CustomerId == customerId)
                   .ToArray();
        }

        public Order GetOrder(int orderId)
        {
            var order = _db.Orders.SingleOrDefault(order => order.Id == orderId);

            if (order == null)
            {
                throw new ArgumentException($"No order found from Id({orderId})!");
            }

            return order;
        }

        public decimal GetMoneySpentBy(int customerId)
        {
            return _db.Orders.Join(_db.Products,
                   (o) => o.ProductId,
                   (p) => p.Id,
                   (o, p) => new { p.Price, o.CustomerId })
                   .Where(x => x.CustomerId == customerId)
                   .Sum(x => x.Price);
        }

        public Product[] GetAllProductsPurchased(int customerId)
        {
            return GetOrders(customerId)
                   .Join(_db.Products, (o) => o.ProductId, (p) => p.Id, (o, p) => p)
                   .ToArray();
        }

        public Product[] GetUniqueProductsPurchased(int customerId)
        {
            return GetOrders(customerId)
                   .Join(_db.Products, (o) => o.ProductId, (p) => p.Id, (o, p) => p)
                   .Distinct()
                   .ToArray();
        }

        public int GetTotalProductsPurchased(int productId)
        {
            return _db.Orders.Where(p => p.ProductId == productId).Count();
        }

        public bool HasEverPurchasedProduct(int customerId, int productId)
        {
            return GetOrders(customerId).Any(x => x.ProductId == productId);
        }

        public bool AreAllPurchasesHigherThan(int customerId, decimal targetPrice)
        {
            var products = GetAllProductsPurchased(customerId);

            if (products.Length == 0)
            {
                return false;
            }

            return products.All(p => p.Price > targetPrice);
        }

        public bool DidPurchaseAllProducts(int customerId, params int[] productIds)
        {
            return GetOrders(customerId)
                   .Select(o => o.ProductId)
                   .Distinct()
                   .Intersect(productIds)
                   .Count() == productIds.Length;
        }

        public CustomerOverView GetCustomerOverview(int customerId)
        {
            var name = _db.Customers.Single(x => x.Id == customerId).Name;

            return new CustomerOverView
            {
                Name = name,
                TotalMoneySpent = GetMoneySpentBy(customerId),
                FavoriteProductName = GetFavoriteProductName(customerId),
                MaxAmountSpentPerProducts = GetAllProductsPurchased(customerId).Max(p => p.Price),
                TotalProductsPurchased = GetAllProductsPurchased(customerId).Length
            };
        }

        public List<(string productName, int numberOfPurchases)> GetProductsPurchased(int customerId)
        {
           return GetOrders(customerId).Join(_db.Products,
                    (o) => o.ProductId,
                    (p) => p.Id,
                    (o, p) => new
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                    })
                    .GroupBy(x => x.ProductId)
                    .Select(g => (g.First().ProductName, g.Count()))
                    .ToList();
        }

        private string GetFavoriteProductName(int customerId)
        {
            return GetOrders(customerId).Join(_db.Products,
                    (o) => o.ProductId,
                    (p) => p.Id,
                    (o, p) => new
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                    })
                    .GroupBy(x => x.ProductId)
                    .Select(g => new
                    {
                        ProductId = g.Key,
                        Count = g.Count(),
                        ProductName = g.First().ProductName
                    })
                    .OrderBy(x => x.Count)
                    .Last()
                    .ProductName;
        }
    }
}
