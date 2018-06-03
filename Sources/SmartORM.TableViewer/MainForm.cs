using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartORM.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartORM.TableViewer
{
    public partial class MainForm : Form
    {
        private TableView TableView
        {
            get;
            set;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                TableView = new TableView(new SmartFile(openFileDialog1.FileName));
                listBox1.Items.AddRange(TableView.GetDirectoriesNames());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableView.SelectedTable = listBox1.SelectedItem.ToString();

            dataGridView1.Columns.Clear();

            foreach (var property in TableView.GetProperties())
            {
                dataGridView1.Columns.Add(property, property);
            }

            foreach (var pair in TableView.GetPropertiesValues())
            {
                dataGridView1.Rows.Add(pair.Value);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("oops");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Search(textBox1.Text);
        }
        private void ResetCells()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = Color.White;
                }
            }
        }

        private int SearchResultIndex = 0;

        private string LastSearchWord;

        private List<DataGridViewCell> SearchResults = new List<DataGridViewCell>();

        private void Search(string text)
        {
            if (text != LastSearchWord)
            {
                ResetCells();
                SearchResultIndex = 0;
                SearchResults = new List<DataGridViewCell>();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString().ToLower().Contains(text.ToLower()))
                        {
                            SearchResults.Add(cell);
                            cell.Style.BackColor = Color.LightGreen;
                        }
                    }
                }
            }
            if (SearchResultIndex == SearchResults.Count)
            {
                SearchResultIndex = 0;
            }
            dataGridView1.FirstDisplayedScrollingRowIndex = SearchResults[SearchResultIndex].RowIndex;
            SearchResultIndex++;
            LastSearchWord = text;
        }
    }
}
