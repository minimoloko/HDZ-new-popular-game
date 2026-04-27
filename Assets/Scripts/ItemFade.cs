using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;

    IEnumerator FadeOut()
    {
        float timer = 0;
        Color startColor = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha); // fading

            yield return new WaitForEndOfFrame(); // wait frame
        }

        fadeImage.gameObject.SetActive(false);
    }


    public void StartFade()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

}
