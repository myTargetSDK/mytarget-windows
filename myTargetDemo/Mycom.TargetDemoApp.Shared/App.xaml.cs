using System;
using System.Linq;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Mycom.Target.Core.Facades;
using Mycom.TargetDemoApp.Views;

namespace Mycom.TargetDemoApp
{
    public sealed partial class App
    {
        private static Boolean IsPhone()
        {
#if WINDOWS_UWP
            return ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0);
#else
            return true;
#endif
        }

        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            AbstractAd.IsDebugMode = true;

            if (Window.Current.Content == null)
            {
                var rootView = new Frame
                               {
                                   ContentTransitions = new TransitionCollection
                                                        {
                                                            new EdgeUIThemeTransition { Edge = EdgeTransitionLocation.Bottom }
                                                        },
                                   CacheSize = 2
                               };
                Window.Current.Content = rootView;

                if (IsPhone())
                {
                    var statusBar = StatusBar.GetForCurrentView();
                    statusBar.BackgroundColor = Color.FromArgb(0xFF, 0xCC, 0x0, 0x0);
                    statusBar.BackgroundOpacity = 1.0;
                    statusBar.ForegroundColor = Colors.White;

                    HardwareButtons.BackPressed += (sender, args) =>
                                                   {
                                                       var popup = VisualTreeHelper.GetOpenPopups(Window.Current)
                                                                                   .FirstOrDefault();

                                                       if (popup != null)
                                                       {
                                                           popup.IsOpen = false;
                                                           args.Handled = true;
                                                       }
                                                       else if (rootView.CanGoBack)
                                                       {
                                                           args.Handled = true;
                                                           rootView.GoBack();
                                                       }
                                                   };
                }

                rootView.Navigate(typeof (StartPage));
            }

            Window.Current.Activate();
        }
    }
}