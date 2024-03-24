using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using ShowCaseToDo.Models;
using ShowCaseToDo.Services;

namespace ShowCaseToDo
{
    public partial class Home
    {
        bool overlayVisible;

        public IEnumerable<Item> items = []; 

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            items = await DataAccessService.GetAllAsync();
        }

        private async Task RemoveItem(Item item)
        {
            await DataAccessService.DeleteAsync(item);
        }

        private async Task AddItem()
        {
            var item = new Item();
            DialogParameters parameters = new()
            {
                Title = $"Create New Task",
                Width = "500px",
                TrapFocus = true,
                Modal = true,
                PreventScroll = false
            };

            IDialogReference dialog = await DialogService.ShowDialogAsync<EditDialog>(item, parameters);
            await dialog.Result;

            if (!string.IsNullOrWhiteSpace(item.Title) || !string.IsNullOrWhiteSpace(item.Details))
            {
                await DataAccessService.CreateAsync(item);
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            JS.InvokeVoidAsync("itemNoPadding");
        }

        private async Task UpdateStatus(Tuple<Status, Item> statusitem)
        {
            statusitem.Item2.Status = statusitem.Item1;
            await DataAccessService.UpdateAsync(statusitem.Item2);
        }

        private async Task ShowDetails(Item item)
        {
            DialogParameters parameters = new()
            {
                Title = $"Edit {item.Title}",
                Width = "500px",
                TrapFocus = true,
                Modal = true,
                PreventScroll = false
            };

            IDialogReference dialog = await DialogService.ShowDialogAsync<EditDialog>(item, parameters);
            await DataAccessService.UpdateAsync(item);
            overlayVisible = true;
            await Task.Delay(1000);
            overlayVisible = false;
        }
        private async Task SortList(FluentSortableListEventArgs args)
        {
            if (args is null || args.OldIndex == args.NewIndex)
                return;

            await DataAccessService.MoveToNewPosition(args.OldIndex, args.NewIndex);
        }



    }
}