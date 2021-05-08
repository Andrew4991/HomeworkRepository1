using System;
using FluentAssertions;
using ShopApp;
using Xunit;

namespace ShopLINQ.Tests
{
    [Collection("Repository")]
    public class RepositoryTests
    {
        [Fact]
        public void AddCustomer_Always_AddedSuccessfully()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            repository.AddCustomer("Jonh");

            // assert
            db.Customers.Should().BeEquivalentTo(new Customer(1, "Jonh"));
        }

        [Fact]
        public void AddProduct_Always_AddedSuccessfully()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            repository.AddProduct("Box", 10);

            // assert
            db.Products.Should().BeEquivalentTo(new Product(1, "Box", 10));
        }

        [Fact]
        public void AddProduct_ForNegativePrice_ReturnException()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            decimal price = -10;
            Action act = () => repository.AddProduct("Box", price);

            // assert
            act.Should().Throw<ArgumentException>($"Invalid price: {price}!");
        }

        [Fact]
        public void AddOrder_Always_AddedSuccessfully()
        {
            // arrange
            var db = new FakeDatabase();
            db.Customers.Add( new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));

            var repository = new Repository(db);
            var countBefore = db.Orders.Count;

            // act
            repository.AddOrder(1, 1);
            repository.AddOrder(1, 1);
            repository.AddOrder(1, 1);

            // assert
            var countsAfter = db.Orders.Count;
            countsAfter.Should().Be(countBefore + 3);
        }

        [Fact]
        public void AddOrder_ForNonExistingCustomer_ReturnException()
        {
            // arrange
            var db = new FakeDatabase();
            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));

            var repository = new Repository(db);

            // act
            int customerId = -1;
            Action act = () => repository.AddOrder(customerId, 1);

            // assert
            act.Should().Throw<ArgumentException>($"Invalid customerId: {customerId}!");
        }

        [Fact]
        public void AddOrder_ForNonExistingProduct_ReturnException()
        {
            // arrange
            var db = new FakeDatabase();
            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));

            var repository = new Repository(db);

            // act
            int productId = -1;
            Action act = () => repository.AddOrder(1, productId);

            // assert
            act.Should().Throw<ArgumentException>($"Invalid productId: {productId}!");
        }

        [Fact]
        public void GetOrders_ForNonExistingCustomer_ReturnsEmptyResult()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            var orders = repository.GetOrders(-1);

            // assert
            Assert.Empty(orders);
        }

        [Fact]
        public void GetOrders_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1, 1, 1));

            var repository = new Repository(db);

            // act
            var orders = repository.GetOrders(1);

            // assert
            orders.Should().BeEquivalentTo(new Order(1, 1, 1));
        }

        [Fact]
        public void GetOrder_ForNonExistingOrder_ReturnException()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            var orderId = -1;
            Action act = () => repository.GetOrder(orderId);

            // assert
            act.Should().Throw<ArgumentException>($"No order found from Id({orderId})!");
        }

        [Fact]
        public void GetOrder_ForExistingOrder_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);
            var orderId = 1;

            db.Orders.Add(new Order(orderId, 1, 1));

            // act
            var orders = repository.GetOrders(orderId);

            // assert
            orders.Should().BeEquivalentTo(new Order(orderId, 1, 1));
        }

        [Fact]
        public void GetMoneySpentBy_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));
            db.Orders.Add(new Order(1, 1, 1));

            // act
            var money = repository.GetMoneySpentBy(1);

            // assert
            money.Should().Be(500);
        }

        [Fact]
        public void GetMoneySpentBy_ForUnExistingCustomer_ReturnsZero()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            var money = repository.GetMoneySpentBy(-1);

            // assert
            money.Should().Be(0);
        }

        [Fact]
        public void GetAllProductsPurchased_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));
            db.Orders.Add(new Order(1, 1, 1));

            // act
            var products = repository.GetAllProductsPurchased(1);

            // assert
            products.Should().BeEquivalentTo(new Product(1, "Phone", 500));
        }

        [Fact]
        public void GetAllProductsPurchased_ForUnExistingCustomer_ReturnsEmpty()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            var products = repository.GetAllProductsPurchased(-1);

            // assert
            products.Should().BeEmpty();
        }

        [Fact]
        public void GetUniqueProductsPurchased_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));
            db.Orders.Add(new Order(1, 1, 1));

            // act
            var products = repository.GetUniqueProductsPurchased(1);

            // assert
            products.Should().BeEquivalentTo(new Product(1, "Phone", 500));
        }

        [Fact]
        public void GetUniqueProductsPurchased_ForUnExistingCustomer_ReturnsEmpty()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            var products = repository.GetUniqueProductsPurchased(-1);

            // assert
            products.Should().BeEmpty();
        }

        [Fact]
        public void GetTotalProductsPurchased_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));
            db.Orders.Add(new Order(1, 1, 1));

            // act
            var count = repository.GetTotalProductsPurchased(1);

            // assert
            count.Should().Be(1);
        }

        [Fact]
        public void GetTotalProductsPurchased_ForUnExistingCustomer_ReturnsZero()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            var count = repository.GetTotalProductsPurchased(-1);

            // assert
            count.Should().Be(0);
        }

        [Fact]
        public void HasEverPurchasedProduct_ForExistingCustomerAndExistingProduct_ReturnsTrue()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));
            db.Orders.Add(new Order(1, 1, 1));

            // act
            var isBought = repository.HasEverPurchasedProduct(1, 1);

            // assert
            isBought.Should().Be(true);
        }

        [Fact]
        public void HasEverPurchasedProduct_ForExistingCustomerAndUnExistingProduct_ReturnsFalse()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Customers.Add(new Customer(1, "Mike"));
            db.Orders.Add(new Order(1, 1, 1));

            // act
            var isBought = repository.HasEverPurchasedProduct(1, -1);

            // assert
            isBought.Should().Be(false);
        }

        [Fact]
        public void HasEverPurchasedProduct_ForUnExistingCustomerAndExistingProduct_ReturnsFalse()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Products.Add(new Product(1, "Phone", 500));
            db.Orders.Add(new Order(1, 1, 1));

            // act
            var isBought = repository.HasEverPurchasedProduct(-1, 1);

            // assert
            isBought.Should().Be(false);
        }

        [Fact]
        public void HasEverPurchasedProduct_ForUnExistingCustomerAndUnExistingProduct_ReturnsFalse()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Orders.Add(new Order(1, 1, 1));

            // act
            var isBought = repository.HasEverPurchasedProduct(-1, -1);

            // assert
            isBought.Should().Be(false);
        }

        [Fact]
        public void AreAllPurchasesHigherThan_ForExistingCustomerAndLowPrice_ReturnsTrue()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));
            db.Orders.Add(new Order(1, 1, 1));

            // act
            var isBought = repository.AreAllPurchasesHigherThan(1, 0);

            // assert
            isBought.Should().Be(true);
        }

        [Fact]
        public void AreAllPurchasesHigherThan_ForExistingCustomerAndHighPrice_Returnsfalse()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));
            db.Orders.Add(new Order(1, 1, 1));

            // act
            var isBought = repository.AreAllPurchasesHigherThan(1, 1000);

            // assert
            isBought.Should().Be(false);
        }

        [Fact]
        public void AreAllPurchasesHigherThan_ForUnExistingCustomerAndLowPrice_Returnsfalse()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));
            db.Orders.Add(new Order(1, 1, 1));

            // act
            var isBought = repository.AreAllPurchasesHigherThan(-1, 0);

            // assert
            isBought.Should().Be(false);
        }

        [Fact]
        public void AreAllPurchasesHigherThan_ForUnExistingCustomerAndHighPrice_Returnsfalse()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));
            db.Orders.Add(new Order(1, 1, 1));

            // act
            var isBought = repository.AreAllPurchasesHigherThan(-1, 1000);

            // assert
            isBought.Should().Be(false);
        }

        [Fact]
        public void DidPurchaseAllProducts_ForNull_ReturnException()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            Action act = () => repository.DidPurchaseAllProducts(1, null);

            // assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void DidPurchaseAllProducts_ForExistingCustomerAndExistingProduct_ReturnsTrue()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 3, 1));
            db.Orders.Add(new Order(4, 4, 1));

            // act
            var isPurchase = repository.DidPurchaseAllProducts(1, 1, 3);

            // assert
            isPurchase.Should().Be(true);
        }

        [Fact]
        public void DidPurchaseAllProducts_ForExistingCustomerAndUnExistingProduct_ReturnsFalse()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 3, 1));
            db.Orders.Add(new Order(4, 4, 1));

            // act
            var isPurchase = repository.DidPurchaseAllProducts(1, 1, 5);

            // assert
            isPurchase.Should().Be(false);
        }

        [Fact]
        public void DidPurchaseAllProducts_ForUnExistingCustomerAndExistingProduct_ReturnsFalse()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 3, 1));
            db.Orders.Add(new Order(4, 4, 1));

            // act
            var isPurchase = repository.DidPurchaseAllProducts(-1, 1, 2);

            // assert
            isPurchase.Should().Be(false);
        }

        [Fact]
        public void DidPurchaseAllProducts_ForUnExistingCustomerAndUnExistingProduct_ReturnsFalse()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 3, 1));
            db.Orders.Add(new Order(4, 4, 1));

            // act
            var isPurchase = repository.DidPurchaseAllProducts(-1, 1, 5);

            // assert
            isPurchase.Should().Be(false);
        }

        [Fact]
        public void GetCustomerOverview_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));
            db.Products.Add(new Product(4, "XBox", 800));
            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 1, 1));
            db.Orders.Add(new Order(3, 4, 1));

            // act
            var customer = repository.GetCustomerOverview(1);

            // assert
            customer.Should().BeEquivalentTo(new CustomerOverView
            {
                Name = "Mike",
                TotalMoneySpent = 1800,
                FavoriteProductName = "Phone",
                MaxAmountSpentPerProducts = 1000,
                TotalProductsPurchased = 3
            });
        }

        [Fact]
        public void GetCustomerOverview_ForNonExistingOrder_ReturnException()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            Action act = () => repository.GetCustomerOverview(-1);

            // assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void GetProductsPurchased_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            db.Customers.Add(new Customer(1, "Mike"));
            db.Products.Add(new Product(1, "Phone", 500));
            db.Orders.Add(new Order(1, 1, 1));

            // act
            var product = repository.GetProductsPurchased(1);

            // assert
            product.Should().BeEquivalentTo(("Phone", 1));
        }

        [Fact]
        public void GetProductsPurchased_ForUnExistingCustomer_ReturnsEmpty()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            var product = repository.GetProductsPurchased(-1);

            // assert
            product.Should().BeEmpty();
        }
    }    
}
