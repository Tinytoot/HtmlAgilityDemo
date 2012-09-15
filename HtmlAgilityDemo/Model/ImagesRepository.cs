using System.Collections.Generic;
using System.IO;
using Gecko.WebWallpaper;
using Gecko.WebWallpaper.Model;

namespace HtmlAgilityDemo.Model
{
    public class ImagesRepository
    {
        public List<SearchItemResult> GetDownloadedImages()
        {
            var results = new List<SearchItemResult>();
            System.IO.DirectoryInfo dirInfo = new DirectoryInfo(ConfigAppOptions.ImageFolder);
            System.IO.FileInfo[] fileNames = dirInfo.GetFiles(ConfigAppOptions.fileExtension);

            foreach (System.IO.FileInfo imageFile in fileNames)
            {
                var startpos =  imageFile.Name.LastIndexOf(@"\") + ConfigAppOptions.findText.Length;
                var fileName = imageFile.Name.Substring(startpos);
                results.Add(new SearchItemResult
                {
                    ImageName =  fileName,
                   ThumbImageUrl = string.Concat("Images/",fileName)
                }
               );
            }

            return results; 
       }
    }
}