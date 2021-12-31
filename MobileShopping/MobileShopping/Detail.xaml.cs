using MobileShopping.Model;
using MobileShopping.Repository;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace MobileShopping
{
    /// <summary>
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : Window
    {
        private HoangHaRepository hoangHaRepository;
        public Detail()
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(Window_Closing);
            hoangHaRepository = new HoangHaRepository();
        }
        public void BindProductDetail(string link)
        {
            Product item = hoangHaRepository.GetProductDetail(link);
            txtTitle.Text = item.Title.Trim();
            txtPrice.Text = item.Price.Trim();
            txtBH.Text = item.BH.Trim();
            txtName.Text = item.ProductName.Trim();
            lvSlides.ItemsSource = item.ListImage;
            lvSpec3.ItemsSource = item.ListSpec;
        }

        public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void LvSlides_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LvSpec3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
