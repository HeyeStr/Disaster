using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineBarDistributeMono : MonoBehaviour
{
    public GameObject MonitorObject;
    public int MedicineDistributeQuantity;
    public int NewMedicineQuantity;
    public int MissionIndex;
    ResourcesManager SourceManager;
    void Start()
    {
        MonitorObject= GameObject.FindGameObjectWithTag("Monitor");
        SourceManager = MonitorObject.GetComponent<ResourcesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NewMedicineQuantity != MedicineDistributeQuantity)
        {
            int TotalMedicalResource = SourceManager.MedicalResource;
            if (NewMedicineQuantity > MedicineDistributeQuantity)
            {
                
                
                if (TotalMedicalResource - NewMedicineQuantity + MedicineDistributeQuantity<0) {
                                                                                                                            //医疗资源小于0，不能这样操作！！！！！！！！！！！！！
                }
                else
                {
                    TotalMedicalResource -= NewMedicineQuantity - MedicineDistributeQuantity;

                    SourceManager.MedicalResource= TotalMedicalResource;
                    MedicineDistributeQuantity = NewMedicineQuantity;
                }
            }
            else
            {
                TotalMedicalResource +=  MedicineDistributeQuantity- NewMedicineQuantity;
                SourceManager.MedicalResource = TotalMedicalResource;
                MedicineDistributeQuantity = NewMedicineQuantity;
            }
        }
    }
}
