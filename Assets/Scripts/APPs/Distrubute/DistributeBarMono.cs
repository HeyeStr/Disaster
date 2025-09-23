using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        Transform textransform = gameObject.transform.Find("TextScore");
        CalculatePoints calculatePoints = gameObject.GetComponent<CalculatePoints>();
        GameObject Monitor = GameObject.FindGameObjectWithTag("Monitor");
        AllMissionsMono allMissionsMono = Monitor.GetComponent<AllMissionsMono>();
        foreach (var mission in missions)
        {
            if (mission.Address == Address)
            {
                if (mission.PhoneNumber == PhoneNumber)
                {

                    
                    textransform.gameObject.GetComponent<TextMeshProUGUI>().text = Score.ToString();
                    // Score = calculatePoints.Calculate(LivingResourceQuantity, FoodResourceQuantity, MedicalResourceQuantity, allMissionsMono.GetLivingResource(), allMissionsMono.GetFoodResource(), allMissionsMono.GetMedicineResource());

                    missions.Remove(mission);
                    return Score;
                }
                else
                {
                    Score = 0;                                  //�绰�͵�ַ�Բ�������ʧ��

                   
                    textransform.gameObject.GetComponent<TextMeshProUGUI>().text = Score.ToString();
                    missions.Remove(mission);
                    return Score;

                }
            }
        }
        textransform.gameObject.GetComponent<TextMeshProUGUI>().text = Score.ToString();

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
