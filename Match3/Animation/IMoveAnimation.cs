using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Animation
{
    internal interface IMoveAnimation
    {
        void Anime(List<ElementToMove> list);
    }
}
