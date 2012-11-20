namespace MonkeyMusicCloud.Utilities.Interface
{
    public interface IMusicPlayer
    {
        void Play(byte[] file);
        void Stop();
        event PurcentagePlayedHandler PurcentagePlayed;
        event SongFinishedHandler SongFinished;
        void Pause();
        void Resume();
    }

    public delegate void SongFinishedHandler();
    public delegate void PurcentagePlayedHandler(int elapsedTime, int totalTime);
}
