/// <summary>
/// Classe qui gère la base de données et agit comme intermédiaire entre la logique de jeu et le service de gestion de la base de données.
/// </summary>
public class DatabaseManager : IDatabaseMocker
{
    private SongDatabaseRepositery songDatabaseRepositery;
    private NoteDatabaseRepositery noteDatabaseRepositery;
    private SongMapper songMappper;
    private NoteMapper noteMapper;
    private SongWithoutNotesMapper songWithoutNotesMapper;

    public DatabaseManager()
    {
        noteMapper = new NoteMapper();
        noteDatabaseRepositery = new NoteDatabaseRepositery(noteMapper);
        songMappper = new SongMapper(noteDatabaseRepositery);
        songWithoutNotesMapper = new SongWithoutNotesMapper();
        songDatabaseRepositery = new SongDatabaseRepositery(songMappper, songWithoutNotesMapper, noteDatabaseRepositery);
    }

    public Song[] GetAllSongs()
    {
        return songDatabaseRepositery.SelectAll();
    }

    public Song GetSong(string songName)
    {
        return songDatabaseRepositery.Select(songName);
    }

    public void InsertSong(Song song)
    {
        songDatabaseRepositery.Insert(song);
    }

    public void UpdateHighscoreSong(string songName, int newHighScore)
    {
        songDatabaseRepositery.Update(songName, newHighScore);
    }
}

