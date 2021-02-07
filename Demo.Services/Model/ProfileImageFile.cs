using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Services.Model
{
    public class ProfileImageFile
    {
        public int ImageFileTypeID { get; set; }
        public string ImageFileTypeName { get; set; }
        public int ProfileImageFileID { get; set; }
        public byte[] ImageFileData { get; set; }
        public string ImageFileName { get; set; }
        public int ImageFileSize { get; set; }
    }
}
