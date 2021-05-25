using SixLabors.ImageSharp;
using System;
using System.IO;

namespace Media
{
    public class ImageData : IDisposable
    {
        public string FileName { get; set; }

        public Size Size { get; set; }

        public string Extension { get; set; }

        public string MimeType { get; set; }

        /// <summary>
        /// Do not use this directly.  Use methods in this library instead.
        /// </summary>
        public Image _InternalImage { get; set; }


        public void Dispose()
        {
            if (_InternalImage != null)
                _InternalImage.Dispose();
        }
    }
}
