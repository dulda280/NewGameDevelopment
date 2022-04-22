using System; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.AI; 
using UnityEngine.UI; 
 
public class NPC_Path_Behaviour : MonoBehaviour 
{ 
    [SerializeField] private Transform[] waypoints; 
    [SerializeField] private float moveSpeed = 2f; 
 
    private int waypointIndex = 0; 
     
    void Start() 
    { 
        transform.position = waypoints[waypointIndex].transform.position; 
    } 
 
    void Update() 
    { 
        Move(); 
    } 
 
    void Move() 
    { 
        if (waypointIndex <= waypoints.Length - 1) 
        { 
            transform.position = Vector3.MoveTowards(transform.position, 
                waypoints[waypointIndex].transform.position, 
                moveSpeed * Time.deltaTime); 
 
            if (transform.position == waypoints[waypointIndex].transform.position) 
            { 
                waypointIndex += 1; 
            } 
        } 
    } 
}