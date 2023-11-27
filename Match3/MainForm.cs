using Match3.Animation.DeleteAnimation;
using Match3.Animation.MoveAnimation;
using Match3.Elements;
using Match3.Models;

namespace Match3
{
    public partial class MainForm : Form
	{
		private PlayingField playingField;
		private Button? clickedButton;
		private GroupBox mainArea;
		private GroupBox infoArea;
		private Label timeLabel;
		private Label pointsLabel;
		private int time;
		private PointCounter pointCounter;

		public MainForm()
		{
			InitializeComponent();
			InitializeGame();

			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
		}

		private void InitializeGame()
		{
			ClientSize = new Size(680, 430);

			mainArea = new GroupBox();
			mainArea.Location = new Point(15, 15);
			mainArea.Name = "groupBox";
			mainArea.Size = new Size(400, 400);
			mainArea.TabStop = false;
			Controls.Add(mainArea);

			infoArea = new GroupBox();
			infoArea.Location = new Point(430, 15);
			infoArea.Name = "groupBox";
			infoArea.Size = new Size(230, 200);
			infoArea.TabStop = false;
			infoArea.Text = "Information";
			Controls.Add(infoArea);

			time = 60;

			timeLabel = new Label();
			timeLabel.Location = new Point(15, 50);
			timeLabel.Text = $"Time: {time}";
			infoArea.Controls.Add(timeLabel);

			pointsLabel = new Label();
			pointsLabel.Location = new Point(15, 100);
			pointsLabel.Text = "Points: 0";
			infoArea.Controls.Add(pointsLabel);

			pointCounter = new PointCounter();
			playingField = new PlayingField(this, pointCounter);

			pointCounter.SetScoreToZero();
			UpdateScore();

			timer1.Start();
		}

		private void ElementClick(object sender, EventArgs e)
		{
			if (clickedButton != null)
			{
				Button clickedButtonTemp = (Button)sender;
				if (clickedButton == clickedButtonTemp)
				{
					clickedButton = null;
					return;
				}

				bool isSuccessful = playingField.TryToSwap((Element)clickedButton.Tag, (Element)clickedButtonTemp.Tag);

				clickedButton = null;
			}
			else
			{
				clickedButton = (Button)sender;
			}
		}

		public void BindButton(Button button)
		{
			button.Click += ElementClick;
			mainArea.Controls.Add(button);
		}

		public void UpdateScore()
		{
			pointsLabel.Text = $"Points: {pointCounter.GetScore()}";
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			time--;
			if (time < 0)
			{
				timer1.Stop();
				MessageBox.Show("Время вышло, игра окончена.", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Close();
			}
			timeLabel.Text = $"Time: {time}";
		}
	}
}