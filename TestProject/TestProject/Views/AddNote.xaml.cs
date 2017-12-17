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

        public static List<Photo> Photos = new List<Photo>();

        public MediaViewModel ViewModel
        {
            get { return BindingContext as MediaViewModel; }
        }


        string photoName = DateTime.Now.Date.ToString("ddMMyyyymmhhMMss") + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4) + ".jpg";

        public AddNote()
        {
            InitializeComponent();
            //BindingContext = new MediaViewModel(todoItems);
            //TodoItems = todoItems;

            CameraButton.Clicked += CameraButton_Clicked;
        }

        private void Delete_Photo_Tapped(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            var tapGestureRecognizer = (TapGestureRecognizer)label.GestureRecognizers.FirstOrDefault();

            ViewModel.RemoveMedia((Photo)tapGestureRecognizer.CommandParameter);
        }

        int cnt = 1;
        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            Camera_View.IsVisible = false;
            PhotoImage.IsVisible = true;
            Slide.IsVisible = true;
            PhotoImage.IsVisible = false;

            photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                PhotoSize = PhotoSize.Custom,
                CompressionQuality = 92,
                CustomPhotoSize = 90,
                AllowCropping = true,
                Directory = "TodoListPhoto",
                Name = photoName,
                DefaultCamera = CameraDevice.Rear
            });

            if (photo != null)
            {
                if (cnt == 1)
                {
                    AddPhotoStack1.IsVisible = false;
                    PhotoStack1.IsVisible = true;

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
                            DocumentPath = photo.Path
                        });
                    }

                    Photo1.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    cnt++;
                }
                else if (cnt == 2)
                {
                    AddPhotoStack2.IsVisible = false;
                    PhotoStack2.IsVisible = true;

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
                            DocumentPath = photo.Path
                        });
                    }

                    //ViewModel.AddMedia(new Photo()
                    //{
                    //    DocumentName = photoName,
                    //    DocumentPath = photo.Path
                    //});

                    //Photo2.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    cnt++;
                }
                else if (cnt == 3)
                {
                    AddPhotoStack3.IsVisible = false;
                    PhotoStack3.IsVisible = true;

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
                            DocumentPath = photo.Path
                        });
                    }

                    //ViewModel.AddMedia(new Photo()
                    //{
                    //    DocumentName = photoName,
                    //    DocumentPath = photo.Path
                    //});

                    //Photo3.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    cnt++;
                }
                
                foreach (var photo in Photos)
                {
                    Log.Error("Fotoğraf eklendi!", photo.DocumentPath);

                    //System.Diagnostics.Debug.WriteLine("Fotoğraf eklendi!", photo);
                }

                photo.Dispose();
            }

        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Dikkat", "İnternet bağlantınızı kontrol ediniz!", "Ok");
            }
            else
            {
                //bool success = await ViewModel.ExecuteLoadItemsCommand(TodoItems);
                await App.Database.SaveItemAsync(new TodoItem()
                {
                    ArticleDate = DateTime.Now,
                    Title = Title.Text,
                    Description = Detail.Text
                });

                await DisplayAlert("Başarılı", "Notunuz kayıt edildi.", "Ok");
            }
        }

        private void Add_Photo_Tapped(object sender, EventArgs e)
        {
            Slide.IsVisible = false;
            Camera_View.IsVisible = true;
        }

        private async void PhotoFromGallery_Clicked(object sender, EventArgs e)
        {
            photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Small,
                CompressionQuality = 92,
                CustomPhotoSize = 90,
            });

            if (photo != null)
            {
                if (cnt == 1)
                {
                    AddPhotoStack1.IsVisible = false;
                    PhotoStack1.IsVisible = true;
                    ViewModel.AddMedia(new Photo()
                    {
                        DocumentName = photoName,
                        DocumentPath = photo.Path
                    });

                    Photo1.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    cnt++;
                }
                else if (cnt == 2)
                {
                    AddPhotoStack2.IsVisible = false;
                    PhotoStack2.IsVisible = true;
                    ViewModel.AddMedia(new Photo()
                    {
                        DocumentName = photoName,
                        DocumentPath = photo.Path
                    });

                    Photo2.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    cnt++;
                }
                else if (cnt == 3)
                {
                    AddPhotoStack3.IsVisible = false;
                    PhotoStack3.IsVisible = true;
                    ViewModel.AddMedia(new Photo()
                    {
                        DocumentName = photoName,
                        DocumentPath = photo.Path
                    });

                    Photo3.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    cnt++;
                }

                photo.Dispose();
            }

        }

        private async void Detail_Photo_Tapped(object sender, TappedEventArgs e)
        {
            var document = (Photo)e.Parameter;
            await Navigation.PushAsync(new MediaDetailPage(document));
        }
    }
}