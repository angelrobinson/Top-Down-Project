using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthBar : MonoBehaviour
{


    [Header("Health Bar settings")]
    [SerializeField] TextMeshProUGUI healthTMP;
    [SerializeField] Slider healthSlider;
    [SerializeField] Text healthText;
    [SerializeField] string textFormat;
    [SerializeField] Image fillImage;

    [Header("Target Settings")]
    [SerializeField] bool trackTarget;
    [SerializeField] bool destroyWithTarget;
    [SerializeField] Vector3 placementOffset;
    [SerializeField] Transform placementTransform;
    [SerializeField] ObjectHealth target;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = string.Format(textFormat, target.Health, target.MaxHealth, target.HealthPercent().ToString());        
        fillImage.fillAmount = target.HealthPercent() / 100;

        healthTMP.text = string.Format(textFormat, target.Health, target.MaxHealth, target.HealthPercent().ToString());
        healthSlider.value = target.HealthPercent() / 100;
    }

    #region Helper Methods
    public void SetTarget(ObjectHealth health)
    {
        target = health;
    }
    #endregion
}
