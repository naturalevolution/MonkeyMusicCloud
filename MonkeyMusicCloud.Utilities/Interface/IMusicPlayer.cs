using System;

namespace MonkeyMusicCloud.Utilities.Interface
{
    public interface IMusicPlayer
    {
        void Play(Guid id, byte[] file);
        void Stop();
        event PurcentagePlayedHandler PurcentagePlayed;
        event SongFinishedHandler SongFinished;
        void Pause();
        void Resume();
        void PlayAt(double purcentage);
        void ChangeVolume(double volume);
    }

    public delegate void SongFinishedHandler();
    public delegate void PurcentagePlayedHandler(int elapsedTime, int totalTime);
}
