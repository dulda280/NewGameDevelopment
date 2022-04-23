using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject player;
    public Image fillImage;
    private Slider slider;
    public float fillValue;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value <= 0)
        {
            fillImage.enabled = false;

        }

        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }

        if (player.GetComponent<PlayerClass>().hpUpdate == true)
        {
            slider.value = player.GetComponent<PlayerClass>().hp;
            player.GetComponent<PlayerClass>().hpUpdate = false;
        }

    }
}
