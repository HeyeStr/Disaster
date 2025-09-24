using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TOTALResourceDisplay : MonoBehaviour
{
    public GameObject LivingText;
    public GameObject FoodText;
    public GameObject MedicineText;
    public GameObject LivingIcon;
    public GameObject FoodIcon;
    public GameObject MedicineIcon;
    public GameObject MonitorObjeect;

    void Start()
    {
        Debug.Log("开始寻找");
        LivingText = transform.Find("LivingText").gameObject;
        FoodText = transform.Find("FoodText").gameObject;
        MedicineText = transform.Find("MedicineText").gameObject;
        MonitorObjeect = GameObject.FindGameObjectWithTag("Monitor");
        FoodIcon= transform.Find("FoodIcon").gameObject;
        MedicineIcon = transform.Find("MedicineIcon").gameObject;
        LivingIcon= transform.Find("LivingIcon").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ResourcesManager resourcesManager= MonitorObjeect.GetComponent<ResourcesManager>();
        if(LivingIcon.GetComponent<LivingIconMono>().Living!= resourcesManager.LivingResource)
        {
            LivingIcon.GetComponent<LivingIconMono>().Living = resourcesManager.LivingResource;
            LivingText.GetComponent<TextMeshProUGUI>().text = LivingIcon.GetComponent<LivingIconMono>().Living.ToString();
        }
        if (MedicineIcon.GetComponent<MedicineIconMono>().Medicine != resourcesManager.MedicalResource)
        {
            MedicineIcon.GetComponent<MedicineIconMono>().Medicine = resourcesManager.MedicalResource;
            MedicineText.GetComponent<TextMeshProUGUI> ().text= MedicineIcon.GetComponent<MedicineIconMono>().Medicine.ToString();
        }
        if(FoodIcon.GetComponent<FoodIconMono>().Food != resourcesManager.FoodResource)
        {
            FoodIcon.GetComponent<FoodIconMono>().Food = resourcesManager.FoodResource;
            FoodText.GetComponent<TextMeshProUGUI>().text = FoodIcon.GetComponent<FoodIconMono>().Food.ToString();
        }

    }
}
