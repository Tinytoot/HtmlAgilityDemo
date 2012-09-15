
namespace Gecko.WebWallpaper
{
    public static class ConfigAppOptions
    {
        public const string ImageFolder = @"C:\Images\";
        public const string fileExtension = "*.jpg";
        public const string cacheKey = "foundImages";
        public const string findText = @"/";
        public const string findHtmlElement = "img";
        public const string findSrcAttribute = "src";
        public const string findClassAttribute = "class";
        public const string findClassAttributeContaining = "article";
        public const string findSrcAttributeContaining = "media";
        public const string DefaultWallPaperSize = "2560x1600";
        public const string DefaultThumbImageSize = "100";
        public const string WebSiteURL = @"http://www.wizards.com";
        public const int DownloadingFileLimit = 10;
    }
}
