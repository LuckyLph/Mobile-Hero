/// <summary>
/// Classe qui contient les requêtes SQL et leurs informations pertinentes sur <see cref="Note"/>.
/// </summary>
public class NoteDataTable
{
    private NoteDataTable()
    {
    }

    public const string COLUMN_0 = "pk_idNote";
    public const string COLUMN_1 = "column";
    public const string COLUMN_2 = "delay";
    public const string COLUMN_3 = "fk_Song_idSong";

    public const string CREATE_SQL =
        "CREATE TABLE IF NOT EXISTS MobileHeroDB.Note(" +
        "pk_idNote INT NOT NULL AUTO_INCREMENT," +
        "column INT NULL," +
        "delay FLOAT NULL," +
        "fk_Song_idSong INT NOT NULL," +
        "PRIMARY KEY(pk_idNote, fk_Song_idSong)," +
        "INDEX fk_Note_Song_idx(fk_Song_idSong ASC)," +
        "CONSTRAINT fk_Note_Song" +
        "FOREIGN KEY(fk_Song_idSong)" +
        "REFERENCES MobileHeroDB.Song(idSong)" +
        "ON DELETE NO ACTION" +
        "ON UPDATE NO ACTION);";

    public const string INSERT_SQL =
        "INSERT INTO Note (pk_idNote, column, delay, fk_Song_idSong) VALUES (NULL, @column, @delay, (SELECT idSong FROM Song WHERE name = @name));";
    public const string INSERT_SQL_PARAMETER_0 = "@column";
    public const string INSERT_SQL_PARAMETER_1 = "@delay";
    public const string INSERT_SQL_PARAMETER_2 = "@name";

    public const string SELECT_SQL =
        "SELECT * FROM Note WHERE Note.fk_Song_idSong = (SELECT Song.pk_idSong FROM Song WHERE Song.name = @name);";
    public const string SELECT_SQL_PARAMETER_0 = "@name";

    public const string UPDATE_SQL =
        "UPDATE Note SET column = @column, delay = @delay WHERE pk_idNote = @pk_idNote;";
    public const string UPDATE_SQL_PARAMETER_0 = "@pk_idNote";

    public const string DELETE_SQL =
        "DELETE FROM Note WHERE fk_Song_idSong = (SELECT idSong FROM Song WHERE name = @name);";
    public const string DELETE_SQL_PARAMETER_0 = "@name";

    public const string DROP_SQL =
        "DROP TABLE Note;";
}