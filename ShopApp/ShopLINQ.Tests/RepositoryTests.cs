using System;
using FluentAssertions;
using ShopApp;
using Xunit;
using Moq;
using System.Linq;
using System.Collections.Generic;

namespace ShopLINQ.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void AddCustomer_Always_AddedSuccessfully()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());

            var repository = new Repository(mock.Object);

            // act
            repository.AddCustomer("Jonh");

            // assert
            mock.Object.Customers.Should().BeEquivalentTo(new Customer(1, "Jonh"));
        }

        [Fact]
        public void AddProduct_Always_AddedSuccessfully()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>());

            var repository = new Repository(mock.Object);

            // act
            repository.AddProduct("Box", 10);

            // assert
            mock.Object.Products.Should().BeEquivalentTo(new Product(1, "Box", 10));
        }

        [Fact]
        public void AddProduct_ForNegativePrice_ThrowsException()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>());

            var repository = new Repository(mock.Object);

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
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product> { new Product(1, "Phone", 500) });
            mock.Setup(repo => repo.Customers).Returns(new List<Customer> { new Customer(1, "Mike") });
            mock.Setup(repo => repo.Orders).Returns(new List<Order>());

            var repository = new Repository(mock.Object);

            // act
            repository.AddOrder(1, 1);
            repository.AddOrder(1, 1);
            repository.AddOrder(1, 1);

            // assert
            mock.Object.Orders.Count.Should().Be(3);
        }

        [Theory]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        public void AddOrder_ForNonExistingCustomerOrNonExistingProduct_ThrowsException(int customerId, int productId)
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product> { new Product(1, "Phone", 500) });
            mock.Setup(repo => repo.Customers).Returns(new List<Customer> { new Customer(1, "Mike") });
            mock.Setup(repo => repo.Orders).Returns(new List<Order>());

            var repository = new Repository(mock.Object);

            // act
            Action act = () => repository.AddOrder(customerId, productId);

            // assert
            act.Should().Throw<ArgumentException>();
        }      

        [Fact]
        public void GetOrders_ForCustomer_ReturnsEmptyResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order>());

            var repository = new Repository(mock.Object);

            // act
            var orders = repository.GetOrders(-1);

            // assert
            orders.Should().BeEmpty();
        }

        [Fact]
        public void GetOrders_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order> { new Order(1, 1, 1) });

            var repository = new Repository(mock.Object);

            // act
            var orders = repository.GetOrders(1);

            // assert
            orders.Should().BeEquivalentTo(new Order(1, 1, 1));
        }

        [Fact]
        public void GetOrder_ForNonExistingOrder_ThrowsException()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order>());

            var repository = new Repository(mock.Object);

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
            var orderId = 1;

            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order> { new Order(orderId, 1, 1) });

            var repository = new Repository(mock.Object);

            // act
            var orders = repository.GetOrders(orderId);

            // assert
            orders.Should().BeEquivalentTo(new Order(orderId, 1, 1));
        }

        [Theory]
        [InlineData(1, 500)]
        [InlineData(-1, 0)]
        public void GetMoneySpentBy_ForCustomer_ReturnsResult(int customerId, decimal rezult)
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product> { new Product(1, "Phone", 500) });
            mock.Setup(repo => repo.Customers).Returns(new List<Customer> { new Customer(1, "Mike") });
            mock.Setup(repo => repo.Orders).Returns(new List<Order> { new Order(1, 1, 1) });

            var repository = new Repository(mock.Object);

            // act
            var money = repository.GetMoneySpentBy(customerId);

            // assert
            money.Should().Be(rezult);
        }

        [Fact]
        public void GetAllProductsPurchased_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product> { new Product(1, "Phone", 500) });
            mock.Setup(repo => repo.Customers).Returns(new List<Customer> { new Customer(1, "Mike") });
            mock.Setup(repo => repo.Orders).Returns(new List<Order> { new Order(1, 1, 1) });

            var repository = new Repository(mock.Object);

            // act
            var products = repository.GetAllProductsPurchased(1);

            // assert
            products.Should().BeEquivalentTo(new Product(1, "Phone", 500));
        }

        [Fact]
        public void GetAllProductsPurchased_ForNonExistingCustomer_ReturnsEmpty()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order>());

            var repository = new Repository(mock.Object);

            // act
            var products = repository.GetAllProductsPurchased(-1);

            // assert
            products.Should().BeEmpty();
        }

        [Fact]
        public void GetUniqueProductsPurchased_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product> { new Product(1, "Phone", 500) });
            mock.Setup(repo => repo.Customers).Returns(new List<Customer> { new Customer(1, "Mike") });
            mock.Setup(repo => repo.Orders).Returns(new List<Order> { new Order(1, 1, 1), new Order(2, 1, 1) });

            var repository = new Repository(mock.Object);

            // act
            var products = repository.GetUniqueProductsPurchased(1);

            // assert
            products.Should().BeEquivalentTo(new Product(1, "Phone", 500));
        }

        [Fact]
        public void GetUniqueProductsPurchased_ForNonExistingCustomer_ReturnsEmpty()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order>());

            var repository = new Repository(mock.Object);

            // act
            var products = repository.GetUniqueProductsPurchased(-1);

            // assert
            products.Should().BeEmpty();
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(-1, 0)]
        public void GetTotalProductsPurchased_ForProductId_ReturnsResult(int productId, int rezult)
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product> { new Product(1, "Phone", 500) });
            mock.Setup(repo => repo.Customers).Returns(new List<Customer> { new Customer(1, "Mike") });
            mock.Setup(repo => repo.Orders).Returns(new List<Order> { new Order(1, 1, 1), new Order(2, 1, 1) });

            var repository = new Repository(mock.Object);

            // act
            var count = repository.GetTotalProductsPurchased(productId);

            // assert
            count.Should().Be(rezult);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, -1, false)]
        [InlineData(-1, 1, false)]
        [InlineData(-1, -1, false)]
        public void HasEverPurchasedProduct_Always_ReturnsRezult(int customerId, int productId, bool rezult)
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product> { new Product(1, "Phone", 500) });
            mock.Setup(repo => repo.Customers).Returns(new List<Customer> { new Customer(1, "Mike") });
            mock.Setup(repo => repo.Orders).Returns(new List<Order> { new Order(1, 1, 1) });

            var repository = new Repository(mock.Object);

            // act
            var isBought = repository.HasEverPurchasedProduct(customerId, productId);

            // assert
            isBought.Should().Be(rezult);
        }

        [Theory]
        [InlineData(1, 0, true)]
        [InlineData(1, 1000, false)]
        [InlineData(-1, 0, false)]
        [InlineData(-1, 1000, false)]
        public void AreAllPurchasesHigherThan_Always_ReturnsRezult(int customerId, decimal targetPrice, bool rezult)
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product> { new Product(1, "Phone", 500) });
            mock.Setup(repo => repo.Customers).Returns(new List<Customer> { new Customer(1, "Mike") });
            mock.Setup(repo => repo.Orders).Returns(new List<Order> { new Order(1, 1, 1) });

            var repository = new Repository(mock.Object);

            // act
            var isBought = repository.AreAllPurchasesHigherThan(customerId, targetPrice);

            // assert
            isBought.Should().Be(rezult);
        }

        [Fact]
        public void DidPurchaseAllProducts_ForNull_ThrowsException()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order>());

            var repository = new Repository(mock.Object);

            // act
            Action act = () => repository.DidPurchaseAllProducts(1, null);

            // assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(1, true, 1, 3)]
        [InlineData(1, false, 1, 5)]
        [InlineData(-1, false, 1, 2)]
        [InlineData(-1, false, 1, 5)]
        public void DidPurchaseAllProducts_ForExistingCustomerAndExistingProduct_ReturnsTrue(int customerId, bool rezult, params int[] productIds)
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            {
                new Order(1, 1, 1),
                new Order(2, 2, 1),
                new Order(3, 3, 1),
                new Order(4, 4, 1)
            });

            var repository = new Repository(mock.Object);

            // act
            var isPurchase = repository.DidPurchaseAllProducts(customerId, productIds);

            // assert
            isPurchase.Should().Be(rezult);
        }

        [Fact]
        public void GetCustomerOverview_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            {
                new Product(1, "Phone", 500),
                new Product(4, "XBox", 800)
            });
            mock.Setup(repo => repo.Customers).Returns(new List<Customer> { new Customer(1, "Mike") });
            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            {
                new Order(1, 1, 1),
                new Order(2, 1, 1),
                new Order(3, 4, 1)
            });

            var repository = new Repository(mock.Object);

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
        public void GetCustomerOverview_ForNonExistingOrder_ThrowsException()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order>());

            var repository = new Repository(mock.Object);

            // act
            Action act = () => repository.GetCustomerOverview(-1);

            // assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void GetProductsPurchased_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product> { new Product(1, "Phone", 500) });
            mock.Setup(repo => repo.Customers).Returns(new List<Customer> { new Customer(1, "Mike") });
            mock.Setup(repo => repo.Orders).Returns(new List<Order> { new Order(1, 1, 1) });

            var repository = new Repository(mock.Object);

            // act
            var product = repository.GetProductsPurchased(1);

            // assert
            product.Should().BeEquivalentTo(("Phone", 1));
        }

        [Fact]
        public void GetProductsPurchased_ForNonExistingCustomer_ReturnsEmpty()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order>());

            var repository = new Repository(mock.Object);

            // act
            var product = repository.GetProductsPurchased(-1);

            // assert
            product.Should().BeEmpty();
        }

        [Fact]
        public void GetProductsPurchased_ForAllCustomers_Always_ReturnsRezult()
        {
            // arrange
            var mock = new Mock<IDatabase>();
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            {
                new Product(1, "Phone", 500),
                new Product(2, "Notebook", 1000),
                new Product(3, "PC", 1500),
                new Product(4, "XBox", 800)
            });
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            {
                new Customer(1, "Mike"),
                new Customer(2, "John")
            });
            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            {
                new Order(1, 1, 1),
                new Order(2, 1, 1),
                new Order(3, 4, 1),
                new Order(4, 2, 2),
                new Order(5, 3, 2)
            });

            var repository = new Repository(mock.Object);

            // act
            var products = repository.GetProductsPurchasedForAllCustomers();

            // assert
            products.Should().BeEquivalentTo(new List<ProductsOverView>
            {
                new ProductsOverView("Mike", "Phone", 500, 2),
                new ProductsOverView("Mike", "XBox", 800, 1),
                new ProductsOverView("John", "Notebook", 1000, 1),
                new ProductsOverView("John", "PC", 1500, 1)
            });
        }
    }    
}
