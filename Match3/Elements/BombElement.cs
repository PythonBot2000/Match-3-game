using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Elements
{
    internal class BombElement : Element
	{
		public BombElement(PlayingField PlayingField, Button Button, Point PositionOnField) : base(PlayingField, Button, PositionOnField)
		{
			button.Text = "Bomb";
		}

		public override List<Point>? Destroy()
		{
			List<Point> list = new List<Point>();
			for(int i = PosOnField.X - 1; i <= PosOnField.X + 1; i++)
			{
				for (int j = PosOnField.Y - 1; j <= PosOnField.Y + 1; j++)
				{
					Point point = new Point(i, j);
					if (ValidatePoint(point)) list.Add(point);
				}
			}
			isExist = false;

			return list;
		}
	}
}
