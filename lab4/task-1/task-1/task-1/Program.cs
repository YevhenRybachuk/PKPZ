using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PensionersApp
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
        private Button btnLoad;
        private Button btnProcess;
        private Button btnSave;
        private DataGridView dgvAll;
        private DataGridView dgvFiltered;
        private Label lblAll;
        private Label lblFiltered;

        private const int COLS = 17;
        private string[,] dataArray = new string[0, COLS]; 

        public MainForm()
        {
            Text = "Pensioners — >5 years on pension";
            Width = 1100;
            Height = 700;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            btnLoad = new Button { Text = "Load Input File", Left = 10, Top = 10, Width = 140 };
            btnProcess = new Button { Text = "Process", Left = 160, Top = 10, Width = 100 };
            btnSave = new Button { Text = "Save Output File", Left = 270, Top = 10, Width = 140 };

            btnLoad.Click += BtnLoad_Click;
            btnProcess.Click += BtnProcess_Click;
            btnSave.Click += BtnSave_Click;

            lblAll = new Label { Text = "All records (loaded):", Left = 10, Top = 50, Width = 300 };
            dgvAll = new DataGridView { Left = 10, Top = 70, Width = 1060, Height = 260, ReadOnly = true, AllowUserToAddRows = false };

            lblFiltered = new Label { Text = "Pensioners > 5 years on pension:", Left = 10, Top = 350, Width = 400 };
            dgvFiltered = new DataGridView { Left = 10, Top = 370, Width = 1060, Height = 260, ReadOnly = true, AllowUserToAddRows = false };

            Controls.Add(btnLoad);
            Controls.Add(btnProcess);
            Controls.Add(btnSave);
            Controls.Add(lblAll);
            Controls.Add(dgvAll);
            Controls.Add(lblFiltered);
            Controls.Add(dgvFiltered);
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text files|*.txt;*.csv|All files|*.*";
                ofd.Title = "Select Input Data file";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var lines = File.ReadAllLines(ofd.FileName)
                                        .Where(l => !string.IsNullOrWhiteSpace(l))
                                        .Select(l => l.Trim())
                                        .ToArray();

                        int rows = lines.Length;
                        dataArray = new string[rows, COLS];

                        for (int i = 0; i < rows; i++)
                        {
                            var parts = lines[i].Split(';').Select(p => p.Trim()).ToArray();
                            if (parts.Length != COLS)
                            {
                                MessageBox.Show(
                                    $"Warning: line {i+1} has {parts.Length} fields (expected {COLS}). Line will be padded/trimmed.",
                                    "Format warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            for (int j = 0; j < COLS; j++)
                            {
                                if (j < parts.Length) dataArray[i, j] = parts[j];
                                else dataArray[i, j] = string.Empty;
                            }
                        }

                        PopulateDataGridFromArray(dgvAll, dataArray);
                        dgvFiltered.DataSource = null; 
                        MessageBox.Show("File loaded successfully.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading file: {ex.Message}");
                    }
                }
            }
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            if (dataArray.Length == 0)
            {
                MessageBox.Show("No data loaded. Please load an input file first.");
                return;
            }

            var matches = new System.Collections.Generic.List<string[]>();

            DateTime today = DateTime.Now.Date;

            int rows = dataArray.GetLength(0);

            for (int i = 0; i < rows; i++)
            {
                try
                {
                    string gender = dataArray[i, 3]?.Trim() ?? "";
                    string byearS = dataArray[i, 5];
                    string bmonthS = dataArray[i, 6];
                    string bdayS = dataArray[i, 7];

                    if (!int.TryParse(byearS, out int byear) ||
                        !int.TryParse(bmonthS, out int bmonth) ||
                        !int.TryParse(bdayS, out int bday))
                    {
                        continue;
                    }

                    DateTime birth = new DateTime(byear, bmonth, bday);

                    int retirementAge = 60; 
                    if (!string.IsNullOrEmpty(gender))
                    {
                        var g = gender.ToLower();
                        if (g.StartsWith("f") || g.StartsWith("w")) retirementAge = 55; 
                    }

                    DateTime retirementDate = birth.AddYears(retirementAge);
                    int yearsSinceRetirement = today.Year - retirementDate.Year;
                    if (retirementDate > today.AddYears(-yearsSinceRetirement)) yearsSinceRetirement--;

                    if (yearsSinceRetirement > 5)
                    {
                        var row = new string[COLS];
                        for (int c = 0; c < COLS; c++) row[c] = dataArray[i, c];
                        matches.Add(row);
                    }
                }
                catch
                {
                    continue;
                }
            }

            if (matches.Count == 0)
            {
                MessageBox.Show("No pensioners found who have been on pension > 5 years.", "Result");
                dgvFiltered.DataSource = null;
                return;
            }

            var dt = new System.Data.DataTable();
            string[] headers = new string[] {
                "Surname","Name","Patronymic","Gender","Nationality",
                "BirthYear","BirthMonth","BirthDay","Phone","PostalCode",
                "Country","Region","District","City","Street","House","Apartment"
            };
            foreach (var h in headers) dt.Columns.Add(h);

            foreach (var r in matches)
            {
                var newRow = dt.NewRow();
                for (int j = 0; j < COLS; j++) newRow[j] = r[j] ?? "";
                dt.Rows.Add(newRow);
            }

            dgvFiltered.DataSource = dt;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (dgvFiltered.DataSource == null)
            {
                MessageBox.Show("No filtered data to save. Process data first.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text files|*.txt|All files|*.*";
                sfd.FileName = "Output Data.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Write rows from dgvFiltered
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            // header optional
                            int cols = dgvFiltered.Columns.Count;
                            for (int r = 0; r < dgvFiltered.Rows.Count; r++)
                            {
                                var cells = new string[cols];
                                for (int c = 0; c < cols; c++)
                                {
                                    var val = dgvFiltered.Rows[r].Cells[c].Value;
                                    cells[c] = val?.ToString() ?? "";
                                }
                                sw.WriteLine(string.Join(";", cells));
                            }
                        }
                        MessageBox.Show("Output file saved.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}");
                    }
                }
            }
        }

        private void PopulateDataGridFromArray(DataGridView dgv, string[,] arr)
        {
            var dt = new System.Data.DataTable();
            string[] headers = new string[] {
                "Surname","Name","Patronymic","Gender","Nationality",
                "BirthYear","BirthMonth","BirthDay","Phone","PostalCode",
                "Country","Region","District","City","Street","House","Apartment"
            };
            foreach (var h in headers) dt.Columns.Add(h);

            int rows = arr.GetLength(0);
            for (int i = 0; i < rows; i++)
            {
                var row = dt.NewRow();
                for (int j = 0; j < COLS; j++) row[j] = arr[i, j] ?? "";
                dt.Rows.Add(row);
            }
            dgv.DataSource = dt;
        }
    }
}
