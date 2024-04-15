using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameObject player;

    private Vector3 _angles;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 d = player.transform.position - transform.position;

        d.Normalize();

        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(Vector3.forward, d));

        Vector3 cross = Vector3.Cross(Vector3.forward, d);
        if (cross.y < 0.0f)
        {
            angle = -angle;
        }

        _angles.y = angle;
        transform.eulerAngles = _angles;
    }

}
