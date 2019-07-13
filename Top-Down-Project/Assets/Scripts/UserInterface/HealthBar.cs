using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthBar : MonoBehaviour
{


    [Header("Health Bar settings")]
    [SerializeField] TextMeshProUGUI healthTMP = default;
    [SerializeField] Slider healthSlider;    
    [SerializeField] string textFormat;
    

    [Header("Target Settings")]
    [SerializeField] bool trackTarget;
    [SerializeField] bool destroyWithTarget;
    [SerializeField] Vector3 placementOffset;
    [SerializeField] ObjectHealth target;
    [SerializeField] Camera uiCamera;

    public bool IsTracking { get { return trackTarget; } set { trackTarget = value; } }

    // Start is called before the first frame update
    void Start()
    {
        //defaults
        if (healthSlider == null)
        {
            healthSlider = GetComponent<Slider>();
        }

        if (string.IsNullOrEmpty(textFormat))
        {
            textFormat = "{0}/{1}: {2}%";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //set text of the health bar
        if (!string.IsNullOrEmpty(textFormat))
        {
            healthTMP.text = string.Format(textFormat, target.Health, target.MaxHealth, target.HealthPercent().ToString());
        }
        

        //show the health on the slider fill image
        healthSlider.value = target.HealthPercent() / 100;
        
    }

    #region Helper Methods
    public void SetTarget(ObjectHealth health)
    {
        target = health;
    }

    public void SetCamera(Camera cam)
    {        
        uiCamera = cam;
    }
    #endregion
}
