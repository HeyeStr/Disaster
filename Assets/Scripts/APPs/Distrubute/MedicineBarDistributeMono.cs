using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MedicineBarDistributeMono : MonoBehaviour
{
    public GameObject MonitorObject;
    public int MedicineDistributeQuantity;
    public int NewMedicineQuantity;
    public int MissionIndex;
    ResourcesManager SourceManager;
    private GameObject Text;
    private GameObject Slider;
    public GameObject DistributeBar;
    void Start()
    {
        MedicineDistributeQuantity = 0;
        MonitorObject = GameObject.FindGameObjectWithTag("Monitor");
        SourceManager = MonitorObject.GetComponent<ResourcesManager>();
        Text = transform.Find("Text").gameObject;
        Text.GetComponent<TextMeshProUGUI>().text = "0";
        Slider = transform.Find("SliderMedicine").gameObject;


    }

    // Update is called once per frame
    void Update()
    {
        NewMedicineQuantity= (int)Slider.GetComponent<Slider>().value;
        if (NewMedicineQuantity != MedicineDistributeQuantity)
        {
            int TotalMedicalResource = SourceManager.MedicalResource;
            if (NewMedicineQuantity > MedicineDistributeQuantity)
            {
                
                
                if (TotalMedicalResource - NewMedicineQuantity + MedicineDistributeQuantity<0) {
                    Slider.GetComponent<Slider>().value = MedicineDistributeQuantity;                                                                                                //医疗资源小于0，不能这样操作！！！！！！！！！！！！！
                }
                else
                {
                    TotalMedicalResource -= NewMedicineQuantity - MedicineDistributeQuantity;

                    SourceManager.MedicalResource= TotalMedicalResource;
                    MedicineDistributeQuantity = NewMedicineQuantity;
                    Text.GetComponent<TextMeshProUGUI>().text = MedicineDistributeQuantity.ToString();
                    DistributeBar.GetComponent<DistributeBarMono>().SetMedicalResourceQuantity(MedicineDistributeQuantity);
                }
            }
            else
            {
                TotalMedicalResource +=  MedicineDistributeQuantity- NewMedicineQuantity;
                SourceManager.MedicalResource = TotalMedicalResource;
                MedicineDistributeQuantity = NewMedicineQuantity;
                Text.GetComponent<TextMeshProUGUI>().text = MedicineDistributeQuantity.ToString();
                DistributeBar.GetComponent<DistributeBarMono>().SetMedicalResourceQuantity(MedicineDistributeQuantity);
            }
        }
    }
}
