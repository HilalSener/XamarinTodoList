using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TestProject.Models;

namespace TestProject.DAL
{
    public class ItemsDb
    {
        readonly SQLiteAsyncConnection database;

        public ItemsDb(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<TodoItem>().Wait();
            database.CreateTableAsync<Photo>().Wait();
        }

        public List<ItemWithPhotos> GetItemsAsync()
        {
            var items = database.QueryAsync<ItemWithPhotos>("Select * from [TodoItem]").Result;

            foreach (var item in items)
            {
                item.Photos = database.QueryAsync<Photo>("Select * from [Photo] where ItemId=" + item.Id).Result;
            }

            return items;
        }

        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<Photo> GetItemAsync(int id)
        {
            return database.Table<Photo>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<TodoItem>> GetItemsWithPhotosAsync(List<Photo> photos)
        {
            if (photos != null && photos.Count > 0)
            {
                return database.QueryAsync<TodoItem>($"SELECT * FROM [TodoItem] INNER JOIN [Photo] ON [TodoItem.Id] = [Photo.ItemId]");
            }
            else
            {
                return null;
            }

        }

        public Task<int> SaveItemAsync(TodoItem item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                try
                {
                    database.InsertAsync(item);
                    var data = database.QueryAsync<TodoItem>($"SELECT Id FROM [TodoItem] ORDER BY Id DESC LIMIT 1");
                    var x = data.Result.FirstOrDefault().Id;
                    return Task.FromResult(x);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }

        public Task<int> SavePhotosAsync(List<Photo> photos)
        {
            //TODO: internet varsa servise kaydet
            return database.InsertAllAsync(photos);
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> DeletePhotoAsync(Photo photo)
        {
            return database.DeleteAsync(photo);
        }
    }
}
