using Match3.Elements.DeleteAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Elements
{
    internal class LineElement : Element
	{
		bool isHorizontal;
		public LineElement(PlayingField PlayingField, Button Button, Point PositionOnField, bool isHorizontal)
			: base(PlayingField, Button, PositionOnField)
		{
			this.isHorizontal = isHorizontal;

			if(isHorizontal) button.Text = "Line H";
			else button.Text = "Line V";
		}

		public override List<Point>? Destroy()
		{
			List<Point> list = new List<Point>();

			if(isHorizontal)
			{
				for(int i = 0; i < PlayingField.Rows; i++)
				{
					list.Add(new Point(i, PosOnField.Y));
				}
			}
            else
            {
				for (int i = 0; i < PlayingField.Columns; i++)
				{
					list.Add(new Point(PosOnField.X, i));
				}
			}

            isExist = false;
			return list;
		}
	}
}
