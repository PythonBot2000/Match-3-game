using Match3.Elements;

namespace Match3.ElementsFactories
{
	internal class DefaultElementFactory : ElementFactory
	{
		public DefaultElementFactory(PlayingField field) : base(field) { }
		public override Element Create(Point point)
		{
			return new DefaultElement(
							Field,
							new Button
							{
								Size = new Size(ElementSize, ElementSize),
								TextAlign = ContentAlignment.MiddleCenter,
								BackColor = IntToColor(random.Next(5)),
							},
							point
						);
		}
	}
}
