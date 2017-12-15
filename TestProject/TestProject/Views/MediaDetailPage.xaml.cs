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
    public partial class MediaDetailPage : ContentPage
    {
        public MediaDetailPage(Photo dto)
        {
            InitializeComponent();

            Title = dto.DocumentName;

            img.Source = dto.DocumentPath;
            img.IsVisible = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
