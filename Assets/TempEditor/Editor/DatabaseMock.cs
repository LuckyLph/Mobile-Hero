using System.Collections.Generic;

public class DatabaseMock : IDatabaseMocker
{
    public Song[] GetAllSongs()
    {
        return new Song[3];
    }

    public Song GetSong(string songName)
    {
        return new Song(songName, "", SongDifficultyType.EASY, new List<LogicalNote>(), 0);
    }

    public void InsertSong(Song song)
    {

    }

    public void UpdateHighscoreSong(string songName, int newHighScore)
    {

    }

}
