using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        IConnectivity connectivity;
        public MainViewModel(IConnectivity connectivity)
        {
            Items = new ObservableCollection<string>();
            this.connectivity = connectivity;
        }


        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        string text;

        [CommunityToolkit.Mvvm.Input.ICommand]
        async void Add()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return;

            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Uh ho!", "Sem conexão com a internet", "OK");
                return;
            }

            Items.Add(Text);
            Text = string.Empty;
        }

        [CommunityToolkit.Mvvm.Input.ICommand]
        void Delete(string task)
        {
            if (Items.Contains(task))
                Items.Remove(task);
        }

        [CommunityToolkit.Mvvm.Input.ICommand]
        async Task Tap(string task)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={task}",
                new Dictionary<string, object> {
                    {nameof(DetailPage), new object()},
                });
        }
    }
}
