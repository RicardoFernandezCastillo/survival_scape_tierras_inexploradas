using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{

    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI,constructionUI;

    public List<string> inventoryItemList = new List<string>();

    public static CraftingSystem instance { get; set; }

    Button toolsBtn, constructionBtn;
    Button craftAxeBtn, craftSwordBtn, craftWallBtn, craftFoundationBtn;

    TextMeshProUGUI AxeReq1, AxeReq2 , WallReq , FoundationReq;

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
        Blueprint WallBlp = new Blueprint("Wall", 1, "Log", 2,"",0);
        Blueprint FoundationBlp = new Blueprint("Foundation", 1, "Log", 3, "", 0);

        isOpen = false;
        toolsBtn = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenToolsCategory();});

        toolsBtn = craftingScreenUI.transform.Find("ConstructionButton").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenConstructionCategory(); });


        //Axe

        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        WallReq = constructionUI.transform.Find("Wall").transform.Find("req1").GetComponent<TextMeshProUGUI>();

        FoundationReq = constructionUI.transform.Find("Foundation").transform.Find("req1").GetComponent<TextMeshProUGUI>();


        craftWallBtn = constructionUI.transform.Find("Wall").transform.Find("Button").GetComponent<Button>();
        craftWallBtn.onClick.AddListener(delegate { CraftAnyItem(WallBlp); });

        craftFoundationBtn = constructionUI.transform.Find("Foundation").transform.Find("Button").GetComponent<Button>();
        craftFoundationBtn.onClick.AddListener(delegate { CraftAnyItem(FoundationBlp); });

        craftAxeBtn = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBtn.onClick.AddListener(delegate { CraftAnyItem(AxeBlp); });

        SwordReq1 = toolsScreenUI.transform.Find("Sword").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        SwordReq2 = toolsScreenUI.transform.Find("Sword").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftSwordBtn = toolsScreenUI.transform.Find("Sword").transform.Find("Button").GetComponent<Button>();
        craftSwordBtn.onClick.AddListener(delegate { CraftAnyItem(SwordBlp); });




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

    void OpenConstructionCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);
        constructionUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        RefreshNeededItems();

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;

            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                SelectionManager.Instance.EnableSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
            }
            isOpen = false;
        }
    }

    public void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;
        int log_count = 0;

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
                case "Log":
                    log_count += 1;
                    break;
            }
        }

        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "2 Stick [" + stick_count + "]";

        //SwordReq1.text = "2 Stone [" + stone_count + "]";
        //SwordReq2.text = "1 Stick [" + stick_count + "]";

        WallReq.text = "2 Log[" + log_count + "]";
        FoundationReq.text = "3 Log[" + log_count + "]";


        if(log_count >= 2)
        {
            craftWallBtn.gameObject.SetActive(true);
        }
        else
        {
            craftWallBtn.gameObject.SetActive(false);
        }

        if (log_count >= 3)
        {
            craftFoundationBtn.gameObject.SetActive(true);
        }
        else
        {
            craftFoundationBtn.gameObject.SetActive(false);
        }



        if (stone_count >= 3 && stick_count >= 3)
        {
            craftAxeBtn.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBtn.gameObject.SetActive(false);
        }
            
    }
}
