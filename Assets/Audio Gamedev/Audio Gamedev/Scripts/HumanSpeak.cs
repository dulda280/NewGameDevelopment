using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;
using UnityEngine.Audio;



public class HumanSpeak : MonoBehaviour
{

    public Sound[] sounds;

    private AudioSource source;

    private float randomNumber;


    private void Start()
    {
        StartCoroutine("DoCheck");
    }

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialize = true;
            s.source.spatialBlend = 1;
        }
    }
    
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
            
        s.source.Play();
    }

    IEnumerator DoCheck() {
        for(;;) {
            Play("Speak" + Random.Range(1, 5));
            yield return new WaitForSeconds(Random.Range(6f, 11f));
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    
   
}
