using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesDetailPage : ContentPage
    {
        public ItemWithPhotos item { get; set; }
        public NotesDetailPage()
        {
            InitializeComponent();
        }

        public NotesDetailPage(ItemWithPhotos dto)
        {
            item.Id = dto.Id;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //MessagingCenter.Subscribe<ItemWithPhotos>(this, "documentInfo", async (args) =>
            //{
            //    listView.ItemsSource = await App.Database.GetItemsAsync();
            //});

        }

    }
}
