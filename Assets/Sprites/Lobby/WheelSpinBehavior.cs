using System;
using UnityEngine;

public class WheelSpinBehavior : MonoBehaviour
{
    public float rotationValue = 0.0f;
    public float rotationSpeed = 0.0f;
    private Animator anim;
    private Unity.Mathematics.Random random;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("duration", UnityEngine.Random.Range(1.75f, 2.75f));
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("duration", anim.GetFloat("duration") - Time.deltaTime);
        rotationValue += rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, 0, rotationValue);
    }
}
