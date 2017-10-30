public interface IDatabaseMocker
{

    Song[] GetAllSongs();

    Song GetSong(string songName);

    void InsertSong(Song song);

    void UpdateHighscoreSong(string songName, int newHighScore);
}

