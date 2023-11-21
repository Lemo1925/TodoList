using Microsoft.VisualBasic;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvaloniaTodoListApp.ViewModels
{
    public class EditWindowViewModel : ViewModelBase
    {
        public string TextContent { get; set; } = string.Empty;

        private ViewModelBase _text;

        public EditWindowViewModel(string destription) 
        {
            TextContent = destription;
            TextViewModel EditText = new TextViewModel();
            _text = EditText;
        }

        public ViewModelBase ContentViewModel 
        { 
            get => _text; 
            private set => this.RaiseAndSetIfChanged(ref _text, value);
        }
    }
}
