using AvaloniaTodoListApp.DataModels;
using AvaloniaTodoListApp.Services;
using ReactiveUI;
using System;
using System.Reactive.Linq;

namespace AvaloniaTodoListApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _content;
        // 这个视图模型依赖于 ToDoListService
        private TodoListServices service;

        // Put data into Container ---- 依赖
        public MainWindowViewModel() 
        {
            service = new TodoListServices();
            ToDoList = new TodoListViewModel(service.GetItemsFromJson());
            _content = ToDoList;
        }
        
        public ViewModelBase ContentViewModel 
        { 
            get => _content; 
            private set => this.RaiseAndSetIfChanged(ref _content, value); 
        }

        public TodoListViewModel ToDoList { get; }

        public void AddItem()
        {
            AddItemViewModel addItemVM = new();

            Observable.Merge(
                addItemVM.OkCommand,
                addItemVM.CancelCommand.Select(_ => ( TodoItem?)null)).Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        ToDoList.Items.Add(model);
                        // 将新的todoItme写入数据库
                        DBHelper.ExcuteNoneQuery($"Insert into items (Description) values ('{model.Description}')");
                    }
                    ContentViewModel = ToDoList;
                });

            ContentViewModel = addItemVM;
        }


    }
}