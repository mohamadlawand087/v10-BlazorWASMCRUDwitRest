using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TodoClient.Models;
using TodoClient.Services;

namespace TodoClient.Pages
{
    public partial class ItemEdit
    {
        // State Management
        protected string Message = string.Empty;
        protected bool Saved;

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public ITodoDataService TodoDataService {get;set;}

        [Parameter]
        public string Id { get; set; }

        public ItemData Item { get; set; } = new ItemData();

        protected async override Task OnInitializedAsync()
        {
            Saved = false;

            if(!String.IsNullOrEmpty(Id))
            {
                var itemId = Convert.ToInt32(Id);
                Item = await TodoDataService.GetItemDetails(itemId);
            }
           
        }

        protected async Task HandleValidRequest()
        {
            if(String.IsNullOrEmpty(Id)) // We need to add the item
            {
                var res = await TodoDataService.AddItem(Item);

                if(res != null)
                {
                    Saved = true;
                    Message = "Item has been added";
                } else
                {
                    Message = "Something went wrong";
                }
            } else // We are updating the item
            {
                await TodoDataService.UpdateItem(Item);
                Saved = true;
                Message = "Item has been updated";
            }
        }

        protected void HandleInvalidRequest()
        {
            Message = "Failed to submit form";
        }

        protected void goToOverview()
        {
            navigationManager.NavigateTo("/ItemOverview");
        }

        protected async Task DeleteItem()
        {
            if(!String.IsNullOrEmpty(Id))
            {
                var itemId = Convert.ToInt32(Id);
                await TodoDataService.DeleteItem(itemId);

                navigationManager.NavigateTo("/ItemOverview");
            }

            Message = "Something went wrong, unable to delete";
        }
    }
}