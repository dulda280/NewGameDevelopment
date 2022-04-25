using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using Random2 = UnityEngine.Random;

public class PlayerClass : MonoBehaviour
{
    public static string playerName = "";
    public int hp;
    private float weight;
    private int carryCap;
    public static bool isWalking;
    public static bool isHumanWalk;
    public static bool isSneaky;
    public static bool isRunning;
    public float playerNoise = 15;
    public static float scavengeTime = 2.0f;
    public InventoryScript inventory;
    public float invenWeight;
    public bool newItem = false;
    public float scavengeTimer = 0;
    public bool scavengeTimerBool;
    public float sneakSpeed = 1;
    public float walkSpeed = 5;
    public float runSpeed = 7;
    public bool buttonBool = true;
    public GameObject panel;
    private Rigidbody playerRB;
    public ThirdPersonController controller;
    //public Collider[] noiseColliders = new Collider[10];
    private float hwTimerLeft = 0;
    private float hwTimerRight = 0;
    private float hwTimeCap = 2;
    public float sneakVal = 1;
    public float disguiseVal = 1;
    public float inventoryVal = 1;
    public float speedVal = 1;
    public float badWalkForce = 5000f;
    public bool isGrabbed = false;
    public bool isGrabbedInven = false;
    public Camera cameraMain;
    public bool hpUpdate = false;

    public GameObject QEDisplay;
    public GameObject QTimerUI;
    public GameObject ETimerUI;
    public GameObject walkingErrorDisplay;
    public GameObject walkingErrorSubDisplay;

    public GameObject upgradeInstruct;
    public GameObject upgradePanel;
    public GameObject instructionPanel;
 
    public PlayerClass(string name)
    {
        playerName = name;
        controller = GetComponent<ThirdPersonController>();
        
    }

    public void run()
    {
        isRunning = true;
        playerNoise = 20;
        controller.maxSpeed = runSpeed;
        print("SPRINTING");

    }

    public void humanWalk()
    {

        // Her ska være mechanics for menneske-gang
        // der kan eg. være lyd-straf for at gå dårligt
        if (isHumanWalk)
        {
            if (hwTimerLeft > hwTimeCap)
            {
                print("TOO SLOW, LEFT, TIME: " + hwTimerLeft);
                playerRB.AddForce(Vector3.left* badWalkForce, ForceMode.Impulse);
                int rand = UnityEngine.Random.Range(1, 3);
                controller.maxSpeed = rand;
                walkingErrorDisplay.SetActive(true);
                walkingErrorSubDisplay.SetActive(true);
                playerNoise = 30;
                hwTimerLeft = 0;
            }
            
            hwTimerLeft += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                hwTimerLeft = 0;
                controller.maxSpeed = walkSpeed;
                playerNoise = 0;
                walkingErrorDisplay.SetActive(false);
                walkingErrorSubDisplay.SetActive(false);
            }

            if (hwTimerRight > hwTimeCap + 1)
            {
                print("TOO SLOW, RIGHT, TIME: " + hwTimerRight);
                playerRB.AddForce(Vector3.right * badWalkForce, ForceMode.Impulse);
                int rand = UnityEngine.Random.Range(10, 20);
                controller.maxSpeed = rand;
                walkingErrorDisplay.SetActive(true);
                walkingErrorSubDisplay.SetActive(true);
                playerNoise = 30;
                hwTimerRight = 0;
            }
            
            hwTimerRight += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.E))
            {
                hwTimerRight = 0;
                controller.maxSpeed = walkSpeed;
                playerNoise = 0;
                walkingErrorDisplay.SetActive(false);
                walkingErrorSubDisplay.SetActive(false);
            }
            
        }

    }

    public void sneak()
    {
        controller.maxSpeed = sneakSpeed;
        isSneaky = true;
        playerNoise = 3;
        print("SNEAKING");
        
    }

    public void normalState()
    {
        controller.maxSpeed = walkSpeed;
        playerNoise = 15;
    }
    
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ScavengeO"))
        {
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                print("other" + other);
                scavengeTimerBool = true;
                print("Scavenging");
                FindObjectOfType<AudioManager>().Play("Scavenge" + Random2.Range(1, 4));
            }
            if (scavengeTimerBool)
            {
                scavengeTimer += Time.deltaTime;
                print("timer: " + scavengeTimer);
                if (scavengeTimer >= scavengeTime)
                {
                    //Returner en item genereret item class
                    print("SCAVENGING");
                    //generate new item og append til inventory
                    print("other" + other.name);
                    var itemmm = other.GetComponent<ScavengableObject>();
                    print(itemmm);
                    if (itemmm)
                    {
                        
                        inventory.AddItem(itemmm.item1, 1);
                        print("Successfully scavenged");
                        print(inventory);
                        inventory.Save();
                        getUpgradeVals();
                        print("disguiseval: " + disguiseVal);

                    }
                    
                    newItem = true;
                    scavengeTimer = 0;
                    scavengeTimerBool = false;
                    
                }
            }  
            
        }

    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ScavengeO"))
        {
            if (scavengeTimerBool)
            {
                print("Scavenging interrupted");
            }
           
            scavengeTimerBool = false;
            scavengeTimer = 0;
        } 
    }

    // gets weight from item
    public void updateWeight()
    {
        invenWeight = 1;
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            invenWeight = invenWeight + inventory.Container[i].getWeight();
        }
    }

    public void getUpgradeVals()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            sneakVal += inventory.Container[i].getsneakVal();
            disguiseVal += inventory.Container[i].getdisguiseVal();
            inventoryVal += inventory.Container[i].getinvenVal();
            speedVal += inventory.Container[i].getsneakVal();
        }
    }

    void checkNoiseSphere()
    {
        //int noiseSphere = Physics.OverlapSphereNonAlloc(transform.position, playerNoise, noiseColliders);
        Collider[] noiseSphere = Physics.OverlapSphere(transform.position, playerNoise);
        if (noiseSphere.Length > 0)
        {
            
            for (int i = 0; i < noiseSphere.Length; i++)
            {
                float dist = Vector3.Distance(noiseSphere[i].transform.position, transform.position);
                
                if (dist <= playerNoise && noiseSphere[i].CompareTag("NPC"))
                {
                    print("message sent");
                    noiseSphere[i].SendMessage("IHeardSomething");
                }
            }
            
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, playerNoise);
        
    }

    public void openIven()
    {
        panel.SetActive(true);
        
    }
    public void closeInven()
    {
        panel.SetActive(false);
        
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

    private void whenGrabbed()
    {
        FindObjectOfType<AudioManager>().Play("Grabbed");
        if (inventory.Container.Count > 0)
        {
            inventory.Container.RemoveAt(inventory.Container.Count - 1);
            print("checker 123");
            print("inventorycount: " + inventory.Container.Count);
        }
        else
        {
            hp -= 4;
            hpUpdate = true;
        }
    }



    public void Start()
    {
        panel.SetActive(false);
        walkSpeed = 5;
        sneakSpeed = 1;
        runSpeed = 7;
        hwTimeCap = 2;
        playerRB = this.GetComponent<Rigidbody>();
        print("RigidBody: " + playerRB);
        hp = 4;
        cameraMain.enabled = true;
    }

    public void Update()
    {
        
        
        // Check states
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isSneaky)
            {
                isSneaky = false;
                normalState();
            }
            else if (!isSneaky)
            {
                sneak();
            }
        }
        if (newItem)
        {
            updateWeight();
            
            controller.maxSpeed /= invenWeight;
            
            newItem = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isRunning)
            {
                isRunning = false;
                normalState();
            }
            else if (!isRunning)
            {
                run();
            }
            
        }

        if (Input.GetKeyDown("B"))
        {
            //upgrade panel intro
            
        }
        if (Input.GetKeyDown("O"))
        {
            //instructions
            
        }
        
        if (Input.GetKeyDown("U"))
        {
            //uprade panel
            
        }
        
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isHumanWalk)
            {
                isHumanWalk = false;
                hwTimerLeft = 0;
                hwTimerRight = 0;
                normalState();
                QEDisplay.SetActive(false);
                QTimerUI.SetActive(false);
                ETimerUI.SetActive(false);
                walkingErrorDisplay.SetActive(false);
                walkingErrorSubDisplay.SetActive(false);
                
            }
            else if (!isHumanWalk)
            {
                isHumanWalk = true;
                humanWalk();
                QEDisplay.SetActive(true);
                QTimerUI.SetActive(true);
                ETimerUI.SetActive(true);
                FindObjectOfType<AudioManager>().Play("HumanWalk");
            }
            
        }

        if (isHumanWalk)
        { 
            QTimerUI.GetComponent<Text>().text = "" + hwTimerLeft;
            ETimerUI.GetComponent<Text>().text = "" + hwTimerRight;
            humanWalk();
        }
        
        
        if (Input.GetKeyDown("i") && buttonBool == true)
        {
            openIven();
            FindObjectOfType<AudioManager>().Play("Inventory");
            buttonBool = false;
        }
        else if (Input.GetKeyDown("i") && buttonBool == false)
        {
            closeInven();
            FindObjectOfType<AudioManager>().Play("InventoryClose");
            buttonBool = true;
        }

        checkNoiseSphere();

        if (isGrabbed == true)
        {
            whenGrabbed();
            print("current hp: " + hp);
            isGrabbed = false;
        }

        if (hp <= 0)
        {
            cameraMain.enabled = false;
        }
       

    }
}
