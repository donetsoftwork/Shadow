using ShadowSql.Logics;

namespace ShadowSql.Previews;

/// <summary>
/// 复合逻辑预览
/// </summary>
public class ComplexLogicPreview : LogicPreviewBase
{
    /// <summary>
    /// 复合逻辑预览
    /// </summary>
    /// <param name="complex"></param>
    public ComplexLogicPreview(ComplexLogicBase complex)
    {
        _complex = complex;
        Init();
    }
    #region 配置
    /// <summary>
    /// 复合逻辑
    /// </summary>
    protected readonly ComplexLogicBase _complex;
    /// <summary>
    /// 复合逻辑
    /// </summary>
    public ComplexLogicBase Complex
        => _complex;    
    #endregion
    /// <summary>
    /// 初始化
    /// </summary>
    protected virtual void Init()
    {
        ExpandTwo(_complex);
    }
    /// <summary>
    /// 展开两个
    /// </summary>
    /// <param name="complex"></param>
    /// <returns></returns>
    protected bool ExpandTwo(ComplexLogicBase complex)
    {
        var logics = complex._logics;
        switch (logics.Count)
        {
            case 0:
                foreach (ComplexLogicBase item in complex._others)
                {
                    if (_isEmpty)
                    {
                        if (ExpandTwo(item))
                            return true;
                    }
                    else
                    {
                        if (ExpandSingle(complex))
                            return true;
                    }
                }
                break;
            case 1:
                First = logics[0];
                foreach (ComplexLogicBase item in complex._others)
                {
                    if (ExpandSingle(item))
                    {
                        HasSecond = true;
                        return true;
                    }
                }
                break;
            default:
                First = logics[0];
                HasSecond = true;
                return true;
        }
        return false;
    }
    /// <summary>
    /// 展开一个
    /// </summary>
    /// <param name="complex"></param>
    /// <returns></returns>
    protected static bool ExpandSingle(ComplexLogicBase complex)
    {
        if (complex._logics.Count > 0)
            return true;
        foreach (ComplexLogicBase item in complex._others)
        {
            if (ExpandSingle(item))
                return true;
        }
        return false;
    }
}
