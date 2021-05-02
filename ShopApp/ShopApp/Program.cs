using System;

namespace ShopApp
{
    public class Program
    {
        private static readonly Repository _repository = new();

        public static void Main(string[] args)
        {
            bool alive = true;

            while (alive)
            {
                try
                {
                    switch (GetNumberItemOfMenu())
                    {
                        case 1:
                            PrintOrders();
                            break;
                        case 2:
                            PrintOrder();
                            break;
                        case 3:
                            PrintMoneySpent();
                            break;
                        case 4:
                            PrintProducts();
                            break;
                        case 5:
                            PrintUniqueProducts();
                            break;
                        case 6:
                            PrintTotalProductsPurchased();
                            break;
                        case 7:
                            PrintDidBuy();
                            break;
                        case 8:
                            PrintAreAllPurchasesHigherThan();
                            break;
                        case 9:
                            PrintDidPurchaseAllProducts();
                            break;
                        case 10:
                            PrintCustomerOvervie();
                            break;
                        case 11:
                            PrintProductsPurchased();
                            break;
                        case 12:
                            alive = false;
                            continue;
                        default:
                            throw new ArgumentException("There is no such menu item!");
                    }

                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
            }
        }

        private static int GetNumberItemOfMenu()
        {
            PrintMenu();

            int itemSelect;

            while (!int.TryParse(Console.ReadLine(), out itemSelect))
            {
                Console.Write("You entered the wrong item. Please try again: ");
            }

            return itemSelect;
        }

        private static void ShowException(Exception ex)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = color;
            Console.ReadKey();
        }

        private static void PrintMenu()
        {
            Console.Clear();
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Print all orders by customerId");
            Console.WriteLine("2. Print order by orderId");
            Console.WriteLine("3. Print Money Spent By customerId");
            Console.WriteLine("4. Print all products purchased By customerId");
            Console.WriteLine("5. Print unique products purchased By customerId");
            Console.WriteLine("6. Print total products purchased By productId");
            Console.WriteLine("7. Print has ever purchased productId By customerId");
            Console.WriteLine("8. Print are all purchases higher than By customerId");
            Console.WriteLine("9. Print did purchase all products By customerId");

            Console.WriteLine("10. Print customer overview By customerId");
            Console.WriteLine("11. Print products purchased By customerId");
            Console.WriteLine("12. Exit program");

            Console.WriteLine("Enter the item number:");
            Console.ForegroundColor = color;
        }

        private static void PrintOrders()
        {
            var customerId = ReadId("Enter the customerId: ");
            var ordersById = _repository.GetOrders(customerId);

            if (ordersById.Length == 0)
            {
                throw new ArgumentException($"No orders found from customer({customerId})!");
            }

            foreach (var order in ordersById)
            {
                PrintOrder(order);
            }
        }

        private static void PrintOrder()
        {
            var orderId = ReadId("Enter the orderId: ");
            var order = _repository.GetOrder(orderId);

            if (order == null)
            {
                throw new ArgumentException($"No order found from Id({orderId})!");
            }

            PrintOrder(order);
        }

        private static void PrintMoneySpent()
        {
            var customerId = ReadId("Enter the customerId: ");
            var money = _repository.GetMoneySpentBy(customerId);

             if (money == 0)
             {
                 throw new ArgumentException($"Customer: { customerId } didn't buy anything !");
             }

            Console.WriteLine($"Customer: {customerId} spent: {money}");
        }

        private static void PrintProducts()
        {
            var customerId = ReadId("Enter the customerId: ");
            var products = _repository.GetAllProductsPurchased(customerId);

            if (products.Length == 0)
            {
                throw new ArgumentException($"No products found from customer({customerId})!");
            }

            Console.WriteLine($"Customer {customerId} buy: ");

            foreach (var product in products)
            {
                Console.WriteLine($"ProductId: {product.Id}    ProductName: {product.Name}    ProductPrice: {product.Price}");
            }
        }

        private static void PrintUniqueProducts()
        {
            var customerId = ReadId("Enter the customerId: ");
            var products = _repository.GetUniqueProductsPurchased(customerId);

            if (products.Length == 0)
            {
                throw new ArgumentException($"No products found from customer({customerId})!");
            }

            Console.WriteLine($"Customer {customerId} buy: ");

            foreach (var product in products)
            {
                Console.WriteLine($"ProductId: {product.Id}    ProductName: {product.Name}    ProductPrice: {product.Price}");
            }
        }

        private static void PrintTotalProductsPurchased()
        {
            var produtId = ReadId("Enter the productId: ");
            var totalProduct = _repository.GetTotalProductsPurchased(produtId);

            if (totalProduct == 0)
            {
                throw new ArgumentException($"Products: { produtId } didn't buy!");
            }

            Console.WriteLine($"The product: { produtId } has been purchased {totalProduct} times ");
        }

        private static void PrintDidBuy()
        {
            var customerId = ReadId("Enter the customerId: ");
            var produtId = ReadId("Enter the productId: ");

            if (_repository.HasEverPurchasedProduct(customerId, produtId))
            {
                Console.WriteLine($"The customer: { customerId } bought product: {produtId}");
            }
            else
            {
                Console.WriteLine($"The customer: { customerId } didn't buy product: {produtId}");
            }
        }

        private static void PrintAreAllPurchasesHigherThan()
        {
            var customerId = ReadId("Enter the customerId: ");
            var price = ReadPrice("Enter the target price: ");

            if (_repository.AreAllPurchasesHigherThan(customerId, price))
            {
                Console.WriteLine($"The price of all purchased products is higher.");
            }
            else
            {
                Console.WriteLine($"The price of not all purchased products is higher.");
            }
        }

        private static void PrintDidPurchaseAllProducts()
        {
            var customerId = ReadId("Enter the customerId: ");
            var countProduct = 2;
            var productIds = new int[countProduct];

            for (int i = 0; i < countProduct; i++)
            {
                productIds[i] = ReadId($"Enter the productId[{i}]: ");
            }

            if (_repository.DidPurchaseAllProducts(customerId, productIds))
            {
                Console.WriteLine($"The costumer: {customerId} bought all products.");
            }
            else
            {
                Console.WriteLine($"The costumer: {customerId} didn't buy all products.");
            }
        }

        private static void PrintCustomerOvervie()
        {
            var customerId = ReadId("Enter the customerId: ");
            var customer= _repository.GetCustomerOverview(customerId);

            Console.WriteLine($"The costumerName: {customer.Name}\n" +
                $"TotalProductsPurchased: {customer.TotalProductsPurchased}\n" +
                $"FavoriteProductName: {customer.FavoriteProductName}\n" +
                $"MaxAmountSpentPerProducts: {customer.MaxAmountSpentPerProducts}\n" +
                $"TotalMoneySpent: {customer.TotalMoneySpent}");
        }

        private static void PrintProductsPurchased()
        {
            var customerId = ReadId("Enter the customerId: ");
            var products = _repository.GetProductsPurchased(customerId);

            if (products.Count == 0)
            {
                throw new ArgumentException($"No products found from customer({customerId})!");
            }

            foreach (var (productName, numberOfPurchases) in products)
            {
                Console.WriteLine($"Product: {productName} bought {numberOfPurchases} times.");
            }
        }

        private static int ReadId(string message)
        {
            Console.WriteLine(message);

            int id;

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("You entered the wrong Id. Please try again: ");
            }

            return id;
        }

        private static decimal ReadPrice(string message)
        {
            Console.WriteLine(message);

            decimal price;

            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.Write("You entered the wrong price. Please try again: ");
            }

            return price;
        }

        private static void PrintOrder(Order order)
        {
            Console.WriteLine($"OrderId: {order.Id}    ProductId: {order.ProductId}    CustomerId: {order.CustomerId}");
        }
    }
}
