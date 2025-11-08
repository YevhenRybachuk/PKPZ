using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows.Forms;

namespace PoliklinikaApp
{
    public struct POLI
    {
        public string CABINET_NAME;
        public string CABINET_NUM;
        public string DOCTOR;
        public int DAY; 
        public TimeSpan START;
        public TimeSpan END;
    }

    public class MainForm : Form
    {
        TextBox txtCabName, txtCabNum, txtDoctor, txtStart, txtEnd, txtCheckTime;
        NumericUpDown nudDay, nudCheckDay;
        Button btnAdd, btnShowAll, btnCheckFluoro, btnFridayAfternoon, btnTherapistLastToday, btnTomorrowShift;
        ListBox listBoxOutput;
        List<POLI> records = new List<POLI>();

        public MainForm()
        {
            Text = "Поліклініка — Обробка записів";
            Size = new Size(820, 700);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10);
            BackColor = Color.WhiteSmoke;

            int x = 12, y = 12;
            int inputX = 180, gapY = 36;

            Controls.Add(new Label { Text = "Назва кабінету:", Location = new Point(20, y), AutoSize = true });
            txtCabName = new TextBox { Location = new Point(200, y), Width = 200 };
            Controls.Add(txtCabName);
            y += 35;

            Controls.Add(new Label { Text = "Номер кабінету:", Location = new Point(20, y), AutoSize = true });
            txtCabNum = new TextBox { Location = new Point(200, y), Width = 100 };
            Controls.Add(txtCabNum);
            y += 35;

            Controls.Add(new Label { Text = "ПІБ лікаря:", Location = new Point(20, y), AutoSize = true });
            txtDoctor = new TextBox { Location = new Point(200, y), Width = 200 };
            Controls.Add(txtDoctor);
            y += 35;

            Controls.Add(new Label { Text = "День прийому (1–7):", Location = new Point(20, y), AutoSize = true });
            nudDay = new NumericUpDown
                { Location = new Point(200, y), Minimum = 1, Maximum = 7, Width = 60, Value = 1 };
            Controls.Add(nudDay);
            y += 35;

            Controls.Add(new Label { Text = "Час початку (ГГ:ХХ:СС):", Location = new Point(20, y), AutoSize = true });
            txtStart = new TextBox { Location = new Point(200, y), Width = 100, Text = "09:00:00" };
            Controls.Add(txtStart);
            y += 35;

            Controls.Add(
                new Label { Text = "Час закінчення (ГГ:ХХ:СС):", Location = new Point(20, y), AutoSize = true });
            txtEnd = new TextBox { Location = new Point(200, y), Width = 100, Text = "17:00:00" };
            Controls.Add(txtEnd);
            y += 45;

            btnAdd = new Button { Text = "Додати запис", Location = new Point(inputX, y), Width = 140 };
            btnAdd.Click += BtnAdd_Click;
            Controls.Add(btnAdd);
            btnShowAll = new Button
                { Text = "Показати всі записи", Location = new Point(inputX + 150, y), Width = 170 };
            btnShowAll.Click += BtnShowAll_Click;
            Controls.Add(btnShowAll);
            y += gapY + 4;
            
            Controls.Add(new Label
            {
                Text = "Операції", Location = new Point(x, y), AutoSize = true,
                Font = new Font(Font, FontStyle.Bold)
            });
            y += gapY;

            Controls.Add(new Label { Text = "Перевірити флюорографію:", Location = new Point(x, y), AutoSize = true });
            y += gapY;
            Controls.Add(new Label { Text = "День (1..7):", Location = new Point(x, y), AutoSize = true });
            nudCheckDay = new NumericUpDown
                { Location = new Point(inputX, y), Minimum = 1, Maximum = 7, Width = 70, Value = 1 };
            Controls.Add(nudCheckDay);
            Controls.Add(new Label { Text = "Час (ГГ:ХХ:СС):", Location = new Point(inputX + 90, y), AutoSize = true });
            txtCheckTime = new TextBox { Location = new Point(inputX + 190, y), Width = 120, Text = "10:00:00"};
            btnCheckFluoro = new Button
                { Text = "Перевірити флюорографію", Location = new Point(inputX + 320, y), Width = 200 };
            btnCheckFluoro.Click += BtnCheckFluoro_Click;
            Controls.AddRange(new Control[] { txtCheckTime, btnCheckFluoro });
            y += gapY;

            btnFridayAfternoon = new Button
                { Text = "Лікарі, що починають у п'ятницю після 12:00", Location = new Point(inputX, y), Width = 380 };
            btnFridayAfternoon.Click += BtnFridayAfternoon_Click;
            Controls.Add(btnFridayAfternoon);
            y += gapY;
            btnTherapistLastToday = new Button
                { Text = "Терапевт, що останнім закінчує сьогодні", Location = new Point(inputX, y), Width = 380 };
            btnTherapistLastToday.Click += BtnTherapistLastToday_Click;
            Controls.Add(btnTherapistLastToday);
            y += gapY;
            btnTomorrowShift = new Button
            {
                Text = "Співробітники: починають завтра до 12:00 та закінчують після 12:00",
                Location = new Point(inputX, y), Width = 600
            };
            btnTomorrowShift.Click += BtnTomorrowShift_Click;
            Controls.Add(btnTomorrowShift);
            y += gapY + 4;

            listBoxOutput = new ListBox
                { Location = new Point(12, y), Size = new Size(810, 360), Font = new Font("Consolas", 10) };
            Controls.Add(listBoxOutput);
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParseExact(txtStart.Text.Trim(), "hh\\:mm\\:ss", CultureInfo.InvariantCulture,
                    out TimeSpan start) ||
                !TimeSpan.TryParseExact(txtEnd.Text.Trim(), "hh\\:mm\\:ss", CultureInfo.InvariantCulture,
                    out TimeSpan end))
            {
                MessageBox.Show("Невірний формат часу. Використайте ГГ:ХХ:СС", "Помилка");
                return;
            }

            POLI p = new POLI
            {
                CABINET_NAME = txtCabName.Text.Trim(),
                CABINET_NUM = txtCabNum.Text.Trim(),
                DOCTOR = txtDoctor.Text.Trim(),
                DAY = (int)nudDay.Value,
                START = start,
                END = end
            };
            records.Add(p);
            listBoxOutput.Items.Add(
                $"Додано: {p.CABINET_NAME} | {p.CABINET_NUM} | {p.DOCTOR} | День {p.DAY} | {p.START:hh\\:mm\\:ss}-{p.END:hh\\:mm\\:ss}");
        }
        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            listBoxOutput.Items.Clear();
            if (records.Count == 0)
            {
                listBoxOutput.Items.Add("Немає записів.");
                return;
            }

            foreach (var r in records)
            {
                TimeSpan dur = r.END >= r.START ? r.END - r.START : r.END.Add(TimeSpan.FromHours(24)) - r.START;
                listBoxOutput.Items.Add(
                    $"{r.CABINET_NAME,-28} | {r.CABINET_NUM,-6} | {r.DOCTOR,-25} | День {r.DAY} | {r.START:hh\\:mm\\:ss}-{r.END:hh\\:mm\\:ss} | Тривалість {dur.TotalMinutes} хв");
            }
        }
        private void BtnCheckFluoro_Click(object sender, EventArgs e)
        {
            listBoxOutput.Items.Clear();

            string input = txtCheckTime.Text.Trim();

            if (!TimeSpan.TryParseExact(input, "hh\\:mm\\:ss", CultureInfo.InvariantCulture, out TimeSpan qtime))
            {
                MessageBox.Show("Невірний формат часу. Використайте формат 10:00:00", "Помилка");
                return;
            }

            int qday = (int)nudCheckDay.Value;

            var fluoro = records
                .Where(r => r.DAY == qday && r.CABINET_NAME.IndexOf("флюр", StringComparison.InvariantCultureIgnoreCase) >= 0)
                .ToList();

            if (fluoro.Count == 0)
            {
                listBoxOutput.Items.Add($"Кабінет флюрографії не знайдено в записах на день {qday}.");
                return;
            }

            bool anyOpen = false;

            foreach (var r in fluoro)
            {
                TimeSpan start = r.START;
                TimeSpan end = r.END;

                bool isOpen;

                if (end >= start)
                {
                    isOpen = qtime >= start && qtime <= end;
                }
                else
                {
                    isOpen = qtime >= start || qtime <= end;
                }

                if (isOpen)
                {
                    anyOpen = true;
                    listBoxOutput.Items.Add(
                        $"Флюрографія відкрита: {r.CABINET_NAME} №{r.CABINET_NUM} | Лікар: {r.DOCTOR} | {r.START:hh\\:mm\\:ss}-{r.END:hh\\:mm\\:ss}");
                }
            }

            if (!anyOpen)
                listBoxOutput.Items.Add($"У день {qday} у час {qtime:hh\\:mm\\:ss} кабінет флюрографії НЕ приймає.");
        }
        
        private void BtnFridayAfternoon_Click(object sender, EventArgs e)
        {
            listBoxOutput.Items.Clear();
            var list = records.Where(r => r.DAY == 5 && r.START > TimeSpan.FromHours(12)).ToList();
            listBoxOutput.Items.Add($"Лікарі у п'ятницю після 12:00 ({list.Count}):");
            foreach (var r in list) listBoxOutput.Items.Add($"{r.CABINET_NAME} | {r.DOCTOR} | {r.START:hh\\:mm\\:ss}");
        }
        private void BtnTherapistLastToday_Click(object sender, EventArgs e)
        {
            listBoxOutput.Items.Clear();
            int today = DayOfWeekToInt(DateTime.Now.DayOfWeek);
            var list = records.Where(r =>
                    r.DAY == today && r.CABINET_NAME.IndexOf("терап", StringComparison.InvariantCultureIgnoreCase) >= 0)
                .Select(r => new { r, EndAdjusted = r.END < r.START ? r.END + TimeSpan.FromHours(24) : r.END })
                .OrderByDescending(x => x.EndAdjusted).ToList();
            if (list.Count == 0)
            {
                listBoxOutput.Items.Add("Терапевтів сьогодні немає");
                return;
            }

            var rr = list.First().r;
            listBoxOutput.Items.Add(
                $"Терапевт, що останнім закінчує сьогодні: {rr.CABINET_NAME} | {rr.DOCTOR} | {rr.START:hh\\:mm\\:ss}-{rr.END:hh\\:mm\\:ss}");
        }
        private void BtnTomorrowShift_Click(object sender, EventArgs e)
        {
            listBoxOutput.Items.Clear();
            int tomorrow = Today() == 7 ? 1 : Today() + 1;
            var list = records.Where(r =>
                r.DAY == tomorrow && r.START < TimeSpan.FromHours(12) && r.END > TimeSpan.FromHours(12)).ToList();
            listBoxOutput.Items.Add($"Співробітники завтра: ({list.Count})");
            foreach (var r in list)
                listBoxOutput.Items.Add($"{r.CABINET_NAME} | {r.DOCTOR} | {r.START:hh\\:mm\\:ss}-{r.END:hh\\:mm\\:ss}");
        }

        private int DayOfWeekToInt(DayOfWeek day) => day == DayOfWeek.Sunday ? 7 : (int)day;
        private int Today() => DayOfWeekToInt(DateTime.Now.DayOfWeek);

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}


