using System;
using System.IO;
using System.Windows.Forms;

namespace PlanetApp
{
    public class Planet
    {
        private string name;
        private double mass;
        private double radius;
        private double distanceFromSun;
        private int numberOfMoons;
        private bool hasRings;
        private string atmosphereType;

        public string Name { get => name; set => name = value; }
        public double Mass { get => mass; set => mass = value; }
        public double Radius { get => radius; set => radius = value; }
        public double DistanceFromSun { get => distanceFromSun; set => distanceFromSun = value; }
        public int NumberOfMoons { get => numberOfMoons; set => numberOfMoons = value; }
        public bool HasRings { get => hasRings; set => hasRings = value; }
        public string AtmosphereType { get => atmosphereType; set => atmosphereType = value; }

        public Planet()
        {
            name = "Unknown";
            mass = 0;
            radius = 0;
            distanceFromSun = 0;
            numberOfMoons = 0;
            hasRings = false;
            atmosphereType = "None";
        }

        public double GetDensity()
        {
            double volume = (4.0 / 3.0) * Math.PI * Math.Pow(radius, 3);
            return mass / volume;
        }

        public bool HasAtmosphere()
        {
            return atmosphereType.ToLower() != "none";
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine($"Name: {name}");
                sw.WriteLine($"Mass: {mass} kg");
                sw.WriteLine($"Radius: {radius} km");
                sw.WriteLine($"Distance from Sun: {distanceFromSun} million km");
                sw.WriteLine($"Moons: {numberOfMoons}");
                sw.WriteLine($"Has Rings: {hasRings}");
                sw.WriteLine($"Atmosphere: {atmosphereType}");
                sw.WriteLine($"Density: {GetDensity():F2}");
            }
        }
    }

    public class PlanetForm : Form
    {
        Planet planet = new Planet();

        TextBox txtName = new TextBox();
        TextBox txtMass = new TextBox();
        TextBox txtRadius = new TextBox();
        TextBox txtDistance = new TextBox();
        TextBox txtMoons = new TextBox();
        CheckBox chkRings = new CheckBox();
        TextBox txtAtmosphere = new TextBox();
        Button btnCreate = new Button();
        Button btnShow = new Button();
        Button btnSave = new Button();
        Label lblInfo = new Label();

        public PlanetForm()
        {
            Text = "Planet Information";
            Width = 400;
            Height = 500;

            Label lbl1 = new Label() { Text = "Name:", Top = 20, Left = 20, Width = 120 };
            txtName.SetBounds(150, 20, 200, 20);

            Label lbl2 = new Label() { Text = "Mass (kg):", Top = 60, Left = 20, Width = 120 };
            txtMass.SetBounds(150, 60, 200, 20);

            Label lbl3 = new Label() { Text = "Radius (km):", Top = 100, Left = 20, Width = 120 };
            txtRadius.SetBounds(150, 100, 200, 20);

            Label lbl4 = new Label() { Text = "Distance from Sun (mln km):", Top = 140, Left = 20, Width = 200 };
            txtDistance.SetBounds(150, 160, 200, 20);

            Label lbl5 = new Label() { Text = "Moons:", Top = 200, Left = 20, Width = 120 };
            txtMoons.SetBounds(150, 200, 200, 20);

            Label lbl6 = new Label() { Text = "Has Rings:", Top = 240, Left = 20, Width = 120 };
            chkRings.SetBounds(150, 240, 20, 20);

            Label lbl7 = new Label() { Text = "Atmosphere:", Top = 280, Left = 20, Width = 120 };
            txtAtmosphere.SetBounds(150, 280, 200, 20);

            btnCreate.Text = "Create Planet";
            btnCreate.SetBounds(20, 320, 100, 30);
            btnCreate.Click += BtnCreate_Click;

            btnShow.Text = "Show Info";
            btnShow.SetBounds(140, 320, 100, 30);
            btnShow.Click += BtnShow_Click;

            btnSave.Text = "Save to File";
            btnSave.SetBounds(260, 320, 100, 30);
            btnSave.Click += BtnSave_Click;

            lblInfo.SetBounds(20, 370, 350, 80);
            lblInfo.AutoSize = false;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;

            Controls.AddRange(new Control[]
            {
                lbl1, txtName, lbl2, txtMass, lbl3, txtRadius,
                lbl4, txtDistance, lbl5, txtMoons, lbl6, chkRings,
                lbl7, txtAtmosphere, btnCreate, btnShow, btnSave, lblInfo
            });
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            planet = new Planet
            {
                Name = txtName.Text,
                Mass = double.Parse(txtMass.Text),
                Radius = double.Parse(txtRadius.Text),
                DistanceFromSun = double.Parse(txtDistance.Text),
                NumberOfMoons = int.Parse(txtMoons.Text),
                HasRings = chkRings.Checked,
                AtmosphereType = txtAtmosphere.Text
            };
            MessageBox.Show("Planet created successfully!");
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            lblInfo.Text =
                $"Name: {planet.Name}\n" +
                $"Mass: {planet.Mass} kg\n" +
                $"Radius: {planet.Radius} km\n" +
                $"Distance from Sun: {planet.DistanceFromSun} million km\n" +
                $"Number of Moons: {planet.NumberOfMoons}\n" +
                $"Has Rings: {planet.HasRings}\n" +
                $"Atmosphere: {planet.AtmosphereType}\n" +
                $"Density: {planet.GetDensity():F2}\n" +
                $"Has Atmosphere: {planet.HasAtmosphere()}";
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            planet.SaveToFile("planet.txt");
            MessageBox.Show("Data saved to planet.txt");
        }
    }

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new PlanetForm());
        }
    }
}
