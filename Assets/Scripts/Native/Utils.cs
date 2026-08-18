using System.Collections;
using TMPro;
using UnityEngine;

static public class Utils
{
    static public IEnumerator RevealTextOverTime(
        TextMeshProUGUI textMeshpro,
        float timeDelay,
        string text,
        int maxLength,
        AudioSource audioSource = null)
    {
        textMeshpro.maxVisibleCharacters = 0;
        textMeshpro.text = text;

        if (audioSource)
            audioSource.Play();

        while (textMeshpro.maxVisibleCharacters != text.Length && textMeshpro.maxVisibleCharacters != maxLength)
        {
            textMeshpro.maxVisibleCharacters++;

            yield return new WaitForSeconds(timeDelay);
        }

        if (audioSource)
            audioSource.Stop();
    }


    public static T FindOrThrow<T>(System.Func<T> finder) where T : UnityEngine.Object
    {
        T val = finder.Invoke();

        if (!val)
            throw new UnityEngine.MissingReferenceException($"The object of type {typeof(T).Name} was not found ");

        return val;
    }
}
