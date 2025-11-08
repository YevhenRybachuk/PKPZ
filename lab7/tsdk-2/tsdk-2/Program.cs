using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ZodiacApp
{
    public struct ZNAK
    {
        public string NAME;      
        public string ZODIAC;   
        public int[] BDAY;       
    }

    public class MainForm : Form
    {
        TextBox txtName, txtZodiac, txtDay, txtMonth, txtYear, txtSearch;
        Label lblResult;
        ListBox listBoxAll;
        List<ZNAK> BOOK = new List<ZNAK>();

        public MainForm()
        {
            this.Text = "Облік знаків Зодіаку";
            this.Size = new Size(600, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10);
            this.BackColor = Color.Beige;

            int y = 20;

            Controls.Add(new Label() { Text = "Прізвище, ім’я:", Location = new Point(20, y) });
            txtName = new TextBox() { Location = new Point(180, y), Width = 250 };
            Controls.Add(txtName);
            y += 40;

            Controls.Add(new Label() { Text = "Знак Зодіаку:", Location = new Point(20, y) });
            txtZodiac = new TextBox() { Location = new Point(180, y), Width = 250 };
            Controls.Add(txtZodiac);
            y += 40;

            Controls.Add(new Label() { Text = "Дата нар. (день/місяць/рік):", Location = new Point(20, y) });
            txtDay = new TextBox() { Location = new Point(250, y), Width = 40 };
            txtMonth = new TextBox() { Location = new Point(300, y), Width = 40 };
            txtYear = new TextBox() { Location = new Point(350, y), Width = 80 };
            Controls.AddRange(new Control[] { txtDay, txtMonth, txtYear });
            y += 50;

            var btnAdd = new Button() { Text = "Додати запис", Location = new Point(20, y), Width = 150 };
            btnAdd.Click += BtnAdd_Click;
            Controls.Add(btnAdd);

            var btnSort = new Button() { Text = "Сортувати за датою", Location = new Point(200, y), Width = 180 };
            btnSort.Click += BtnSort_Click;
            Controls.Add(btnSort);

            var btnShowAll = new Button() { Text = "Показати всі записи", Location = new Point(400, y), Width = 160 };
            btnShowAll.Click += BtnShowAll_Click;
            Controls.Add(btnShowAll);
            y += 50;

            Controls.Add(new Label() { Text = "Пошук за прізвищем:", Location = new Point(20, y), AutoSize = true});
            txtSearch = new TextBox() { Location = new Point(220, y), Width = 200 };
            Controls.Add(txtSearch);
            y += 40;

            var btnSearch = new Button() { Text = "Знайти", Location = new Point(20, y), Width = 150 };
            btnSearch.Click += BtnSearch_Click;
            Controls.Add(btnSearch);
            y += 50;

            lblResult = new Label()
            {
                Location = new Point(20, y),
                Size = new Size(530, 80),
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.TopLeft
            };
            Controls.Add(lblResult);
            y += 100;

            listBoxAll = new ListBox()
            {
                Location = new Point(20, y),
                Size = new Size(540, 200),
                Font = new Font("Consolas", 10)
            };
            Controls.Add(listBoxAll);
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var person = new ZNAK
                {
                    NAME = txtName.Text.Trim(),
                    ZODIAC = txtZodiac.Text.Trim(),
                    BDAY = new int[]
                    {
                        int.Parse(txtDay.Text),
                        int.Parse(txtMonth.Text),
                        int.Parse(txtYear.Text)
                    }
                };

                BOOK.Add(person);
                MessageBox.Show("Запис успішно додано!", "Інформація");
                ClearInputs();
            }
            catch
            {
                MessageBox.Show("Помилка введення даних!", "Помилка");
            }
        }
        private void BtnSort_Click(object sender, EventArgs e)
        {
            BOOK = BOOK
                .OrderByDescending(p => new DateTime(p.BDAY[2], p.BDAY[1], p.BDAY[0]))
                .ToList();

            MessageBox.Show("Записи відсортовано за спаданням дат народження!", "OK");
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower();
            var found = BOOK.FirstOrDefault(p => p.NAME.ToLower().Contains(search));

            if (!string.IsNullOrEmpty(found.NAME))
            {
                lblResult.Text = $"Ім'я: {found.NAME}\n" +
                                 $"Знак Зодіаку: {found.ZODIAC}\n" +
                                 $"Дата народження: {found.BDAY[0]}.{found.BDAY[1]}.{found.BDAY[2]}";
            }
            else
            {
                lblResult.Text = "Такої людини не знайдено.";
            }
        }
        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            listBoxAll.Items.Clear();

            if (BOOK.Count == 0)
            {
                listBoxAll.Items.Add("Немає записів.");
                return;
            }

            foreach (var p in BOOK)
            {
                string line = $"{p.NAME,-25} | {p.ZODIAC,-12} | {p.BDAY[0]:00}.{p.BDAY[1]:00}.{p.BDAY[2]}";
                listBoxAll.Items.Add(line);
            }
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtZodiac.Clear();
            txtDay.Clear();
            txtMonth.Clear();
            txtYear.Clear();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
