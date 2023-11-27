using Match3.Elements.DeleteAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Match3.Elements
{
    internal abstract class Element
    {
        protected PlayingField PlayingField { get; set; }
        private Point _positionOnField;
		public bool isExist;
        public IDeleteAlgorithm? DeleteAlgorithm { get; set; } = null;

		public Button button { get; set; }

		public Point PosOnField
        {
            get { return _positionOnField; }
            set
            {
                _positionOnField = value;
                UpdatePositionOnWindow();
            }
        }

		public Point PosOnWindow
		{
			get { return button.Location; }
			set { button.Location = value; }
		}

		public Color Color
        {
            get { return button.BackColor; }
            set { button.BackColor = value; }
        }

        public void UpdatePositionOnWindow()
        {
			PosOnWindow = PosOnFieldToWindow(_positionOnField);
        }

		public Element(PlayingField PlayingField, Button Button, Point PositionOnField)
        {
			isExist = true;
            this.PlayingField = PlayingField;
			button = Button;
            PosOnField = PositionOnField;
            this.DeleteAlgorithm = DeleteAlgorithm;
        }

        public static Point PosOnFieldToWindow(Point point)
        {
            return new Point(point.X * 50, point.Y * 50);
		}

		public void DeleteAnimation(int step, int stepCount)
        {
            if (DeleteAlgorithm != null)
            {
                Color = DeleteAlgorithm.Delete(step, stepCount);
			}
        }

		protected bool ValidatePoint(Point point)
		{
			return point.X >= 0 && point.Y >= 0 && point.X < PlayingField.Rows && point.Y < PlayingField.Columns;
		}

		public abstract List<Point>? Destroy();


	}
}
