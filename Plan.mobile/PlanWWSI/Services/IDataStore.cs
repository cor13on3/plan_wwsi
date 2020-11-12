using App1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App1.Services
{
    public interface IDataStore<T> where T : ModelBase
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(string data, bool forceRefresh = false);
    }
}
