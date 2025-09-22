using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeBarMono : MonoBehaviour
{
    private GameObject DistributeControlObject;
    public string Address;
    public string PhoneNumber;
    public int LivingResourceQuantity;
    public int FoodResourceQuantity;
    public int MedicalResourceQuantity;
    public int Score;
    public  GameObject AddressBar;
    public  GameObject PhoneNumberBar;
    
    void Start()
    {

    }

    void Update()
    {

    }
    public int Submit()
    {
        GameObject TodoList = GameObject.FindGameObjectWithTag("ToDoList");
        List<Mission> missions = TodoList.GetComponent<TaskToDoListTextMono>().Missions;
        Score = 0;
        foreach (var mission in missions)
        {
            if (mission.Address == Address)
            {
                if (mission.PhoneNumber == PhoneNumber)
                {

                    missions.Remove(mission);
                    return Score;
                }
                else
                {
                    Score = 0;                                  //电话和地址对不上任务失败

                    missions.Remove(mission);
                    return Score;

                }
            }
        }
        return Score;
    }
    public void DeleteInformation()
    {
        
        Address = "";
        PhoneNumber = "";
        AddressBar .GetComponent<AddressBarMono>().DeleteInformation();
        PhoneNumberBar.GetComponent<PhoneNumberBarMono>().DeleteInformation();
    }
    public void SetFoodResourceQuantity(int Quantity)
    {
        FoodResourceQuantity = Quantity;
    }
    public void SetMedicalResourceQuantity(int Quantity)
    {
        MedicalResourceQuantity = Quantity;

    }
    public void SetLivingResourceQuantity(int Quantity)
    {
        LivingResourceQuantity = Quantity;
    }
}
