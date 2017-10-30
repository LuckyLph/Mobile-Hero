using UnityEngine;

/// <summary>
/// Détruit le gameoobject lorsque les particules terminent
/// </summary>
public class AutoDestroyUponParticleEnded : MonoBehaviour
{

	private void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}
