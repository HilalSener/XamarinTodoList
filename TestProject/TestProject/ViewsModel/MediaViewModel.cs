using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestProject.DAL;
using TestProject.Interfaces;
using TestProject.Models;
using TestProject.Utility;
using Xamarin.Forms;

namespace TestProject.ViewsModel
{
    public class MediaViewModel
    {
        private Photo Photos;

        public MediaViewModel(Photo photo)
        {
            Photos = photo;
            BindingData();
        }

        private ObservableCollection<Photo> mediaModel = new ObservableCollection<Photo>();

        public ObservableCollection<Photo> MediaModel
        {
            get { return mediaModel; }
            set { mediaModel = value; OnPropertyChanged(); }
        }

        public void BindingData()
        {
            MediaModel = new ObservableCollection<Photo>();
        }

        public void AddMedia(Photo media)
        {
            //Items.Photos.Add(media);
            MediaModel.Add(media);
        }

        public void RemoveMedia(Photo media)
        {
            MediaModel.Remove(media);
            //Items.Photos.Remove(media);
        }

        //public async Task<bool> ExecuteLoadItemsCommand(Photo todoItems)
        //{
        //    if (todoItems. != null)
        //    {
        //        foreach (var document in todoItems.Photos)
        //        {
        //            var imageService = DependencyService.Get<IImageService>();
        //            var media = await document.DocumentPath.FileToByteArray();
        //            var result = imageService.ResizeImage(media, 1200, 768);
        //            document.Document = Convert.ToBase64String(result);
        //        }
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
