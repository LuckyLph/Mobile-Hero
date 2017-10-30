using System.Collections;
using UnityEngine;

/// <summary>
/// Instance de note dans le jeu, avec aspect
/// </summary>
public class Note : MonoBehaviour
{
    private int column;
    private float speed;
    public int Worth { get; private set; }
    private bool counted;
    private Player player;

    // Use this for initialization
	private void Start()
    {
        GetPlayer();
        counted = false;
    }

    // Update is called once per frame
	private void Update()
    {
        //Move
        if (!counted)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0, Space.World);
        }
        if (transform.position.y <= Constants.OUT_OF_BOUND_Y)
        {
            player.ResetStreak();
            player.AddTotalNote();
            Destroy(gameObject);
        }

    }
    /// <summary>
    /// Regle les parametres de la note selon ce qui est précisé
    /// </summary>
    /// <param name="_column">Colonne </param>
    /// <param name="_speed">Vitesse</param>
    /// <param name="_worth">Valeur de la note</param>
    public void SetParameters(int _column, float _speed, int _worth)
    {
        column = _column;
        speed = _speed;
        Worth = _worth;
        GetComponent<SpriteRenderer>().color = GameObject.Find(Constants.LINE_NAME + column).GetComponent<SpriteRenderer>().color;
    }

	private void GetPlayer()
    {
        player = GameObject.Find(Constants.CAMERA_NAME).GetComponent<MobileHero>().GetPlayerInstance();
    }

    /// <summary>
    /// Renvoie si la note a été appuyée au bon moment
    /// </summary>
    /// <returns><c>true</c> Si la note est éligible à ; sinon, <c>false</c>.</returns>
    /// <param name="column">Column.</param>
    public bool IsInRange(int column)
    {
        if (counted)
        {
            return false;
        }
        bool returnValue = ((transform.position.y > Constants.IN_RANGE_BOTTOM_Y) && (transform.position.y < Constants.IN_RANGE_TOP_Y) && this.column == column);
        if (returnValue)
        {
            counted = true;
            StartCoroutine(GrowAndDie());
        }
        return returnValue;
    }

    /// <summary>
    /// Grows the and die.
    /// </summary>
    /// <returns>The and die.</returns>
    private IEnumerator GrowAndDie()
    {
        //Effet de lumière, très bref
        Instantiate((GameObject)Resources.Load(System.IO.Path.Combine(Constants.PREFAB_PATH, Constants.FIREBALL_NAME)), transform.position, Quaternion.identity);
        float growPerIteration = Constants.GROWTH_BEFORE_DEATH;
        growPerIteration /= Constants.TIME_TO_GROW;

        float currentGrowth = 1f;

        while (currentGrowth < Constants.GROWTH_BEFORE_DEATH)
        {
            transform.localScale *= 1 + (growPerIteration * Time.deltaTime);
            currentGrowth *= 1 + (growPerIteration * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}

/// <summary>
/// Classe qui représente une note de jeu.
/// </summary>
public class LogicalNote
{
    public int Column { get; private set; }
    public float Delay { get; private set; }

    public LogicalNote(int column, float delay)
    {
        Column = column;
        Delay = delay;
    }
}

