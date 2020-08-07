using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoZeroFourEight
{
    /// <summary>
    /// 方向
    /// </summary>
    public enum Direction
    {
        Left = 0,
        Right = 1,
        Up = 2,
        Down = 3,
    }

    /// <summary>
    /// 状态
    /// </summary>
    public enum State
    {
        Idel,
        Start,
        Running,
    }
}
