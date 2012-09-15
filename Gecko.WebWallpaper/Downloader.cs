using System;
using System.Collections.Generic;
using System.Linq;
using Gecko.WebWallpaper.Model;
using HtmlAgilityPack;
using System.Net;
using Newtonsoft.Json;
using System.IO;


namespace Gecko.WebWallpaper
{
    public class Downloader
    {

        public string ResultMessage { get; set; }
        
        public string FindImages(string url)
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);

            // Get <a> tags that have a href attribute and non-whitespace inner text
            IEnumerable<SearchItemResult> results = from lnks in document.DocumentNode.Descendants()
                                                    where lnks.Name == ConfigAppOptions.findHtmlElement &&
                                                          lnks.Attributes[ConfigAppOptions.findSrcAttribute] != null &&
                                                          lnks.Attributes[ConfigAppOptions.findClassAttribute] != null &&
                                                          lnks.Attributes[ConfigAppOptions.findClassAttribute].Value.ToString().Contains(ConfigAppOptions.findClassAttributeContaining)
                                                    select new SearchItemResult
                                                    {
                                                        ThumbImageUrl = lnks.Attributes[ConfigAppOptions.findSrcAttribute].Value.Contains(ConfigAppOptions.findSrcAttributeContaining) ? lnks.Attributes[ConfigAppOptions.findSrcAttribute].Value : String.Concat(ConfigAppOptions.WebSiteURL, lnks.Attributes[ConfigAppOptions.findSrcAttribute].Value),
                                                        ImageName = lnks.InnerText
                                                    };

            ResultMessage = string.Format("URL {0} loaded in {1:N0} milliseconds. {2:N0} links discovered...", webGet.ResponseUri.ToString(), webGet.RequestDuration, results.Count());
            return JsonConvert.SerializeObject(results); ;
        }

        public void DownloadImage(string imageFolder, string imagesFound)
        {
            var client = new WebDownloadClient(120000);
            var itemCounter = 0;

            var images = JsonConvert.DeserializeObject<IEnumerable<SearchItemResult>>(imagesFound);

            foreach (SearchItemResult image in images)
            {
                image.WallPaperUrl = image.ThumbImageUrl.Replace(ConfigAppOptions.DefaultThumbImageSize, ConfigAppOptions.DefaultWallPaperSize);
                var startpos = image.ThumbImageUrl.LastIndexOf(ConfigAppOptions.findText) + ConfigAppOptions.findText.Length;
                image.ImageName = image.WallPaperUrl.Substring(startpos);
                var ImagepathName = String.Concat(imageFolder, image.ImageName);
                if (itemCounter <= (ConfigAppOptions.DownloadingFileLimit + 1))
                {
                    if (fileExits(image.WallPaperUrl))
                    {
                        client.DownloadFile(image.WallPaperUrl, ImagepathName);
                    }
                    itemCounter++;
                }
                else
                {
                    return;
                }
            }
        }

        public bool fileExits(string fileUrl)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(fileUrl);
            request.Method = "HEAD";

            bool exists;
            try
            {
                request.GetResponse();
                exists = true;
            }
            catch
            {
                exists = false;
            }

            return exists;
        }


        public void Save(string json)
        {
            var filePath = String.Concat(ConfigAppOptions.ImageFolder, "Images.json");
            File.WriteAllText(filePath, json);
        }

       public string GetImages(string Url)
        {
            var filepath = String.Concat(ConfigAppOptions.ImageFolder, "Images.json");

            var results = string.Empty;
            if (File.Exists(filepath))
            {
                results = File.ReadAllText(String.Concat(ConfigAppOptions.ImageFolder, "Images.json"));
                ResultMessage = "using the json file!!!";
            }
            else
            {
                results = FindImages(Url);
           }

            return results;
        }

    }
}
