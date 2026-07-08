using System.Collections;
using TMPro;
using UnityEngine;

static public class Utils
{
    static public IEnumerator RevealTextOverTime(TextMeshProUGUI textMeshpro, float timeDelay, string text, int maxLength)
    {
        textMeshpro.maxVisibleCharacters = 0;
        textMeshpro.text = text;

        while (textMeshpro.maxVisibleCharacters != text.Length && textMeshpro.maxVisibleCharacters != maxLength)
        {
            textMeshpro.maxVisibleCharacters++;

            yield return new WaitForSeconds(timeDelay);
        }
    }
}
