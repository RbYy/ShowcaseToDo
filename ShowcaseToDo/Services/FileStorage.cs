using Newtonsoft.Json;
using ShowCaseToDo.Models;

namespace ShowCaseToDo.Services
{
    internal class FileStorage<T> : IDataAccessService<T>, ISortable where T : IIdentifiable<string>
    {
        string DataFilePath
        {
            get
            {
                var appSubdirectory = Path.Combine(FileSystem.AppDataDirectory, AppInfo.Current.Name);
                var filePath = Path.Combine(appSubdirectory, "data.json");
                if (!Directory.Exists(appSubdirectory))
                {
                    Directory.CreateDirectory(appSubdirectory);
                }
                return filePath;
            }
        }

        List<T> items;
        bool initialized;
        public async Task DeleteAsync(T item) 
        {
            T itemToRemove = items.FirstOrDefault(i => i.Id == item.Id);
            if (itemToRemove == null)
                return;
            _ = items?.Remove(itemToRemove);
            await File.WriteAllTextAsync(DataFilePath, JsonConvert.SerializeObject(items));
        }

        public T? Get(string id)
        {
            return items.FirstOrDefault(i => i.Id == id);
        }

        public async Task<List<T>> GetAllAsync()
        {       
            if (initialized) return items;
            if (!File.Exists(DataFilePath)) 
                return items = new List<T>();
            var jsonString = await File.ReadAllTextAsync(DataFilePath);
            if (jsonString == null) 
                return new List<T>(); 
            items = JsonConvert.DeserializeObject<List<T>>(jsonString);
            initialized = true;
            return items;
        }

        public async Task<T> CreateAsync(T item)
        {
            items.Add(item);
            await File.WriteAllTextAsync(DataFilePath, JsonConvert.SerializeObject(items));
            return item;
        }

        public async Task<T> UpdateAsync(T newItem)
        {
            var item = items.FirstOrDefault(i => i.Id == newItem.Id);
            item = newItem;
            await File.WriteAllTextAsync(DataFilePath, JsonConvert.SerializeObject(items));
            return item;
        }

        public async Task MoveToNewPosition(int oldIndex, int newIndex)
        {
            items = await GetAllAsync();
            var itemToMove = items[oldIndex];
            items.RemoveAt(oldIndex);

            if (newIndex < items.Count)
            {
                items.Insert(newIndex, itemToMove);
            }
            else
            {
                items.Add(itemToMove);
            }
            await File.WriteAllTextAsync(DataFilePath, JsonConvert.SerializeObject(items));
        }
    }
}
