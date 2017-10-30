using System;
using System.Collections.Generic;
#if UNITY_ANDROID
using System.Data.Common;
using Mono.Data.SqliteClient;
#endif
#if !UNITY_ANDROID
using System.Data.SQLite;
using System.Data;
using System.Data.Common;
#endif

/// <summary>
/// Classe abstraite qui contient la logique de transaction entre la BD et les classes du jeu.
/// Peut être instanciée seulement avec un classe de la logique de jeu.
/// </summary>
/// <typeparam name="T">Type de classe que l'instance de cette classe gère.</typeparam>
public abstract class DatabaseRepository<T> where T : class
{

    protected DatabaseRepository()
    {

    }

#if UNITY_ANDROID
    protected virtual IList<T> ExecuteQuery(string query, DataReaderMapper<T> readerMapper, SqliteParameter[] parameters)
#endif
#if !UNITY_ANDROID
    protected virtual IList<T> ExecuteQuery(string query, DataReaderMapper<T> readerMapper, SQLiteParameter[] parameters)
#endif
    {
        try
        {
            IList<T> dataObjects = new List<T>();
            using (DbConnection connection = DatabaseConnectionFactory.GetSQLiteConnection(Constants.MOBILE_HERO_DATABASE_NAME) as DbConnection)
            {
                connection.Open();
                DbDataReader reader;
                DbCommand databaseCommand = connection.CreateCommand();
                databaseCommand.CommandText = query;
                for (int i = 0; i < parameters.Length; i++)
                {
                    databaseCommand.Parameters.Add(parameters[i]);
                }

                using (reader = databaseCommand.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        dataObjects.Add(readerMapper.GetObjectFromReader(reader));
                    }
                }
            }

            return dataObjects;
        }
        //BEN_CORRECTION : Catch inutile, car il ne fait que relancer l'exception. Voir "Warning".
        catch (Exception e)
        {
            throw e;
        }
    }
#if UNITY_ANDROID
    protected virtual void ExecuteNonQuery(string query, SqliteParameter[] parameters)
#endif
#if !UNITY_ANDROID
    protected virtual void ExecuteNonQuery(string query, SQLiteParameter[] parameters)
#endif
    {
        try
        {
            using (DbConnection connection = DatabaseConnectionFactory.GetSQLiteConnection(Constants.MOBILE_HERO_DATABASE_NAME) as DbConnection)
            {
                connection.Open();
                DbCommand databaseCommand = connection.CreateCommand();
                databaseCommand.CommandText = query;
                for (int i = 0; i < parameters.Length; i++)
                {
                    databaseCommand.Parameters.Add(parameters[i]);
                }
                databaseCommand.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}
