using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab1_2
{
    interface IPerson
    {
        void InputData(string name, int age, string extra, int count);
        string GetInfo();
    }

    interface ICompany
    {
        void Work();
        void Report();
    }

    class Lawyer : IPerson, ICompany
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string LawField { get; set; }
        public int CasesWon { get; set; }
        public void InputData(string name, int age, string lawField, int casesWon)
        {
            Name = name;
            Age = age;
            LawField = lawField;
            CasesWon = casesWon;
        }

        public string GetInfo()
        {
            return $"Lawyer: {Name}, Age: {Age}, Field: {LawField}, Cases won: {CasesWon}";
        }

        public void Work()
        {
            MessageBox.Show($"{Name} is preparing legal documents.");
        }

        public void Report()
        {
            MessageBox.Show($"{Name} has won {CasesWon} cases.");
        }

        public string DefendClient()
        {
            return $"{Name} is defending a client.";
        }

        public string StudyLaw()
        {
            return $"{Name} is studying.";
        }
    }

    class Engineer : IPerson, ICompany
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Specialty { get; set; }
        public int Projects { get; set; }
        public void InputData(string name, int age, string specialty, int projects)
        {
            Name = name;
            Age = age;
            Specialty = specialty;
            Projects = projects;
        }

        public string GetInfo()
        {
            return $"Engineer: {Name}, Age: {Age}, Specialty: {Specialty}, Projects: {Projects}";
        }

        public void Work()
        {
            MessageBox.Show($"{Name} is designing a new system.");
        }

        public void Report()
        {
            MessageBox.Show($"{Name} completed {Projects} projects.");
        }

        public string Design()
        {
            return $"{Name} is designing a building.";
        }

        public string TestPrototype()
        {
            return $"{Name} is testing a prototype.";
        }
    }

    class MainForm : Form
    {
        TextBox txtName, txtAge, txtExtra, txtCount;
        Button btnLawyer, btnEngineer, btnAction1, btnAction2;
        ListBox list;
        Label lbl1, lbl2, lbl3, lbl4;

        List<object> persons = new List<object>();

        public MainForm()
        {
            this.Text = "Persons and Companies";
            this.Width = 450;
            this.Height = 500;

            lbl1 = new Label() { Text = "Name:", Left = 20, Top = 20 };
            lbl2 = new Label() { Text = "Age:", Left = 20, Top = 60 };
            lbl3 = new Label() { Text = "Field / Specialty:", Left = 20, Top = 100 };
            lbl4 = new Label() { Text = "Cases / Projects:", Left = 20, Top = 140 };

            txtName = new TextBox() { Left = 140, Top = 20, Width = 200 };
            txtAge = new TextBox() { Left = 140, Top = 60, Width = 200 };
            txtExtra = new TextBox() { Left = 140, Top = 100, Width = 200 };
            txtCount = new TextBox() { Left = 140, Top = 140, Width = 200 };

            btnLawyer = new Button() { Text = "Add Lawyer", Left = 50, Top = 190, Width = 150 };
            btnEngineer = new Button() { Text = "Add Engineer", Left = 230, Top = 190, Width = 150 };

            list = new ListBox() { Left = 20, Top = 240, Width = 390, Height = 150 };

            btnAction1 = new Button() { Text = "Action 1", Left = 50, Top = 410, Width = 150 };
            btnAction2 = new Button() { Text = "Action 2", Left = 230, Top = 410, Width = 150 };

            btnLawyer.Click += BtnLawyer_Click;
            btnEngineer.Click += BtnEngineer_Click;
            btnAction1.Click += BtnAction1_Click;
            btnAction2.Click += BtnAction2_Click;

            this.Controls.Add(lbl1);
            this.Controls.Add(lbl2);
            this.Controls.Add(lbl3);
            this.Controls.Add(lbl4);
            this.Controls.Add(txtName);
            this.Controls.Add(txtAge);
            this.Controls.Add(txtExtra);
            this.Controls.Add(txtCount);
            this.Controls.Add(btnLawyer);
            this.Controls.Add(btnEngineer);
            this.Controls.Add(list);
            this.Controls.Add(btnAction1);
            this.Controls.Add(btnAction2);
        }

        private void BtnLawyer_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAge.Text, out int age) || !int.TryParse(txtCount.Text, out int casesWon))
            {
                MessageBox.Show("Enter valid numbers for Age and Cases!");
                return;
            }

            Lawyer l = new Lawyer();
            l.InputData(txtName.Text, age, txtExtra.Text, casesWon);

            persons.Add(l);
            list.Items.Add(l.GetInfo());
            ClearFields();
        }

        private void BtnEngineer_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAge.Text, out int age) || !int.TryParse(txtCount.Text, out int projects))
            {
                MessageBox.Show("Enter valid numbers for Age and Projects!");
                return;
            }

            Engineer eng = new Engineer();
            eng.InputData(txtName.Text, age, txtExtra.Text, projects);

            persons.Add(eng);
            list.Items.Add(eng.GetInfo());
            ClearFields();
        }

        private void BtnAction1_Click(object sender, EventArgs e)
        {
            if (list.SelectedIndex == -1)
            {
                MessageBox.Show("Select a person from the list.");
                return;
            }

            var obj = persons[list.SelectedIndex];
            string msg = "";

            if (obj is Lawyer lawyer)
                msg = lawyer.DefendClient();
            else if (obj is Engineer engineer)
                msg = engineer.Design();

            MessageBox.Show(msg);
        }

        private void BtnAction2_Click(object sender, EventArgs e)
        {
            if (list.SelectedIndex == -1)
            {
                MessageBox.Show("Select a person from the list.");
                return;
            }

            var obj = persons[list.SelectedIndex];
            string msg = "";

            if (obj is Lawyer lawyer)
                msg = lawyer.StudyLaw();
            else if (obj is Engineer engineer)
                msg = engineer.TestPrototype();

            MessageBox.Show(msg);
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtExtra.Text = "";
            txtCount.Text = "";
        }
    }

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
