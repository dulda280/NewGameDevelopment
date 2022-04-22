using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempSound : MonoBehaviour
{
    private float noiseRadius = 10;
    private Collider[] noiseColliders = new Collider[5];
    
    void checkNoiseSphere()
    {
        int noiseSphere = Physics.OverlapSphereNonAlloc(transform.position, noiseRadius, noiseColliders);
        
        if (noiseSphere > 0)
        {
            
            for (int i = 0; i < noiseSphere; i++)
            {
                float dist = Vector3.Distance(noiseColliders[i].transform.position, transform.position);
                
                if (dist <= noiseRadius && noiseColliders[i].CompareTag("NPC"))
                {
                    noiseColliders[i].SendMessage("IHeardSomething");
                }
            }
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        checkNoiseSphere();
    }
}
