using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    public GameObject Gargoyels;
     private Vector3 _angles;

    bool m_IsPlayerInRange;

    void OnTriggerEnter (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update ()
    {

        
        if (m_IsPlayerInRange)
        {
            float maxSpeed = 5.0f;
        float minSpeed = 1.0f;
        float alpha = 1.0f; // normalized to [0.0, 1.0] scale
        float interpolatedSpeed = (1.0f - alpha) * minSpeed + alpha * maxSpeed;

        transform.Rotate (_angles * interpolatedSpeed * Time.deltaTime);

            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            
            if (Physics.Raycast (ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer ();
                }
            }
        }
        
    }
    
}
