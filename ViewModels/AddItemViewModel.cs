using AvaloniaTodoListApp.DataModels;
using ReactiveUI;
using System.Reactive;

namespace AvaloniaTodoListApp.ViewModels
{
    public class AddItemViewModel : ViewModelBase
    {
        private string _description = string.Empty;

        public ReactiveCommand<Unit, TodoItem> OkCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public AddItemViewModel() 
        {
            var isValidObservable = this.WhenAnyValue(x => x.Description, x => !string.IsNullOrWhiteSpace(x));

            OkCommand = ReactiveCommand.Create(() => new TodoItem { Description = Description }, isValidObservable);
            CancelCommand = ReactiveCommand.Create(() => { });
        }

        public string Description 
        { 
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value); 
        }
    }
}
