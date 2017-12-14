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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNote : ContentPage
    {
        MediaFile photo = null;

        //public List<Photo> Photos = new List<Photo>();
        
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

            if (photo != null)
            {
                //var imageService = DependencyService.Get<IImageService>();
                //var result = imageService.ResizeImage(photo.GetStream().ReadFully(), 1200, 768);
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

        //public async Task<bool> ExecuteLoadItemsCommand(FieldVisitDto fieldVisitDto)
        //{
        //    UserDialogs.Instance.ShowLoading(AppResources.loading, MaskType.Gradient);
        //    if (fieldVisitDto.FieldVisitDocument != null)
        //    {
        //        foreach (var document in fieldVisitDto.FieldVisitDocument)
        //        {
        //            if (document.DocumentTypeId == 1)
        //            {
        //                var imageService = DependencyService.Get<IImageService>();
        //                var media = await document.DocumentPath.FileToByteArray();
        //                var result = imageService.ResizeImage(media, 1200, 768);
        //                document.Document = Convert.ToBase64String(result);

        //            }
        //            else
        //            {
        //                document.Document = await document.DocumentPath.FileToBase64();
        //            }
        //        }

        //    }
        //    var resultFields = await _UserFormService.SendFieldVisit(SetAndGetData(fieldVisitDto));
        //    if (resultFields != null && resultFields.ResultCode == 0)
        //    {
        //        UserDialogs.Instance.HideLoading();
        //        UserDialogs.Instance.ShowSuccess(AppResources.proccesSucces);
        //        return true;
        //    }
        //    else
        //    {
        //        UserDialogs.Instance.HideLoading();
        //        UserDialogs.Instance.ShowError(AppResources.errormsg);
        //        return false;
        //    }


        //}

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
                    DocumentPath = photo.Path
                });

                DisplayAlert("Başarılı", "Notunuz kayıt edildi.", "Ok");
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

        private void SaveMedia()
        {
            if (photo != null)
            {
                var imageService = DependencyService.Get<IImageService>();
                var result = imageService.ResizeImage(photo.GetStream().ReadFully(), 1200, 768);
                if (result.Length > 3145728)
                {
                    DisplayAlert("Bilgi", "En fazla 3mb dosya ekleyebilirsiniz.", "Tamam");
                }
                else
                {
                    
                }
                photo.Dispose();
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