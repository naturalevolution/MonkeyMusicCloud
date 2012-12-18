using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Utilities
{
    public class ImageGoogleSearch : IImageSearch
    {
        public string GetImagePath(string search)
        {/*
            GimageSearchClient client = new GimageSearchClient("http://www.google.com");
            
            IList<IImageResult> results = client.Search(search, 1);
            return results[0].Url;*/
            return search;
        }
    }
}