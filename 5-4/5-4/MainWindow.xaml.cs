using System;
using System.Windows;
using CityLibrary;

namespace CityApp
{
    public partial class MainWindow : Window
    {
        private City _city;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameInput.Text;

                if (!int.TryParse(PopulationInput.Text, out int population) || population <= 0)
                    throw new ArgumentException("Численность населения должна быть положительным числом.");

                if (!double.TryParse(AreaInput.Text, out double area) || area <= 0)
                    throw new ArgumentException("Площадь должна быть положительным числом.");

                if (!int.TryParse(LandmarksInput.Text, out int landmarks) || landmarks < 0)
                    throw new ArgumentException("Количество достопримечательностей не может быть отрицательным.");

                if (!double.TryParse(DailyCostInput.Text, out double dailyCost) || dailyCost <= 0)
                    throw new ArgumentException("Стоимость проживания должна быть положительным числом.");

                if (!int.TryParse(LandmarksPerDayInput.Text, out int landmarksPerDay) || landmarksPerDay <= 0)
                    throw new ArgumentException("Количество достопримечательностей в день должно быть больше 0.");

                _city = new City(name, population, area, landmarks, dailyCost);

                // Расчёты
                double density = _city.GetPopulationDensity();
                int daysToVisit = _city.GetDaysToVisit(landmarksPerDay);

                Output.Text = $"Город: {name}\n" +
                              $"Плотность населения: {density:F2} чел./км²\n" +
                              $"Дней на посещение: {daysToVisit}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
