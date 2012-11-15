using System.IO;
using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Utilities
{
    //TODO : À tester
    public class StreamHelper : IStreamHelper
    {
        private Stream Stream { get; set; }

        public byte[] ReadToEnd(string path)
        {
            Stream = new FileStream(path, FileMode.Open);
            var memoryStream = new MemoryStream();
            Stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}