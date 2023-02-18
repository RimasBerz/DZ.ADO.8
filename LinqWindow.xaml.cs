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

namespace Sales
{
    /// <summary>
    /// Interaction logic for LinqWindow.xaml
    /// </summary>
    public partial class LinqWindow : Window
    {
        private LinqContext.DataContext context;
        public LinqWindow()
        {
            InitializeComponent();
            try
            {
                context = new(App.ConnectinString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SimpleN_click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.OrderBy(p => p.Name);
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Price + " " +"\n";
            }
            textBlock1.Text += query.Count() + " Total" + "\n";
        }
        private void SimpleRP_click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.OrderByDescending(p => p.Price);
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Price + " " + item.Name + "\n";
            }
            textBlock1.Text += query.Count() + " Total" + "\n";
        }
        private void SimpleTC_click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.Where(p => p.Price < 200).OrderBy(prop => prop.Price);
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Price + " " + item.Name + "\n";
            }
            textBlock1.Text += query.Count() + " Total" + "\n";
        }
        private void SimpleP_click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.OrderBy(p => p.Price);
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Price + " " + item.Name + "\n";
            }
            textBlock1.Text += query.Count() + " Total" + "\n";
        }

        private void SimpleGOW_click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.Where(p => p.Name.StartsWith("%Г%" + "ов%"));
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Name + "\n";
            }
        }
        // Не понял,кто такое руководитель
        #region
        private void SimpleMD_click(object sender, RoutedEventArgs e)
        {

            var query = from m in context.Managers
                        join d in context.Departments on m.Id_main_dep equals d.Id
                        select new
                        {
                            Manager = m.Surname + " " + m.Name,
                            Department = d.Name
                        };
            var query2 = context.Managers.Join(context.Departments, m => m.Id_main_dep, d => d.Id, (m, d) => new { Manager = m.Surname + " " + m.Name, Deparment = d.Name });
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Manager + item.Department + " - "+ "\n";
            }
            textBlock1.Text += "\n" + query.Count() + " Total";
        }

        private void SimpleMW_click(object sender, RoutedEventArgs e)
        {
            var query = from m in context.Managers
                        join d in context.Departments on m.Id_main_dep equals d.Id
                        select new
                        {
                            Manager = m.Surname + " " + m.Name,
                            Department = d.Name
                        };
            var query2 = context.Managers.Join(context.Departments, m => m.Id_main_dep, d => d.Id, (m, d) => new { Manager = m.Surname + " " + m.Name, Deparment = d.Name });
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Manager + item.Department + " - " + "\n";
            }
            textBlock1.Text += "\n" + query.Count() + " Total";

        }

        private void SimpleMDW_click(object sender, RoutedEventArgs e)
        {
            var query = from m in context.Managers
                        join d in context.Departments on m.Id_main_dep equals d.Id
                        select new
                        {
                            Manager = m.Surname + " " + m.Name,
                            Department = d.Name
                        };
            var query2 = context.Managers.Join(context.Departments, m => m.Id_main_dep, d => d.Id, (m, d) => new { Manager = m.Surname + " " + m.Name, Deparment = d.Name });
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Manager + item.Department + " - " + "\n";
            }
            textBlock1.Text += "\n" + query.Count() + " Total";
            #endregion
        }
    }
}
