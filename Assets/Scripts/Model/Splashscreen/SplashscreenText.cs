using System.Collections;
using UnityEngine;

public class SplashscreenText : MonoBehaviour
{

    public void Start()
    {
        StartCoroutine(UpdateTextSize());
    }
    
    //BEN_CORRECTION : Vide. Perte de performances (et oui!).
    public void Update()
    {

    }

    public IEnumerator UpdateTextSize()
    {
        //BEN_REVIEW : Prenez garde à ne pas faire GetComponent fait à chaque frame. 
        //             C'est INCOMENSURABLEMENT LENT! Ce n'est pas votre cas ici, car cette partie
        //             n'est fait qu'une seule fois dans la Coroutine, mais juste garder ça en tête.
        Vector3 startScale = GetComponent<RectTransform>().localScale;
        Vector3 endScale = (startScale * Constants.SPLASH_SCREEN_ZOOM_TOTAL_GROWTH);
        float multiplicatorPerSecond = Constants.SPLASH_SCREEN_ZOOM_TOTAL_GROWTH / Constants.SPLASHSCREEN_ZOOM_GROWTH_TIME;
        RectTransform localTransform = GetComponent<RectTransform>();
        int growthPolarity = 1;

        //BEN_CORRECTION : Aurait mérité un découpage en petites méthodes pour simplifier la lecture.
        while (true)
        {
            localTransform.localScale *= 1 + (multiplicatorPerSecond * Time.deltaTime * growthPolarity);
            if (localTransform.localScale.x >= endScale.x)
            {
                growthPolarity = -1;
            }
            else if (localTransform.localScale.x <= startScale.x)
            {
                growthPolarity = 1;
            }
            yield return null;
        }
    }
}
