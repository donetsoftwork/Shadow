using ShadowSql.Engines;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Fragments;

/// <summary>
/// sql片段组合
/// </summary>
public sealed class JoinFragment : ISqlFragment
{
    /// <summary>
    /// sql片段组合
    /// </summary>
    /// <param name="separator"></param>
    public JoinFragment(string separator = ",")
        : this([], separator)
    {
    }
    /// <summary>
    /// sql片段组合
    /// </summary>
    /// <param name="items"></param>
    /// <param name="separator"></param>
    internal JoinFragment(List<string> items, string separator = ",")
    {
        _items = items;
        _separator = separator;
    }
    /// <summary>
    /// 分隔符
    /// </summary>
    internal readonly string _separator;
    /// <summary>
    /// 片段
    /// </summary>
    internal readonly List<string> _items;
    /// <summary>
    /// 分隔符
    /// </summary>
    public string Separator
        => _separator;
    /// <summary>
    /// 片段
    /// </summary>
    public IEnumerable<string> Items
        => _items;
    /// <summary>
    /// 子项数量
    /// </summary>
    public int Count
        => _items.Count;
    /// <summary>
    /// 索引器
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string this[int index]
        => _items[index];
    /// <summary>
    /// 添加片段
    /// </summary>
    /// <param name="items"></param>
    public void Add(params IEnumerable<string> items)
    {
        foreach (var item in items)
        {
            if(string.IsNullOrEmpty(item))
                continue;
            _items.Add(item);
        }
    }
    #region ISqlFragment
    /// <summary>
    /// 拼接片段
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="builder"></param>
    /// <returns></returns>
    bool ISqlFragment.TryWrite(ISqlEngine engine, StringBuilder builder)
        => TryWrite(engine, builder, false);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="builder"></param>
    /// <param name="appended"></param>
    /// <returns></returns>
    public bool TryWrite(ISqlEngine engine, StringBuilder builder, bool appended = false)
    {
        foreach (var item in _items)
        {
            if (appended)
                builder.Append(_separator);
            builder.Append(item);
            appended = true;
        }
        return appended;
    }
    #endregion
}
