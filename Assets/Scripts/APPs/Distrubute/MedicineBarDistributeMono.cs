using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineBarDistributeMono : MonoBehaviour
{
    // Start is called before the first frame update

    public int MedicineDistributeQuantity;
    public int MissionIndex;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConfirmQuantity()
    {
        ResourcesManager.Instance.MedicalResource -= MedicineDistributeQuantity;
        
    }
}
