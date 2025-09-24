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
    [SerializeField] private GradeConfig[] gradeConfigs = new GradeConfig[6];

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
                          int actualDemand_living, int actualDemand_food, int actualDemand_medicine,float RiskIndex)
    {
        float totalScore = 0f;

        Debug.Log($"输入参数 - 分配量: Living:{allocation_living}, Food:{allocation_food}, Medicine:{allocation_medicine}");
        Debug.Log($"输入参数 - 实际需求: Living:{actualDemand_living}, Food:{actualDemand_food}, Medicine:{actualDemand_medicine}");

        int livingGradeIndex = GetGradeIndexByDemand(actualDemand_living);
        float livingScore = CalculateTotalScore(livingGradeIndex, allocation_living, actualDemand_living);
        
        int foodGradeIndex = GetGradeIndexByDemand(actualDemand_food);
        float foodScore = CalculateTotalScore(foodGradeIndex, allocation_food, actualDemand_food);

        int medicineGradeIndex = GetGradeIndexByDemand(actualDemand_medicine);
        float medicineScore = CalculateTotalScore(medicineGradeIndex, allocation_medicine, actualDemand_medicine);
        
        totalScore = livingScore + foodScore + medicineScore;
        Debug.Log(allocation_living+" " + allocation_food + " " + allocation_medicine + " " + actualDemand_living + " " + actualDemand_food + " " + actualDemand_medicine);

        Debug.Log("livingScore: " + livingScore);
        Debug.Log("foodScore: " + foodScore);
        Debug.Log("medicineScore: " + medicineScore);
        Debug.Log("totalScore: " + totalScore);
        return totalScore;
    }

    private int GetGradeIndexByDemand(int demand)
    {
        switch (demand)
        {
            case 0:
                return 5;  // 需求量0 → 使用档位6 (基础分0，扣分基数0)
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
                Debug.LogWarning($"未知的需求量: {demand}，使用默认档位");
                return 5;  // 默认使用需求量0的档位
        }
    }
    
    private void InitializeDefaultGrades()
    {

        
        // 根据GetGradeIndexByDemand的映射关系正确初始化
        gradeConfigs[0] = new GradeConfig { actualDemand = "5", baseScore = 100f, deductionBase = 20f }; // 需求量5 → 索引0
        gradeConfigs[1] = new GradeConfig { actualDemand = "4", baseScore = 70f, deductionBase = 14f };  // 需求量4 → 索引1
        gradeConfigs[2] = new GradeConfig { actualDemand = "3", baseScore = 40f, deductionBase = 8f };   // 需求量3 → 索引2
        gradeConfigs[3] = new GradeConfig { actualDemand = "2", baseScore = 30f, deductionBase = 6f };   // 需求量2 → 索引3
        gradeConfigs[4] = new GradeConfig { actualDemand = "1", baseScore = 20f, deductionBase = 4f };   // 需求量1 → 索引4
        gradeConfigs[5] = new GradeConfig { actualDemand = "0", baseScore = 0f, deductionBase = 0f };    // 需求量0 → 索引5
        
        Debug.Log("配置创建完成！");
        for (int i = 0; i < gradeConfigs.Length; i++)
        {
            if (gradeConfigs[i] != null)
            {
                Debug.Log($"gradeConfigs[{i}]: 需求量{gradeConfigs[i].actualDemand}, 基础分{gradeConfigs[i].baseScore}, 扣分基数{gradeConfigs[i].deductionBase}");
            }
            else
            {
                Debug.Log($"gradeConfigs[{i}]: null");
            }
        }
        
        // 测试需求量为0的情况
        TestZeroDemandCase();
    }
    
    private void TestZeroDemandCase()
    {
        Debug.Log("=== 测试需求量为0的情况 ===");
        float score = Calculate(0, 0, 0, 0, 0, 0,1);
        Debug.Log($"需求量和分配量都为0时的得分: {score}");
        
        // 预期结果应该是0分，因为需求量0对应基础分0
        if (Mathf.Approximately(score, 0f))
        {
            Debug.Log("✓ 测试通过：需求量为0时正确返回0分");
        }
        else
        {
            Debug.LogWarning($"✗ 测试失败：需求量为0时应该返回0分，但实际返回{score}分");
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
    
    private GradeConfig GetGradeConfig(int gradeIndex)
    {
        // 直接返回对应的配置，不依赖数组
        switch (gradeIndex)
        {
            case 0: // 需求量5
                return new GradeConfig { actualDemand = "5", baseScore = 100f, deductionBase = 20f };
            case 1: // 需求量4
                return new GradeConfig { actualDemand = "4", baseScore = 70f, deductionBase = 14f };
            case 2: // 需求量3
                return new GradeConfig { actualDemand = "3", baseScore = 40f, deductionBase = 8f };
            case 3: // 需求量2
                return new GradeConfig { actualDemand = "2", baseScore = 30f, deductionBase = 6f };
            case 4: // 需求量1
                return new GradeConfig { actualDemand = "1", baseScore = 20f, deductionBase = 4f };
            case 5: // 需求量0
                return new GradeConfig { actualDemand = "0", baseScore = 0f, deductionBase = 0f };
            default:
                Debug.LogError($"无效的档位索引: {gradeIndex}");
                return new GradeConfig { actualDemand = "default", baseScore = 0f, deductionBase = 0f };
        }
    }

    private float CalculateTotalScore(int gradeIndex, float allocation, float actualDemand)
    {
        // 验证输入参数
        if (gradeIndex < 0 || gradeIndex >= 6)
        {
            Debug.LogError($"无效的档位索引: {gradeIndex}");
            return 0f;
        }
        
        // 直接获取配置，不依赖数组
        GradeConfig grade = GetGradeConfig(gradeIndex);
        
        Debug.Log($"档位索引: {gradeIndex}, 基础分: {grade.baseScore}, 扣分基数: {grade.deductionBase}");
        Debug.Log($"分配量: {allocation}, 实际需求: {actualDemand}");
        
        AllocationRelation relation = GetAllocationRelation(allocation, actualDemand);
        Debug.Log($"分配关系: {relation}");
        
        float totalScore = 0f;
        
        switch (relation)
        {
            case AllocationRelation.Equal:
                totalScore = CalculateEqualScore(grade);
                Debug.Log($"相等分配，计算结果: {totalScore} = {grade.baseScore} * {GetCurrentRiskIndex()}");
                break;
                
            case AllocationRelation.Excess:
                totalScore = CalculateExcessScore(grade);
                Debug.Log($"过量分配，计算结果: {totalScore}");
                break;
                
            case AllocationRelation.Shortage:
                totalScore = CalculateShortageScore(grade, allocation, actualDemand);
                Debug.Log($"不足分配，计算结果: {totalScore}");
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
        float deduction = shortage * grade.baseScore/4 * GetCurrentRiskIndex();
        return grade.baseScore - deduction;
    }
}
