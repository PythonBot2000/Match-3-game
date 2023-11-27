using Match3.Animation;
using Match3.Animation.DeleteAnimation;
using Match3.Animation.MoveAnimation;
using Match3.Elements;
using Match3.Elements.DeleteAlgorithm;
using Match3.ElementsFactories;
using Match3.Models;

namespace Match3
{
    internal class PlayingField
    {
        const int ElementSize = 50;

        public int Rows { get; }
        public int Columns { get; }
        public List<List<Element>> Field { get; }
        public MainForm BoundForm { get; }
        public PointCounter pointCounter { get; }

        Random random = new Random();

        IMoveAnimation moveAnimation;
        IDeleteAnimation deleteAnimation;

        bool cantCreateExtraElements;

		private Color IntToColor(int colorNum) => colorNum switch
        {
            0 => Color.Orange,
            1 => Color.Red,
            2 => Color.Blue,
            3 => Color.Green,
            4 => Color.Yellow,
            _ => Color.White
        };

        public PlayingField(MainForm form, PointCounter pointCounter, int width = 8, int height = 8)
        {
            BoundForm = form;
            this.pointCounter = pointCounter;
            Rows = height;
            Columns = width;

            moveAnimation = new NoMoveAnimation(this);
			deleteAnimation = new NoDeleteAnimation(this);

			cantCreateExtraElements = true;

			Field = new List<List<Element>>();
			ElementFactory elementFactory = new DefaultElementFactory(this);

			for (int i = 0; i < Rows; i++)
            {
                List<Element> row = new List<Element>(); 

				for (int j = 0; j < Columns; j++)
                {
                    Element element = elementFactory.Create(new Point(i, j));
                    element.button.Tag = element;

                    row.Add(element);
                    BoundForm.BindButton(element.button);
                }
                Field.Add(row);
            }
            UpdateWhileHaveMatches();

			moveAnimation = new DefaultMoveAnimation(this, 10, 1);
			deleteAnimation = new DefaultDeleteAnimation(this, 8, 1);

			cantCreateExtraElements = false;
		}

        public bool TryToSwap(Element firstElement, Element secondElement)
        {
            Point firstPos = firstElement.PosOnField;
            Point secondPos = secondElement.PosOnField;

            if (Math.Abs(firstPos.X - secondPos.X) <= 1 && Math.Abs(firstPos.Y - secondPos.Y) == 0 ||
                Math.Abs(firstPos.X - secondPos.X) == 0 && Math.Abs(firstPos.Y - secondPos.Y) <= 1)
            {
                Swap(firstElement, secondElement);

                bool isMatchWithFirst = CheckThreeInRow(firstElement);
				bool isMatchWithSecond = CheckThreeInRow(firstElement);

				if (isMatchWithFirst || isMatchWithSecond)
                {
					UpdateWhileHaveMatches();
                    return true;
                }
                else
                {
                    Swap(firstElement, secondElement);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void Swap(Element firstElement, Element secondElement)
        {
            List<ElementToMove> listForMove = new List<ElementToMove>
            {
                new ElementToMove(firstElement, secondElement.PosOnField),
                new ElementToMove(secondElement, firstElement.PosOnField)
            };

            moveAnimation.Anime(listForMove);
        }

        private bool CheckThreeInRow(Element element)
        {
            Point point = element.PosOnField;

            List<Element> verticalList = Field[point.X];
            List<Element> horizontalList = Field.SelectMany(list =>
                    list.Where(pointTemp => pointTemp.PosOnField.Y == point.Y)).ToList();

            int horRightCount = 0;
            for (int i = point.X + 1; i < horizontalList.Count; i++)
            {
                if (element.Color != horizontalList[i].Color) i = horizontalList.Count;
                else horRightCount++;
            }

            int horLeftCount = 0;
            for (int i = point.X - 1; i >= 0; i--)
            {
                if (element.Color != horizontalList[i].Color) i = 0;
                else horLeftCount++;
            }

            int verDownCount = 0;
            for (int i = point.Y + 1; i < verticalList.Count; i++)
            {
                if (element.Color != verticalList[i].Color) i = verticalList.Count;
                else verDownCount++;
            }

            int verUpCount = 0;
            for (int i = point.Y - 1; i >= 0; i--)
            {
                if (element.Color != verticalList[i].Color) i = 0;
                else verUpCount++;
            }

            if (horRightCount + horLeftCount + 1 >= 3 || verDownCount + verUpCount + 1 >= 3)
            {
                if (horRightCount + horLeftCount + 1 >= 3)
                {
                    for (int i = point.X - horLeftCount; i <= point.X + horRightCount; i++)
                    {
                        MarkForDelete(horizontalList[i].Destroy(), horizontalList[i].DeleteAlgorithm);
                    }
                }
                if (verDownCount + verUpCount + 1 >= 3)
                {
                    for (int i = point.Y - verUpCount; i <= point.Y + verDownCount; i++)
                    {
                        MarkForDelete(verticalList[i].Destroy(), verticalList[i].DeleteAlgorithm);
                    }
                }

				return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateWhileHaveMatches()
        {
            bool haveSomethingToChange = true;
            while (haveSomethingToChange)
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        CheckThreeInRow(Field[i][j]);
                    }
                }
                haveSomethingToChange = UpdateField();
            }

        }

        private bool UpdateField()
        {
            bool isChanged = false;
            List<Element> elementsOnDelete = new List<Element>();
            Field.ForEach(x => x.ForEach(element =>
            {
                if (!element.isExist)
                {
                    pointCounter.Count++;
                    elementsOnDelete.Add(element);
                    isChanged = true;
                }
            }));

            if (elementsOnDelete.Count != 0)
            {
                deleteAnimation.DeleteAnime(elementsOnDelete);

				for (int i = 0; i < elementsOnDelete.Count; i++)
				{
					BoundForm.Controls.Remove(elementsOnDelete[i].button);
					elementsOnDelete[i].button.Dispose();
				}
			}

            BoundForm.UpdateScore();
            RelocateAndCreateNewElements();

            return isChanged;
        }

        private void RelocateAndCreateNewElements()
        {
            List<ElementToMove> listForMove = new List<ElementToMove>();
            List<int> listToCreate = new List<int>();

            for (int i = 0; i < Rows; i++)
            {
                int deletedElementsCount = 0;
                for (int j = Columns - 1; j >= 0; j--)
                {
                    if (!Field[i][j].isExist)
                    {
                        deletedElementsCount++;
                    }
                    else if (deletedElementsCount != 0)
                    {
                        Point point = new Point(Field[i][j].PosOnField.X, Field[i][j].PosOnField.Y + deletedElementsCount);
                        listForMove.Add(new ElementToMove(Field[i][j], point));
                    }

                    if (j == 0)
                    {
                        listToCreate.Add(deletedElementsCount);
                    }
                }
            }

            for (int i = 0; i < listToCreate.Count; i++)
            {
                for (int j = listToCreate[i] - 1; j >= 0; j--)
                {
                    ElementFactory factory;
                    if (cantCreateExtraElements || random.Next(100) < 95)
                    {
						factory = new DefaultElementFactory(this);
                        Field[i][j] = factory.Create(new Point(i, j));
                    }
                    else
                    {
                        if (random.Next(100) < 50)
						{
							factory = new BombElementFactory(this);
							Field[i][j] = factory.Create(new Point(i, j));
                        }
                        else
                        {
                            if (random.Next(100) < 50)
							{
								factory = new LineElementFactory(this, true);
								Field[i][j] = factory.Create(new Point(i, j));
							}
                            else
							{
								factory = new LineElementFactory(this, false);
								Field[i][j] = factory.Create(new Point(i, j));
							}
								
                        }
                    }
                    Field[i][j].button.Tag = Field[i][j];

                    Field[i][j].PosOnWindow = new Point(Field[i][j].PosOnWindow.X, Field[i][j].PosOnWindow.Y - 50 * listToCreate[i]);
                    listForMove.Add(new ElementToMove(Field[i][j], Field[i][j].PosOnField));

                    BoundForm.BindButton(Field[i][j].button);
                }
            }
            if (listForMove.Count != 0) moveAnimation.Anime(listForMove);
        }

        public void SetElement(Element element, Point point)
        {
            if (ValidatePoint(point))
            {
                Field[point.X][point.Y] = element;
            }
        }

        private void MarkForDelete(List<Point>? list, IDeleteAlgorithm? deleteAlgorithm)
        {
            if (list == null) return;

            foreach (Point point in list)
			{
				Field[point.X][point.Y].DeleteAlgorithm = deleteAlgorithm;
				if (ValidatePoint(point) && Field[point.X][point.Y].isExist)
                {
					MarkForDelete(Field[point.X][point.Y].Destroy(), Field[point.X][point.Y].DeleteAlgorithm);
				}
            }
        }

        private bool ValidatePoint(Point point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < Rows && point.Y < Columns;
        }
    }
}
