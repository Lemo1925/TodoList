using Avalonia.Controls;
using AvaloniaTodoListApp.Services;

namespace AvaloniaTodoListApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += (sender, e) => TodoListServices.SaveJson();
        }
    }
}