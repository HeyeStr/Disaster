using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    [Header("总分显示")]
    [SerializeField] private Text totalScoreText; // UI Text组件
    [SerializeField] private string scorePrefix = "总分: "; // 分数前缀
    [SerializeField] private string scoreSuffix = " 分"; // 分数后缀
    
    [Header("测试数据")]
    public int testGradeIndex = 0;
    public RiskFactor testRiskFactor = RiskFactor.Low;
    public float testAllocation = 100f;
    public float testActualDemand = 80f;
    
    // 从外部传入的参数
    private float currentActualDemand = 0f;
    private float currentPlayerAllocation = 0f;
    
    // 累加分数相关
    private float totalAccumulatedScore = 0f;
    private List<float> scoreHistory = new List<float>(); // 记录每次计算的分数
    
    void Start()
    {
        InitializeDefaultGrades();
        UpdateTotalScoreDisplay();
    }
    
    void Update()
    {
        // 测试用 - 按空格键计算测试分数
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float result = CalculateTotalScore(testGradeIndex, testRiskFactor, testAllocation, testActualDemand);
            AddToTotalScore(result);
        }
        
        // 测试用 - 按R键重置总分
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTotalScore();
        }
    }
    
    /// <summary>
    /// 设置总分显示的Text组件
    /// </summary>
    /// <param name="textComponent">UI Text组件</param>
    public void SetTotalScoreText(Text textComponent)
    {
        totalScoreText = textComponent;
        UpdateTotalScoreDisplay();
    }
    
    /// <summary>
    /// 将分数添加到总分中
    /// </summary>
    /// <param name="score">要添加的分数</param>
    public void AddToTotalScore(float score)
    {
        totalAccumulatedScore += score;
        scoreHistory.Add(score);
        UpdateTotalScoreDisplay();
        Debug.Log($"添加分数: {score}, 当前总分: {totalAccumulatedScore}");
    }
    
    /// <summary>
    /// 计算分数并自动添加到总分
    /// </summary>
    /// <param name="gradeIndex">档位索引</param>
    /// <param name="riskFactor">风险系数</param>
    /// <param name="allocation">分配量</param>
    /// <param name="actualDemand">实际需求量</param>
    /// <returns>计算得出的分数</returns>
    public float CalculateAndAddScore(int gradeIndex, RiskFactor riskFactor, float allocation, float actualDemand)
    {
        float score = CalculateTotalScore(gradeIndex, riskFactor, allocation, actualDemand);
        AddToTotalScore(score);
        return score;
    }
    
    /// <summary>
    /// 使用当前值计算分数并添加到总分
    /// </summary>
    /// <param name="gradeIndex">档位索引</param>
    /// <param name="riskFactor">风险系数</param>
    /// <returns>计算得出的分数</returns>
    public float CalculateAndAddScoreWithCurrentValues(int gradeIndex, RiskFactor riskFactor)
    {
        float score = CalculateScoreWithCurrentValues(gradeIndex, riskFactor);
        AddToTotalScore(score);
        return score;
    }
    
    /// <summary>
    /// 获取当前总分
    /// </summary>
    /// <returns>累积总分</returns>
    public float GetTotalScore()
    {
        return totalAccumulatedScore;
    }
    
    /// <summary>
    /// 重置总分
    /// </summary>
    public void ResetTotalScore()
    {
        totalAccumulatedScore = 0f;
        scoreHistory.Clear();
        UpdateTotalScoreDisplay();
        Debug.Log("总分已重置");
    }
    
    /// <summary>
    /// 获取分数历史记录
    /// </summary>
    /// <returns>分数历史列表</returns>
    public List<float> GetScoreHistory()
    {
        return new List<float>(scoreHistory);
    }
    
    /// <summary>
    /// 更新总分显示
    /// </summary>
    private void UpdateTotalScoreDisplay()
    {
        if (totalScoreText != null)
        {
            totalScoreText.text = $"{scorePrefix}{totalAccumulatedScore:F1}{scoreSuffix}";
        }
    }
    
    /// <summary>
    /// 设置分数显示格式
    /// </summary>
    /// <param name="prefix">前缀文本</param>
    /// <param name="suffix">后缀文本</param>
    public void SetScoreDisplayFormat(string prefix, string suffix)
    {
        scorePrefix = prefix;
        scoreSuffix = suffix;
        UpdateTotalScoreDisplay();
    }

    public void SetActualDemand(float actualDemand)
    {
        currentActualDemand = actualDemand;
        Debug.Log($"设置实际需求量: {actualDemand}");
    }

    public void SetPlayerAllocation(float playerAllocation)
    {
        currentPlayerAllocation = playerAllocation;
        Debug.Log($"设置玩家分配量: {playerAllocation}");
    }

    public void SetBothValues(float actualDemand, float playerAllocation)
    {
        currentActualDemand = actualDemand;
        currentPlayerAllocation = playerAllocation;
        Debug.Log($"设置值 - 实际需求量: {actualDemand}, 玩家分配量: {playerAllocation}");
    }

    public float CalculateScoreWithCurrentValues(int gradeIndex, RiskFactor riskFactor)
    {
        return CalculateTotalScore(gradeIndex, riskFactor, currentPlayerAllocation, currentActualDemand);
    }

    public float GetCurrentActualDemand()
    {
        return currentActualDemand;
    }

    public float GetCurrentPlayerAllocation()
    {
        return currentPlayerAllocation;
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
