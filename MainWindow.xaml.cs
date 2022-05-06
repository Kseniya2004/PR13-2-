﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PR13_2_.Classes;

namespace PR13_2_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConnectHelper.ReadListFromFile(@"ListPrice.txt");          

            DtgListPreparate.ItemsSource = ConnectHelper.pricies;         
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.pricies.ToList();
            DtgListPreparate.SelectedIndex = -1;
            int pr = ConnectHelper.pricies.Count();
            txtResult.Text += pr.ToString();
        }

        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.pricies.OrderBy(x => x.Name).ToList();
        }

        private void RbDown_Checked(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.pricies.OrderByDescending(x => x.Name).ToList();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.pricies.Where(x =>
                x.Name.ToLower().Contains(TxtSearch.Text.ToLower())).ToList();
        }

        private void CmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbFiltr.SelectedIndex == 0)
            {
                DtgListPreparate.ItemsSource = ConnectHelper.pricies.Where(x =>
                    x.Amount >= 0 && x.Amount <= 10).ToList();
                MessageBox.Show("Недостаточное количество препаратов на складе!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                if (CmbFiltr.SelectedIndex == 1)
            {
                DtgListPreparate.ItemsSource = ConnectHelper.pricies.Where(x =>
                    x.Amount >= 11 && x.Amount <= 50).ToList();
                MessageBox.Show("Необходимо пополнить запасы препаратов в ближайшее время",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DtgListPreparate.ItemsSource = ConnectHelper.pricies.Where(x =>
                   x.Amount >= 51).ToList();
                MessageBox.Show("Достаточное количество препаратов на складе!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddPrice windowAdd = new WindowAddPrice();
            windowAdd.ShowDialog();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = null;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowAddPrice windowAdd = new WindowAddPrice((sender as Button).DataContext as PRICE);
            windowAdd.ShowDialog();
        }
        /// <summary>
        /// удаление записи с подтверждением
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var resMessage = MessageBox.Show("Удалить запись?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resMessage == MessageBoxResult.Yes)
            {
                int ind = DtgListPreparate.SelectedIndex;
                ConnectHelper.pricies.RemoveAt(ind);
                DtgListPreparate.ItemsSource = ConnectHelper.pricies.ToList();
                ConnectHelper.SaveListToFile(@"ListPrice.txt");
            }

        }
    }
}
