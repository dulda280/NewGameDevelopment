using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
 [SerializeField] private Transform Player;
 [SerializeField] private Transform respawnPoint;

//Object that triggers the respawn(Enemy, out of bounds, etc.) needs to have a box collider with is Trigger ticked
//Script is attached to the danger not the player, once the script is attached drag a respawnpoint and the player onto it
//Remember to tag player
 private void OnTriggerEnter(Collider other)
 {
   if (other.CompareTag("Player"))
   {
    Player.transform.position = respawnPoint.transform.position;
    Physics.SyncTransforms();
   }
 }
}
