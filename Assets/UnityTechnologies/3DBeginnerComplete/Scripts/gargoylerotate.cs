using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gargoylerotate : MonoBehaviour
{
  
     public Transform target;
     float rotationTime;

     Quaternion targetRotation;
    float speed = 1.0f;
    float timeCount = 0.0f;
    bool rotating;
    Quaternion originalRotationValue;

  

    // Start is called before the first frame update
    void Start()
    {
         
         //_angles = new Vector3(0.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        /*float maxSpeed = 20.0f;
        float minSpeed = 0.0f;
        float alpha = 1.0f; // normalized to [0.0, 1.0] scale
        */
        
        //float interpolatedSpeed = (1.0f - alpha) * minSpeed + alpha * maxSpeed;

        //transform.Rotate (_angles * interpolatedSpeed * Time.deltaTime);

         Vector3 relativePosition = target.position - transform.position;
			targetRotation = Quaternion.LookRotation (relativePosition);
			rotating = true;
            if (rotating){
			rotationTime = 0;
			rotationTime += Time.deltaTime * speed;
			transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, rotationTime);
         
     
    }
}
    }
    

