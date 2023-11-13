using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public Light direcctionalLight;
    public float dayDurationInSeconds = 24.0f;

    public int currentHour;
    float currentTimeOfDay = 0.0f;

    public List<SkyboxTimeMapping> timeMapping;

    float blendedValue = 0.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeOfDay += Time.deltaTime / dayDurationInSeconds;
        currentTimeOfDay %= 1;

        currentHour = Mathf.FloorToInt(currentTimeOfDay * 24);

        direcctionalLight.transform.rotation = Quaternion.Euler(new Vector3((currentTimeOfDay*360)-90,170,0));

        UpdateSkybox();
    }

    private void UpdateSkybox()
    {
        Material currentSkybox = null;
        foreach (var mapping in timeMapping)
        {
            if (currentHour == mapping.hour)
            {
                currentSkybox = mapping.skyboxMaterial;

                if (currentSkybox.shader.name == "Custom/SkyboxTransition")
                {
                    blendedValue += Time.deltaTime;
                    blendedValue = Mathf.Clamp01(blendedValue);

                    currentSkybox.SetFloat("_TransitionFactor", blendedValue);
                }
                else
                {
                    blendedValue = 0;
                }



                break;
            }

        }
        if (currentSkybox != null)
        {
            RenderSettings.skybox = currentSkybox;
        }
    }
}

[System.Serializable]
public class SkyboxTimeMapping
{
    public string phaseName;
    public int hour; //hora 0 - 23
    public Material skyboxMaterial; //Material correspondiente a la hora
}
