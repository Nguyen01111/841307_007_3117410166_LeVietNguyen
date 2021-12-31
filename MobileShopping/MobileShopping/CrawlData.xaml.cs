using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MobileShopping.Repository;
using System.Collections.ObjectModel;
using MobileShopping.Model;
using System.ComponentModel;

namespace MobileShopping
{



    public partial class CrawlWindow : Window
    {
        private HoangHaRepository hoangHaRepository1;

        private int currentPageIndex = 0;
        private int totalPage = -1;
        // tạo 1 object chứa thông tin các sản phẩm
        public ObservableCollection<Product> ProductList1
        {
            get { return (ObservableCollection<Product>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        // tạo dependency property ProductList
        public static DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(ObservableCollection<Product>), typeof(CrawlWindow), null);



        public CrawlWindow()
        {
            // khởi tạo hàm để sử dụng HoangHaRepository
            hoangHaRepository1 = new HoangHaRepository();
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window_Loaded1);
            this.Closing += new CancelEventHandler(Window_Closing);
        }
        // đưa dữ liệu vào productlist
        private void BindProductListView1()
        {
            if (ProductList1 == null)
            {
                ProductList1 = new ObservableCollection<Product>();
            }
            else
            {
                ProductList1.Clear();
            }
            // Tạo mới khi tải thêm sản phẩm
            ProductList1 = new ObservableCollection<Product>(hoangHaRepository1.CrawlData(currentPageIndex + 1));
            if (totalPage == -1)
            {
                // tải tất cả sản phẩm với số trang đã lấy được
                totalPage = hoangHaRepository1.GetTotalProduct();
            }

        }

        private void Window_Loaded1(object sender, RoutedEventArgs e)
        {

            BindProductListView1();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex != totalPage - 1)
            {
                currentPageIndex = totalPage - 1;
            }
            BindProductListView1();
        }

        public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
