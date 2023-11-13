using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    private Slider slider; //Is not necesary 'cause GetComponent<Slider>
    public TextMeshProUGUI healthCounter;

    public GameObject playerState;

    private float maxHealth, currentHealth;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerState.GetComponent<playerState>().currentHealth;
        maxHealth = playerState.GetComponent<playerState>().maxHealth;
        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;

        healthCounter.text = currentHealth + "/"+ maxHealth;
    }
}
