using ReactiveUI;
using System.Reactive;

namespace AvaloniaTodoListApp.ViewModels
{
    public class TextViewModel : ViewModelBase
    {
        private string _content = string.Empty;

        public ReactiveCommand<Unit, string> EditCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public TextViewModel(string content) 
        { 
            var isValidObservable = this.WhenAnyValue(x => x.Content, x => !string.IsNullOrWhiteSpace(x));
            Content = content;

            EditCommand = ReactiveCommand.Create(() => Content, isValidObservable);
            CancelCommand = ReactiveCommand.Create(() => { });
        }

        public string Content
        {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }
    }
}
