using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ksp_upgrade
{
    public partial class Form1 : Form
    {
        private Label playerChoiceLabel, computerChoiceLabel, resultLabel, scoreLabel;
        private int wins = 0, totalGames = 0;
        private Button ssButton, resetButton, backToScoreboardButton, rockButton, paperButton, scissorsButton;
        private PictureBox playerChoicePictureBox;
        private const string ScoreFilePath = "scores.txt";

        public Form1()
        {
            InitializeComponent();
            LoadScoresAndAdjustListView();
        }

        private void SaveScore()
        {
            string score = $"{DateTime.Now:HH:mm:ss},{wins}/{totalGames}";
            File.AppendAllText(ScoreFilePath, score + Environment.NewLine);
            ResetGameStats();
        }

        private void ResetGameStats()
        {
            wins = 0;
            totalGames = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ToggleStartScreen(false);
            ShowGameplayInterface();
        }

        private void ToggleStartScreen(bool show)
        {
            btnStart.Visible = show;
            scoreboardListView.Visible = show;
        }

        private void CreateButton(ref Button button, string text, Point location, EventHandler onClick)
        {
            button = new Button
            {
                Text = text,
                Size = new Size(75, 30),
                Location = location,
                ForeColor = Color.White,
                BackColor = Color.Black
            };
            button.Click += onClick;
            Controls.Add(button);
        }


        private void CreateLabel(ref Label label, string text, Point location)
        {
            label = new Label { Text = text, Location = location, Size = new Size(ClientSize.Width - 130, 30) };
            Controls.Add(label);
        }

        private void CreatePictureBox(ref PictureBox pictureBox, Point location, Size size)
        {
            pictureBox = new PictureBox
            {
                Location = location,
                Size = size,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Controls.Add(pictureBox);
            pictureBox.BringToFront();
        }

        private void ShowGameplayInterface()
        {
            int startX = 65, startY = 160, spacing = 10;

            CreateButton(ref ssButton, "Shrani rezultat", new Point(startX + 85, 40), ssButton_Click);
            CreateButton(ref resetButton, "Ponastavi rezultate", new Point(startX + 85, 80), resetButton_Click);
            CreateButton(ref backToScoreboardButton, "Nazaj na lestvico", new Point(startX, startY + 190), BackToScoreboardButton_Click);
            CreateButton(ref rockButton, "Kamen", new Point(startX, startY), (s, e) => DetermineRoundWinner("Kamen"));
            CreateButton(ref paperButton, "Papir", new Point(startX + 85, startY), (s, e) => DetermineRoundWinner("Papir"));
            CreateButton(ref scissorsButton, "Škarje", new Point(startX + 170, startY), (s, e) => DetermineRoundWinner("Škarje"));

            CreateLabel(ref playerChoiceLabel, "Igralèeva izbira: ", new Point(startX, startY + 40));
            CreateLabel(ref computerChoiceLabel, "Izbira raèunalnika: ", new Point(startX, startY + 80));
            CreateLabel(ref resultLabel, "Rezultat: ", new Point(startX, startY + 120));
            CreateLabel(ref scoreLabel, "Rezultat: 0/0", new Point(startX, startY + 160));

            
            int pictureBoxWidth = 150;
            int pictureBoxHeight = 150;
            int pictureBoxX = ClientSize.Width - pictureBoxWidth - 10;
            int pictureBoxY = ClientSize.Height - pictureBoxHeight - 10;

            CreatePictureBox(ref playerChoicePictureBox, new Point(pictureBoxX, pictureBoxY), new Size(pictureBoxWidth, pictureBoxHeight));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BackToScoreboardButton_Click(object sender, EventArgs e)
        {
            ToggleStartScreen(true);
            LoadScoresAndAdjustListView();
            HideGameplayInterface();
            ResetGameStats();
        }

        private void HideGameplayInterface()
        {
            ssButton.Hide();
            resetButton.Hide();
            backToScoreboardButton.Hide();
            rockButton.Hide();
            paperButton.Hide();
            scissorsButton.Hide();
            playerChoiceLabel.Hide();
            computerChoiceLabel.Hide();
            resultLabel.Hide();
            scoreLabel.Hide();
            playerChoicePictureBox.Hide();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            File.WriteAllText(ScoreFilePath, string.Empty);
            scoreboardListView.Items.Clear();
        }

        private void ssButton_Click(object sender, EventArgs e) => SaveScore();

        private void DetermineRoundWinner(string playerChoice)
        {
            string computerChoice = new[] { "Kamen", "Papir", "Škarje" }[new Random().Next(3)];

            playerChoiceLabel.Text = "Igralèeva izbira: " + playerChoice;
            computerChoiceLabel.Text = "Izbira raèunalnika: " + computerChoice;
            resultLabel.Text = GetResult(playerChoice, computerChoice);
            scoreLabel.Text = $"Rezultat: {wins}/{totalGames}";

            ShowPlayerChoiceImage(playerChoice);
        }

        private void ShowPlayerChoiceImage(string playerChoice)
        {
            string imagePath = Path.Combine("slike", playerChoice.ToLower() + ".png");
            if (File.Exists(imagePath))
            {
                playerChoicePictureBox.Image = Image.FromFile(imagePath);
                playerChoicePictureBox.Show();
            }
            else
            {
                playerChoicePictureBox.Hide();
            }
        }

        private string GetResult(string playerChoice, string computerChoice)
        {
            if (playerChoice == computerChoice)
            {
                totalGames++;
                return "Rezultat: Neodloèeno!";
            }
                
            if ((playerChoice == "Kamen" && computerChoice == "Škarje") ||
                (playerChoice == "Papir" && computerChoice == "Kamen") ||
                (playerChoice == "Škarje" && computerChoice == "Papir"))
            {
                wins++;
                totalGames++;
                return "Rezultat: Zmagaš!";
            }
            totalGames++;
            return "Rezultat: Raèunalnik zmaga!";
        }

        private void LoadScoresAndAdjustListView()
        {
            scoreboardListView.Items.Clear();
            if (scoreboardListView.Columns.Count == 0)
            {
                scoreboardListView.Columns.Add("Datum", 85);
                scoreboardListView.Columns.Add("Dosežek", 110);
            }
            if (File.Exists(ScoreFilePath))
            {
                foreach (string line in File.ReadAllLines(ScoreFilePath))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        scoreboardListView.Items.Add(new ListViewItem(parts));
                    }
                }
            }
        }

        private void scoreboardListView_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
