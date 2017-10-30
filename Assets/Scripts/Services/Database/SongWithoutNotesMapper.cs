using System.Collections.Generic;
#if UNITY_ANDROID
using System.Data.Common;
#endif
#if !UNITY_ANDROID
using System.Data.Common;
#endif

public class SongWithoutNotesMapper : DataReaderMapper<Song>
{
    public Song GetObjectFromReader(DbDataReader reader)
    {
        return new Song(reader.GetString(reader.GetOrdinal(SongDataTable.COLUMN_1)), reader.GetString(reader.GetOrdinal(SongDataTable.COLUMN_2)),
            (SongDifficultyType)reader.GetInt32(reader.GetOrdinal(SongDataTable.COLUMN_3)), new List<LogicalNote>(), reader.GetInt32(reader.GetOrdinal(SongDataTable.COLUMN_4)));
    }
}
