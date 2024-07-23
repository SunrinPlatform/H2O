using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform player;
    [SerializeField] float smoothing = 0.2f;

    [SerializeField] float yPos;

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y + yPos, this.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}
