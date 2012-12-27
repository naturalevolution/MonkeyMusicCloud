using MonkeyMusicCloud.Client.Exceptions;

namespace MonkeyMusicCloud.Client.Observers
{
    public class ExceptionObserver
    {
        public static event ExceptionCatchedHandler ExceptionCatched;

        public static void NotifyExceptionCatched(BaseException exception)
        {
            ExceptionCatchedHandler handler = ExceptionCatched;
            if (handler != null) handler(exception);
        }
    }

    public delegate void ExceptionCatchedHandler(BaseException exception);
}