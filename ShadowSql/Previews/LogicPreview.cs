using ShadowSql.Logics;

namespace ShadowSql.Previews;

/// <summary>
/// 逻辑展开
/// </summary>
public class LogicPreview : LogicPreviewBase
{
    /// <summary>
    /// 复合逻辑展开
    /// </summary>
    /// <param name="logic"></param>
    public LogicPreview(Logic logic)
    {
        _logic = logic;
        ExpandTwo(logic);
    }
    #region 配置
    private readonly Logic _logic;
    /// <summary>
    /// 逻辑
    /// </summary>
    public Logic Logic
        => _logic;    
    #endregion
    /// <summary>
    /// 展开两个
    /// </summary>
    /// <param name="logic"></param>
    /// <returns></returns>
    private bool ExpandTwo(Logic logic)
    {
        var logics = logic._logics;
        switch (logics.Count)
        {
            case 0:
                break;
            case 1:
                First = logics[0];
                break;
            default:
                First = logics[0];
                HasSecond = true;
                return true;
        }
        return false;
    }
}
