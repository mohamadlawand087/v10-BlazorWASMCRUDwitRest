using System.Collections.Generic;
using System.Threading.Tasks;
using TodoClient.Models;

namespace TodoClient.Services
{
    public interface ITodoDataService
    {
         Task<IEnumerable<ItemData>> GetAllItems();
         Task<ItemData> GetItemDetails(int id);
         Task<ItemData> AddItem(ItemData item);
         Task UpdateItem(ItemData item);
         Task DeleteItem(int id);
    }
}