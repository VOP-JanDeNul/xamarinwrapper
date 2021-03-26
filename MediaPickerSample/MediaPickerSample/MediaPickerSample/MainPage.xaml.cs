using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Tesseract;
using Xamarin.Forms.PlatformConfiguration;

namespace MediaPickerSample
{
    public partial class MainPage : ContentPage
    {

        private readonly ITesseractApi _tesseractApi;
        private string fullFileName;

        public MainPage()
        {
            _tesseractApi = DependencyService.Get<Test>().GetTesseractApi();
            //_tesseractApi = Resolver.Resolve<ITesseractApi>();
            //TesseractApi api = new TesseractApi(ApplicationContext, AssetsDeployment.OncePerVersion);
            InitializeComponent();
        }

        private async void LoadImageButton_OnClicked(object sender, EventArgs e)
        {
            if (!_tesseractApi.Initialized)
                _tesseractApi.Init("eng");

            //var image = new Image { Source = "businesscard1.jpg" };
            //var tessResult = await _tesseractApi.SetImage(image.ToString());

            //if (tessResult)
            //{

            //    imageName.Source = image.Source;
            //    labelName.Text = _tesseractApi.Text;
            //}

            var photo = await MediaPicker.CapturePhotoAsync();

            if (photo != null)
            {
                // When setting an ImageSource using a stream, 
                // the stream gets closed, so to avoid that I backed up
                // the image into a byte array with the following code:

                var stream = photo.OpenReadAsync();

                //var imageBytes = new byte[stream.Result.Length];
                //stream.Result.Position = 0;
                //stream.Result.Read(imageBytes, 0, (int)stream.Result.Length);
                //stream.Result.Position = 0;

                var imageBytes = ReadFully(stream.Result);

                var tessResult = await _tesseractApi.SetImage(imageBytes);

                if (tessResult)
                {

                    //imageName.Source = photo.FullPath;
                    labelName.Text = _tesseractApi.Text;
                }
            }


        }


        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }




    }
}
