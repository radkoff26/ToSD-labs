using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Text.Encodings.Web;
using Lab5.data;

namespace Lab5
{
    public partial class Form1 : Form
    {
        private BindingSource bs = new BindingSource();
        private BindingList<Car> carsBindingList;

        public Form1()
        {
            InitializeComponent();

            List<Car> cars = GetInitialList();
            carsBindingList = new BindingList<Car>(cars);
            bs.DataSource = carsBindingList;
            nav.BindingSource = bs;

            SetUpGrid();
            SetUpComboBox();
            SetUpPictureBox();
            SetUpChart();

            propertyGrid.DataBindings.Add("SelectedObject", bs, "");
        }

        private void SetUpGrid()
        {
            grid.AutoGenerateColumns = false;
            grid.DataSource = bs;
            grid.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    Width = 180,
                    Name = "modelName",
                    HeaderText = "Модель",
                    DataPropertyName = "ModelName"
                },
                new DataGridViewTextBoxColumn
                {
                    Width = 100,
                    Name = "manufacturer",
                    HeaderText = "Производитель",
                    DataPropertyName = "Manufacturer"
                },
                new DataGridViewTextBoxColumn
                {
                    Width = 80,
                    Name = "year",
                    HeaderText = "Год выпуска",
                    DataPropertyName = "Year"
                },
                new DataGridViewComboBoxColumn
                {
                    Width = 100,
                    Name = "fuelType",
                    HeaderText = "Тип топлива",
                    DataPropertyName = "FuelType",
                    DataSource = Enum.GetValues(typeof(FuelType))
                },
                new DataGridViewTextBoxColumn
                {
                    Width = 100,
                    Name = "mileage",
                    HeaderText = "Пробег",
                    DataPropertyName = "Mileage",
                }
            );
        }

        private void SetUpComboBox()
        {
            var items = grid.Columns.Cast<DataGridViewColumn>().ToList();
            columnComboBox.ComboBox.DataSource = items;
            columnComboBox.ComboBox.DisplayMember = "HeaderText";
            columnComboBox.ComboBox.SelectedIndex = 0;
        }

        private void SetUpPictureBox()
        {
            picBox.SizeMode = PictureBoxSizeMode.Zoom;
            picBox.DataBindings.Add("ImageLocation", bs, "Img");
            picBox.DoubleClick += PicBox_DoubleClick;
        }

        private void SetUpChart()
        {
            chart.DataSource = from w in bs.DataSource as BindingList<Car>
                               group w by w.FuelType.ToString()
                                into g
                               select new { FuelType = g.Key, AverageMileage = g.Average(w => w.Mileage) };
            chart.Series[0].XValueMember = "FuelType";
            chart.Series[0].YValueMembers = "AverageMileage";
            chart.Legends.Clear();
            chart.Titles.Clear();
            chart.Titles.Add("Диаграмма: средний пробег - тип топлива");
            bs.CurrentChanged += (o, e) => chart.DataBind(); // Обновляем значения в диаграмме при изменении
        }

        private List<Car> GetInitialList() => new List<Car>
        {
            new Car("Skyline", "Nissan", 1997, "../../images/skyline.jpg", FuelType.Бензин, 30000),
            new Car("Mark II", "Toyota", 2000, "../../images/mark2.jpg", FuelType.Дизель, 50000),
            new Car("Golf", "Volkswagen", 2020, "../../images/golf.jpg", FuelType.Бензин, 10000),
        };

        private void PicBox_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "Картинка в формате jpg|*.jpg|Картинка в формате jpeg|*.jpeg|Картинка в формате png|*.png"
            };
            if (opf.ShowDialog() == DialogResult.OK)
            {
                (bs.Current as Car).Img = opf.FileName;
                bs.ResetBindings(false);
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "Файл в формате json|*.json|Файл в формате xml|*.xml"
            };
            var dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string filename = dialog.FileName;
                string extension = Path.GetExtension(filename).ToLower();
                if (extension == ".json")
                {
                    LoadJson(filename);
                }
                else if (extension == ".xml")
                {
                    LoadXml(filename);
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "Файл в формате json|*.json|Файл в формате xml|*.xml"
            };
            var dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string filename = dialog.FileName;
                string extension = Path.GetExtension(filename).ToLower();
                if (extension == ".json")
                {
                    SaveJson(filename);
                }
                else if (extension == ".xml")
                {
                    SaveXml(filename);
                }
            }
        }

        private void LoadXml(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Car>));
            using (Stream sw = new FileStream(fileName, FileMode.Open))
            {
                bs.DataSource = (List<Car>)ser.Deserialize(sw);
            }
        }

        private void LoadJson(string file)
        {
            string jsonString = File.ReadAllText(file);
            bs.DataSource = JsonSerializer.Deserialize<List<Car>>(jsonString);
        }

        private void SaveXml(string file)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Car>));
            using (Stream sw = new FileStream(file, FileMode.Create))
            {
                ser.Serialize(sw, bs.DataSource);
            }
        }

        private void SaveJson(string file)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string jsonString = JsonSerializer.Serialize(bs.DataSource, options);
            File.WriteAllText(file, jsonString);
        }

        private void columnComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGridData();
        }

        private void grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            columnComboBox.SelectedIndex = e.ColumnIndex;
        }

        private void grid_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var value = grid["modelName", e.RowIndex].Value;
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                grid["modelName", e.RowIndex].Value = "Не указана";
            }
        }

        private void grid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (grid.Columns[e.ColumnIndex].Name == "year" || grid.Columns[e.ColumnIndex].Name == "mileage")
            {
                int newValue;
                if (!int.TryParse(e.FormattedValue.ToString(), out newValue) || newValue < 0 || newValue > int.MaxValue)
                {
                    e.Cancel = true;
                    MessageBox.Show("Введите числовое значение в допустимых пределах (от 0 до " + int.MaxValue + ").");
                    int value;
                    if (grid.Columns[e.ColumnIndex].Name == "year")
                    {
                        value = carsBindingList[e.RowIndex].Year;
                    } else
                    {
                        value = carsBindingList[e.RowIndex].Mileage;
                    }
                    grid[e.ColumnIndex, e.RowIndex].Value = value;
                    grid.BeginEdit(true);
                }
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateGridData();
        }

        private void UpdateGridData()
        {
            string value = toolStripTextBox1.Text;
            if (string.IsNullOrWhiteSpace(value))
            {
                bs.DataSource = carsBindingList;
                grid.Refresh();
            }
            else
            {
                var column = columnComboBox.SelectedItem as DataGridViewColumn;
                FilterGridData(value, column.DataPropertyName);
            }
        }

        private void FilterGridData(string columnValue, string columnName)
        {
            IEnumerable<Car> filteredList = carsBindingList
                        .Where(car => car.GetType().GetProperty(columnName).GetValue(car).ToString() == columnValue).ToList();

            bs.DataSource = new BindingList<Car>(filteredList.ToList());
            grid.Refresh();
        }
    }
}
