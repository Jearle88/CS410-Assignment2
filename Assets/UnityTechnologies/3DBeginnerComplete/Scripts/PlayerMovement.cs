using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int count;

    public InputAction MoveAction;
    
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    public AudioSource collect_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    Vector3 startpos;
    Vector3 endpos;

    public float speed = 1.0F;
    private float journeyLength;

     private float startTime;

    void Start ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
        
        MoveAction.Enable();

        count = 0;
    }

    void FixedUpdate ()
    {
        var pos = MoveAction.ReadValue<Vector2>();
        
        float horizontal = pos.x;
        float vertical = pos.y;
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);
        
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);

    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("Kid"))
        {
            // Plays audio when picked up
            collect_AudioSource.Play();

            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
            
            // Increment the count of "Kid" objects collected.
            count = count + 1;
        }
    }
    /*private void Update()
    {

       //first get start and end pos
        // end pos will be 2 units forward
        //use lerp to dash across the distance
       startpos=GetComponent<Rigidbody>().transform.position;
       endpos=startpos;
       endpos.x=endpos.x+10f; 
      int dash_count=1;
    
    journeyLength= Vector3.Distance(startpos,endpos);
      if(Input.GetKeyDown(KeyCode.Space) )
      {
         //if(dash_count>0){
            startTime=Time.time;
             float distCovered = (Time.time - startTime) * speed;
              float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startpos, endpos, fractionOfJourney);
         //}  
      }
}
} */
}