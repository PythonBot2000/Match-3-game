using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match3.Elements;

namespace Match3.Animation
{
    internal class ElementToMove
    {
        public Element Element { get; }
        public Point TargetPosOnField { get; }
        public Point TargetPosOnWindow { get; }
        public Point OriginPosOnField { get; }
        public Point OriginPosOnWindow { get; }

        public ElementToMove(Element Element, Point TargetPos)
        {
            this.Element = Element;
            TargetPosOnField = TargetPos;
            OriginPosOnField = Element.PosOnField;
            TargetPosOnWindow = Element.PosOnFieldToWindow(TargetPos);
            OriginPosOnWindow = Element.PosOnWindow;
        }
    }
}
