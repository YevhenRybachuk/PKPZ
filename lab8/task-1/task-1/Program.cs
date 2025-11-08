using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BusParkApp
{
    internal static class Program
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
        private List<(string Name, int Year, string RegNumber, int Seats)> buses 
            = new List<(string, int, string, int)>();

        TextBox txtName, txtYear, txtRegNumber, txtSeats;
        Button btnAdd, btnShowAll, btnShowEligible, btnShowOne;
        ListBox listBoxBuses;

        public MainForm()
        {
            this.Text = "Автобусний парк";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblName = new Label() { Text = "Назва:", Location = new Point(20, 20) };
            txtName = new TextBox() { Location = new Point(150, 20), Width = 150 };

            Label lblYear = new Label() { Text = "Рік випуску:", Location = new Point(20, 60) };
            txtYear = new TextBox() { Location = new Point(150, 60), Width = 150 };

            Label lblReg = new Label() { Text = "Номер:", Location = new Point(20, 100) };
            txtRegNumber = new TextBox() { Location = new Point(150, 100), Width = 150 };

            Label lblSeats = new Label() { Text = "Кількість місць:", Location = new Point(20, 140) };
            txtSeats = new TextBox() { Location = new Point(150, 140), Width = 150 };

            btnAdd = new Button() { Text = "Додати автобус", Location = new Point(20, 180), Width = 150 };
            btnShowAll = new Button() { Text = "Показати всі", Location = new Point(200, 180), Width = 150 };
            btnShowEligible = new Button() { Text = "Міжобласні", Location = new Point(380, 180), Width = 150 };
            btnShowOne = new Button() { Text = "Деталі обраного", Location = new Point(380, 380), Width = 150 };

            listBoxBuses = new ListBox() { Location = new Point(20, 230), Size = new Size(350, 200) };

            btnAdd.Click += BtnAdd_Click;
            btnShowAll.Click += BtnShowAll_Click;
            btnShowEligible.Click += BtnShowEligible_Click;
            btnShowOne.Click += BtnShowOne_Click;

            Controls.AddRange(new Control[] {
                lblName, txtName,
                lblYear, txtYear,
                lblReg, txtRegNumber,
                lblSeats, txtSeats,
                btnAdd, btnShowAll, btnShowEligible, btnShowOne,
                listBoxBuses
            });
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                int year = int.Parse(txtYear.Text);
                string regNumber = txtRegNumber.Text;
                int seats = int.Parse(txtSeats.Text);

                var bus = (name, year, regNumber, seats);
                buses.Add(bus);

                listBoxBuses.Items.Add($"{bus.name} ({bus.year}) [{bus.regNumber}] {bus.seats} місць");

                txtName.Clear();
                txtYear.Clear();
                txtRegNumber.Clear();
                txtSeats.Clear();
            }
            catch
            {
                MessageBox.Show("Будь ласка, введіть правильні дані!");
            }
        }

        private void BtnShowAll_Click(object? sender, EventArgs e)
        {
            listBoxBuses.Items.Clear();
            foreach (var bus in buses)
            {
                listBoxBuses.Items.Add($"{bus.Name} ({bus.Year}) [{bus.RegNumber}] {bus.Seats} місць");
            }
        }

        private void BtnShowEligible_Click(object? sender, EventArgs e)
        {
            int currentYear = DateTime.Now.Year;
            int count = 0;

            foreach (var bus in buses)
            {
                if (currentYear - bus.Year <= 10)
                    count++;
            }

            MessageBox.Show($"Кількість автобусів, які можуть виконувати міжобласні рейси: {count}");
        }

        private void BtnShowOne_Click(object? sender, EventArgs e)
        {
            if (listBoxBuses.SelectedIndex >= 0)
            {
                var bus = buses[listBoxBuses.SelectedIndex];
                MessageBox.Show($"Назва: {bus.Name}\nРік: {bus.Year}\nНомер: {bus.RegNumber}\nМісць: {bus.Seats}",
                                "Інформація про автобус");
            }
            else
            {
                MessageBox.Show("Оберіть автобус зі списку!");
            }
        }
    }
}
