using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Ns1;

public class DoorsMotion : MonoBehaviour
{
    public Animator animator_door;
    public Animator animator_train;
    public TrainMotion trainMotion;
    public Vector3 destination;
    

    // Start is called before the first frame update
    void Start()
    {
    animator_door = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     Vector3 direction = other.transform.position - transform.position;
    //     if ( other.gameObject.tag == "Player")
    //     {   
            
    //     }
    // }

    void OnTriggerExit(Collider other)
    {
        Vector3 direction = other.transform.position - transform.position;
        if ( other.gameObject.tag == "Player" || other.gameObject.tag == "player2" )
        {   
             if (Vector3.Dot (transform.forward, direction) > 0) {
                  PlayerMotion playerMotion = other.gameObject.GetComponent<PlayerMotion>();          
                destination = GameObject.FindWithTag("train1").transform.position;
                if(animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdleDic"))
                GameObject.FindWithTag("player2").transform.position = destination;     
                trainMotion.passengerDic = 0;
                trainMotion.removePassengerCount();
                // trainMotion.removePassenger();
            } 
            if (Vector3.Dot (transform.forward, direction) < 0) {             
                trainMotion.addPassengerCount();
                trainMotion.removePassenger();
                PlayerMotion playerMotion = other.gameObject.GetComponent<PlayerMotion>();
                playerMotion.isInTrain = true;
    
            } 
          
        }

    }
}