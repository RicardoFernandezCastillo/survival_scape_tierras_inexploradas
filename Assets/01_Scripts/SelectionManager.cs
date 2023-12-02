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
                }
                else
                {
                    centerDontImage.gameObject.SetActive(true);
                    handIcon.gameObject.SetActive(false);
                }
            }
            else
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
                centerDontImage.gameObject.SetActive(true);
                handIcon.gameObject.SetActive(false);
            }

        }
        else
        {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
            centerDontImage.gameObject.SetActive(true);
            handIcon.gameObject.SetActive(false);
        }
    }
}
