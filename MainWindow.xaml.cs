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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PR13_2_.Classes;
using Microsoft.Win32;

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
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.pricies.ToList();
            DtgListPreparate.SelectedIndex = -1;
            //количество товаров на складе
            txbCount.Text += ConnectHelper.pricies.Count().ToString();
            //товар с максимальной ценой
            double pr = ConnectHelper.pricies.Max(x => x.Price);
            txbMax.Text = ConnectHelper.pricies.First(x => x.Price == pr).Name.ToString();
            //товар с минимальной ценой
            double prMin = ConnectHelper.pricies.Min(x => x.Price);
            txbMin.Text = ConnectHelper.pricies.First(x => x.Price == prMin).Name.ToString();
            //Общая стоимость товаров
            double prSum = ConnectHelper.pricies.Sum(x => x.Price * x.Amount);
            txbSum.Text = prSum.ToString();
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
                ConnectHelper.SaveListToFile(ConnectHelper.fileName);
            }

        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            //загрузка данных из файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)openFileDialog.ShowDialog())
            {
                //получаем выбранный файл
                ConnectHelper.fileName = openFileDialog.FileName;
                ConnectHelper.ReadListFromFile(ConnectHelper.fileName);
                //ConnectHelper.ReadListFromFile(@"ListPreparates.txt");
            }
            else
                return;

            DtgListPreparate.ItemsSource = ConnectHelper.pricies.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if ((bool)saveFileDialog.ShowDialog())
            {
                string file = saveFileDialog.FileName;
                ConnectHelper.SaveListToFile(file);
            }
        }
    }
}
