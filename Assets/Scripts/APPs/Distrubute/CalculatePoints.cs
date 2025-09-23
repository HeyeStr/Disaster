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

    [Header("风险系数设置")]
    public float LowRiskIndex = 1.1f;      // 低风险系数
    public float MediumRiskIndex = 1.25f;  // 中风险系数
    public float HighRiskIndex = 1.5f;     // 高风险系数
    
    [Header("当前选择的风险等级")]
    public RiskFactor CurrentRiskLevel = RiskFactor.Low;

    void Start()
    {
        InitializeDefaultGrades();
    }



    public float Calculate(int allocation_living, int allocation_food, int allocation_medicine, 
                          int actualDemand_living, int actualDemand_food, int actualDemand_medicine)
    {
        float totalScore = 0f;

        int livingGradeIndex = GetGradeIndexByDemand(actualDemand_living);
        float livingScore = CalculateTotalScore(livingGradeIndex, allocation_living, actualDemand_living);
        
        int foodGradeIndex = GetGradeIndexByDemand(actualDemand_food);
        float foodScore = CalculateTotalScore(foodGradeIndex, allocation_food, actualDemand_food);

        int medicineGradeIndex = GetGradeIndexByDemand(actualDemand_medicine);
        float medicineScore = CalculateTotalScore(medicineGradeIndex, allocation_medicine, actualDemand_medicine);
        
        totalScore = livingScore + foodScore + medicineScore;
        Debug.Log(allocation_living+" " + allocation_food + " " + allocation_medicine + " " + actualDemand_living + " " + actualDemand_food + " " + actualDemand_medicine);

        Debug.Log("livingScore" + livingScore);
        Debug.Log("foodScore" + foodScore);
        Debug.Log("medicineScore" + medicineScore);
        return totalScore;
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
    
    private float GetCurrentRiskIndex()
    {
        switch (CurrentRiskLevel)
        {
            case RiskFactor.Low:
                return LowRiskIndex;
            case RiskFactor.Medium:
                return MediumRiskIndex;
            case RiskFactor.High:
                return HighRiskIndex;
            default:
                return LowRiskIndex;
        }
    }
    
    private float CalculateTotalScore(int gradeIndex, float allocation, float actualDemand)
    {
        // 验证输入参数
        if (gradeIndex < 0 || gradeIndex >= gradeConfigs.Length)
        {
            return 0f;
        }
        
        GradeConfig grade = gradeConfigs[gradeIndex];
        AllocationRelation relation = GetAllocationRelation(allocation, actualDemand);
        
        float totalScore = 0f;
        
        switch (relation)
        {
            case AllocationRelation.Equal:
                totalScore = CalculateEqualScore(grade);
                break;
                
            case AllocationRelation.Excess:
                totalScore = CalculateExcessScore(grade);
                break;
                
            case AllocationRelation.Shortage:
                totalScore = CalculateShortageScore(grade, allocation, actualDemand);
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
    
    private float CalculateEqualScore(GradeConfig grade)
    {
        return grade.baseScore * GetCurrentRiskIndex();
    }
    
    private float CalculateExcessScore(GradeConfig grade)
    {
        return grade.baseScore;
    }
    
    private float CalculateShortageScore(GradeConfig grade, float allocation, float actualDemand)
    {
        float shortage = actualDemand - allocation;
        float deduction = shortage * grade.deductionBase * GetCurrentRiskIndex();
        return grade.baseScore - deduction;
    }
}
