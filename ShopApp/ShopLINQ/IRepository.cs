﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp
{
    public interface IRepository
    {
        void AddOrder(int customerId, int productId);
        Order[] GetOrders(int customerId);
        Order GetOrder(int orderId);
        decimal GetMoneySpentBy(int customerId);
        Product[] GetAllProductsPurchased(int customerId);
        Product[] GetUniqueProductsPurchased(int customerId);
        int GetTotalProductsPurchased(int productId);
        bool HasEverPurchasedProduct(int customerId, int productId);
        bool AreAllPurchasesHigherThan(int customerId, decimal targetPrice);
        bool DidPurchaseAllProducts(int customerId, params int[] productIds);
        CustomerOverView GetCustomerOverview(int customerId);
        List<(string productName, int numberOfPurchases)> GetProductsPurchased(int customerId);
        public List<ProductsOverView> GetProductsPurchasedForAllCustomers();
    }
}
