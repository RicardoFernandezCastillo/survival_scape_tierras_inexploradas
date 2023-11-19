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
    Button carftBtn;

    TextMeshProUGUI AxeReq1, AxeReq2;

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
        isOpen = false;
        toolsBtn = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenToolsCategory();});
    }

    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }
}
