using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoZeroFourEight
{
    /// <summary>
    /// 是否可以控制
    /// </summary>
    interface IControlable
    {
        void ToLeft();
        void ToRight();
        void ToUp();
        void ToDown();
    }

    /// <summary>
    /// 是否可以移动
    /// </summary>
    interface IMovable
    {
        bool MoveLeft();
        bool MoveRight();
        bool MoveUp();
        bool MoveDown();
    }
}
