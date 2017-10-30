using UnityEngine;

/// <summary>
/// Détruit le gameobject lorsque le son est terminé
/// </summary>
public class AutoDestroyUponSoundEnded : MonoBehaviour
{

	private void Start()
    {
        Destroy(gameObject, GetComponent<AudioSource>().clip.length);
    }
}
