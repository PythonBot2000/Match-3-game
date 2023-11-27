using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Models
{
	internal class PointCounter
	{
		public int Score { get; private set; }
		public int Count { get; set; }

		public PointCounter(int Count = 0)
		{
			this.Count = Count;
			Score = 0;
		}

		public int GetScore()
		{
			float multiplier;
			if(Count == 3)
			{
				multiplier = 1f;
			}
			else if(Count == 4) 
			{
				multiplier = 1.5f;
			}
			else if(Count == 5)
			{
				multiplier = 2f;
			}
			else
			{
				multiplier = 3f;
			}

			Score += (int)((Count * 10) * multiplier);
			Count = 0;

			return Score;
		}

		public void SetScoreToZero()
		{
			Score = 0;
		}
	}
}
