using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public int mouseSensitivity;
    public Transform player;
    public Transform cam;
    public float xRotation;
    public float xOffset;
    public float yOffset;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        var xTotal = Mathf.Clamp(xRotation + xOffset, -90f, 90f);

        cam.localRotation = Quaternion.Euler(xTotal, yOffset, 0);
        player.transform.Rotate(Vector3.up * mouseX);
    }
}
