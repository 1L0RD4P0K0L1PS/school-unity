using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRoamCamera : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of the camera's movement
    public float lookSpeed = 3f;  // Speed of the camera's rotation
    public float verticalLookLimit = 80f; // Limit to how far up or down the camera can look

    private float pitch = 0f;  // Vertical rotation (up/down)
    private float yaw = 0f;    // Horizontal rotation (left/right)

    void Update()
    {
        // Get input for camera movement (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");    // W/S or Up/Down
        float moveY = 0f;

        // Optional: Use the mouse scroll wheel to move up/down
        if (Input.GetKey(KeyCode.Space)) moveY = 1f; // Move Up with Space key
        if (Input.GetKey(KeyCode.LeftControl)) moveY = -1f; // Move Down with Ctrl key

        // Move the camera using keyboard input
        Vector3 move = new Vector3(moveX, moveY, moveZ) * moveSpeed * Time.deltaTime;

        // Apply the movement to the camera
        transform.Translate(move, Space.World);

        // Get mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Rotate the camera based on mouse input
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -verticalLookLimit, verticalLookLimit);  // Clamp vertical rotation to avoid flipping

        // Apply the rotation to the camera
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}