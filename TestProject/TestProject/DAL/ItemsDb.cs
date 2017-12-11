using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TestProject.DAL
{
    public class ItemsDb
    {
        readonly SQLiteAsyncConnection database;

        public ItemsDb(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TodoItem>().Wait();
        }
        public Task<List<TodoItem>> GetItemsAsync()
        {
            return database.Table<TodoItem>().ToListAsync();
        }

        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            return database.Table<TodoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return database.DeleteAsync(item);
        }
    }
}
