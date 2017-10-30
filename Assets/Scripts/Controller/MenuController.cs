using System.IO;
using UnityEngine;

/// <summary>
/// Gère le menu principal (sélection de la chanson)
/// </summary>
public class MenuController : MonoBehaviour
{

    [SerializeField]
    private GameObject startButtonSample;
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private Transform canvas;

	private Song[] allSongs;
	private MenuItem[] allItems;
	private DatabaseManager database;
	private int creatingIndex = 0;

	private float minimumOffset;
	private float maximumOffset;
	private float currentOffset;

    // Use this for initialization
	private void Start()
    {
        database = new DatabaseManager();
        allSongs = database.GetAllSongs();
        SortSongsByDifficulty();
        foreach (Song song in allSongs)
        {
            GameObject temporaryClone = (GameObject)Instantiate(startButtonSample, canvas);
            temporaryClone.transform/*.Find(Constants.GRAPHIC_BUTTON_NAME)*/.Find(Constants.SONGNAME_OBJECT_NAME).GetComponent<UnityEngine.UI.Text>().text = SongToString(song);
            temporaryClone.transform.position = spawnPosition.position;
            temporaryClone.transform.Translate(0, -1 * creatingIndex * Constants.DISTANCE_BETWEEN_BUTTONS, 0, Space.World);
            creatingIndex++;
        }
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MenuItem");
        allItems = new MenuItem[objects.Length];
        int index = 0;
        foreach (GameObject obj in objects)
        {
            allItems[index] = obj.GetComponent<MenuItem>();
            index++;
        }
        minimumOffset = 0;
        currentOffset = minimumOffset;
        maximumOffset = Mathf.Max((allSongs.Length - 6) * (Constants.DISTANCE_BETWEEN_BUTTONS + 75f), 0f);

        SoundPlayerCore.PlaySound(Path.Combine(Constants.SOUND_EFFECTS_PATH, Constants.GUITAR_TRANSITION_PATH), Constants.TRANSITION_GUITAR_TEXT);



    }
	/// <summary>
	/// Retourne une string formattée en fonction d'une difficulté
	/// </summary>
	/// <returns>The to string.</returns>
	/// <param name="difficulty">Difficulty.</param>
	private string DifficultyToString(SongDifficultyType difficulty)
    {
        return difficulty.ToString().Replace('_', ' ');
    }

	/// <summary>
	/// Retourne une string formattée en fonction d'une chanson
	/// </summary>
	/// <returns>The to string.</returns>
	/// <param name="song">Song.</param>
	private string SongToString(Song song)
    {
        return song.SongName + "\n" + DifficultyToString(song.Difficulty);
    }
	/// <summary>
	/// Trie la liste de chansons par difficulté croissante
	/// </summary>
	private void SortSongsByDifficulty()
    {
        bool doneSomethingThisIteration = false;
        do
        {
            for (int i = 0; i < allSongs.Length - 1; i++)
            {
                doneSomethingThisIteration = false;
                if ((int)allSongs[i].Difficulty > (int)allSongs[i + 1].Difficulty)
                {
                    Song previous = allSongs[i];
                    allSongs[i] = allSongs[i + 1];
                    allSongs[i + 1] = previous;
                    doneSomethingThisIteration = true;
                    break;
                }
            }

        } while (doneSomethingThisIteration);
    }


	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.SPLASHSCREEN_SCENE_NAME);
        }

        if (Input.touchCount > 0)
        {
            ProcessUserScroll(Input.touches[0]);
        }
#if !UNITY_ANDROID

        ProcessMouseScroll();

#endif

    }

#if !UNITY_ANDROID
	/// <summary>
	/// Gère la molette de souris, faisant "scroll" le menu en fonction de celle-ci
	/// </summary>
	private void ProcessMouseScroll()
    {
        float scroll = Input.mouseScrollDelta.y * -20f;
        if (scroll != 0 && maximumOffset > 0)
        {
            currentOffset = Mathf.Clamp(currentOffset + scroll, minimumOffset, maximumOffset);
            foreach (MenuItem item in allItems)
            {
                item.UpdatePosition(currentOffset);
            }
        }
    }
#endif
	/// <summary>
	/// Gère le touch, faisant "scroll" le menu en fonction de ce dernier
	/// </summary>
	/// <param name="touchToProcess">Touch to process.</param>
	private void ProcessUserScroll(Touch touchToProcess)
    {
        if (touchToProcess.phase == TouchPhase.Moved && maximumOffset > 0)
        {
            currentOffset = Mathf.Clamp(currentOffset + touchToProcess.deltaPosition.y, minimumOffset, maximumOffset);
            foreach (MenuItem item in allItems)
            {
                item.UpdatePosition(currentOffset);
            }
        }
    }
	/// <summary>
	/// Appellé lorsque l'un des bouton est appuyé
	/// </summary>
	/// <param name="sender">Sender.</param>
    public void OnStartClick(UnityEngine.UI.Button sender)
    {
        string track = sender.transform.Find("SongName").GetComponent<UnityEngine.UI.Text>().text;
        string[] mySongNameAndDifficulty = track.Split('\n');
        StartLevel(mySongNameAndDifficulty[0]);
    }
	/// <summary>
	/// Commence un niveau précis.
	/// </summary>
	/// <param name="trackName">Le nom de la chanson à partir</param>
	private void StartLevel(string trackName)
    {
        LevelLoader.songToLoad = trackName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.GAME_SCENE_NAME);
    }
}
