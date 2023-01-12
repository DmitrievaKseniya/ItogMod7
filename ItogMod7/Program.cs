using System;

namespace ItogMod7
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
        }
    }

    public class PhoneNumber
    {
        public string Value { get; }
        public PhoneNumber(string phone)
        {
            Value = phone;
        }
    }

    public class Address
    {
        public string Value { get; }
        public Address(string address)
        {
            Value = address;
        }
    }

    public abstract class Delivery
    {
        protected Address Address;
        protected PhoneNumber PhoneClient;
        public abstract string DisplayDelivery();

        public Delivery(Address address, PhoneNumber phoneClient)
        {
            Address = address;
            PhoneClient = phoneClient;
        }
    }

    public sealed class HomeDelivery : Delivery
    {
        private PhoneNumber PhoneCourier;
        public override string DisplayDelivery()
        {
            return $"Доставка на дом по адресу {Address.Value}, телефон получателя: {PhoneClient.Value}, телефон курера: {PhoneCourier.Value}";
        }

        public HomeDelivery(Address address, PhoneNumber phoneClient, PhoneNumber courierPhone) : base(address, phoneClient)
        {
            PhoneCourier = courierPhone;
        }
    }

    public sealed class PickPointDelivery : Delivery
    {
        private string Schedule;
        public override string DisplayDelivery()
        {
            return $"Забрать с точки выдачи по адресу {Address.Value} график работы {Schedule}, телефон получателя: {PhoneClient.Value}";
        }

        public PickPointDelivery(Address address, PhoneNumber phoneClient, string schedule) : base(address, phoneClient)
        {
            Schedule = schedule;
        }
    }

    public sealed class ShopDelivery : Delivery
    {
        private string Schedule;
        public override string DisplayDelivery()
        {
            return $"Забрать с магазина по адресу {Address.Value} график работы {Schedule}, телефон получателя: {PhoneClient.Value}";
        }
        public ShopDelivery(Address address, PhoneNumber phoneClient, string schedule) : base(address, phoneClient)
        {
            Schedule = schedule;
        }
    }

    public class Product
    {
        public string Name;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator == (Product a, Product b)
        {
            return a.Name == b.Name;
        }
        public static bool operator !=(Product a, Product b)
        {
            return a.Name != b.Name;
        }
    }

    public class ProductCollection
    {
        private Product[] Products;

        public Product this[int index]
        {
            get { return Products[index]; }
        }

        public ProductCollection(Product[] products)
        {
            Products = products;
        }
    }

    public abstract class Order<TDelivery> 
        where TDelivery : Delivery
    {
        protected TDelivery Delivery;
        public  ProductCollection Products { get; }

        public Order(ProductCollection products)
        {
            Products = products;
        }

        public void DisplayDeliveryInfo()
        {
            System.Console.WriteLine(Delivery.DisplayDelivery());
        }
    }

    public class HomeOrder : Order<HomeDelivery>
    {
        public HomeOrder(ProductCollection products, PhoneNumber clientPhone, PhoneNumber courierPhone, Address address) : base(products)
        {
            Delivery = new HomeDelivery(address, clientPhone, courierPhone);
        }
    }

    public class PickPointOrder : Order<PickPointDelivery>
    {
        public PickPointOrder(ProductCollection products, Address address, PhoneNumber clientPhone, string schedule) : base(products)
        {
            Delivery = new PickPointDelivery(address, clientPhone, schedule);
        }
    }

    public class ShopOrder : Order<ShopDelivery>
    {
        public ShopOrder(ProductCollection products, Address address, PhoneNumber clientPhone, string schedule) : base(products)
        {
            Delivery = new ShopDelivery(address, clientPhone, schedule);
        }
    }
}
