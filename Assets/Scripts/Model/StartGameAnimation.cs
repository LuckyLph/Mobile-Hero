using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe, attachée à la caméra, fera une jolie animation de début de partie.
/// </summary>
public class StartGameAnimation : MonoBehaviour
{
    private Vector3 endPosition;
    [SerializeField]
    private float startFromMinus;
    [SerializeField]
    private float speed;


	private void Start()
    {
        endPosition = transform.position;
        transform.Translate(0, -startFromMinus, 0);
    }

	private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, endPosition, speed * Time.deltaTime);
        if (transform.position == endPosition)
        {
            enabled = false;
        }
    }
}
