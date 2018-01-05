using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DAL;
using TestProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notes : ContentPage
    {
        public List<ItemWithPhotos> itemWithPhotos;
        public List<Photo> PhotoList = new List<Photo>();
        public Notes()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            itemWithPhotos = App.Database.GetItemsAsync();
            

            foreach (var item in itemWithPhotos)
            {
                foreach (var photo in item.Photos)
                {
                    photo.DocumentStr = new MemoryStream(photo.Document);
                    PhotoList.Add(photo);
                }
            }
        }
    }
}