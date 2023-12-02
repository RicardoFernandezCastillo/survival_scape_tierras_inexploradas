using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{

    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;

    public List<string> inventoryItemList = new List<string>();

    public static CraftingSystem instance { get; set; }

    Button toolsBtn;
    Button craftBtn;


    TextMeshProUGUI AxeReq1, AxeReq2;
    TextMeshProUGUI SwordReq1, SwordReq2;

    public bool isOpen;

    

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Blueprint AxeBlp = new Blueprint("Axe", 2, "Stone", 3, "Stick", 2);
        Blueprint SwordBlp = new Blueprint("Sword", 2, "Stone", 2, "Stick", 1);

        isOpen = false;
        toolsBtn = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenToolsCategory();});

        //Axe

        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftBtn = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftBtn.onClick.AddListener(delegate { CraftAnyItem(AxeBlp); });

        SwordReq1 = toolsScreenUI.transform.Find("Sword").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        SwordReq2 = toolsScreenUI.transform.Find("Sword").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftBtn = toolsScreenUI.transform.Find("Sword").transform.Find("Button").GetComponent<Button>();
        craftBtn.onClick.AddListener(delegate { CraftAnyItem(SwordBlp); });

    }

    public void CraftAnyItem(Blueprint blueprintToCraft)
    {

        Debug.Log("Crafteo");

        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);
        if(blueprintToCraft.numOfRequirements == 1)
        {
           InventorySystem.Instance.RemoveItem(blueprintToCraft.req1, blueprintToCraft.req1Amount);
        }
        else if(blueprintToCraft.numOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.req1, blueprintToCraft.req1Amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.req2, blueprintToCraft.req2Amount);
        }


        StartCoroutine(calculate());
        RefreshNeededItems();
    }

    public IEnumerator calculate()
    {
        yield return new WaitForSeconds(1f);
        InventorySystem.Instance.ReCaculateList();
    }

    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        RefreshNeededItems();

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isOpen = false;
        }
    }

    public void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string item in inventoryItemList)
        {
            switch (item)
            {
                case "Stone":
                    stone_count += 1;
                    break;
                case "Stick":
                    stick_count += 1;
                    break;
            }
        }

        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "2 Stick [" + stick_count + "]";

        SwordReq1.text = "2 Stone [" + stone_count + "]";
        SwordReq2.text = "1 Stick [" + stick_count + "]";

        if (stone_count >= 3 && stick_count >= 3)
        {
            craftBtn.gameObject.SetActive(true);
        }
        else
        {
            craftBtn.gameObject.SetActive(false);
        }
            
    }
}
