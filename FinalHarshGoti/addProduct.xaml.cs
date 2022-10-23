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

namespace FinalHarshGoti
{
    /// <summary>
    /// Interaction logic for addProduct.xaml
    /// </summary>
    public partial class addProduct : Window
    {
        public addProduct()
        {
            InitializeComponent();

            using (var context = new NorthwindEntities())
            {
                foreach (var item in context.Categories.ToList())
                {
                    cb_new_cat.Items.Add(item.CategoryName);
                }

            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void btn_add_new_prod_Click(object sender, RoutedEventArgs e)
        {
            if (txt_new_prod_name.Text == "")
            {

                MessageBox.Show("Please enter product name");
            } else if (txt_new_price.Text == "")
            {

                MessageBox.Show("Please enter product's price");
            } else if (cb_new_cat.SelectedIndex == -1)
            {

                MessageBox.Show("Please select product category");
            }
            else
            {
                using (var context = new NorthwindEntities())
                {
                    Product product = new Product();
                    product.ProductName = txt_new_prod_name.Text;
                    product.UnitPrice = decimal.Parse(txt_new_price.Text);
                    product.CategoryID = cb_new_cat.SelectedIndex + 1;

                    context.Products.Add(product);
                    context.SaveChanges();

                    MessageBox.Show("New Product Added");
                }
            }
            
        }
    }
}
