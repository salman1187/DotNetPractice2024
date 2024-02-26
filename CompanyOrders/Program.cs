using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyOrders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Company cpy = new Company();

            Item i1 = new Item();
            i1.Rate = 100;
            Item i2 = new Item();
            i2.Rate = 50;

            Customer c1 = new Customer();
            Customer c2 = new Customer();
            Customer c3 = new Customer();
            RegCustomer r1 = new RegCustomer();
            r1.SplDiscount = 10;
            c3 = r1;

            Order o1 = new Order();
            Order o2 = new Order();
            OrderedItem oitem1 = new OrderedItem();
            OrderedItem oitem2 = new OrderedItem();
            oitem1.TheItem = i1;
            oitem2.TheItem = i2;
            oitem1.Qty = 2;
            oitem2.Qty = 5;
            o1.OrderedItems.Add(oitem1);
            o2.OrderedItems.Add(oitem2);

            c1.Orders.Add(o1);
            c2.Orders.Add(o2);
            c3.Orders.Add(o1);

            cpy.Items.Add(i1);
            cpy.Items.Add(i2);

            cpy.Customers.Add(c1);
            cpy.Customers.Add(c2);
            cpy.Customers.Add(c3);

            Console.WriteLine($"Total = {cpy.GetTotalWorthOfOrdersPlaced()}");


        }
    }
    class Company
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public double GetTotalWorthOfOrdersPlaced()
        {
            double total = 0;
            foreach(Customer cus in Customers)
            {
                if(cus is RegCustomer regCustomer)
                {
                    foreach (Order ord in cus.Orders)
                    {
                        foreach (OrderedItem ordItem in ord.OrderedItems)
                            total += (ordItem.Qty * ordItem.TheItem.Rate) - ((ordItem.Qty * ordItem.TheItem.Rate) * regCustomer.SplDiscount/100);
                    }

                }
                else
                {
                    foreach (Order ord in cus.Orders)
                    {
                        foreach (OrderedItem ordItem in ord.OrderedItems)
                            total += (ordItem.Qty * ordItem.TheItem.Rate);
                    }
                }
            }
           
            return total;
        }
    }
    class Item
    {
        public string Description { get; set; }
        public double Rate { get; set; }
    }
    class Customer
    {
        public List<Order> Orders { get; set; } = new List<Order>();
    }
    class Order
    {
        public Customer TheCustomer { get; set; }
        public List<OrderedItem> OrderedItems { get; set; } = new List<OrderedItem>();
    }
    class OrderedItem
    {
        public int Qty { get; set; }
        public Item TheItem { get; set; }
    }
    class RegCustomer : Customer
    {
        public double SplDiscount { get; set; }
    }
}
