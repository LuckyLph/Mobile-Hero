using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Cette classe permet de jouer des sons sur plusieurs channels, en global ou en 3D
/// </summary>
public static class SoundPlayerCore
{

    /// <summary>
    /// La liste des sources de sons. Attention: peut contenir des NULLs, si le son a terminé de jouer
    /// </summary>
    private static List<AudioSource> listeSources = new List<AudioSource>();

    /// <summary>
    /// Joue un son de manière globale
    /// </summary>
    /// <param name="filename">Le nom du fichier, situé dans le dossier Resources</param>
    /// <param name="objectName">Le nom du gameobject créé</param>
    /// <param name="percentVolume">Le volume, en pourcent (%)</param>
	public static void PlaySound(string filename, string objectName = "Soundplayer", int percentVolume = 100, bool autodestroy = true)
    {
        try
        {
            AudioClip toPlay = Resources.Load<AudioClip>(filename);
            GameObject globalObject = Resources.Load<GameObject>(Path.Combine(Constants.PREFAB_PATH, "GlobalSound"));
            GameObject clone = Object.Instantiate(globalObject, Vector3.zero, Quaternion.identity);
            AudioSource source = clone.GetComponent<AudioSource>();
            clone.name = objectName;
            if (!autodestroy)
            {
                clone.GetComponent<AutoDestroyUponSoundEnded>().enabled = false;
            }
            source.volume = ((float)percentVolume / 100f);
            source.clip = toPlay;
            source.Play();
            listeSources.Add(source);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.StackTrace);
        }
    }

    /// <summary>
    /// Delayeds the sound.
    /// </summary>
    /// <param name="filename">Filename.</param>
    /// <param name="delay">Delay.</param>
    /// <param name="objectName">Object name.</param>
    /// <param name="percentVolume">Percent volume.</param>
	public static void DelayedSound(string filename, float delay, string objectName = "Soundplayer", int percentVolume = 100, bool autodestroy = true)
    {
        try
        {
            AudioClip toPlay = Resources.Load<AudioClip>(filename);
            GameObject globalObject = Resources.Load<GameObject>(Path.Combine(Constants.PREFAB_PATH, "GlobalSound"));
            GameObject clone = Object.Instantiate(globalObject, Vector3.zero, Quaternion.identity);
            AudioSource source = clone.GetComponent<AudioSource>();
            source.clip = toPlay;
            source.pitch = Constants.TIMESCALE;
            if (!autodestroy)
            {
                clone.GetComponent<AutoDestroyUponSoundEnded>().enabled = false;
            }
            clone.name = objectName;
            source.volume = ((float)percentVolume / 100f);
            source.PlayDelayed(delay);
            listeSources.Add(source);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.StackTrace);
        }
    }

    /// <summary>
    /// Joue un son à la position voulue (son 3D)
    /// </summary>
    /// <param name="filename">Le nom du fichier, situé dans le dossier Resources</param>
    /// <param name="position">La position de la source audio</param>
    /// <param name="range">La portée du son</param>
    /// <param name="objectName">Le nom du gameobject créé</param>
    /// <param name="percentVolume">Le volume, en pourcent (%)</param>
    public static void PlaySound(string filename, Vector3 position, float range, string objectName = "Soundplayer", int percentVolume = 100)
    {
        try
        {
            AudioClip toPlay = Resources.Load<AudioClip>(filename);
            GameObject globalObject = Resources.Load<GameObject>(Path.Combine(Constants.PREFAB_PATH, "LocalSound"));
            GameObject clone = Object.Instantiate(globalObject, position, Quaternion.identity);
            AudioSource source = clone.GetComponent<AudioSource>();
            clone.name = objectName;
            source.volume = ((float)percentVolume / 100f);
            source.maxDistance = range;
            source.clip = toPlay;
            source.Play();
            listeSources.Add(source);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.StackTrace);
        }
    }

    /// <summary>
    /// Retourne un tableau de toutes les sources non nulles (qui jouent en ce moment même)
    /// </summary>
    /// <returns>Toutes les sources non nulles</returns>
    public static AudioSource[] GetAllSources()
    {
        AudioSource[] returnValue;
        int nonNullSourcesAmount = 0;
        for (int i = 0; i < listeSources.Count; i++)
        {
            if (listeSources[i] != null)
            {
                nonNullSourcesAmount++;
            }
        }
        returnValue = new AudioSource[nonNullSourcesAmount];
        int index = 0;
        for (int i = 0; i < listeSources.Count; i++)
        {
            if (listeSources[i] != null)
            {
                returnValue[index] = listeSources[i];
                index++;
            }
        }
        return returnValue;
    }
}
