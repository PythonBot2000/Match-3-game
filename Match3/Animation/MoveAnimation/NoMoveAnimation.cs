using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Animation.MoveAnimation
{
	internal class NoMoveAnimation : IMoveAnimation
	{
		PlayingField Field;

		public NoMoveAnimation(PlayingField field)
		{
			Field = field;
		}

		public void Anime(List<ElementToMove> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Element.PosOnField = list[i].TargetPosOnField;

				Field.SetElement(list[i].Element, list[i].TargetPosOnField);
			}
		}
	}
}
