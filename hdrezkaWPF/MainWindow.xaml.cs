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

namespace hdrezkaWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new SearchPage());

            profileButton.Style = FindResource("SelectedNavigationButtonStyle") as Style;
            listTaskButton.Style = FindResource("UnselectedNavigationButtonStyle") as Style;
            addTaskButton.Style = FindResource("UnselectedNavigationButtonStyle") as Style;

            pagename.Text = "Поиск фильмов, сериалов, мультфильмов";


            // Обновление источника изображения кнопки
            Uri imageUri = new Uri("images/add.png", UriKind.Relative);
            ImageSource imageSource = new BitmapImage(imageUri);
            addTaskButton.Content = new System.Windows.Controls.Image { Source = imageSource, Width = 30, Height = 30 };

            // Обновление источника изображения кнопки
            Uri imageUri2 = new Uri("images/task.png", UriKind.Relative);
            ImageSource imageSource2 = new BitmapImage(imageUri2);
            listTaskButton.Content = new System.Windows.Controls.Image { Source = imageSource2, Width = 30, Height = 30 };

            // Обновление источника изображения кнопки
            Uri imageUri3 = new Uri("images/profile-white.png", UriKind.Relative);
            ImageSource imageSource3 = new BitmapImage(imageUri3);
            profileButton.Content = new System.Windows.Controls.Image { Source = imageSource3, Width = 30, Height = 30 };
        }

        private void listTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
