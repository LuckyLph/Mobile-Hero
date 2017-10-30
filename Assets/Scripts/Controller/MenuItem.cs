using UnityEngine;

public class MenuItem : MonoBehaviour
{
    private float currentOffset = 0;


    void Start()
    {

    }

    public void UpdatePosition(float offset)
    {
        transform.Translate(0, -(currentOffset - offset), 0);
        currentOffset = offset;
        /*transform.Find ("GraphicButton").*/
        gameObject.SetActive(transform.localPosition.y <= Constants.HIDE_BUTTONS_HIGHER_THAN);
    }
}
