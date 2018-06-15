using System;
using System.Collections.Generic;
using System.Text;

namespace SmartImageResizer
{
    public class ImageSizes
    {
        public ImageSizes()
        {
            Sizes = new List<ImageSize>
            {
                new ImageSize() {Width = 960, Height = 600},
                new ImageSize() {Width = 960, Height = 400},
                new ImageSize() {Width = 960, Height = 300},
                new ImageSize() {Width = 800, Height = 600},
                new ImageSize() {Width = 400, Height = 300},
                new ImageSize() {Width = 631, Height = 427},
                new ImageSize() {Width = 250, Height = 250},
                new ImageSize() {Width = 150, Height = 250},
                new ImageSize() {Width = 50, Height = 50},
                new ImageSize() {Width = 30, Height = 30}
            };

        }

        public List<ImageSize> Sizes { get; set; }
    }

    public class ImageSize
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
