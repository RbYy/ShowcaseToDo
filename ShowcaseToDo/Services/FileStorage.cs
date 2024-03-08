using Newtonsoft.Json;
using ShowCaseToDo.Models;

namespace ShowCaseToDo.Services
{
    internal class FileStorage : IStorage<Item>, ISortable
    {
        List<Item> items;
        bool initialized;
        public async Task DeleteAsync(Item item)
        {
            items.Remove(item);
            await File.WriteAllTextAsync(App.DataFilePath, JsonConvert.SerializeObject(items));
        }

        public Item Get(int id)
        {
            return items.FirstOrDefault(i => i.Id == id);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            if (initialized) return items;
            if (!File.Exists(App.DataFilePath)) 
                return new List<Item>();
            var jsonString = await File.ReadAllTextAsync(App.DataFilePath);
            if (jsonString == null) 
                return new List<Item>(); 
            items = JsonConvert.DeserializeObject<List<Item>>(jsonString);
            initialized = true;
            return items;
        }

        public async Task<Item> CreateAsync(Item item)
        {
            items.Add(item);
            await File.WriteAllTextAsync(App.DataFilePath, JsonConvert.SerializeObject(items));
            return item;
        }

        public async Task<Item> UpdateAsync(Item newItem)
        {
            var item = items.FirstOrDefault(i => i.Id == newItem.Id);
            item = newItem;
            await File.WriteAllTextAsync(App.DataFilePath, JsonConvert.SerializeObject(items));
            return item;
        }

        public async Task MoveToNewPosition(int oldIndex, int newIndex)
        {
            items = (List<Item>)await GetAllAsync();
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
            await File.WriteAllTextAsync(App.DataFilePath, JsonConvert.SerializeObject(items));
        }
    }
}
