﻿using System.Threading;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels
{
    public class UploadTask : Task
    {
        public Song Song { get; set; }
        public MediaFile MediaFile { get; set; }

        public UploadTask(Song song, MediaFile mediaFile)
        {
            Song = song;
            MediaFile = mediaFile;
        }

        protected override void DoAction()
        {
            Service.AddASong(Song, MediaFile);
        }
    }
}
