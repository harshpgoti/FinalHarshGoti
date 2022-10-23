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

namespace FinalHarshGoti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_get_all_prod_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new NorthwindEntities())
            {
                var products = from prod in context.Products
                                select new {prod.ProductID,prod.ProductName,prod.QuantityPerUnit,prod.UnitPrice,prod.UnitsInStock,prod.UnitsOnOrder,prod.ReorderLevel,prod.Discontinued
                                };
                dg_products.ItemsSource = products.ToList();

                foreach (var item in context.Categories.ToList())
                {
                    cb_category.Items.Add(item.CategoryName);
                }
            }
        }

        private void btn_add_new_product_Click(object sender, RoutedEventArgs e)
        {
            addProduct objaddProduct = new addProduct();
            objaddProduct.Show();
            this.Hide();
        }

        private void cb_category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context = new NorthwindEntities())
            {
                var products = from prod in context.Products
                               join cat in context.Categories on prod.CategoryID equals cat.CategoryID
                               where cat.CategoryName == cb_category.SelectedItem
                               select new {prod.ProductID,prod.ProductName,prod.QuantityPerUnit,prod.UnitPrice,prod.UnitsInStock,prod.UnitsOnOrder,prod.ReorderLevel,prod.Discontinued
                                };
                dg_products.ItemsSource = products.ToList();
            }
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            if(txt_search.Text != "")
            {
                using (var context = new NorthwindEntities())
                {
                    var products = (from prod in context.Products
                                    where prod.ProductName.Contains(txt_search.Text)
                                    select prod).ToList();

                    dg_products.ItemsSource = products;
                }
            } else
            {
                MessageBox.Show("Please enter product name");
            }

            
        }

        private void btn_clear_data_Click(object sender, RoutedEventArgs e)
        {
            dg_products.ItemsSource = null;
            cb_category.Items.Clear();
            txt_search.Text = null;
        }

        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
