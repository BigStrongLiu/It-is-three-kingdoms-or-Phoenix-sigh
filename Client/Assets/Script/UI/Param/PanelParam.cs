/*
    使用这个类的原因:
        1.不想使用object的装箱、拆箱过程（会增加开销，并影响安全性）
        2.方便管理
    至于这个类的开销 呵呵，待测。。。（2015.05.22）
*/
/// <summary>
/// 打开Panel的时候传递的参数
/// </summary
using System.Collections.Generic;

public class PanelParam
{
    public int? ID { get; set; }
    public long? UID { get; set; }
    public AlertParam Alert { get; set; }
}
