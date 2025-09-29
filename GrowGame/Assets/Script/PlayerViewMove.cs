using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewMove : MonoBehaviour
{
    public float moveSpeed;

    public float minX = -20f;
    public float maxX = 100f;
    public float minZ = -105f;
    public float maxZ = 40f;

    private void Update()
    {
        ViewMove();
        ClampPosition();
    }

    private void ViewMove()
    {
        Vector3 dir = Vector3.zero;

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            dir += Vector3.forward;
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            dir += Vector3.back;

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            dir += Vector3.left;
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            dir += Vector3.right;

        if (dir != Vector3.zero)
        {
            transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void ClampPosition()
    {
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x, minX, maxX);
        p.z = Mathf.Clamp(p.z, minZ, maxZ);
        transform.position = p;
    }
}
