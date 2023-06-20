using System.Windows;
using System.Windows.Media.Imaging;

namespace WetterApp
{
    public partial class MainWindow : Window
    {
        private WeatherViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new WeatherViewModel();
            this.DataContext = _viewModel;

            _viewModel.InitialUpdateUI("Bielefeld");

        }
    }
}
