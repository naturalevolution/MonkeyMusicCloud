using System;
using MonkeyMusicCloud.Client.Observers;

namespace MonkeyMusicCloud.Client.Exceptions
{
    public class BaseException : Exception
    {
        protected BaseException()
        {
            ExceptionObserver.NotifyExceptionCatched(this);
        }
    }
}