using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairFollow : MonoBehaviour
{
    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        transform.position = mousePos;
    }
}