using Android.Util;
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
using TestProject.Models;
using TestProject.Utility;
using TestProject.ViewsModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNote : ContentPage
    {
        MediaFile photo = null;

        public TodoItem TodoItems;

        public ItemWithPhotos itemPhotos;

        public static List<Photo> Photos = new List<Photo>();

        public MediaViewModel ViewModel
        {
            get { return BindingContext as MediaViewModel; }
        }


        string photoName = DateTime.Now.Date.ToString("ddMMyyyymmhhMMss") + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4) + ".jpg";

        public AddNote()
        {
            InitializeComponent();

            CameraButton.Clicked += CameraButton_Clicked;

        }
        private async void Add_Photo_Tapped1(object sender, EventArgs e)
        {
            LastSelectedType = 1;
            await AddPhoto(1);
        }

        private async void Add_Photo_Tapped2(object sender, EventArgs e)
        {
            LastSelectedType = 2;
            await AddPhoto(2);
        }

        private async void Add_Photo_Tapped3(object sender, EventArgs e)
        {
            LastSelectedType = 3;
            await AddPhoto(3);
        }

        private int LastSelectedType = -1;

        private async Task AddPhoto(int type)
        {
            if (photo != null)
            {

                switch (type)
                {
                    case 1:
                        AddPhotoStack1.IsVisible = false;
                        PhotoStack1.IsVisible = true;
                        break;
                    case 2:
                        AddPhotoStack2.IsVisible = false;
                        PhotoStack2.IsVisible = true;
                        break;
                    case 3:
                        AddPhotoStack3.IsVisible = false;
                        PhotoStack3.IsVisible = true;
                        break;
                    default:
                        break;
                }

                var imageService = DependencyService.Get<IImageService>();
                var result = imageService.ResizeImage(photo.GetStream().ReadFully(), 1200, 768);
                if (result.Length > 3145728)
                {
                    await DisplayAlert("UYARI", "En fazla 3mb dosya ekleyebilirsibniz.", "OK");
                }
                else
                {
                    Photos.Add(new Photo()
                    {
                        DocumentName = photoName,
                        DocumentPath = photo.Path,
                        SortingId = type
                    });
                }

                switch (type)
                {
                    case 1:
                        Photo1.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                        break;
                    case 2:
                        Photo2.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                        break;
                    case 3:
                        Photo3.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                        break;
                    default:
                        break;
                }

                photo = null;
            }
            else
            {
                Slide.IsVisible = false;
                Camera_View.IsVisible = true;
            }
        }

        // int cnt = 1;
        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            Camera_View.IsVisible = false;
            PhotoImage.IsVisible = true;
            Slide.IsVisible = true;
            PhotoImage.IsVisible = false;

            photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                PhotoSize = PhotoSize.Small,
                CompressionQuality = 92,
                CustomPhotoSize = 90,
                AllowCropping = true,
                Directory = "TodoListPhoto",
                Name = photoName,
                DefaultCamera = CameraDevice.Rear
            });

            await AddPhoto(LastSelectedType);


            //if (photo != null)
            //{
            //    if (cnt == 1)
            //    {
            //        AddPhotoStack1.IsVisible = false;
            //        PhotoStack1.IsVisible = true;

            //        var imageService = DependencyService.Get<IImageService>();
            //        var result = imageService.ResizeImage(photo.GetStream().ReadFully(), 1200, 768);
            //        if (result.Length > 3145728)
            //        {
            //            await DisplayAlert("UYARI", "En fazla 3mb dosya ekleyebilirsibniz.", "OK");
            //        }
            //        else
            //        {
            //            Photos.Add(new Photo()
            //            {
            //                DocumentName = photoName,
            //                DocumentPath = photo.Path,
            //                SortingId = 1
            //            });
            //        }

            //        Photo1.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            //        cnt++;
            //    }
            //    else if (cnt == 2)
            //    {
            //        AddPhotoStack2.IsVisible = false;
            //        PhotoStack2.IsVisible = true;

            //        var imageService = DependencyService.Get<IImageService>();
            //        var result = imageService.ResizeImage(photo.GetStream().ReadFully(), 1200, 768);
            //        if (result.Length > 3145728)
            //        {
            //            await DisplayAlert("UYARI", "En fazla 3mb dosya ekleyebilirsibniz.", "OK");
            //        }
            //        else
            //        {
            //            Photos.Add(new Photo()
            //            {
            //                DocumentName = photoName,
            //                DocumentPath = photo.Path,
            //                SortingId = 2
            //            });
            //        }

            //        Photo2.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            //        cnt++;
            //    }
            //    else if (cnt == 3)
            //    {
            //        AddPhotoStack3.IsVisible = false;
            //        PhotoStack3.IsVisible = true;

            //        var imageService = DependencyService.Get<IImageService>();
            //        var result = imageService.ResizeImage(photo.GetStream().ReadFully(), 1200, 768);
            //        if (result.Length > 3145728)
            //        {
            //            await DisplayAlert("UYARI", "En fazla 3mb dosya ekleyebilirsibniz.", "OK");
            //        }
            //        else
            //        {
            //            Photos.Add(new Photo()
            //            {
            //                DocumentName = photoName,
            //                DocumentPath = photo.Path, 
            //                SortingId = 3
            //            });
            //        }

            //        Photo3.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            //        cnt++;
            //    }

            //    foreach (var photo in Photos)
            //    {
            //        Log.Error("Fotoğraf eklendi!", photo.DocumentPath);
            //    }

            //    photo.Dispose();
            //}

        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Dikkat", "İnternet bağlantınızı kontrol ediniz!", "Ok");
            }
            else
            {
                var resultItemId = await App.Database.SaveItemAsync(new TodoItem()
                {
                    ArticleDate = DateTime.Now,
                    Title = Title.Text,
                    Description = Detail.Text
                });

                foreach (var item in Photos)
                {
                    item.ItemId = resultItemId;
                }

                var result = await App.Database.SavePhotosAsync(Photos);

                await DisplayAlert("Başarılı", "Notunuz kayıt edildi.", "Ok");
            }
        }

        //private void Add_Photo_Tapped(object sender, EventArgs e)
        //{
        //    Slide.IsVisible = false;
        //    Camera_View.IsVisible = true;
        //}

        private void PhotoFromGallery_Clicked(object sender, EventArgs e)
        {
            //photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
            //{
            //    PhotoSize = PhotoSize.Small,
            //    CompressionQuality = 92,
            //    CustomPhotoSize = 90,
            //});

            //if (photo != null)
            //{
            //    if (cnt == 1)
            //    {
            //        AddPhotoStack1.IsVisible = false;
            //        PhotoStack1.IsVisible = true;
            //        ViewModel.AddMedia(new Photo()
            //        {
            //            DocumentName = photoName,
            //            DocumentPath = photo.Path
            //        });

            //        Photo1.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            //        cnt++;
            //    }
            //    else if (cnt == 2)
            //    {
            //        AddPhotoStack2.IsVisible = false;
            //        PhotoStack2.IsVisible = true;
            //        ViewModel.AddMedia(new Photo()
            //        {
            //            DocumentName = photoName,
            //            DocumentPath = photo.Path
            //        });

            //        Photo2.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            //        cnt++;
            //    }
            //    else if (cnt == 3)
            //    {
            //        AddPhotoStack3.IsVisible = false;
            //        PhotoStack3.IsVisible = true;
            //        ViewModel.AddMedia(new Photo()
            //        {
            //            DocumentName = photoName,
            //            DocumentPath = photo.Path
            //        });

            //        Photo3.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            //        cnt++;
            //    }

            //    photo.Dispose();
            //}

        }

        private void Detail_Photo_Tapped(object sender, TappedEventArgs e)
        {
            //var document = (Photo)e.Parameter;
            //await Navigation.PushAsync(new MediaDetailPage(document));
        }

        Photo photos = new Photo();
        private void Delete_Photo_Tapped1(object sender, EventArgs e)
        {
            int sortingId = 1;
            foreach (var photo in Photos.ToList())
            {
                if (photo.SortingId == sortingId)
                    Photos.Remove(photo);
            }

            PhotoStack1.IsVisible = false;
            AddPhotoStack1.IsVisible = true;
        }

        private void Delete_Photo_Tapped2(object sender, EventArgs e)
        {
            int sortingId = 2;
            foreach (var photo in Photos.ToList())
            {
                if (photo.SortingId == sortingId)
                    Photos.Remove(photo);
            }

            PhotoStack2.IsVisible = false;
            AddPhotoStack2.IsVisible = true;
        }

        private void Delete_Photo_Tapped3(object sender, EventArgs e)
        {
            int sortingId = 3;
            foreach (var photo in Photos.ToList())
            {
                if (photo.SortingId == sortingId)
                    Photos.Remove(photo);
            }

            PhotoStack3.IsVisible = false;
            AddPhotoStack3.IsVisible = true;
        }


    }
}