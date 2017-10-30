#if UNITY_ANDROID
using System.Data.Common;
#endif
#if !UNITY_ANDROID
using System.Data.SQLite;
using System.Data.Common;
#endif
/// <summary>
/// Classe qui construit une classe de type Note à partir des données d'un <see cref="DbDataReader"/>.
/// </summary>
public class NoteMapper : DataReaderMapper<LogicalNote>
{

    public NoteMapper()
    {

    }

    public LogicalNote GetObjectFromReader(DbDataReader reader)
    {
        //BEN_CORRECTION : N'est-ce pas plus lisible ?
        //                 N'hésitez pas à écrire les paramêtres de méthodes sur plusieurs lignes.
        return new LogicalNote(reader.GetInt32(reader.GetOrdinal(NoteDataTable.COLUMN_1)), 
                               reader.GetFloat(reader.GetOrdinal(NoteDataTable.COLUMN_2)));
    }
}
