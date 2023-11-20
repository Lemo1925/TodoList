using AvaloniaTodoListApp.Services;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AvaloniaTodoListApp.DataModels
{
    // 数据实体
    [DataContract]
    public class TodoItem : INotifyPropertyChanged
    {
        [DataMember]
        private bool _isChecked;

        // Data Model 
        [DataMember]
        public string Description { get; set; } = string.Empty;
        [DataMember]
        public string Date { get; set; } = DateTime.Now.ToString();
        public bool IsChecked 
        { 
            get => _isChecked;
            set 
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                    if (_isChecked) 
                    { 
                        //DBHelper.ExcuteNoneQuery($"Update items set ischecked = 1 where description = '{Description}'");
                        TodoListServices.CheckItem(Description, 1);
                    }
                    else
                    {
                        //DBHelper.ExcuteNoneQuery($"Update items set ischecked = 0 where description = '{Description}'");
                        TodoListServices.CheckItem(Description, 0);
                    }
                }
            } 
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(propertyName,new PropertyChangedEventArgs(propertyName));
        }
    }
}
 