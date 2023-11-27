using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Animation.MoveAnimation
{
    internal class DefaultMoveAnimation : IMoveAnimation
    {
        PlayingField Field;
        MainForm BoundForm;
        public int StepsCount { get; set; }
        public int TimeBetweenSteps { get; set; }

        public DefaultMoveAnimation(PlayingField field, int StepsCount, int TimeBetweenSteps)
        {
            Field = field;
            BoundForm = field.BoundForm;
            this.StepsCount = StepsCount;
            this.TimeBetweenSteps = TimeBetweenSteps;
        }

        public void Anime(List<ElementToMove> list)
        {
            for (int i = 0; i < StepsCount; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    int newX = list[j].Element.PosOnWindow.X + (list[j].TargetPosOnWindow.X - list[j].OriginPosOnWindow.X) / StepsCount;
                    int newY = list[j].Element.PosOnWindow.Y + (list[j].TargetPosOnWindow.Y - list[j].OriginPosOnWindow.Y) / StepsCount;

                    list[j].Element.PosOnWindow = new Point(newX, newY);
                }
                Thread.Sleep(TimeBetweenSteps);
                BoundForm.Refresh();
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Element.PosOnField = list[i].TargetPosOnField;

                Field.SetElement(list[i].Element, list[i].TargetPosOnField);
            }
        }
    }
}
