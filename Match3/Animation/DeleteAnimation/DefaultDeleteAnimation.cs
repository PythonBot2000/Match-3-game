using Match3.Elements;

namespace Match3.Animation.DeleteAnimation
{
    internal class DefaultDeleteAnimation : IDeleteAnimation
    {
        MainForm BoundForm;
        public int StepsCount { get; set; }
        public int TimeBetweenSteps { get; set; }

        public DefaultDeleteAnimation(PlayingField field, int StepsCount, int TimeBetweenSteps)
        {
            BoundForm = field.BoundForm;
            this.StepsCount = StepsCount;
            this.TimeBetweenSteps = TimeBetweenSteps;
        }

        public void DeleteAnime(List<Element> list)
        {
			for (int j = 0; j < list.Count; j++)
			{
				list[j].DeleteAnimation(1, 1);
			}
			Thread.Sleep(20);
			BoundForm.Refresh();

			for (int i = StepsCount; i > 0; i--)
            {
                int alpha = 255 - 255 / i;
                for (int j = 0; j < list.Count; j++)
                {
                    list[j].Color = Color.FromArgb(alpha, list[j].Color);
                }
                Thread.Sleep(TimeBetweenSteps);
                BoundForm.Refresh();
            }
        }
    }
}
