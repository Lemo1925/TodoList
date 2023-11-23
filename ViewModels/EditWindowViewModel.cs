using AvaloniaTodoListApp.DataModels;
using AvaloniaTodoListApp.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace AvaloniaTodoListApp.ViewModels
{
    public partial class EditWindowViewModel : ViewModelBase
    {
        private ViewModelBase _windowContent;

        public ViewModelBase ContentViewModel 
        { 
            get => _windowContent; 
            private set => this.RaiseAndSetIfChanged(ref _windowContent, value);
        }

        public string Description { get; set; } = string.Empty;

        public TextViewModel TextVM { get; }

        public EditWindowViewModel(string destription) 
        {
            Description = destription;
            TextVM = new TextViewModel(Description);
            _windowContent = TextVM;
        }

        public void UpdateText(ObservableCollection<TodoItem> todolist)
        { 
            _ = Observable.Merge(
                TextVM.EditCommand,
                TextVM.CancelCommand.Select(_ => (string?)null)).Take(1)
                .Subscribe(content =>
                {
                    if (content != null)
                    {
                        // 更新todolist Itmes
                        var item = todolist.FirstOrDefault(i => i.Description == Description);
                        if (item!.Description == Description)
                        {
                            todolist.Replace(item,
                                new TodoItem { Description = content, IsChecked = item.IsChecked });
                        }

                        // 同步更新DataTable
                        TodoListServices.EditItem(Description, content);
                    }
                    // 关闭EditWindow
                    CloseWindow();
                });
        }

        [RelayCommand]
        void CloseWindow()
        {
            WeakReferenceMessenger.Default.Send(new CloseWindowMessage
            {
                Sender = new WeakReference(this)
            });
        }
    }
}
