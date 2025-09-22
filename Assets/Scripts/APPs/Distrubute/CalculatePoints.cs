using UnityEngine;

[System.Serializable]
public class GradeConfig
{
    public string actualDemand;        // 实际需求量档位名称
    public float baseScore;         // 基础分数
    public float deductionBase;     // 扣分基数
}

public enum RiskFactor
{
    Low = 0,     
    Medium = 1,  
    High = 2     
}

public enum AllocationRelation
{
    Equal,      // 分配量 = 实际需求量
    Excess,     // 分配量 > 实际需求量
    Shortage    // 分配量 < 实际需求量
}

public class CalculatePoints : MonoBehaviour
{
    [Header("档位配置")]
    [SerializeField] private GradeConfig[] gradeConfigs = new GradeConfig[5];

    [Header("风险系数")]
    [SerializeField] private float[] riskCoefficients = new float[3] { 1.1f, 1.25f, 1.5f };

    void Start()
    {
        InitializeDefaultGrades();
    }



    public float Calculate(int allocation_living, int allocation_food, int allocation_medicine, 
                          int actualDemand_living, int actualDemand_food, int actualDemand_medicine, int settledPeople)
    {
        float totalScore = 0f;

        RiskFactor riskFactor = GetRiskFactorBySettledPeople(settledPeople);

        

        int livingGradeIndex = GetGradeIndexByDemand(actualDemand_living);
        float livingScore = CalculateTotalScore(livingGradeIndex, riskFactor, allocation_living, actualDemand_living);
        
        int foodGradeIndex = GetGradeIndexByDemand(actualDemand_food);
        float foodScore = CalculateTotalScore(foodGradeIndex, riskFactor, allocation_food, actualDemand_food);

        int medicineGradeIndex = GetGradeIndexByDemand(actualDemand_medicine);
        float medicineScore = CalculateTotalScore(medicineGradeIndex, riskFactor, allocation_medicine, actualDemand_medicine);
        
        totalScore = livingScore + foodScore + medicineScore;
        
        return totalScore;
    }

    private RiskFactor GetRiskFactorBySettledPeople(int settledPeople)
    {
        if (settledPeople < 20)
        {
            return RiskFactor.Low;      
        }
        else if (settledPeople <= 60)
        {
            return RiskFactor.Medium;   
        }
        else
        {
            return RiskFactor.High;    
        }
    }
    

    private int GetGradeIndexByDemand(int demand)
    {
        switch (demand)
        {
            case 1:
                return 4;  // 需求量1 → 使用档位5 (基础分20，扣分基数4)
            case 2:
                return 3;  // 需求量2 → 使用档位4 (基础分30，扣分基数6)
            case 3:
                return 2;  // 需求量3 → 使用档位3 (基础分40，扣分基数8)
            case 4:
                return 1;  // 需求量4 → 使用档位2 (基础分70，扣分基数14)
            case 5:
                return 0;  // 需求量5 → 使用档位1 (基础分100，扣分基数20)
            default:
                return 0;  // 默认使用档位1
        }
    }
    
    private void InitializeDefaultGrades()
    {
        if (gradeConfigs[0] == null)
        {
            gradeConfigs[0] = new GradeConfig { actualDemand = "1", baseScore = 100f, deductionBase = 20f };
            gradeConfigs[1] = new GradeConfig { actualDemand = "2", baseScore = 70f, deductionBase = 14f };
            gradeConfigs[2] = new GradeConfig { actualDemand = "3", baseScore = 40f, deductionBase = 8f };
            gradeConfigs[3] = new GradeConfig { actualDemand = "4", baseScore = 30f, deductionBase = 6f };
            gradeConfigs[4] = new GradeConfig { actualDemand = "5", baseScore = 20f, deductionBase = 4f };
        }
    }
    
    private float CalculateTotalScore(int gradeIndex, RiskFactor riskFactor, float allocation, float actualDemand)
    {
        // 验证输入参数
        if (gradeIndex < 0 || gradeIndex >= gradeConfigs.Length)
        {
            return 0f;
        }
        
        GradeConfig grade = gradeConfigs[gradeIndex];
        float riskCoeff = riskCoefficients[(int)riskFactor];
        AllocationRelation relation = GetAllocationRelation(allocation, actualDemand);
        
        float totalScore = 0f;
        
        switch (relation)
        {
            case AllocationRelation.Equal:
                totalScore = CalculateEqualScore(grade, riskCoeff);
                break;
                
            case AllocationRelation.Excess:
                totalScore = CalculateExcessScore(grade);
                break;
                
            case AllocationRelation.Shortage:
                totalScore = CalculateShortageScore(grade, riskCoeff, allocation, actualDemand);
                break;
        }
        
        return totalScore;
    }
    
    private AllocationRelation GetAllocationRelation(float allocation, float actualDemand)
    {
        float tolerance = 0.01f; // 容差值，避免浮点数精度问题
        
        if (Mathf.Abs(allocation - actualDemand) <= tolerance)
            return AllocationRelation.Equal;
        else if (allocation > actualDemand)
            return AllocationRelation.Excess;
        else
            return AllocationRelation.Shortage;
    }
    
    private float CalculateEqualScore(GradeConfig grade, float riskCoeff)
    {
        return grade.baseScore * riskCoeff;
    }
    
    private float CalculateExcessScore(GradeConfig grade)
    {
        return grade.baseScore;
    }
    
    private float CalculateShortageScore(GradeConfig grade, float riskCoeff, float allocation, float actualDemand)
    {
        float shortage = actualDemand - allocation;
        float deduction = shortage * grade.deductionBase * riskCoeff;
        return grade.baseScore - deduction;
    }
}
