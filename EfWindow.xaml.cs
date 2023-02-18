using Sales.EfContext;
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
    /// Interaction logic for EFWindow.xaml
    /// </summary>
    public partial class EFWindow : Window
    {
        public EfContext.DataContext dataContext;
        public EFWindow()
        {
            InitializeComponent();
            dataContext = new();
            //ShowDepartmentsCount();
            //ShowProductsCount();
            //ShowManagersCount();
            //ShowSalesCount();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MonitorDepartments.Content =
                dataContext.Departments.Count();
            MonitorSales.Content =
                dataContext.Sales.Count();
            ShowDailyStatistics();
        }
        //private void ShowDepartmentsCount()
        //{
        //    MonitorDepartments.Content =
        //        dataContext.Departments.Count();
        //}
        //private void ShowManagersCount()
        //{
        //    MonitorManagers.Content =
        //        dataContext.Managers.Count();
        //}
        //private void ShowProductsCount()
        //{
        //    MonitorProducts.Content =
        //        dataContext.Products.Count();
        //}
        //private void ShowSalesCount()
        //{
        //    MonitorSales.Content =
        //        dataContext.Sales.Count();
        //}
        private void ShowDailyStatistics()
        {
            SalesCnt.Content = dataContext.Sales
                .Where(sale => sale.Moment.Date == DateTime.Now.Date).Count();
            SalesTotal.Content = dataContext.Sales
               .Where(sale => sale.Moment.Date == DateTime.Now.Date).Sum(sale => sale.Cnt);
            SalesMoney.Content = dataContext.Sales
                 .Where(sale => sale.Moment.Date == DateTime.Now.Date)
                .Join(dataContext.Products,
                s => s.ProductId, p => p.Id,
                (s, p) => s.Cnt * p.Price)
              .Sum()
              .ToString("0.00");

            SalesTopManager.Content = dataContext.Managers
                 .GroupJoin(
                dataContext.Sales.Where(s => s.Moment.Date == DateTime.Now.Date),
                    m => m.Id,
                    s => s.ManagerId,
                    (m, s) => new { Manager = m, Total = s.Sum(s => s.Cnt) })
                    .OrderByDescending(mix => mix.Total)
                    .Take(1)
                    .Select(mix => mix.Manager.ToShortString() + $"({mix.Total}")
                    .First();

            SalesTopProduct.Content = dataContext.Products
               .GroupJoin(
              dataContext.Sales.Where(s => s.Moment.Date == DateTime.Now.Date),
                  p => p.Id,
                  s => s.ProductId,
                  (p, ss) => new { Product = p, Total = p.Price * ss.Sum(s => s.Cnt ) })
                  .OrderByDescending(mix => mix.Total)
                  .Take(1)
                  .Select(mix => mix.Product.ToShortString() + $"({mix.Total}")
                  .First();

        //   SalesTopDepartment.Content = dataContext.Departments
        //   .GroupJoin (dataContext.Managers
        //    .GroupJoin(
        //        dataContext.Sales.Where(s => s.Moment.Date == DateTime.Now.Date),
        //         m => m.Id,
        //         s => s.ManagerId,
        //         (m, ss) => new { Manager = m, Total = ss.Sum(s => s.Cnt) })
        //    ),
        //   d => d.Id,
        //   mix => mix.Manager.IdMainDep,
        //   (d,mixes) => new {Department = Department,Total = mixes.Sum(mix => mix.Total)}
        //   )
        //   .OrderByDescending(mixd => mixd.Total)
        //.Select(mixd => mixd.Department, Name = $"({mixd.Total}")
        //.Frist();

                 
        }

        private void AddSalesButton_Click(object sender, RoutedEventArgs e)
        {
            int managersCount = dataContext.Managers.Count();
            int productsCount = dataContext.Products.Count();
            for (int i = 0; i < 10; i++)
            {
                dataContext.Sales.Add(new()
                {
                    Id = Guid.NewGuid(),
                    ManagerId = dataContext.Managers
                                .Skip(App.random.Next(managersCount))
                                .First()
                                .Id,
                    ProductId = dataContext.Products
                                .Skip(App.random.Next(productsCount))
                                .First()
                                .Id,
                    Cnt = App.random.Next(1, 10),
                    Moment = DateTime.Now
                });
            }
            dataContext.SaveChanges();

                MonitorSales.Content = dataContext.Sales.Count();
            ShowDailyStatistics();
        }

    }
}
