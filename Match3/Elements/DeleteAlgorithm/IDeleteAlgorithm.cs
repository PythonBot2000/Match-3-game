using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Elements.DeleteAlgorithm
{
	internal interface IDeleteAlgorithm
	{
		Color Delete(int step, int stepNum);
	}
}
