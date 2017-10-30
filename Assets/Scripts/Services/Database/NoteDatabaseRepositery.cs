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
/// Classe qui contient la logique de transaction entre la BD et la classe Note.
/// Hérite de la classe abstraite <see cref="DatabaseRepository{T}"/>.
/// </summary>
public class NoteDatabaseRepositery : DatabaseRepository<LogicalNote>
{
    private DataReaderMapper<LogicalNote> mapper;

    public NoteDatabaseRepositery(DataReaderMapper<LogicalNote> mapper)
    {
        this.mapper = mapper;
    }

    public void Insert(LogicalNote note, string songName)
    {
        try
        {
#if UNITY_ANDROID
            ExecuteNonQuery(NoteDataTable.INSERT_SQL, new SqliteParameter[]
            {
                new SqliteParameter(NoteDataTable.INSERT_SQL_PARAMETER_0, note.Column),
                new SqliteParameter(NoteDataTable.INSERT_SQL_PARAMETER_1, note.Delay),
                new SqliteParameter(NoteDataTable.INSERT_SQL_PARAMETER_2, songName),
            });
#endif
#if !UNITY_ANDROID
            ExecuteNonQuery(NoteDataTable.INSERT_SQL, new SQLiteParameter[]
                {
                    new SQLiteParameter(NoteDataTable.INSERT_SQL_PARAMETER_0, note.Column),
                    new SQLiteParameter(NoteDataTable.INSERT_SQL_PARAMETER_1, note.Delay),
                    new SQLiteParameter(NoteDataTable.INSERT_SQL_PARAMETER_2, songName),
                });
#endif

            Debug.Log(Constants.NOTE_INSERT_SUCCESS_DEBUG_TEXT);
        }
        catch (Exception e)
        {
            Debug.Log(Constants.NOTE_INSERT_FAILURE_DEBUG_TEXT);
        }
    }

    public LogicalNote[] Select(string songName)
    {
        try
        {
#if UNITY_ANDROID
            IList<LogicalNote> notes = ExecuteQuery(NoteDataTable.SELECT_SQL, mapper, new SqliteParameter[]
            {
                new SqliteParameter(NoteDataTable.SELECT_SQL_PARAMETER_0, songName)
            });
#endif
#if !UNITY_ANDROID
            IList<LogicalNote> notes = ExecuteQuery(NoteDataTable.SELECT_SQL, mapper, new SQLiteParameter[]
                {
                    new SQLiteParameter(NoteDataTable.SELECT_SQL_PARAMETER_0, songName)
                });
#endif

            Debug.Log(Constants.NOTE_SELECT_SUCCESS_DEBUG_TEXT);
            return notes.ToArray();
        }

        catch (Exception e)
        {
            Debug.Log(Constants.NOTE_SELECT_FAILURE_DEBUG_TEXT);
            return null;
        }
    }
}

