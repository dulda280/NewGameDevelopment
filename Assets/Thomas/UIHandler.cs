using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIHandler : MonoBehaviour
{

    public GameObject upgrade_menu;
    public GameObject upgrade_info_text;
    
    private bool isUpgradeMenuOpen = false;
    private bool isUpgradeInfoText = false;
    // Start is called before the first frame update
    // void Start()
    // {
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     if (ThirdPersonController.playerActionsAsset.Player.baslls.triggered)
    //     {
    //         if (!isUpgradeMenuOpen)
    //         {
    //             openUpgradeMenu();
    //         }
    //         else
    //         {
    //             closeUpgradeMenu();
    //         }
    //     }
    // }
    //
    // public void openUpgradeMenu()
    // {
    //     upgrade_menu.SetActive(true);
    //     isUpgradeMenuOpen = true;
    // }
    //
    // public void closeUpgradeMenu()
    // {
    //     upgrade_menu.SetActive(false);
    //     isUpgradeMenuOpen = false;
    // }
    //
    // public void showUpgradeInfoText()
    // {
    //     upgrade_info_text.SetActive(true);
    // }
    //
    // public void hideUpgradeInfoText()
    // {
    //     upgrade_info_text.SetActive(false);
    // }
}