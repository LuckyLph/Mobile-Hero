using System;
#if UNITY_ANDROID
using Mono.Data.SqliteClient;
#endif
#if !UNITY_ANDROID
using System.Data.SQLite;
#endif
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Classe qui contient la logique de transaction entre la BD et la classe Song.
/// Hérite de la classe abstraite <see cref="DatabaseRepository{T}"/>.
/// </summary>
public class SongDatabaseRepositery : DatabaseRepository<Song>
{
    private NoteDatabaseRepositery noteDatabaseRepositery;
    //BEN_CORRECTION : Ceci n'est pas marqué comme privé.
    SongMapper songMapper;
    SongWithoutNotesMapper songWithoutNotesMapper;

    public SongDatabaseRepositery(SongMapper songMapper, SongWithoutNotesMapper songWithoutNotesMappper, NoteDatabaseRepositery noteDatabaseRepositery)
    {
        this.noteDatabaseRepositery = noteDatabaseRepositery;
        this.songMapper = songMapper;
        this.songWithoutNotesMapper = songWithoutNotesMappper;
    }

    public void Insert(Song song)
    {
        try
        {
#if UNITY_ANDROID
            ExecuteNonQuery(SongDataTable.INSERT_SQL, new SqliteParameter[]
            {
                new SqliteParameter(SongDataTable.INSERT_SQL_PARAMETER_0, song.SongName),
                new SqliteParameter(SongDataTable.INSERT_SQL_PARAMETER_1, song.Description),
                new SqliteParameter(SongDataTable.INSERT_SQL_PARAMETER_2, song.Difficulty)
            });
#endif
#if !UNITY_ANDROID
            //BEN_CORRECTION : Indentation.
            ExecuteNonQuery(SongDataTable.INSERT_SQL, new SQLiteParameter[]
                {
                    new SQLiteParameter(SongDataTable.INSERT_SQL_PARAMETER_0, song.SongName),
                    new SQLiteParameter(SongDataTable.INSERT_SQL_PARAMETER_1, song.Description),
                    new SQLiteParameter(SongDataTable.INSERT_SQL_PARAMETER_2, song.Difficulty)
                });
#endif

            for (int i = 0; i < song.Notes.Count; i++)
            {
                noteDatabaseRepositery.Insert(song.Notes[i], song.SongName);
            }

            Debug.Log(Constants.SONG_INSERT_SUCCESS_DEBUG_TEXT);
        }
        catch (Exception e)
        {
            Debug.Log(Constants.SONG_INSERT_FAILURE_DEBUG_TEXT + " : " + e.ToString());
        }
    }

    public Song Select(string name)
    {
        try
        {
#if UNITY_ANDROID
            IList<Song> song = ExecuteQuery(SongDataTable.SELECT_SQL, songMapper, new SqliteParameter[]
            {
                new SqliteParameter(SongDataTable.SELECT_SQL_PARAMETER_0, name)
            });
#endif
#if !UNITY_ANDROID
            IList<Song> song = ExecuteQuery(SongDataTable.SELECT_SQL, songMapper, new SQLiteParameter[]
                {
                    new SQLiteParameter(SongDataTable.SELECT_SQL_PARAMETER_0, name)
                });
#endif
            Debug.Log(Constants.SONG_SELECT_SUCCESS_DEBUG_TEXT);
            return song[0];
        }

        catch (Exception e)
        {
            Debug.Log(Constants.SONG_SELECT_FAILURE_DEBUG_TEXT + " : " + e.ToString());
            return null;
        }
    }

    public void Update(string name, int score)
    {
#if UNITY_ANDROID
		ExecuteNonQuery(SongDataTable.UPDATE_SQL, new SqliteParameter[]
			{
			new SqliteParameter(SongDataTable.UPDATE_SQL_PARAMETER_0, name),
			new SqliteParameter(SongDataTable.UPDATE_SQL_PARAMETER_4, score)
			});
#endif
#if !UNITY_ANDROID
        ExecuteNonQuery(SongDataTable.UPDATE_SQL, new SQLiteParameter[]
            {
                new SQLiteParameter(SongDataTable.UPDATE_SQL_PARAMETER_0, name),
                new SQLiteParameter(SongDataTable.UPDATE_SQL_PARAMETER_4, score)
            });
#endif
    }

    public Song[] SelectAll()
    {
        try
        {
#if UNITY_ANDROID
            IList<Song> songs = ExecuteQuery(SongDataTable.SELECT_ALL_SQL, songWithoutNotesMapper, new SqliteParameter[] { });
#endif
#if !UNITY_ANDROID
            IList<Song> songs = ExecuteQuery(SongDataTable.SELECT_ALL_SQL, songWithoutNotesMapper, new SQLiteParameter[] { });
#endif
            Debug.Log(Constants.SONG_SELECT_SUCCESS_DEBUG_TEXT);
            return songs.ToArray();
        }
        catch (Exception e)
        {
            Debug.Log(Constants.SONG_SELECT_FAILURE_DEBUG_TEXT + " : " + e.ToString());
            return null;
        }
    }
}

