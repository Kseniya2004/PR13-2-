using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR13_2_.Classes
{
    /// <summary>
    /// класс, описывающий предметную область Магазин
    /// </summary>
    public class PRICE
    {
        //поля
        private string name;
        private string shop;
        private double price;
        private int amount;
        //свойства
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Shop
        {
            get { return shop; }
            set { shop = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        //конструкторы
        public PRICE() // Конструктор по умолчанию
        {
            name = "";
            shop = "";
            price = 0;
            amount = 0;
        }
        public PRICE(string name, string shop, double price, int amount) // Конструктор с параметром
        {
            Name = name;
            Shop = shop;
            Price = price;
            Amount = amount;
        }
    }
}
