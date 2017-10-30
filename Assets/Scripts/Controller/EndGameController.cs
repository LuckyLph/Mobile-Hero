using UnityEngine;

/// <summary>
/// Cette classe gère les statistiques de fin de partie
/// </summary>
public class EndGameController : MonoBehaviour
{

    public static Player player;
    [SerializeField]
    private UnityEngine.UI.Text overallText;
    [SerializeField]
    private UnityEngine.UI.Image bigScore;
    [SerializeField]
    private GameObject highscoreObject;
    [SerializeField]
    private Sprite[] letterSprites = new Sprite[7];


	private void Start()
    {
        overallText.text = FormatPlayerEndScreen();
        bigScore.sprite = GetOverallScore();
        if (player.Score > LevelLoader.GetCurrentSong().Highscore)
        {
            highscoreObject.SetActive(true);
            SetNewHighscore();
        }
        else
        {
            overallText.rectTransform.localPosition = (highscoreObject.transform.localPosition + new Vector3(0, -Constants.OFFSET_IF_HIGHSCORE, 0));
        }
    }

	/// <summary>
	/// Assigne un nouveau highscore à la chanson jouée
	/// </summary>
	private void SetNewHighscore()
    {
        DatabaseManager database = new DatabaseManager();
        database.UpdateHighscoreSong(LevelLoader.songToLoad, player.Score);
    }

	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }
    }
	/// <summary>
	/// Créer la string représentant au joueur ses statistiques
	/// </summary>
	/// <returns>The player end screen.</returns>
	private string FormatPlayerEndScreen()
    {
        string returnValue = LevelLoader.songToLoad + "\n" +
            Constants.ENDSCREEN_SCORE + player.Score + "\n" +
            Constants.ENDSCREEN_STREAK + player.GetHighestStreak() + "\n" +
            Constants.ENDSCREEN_PERCENT + player.GetPercent() + "%";
        return returnValue;
    }
    
	/// <summary>
	/// Retourne la sprite à afficher au bas de l'écran. Dépend du score du joueur.
	/// </summary>
	/// <returns>The overall score.</returns>
	private Sprite GetOverallScore()
    {
        //BEN_CORRECTION : Pas vraiment la meilleure façon de procéder. Instable.
        float ratio = 0f;
        ratio += ((float)player.Score / (float)player.GetMaximumScore());
        if (ratio < Constants.ENDSCREEN_SCORE_FOR_E)
        {
            //return 'F';
            return letterSprites[6];
        }
        else if (ratio < Constants.ENDSCREEN_SCORE_FOR_E)
        {
            //return 'E';
            return letterSprites[5];
        }
        else if (ratio < Constants.ENDSCREEN_SCORE_FOR_D)
        {
            //return 'D';
            return letterSprites[4];
        }
        else if (ratio < Constants.ENDSCREEN_SCORE_FOR_C)
        {
            //return 'C';
            return letterSprites[3];
        }
        else if (ratio < Constants.ENDSCREEN_SCORE_FOR_B)
        {
            //return 'B';
            return letterSprites[2];
        }
        else if (ratio < Constants.ENDSCREEN_SCORE_FOR_A)
        {
            //return 'A';
            return letterSprites[1];
        }
        else
        {
            //return 'S';
            return letterSprites[0];
        }
    }
	/// <summary>
	/// Retourne au menu de sélection de la chanson
	/// </summary>
    public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.MENU_SCENE_NAME);
    }
}
