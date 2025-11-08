using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab1_3
{
    class Furniture : IComparable<Furniture>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Size { get; set; } 

        public Furniture(string name, double price, double size)
        {
            Name = name;
            Price = price;
            Size = size;
        }
        
        public int CompareTo(Furniture other)
        {
            if (other == null) return 1;
            return Price.CompareTo(other.Price);
        }

        public override string ToString()
        {
            return $"{Name}: {Price} грн, Size: {Size}";
        }
    }

    class FurnitureComparer : IComparer<Furniture>
    {
        private string criteria;

        public FurnitureComparer(string criteria)
        {
            this.criteria = criteria;
        }

        public int Compare(Furniture x, Furniture y)
        {
            if (criteria == "price")
                return x.Price.CompareTo(y.Price);
            else if (criteria == "size")
                return x.Size.CompareTo(y.Size);
            else
                return 0;
        }
    }

    class FurnitureCollection : IEnumerable<Furniture>
    {
        private List<Furniture> list = new List<Furniture>();

        public void Add(Furniture f)
        {
            list.Add(f);
        }

        public IEnumerator<Furniture> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<Furniture> GetList()
        {
            return list;
        }
    }

   class MainForm : Form
    {
        TextBox txtName, txtPrice, txtSize;
        Button btnAdd, btnSortPrice, btnSortSize;
        ListBox listBox;
        FurnitureCollection collection = new FurnitureCollection();

        public MainForm()
        {
            this.Text = "Furniture Sorter";
            this.Width = 400;
            this.Height = 400;

            int labelLeft = 20;
            int textLeft = 150; 
            int topOffset = 20;
            int verticalSpacing = 40;

            Label lbl1 = new Label() { Text = "Name:", Left = labelLeft, Top = topOffset, Width = 120 };
            Label lbl2 = new Label() { Text = "Price:", Left = labelLeft, Top = topOffset + verticalSpacing, Width = 120 };
            Label lbl3 = new Label() { Text = "Size:", Left = labelLeft, Top = topOffset + 2 * verticalSpacing, Width = 120 };

            txtName = new TextBox() { Left = textLeft, Top = topOffset, Width = 180 };
            txtPrice = new TextBox() { Left = textLeft, Top = topOffset + verticalSpacing, Width = 180 };
            txtSize = new TextBox() { Left = textLeft, Top = topOffset + 2 * verticalSpacing, Width = 180 };

            btnAdd = new Button() { Text = "Add", Left = 50, Top = topOffset + 3 * verticalSpacing, Width = 100 };
            btnSortPrice = new Button() { Text = "Sort by Price", Left = 160, Top = topOffset + 3 * verticalSpacing, Width = 120 };
            btnSortSize = new Button() { Text = "Sort by Size", Left = 290, Top = topOffset + 3 * verticalSpacing, Width = 100 };

            listBox = new ListBox() { Left = 20, Top = topOffset + 4 * verticalSpacing + 10, Width = 350, Height = 120 };

            btnAdd.Click += BtnAdd_Click;
            btnSortPrice.Click += BtnSortPrice_Click;
            btnSortSize.Click += BtnSortSize_Click;

            this.Controls.Add(lbl1);
            this.Controls.Add(lbl2);
            this.Controls.Add(lbl3);
            this.Controls.Add(txtName);
            this.Controls.Add(txtPrice);
            this.Controls.Add(txtSize);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnSortPrice);
            this.Controls.Add(btnSortSize);
            this.Controls.Add(listBox);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                double price = double.Parse(txtPrice.Text);
                double size = double.Parse(txtSize.Text);

                Furniture f = new Furniture(name, price, size);
                collection.Add(f);

                listBox.Items.Add(f);
                txtName.Text = txtPrice.Text = txtSize.Text = "";
            }
            catch
            {
                MessageBox.Show("Invalid input!");
            }
        }

        private void BtnSortPrice_Click(object sender, EventArgs e)
        {
            List<Furniture> list = collection.GetList();
            list.Sort(); 
            UpdateList(list);
        }

        private void BtnSortSize_Click(object sender, EventArgs e)
        {
            List<Furniture> list = collection.GetList();
            list.Sort(new FurnitureComparer("size")); 
            UpdateList(list);
        }

        private void UpdateList(List<Furniture> list)
        {
            listBox.Items.Clear();
            foreach (Furniture f in list)
                listBox.Items.Add(f.ToString());
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
