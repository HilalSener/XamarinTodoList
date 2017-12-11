using Java.Util;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestProject
{
    public partial class MainPage : TabbedPage
    {
        private bool _isRunning;
        public MainPage()
        {
            InitializeComponent();
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            ToolbarItems.Clear();

            if (!CrossConnectivity.Current.IsConnected)
            {
                this.ToolbarItems.Add(Status0);
                this.ToolbarItems.Add(Timer);

                _isRunning = true;
                RunTimer();
            }
            else
            {
                this.ToolbarItems.Add(Status1);
                _isRunning = false;
            }

            //if (!CrossConnectivity.Current.IsConnected)
            //    this.ToolbarItems.Add(new ToolbarItem() { Icon = "red_circle.png" });
            //else
            //    this.ToolbarItems.Add(new ToolbarItem() { Icon = "green_circle.png" });
        }

        public void RunTimer()
        {
            int i = 1;
            int k = 1;
            TimeSpan tmspan = new TimeSpan(00, 00, 00);

            Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
            {
                if (i == 60*5*k)
                {
                    DisplayAlert("Bilgi", "5 dk dır internete bağlı değilsiniz!", "Ok");
                    k++;
                }
                else
                {
                    //Timer.Text = DateTime.Now.ToString("HH:mm:ss");*,
                    Timer.Text = string.Format("{0:hh\\:mm\\:ss}", tmspan.Add(TimeSpan.FromSeconds(i)));
                    i += 1;
                }
                return _isRunning;

            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ToolbarItems.Clear();

            if (!CrossConnectivity.Current.IsConnected)
            {
                this.ToolbarItems.Add(Status0);
                this.ToolbarItems.Add(Timer);

                _isRunning = true;
                RunTimer();
            }
            else
            {
                //this.ToolbarItems.Add(new ToolbarItem() { Icon = "green_circle.png" });
                this.ToolbarItems.Add(Status1);
                _isRunning = false;
            }
        }
    }
}
