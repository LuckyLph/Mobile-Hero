using System.Collections.Generic;
#if UNITY_ANDROID
using System.Data.Common;
#endif
#if !UNITY_ANDROID
using System.Data.SQLite;
using System.Data.Common;
#endif

/// <summary>
/// Classe qui construit une classe de type Song à partir des données d'un <see cref="DbDataReader"/>.
/// </summary>
public class SongMapper : DataReaderMapper<Song>
{
    //BEN_CORRECTION : Private ?
    NoteDatabaseRepositery noteDatabaseRepositery;

    public SongMapper(NoteDatabaseRepositery noteDatabaseRepositery)
    {
        this.noteDatabaseRepositery = noteDatabaseRepositery;
    }

    public Song GetObjectFromReader(DbDataReader reader)
    {
        //BEN_CORRECTION : OULÀLÀ.....c'est pas lisible....
        LogicalNote[] notes = noteDatabaseRepositery.Select(reader.GetString(reader.GetOrdinal(SongDataTable.COLUMN_1)));
        return new Song(reader.GetString(reader.GetOrdinal(SongDataTable.COLUMN_1)), reader.GetString(reader.GetOrdinal(SongDataTable.COLUMN_2)),
            (SongDifficultyType)reader.GetInt32(reader.GetOrdinal(SongDataTable.COLUMN_3)), new List<LogicalNote>(notes), reader.GetInt32(reader.GetOrdinal(SongDataTable.COLUMN_4)));
    }
}
