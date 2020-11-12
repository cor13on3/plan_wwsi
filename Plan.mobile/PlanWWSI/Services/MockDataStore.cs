using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App1.Models;

namespace App1.Services
{
    public class MockDataStore<T> : IDataStore<T> where T : ModelBase
    {
        List<T> items;

        public MockDataStore()
        {
            items = new List<T>();
        }

        public async Task<bool> AddItemAsync(T item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            var oldItem = items.Where((T arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((T arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<T> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string data, bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}