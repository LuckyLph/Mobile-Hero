using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Cette classe représente le chargement du niveau via la base de données
/// </summary>
public class LevelLoader : MonoBehaviour
{
    /// <summary>
    /// Le titre de la chanson à jouer. Doit être valide.
    /// </summary>
    public static string songToLoad = "";

    private static Song song;
    private DatabaseManager database;
    private LogicalNote[] listeNote;
    private GameObject blankNote;
    private Transform[] spawns;

    public void Start()
    {
        if (songToLoad == "" || songToLoad == null)
        {
            Debug.LogError(Constants.NO_SONG_SELECTED);
            this.enabled = false;
        }
        else
        {
            InitializeVars();
            LoadSong();
            SoundPlayerCore.PlaySound(Path.Combine(Constants.SOUND_EFFECTS_PATH, Constants.GUITAR_MISS1_PATH));
        }
    }

    /// <summary>
    /// Initialize les variables nécessaire au fonctionnement du LevelLoader
    /// </summary>
	private void InitializeVars()
    {
        Time.timeScale = Constants.TIMESCALE;
        //Les quatres spawns, pour les 4 colonnes de jeu 
        spawns = new Transform[4];
        for (int i = 0; i < 4; i++)
        {
            spawns[i] = GameObject.Find(Constants.SPAWN_NAME + i).transform;
        }
        blankNote = Resources.Load<GameObject>(Path.Combine(Constants.PREFAB_PATH, Constants.NOTE_NAME));
        database = new DatabaseManager();
    }

    /// <summary>
    /// Charche une chanson ainsi que toute ses notes
    /// </summary>
	private void LoadSong()
    {
        song = database.GetSong(songToLoad);
        Debug.Log("highscore = " + song.Highscore);
        List<LogicalNote> tempList = song.Notes;
        Debug.Log(song.Difficulty.ToString());
        listeNote = new LogicalNote[tempList.Count];
        for (int i = 0; i < tempList.Count; i++)
        {
            listeNote[i] = tempList[i];
        }
        float noteSpeed = (Constants.NOTE_SPEED + (int)song.Difficulty * Constants.NOTE_SPEED_PER_DIFFICULTY);
        float distanceUntilBottom = 8.3f;
        float delayToSyncSong = distanceUntilBottom / noteSpeed;
        SoundPlayerCore.DelayedSound(Path.Combine(Constants.SOUNDTRACK_PATH, song.SongName), (delayToSyncSong + Constants.OVERALL_DELAY_BEFORE_PLAY) / Constants.TIMESCALE, Constants.MUSIC_OBJECT_NAME, (int)(Constants.VOLUME_MUSIC * 100), false);
        Sprite backgroundSprite = Resources.Load<Sprite>(Path.Combine(Constants.BACKGROUND_PATH, song.SongName));
        if (backgroundSprite != null)
        {
            GameObject.Find(Constants.BACKGROUND_NAME).GetComponent<SpriteRenderer>().sprite = backgroundSprite;
        }
        StartCoroutine(SpawnAllNotes());
    }
	/// <summary>
	/// Retourne la chanson chargée actuellement
	/// </summary>
	/// <returns>La chanson chargée</returns>
    public static Song GetCurrentSong()
    {
        return song;
    }

    /// <summary>
    /// Joue la chanson en créant des notes au bon moment
    /// </summary>
    /// <returns>L'IEnumerator pour controller l'éxécution</returns>
	private IEnumerator SpawnAllNotes()
    {
        int noteIndex = 0;
        yield return new WaitForSeconds(Constants.OVERALL_DELAY_BEFORE_PLAY);
        while (noteIndex < listeNote.Length)
        {
            Note currentNote = Instantiate(blankNote, spawns[listeNote[noteIndex].Column].transform.position, Quaternion.identity).GetComponent<Note>();
            currentNote.SetParameters(listeNote[noteIndex].Column, (Constants.NOTE_SPEED + (int)song.Difficulty * Constants.NOTE_SPEED_PER_DIFFICULTY), Constants.NOTE_WORTH);
            noteIndex++;
            yield return new WaitForSeconds(listeNote[noteIndex - 1].Delay + (Time.deltaTime * -0.5f) + Constants.ADDITIONAL_DELAY_PER_NOTE);
        }
        yield return new WaitForSeconds(Constants.WAIT_BEFORE_BACKTOMENU);
        Camera.main.GetComponent<MobileHero>().EndGame();
    }


}
