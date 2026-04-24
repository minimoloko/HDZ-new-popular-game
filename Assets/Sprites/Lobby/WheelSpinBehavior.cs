using UnityEngine;

public class WheelSpinBehavior : MonoBehaviour
{
    public float rotationValue = 0.0f;
    public float rotationSpeed = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        rotationValue += rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, 0, rotationValue);
    }
}
