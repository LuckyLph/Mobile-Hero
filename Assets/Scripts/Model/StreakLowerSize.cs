using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attaché au GUI du streak, il fera rétrécir le GUI jusqu'a sa taille normal.
/// </summary>
public class StreakLowerSize : MonoBehaviour
{

	private Vector3 startSize;
	private const float shrinkingPerSecond = 4f;
    // Use this for initialization
	private void Start()
    {
        startSize = transform.localScale;
    }

    // Update is called once per frame
	private void Update()
    {
        if (transform.localScale.x > startSize.x)
        {
            transform.localScale = transform.localScale * (Time.deltaTime * shrinkingPerSecond);
        }
        else
        {
            transform.localScale = startSize;
        }
    }
}
