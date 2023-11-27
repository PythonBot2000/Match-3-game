using Match3.Elements;
using Match3.Elements.DeleteAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.ElementsFactories
{
	internal class BombElementFactory : ElementFactory
	{
		public BombElementFactory(PlayingField field) : base(field) { }
		public override Element Create(Point point)
		{
			Element element = new BombElement(
							Field,
							new Button
							{
								Size = new Size(ElementSize, ElementSize),
								TextAlign = ContentAlignment.MiddleCenter,
								BackColor = IntToColor(random.Next(5)),
							},
							point
						);
			element.DeleteAlgorithm = new DefaultDeleteAlgorithm(element.Color, Color.OrangeRed);
			return element;
		}
	}
}
