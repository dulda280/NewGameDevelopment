using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Upgrades : MonoBehaviour
{
    public GameObject craftingBench;

    public PlayerClass playerClass;
    public InventoryScript inventoryClass;
    public FieldOfView fieldOfViewClass;
    public AIBehaviour AIBehaviourClass;

    public GameObject upgradeMenu;  // Panel
    public GameObject message;      // Panel

    private bool canCraft;      // Is the player close enough to the bench? 
    [Range(0, 360)]
    public float angle = 180f;  // View angle
    public float radius = 5f;   // Distance from player to bench
    public float craftingTime;  // How long it takes to craft/upgrade

    public LayerMask craftingBenchMask;
    public LayerMask obstructionMask;

    // The cost of upgrading increases every time you upgrade
    private int upgrInventoryCost = 2;
    private int upgrSneakCost = 2;
    private int upgrSpeedCost = 2;
    private int upgrDisguiseCost = 2;

    // Win condition
    private int disguiseLevel;

    // Start is called before the first frame update
    void Start()
    {
        craftingBench = GameObject.FindGameObjectWithTag("Crafting bench");

        // playerClass = GetComponent<PlayerClass>();
        // inventoryClass = GetComponent<InventoryScript>();
        // fieldOfViewClass = GetComponent<FieldOfView>();
        // AIBehaviourClass = GetComponent<AIBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckDistance())
        {
            MakeUpgrade();
        }
    }

    bool CheckDistance()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, craftingBenchMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canCraft = true;
                    ShowMessage();
                }
                else
                    canCraft = false;
            }
            else
                canCraft = false;
        }
        else if (canCraft)
            canCraft = false;

        return canCraft;
    }

    void ShowMessage()
    {
        // Debug.Log("You can upgrade by pressing 1, 2, 3 or 4");
        if (message != null)
        {
            message.SetActive(true);
        }
    }

    void MakeUpgrade()
    {
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key was pressed");
            if(upgradeMenu != null)
            {
                upgradeMenu.SetActive(true);
            }
        }
        */

        Debug.Log("You should press the E button.");

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("The button E was pressed");
            UpgradeInventory();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpgradeSneak();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpgradeDisguise();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpgradeSpeed();
        }
    }


    void UpgradeInventory()
    {

        if (playerClass.inventoryVal >= upgrInventoryCost)
        {
            inventoryClass.inventorySpace -= upgrInventoryCost; // Cost of upgrade
            upgrInventoryCost += 1; // It gets more expensive to upgrade this next time

            inventoryClass.maxInven += 1;   // increases max inventory space
            playerClass.playerNoise += 2;   // increases noise

            // a big inventory slows you down when running, but running will always be faster than walking
            if (playerClass.runSpeed >= playerClass.walkSpeed + 3)
            {
                playerClass.runSpeed -= 1;
            }

            // man bliver nemmere opdaget, når man har større inventory
            fieldOfViewClass.radius += 1;
            if (AIBehaviourClass._suspicionTimer >= 2.5)
            {
                AIBehaviourClass._suspicionTimer -= 0.5f;
            }
            Debug.Log("Upgraded inventory");
        }
        else
        {
            Debug.Log("You don't have enough items to upgrade your inventory");
        }
    }

    void UpgradeSneak()
    {

        if (playerClass.sneakVal >= upgrSneakCost)
        {
            playerClass.sneakVal -= upgrSneakCost; // Cost of upgrade
            upgrSneakCost += 1; // It gets more expensive to upgrade this next time

            // Can move faster and more silent while sneaking
            playerClass.sneakSpeed += 1;

            if (playerClass.playerNoise >= 3)
            {
                playerClass.playerNoise -= 3;   // decreases noise
            }
            Debug.Log("Upgraded sneak");
        }
        else
        {
            Debug.Log("You don't have enough items to upgrade sneak");
        }
    }

    void UpgradeDisguise()
    {

        // playerClass.speedVal = 0;

        if (playerClass.disguiseVal >= upgrDisguiseCost)
        {
            playerClass.disguiseVal -= upgrDisguiseCost; // Cost of upgrade
            upgrDisguiseCost += 1; // It gets more expensive to upgrade this next time

            // Man skal tættere på AIs for at blive opdaget og man skal være i deres FoV i længere tid.
            if (fieldOfViewClass.radius >= 3)
            {
                fieldOfViewClass.radius -= 3;
            }
            else
            {
                fieldOfViewClass.radius = 0;
                Debug.Log("You won the game!"); // Man vinder, når AIs ikke længere ser spilleren
            }

            AIBehaviourClass._suspicionTimer += 1.0f;

            Debug.Log("Upgraded disguise");
        }
        else
        {
            Debug.Log("You don't have enough items to upgrade disguise");
        }
    }

    void UpgradeSpeed()
    {
        if (playerClass.speedVal >= upgrSpeedCost)
        {
            playerClass.speedVal -= upgrSpeedCost; // Cost of upgrade
            upgrSpeedCost += 1; // It gets more expensive to upgrade this next time

            playerClass.walkSpeed += 1;
            playerClass.runSpeed += 2;

            Debug.Log("Upgraded speed");
        }
        else
        {
            Debug.Log("You don't have enough items to upgrade speed");
        }
    }
}
    
