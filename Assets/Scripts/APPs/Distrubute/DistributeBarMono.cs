using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeBarMono : MonoBehaviour
{
    private GameObject DistributeControlObject;
    public int missionIndex;
    public string Address;
    public string PhoneNumber;
    public int LivingResourceQuantity;
    public int FoodResourceQuantity;
    public int MedicalResourceQuantity;
    public int Score;
    private GameObject AddressBar;
    private GameObject PhoneNumberBar;
    
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


                    return Score;
                }
                else
                {
                    Score = 0;                                  //�绰�͵�ַ�Բ�������ʧ��


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
