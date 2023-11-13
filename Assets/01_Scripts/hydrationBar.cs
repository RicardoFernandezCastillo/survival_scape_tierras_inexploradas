using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class hydrationBar : MonoBehaviour
{
    private Slider slider; //Is not necesary 'cause GetComponent<Slider>
    public TextMeshProUGUI hydrationCounter;

    public GameObject playerState;

    float maxHydration, currentHydration;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHydration = playerState.GetComponent<playerState>().currentHydrationPercent;
        maxHydration = playerState.GetComponent<playerState>().maxHydrationPercent;
        float fillValue = currentHydration / maxHydration;
        slider.value = fillValue;

        hydrationCounter.text = currentHydration + "%";
    }
}
