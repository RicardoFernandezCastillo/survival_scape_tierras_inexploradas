using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; set; }

    public bool onTarget;

    public GameObject selectedObject;

    public GameObject interaction_Info_UI;
    TextMeshProUGUI interaction_text;

    public Image centerDontImage;
    public Image handIcon;

    public bool handIsVisible;

    public GameObject selectedTree;
    public GameObject chopHolder;


    private void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
    }

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

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InterectableObject interectable = selectionTransform.GetComponent<InterectableObject>();

            ChoppableTree choppableTree = selectionTransform.GetComponent<ChoppableTree>();

            if (choppableTree && choppableTree.playerInRange)
            {
                choppableTree.canBeChopped = true;
                selectedTree = choppableTree.gameObject;
                chopHolder.gameObject.SetActive(true);
            }
            else
            {
                if(selectedTree != null)
                {
                    selectedTree.gameObject.GetComponent<ChoppableTree>().canBeChopped = false;
                    selectedTree = null;
                    chopHolder.gameObject.SetActive(false);
                }
            }

            if (interectable && interectable.playerRange)
            {

                onTarget = true;
                selectedObject = interectable.gameObject;
                interaction_text.text = interectable.GetItemName();
                interaction_Info_UI.SetActive(true);

                if (interectable.CompareTag("pickable"))
                {
                    centerDontImage.gameObject.SetActive(false);
                    handIcon.gameObject.SetActive(true);

                    handIsVisible = true;
                }
                else
                {
                    centerDontImage.gameObject.SetActive(true);
                    handIcon.gameObject.SetActive(false);

                    handIsVisible = false;
                }
            }
            else
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
                centerDontImage.gameObject.SetActive(true);
                handIcon.gameObject.SetActive(false);

                handIsVisible = false;
            }

        }
        else
        {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
            centerDontImage.gameObject.SetActive(true);
            handIcon.gameObject.SetActive(false);

            handIsVisible = false;
        }
    }

    public void DisableSelection()
    {
        handIcon.enabled = false;
        centerDontImage.enabled = false;
        interaction_Info_UI.SetActive(false);

        selectedObject = null;
    }
    public void EnableSelection()
    {
        handIcon.enabled = true;
        centerDontImage.enabled = true;
        interaction_Info_UI.SetActive(true);
    }
}
