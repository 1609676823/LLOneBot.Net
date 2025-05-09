using System.ComponentModel;
using System.Runtime.Serialization;


/// <summary>
/// 请求的body类型
/// </summary>
public enum BodyType
{
    /// <summary>
    ///none，默认不指定，使用GET请求的时候也不指定
    /// </summary>
    [EnumMember(Value = "none")]
    [Description("none")]
    none,
    /// <summary>
    ///formdata类型, 使用post请求需要传送body的时候，必须指定其中之一
    /// </summary>
    [EnumMember(Value = "formdata")]
    [Description("formdata")]
    formdata,
    /// <summary>
    ///urlencoded类型， 使用post请求需要传送body的时候，必须指定其中之一
    /// </summary>
    [EnumMember(Value = "urlencoded")]
    [Description("urlencoded")]
    urlencoded,
    /// <summary>
    ///raw类型, 使用post请求需要传送body的时候，必须指定其中之一
    /// </summary>
    [EnumMember(Value = "raw")]
    [Description("raw")]
    raw,
}

