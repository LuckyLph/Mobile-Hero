using UnityEngine;

/// <summary>
/// Le controlleur de l'interface visuel. patron singleton pour accès rapide.
/// </summary>
public class UIController : MonoBehaviour
{
    private RectTransform canvas;
    private UnityEngine.UI.Text score;
    private UnityEngine.UI.Text streak;
    private Player player;

    private static UIController instance;
    private const float growthUponHit = 1.4f;

	private void Start()
    {
        InitializeVars();
        UpdateScreen();
    }

    /// <summary>
    /// Initialize les variables (liens vers les GUIText)
    /// </summary>
	private void InitializeVars()
    {
        player = GameObject.Find(Constants.CAMERA_NAME).GetComponent<MobileHero>().GetPlayerInstance();
        canvas = GameObject.Find(Constants.CANVAS_NAME).GetComponent<RectTransform>();
        score = canvas.Find(Constants.TEXT_SCORE_NAME).GetComponent<UnityEngine.UI.Text>();
        streak = canvas.Find(Constants.TEXT_STREAK_NAME).GetComponent<UnityEngine.UI.Text>();
    }

    /// <summary>
    /// Met à jour les GUI afin de correspondre aux valeurs du joueur (Player)
    /// </summary>
    public void UpdateScreen()
    {
        score.text = Constants.TEXT_SCORE_TEXT + player.Score;
        streak.text = Constants.TEXT_STREAK_TEXT + player.Streak;
    }
	/// <summary>
	/// Fait grossir le GUI du streak
	/// </summary>
    public void GrowStreakUi()
    {
        //BEN_CORRECTION : Où est le deltatime là dedans ? Toujours utiliser le deltatime dans les calculs
        //                 affectant directement le jeu.
        streak.transform.localScale = streak.transform.localScale * growthUponHit;
    }

    /// <summary>
    /// Retourne l'instance unique de cette classe. Utile pour accès très rapide
    /// </summary>
    /// <returns>The instance.</returns>
    public static UIController GetInstance()
    {
        //BEN_CORRECTION : Trop lent. Deux appels à Find pour la même chose. Améliorable.
        if (instance == null && GameObject.Find(Constants.CAMERA_NAME) != null)
        {
            instance = GameObject.Find(Constants.CAMERA_NAME).GetComponent<UIController>();
        }
        return instance;
    }
}
