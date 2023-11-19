using AvaloniaTodoListApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.DataContracts;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AvaloniaTodoListApp.Services
{
    // 在真实的应用程序中，可能会使用 SQL 数据库，并基于 Microsoft Entity Framework 编写读取和写入数据的服务。
    // 为了简单这里只创建一个虚拟数据服务，它会像有一个数据库一样运行，但实际上只是在内存中使用一个数组。
    public class TodoListServices
    {
        private DataTable _data;

        // A Service to get data from file or DataBase;
        public TodoListServices() => _data = DBHelper.ExcuteQuery("Select * from items");

        public IEnumerable<TodoItem> GetItems()
         {
            List<TodoItem> list = new List<TodoItem>();
            foreach (DataRow row in _data.Rows) 
            {
                list.Add(new TodoItem { 
                    Description = Convert.ToString(row["Description"])!, 
                    IsChecked = Convert.ToUInt32(row["IsChecked"]) == 1 
                });
            }
            return list;
        }

        // 将Todo Item序列化成Json格式
        private string GetJson()
        {
            _data = DBHelper.ExcuteQuery("Select * from items"); 
            var stream = new MemoryStream();
            var serializer = new DataContractJsonSerializer(typeof(List<TodoItem>));
            serializer.WriteObject(stream, GetItems());
            byte[] dataJson = stream.ToArray();
            stream.Close();
            return Encoding.UTF8.GetString(dataJson, 0, dataJson.Length);
        }

        // 将Json文件存储到本地
        public void SaveJson() => File.WriteAllText("..\\..\\..\\Other\\todolist.json", GetJson());

        // 读取本地的Json文件
        public string LoadJson() => File.ReadAllText("..\\..\\..\\Other\\todolist.json");

        // 从Json中解析出Todo Item
        public IEnumerable<TodoItem> GetItemsFromJson()
        {
            var itemList = new List<TodoItem>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(LoadJson()));
            var serializer = new DataContractJsonSerializer (itemList.GetType());
            itemList = serializer.ReadObject(stream) as List<TodoItem>;
            return itemList!;
        }
    }
}
