using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonkeyMusicCloud.Client.Observers;

namespace MonkeyMusicCloud.Client.Exceptions
{
    public class PlayException : BaseException
    {
        public PlayException()
        {
            PlayerObserver.NotifyCurrentSongFinished();
        }
    }
}
