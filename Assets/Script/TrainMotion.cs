using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrainMotion : MonoBehaviour
{

    public int passengerCount;
    public Animator animator_door;
    public Animator animator_door1;
    public Animator animator_train;
    public int passengerEast;
    public int passengerDic;
    public bool needToOpen;
    public bool flagEnter;
    public GameObject player;
    public Vector3 destination;
  




    // Start is called before the first frame update
    void Start()
    {
        passengerCount = 0;
        passengerEast = 1;
        passengerDic = 0;
        needToOpen = true;
        flagEnter= true;
    }

    // Update is called once per frame
    void Update()
    {
        handleDoorsTrain();

    }

    void handleDoorsTrain()
    {
        if(!animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdle")&& 
        !animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdleDic")){
            needToOpen=true;
            flagEnter=true;
        }
        if (animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdle"))
        {
            if(needToOpen){
                needToOpen = false; 
                animator_door.SetTrigger("open");
                animator_door1.SetTrigger("open");
            }
            //EnterOnlyOnce
            if(flagEnter){
                passengerEast+=passengerCount;
                flagEnter=false;
            }
            if (passengerEast == 0)
            {
                animator_door.SetTrigger("close");
                animator_door1.SetTrigger("close");
                animator_train.SetTrigger("drive");
            }
        }
        if (animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdleDic"))
        {   
          
            // destination = GameObject.FindWithTag("train").transform.position;
            // player.transform.position = new Vector3(destination.x-20, 28.65f, 630); 
            if(needToOpen){
                needToOpen = false; 
                animator_door.SetTrigger("open");
                animator_door1.SetTrigger("open");
            }
            //EnterOnlyOnce
            if(flagEnter){
                passengerDic+=passengerCount;
                flagEnter=false;
            }
            if (passengerDic == 0)
            {
                animator_door.SetTrigger("close");
                animator_door1.SetTrigger("close");
                animator_train.SetTrigger("drive");
            }
        }
    }

    public void addPassengerCount(){
        passengerCount++;
    }


    public void addPassenger()
    {
        if (animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdle"))
        {
            addPassengerEast();
        }
        if (animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdleFic"))
        {
            addPassengerDic();
        }
    }
    public void addPassengerEast()
    {
        passengerEast++;

    }
    public void addPassengerDic()
    {
        passengerDic++;
    }
  public void removePassengerCount(){
        passengerCount--;
    }

    public void removePassenger()
    {
        if (animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdle"))
        {
            removePassengerEast();
        }
        if (animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdleFic"))
        {
            removePassengerEast();
        }
    }

    void removePassengerEast()
    {
        passengerEast--;

    }
    void removePassengerDic()
    {
        passengerDic--;

    }
}
