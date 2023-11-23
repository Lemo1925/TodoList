using AvaloniaTodoListApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AvaloniaTodoListApp.Services
{
    // 数据表管理
    public class TodoListServices
    {
        private static DataTable? _data;
        private const string JsonFilePath = "..\\..\\..\\Other\\todolist.json";

        // A Service to get data from file or DataBase;
        public TodoListServices()
        {
            //_data = DBHelper.ExcuteQuery("Select * from items");
            _data = new DataTable();
            _data.Columns.Add("ID",typeof(int));
            _data.Columns.Add("Description");
            _data.Columns.Add("IsChecked", typeof(bool));
            _data.Columns.Add("Date");
        }

        // 从data里获取Todo Item
        public static IEnumerable<TodoItem> GetItems()
        {
            List<TodoItem> list = new List<TodoItem>();
            foreach (DataRow row in _data!.Rows) 
            {
                list.Add(new TodoItem { 
                    Description = (string)row["Description"], 
                    IsChecked = (bool)row["IsChecked"],
                    Date = (string)row["Date"]
                });
            }
            return list;
        }

        // 向data里面写入新增的Todo Item
        public static void AddItem(string Description)
        {
            TodoItem item = new TodoItem { Description = Description, IsChecked = false };
            _data!.Rows.Add(_data.Rows.Count, item.Description, item.IsChecked, item.Date);
        }

        // Check的时候同步更新data
        public static void CheckItem(string Description, int state)
        {
            foreach (DataRow row in _data!.Rows)
            {
                string description = Convert.ToString(row["Description"])!;
                if (description == Description)
                {
                    row["IsChecked"] = state;
                }
            }
        }

        /// <summary>
        /// 同步data里面Description的内容
        /// </summary>
        /// <param name="OriDes">需要修改的Item</param>
        /// <param name="NewDes">新的内容</param>
        public static void EditItem(string OriDes, string NewDes)
        {
            foreach(DataRow row in _data!.Rows)
            {
                string description = Convert.ToString(row["Description"])!;
                if (description == OriDes)
                {
                    row["Description"] = NewDes;
                    return;
                }
            }
        }

        // 将Todo Item序列化成Json格式
        private static string GetJson()
        {
            //_data = DBHelper.ExcuteQuery("Select * from items"); 
            var stream = new MemoryStream();
            var serializer = new DataContractJsonSerializer(typeof(List<TodoItem>));
            serializer.WriteObject(stream, GetItems());
            byte[] dataJson = stream.ToArray();
            stream.Close();
            return Encoding.UTF8.GetString(dataJson, 0, dataJson.Length);
        }

        // 将Json文件存储到本地
        public static void SaveJson() => File.WriteAllText(JsonFilePath, GetJson());

        // 读取本地的Json文件
        public string LoadJson() => File.ReadAllText(JsonFilePath);

        // 从Json中解析出Todo Item
        public IEnumerable<TodoItem> GetItemsFromJson()
        {
            var itemList = new List<TodoItem>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(LoadJson()));
            var serializer = new DataContractJsonSerializer (itemList.GetType());
            itemList = serializer.ReadObject(stream) as List<TodoItem>;

            // 将解析出来的Item放入data
            if (itemList != null)
            {
                foreach (var item in itemList)
                {
                    _data!.Rows.Add(_data.Rows.Count, item.Description, item.IsChecked, item.Date);
                }
            }

            return itemList!;
        }
    }
}
