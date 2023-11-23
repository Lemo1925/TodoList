using Avalonia.ReactiveUI;
using AvaloniaTodoListApp.Services;
using AvaloniaTodoListApp.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace AvaloniaTodoListApp.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(action => action(ViewModel!.showEdit.RegisterHandler(DoShowDialogAsync)));

            Closing += (sender, e) => TodoListServices.SaveJson();
        }

        private async Task DoShowDialogAsync(InteractionContext<EditWindowViewModel, TextViewModel?> interaction)
        {
            var dialog = new EditWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<TextViewModel?>(this);
            interaction.SetOutput(result);
        }
    }
}