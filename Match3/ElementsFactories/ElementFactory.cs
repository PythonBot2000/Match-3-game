using Match3.Elements;

namespace Match3.ElementsFactories
{
	internal abstract class ElementFactory
	{
		protected const int ElementSize = 50;
		protected PlayingField Field { get; set; }
		protected Color IntToColor(int colorNum) => colorNum switch
		{
			0 => Color.Orange,
			1 => Color.Red,
			2 => Color.Blue,
			3 => Color.Green,
			4 => Color.Yellow,
			_ => Color.White
		};
		protected Random random = new Random();
		protected ElementFactory(PlayingField field) 
		{
			Field = field;
		}

		public abstract Element Create(Point point);
	}
}
