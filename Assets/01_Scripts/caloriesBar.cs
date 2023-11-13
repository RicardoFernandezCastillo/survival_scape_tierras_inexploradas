using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class caloriesBar : MonoBehaviour
{
    private Slider slider; //Is not necesary 'cause GetComponent<Slider>
    public TextMeshProUGUI caloriesCounter;

    public GameObject playerState;

    float maxCalories, currentCalories;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCalories = playerState.GetComponent<playerState>().currentCalories;
        maxCalories = playerState.GetComponent<playerState>().maxCalories;
        float fillValue = currentCalories / maxCalories;
        slider.value = fillValue;

        caloriesCounter.text = currentCalories + "/" + maxCalories;
    }
}
