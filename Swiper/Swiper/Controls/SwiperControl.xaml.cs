using Swiper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Swiper.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwiperControl : ContentView
    {
        private readonly double _initialRotation;
        private static readonly Random _random = new Random();
        public SwiperControl()
        {
            InitializeComponent();

            var picture = new Picture();
            var panGesture = new PanGestureRecognizer();

            descriptionLabel.Text = picture.Description;

            image.Source = new UriImageSource() { Uri = picture.Uri };

            loadingLabel.SetBinding(IsVisibleProperty, "IsLoading");
            loadingLabel.BindingContext = image;

            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);

            _initialRotation = _random.Next(-10, 10);
            photo.RotateTo(_initialRotation, 100, Easing.SinOut);
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    PanStarted();
                    break;
                case GestureStatus.Running:
                    PanRunning(e);
                    break;
                case GestureStatus.Completed:
                    PanCompleted();
                    break;
            }
        }
        private void PanStarted()
        {
            photo.ScaleTo(1.1, 100);
        }
        private void PanRunning(PanUpdatedEventArgs e)
        {
            photo.TranslationX = e.TotalX;
            photo.TranslationY = e.TotalY;
            photo.Rotation = _initialRotation + (photo.TranslationX / 25);
        }
        private void PanCompleted()
        {
            photo.TranslateTo(0, 0, 250, Easing.SpringOut);
            photo.RotateTo(_initialRotation, 250, Easing.SpringOut);
            photo.ScaleTo(1, 250);
        }

        

        
    }
}