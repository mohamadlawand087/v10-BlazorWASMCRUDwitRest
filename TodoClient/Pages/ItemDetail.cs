using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TodoClient.Models;
using TodoClient.Services;

namespace TodoClient.Pages
{
    public partial class ItemDetail
    {
        [Parameter]
        public string Id {get;set;}

        [Inject]
        public ITodoDataService TodoDataService { get; set;}

        public ItemData Item {get;set;} = new ItemData();

        protected async override Task OnInitializedAsync()
        {
            var itemId = Convert.ToInt32(Id);
            Item = await TodoDataService.GetItemDetails(itemId);
        }
    }
}