using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace PR13_2_.Classes
{
    class ConnectHelper
    {
        public static List<PRICE> pricies = new List<PRICE>();

        public static string fileName;

        public static void ReadListFromFile(string filename)
        {
            pricies.Clear();
            try
            {
                StreamReader streamReader = new StreamReader(filename, Encoding.UTF8);
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    string[] items = line.Split(';');
                    PRICE pricy = new PRICE()
                    {
                        Name = items[0].Trim(),
                        Shop = items[1].Trim(),
                        Price = double.Parse(items[2].Trim()),
                        Amount = int.Parse(items[3].Trim())
                    };
                    pricies.Add(pricy);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверный формат файла!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public static void SaveListToFile(string filename)
        {
            StreamWriter streamWriter = new StreamWriter(filename, false, Encoding.UTF8);
            foreach (PRICE pr in pricies)
            {
                streamWriter.WriteLine($"{pr.Name};{pr.Shop};{pr.Price};{pr.Amount}");
            }
            streamWriter.Close();
        }
    }
}
