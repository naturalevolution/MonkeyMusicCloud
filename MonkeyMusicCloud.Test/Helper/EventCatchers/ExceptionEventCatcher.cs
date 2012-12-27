using System.Collections.Generic;
using MonkeyMusicCloud.Client.Exceptions;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;

namespace MonkeyMusicCloud.Test.Helper.EventCatchers
{
    public class ExceptionEventCatcher
    {
        public ExceptionEventCatcher()
        {
            ExceptionObserver.ExceptionCatched += delegate(BaseException exception)
            {
                ExceptionEventInvoked = true;
                Exception = exception;
            };

        }

        public BaseException Exception { get; set; }
        public bool ExceptionEventInvoked = false;
    }
}
