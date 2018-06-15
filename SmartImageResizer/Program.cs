using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SmartImageResizer
{
    class Program
    {

        public static Byte[] AzureResize(Stream myBlob, int width, int height, bool smartCrop)
        {
            bool smartCropping = smartCrop;
            string _apiKey = "yourAzureKey";
            string _apiUrlBase = "https://westeurope.api.cognitive.microsoft.com/vision/v1.0/generateThumbnail";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_apiUrlBase);
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);
                using (HttpContent content = new StreamContent(myBlob))
                {
                    //get response
                    content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/octet-stream");
                    var uri = $"{_apiUrlBase}?width={width}&height={height}&smartCropping={smartCropping.ToString()}";
                    var response = httpClient.PostAsync(uri, content).Result;
                    var responseBytes = response.Content.ReadAsByteArrayAsync().Result;

                    return responseBytes;
                }
            }
        }

        //ImageSmartResizer wrapper 

        static void Main(string[] args)
        {

            //path to the original image 
            string fileToResize = @"c:\temp\demo.jpg";

            //Folder where resized images will be uploaded 
            string rootOutputFolder = @"c:\temp\ImageResize";

            
            if (!Directory.Exists(rootOutputFolder))
                Directory.CreateDirectory(rootOutputFolder);

            var imageSizes = new ImageSizes();
            var f = new FileInfo(fileToResize);

            foreach (var imgSize in imageSizes.Sizes)
            {
                using (var fileStream = File.Open(fileToResize, FileMode.Open))
                {
                    var outputImage = AzureResize(fileStream, imgSize.Width, imgSize.Height, true );
                    File.WriteAllBytes(
                        rootOutputFolder + "/" + Path.GetFileNameWithoutExtension(f.Name) + "_" + imgSize.Width + "x" + imgSize.Height + f.Extension,
                        outputImage);
                }
            }

        }
    }
}
