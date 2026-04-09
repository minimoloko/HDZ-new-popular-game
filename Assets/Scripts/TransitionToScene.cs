using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraEffects : MonoBehaviour
{
    public float targetSize = 2f;
    public float rotationAngle = 15f;
    public float duration = 1.5f;
    public Image fadeImage;

    private float originalSize;
    private Quaternion originalRotation;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam != null)
        {
            originalSize = cam.orthographicSize;
            originalRotation = transform.localRotation;
        }

        if (fadeImage)
        {
            fadeImage.color = new Color(0, 0, 0, 0);
            fadeImage.raycastTarget = false;
        }
    }

    public void StartZoomAndRotate(string nextSceneName)
    {
        if (cam == null) cam = GetComponent<Camera>();
        StartCoroutine(ZoomInRoutine(nextSceneName));
    }

    IEnumerator ZoomInRoutine(string nextSceneName)
    {
        if (fadeImage) fadeImage.raycastTarget = true;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);

            if (cam != null)
            {
                cam.orthographicSize = Mathf.Lerp(originalSize, targetSize, t);
            }

            transform.localRotation = originalRotation * Quaternion.Euler(0, 0, rotationAngle * t);

            if (fadeImage)
            {
                fadeImage.color = new Color(0, 0, 0, t);
            }

            yield return null;
        }
        SceneManager.LoadScene(nextSceneName);
    }
}
// idk what happens there
