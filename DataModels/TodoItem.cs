using AvaloniaTodoListApp.Services;
using System.ComponentModel;

namespace AvaloniaTodoListApp.DataModels
{
    // 数据实体
    public class TodoItem : INotifyPropertyChanged
    {
        private bool _isChecked;

        // Data Model 
        public string Description { get; set; } = string.Empty;
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
                        DBHelper.ExcuteNoneQuery($"Update items set ischecked = 1 where description = '{Description}'");
                    
                    else
                        DBHelper.ExcuteNoneQuery($"Update items set ischecked = 0 where description = '{Description}'");
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
 