using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DAL;
using TestProject.Interfaces;
using TestProject.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProject.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNote : ContentPage
    {
        MediaFile media = null;
        MediaFile photo = null;

        string photoName = DateTime.Now.Date.ToString("ddMMyyyymmhhMMss") + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4) + ".jpg";

        public AddNote()
        {
            InitializeComponent();

            CameraButton.Clicked += CameraButton_Clicked;
        }

        int cnt = 1;
        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            Camera_View.IsVisible = false;
            PhotoImage.IsVisible = true;
            Slide.IsVisible = true;
            PhotoImage.IsVisible = false;

            photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() { });

            if (photo != null)
            {
                var imageService = DependencyService.Get<IImageService>();
                //var result = imageService.ResizeImage(photo.GetStream().ReadFully(), 800, 600);
                //if (result.Length > 3145728)
                //{ }
                if (cnt == 1)
                {
                    AddPhotoStack1.IsVisible = false;
                    PhotoStack1.IsVisible = true;
                    Photo1.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    cnt++;
                }
                else if (cnt == 2)
                {
                    AddPhotoStack2.IsVisible = false;
                    PhotoStack2.IsVisible = true;
                    Photo2.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    cnt++;
                }
                else if (cnt == 3)
                {
                    AddPhotoStack3.IsVisible = false;
                    PhotoStack3.IsVisible = true;
                    Photo3.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    cnt++;
                }
            }
                
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                DisplayAlert("Dikkat", "İnternet bağlantınızı kontrol ediniz!", "Ok");
            }
            else
            {
                App.Database.SaveItemAsync(new TodoItem()
                {
                    ArticleDate = DateTime.Now,
                    Title = Title.Text,
                    Description = Detail.Text,
                    DocumentName = photoName,
                    DocumentPath = media.Path
                });

                DisplayAlert("Başarılı", "Notunuz kayıt edildi.", "Ok");
            }
        }

        private void Add_Photo_Tapped(object sender, EventArgs e)
        {
            Slide.IsVisible = false;
            Camera_View.IsVisible = true;
        }

        //private async void PhotoButton_Clicked(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();

        //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        await DisplayAlert("UYARI", "Cihazınızın kamerası aktif değil!", "OK");
        //        return;
        //    }

        //    media = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
        //    {
        //        PhotoSize = PhotoSize.Custom,
        //        CompressionQuality = 92,
        //        CustomPhotoSize = 90,
        //        AllowCropping = true,
        //        Directory = "TodoListPhoto",
        //        Name = photoName,
        //        DefaultCamera = CameraDevice.Rear
        //    });
        //}

        private async void PhotoFromGallery_Clicked(object sender, EventArgs e)
        {
            media = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Small,
                CompressionQuality = 92,
                CustomPhotoSize = 90,
            });
        }

        private void SaveMedia()
        {
            if (media != null)
            {
                var imageService = DependencyService.Get<IImageService>();
                var result = imageService.ResizeImage(media.GetStream().ReadFully(), 1200, 768);
                if (result.Length > 3145728)
                {
                    DisplayAlert("Bilgi", "En fazla 3mb dosya ekleyebilirsiniz.", "Tamam");
                }
                else
                {
                    
                }
                media.Dispose();
            }
            else
            {
                return;
            }
        }

        private void Delete_Photo_Tapped(object sender, EventArgs e)
        {
            //TODO:
        }
    }
}