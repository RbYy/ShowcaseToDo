using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using ShowCaseToDo.Models;

namespace ShowCaseToDo
{
    public partial class Home
    {
        bool overlayVisible;

        public IEnumerable<Item> items = Enumerable.Range(1, 10).Select(i =>
            new Item
            {
                Id = i,
                Title = $"Title {i}",
                Details = @"'ShowCaseToDo.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.2\System.Runtime.Numerics.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
                'ShowCaseToDo.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.2\System.Formats.Asn1.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
               CaseToDo.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.2\Microsoft.Win32.Primitives.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
                "
            }
        ).ToList();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            items = (List<Item>)await Storage.GetAllAsync();
        }

        private async Task RemoveItem(Item item)
        {
            await Storage.DeleteAsync(item);
            await File.WriteAllTextAsync(App.DataFilePath, JsonConvert.SerializeObject(items));
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
            DialogResult? result = await dialog.Result;


            if (!string.IsNullOrWhiteSpace(item.Title) || !string.IsNullOrWhiteSpace(item.Details))
            {
                await Storage.CreateAsync(item);
                overlayVisible = true;
                await Task.Delay(1000);
                overlayVisible = false;
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
            await Storage.UpdateAsync(statusitem.Item2);
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
            DialogResult? result = await dialog.Result;

            await File.WriteAllTextAsync(App.DataFilePath, JsonConvert.SerializeObject(items));
            overlayVisible = true;
            await Task.Delay(1000);
            overlayVisible = false;
        }
        private async Task SortList(FluentSortableListEventArgs args)
        {
            if (args is null || args.OldIndex == args.NewIndex)
                return;

            await Storage.MoveToNewPosition(args.OldIndex, args.NewIndex);
        }



    }
}