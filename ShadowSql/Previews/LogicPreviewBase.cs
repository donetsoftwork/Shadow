using ShadowSql.Logics;
using System;

namespace ShadowSql.Previews;

/// <summary>
/// 逻辑展开基类
/// </summary>
public abstract class LogicPreviewBase : IPreview<AtomicLogic>
{
    #region 配置
    /// <summary>
    /// 是否为空
    /// </summary>
    protected bool _isEmpty = true;
    private AtomicLogic? _first;
    private bool _hasSecond = false;
    /// <summary>
    /// 是否为空
    /// </summary>

    public bool IsEmpty
        => _isEmpty;
    /// <summary>
    /// 第一个
    /// </summary>
    public AtomicLogic First
    {
        get { return _first ?? throw new InvalidOperationException("不存在"); }
        protected set
        {
            _first = value;
            _isEmpty = false;
        }
    }
    /// <summary>
    /// 是否含第二个
    /// </summary>
    public bool HasSecond
    {
        get { return _hasSecond; }
        protected set
        {
            _hasSecond = true;
        }
    }
    #endregion
}
