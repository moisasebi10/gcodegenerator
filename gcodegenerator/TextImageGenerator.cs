using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;

namespace ImageGenerator
{
    // <copyright file="TextImageGenerator.cs" company="dotjord.wordpress.com">
    // Free to use
    // </copyright>
    // <author>Jordan Atanasovski</author> 
    // <date>07/18/2015 11:39:58 AM </date>
    // <summary>Class that enables you to create images from text</summary>
    public class TextImageGenerator
    {
        private Color TextColor { get; set; }
        private Color BackgroundColor { get; set; }
        private Font Font { get; set; }
        private int Padding { get; set; }
        private int FontSize { get; set; }
         
        public TextImageGenerator(Color textColor, Color backgroundColor, string font, int padding, int fontSize)
        {
            TextColor = textColor;
            BackgroundColor = backgroundColor;
            Font = new Font(font, fontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);;
            Padding = padding;
            FontSize = fontSize;
        }
        public TextImageGenerator() 
        : this(Color.Black, Color.White, "Arial", 20, 20)
        { }
        public Bitmap CreateBitmap(string text)
        {
            // Create graphics for rendering 
            Graphics retBitmapGraphics = Graphics.FromImage(new Bitmap(1, 1));
            // measure needed width for the image
            var bitmapWidth = (int)retBitmapGraphics.MeasureString(text, Font).Width;
            // measure needed height for the image
            var bitmapHeight = (int)retBitmapGraphics.MeasureString(text, Font).Height;
            // Create the bitmap with the correct size and add padding
            Bitmap retBitmap = new Bitmap(bitmapWidth + Padding, bitmapHeight + Padding);
            // Add the colors to the new bitmap.
            retBitmapGraphics = Graphics.FromImage(retBitmap);
            // Set Background color
            retBitmapGraphics.Clear(BackgroundColor);
            retBitmapGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            retBitmapGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            retBitmapGraphics.DrawString(text, Font, new SolidBrush(TextColor), Padding / 2, Padding / 2);
            // flush the changes
            retBitmapGraphics.Flush();

            return (retBitmap);
        }
        public string CreateBase64Image(string text, ImageFormat imageFormat)
        {
            var bitmap = CreateBitmap(text);
            var stream = new System.IO.MemoryStream();
            // save into stream
            bitmap.Save(stream, imageFormat);
            // convert to byte array
            var imageBytes = stream.ToArray();
            // convert to base64 string
            return Convert.ToBase64String(imageBytes);
        }
        public void SaveAsJpg(string filename, string text)
        {
            var bitmap = CreateBitmap(text);
            bitmap.Save(filename, ImageFormat.Jpeg);
        }
        public void SaveAsPng(string filename, string text)
        {
            var bitmap = CreateBitmap(text);
            bitmap.Save(filename, ImageFormat.Png);
        }
        public void SaveAsGif(string filename, string text)
        {
            var bitmap = CreateBitmap(text);
            bitmap.Save(filename, ImageFormat.Gif);
        }
        public void SaveAsBmp(string filename, string text)
        {
            var bitmap = CreateBitmap(text);
            bitmap.Save(filename, ImageFormat.Bmp);
        }
    }
}
