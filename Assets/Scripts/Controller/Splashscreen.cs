using UnityEngine;

/// <summary>
/// Cette classe gère l'écran de splashscreen
/// </summary>
public class Splashscreen : MonoBehaviour
{

	private void Update()
    {
        if (Input.anyKey)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.MENU_SCENE_NAME);
        }
    }
}
