using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class MovePlayer : MonoBehaviour 
{ 
    [SerializeField] private float speed = 5.0f; 
    [SerializeField] private float rotationSpeed = 5.0f; 
     
 
    private void Update() 
    { 
        var horizontal = Input.GetAxis("Horizontal"); 
        var vertical = Input.GetAxis("Vertical"); 
        Vector3 movementDirection = new Vector3(horizontal, 0, vertical); 
        movementDirection.Normalize(); 
        transform.Translate(movementDirection * (speed * Time.deltaTime)); 
        if (movementDirection != Vector3.zero) 
        { 
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up); 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,  
                rotationSpeed * Time.deltaTime); 
        } 
    } 
}