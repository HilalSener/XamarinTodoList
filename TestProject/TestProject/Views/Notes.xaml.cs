using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DAL;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notes : ContentPage
    {
        public Notes()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetItemsAsync();
        }
    }
}