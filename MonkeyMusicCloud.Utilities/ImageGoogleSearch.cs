using System;
using System.Net;
using MonkeyMusicCloud.Utilities.Amazon;
using MonkeyMusicCloud.Utilities.Interface;
using MusicBrainz;

namespace MonkeyMusicCloud.Utilities
{
    public class ImageGoogleSearch : IImageSearch
    {
        public string GetImagePath(string album)
        {
           
            KeywordRequest req = new KeywordRequest();

            req.keyword = album;

            // Remember to register to get your own devtag.

            req.devtag = "AKIAIKJ5PIY4YWNB6NQQ";
            req.mode = "music";
            req.type = "lite";
            req.page = "1";

            using (AmazonSearchPortClient search = new AmazonSearchPortClient())
            {
                ProductInfo productInfo = search.KeywordSearchRequest(req);
                if (productInfo.Details.Length > 0)
                {
                    string url = productInfo.Details[0].ImageUrlLarge;
                    //webClient.DownloadFile(url, dirName + "/" + albumTitle + ".jpg");
                }

            }
            return "../../Themes/Images/MusiqueFolder.png";
        }
    }
}