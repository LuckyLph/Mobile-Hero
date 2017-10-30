
#if UNITY_ANDROID
using System.Data;

using Mono.Data.SqliteClient;
#endif
#if !UNITY_ANDROID
using System.Data.SQLite;
using System.Data;
#endif
using System.IO;
using UnityEngine;

public class DatabaseConnectionFactory
{
    public static IDbConnection GetSQLiteConnection(string databaseName)
    {

        if (Application.platform != RuntimePlatform.Android)
        {
#if !UNITY_ANDROID
            return new SQLiteConnection(Path.Combine(Constants.DATABASE_CONNECTION_PATH_PREFIX + Application.streamingAssetsPath, databaseName));
#endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
			return new SqliteConnection (CopyFile());
#endif
        }
        return null;
    }

#if UNITY_ANDROID
	private static string CopyFile () 
	{
		string filepath = Application.persistentDataPath + "/" + Constants.MOBILE_HERO_DATABASE_NAME;
		Debug.Log ("FoundPath: " + filepath);

		if(!File.Exists(filepath))

		{
			Debug.Log ("doesnt exists");

			// if it doesn't ->

			// open StreamingAssets directory and load the db ->

			WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + Constants.MOBILE_HERO_DATABASE_NAME);  // this is the path to your StreamingAssets in android

			while(!loadDB.isDone) {}  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check

			// then save to Application.persistentDataPath

			File.WriteAllBytes(filepath, loadDB.bytes);
			Debug.Log ("Wrote file");
		}



		//open db connection

		return ("URI=file:" + filepath);

	}
#endif
}
