using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopLINQ;

namespace ShopApp
{
    public class Repository : IRepository
    {
        private readonly IDatabase _db;

        public Repository()
        {
            _db = new Database();
        }

        public Repository(IDatabase db)
        {
            _db = db;
        }

        public void AddCustomer(string name)
        {
            _db.Customers.Add(new Customer(_db.Customers.Count+1, name));
        }

        public void AddProduct(string name, decimal price)
        {
            AssertValidPrice(price);

            _db.Products.Add(new Product(_db.Products.Count + 1, name, price));
        }

        public void AddOrder(int customerId, int productId)
        {
            AssertValidCustomerId(customerId);
            AssertValidProductId(productId);

            _db.Orders.Add(new Order(_db.Orders.Count + 1, customerId, productId));
        }

        public Order[] GetOrders(int customerId)
        {
            return GetOrdersInternal(customerId).ToArray();
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
            return GetProductOrdersJoined(customerId).Sum(x => x.product.Price);
        }

        public Product[] GetAllProductsPurchased(int customerId)
        {
            return GetAllProductsPurchasedInternal(customerId).ToArray();
        }

        public Product[] GetUniqueProductsPurchased(int customerId)
        {
            return GetAllProductsPurchasedInternal(customerId).Distinct().ToArray();
        }

        public int GetTotalProductsPurchased(int productId)
        {
            return _db.Orders.Where(p => p.ProductId == productId).Count();
        }

        public bool HasEverPurchasedProduct(int customerId, int productId)
        {
            return GetOrdersInternal(customerId).Any(x => x.ProductId == productId);
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
            return GetOrdersInternal(customerId)
                   .Select(o => o.ProductId)
                   .Distinct()
                   .Intersect(productIds)
                   .Count() == productIds.Length;
        }

        public CustomerOverView GetCustomerOverview(int customerId)
        {
            return new CustomerOverView
            {
                Name = _db.Customers.Single(x => x.Id == customerId).Name,
                TotalMoneySpent = GetMoneySpentBy(customerId),
                FavoriteProductName = GetFavoriteProductName(customerId),
                MaxAmountSpentPerProducts = GetMaxAmountSpentPerProducts(customerId),
                TotalProductsPurchased = GetAllProductsPurchasedInternal(customerId).Count()
            };
        }

        public List<(string productName, int numberOfPurchases)> GetProductsPurchased(int customerId)
        {
            return GetProductOrdersJoined(customerId)
                .GroupBy(x => x.order.ProductId)
                .Select(g => (g.First().product.Name, g.Count()))
                .ToList();
        }

        public List<(string customerName, string productName, decimal price, int numberOfPurchases)> GetProductsPurchasedForAllCustomers()
        {
            List<(string customerName, string productName, decimal price, int numberOfPurchases)> rezult = new();

            foreach (var id in _db.Customers.Select(x => x.Id))
            {
                rezult.AddRange(GetProductsPurchasedForOneCustomers(id));
            }

            return rezult;
        }

        private List<(string customerName, string productName, decimal price, int numberOfPurchases)> GetProductsPurchasedForOneCustomers(int customerId)
        {
            return GetProductOrdersJoined(customerId)
                .GroupBy(x => x.order.ProductId)
                .Select(g => (
                _db.Customers.Single(x => x.Id == customerId).Name,
                g.First().product.Name,
                g.First().product.Price,
                g.Count()))
                .ToList();
        }

        private string GetFavoriteProductName(int customerId)
        {
            return GetProductOrdersJoined(customerId)
                    .GroupBy(x => x.order.ProductId)
                    .Select(g => new
                    {
                        ProductId = g.Key,
                        Count = g.Count(),
                        ProductName = g.First().product.Name
                    })
                    .OrderBy(x => x.Count)
                    .Last()
                    .ProductName;
        }

        private decimal GetMaxAmountSpentPerProducts(int customerId)
        {
            return GetProductOrdersJoined(customerId)
                    .GroupBy(x => x.order.ProductId)
                    .Select(g => new
                    {
                        Price = g.Count() * g.First().product.Price
                    })
                    .Max(x => x.Price);
        }

        private IEnumerable<Order> GetOrdersInternal(int customerId)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId);
        }

        private IEnumerable<(Product product, Order order)> GetProductOrdersJoined(int customerId)
        {
            return GetOrdersInternal(customerId).Join(_db.Products, (o) => o.ProductId, (p) => p.Id, (o, p) => (p, o));
        }

        private IEnumerable<Product> GetAllProductsPurchasedInternal(int customerId)
        {
            return GetProductOrdersJoined(customerId).Select(p => p.product);
        }

        private void AssertValidCustomerId(int customerId)
        {
            if (!_db.Customers.Any(c => c.Id == customerId))
            {
                throw new ArgumentException($"Invalid customerId: {customerId}!");
            }
        }

        private void AssertValidProductId(int productId)
        {
            if (!_db.Products.Any(p => p.Id == productId))
            {
                throw new ArgumentException($"Invalid productId: {productId}!");
            }
        }

        private void AssertValidPrice(decimal price)
        {
            if (price<0)
            {
                throw new ArgumentException($"Invalid price: {price}!");
            }
        }
    }
}
