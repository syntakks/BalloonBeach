using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float offset = 10;
    public float cameraSpeed = 100f;

    private void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(0f, gameObject.transform.position.y, player.transform.position.z - offset);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPosition, cameraSpeed * Time.deltaTime);
    }
}
