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

namespace MobileShopping
{



    public partial class MainWindow : Window
    {
        private HoangHaRepository hoangHaRepository;

        // tạo 1 object chứa thông tin các sản phẩm
        public ObservableCollection<Product> ProductList
        {
            get { return (ObservableCollection<Product>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        // tạo dependency property ProductList
        public static DependencyProperty DataProperty =
        DependencyProperty.Register("ProductList", typeof(ObservableCollection<Product>), typeof(MainWindow), null);



        public MainWindow()
        {
            // khởi tạo hàm để sử dụng HoangHaRepository
            hoangHaRepository = new HoangHaRepository();
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }


        // đưa dữ liệu vào productlist
        private void BindProductListView()
        {
            if (ProductList == null)
            {
                ProductList = new ObservableCollection<Product>();
            }
            else
            {
                ProductList.Clear();
            }
            // Tạo mới khi tải thêm sản phẩm
            ProductList = new ObservableCollection<Product>(hoangHaRepository.GetProductList());

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            BindProductListView();
        }

        private void btnCrawl_Click(object sender, RoutedEventArgs e)
        {

            CrawlWindow crawlWindow = new CrawlWindow();
            crawlWindow.Show();
        }

        void lvProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as Product;
            if (item != null)
            {
                var detailProductWindow = new Detail();

                // Binding dữ liệu với link của sản phẩm
                detailProductWindow.BindProductDetail(item.Link);
                if (!detailProductWindow.IsVisible)
                {
                    // hiện trang chi tiết sản phẩm
                    detailProductWindow.Show();
                }

                if (detailProductWindow.WindowState == WindowState.Minimized)
                {
                    detailProductWindow.WindowState = WindowState.Normal;
                }

                detailProductWindow.Activate();
                detailProductWindow.Topmost = true;
                detailProductWindow.Topmost = false;
                detailProductWindow.Focus();
            }
        }

        private void LvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
