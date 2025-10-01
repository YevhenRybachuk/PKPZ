using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LabRemoveDuplicateLetters_English
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
        private Button processButton;
        private Label resultLabel;
        private TextBox outputTextBox;

        public MainForm()
        {
            Text = "Remove duplicate letters (English only)";
            Width = 600;
            Height = 400;
            StartPosition = FormStartPosition.CenterScreen;

            inputTextBox = new TextBox
            {
                Multiline = true,
                Width = 540,
                Height = 100,
                Left = 20,
                Top = 20
            };

            processButton = new Button
            {
                Text = "Process text",
                Left = 20,
                Top = inputTextBox.Bottom + 10,
                Width = 150
            };
            processButton.Click += ProcessButton_Click;

            resultLabel = new Label
            {
                Text = "Result:",
                Left = 20,
                Top = processButton.Bottom + 15,
                AutoSize = true
            };

            outputTextBox = new TextBox
            {
                Multiline = true,
                Width = 540,
                Height = 100,
                Left = 20,
                Top = resultLabel.Bottom + 5,
                ReadOnly = true
            };

            Controls.Add(inputTextBox);
            Controls.Add(processButton);
            Controls.Add(resultLabel);
            Controls.Add(outputTextBox);
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            string input = inputTextBox.Text;
            
            string result = Regex.Replace(input, @"[A-Za-z]+", match =>
            {
                string word = match.Value;
                StringBuilder sb = new StringBuilder();
                foreach (char c in word)
                {
                    if (!sb.ToString().Contains(c))
                        sb.Append(c);
                }
                return sb.ToString();
            });

            outputTextBox.Text = result;
        }
    }
}