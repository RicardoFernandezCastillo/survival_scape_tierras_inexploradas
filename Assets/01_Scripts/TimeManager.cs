using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static TimeManager instance { get; set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        dayUI.text = $"Day: {dayInGame}";
    }
    public int dayInGame = 1;
    public TextMeshProUGUI dayUI;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TriggerNextDay()
    {
        dayInGame += 1;
        dayUI.text = $"Day: {dayInGame}";
    }
}
