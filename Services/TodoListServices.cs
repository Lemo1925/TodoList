using AvaloniaTodoListApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;

namespace AvaloniaTodoListApp.Services
{
    // 在真实的应用程序中，可能会使用 SQL 数据库，并基于 Microsoft Entity Framework 编写读取和写入数据的服务。
    // 为了简单这里只创建一个虚拟数据服务，它会像有一个数据库一样运行，但实际上只是在内存中使用一个数组。
    public class TodoListServices
    {
        DataTable _data;

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
    }
}
