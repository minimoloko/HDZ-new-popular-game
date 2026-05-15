using System;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WheelSpinBehavior : MonoBehaviour
{
    static int gamesSelected = 0;
    public float rotationValue = 0.0f;
    public float rotationSpeed = 0.0f;
    private Animator anim;
    private Unity.Mathematics.Random random;
    public Sprite[] gameIcons = new Sprite[8];
    public string[] gameScenes = new string[3];
    private bool finished;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("duration", 1.75f);
        if (gamesSelected == 1)
            rotationValue = -45.0f;
        if (gamesSelected == 2)
            anim.SetBool("explosion", true);

        for (int i = 0; i < gameIcons.Length; i++)
        {
            var icon = gameIcons[i];
            var image = GameObject.Find("GameImage" + i + "").GetComponent<UnityEngine.UI.Image>();
            image.sprite = icon;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (finished) return;
        var duration = anim.GetFloat("duration") - Time.deltaTime;
        rotationValue += rotationSpeed * Time.deltaTime;
        anim.SetFloat("duration", duration);
        anim.SetFloat("rotation", Quaternion.Angle(
            Quaternion.Euler(0, 0, rotationValue),
            Quaternion.Euler(0, 0, Mathf.Round((rotationValue) / 45.0f) * 45.0f - 22.5f))) ;
        transform.localRotation = Quaternion.Euler(0, 0, rotationValue);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Done"))
        {
            FinishSpin();
            finished = true;
        }
    }

    void FinishSpin()
    {
        int gameIndex = gamesSelected++;
        Debug.Log(gameIndex);
        StartCoroutine(WaitAndLoad(gameScenes[gameIndex], 0.5f));
    }

    private IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);
    }
}
