using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestProject.DAL;
using TestProject.Interfaces;
using Xamarin.Forms;

namespace TestProject
{
    public partial class App : Application
    {
        static ItemsDb database;
        public static ItemsDb Database
        {
            get
            {

                if (database == null)
                {
                    string localFilePath = DependencyService.Get<IFileHelper>().GetLocalFilePath("test.db3");

                    database = new ItemsDb(localFilePath);
                }

                return database;
            }

        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TestProject.MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
