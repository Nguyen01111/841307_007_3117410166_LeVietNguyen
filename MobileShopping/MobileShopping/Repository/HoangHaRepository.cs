using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileShopping.Model;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

namespace MobileShopping.Repository
{
    public class HoangHaRepository
    {
        private string baseLink = "https://hoanghamobile.com";
        private string mobileListLink = "/dien-thoai-di-dong?p={0}";
        // ket noi với csdl
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-Q077OSR\NGUYEN01;
                                Initial Catalog=CrawlData;User ID=SA;Password=123456");

        // hàm lưu dữ liệu 
        void SaveData(string productName, string price, string image, string path)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO DoAn(Name, Price,Img , Link )   " +
                              "VALUES(@param1,@param2,@param3,@param4)";
            cmd.Parameters.AddWithValue("@param1", productName);
            cmd.Parameters.AddWithValue("@param2", price);
            cmd.Parameters.AddWithValue("@param3", image);
            cmd.Parameters.AddWithValue("@param4", path);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // hàm tải dữ liệu từ csdl lên
        void LoadData(List<Product> items)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from DoAn";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var Name = String.Format("{0}", reader["Name"]);
                    var Price = String.Format("{0}", reader["Price"]);
                    var Link = String.Format("{0}", reader["Link"]);
                    var Img = String.Format("{0}", reader["Img"]);
                    items.Add(new Product()
                    {
                        ProductName = Name,
                        Price = Price,
                        Thumbnail = Img,
                        Link = Link
                    });
                }
            }
            con.Close();
        }

        public List<Product> GetProductList()
        {
            List<Product> items = new List<Product>();
            LoadData(items);
            return items;
        }

        // lấy danh sách sản phẩm trong trang đầu tiên
        public List<Product> CrawlData(int index = 1)
        {
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
            };
            HtmlDocument document = htmlWeb.Load(string.Format(baseLink + mobileListLink, index));
            var threadItems = document.DocumentNode.QuerySelectorAll(".lts-product .item").ToList(); // lấy tất cả các thư mục con
            List<Product> items = new List<Product>();
            foreach (var item in threadItems)   // lưu thư mục vào list
            {
                var productName = HtmlEntity.DeEntitize(item.QuerySelector(".info a").InnerText);
                var price = item.QuerySelector(".price strong").InnerText;
                var image = item.QuerySelector(".img img").Attributes["src"].Value; // chỉ lấy giá trị có thuộc tính src trong thẻ
                var path = item.QuerySelector(".info > a").Attributes["href"].Value;
                items.Add(new Product()
                {
                    ProductName = productName,
                    Price = price,
                    Thumbnail = image,
                    Link = path
                });
                // SaveData(productName, price, image, path);
            }
            return items;
        }



        // hàm lấy thông tin chi tiết từ trang web về
        public Product GetProductDetail(string link)
        {
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
            };
            HtmlDocument document = htmlWeb.Load(baseLink + link); // tải trang web về 
            var detail = document.DocumentNode.QuerySelector(".product-details"); // chọn mục con đầu tiên củ thẻ
            var detail1 = document.DocumentNode.QuerySelector(".product-specs");

            var product = new Product();
            // lưu tạm thời vào các biến đã tạo sẵn trong model
            product.ProductName = HtmlEntity.DeEntitize(detail.QuerySelector("h1").InnerText);
            product.Price = detail.QuerySelector(".price strong").InnerText;
            product.Title = HtmlEntity.DeEntitize(detail1.QuerySelector("h3").InnerText);
            product.BH = HtmlEntity.DeEntitize(detail.QuerySelector(".warranty > p > span strong").InnerText);
            var Spec = detail1.QuerySelectorAll(".specs-special > ol").ToList();
            foreach (var item in Spec)
            {
                // Xóa đầu cuối
                var sent = HtmlEntity.DeEntitize(item.InnerText).Trim();
                // Xóa khoảng trắng thừa trong chuỗi
                Regex trimmer = new Regex(@"\s\s+");
                sent = trimmer.Replace(sent, " ");
                product.ListSpec.Add(sent);

            }
            var links = detail.QuerySelectorAll("#imagePreview .viewer  img").ToList();
            foreach (var item in links)
            {
                product.ListImage.Add(item.Attributes["src"].Value);
            }
            return product;
        }

        // hàm lấy tất cả sản phẩm,có 6 trang
        public int GetTotalProduct()
        {


            var total = 6;
            return total;
            //HtmlWeb htmlWeb = new HtmlWeb()
            //{
            //    AutoDetectEncoding = false,
            //    OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
            //};
            //HtmlDocument document = htmlWeb.Load(string.Format(baseLink + mobileListLink, 1));
            //if (document.DocumentNode.QuerySelectorAll(".more-product > a").Count() != 0) // láy được  đoạn phân trang
            //{
            //    var link = document.DocumentNode.QuerySelectorAll(".more-product > a").Last().Attributes["href"].Value; // lấy đường link cuối cùng 
            //    var total = link.LastIndexOf("2"); // tách phân trang của đường link
            //    return Convert.ToInt32(total); // chuyểntừ chuỗi sang chữ số
            //}
            //return 1;
        }
    }
}
