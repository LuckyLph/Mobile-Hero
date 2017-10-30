using UnityEngine;

/// <summary>
/// Classe qui représente le controlleur du jeu, gère les inputs.
/// </summary>
public class MobileHero : MonoBehaviour
{

    private static bool paused;

    [SerializeField]
    private GameObject pauseMenuObject;
    [SerializeField]
    private GameObject darkenedBackground;

    private Player player;

    /// <summary>
    /// Initialise l'objet, est appellé par la boucle de jeu lorsqu'elle appelle cette classe pour la première fois.
    /// </summary>
    public void Start()
    {
        paused = false;
        Input.multiTouchEnabled = true;
    }

    /// <summary>
    /// Met à jour le controlleur, appellé une fois par mise à jour de la boucle de jeu.
    /// </summary>
    public void Update()
    {
        TouchesToClick();
        //Si le user appuie sur "retour", on retourne au menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
	/// <summary>
	/// Quitte la partie vers le menu
	/// </summary>
    public void QuitGame()
    {
        Time.timeScale = Constants.TIMESCALE;
        UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.MENU_SCENE_NAME);
    }

	/// <summary>
	/// Met le jeu en pause, ou le dépause. (Toggle)
	/// </summary>
    public void TogglePause()
    {
        paused = !paused;
        ProcessPause(paused);
    }

	/// <summary>
	/// Appellé lorsque le "focus" est perdu, et que l'application n'est plus en avant plan
	/// </summary>
	/// <param name="hasFocus">On a le focus de l'appareil</param>
    public void OnApplicationFocus(bool hasFocus)
    {
        if (!paused && !hasFocus)
        {
            TogglePause();
        }
    }
	/// <summary>
	/// Redémarre la chanson
	/// </summary>
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.GAME_SCENE_NAME);
    }

	/// <summary>
	/// Détermine si le jeu est en pause
	/// </summary>
	/// <returns>Si l'application est en pause</returns>
    public static bool IsPaused()
    {
        return paused;
    }

	/// <summary>
	/// Met le jeu en pause, concrètement
	/// </summary>
	/// <param name="pause">Le jeu doit-il être en pause</param>
	private void ProcessPause(bool pause)
    {
        pauseMenuObject.SetActive(pause);
        darkenedBackground.SetActive(pause);
        Time.timeScale = pause ? 0 : Constants.TIMESCALE;
        if (pause)
        {
            GetMusicSource().Pause();
        }
        else
        {
            GetMusicSource().UnPause();
        }
    }

    //BEN_CORRECTON : Pas performant. Comme dans Android, faire les "Find" dans le "OnCreate" (dans Unity, Awake
    //                ou "Start" au plus tard).
	/// <summary>
	/// Retourne la source de la musique principale
	/// </summary>
	/// <returns>The music source.</returns>
    AudioSource GetMusicSource()
    {
        return GameObject.Find(Constants.MUSIC_OBJECT_NAME).GetComponent<AudioSource>();
    }

    /// <summary>
    /// Retourne l'instance représentant le joueur (un seul par partie)
    /// </summary>
    /// <returns>The player instance.</returns>
    public Player GetPlayerInstance()
    {
        if (player == null)
        {
            player = new Player();
        }
        return player;
    }

    /// <summary>
    /// Détermine si les touchés fait sur l'écran sont en collision avec des objets du jeu.
    /// </summary>
    public void TouchesToClick()
    {
        Touch[] touches = Input.touches;
        if (touches.Length < 1)
        {
            //Optimisation
            return;
        }
        for (int i = 0; i < touches.Length; i++)
        {

            Ray temporaryRay = new Ray(ScreenPositionToWorld(touches[i].position), transform.forward);
            Debug.Log(ScreenPositionToWorld(touches[i].position).ToString());
            RaycastHit hit;
            if (Physics.Raycast(temporaryRay, out hit))
            {

                if (touches[i].phase == TouchPhase.Began)
                {

                    hit.transform.gameObject.SendMessage(Constants.TOUCH_BEGIN_TEXT); // nom de méthode
                }
                else if (touches[i].phase == TouchPhase.Ended || touches[i].phase == TouchPhase.Canceled)
                {
                    hit.transform.gameObject.SendMessage(Constants.TOUCH_END_TEXT); // nom de méthode
                }
                else
                {
                    hit.transform.gameObject.SendMessage(Constants.TOUCH_HOLD_TEXT); // nom de méthode
                }

            }
        }
    }

    //BEN_REVIEW : Voir Camera.ScreenToWorldPoint()
    /// <summary>
    /// Convertit une position en pixels (dans l'écran) en position dans l'espace de jeu. Nécessite les constantes indiquées.
    /// </summary>
    /// <returns>The position to world.</returns>
    /// <param name="screenPosition">Screen position.</param>
    private Vector3 ScreenPositionToWorld(Vector2 screenPosition)
    {
        float x = Constants.MIN_WORLD_X;
        float y = Constants.MIN_WORLD_Y;
        float z = 0f;

        z = transform.position.z;

        float deltaX = Constants.MAX_WORLD_X - Constants.MIN_WORLD_X;
        float deltaY = Constants.MAX_WORLD_Y - Constants.MIN_WORLD_Y;

        float percentScreenX = screenPosition.x / Screen.width;
        float percentScreenY = screenPosition.y / Screen.height;

        x += percentScreenX * deltaX;
        y += percentScreenY * deltaY;

        return new Vector3(x, y, z);
    }

	/// <summary>
	/// Met fin à la partie et montre les statistiques
	/// </summary>
    public void EndGame()
    {
        EndGameController.player = GetPlayerInstance();
        UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.ENDGAME_SCENE_NAME);
    }
}
