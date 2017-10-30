using UnityEngine;

/// <summary>
/// Représente le joueur et ses statistiques
/// </summary>
public class Player
{
    private int score = 0;
    private int streak = 0;
    private int highestStreak = 0;
    private int totalNotes = 0;
    private int successfulNotes = 0;

    //BEN_CORRECTION : Constructeur vide à supprimer.
    public Player()
    {

    }

    /// <summary>
    /// Retourne le score actuel du joueur
    /// </summary>
    /// <returns>The score.</returns>
    public int Score { get { return score; } }

    /// <summary>
    /// Retourne le streak actuel (notes réussies en ligne)
    /// </summary>
    /// <returns>The streak.</returns>
    public int Streak { get { return streak; } }

    /// <summary>
    /// Ajoute 1 au nombre total de notes passées
    /// </summary>
    public void AddTotalNote()
    {
        totalNotes++;
        UpdateScreenGUI();
    }

    //BEN_CORRECTION : Percent de quoi ? On devrait pas avoir tout le temps à référer à la doc
    //                 pour ces informations.
    /// <summary>
    /// Retourne le pourcentage de notes validées
    /// </summary>
    /// <returns>The percent.</returns>
    public int GetPercent()
    {
        if (totalNotes == 0)
        {
            return 100;
        }
        float percentOnOne = successfulNotes / (float)totalNotes;
        return Mathf.RoundToInt(percentOnOne * 100);
    }

    /// <summary>
    /// Augmente le score du joueur
    /// </summary>
    /// <param name="worth">La valeur de la note</param>
    /// <param name="distance">La distance entre la note et le bouton</param>
    public void IncreaseScore(int worth, float distance)
    {
        score += GetScoreIncrease(worth, distance, streak);
        UpdateScreenGUI();
    }

    /// <summary>
    /// Retourne le score à ajouter au joueur.
    /// </summary>
    /// <returns>Les points à ajouter</returns>
    /// <param name="noteWorth">La valeur de base de la notes</param>
    /// <param name="_distance">La distance à laquelle la note a été touchée (par rapport au bouton)</param>
    /// <param name="multiplicator">Le multiplicateur de note (streak)</param>
	private int GetScoreIncrease(int noteWorth, float _distance, int multiplicator)
    {
        float scoreInscrease = noteWorth - (_distance * 2f);
        scoreInscrease += Mathf.Sqrt(multiplicator);
        return Mathf.RoundToInt(scoreInscrease);
    }

    /// /// <summary>
    /// Ajoute 1 au streak
    /// </summary>
    /// <returns>The streak.</returns>
    public int AddStreak()
    {
        streak++;
        successfulNotes++;
        AddTotalNote();
        if (GameObject.Find(Constants.MUSIC_OBJECT_NAME) != null)
        {
            GameObject.Find(Constants.MUSIC_OBJECT_NAME).GetComponent<AudioSource>().volume = Constants.VOLUME_MUSIC;
        }

        if (streak > highestStreak)
        {
            highestStreak = streak;
        }
        UpdateScreenGUI();
        if (UIController.GetInstance() != null)
        {
            UIController.GetInstance().GrowStreakUi();
        }
        return streak;
    }

    /// <summary>
    /// Remet le streak à 0
    /// </summary>
    public void ResetStreak()
    {
        streak = 0;
        if (GameObject.Find(Constants.MUSIC_OBJECT_NAME) != null)
        {
            GameObject.Find(Constants.MUSIC_OBJECT_NAME).GetComponent<AudioSource>().volume = Constants.VOLUME_MUSIC_MISSING;
        }
        UpdateScreenGUI();
    }

    /// <summary>
    /// Retourne le streak le plus haut atteint par le joueur dans cette partie
    /// </summary>
    /// <returns>The highest streak.</returns>
    public int GetHighestStreak()
    {
        return highestStreak;
    }

    /// <summary>
    /// Retourne le plus haut score que le joueur puisse avoir sur sa dernière session
    /// </summary>
    /// <returns>Le score maximum.</returns>
    public int GetMaximumScore()
    {
        int returnValue = 0;
        for (int i = 0; i < totalNotes; i++)
        {
            returnValue += GetScoreIncrease(Constants.NOTE_WORTH, 0f, i);

        }
        return returnValue;
    }

    /// <summary>
    /// Met à jour les éléments visuels de l'écran afin de refléter les données de cette instance
    /// </summary>
	private void UpdateScreenGUI()
    {
        if (UIController.GetInstance() != null)
        {
            UIController.GetInstance().UpdateScreen();
        }

    }
}
