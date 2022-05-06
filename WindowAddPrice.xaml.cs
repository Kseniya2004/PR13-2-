using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PR13_2_.Classes;

namespace PR13_2_
{
    /// <summary>
    /// Логика взаимодействия для WindowAddPrice.xaml
    /// </summary>
    public partial class WindowAddPrice : Window
    {
        int mode;
        public WindowAddPrice()
        {
            InitializeComponent();
            mode = 0;
        }
        public WindowAddPrice(PRICE pRICE)
        {
            InitializeComponent();
            TxbName.Text = pRICE.Name;
            TxbShop.Text = pRICE.Shop;
            TxbPrice.Text = pRICE.Price.ToString();
            TxbAmount.Text = pRICE.Amount.ToString();
            mode = 1;
            BtnAddPrice.Content = "Сохранить";
        }
        private void BtnAddPrice_Click(object sender, RoutedEventArgs e)
        {
            if (mode == 0)
            {
                try
                {
                    PRICE pharmacy = new PRICE()
                    {
                        Name = TxbName.Text,
                        Shop = TxbShop.Text,
                        Price = double.Parse(TxbPrice.Text),
                        Amount = int.Parse(TxbAmount.Text)
                    };
                    ConnectHelper.pricies.Add(pharmacy);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Проверьте входные данные: {ex}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                try
                {
                    for (int i = 0; i < ConnectHelper.pricies.Count; i++)
                    {
                        if (ConnectHelper.pricies[i].Name == TxbName.Text)
                        {
                            ConnectHelper.pricies[i].Shop = TxbShop.Text;
                            ConnectHelper.pricies[i].Price = double.Parse(TxbPrice.Text);
                            ConnectHelper.pricies[i].Amount = int.Parse(TxbAmount.Text);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Проверьте входные данные: {ex}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
               
            }
            ConnectHelper.SaveListToFile(@"ListPrice.txt");
                this.Close();
        }
    }
}
