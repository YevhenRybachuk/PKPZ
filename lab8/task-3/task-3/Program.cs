using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ZodiacQuizApp
{
    public enum Zodiac
    {
        Овен, Телець, Близнюки, Рак, Лев, Діва, Терези,
        Скорпіон, Стрілець, Козеріг, Водолій, Риби
    }

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new QuizForm());
        }
    }

    public class QuizForm : Form
    {
        private Label lblQuestion, lblResult;
        private ComboBox cmbZodiac;
        private Button btnCheck, btnExit;
        private Dictionary<Zodiac, string> zodiacInfo = new();

        public QuizForm()
        {
            this.Text = "Вікторина: Знаки зодіаку";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            zodiacInfo.Add(Zodiac.Овен, "Стихія вогню (активність, починання)");
            zodiacInfo.Add(Zodiac.Телець, "Стихія землі (наполегливість, накопичення)");
            zodiacInfo.Add(Zodiac.Близнюки, "Стихія повітря (рух, комунікація)");
            zodiacInfo.Add(Zodiac.Рак, "Стихія води (система, інтуїція)");
            zodiacInfo.Add(Zodiac.Лев, "Стихія вогню (індивідуалізація, творче самовираження)");
            zodiacInfo.Add(Zodiac.Діва, "Стихія землі (служіння, деталізація)");
            zodiacInfo.Add(Zodiac.Терези, "Стихія повітря (врівноважування, дуальність)");
            zodiacInfo.Add(Zodiac.Скорпіон, "Стихія води (трансформація, інстинктивність)");
            zodiacInfo.Add(Zodiac.Стрілець, "Стихія вогню (світоглядність, духовність)");
            zodiacInfo.Add(Zodiac.Козеріг, "Стихія землі (відповідальність, цілеспрямованість)");
            zodiacInfo.Add(Zodiac.Водолій, "Стихія повітря (незалежність, новаторство)");
            zodiacInfo.Add(Zodiac.Риби, "Стихія води (відречення, глибинність)");

            lblQuestion = new Label()
            {
                Text = "Оберіть свій знак зодіаку:",
                Location = new Point(50, 40),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };

            cmbZodiac = new ComboBox()
            {
                Location = new Point(50, 80),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbZodiac.Items.AddRange(Enum.GetNames(typeof(Zodiac)));

            btnCheck = new Button()
            {
                Text = "Дізнатись стихію",
                Location = new Point(50, 130),
                Width = 150
            };
            btnCheck.Click += BtnCheck_Click;

            lblResult = new Label()
            {
                Text = "",
                Location = new Point(50, 180),
                Size = new Size(500, 100),
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.DarkBlue
            };

            btnExit = new Button()
            {
                Text = "Вихід",
                Location = new Point(50, 300),
                Width = 100
            };
            btnExit.Click += (s, e) => this.Close();

            Controls.AddRange(new Control[] { lblQuestion, cmbZodiac, btnCheck, lblResult, btnExit });
        }

        private void BtnCheck_Click(object? sender, EventArgs e)
        {
            if (cmbZodiac.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, оберіть знак зодіаку!", "Увага");
                return;
            }

            bool running = true;
            while (running)
            {
                string selectedName = cmbZodiac.SelectedItem.ToString();
                if (Enum.TryParse(selectedName, out Zodiac sign))
                {
                    switch (sign)
                    {
                        case Zodiac.Овен:
                        case Zodiac.Телець:
                        case Zodiac.Близнюки:
                        case Zodiac.Рак:
                        case Zodiac.Лев:
                        case Zodiac.Діва:
                        case Zodiac.Терези:
                        case Zodiac.Скорпіон:
                        case Zodiac.Стрілець:
                        case Zodiac.Козеріг:
                        case Zodiac.Водолій:
                        case Zodiac.Риби:
                            lblResult.Text = $"{sign}: {zodiacInfo[sign]}";
                            break;
                        default:
                            lblResult.Text = "Невідомий знак!";
                            break;
                    }
                }

                var res = MessageBox.Show("Бажаєте продовжити?", "Продовжити?", MessageBoxButtons.YesNo);

                if (res == DialogResult.No)
                {
                    Application.Exit();
                    return;
                }
                else
                {
                    running = false;
                }
            }
        }
    }
}
