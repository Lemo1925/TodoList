using AvaloniaTodoListApp.DataModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AvaloniaTodoListApp.ViewModels
{
    public class TodoListViewModel:ViewModelBase
    {
        // 接收一个待办事项数据模型的集合，并将其保存在可观察的集合中

        // Data Container
        public TodoListViewModel(IEnumerable<TodoItem> items)
        {
            Items = new ObservableCollection<TodoItem>(items);
        }

        public ObservableCollection<TodoItem> Items { get; }
    }
}
