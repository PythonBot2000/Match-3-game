using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Elements.DeleteAlgorithm
{
	internal class DefaultDeleteAlgorithm : IDeleteAlgorithm
	{
		Color originColor;
		Color targerColor;

		public DefaultDeleteAlgorithm(Color originColor, Color targerColor)
		{
			this.originColor = originColor;
			this.targerColor = targerColor;
		}

		public Color Delete(int step, int stepCount) 
		{
			if (step == stepCount) return targerColor;

			int[] canals = new int[3];

			canals[0] = ((targerColor.R - originColor.R) / stepCount) * step;
			canals[1] = ((targerColor.G - originColor.G) / stepCount) * step;
			canals[2] = ((targerColor.B - originColor.B) / stepCount) * step;

			for(int i = 0; i < 3; i++) 
			{
				if (canals[i] < 0) 
				{
					canals[i] = 0;
				}
				else if (canals[i] > 255)
				{
					canals[i] = 255;
				}
			}

			return Color.FromArgb(canals[0], canals[1], canals[2]);
		}
	}
}
