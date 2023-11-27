using Match3.Elements;
using Match3.Elements.DeleteAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Match3.ElementsFactories
{
	internal class LineElementFactory : ElementFactory
	{
		public bool isHorizontal;

		public LineElementFactory(PlayingField field, bool isHorizontal) : base(field) 
		{
			this.isHorizontal = isHorizontal;
		}

		public override Element Create(Point point)
		{
			Element element = new LineElement(
							Field,
							new Button
							{
								Size = new Size(ElementSize, ElementSize),
								TextAlign = ContentAlignment.MiddleCenter,
								BackColor = IntToColor(random.Next(5)),
							},
							point,
							isHorizontal
						);
			element.DeleteAlgorithm = new DefaultDeleteAlgorithm(element.Color, Color.BlueViolet);
			return element;
		}
	}
}
