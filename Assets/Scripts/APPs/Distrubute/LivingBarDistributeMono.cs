using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LivingBarDistributeMono : MonoBehaviour
{
    public GameObject MonitorObject;
    public int LivingDistributeQuantity;
    public int NewLivingQuantity;
    public int MissionIndex;
    ResourcesManager SourceManager;
    private GameObject Text;
    private GameObject Slider;
    public GameObject DistributeBar;
    void Start()
    {
        LivingDistributeQuantity = 0;
        MonitorObject = GameObject.FindGameObjectWithTag("Monitor");
        SourceManager = MonitorObject.GetComponent<ResourcesManager>();
        Text = transform.Find("Text").gameObject;
        Text.GetComponent<TextMeshProUGUI>().text = "0";
        Slider = transform.Find("SliderLiving").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        NewLivingQuantity = (int)Slider.GetComponent<Slider>().value;
        if (NewLivingQuantity != LivingDistributeQuantity)
        {
            
            int TotalLivingResource = SourceManager.LivingResource;
            if (NewLivingQuantity > LivingDistributeQuantity)
            {

                Debug.Log("NewLivingQuantity > LivingDistributeQuantity");
                if (TotalLivingResource - NewLivingQuantity + LivingDistributeQuantity < 0)
                {
                    Slider.GetComponent<Slider>().value = LivingDistributeQuantity;
                }
                else
                {
                    TotalLivingResource -= NewLivingQuantity - LivingDistributeQuantity;

                    SourceManager.LivingResource = TotalLivingResource;
                    LivingDistributeQuantity = NewLivingQuantity;
                    Text.GetComponent<TextMeshProUGUI>().text = LivingDistributeQuantity.ToString();
                    DistributeBar.GetComponent<DistributeBarMono>().SetLivingResourceQuantity(LivingDistributeQuantity);
                }
            }
            else
            {
                TotalLivingResource += LivingDistributeQuantity - NewLivingQuantity;
                SourceManager.LivingResource = TotalLivingResource;
                LivingDistributeQuantity = NewLivingQuantity;
                Text.GetComponent<TextMeshProUGUI>().text = LivingDistributeQuantity.ToString();
                DistributeBar.GetComponent<DistributeBarMono>().SetLivingResourceQuantity(LivingDistributeQuantity);
            }
        }
    }
}
