using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Ns1{

//using UnityStandardAssets.Characters.ThirdPerson;   
public class PlayerMotion : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public GameObject player;
    public Animator animator_train;
    public Animator animator_door;
    public Vector3 destination;
    public TrainMotion trainMotion;
    public string[] doors = new string[2]{"train1","train2"};
    public bool isInTrain ;
    public bool shouldExitTrain ;
    public bool shouldEnterTrain ;


    // Start is called before the first frame update
    void Start()
    {
        shouldExitTrain = false;
        shouldEnterTrain = true;
        isInTrain = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
          Ray ray =  cam.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;

           if(Physics.Raycast(ray,out hit)){
               agent.SetDestination(hit.point);
           }

        } 
        if(animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdleDic")&&
            agent.transform.position.x >= 807 ){
            destination =  new Vector3(800.08f,28.65f,645f);
            agent.SetDestination(destination);
        }
        
        if(isInTrain){
            if(!animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdle")  ||
            !animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdleDic")){
                destination = GameObject.FindWithTag("train").transform.position;
                player.transform.position = new Vector3(destination.x-20, 28.65f, 630);
                agent.enabled = false;   
            }
            if(animator_door.GetCurrentAnimatorStateInfo(0).IsName("DoorClose")){
                shouldExitTrain = true;
            }
            if(animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdle")  ||
                animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdleDic")){
                destination = GameObject.FindWithTag("train").transform.position;
                player.transform.position = new Vector3(destination.x-20, 28.65f, 630);
                agent.enabled = true;   

            }
        }
        if(shouldEnterTrain)
        handleAgentEnterTrain();
        if(shouldExitTrain)
        handleAgentExitTrain();
       

    }

    void getToTheStationEast(){        
        shouldEnterTrain = true;
        trainMotion.addPassengerEast();

    }
    void getToTheStationDic(){
          shouldEnterTrain = true;
        trainMotion.addPassengerDic();
    }


    void  handleAgentEnterTrain(){
          if (animator_door.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen") && shouldEnterTrain)
        {

            if((animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdle") && player.tag == "Player")
            || (animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdleDic") && player.tag == "player2")){
            int randWhichDoor  = Random.Range(0, 1); 
            string door= doors[randWhichDoor];
            destination = GameObject.FindWithTag(door).transform.position;
            if(player.tag == "Player"){
            agent.SetDestination(destination);     
            } else {
            agent.SetDestination(destination);                   
            }
            shouldEnterTrain = false;
            
        }
        }

    }
    void  handleAgentExitTrain(){
        
        if( shouldExitTrain && animator_door.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen")){
            if (animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdle"))
            {
            shouldExitTrain=false;
            isInTrain=false;
            destination = GameObject.FindWithTag("stationEast").transform.position;
            agent.SetDestination(destination);
            handlePlayerInEast();
            Debug.Log("destination trainIdle : "+ destination);

            }

            if (animator_train.GetCurrentAnimatorStateInfo(0).IsName("trainIdleDic")){  
            shouldExitTrain=false;
            isInTrain=false;
            destination = new Vector3(807f,28.65f,635f);
            agent.Warp(destination);
            handlePlayerInDic();
            }
        }        
    }

    void handlePlayerInEast(){

                //move the player random in the city

    }
    
    void handlePlayerInDic(){   //move the player random in the city
        destination = GameObject.FindWithTag("station").transform.position;
        // player.transform.position = destination; 
        agent.SetDestination(destination);

             
    }

}
}