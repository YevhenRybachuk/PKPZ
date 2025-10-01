using System;
using System.Linq;
using System.Windows.Forms;

namespace task_1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize(); 
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        private TextBox inputTextBox;
        private Button countButton;
        private Label resultLabel;
        private Button clearButton;

        public MainForm()
        {
            Text = "Підрахунок символів '@'";
            Width = 500;
            Height = 300;
            StartPosition = FormStartPosition.CenterScreen;

            inputTextBox = new TextBox
            {
                Multiline = true,
                Width = 440,
                Height = 140,
                Left = 20,
                Top = 20,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            countButton = new Button
            {
                Text = "Порахувати @",
                Left = 20,
                Top = inputTextBox.Bottom + 12,
                Width = 120
            };
            countButton.Click += CountButton_Click;

            clearButton = new Button
            {
                Text = "Очистити",
                Left = countButton.Right + 12,
                Top = countButton.Top,
                Width = 100
            };
            clearButton.Click += (s, e) => { inputTextBox.Clear(); resultLabel.Text = ""; };

            resultLabel = new Label
            {
                Left = clearButton.Right + 12,
                Top = countButton.Top + 6,
                AutoSize = true
            };

            Controls.Add(inputTextBox);
            Controls.Add(countButton);
            Controls.Add(clearButton);
            Controls.Add(resultLabel);
        }

        private void CountButton_Click(object sender, EventArgs e)
        {
            string text = inputTextBox.Text ?? string.Empty;
            int count = text.Count(ch => ch == '@');
            resultLabel.Text = $"Знайдено символів '@': {count}";
        }
    }
}

