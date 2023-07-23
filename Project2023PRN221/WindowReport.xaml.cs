using LiveCharts;
using LiveCharts.Wpf;
using Project2023PRN221.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Project2023PRN221
{
    /// <summary>
    /// Interaction logic for WindowReport.xaml
    /// </summary>
    public partial class WindowReport : Window, INotifyPropertyChanged
    {
        private PRN221PROJECTContext context;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        public ObservableCollection<ChartValues<decimal>> ProductsPercentages { get; set; }
        public WindowReport()
        {
            InitializeComponent();

            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            context = new PRN221PROJECTContext();
            DataContext = this;

            var orderDetail = context.TblChiTietHds.ToList();
            int totalProductSold = (int)orderDetail.Sum(a => a.Soluong);
            ProductsPercentages = new ObservableCollection<ChartValues<decimal>>();
            foreach (var product in orderDetail)
            {
                decimal percentage = (decimal)product.Soluong / totalProductSold;
                ProductsPercentages.Add(new ChartValues<decimal> { percentage });
            }
        }
    }
}
