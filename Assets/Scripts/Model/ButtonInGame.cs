using UnityEngine;
using System.IO;

//BEN_CORRECTION : Nommage.
/// <summary>
/// Représente un bouton dans le jeu. Gère lui-même l'appuis et la frappe d'une note (appelle la note)
/// </summary>
public class ButtonInGame : MonoBehaviour
{

    [SerializeField]
    private int column;
    [SerializeField]
    private Sprite pressedButton;


    private Sprite unpressedButton;
    private SpriteRenderer spriteRenderer;
    private Player player;
    private bool keyIsBeingPressed;

    //PC debug
    private KeyCode[,] codes = new KeyCode[2, 4] { { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R }, { KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L } };
    //PC debug

    private void Start()
    {
        LinkComponentAndObjects();
        //Copier la couleur de la ligne
        spriteRenderer.color = GameObject.Find(Constants.LINE_NAME + column).GetComponent<SpriteRenderer>().color;
        PreloadResources();
    }

    /// <summary>
    /// Relie les dépendances externes à cette classe
    /// </summary>
	private void LinkComponentAndObjects()
    {
        player = GameObject.Find(Constants.CAMERA_NAME).GetComponent<MobileHero>().GetPlayerInstance();
        spriteRenderer = GetComponent<SpriteRenderer>();
        unpressedButton = spriteRenderer.sprite;
    }

    /// <summary>
    /// Charche une ressource dans la mémoire vive afin de l'utiliser rapidement pas la suite.
    /// </summary>
	private void PreloadResources()
    {
        Resources.Load(Path.Combine(Constants.PREFAB_PATH, Constants.FIREWORK_OBJECT_NAME));
        Resources.Load(Path.Combine(Constants.PREFAB_PATH, Constants.FLAMES_OBJECT_NAME));
    }

	private void Update()
    {
#if !UNITY_ANDROID
        //PC debug
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            for (int i = 0; i < codes.GetLength(0); i++)
            {
                if (Input.GetKeyDown(codes[i, column]))
                {
                    OnTouchBegin();
                    break;
                }
                else if (!Input.GetKey(codes[i, column]))
                {
                    OnTouchEnd();
                }
                if (Input.GetKeyDown(KeyCode.U) && !keyIsBeingPressed)
                {
                    keyIsBeingPressed = true;
                    SpawnFireworks();
                }
                else if (Input.GetKeyDown(KeyCode.Y) && !keyIsBeingPressed)
                {
                    keyIsBeingPressed = true;
                    SpawnFlames();
                }
                else if ((Input.GetKeyUp(KeyCode.U) || Input.GetKeyUp(KeyCode.Y)) && keyIsBeingPressed)
                {
                    keyIsBeingPressed = false;
                }
                break;
            }
        }
#endif
        //PC DEBUG
    }

    /// <summary>
    /// Lorsque l'utilisateur touche le bouton longuement.
    /// </summary>
    public void OnTouchHold()
    {

    }

    /// <summary>
    /// Lorsque l'utilisateur touche le bouton (première frame)
    /// </summary>
    public void OnTouchBegin()
    {
        if (!MobileHero.IsPaused())
        {
            if (!TestAllNotes())
            {
                PressedWrongNote();
            }
            spriteRenderer.sprite = pressedButton;
        }
    }

    /// <summary>
    /// Lorsque l'utilisateur touche le bouton (dernière frame)
    /// </summary>
    public void OnTouchEnd()
    {
        spriteRenderer.sprite = unpressedButton;
    }

    /// <summary>
    /// Éxécute les démarches lorsque le bouton est appuyé inutilement
    /// </summary>
	private void PressedWrongNote()
    {
        player.ResetStreak();
        SoundPlayerCore.PlaySound(Path.Combine(Constants.SOUND_EFFECTS_PATH, Constants.GUITAR_MISS_PATH + Random.Range(1, 10)));
    }

    /// <summary>
    /// Teste toutes les notes afin de voir si l'une d'elle est éligible d'être comptée par ce bouton. S'arrête à une note compté
    /// </summary>
    /// <returns>Si une note à été validée</returns>
	private bool TestAllNotes()
    {
        //bool hasValidatedAtLeastOne = false;
        GameObject[] allNotes = GameObject.FindGameObjectsWithTag(Constants.OBJECT_TAG_NOTE);
        foreach (GameObject singleObject in allNotes)
        {

            if (singleObject.GetComponent<Note>().IsInRange(column))
            {
                HitNote(singleObject);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Valide une note
    /// </summary>
    /// <param name="note">L'objet de la note en question</param>
	private void HitNote(GameObject note)
    {
        player.IncreaseScore(note.GetComponent<Note>().Worth, Vector2.Distance(transform.position, note.transform.position));
        int streak = player.AddStreak();
        //Effects
        if (streak % Constants.SPAWN_FLAMES_EVERY == 0)
        {
            SpawnFlames();
            //les deux si deuxieme fois que les flammes spawn
            if (streak > Constants.SPAWN_FLAMES_EVERY)
            {
                SpawnFireworks();
            }
        }
        else if (streak % Constants.SPAWN_FIREWORK_EVERY == 0)
        {
            SpawnFireworks();
        }
    }

    /// <summary>
    /// Joue l'effet de feux d'artifice à l'écran
    /// </summary>
	private void SpawnFireworks()
    {
        Instantiate((GameObject)Resources.Load(Path.Combine(Constants.PREFAB_PATH, Constants.FIREWORK_OBJECT_NAME)), GameObject.Find(Constants.FIREWORK_SPAWN_NAME).transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Joue l'effet de flames à l'écran
    /// </summary>
	private void SpawnFlames()
    {
        Instantiate((GameObject)Resources.Load(Path.Combine(Constants.PREFAB_PATH, Constants.FLAMES_OBJECT_NAME)), GameObject.Find(Constants.FLAMES_SPAWN_NAME).transform.position, Quaternion.Euler(Constants.FLAMES_X_ROTATION, 0, 0));
    }

#if !UNITY_ANDROID
    /// <summary>
    /// Lorsque le bouton est cliqué
    /// </summary>
    public void OnMouseDown()
    {
        OnTouchBegin();
    }

    /// <summary>
    /// Lorsque le bouton est relaché
    /// </summary>
    public void OnMouseUp()
    {
        OnTouchEnd();
    }
#endif
}
