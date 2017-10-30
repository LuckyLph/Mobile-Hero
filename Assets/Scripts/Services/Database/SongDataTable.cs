/// <summary>
/// Classe qui contient les requêtes SQL et leurs informations pertinentes sur <see cref="Song"/>.
/// </summary>
public class SongDataTable
{
    private SongDataTable()
    {

    }

    public const string COLUMN_0 = "pk_idSong";
    public const string COLUMN_1 = "name";
    public const string COLUMN_2 = "description";
    public const string COLUMN_3 = "difficulty";
    public const string COLUMN_4 = "highscore";

    public const string CREATE_SQL =
        "CREATE TABLE IF NOT EXISTS MobileHeroDB.Song(" +
        "pk_idSong INT NOT NULL AUTO_INCREMENT," +
        "name VARCHAR(45) NULL," +
        "description VARCHAR(45) NULL," +
        "difficulty INT NULL," +
        "PRIMARY KEY(pk_idSong));";

    public const string INSERT_SQL =
        "INSERT INTO Song (pk_idSong, name, description, difficulty) VALUES (NULL, @name, @description, @difficulty);";
    public const string INSERT_SQL_PARAMETER_0 = "@name";
    public const string INSERT_SQL_PARAMETER_1 = "@description";
    public const string INSERT_SQL_PARAMETER_2 = "@difficulty";

    public const string SELECT_SQL =
        "SELECT * FROM Song WHERE Song.pk_idSong = (SELECT Song.pk_idSong FROM Song WHERE Song.name = @name);";
    public const string SELECT_SQL_PARAMETER_0 = "@name";

    public const string SELECT_ALL_SQL =
        "SELECT * FROM Song;";

    public const string UPDATE_SQL =
        "UPDATE Song SET highscore = @highscore WHERE (SELECT pk_idSong FROM Note WHERE name = @name);";
    public const string UPDATE_SQL_PARAMETER_0 = "@name";
    public const string UPDATE_SQL_PARAMETER_1 = "@description";
    public const string UPDATE_SQL_PARAMETER_2 = "@difficulty";
    public const string UPDATE_SQL_PARAMETER_3 = "@name";
    public const string UPDATE_SQL_PARAMETER_4 = "@highscore";

    public const string DELETE_SQL =
        "DELETE FROM Song WHEN Song.name = @name;";
    public const string DELETE_SQL_PARAMETER_0 = "@name";

    public const string DROP_SQL =
        "DROP TABLE Song;";
}