using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // --- Is this item trashable --- //
    public bool isTrashable;

    // --- Item Info UI --- //
    private GameObject itemInfoUI;

    private Text itemInfoUI_itemName;
    private Text itemInfoUI_itemDescription;
    private Text itemInfoUI_itemFunctionality;

    public string thisName, thisDescription, thisFunctionality;

    // --- Consumption --- //
    private GameObject itemPendingConsumption;
    public bool isConsumable;

    public float healthEffect;
    public float caloriesEffect;
    public float hydrationEffect;


    public bool isEquippable;
    private GameObject itemPadingEquipping;
    public bool isInsideQuickSlot;

    public bool isSelected;


    private void Start()
    {
        itemInfoUI = InventorySystem.Instance.ItemInfoUI;
        itemInfoUI_itemName = itemInfoUI.transform.Find("Name").GetComponent<Text>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("Description").GetComponent<Text>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("Functionality").GetComponent<Text>();
    }

    void Update()
    {
        if (isSelected)
        {
            gameObject.GetComponent<DragDrop>().enabled = false;
        }
        else
        {
            gameObject.GetComponent <DragDrop>().enabled = true;
        }
    }

    // Triggered when the mouse enters into the area of the item that has this script.
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
        itemInfoUI_itemFunctionality.text = thisFunctionality;
    }

    // Triggered when the mouse exits the area of the item that has this script.
    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoUI.SetActive(false);
    }

    // Triggered when the mouse is clicked over the item that has this script.
    public void OnPointerDown(PointerEventData eventData)
    {
        //Right Mouse Button Click on
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable)
            {
                // Setting this specific gameobject to be the item we want to destroy later
                itemPendingConsumption = gameObject;
                consumingFunction(healthEffect, caloriesEffect, hydrationEffect);
            }

            if (isEquippable && isInsideQuickSlot == false && EquipSystem.Instance.CheckIfFull() == false)
            {
                EquipSystem.Instance.AddToQuickSlots(gameObject);
                isInsideQuickSlot = true;
            }
        }

        
    }

    // Triggered when the mouse button is released over the item that has this script.
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable && itemPendingConsumption == gameObject)
            {
                DestroyImmediate(gameObject);
                InventorySystem.Instance.ReCaculateList();
                CraftingSystem.instance.RefreshNeededItems();
            }
        }
    }

    private void consumingFunction(float healthEffect, float caloriesEffect, float hydrationEffect)
    {
        itemInfoUI.SetActive(false);

        healthEffectCalculation(healthEffect);

        caloriesEffectCalculation(caloriesEffect);

        hydrationEffectCalculation(hydrationEffect);

    }


    private static void healthEffectCalculation(float healthEffect)
    {
        // --- Health --- //

        float healthBeforeConsumption = playerState.Instance.currentHealth;
        float maxHealth = playerState.Instance.maxHealth;

        if (healthEffect != 0)
        {
            if ((healthBeforeConsumption + healthEffect) > maxHealth)
            {
                playerState.Instance.setHealth(maxHealth);
            }
            else
            {
                playerState.Instance.setHealth(healthBeforeConsumption + healthEffect);
            }
        }
    }


    private static void caloriesEffectCalculation(float caloriesEffect)
    {
        // --- Calories --- //

        float caloriesBeforeConsumption = playerState.Instance.currentCalories;
        float maxCalories = playerState.Instance.maxCalories;

        if (caloriesEffect != 0)
        {
            if ((caloriesBeforeConsumption + caloriesEffect) > maxCalories)
            {
                playerState.Instance.setCalories(maxCalories);
            }
            else
            {
                playerState.Instance.setCalories(caloriesBeforeConsumption + caloriesEffect);
            }
        }
    }


    private static void hydrationEffectCalculation(float hydrationEffect)
    {
        // --- Hydration --- //

        float hydrationBeforeConsumption = playerState.Instance.currentHydrationPercent;
        float maxHydration = playerState.Instance.maxHydrationPercent;

        if (hydrationEffect != 0)
        {
            if ((hydrationBeforeConsumption + hydrationEffect) > maxHydration)
            {
                playerState.Instance.setHydration(maxHydration);
            }
            else
            {
                playerState.Instance.setHydration(hydrationBeforeConsumption + hydrationEffect);
            }
        }
    }


}
