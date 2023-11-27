using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Elements
{
    internal class DefaultElement : Element
    {
        public DefaultElement(PlayingField PlayingField, Button Button, Point PositionOnField) 
            : base(PlayingField, Button, PositionOnField) { }

        public override List<Point>? Destroy()
        {
            isExist = false;
            return null;
        }
	}
}
