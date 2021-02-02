using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TodoClient.Models;
using TodoClient.Services;

namespace TodoClient.Pages
{
    public partial class ItemOverview
    {
        public IEnumerable<ItemData> Items {get;set;}

        [Inject]
        public ITodoDataService TodoDataService {get;set;}

        protected async override Task OnInitializedAsync()
        {
            Items = (await TodoDataService.GetAllItems()).ToList();
        }
    }
}