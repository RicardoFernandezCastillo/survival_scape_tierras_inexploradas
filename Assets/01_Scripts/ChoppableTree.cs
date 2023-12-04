using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChoppableTree : MonoBehaviour
{
    public bool playerInRange;
    public bool canBeChopped;

    public float treeMaxHealth;
    public float treeHealth;

    public Animator animator;

    public float caloriesSpentChoppingWood = 20;

    private void Start()
    {
        treeHealth = treeMaxHealth;
        animator = transform.parent.transform.parent.transform.parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


    public IEnumerator hit()
    {
        yield return new WaitForSeconds(0.4f);
        animator.SetTrigger("shake");
        treeHealth -= 1;

        playerState.Instance.currentCalories -= caloriesSpentChoppingWood;

        if(treeHealth <= 1)
        {
            Debug.Log("hola");
            TreeIsDead();
        }
    }

    void TreeIsDead()
    {
        Vector3 treePositopn = transform.position;
        Destroy(transform.parent.transform.parent.transform.parent.gameObject);
        canBeChopped = false;
        SelectionManager.Instance.selectedTree = null;
        SelectionManager.Instance.chopHolder.gameObject.SetActive(false);

        GameObject brokenTree = Instantiate(Resources.Load<GameObject>("Trunk"),new Vector3(treePositopn.x,treePositopn.y,treePositopn.z),Quaternion.Euler(0,0,0));
    }

    public void GetHit()
    {
        StartCoroutine(hit());
    }

    private void Update()
    {
        if (canBeChopped)
        {
            GlobalState.Instance.resourcesHealth = treeHealth;
            GlobalState.Instance.resourcesMaxHealth = treeMaxHealth;
        }
    }
}
