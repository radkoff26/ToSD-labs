using System;
using System.ComponentModel;

namespace Lab5.data
{

    [Serializable]
    public class Car
    {
        [DisplayName("Модель"), Category("Общие сведения")]
        public string ModelName { get; set; }
        [DisplayName("Производитель"), Category("Общие сведения")]
        public string Manufacturer { get; set; }
        [DisplayName("Год выпуска"), Category("Общие сведения")]
        public int Year { get; set; }
        [DisplayName("Изображение"), Category("Общие сведения")]
        public string Img { get; set; } = "../../images/addPic.png";
        [DisplayName("Тип топлива"), Category("Технические характеристики")]
        public FuelType FuelType { get; set; }
        [DisplayName("Пробег"), Category("Технические характеристики")]
        public int Mileage { get; set; }
        public Car (string modelName, string manufacturer, int year, string img, FuelType fuelType, int mileage)
        {
            ModelName = modelName;
            Manufacturer = manufacturer;
            Year = year;
            Img = img;
            FuelType = fuelType;
            Mileage = mileage;
        }
        public Car() { }
    }
}
