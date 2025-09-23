using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class FoodBarDistributeMono : MonoBehaviour
{
    public GameObject MonitorObject;
    public int FoodDistributeQuantity;
    public int NewFoodQuantity;
    public int MissionIndex;
    ResourcesManager SourceManager;
    private GameObject Text;
    private GameObject Slider;
    public GameObject DistributeBar;
    void Start()
    {
        FoodDistributeQuantity = 0;
        MonitorObject = GameObject.FindGameObjectWithTag("Monitor");
        SourceManager = MonitorObject.GetComponent<ResourcesManager>();
        Text = transform.Find("Text").gameObject;
        Text.GetComponent<TextMeshProUGUI>().text="0";
        Slider = transform.Find("SliderFood").gameObject;
    }

    // Update is called once per frame
    void Update()

    {
        NewFoodQuantity = (int)Slider.GetComponent<Slider>().value;
        if (NewFoodQuantity != FoodDistributeQuantity)
        {
            int TotalFoodResource = SourceManager.FoodResource;
            if (NewFoodQuantity > FoodDistributeQuantity)
            {
                

                if (TotalFoodResource - NewFoodQuantity + FoodDistributeQuantity < 0)
                {
                    Slider.GetComponent<Slider>().value = FoodDistributeQuantity;                                        //food资源小于0，不能这样操作！！！！！！！！！！！！！
                }
                else
                {
                    TotalFoodResource -= NewFoodQuantity - FoodDistributeQuantity;

                    SourceManager.FoodResource = TotalFoodResource;
                    FoodDistributeQuantity = NewFoodQuantity;
                    Text.GetComponent<TextMeshProUGUI>().text = FoodDistributeQuantity.ToString();
                    DistributeBar.GetComponent<DistributeBarMono>().SetFoodResourceQuantity (FoodDistributeQuantity);
                }
            }
            else
            {
                TotalFoodResource += FoodDistributeQuantity - NewFoodQuantity;
                SourceManager.FoodResource = TotalFoodResource;
                FoodDistributeQuantity = NewFoodQuantity;
                Text.GetComponent<TextMeshProUGUI>().text = FoodDistributeQuantity.ToString();
                DistributeBar.GetComponent<DistributeBarMono>().SetFoodResourceQuantity(FoodDistributeQuantity);
            }
        }
    }
}
