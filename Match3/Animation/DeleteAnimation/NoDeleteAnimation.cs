using Match3.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Animation.DeleteAnimation
{
	internal class NoDeleteAnimation : IDeleteAnimation
	{
		MainForm BoundForm;
		public NoDeleteAnimation(PlayingField field)
		{
			BoundForm = field.BoundForm;
		}

		public void DeleteAnime(List<Element> list)
		{
			return;
		}
	}
}
