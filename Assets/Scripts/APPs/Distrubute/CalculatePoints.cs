using System.Collections;
using System.Collections.Generic;
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
    
    [Header("测试数据")]
    public int testGradeIndex = 0;
    public RiskFactor testRiskFactor = RiskFactor.Low;
    public float testAllocation = 100f;
    public float testActualDemand = 80f;
    
    void Start()
    {
        InitializeDefaultGrades();
    }
    
    void Update()
    {
        // 测试用 - 按空格键计算测试分数
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float result = CalculateTotalScore(testGradeIndex, testRiskFactor, testAllocation, testActualDemand);
            Debug.Log($"计算结果: {result} 分");
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
    
    public float CalculateTotalScore(int gradeIndex, RiskFactor riskFactor, float allocation, float actualDemand)
    {
        // 验证输入参数
        if (gradeIndex < 0 || gradeIndex >= gradeConfigs.Length)
        {
            Debug.LogError("档位索引超出范围");
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
        
        Debug.Log($"实际需求量: {grade.actualDemand}, 风险系数: {riskCoeff}, 关系: {relation}, 总分: {totalScore}");
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
    
    public GradeConfig GetGradeConfig(int gradeIndex)
    {
        if (gradeIndex >= 0 && gradeIndex < gradeConfigs.Length)
            return gradeConfigs[gradeIndex];
        return null;
    }

    public void SetRiskCoefficient(RiskFactor factor, float coefficient)
    {
        riskCoefficients[(int)factor] = coefficient;
    }
}
