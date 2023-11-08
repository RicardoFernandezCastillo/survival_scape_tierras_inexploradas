using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerState : MonoBehaviour
{
    // Start is called before the first frame update
    public static playerState Instance { get; set; }



    //Player Health
    public float currentHealth;
    public float maxHealth;



    //Player Calories
    public float currentCalories;
    public float maxCalories;



    //Player Hydration
    public float currentHydrationPercent;
    public float maxHydrationPercent;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void Start()
    {
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;
    }
    // Update is called once per frame
    void Update()
    {
        //Just For Testing
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 10;
        }
    }
}
