using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Collection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            string defoltPath = "C:\\Users\\vladi\\OneDrive\\Рабочий стол\\file";
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                File.Create(Path.Combine(defoltPath, $"File_{i}.txt"));
            }
            textBox11.Text = defoltPath;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double.TryParse(textBox1.Text, out var arifmeticFirstValue);
            double.TryParse(textBox2.Text, out var arifmeticSecondValue);
            int.TryParse(textBox3.Text, out var arifmeticLenthProgression);
            if (string.IsNullOrEmpty(textBox1.Text))
                MessageBox.Show("Вы не ввели чисто в поле прогрессии");
            if (string.IsNullOrEmpty(textBox2.Text))
                MessageBox.Show("Вы не ввели шаг  прогрессии");
            if (string.IsNullOrEmpty(textBox3.Text))
                MessageBox.Show("Вы не ввели длинну прогрессии");
            ArifmeticProgressiveList progressivList = new ArifmeticProgressiveList(arifmeticFirstValue, arifmeticSecondValue, arifmeticLenthProgression);
            textBox4.Text = "";
            foreach (double item in progressivList)
            {
                textBox4.Text += item + ",";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            double.TryParse(textBox5.Text, out var geometricFirstValue);
            double.TryParse(textBox6.Text, out var geometricSecondValue);
            int.TryParse(textBox7.Text, out var geometricLenthProgression);
            if (string.IsNullOrEmpty(textBox5.Text))
                MessageBox.Show("Вы не ввели чисто в поле прогрессии");
            if (string.IsNullOrEmpty(textBox6.Text))
                MessageBox.Show("Вы не ввели шаг  прогрессии");
            if (string.IsNullOrEmpty(textBox7.Text))
                MessageBox.Show("Вы не ввели длинну прогрессии");
            GeometricProgressiveList progressivList = new GeometricProgressiveList(geometricFirstValue, geometricSecondValue, geometricLenthProgression);
            textBox8.Text = "";
            foreach (double item in progressivList)
            {
                textBox8.Text += item + ",";
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int.TryParse(textBox9.Text, out var currentValue);
            if (string.IsNullOrEmpty(textBox9.Text))
                MessageBox.Show("Вы не ввели простое число");
            SimpleNumber simpleNumber = new SimpleNumber(currentValue);
            textBox10.Text = "";
            foreach (int item in simpleNumber)
            {
                textBox10.Text += item + ",";
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string path = textBox11.Text;
            string fileName = textBox12.Text;
            string fileType = textBox13.Text;
            if (string.IsNullOrEmpty(path))
                MessageBox.Show("Вы не ввели путь");
            if (string.IsNullOrEmpty(fileName))
                MessageBox.Show("Вы не ввели имя файла");
            if (string.IsNullOrEmpty(fileType))
                MessageBox.Show("Вы не ввели тип файла");
            FileCollection fileCollection = new FileCollection(path, fileName, fileType);
             
            foreach (string file in fileCollection)
            {
                textBox14.Text += file+", ";
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string word = textBox15.Text;
            if (string.IsNullOrEmpty(word))
                MessageBox.Show("Вы не ввели слово");
            RandomWords randomWords = new RandomWords(word);
            textBox16.Text = "";
            foreach (string item in randomWords)
            {
                if (item == word)
                    break;
                textBox16.Text += item + ", ";
            }
        }
    }
    class RandomWords : IEnumerable
    {
        public Random ran = new Random();
        string word;
        public RandomWords(string word)
        {
            this.word = word;
        }
        public IEnumerator GetEnumerator()
        {
            while (true)
            {
                //string randomWords = String.Join("", this.word.ToCharArray().ToList().OrderBy(x => this.ran.Next()));
                string randomWords = new string(this.word.ToCharArray().ToList().OrderBy(x => this.ran.Next()).ToArray());
                yield return randomWords;
            }
        }
    }

    class FileCollection
    {
        string path;
        string fileName;
        string fileType;

        
        public FileCollection(string path, string fileName, string fileType)
        {
            this.path = path;
            this.fileName = fileName;
            this.fileType = fileType;
            
        }
        public IEnumerator<string> GetEnumerator()
        {
            if (Directory.Exists(path))
            {
                Regex regex = new Regex($@"{fileName}\.{fileType}");
                FileInfo fileInfo;
                foreach (string item in Directory.GetFiles(path))
                {
                    fileInfo = new FileInfo(item);
                    Debug.WriteLine($"{fileInfo.Name} {regex.IsMatch(fileInfo.Name)}");
                    if (regex.IsMatch(fileInfo.Name))
                    {
                        yield return fileInfo.Name;
                    }

                }
            }
            
        }
    }
    

    class ArifmeticProgressiveList : IEnumerable
    {
        double arifmeticFirstValue;
        double arifmeticSecondValue;
        int arifmeticLenthProgression;
        public ArifmeticProgressiveList(double arifmeticFirstValue, double arifmeticSecondValue, int arifmeticLenthProgression)
        {
            this.arifmeticFirstValue = arifmeticFirstValue;
            this.arifmeticSecondValue = arifmeticSecondValue;
            this.arifmeticLenthProgression = arifmeticLenthProgression;
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.arifmeticLenthProgression; i++)
            {
                yield return this.arifmeticFirstValue + this.arifmeticSecondValue * i;
            }
        }
    }
    class GeometricProgressiveList : IEnumerable
    {
        double geometricFirstValue;
        double geometricSecondValue;
        int geometricLenthProgression;
        public GeometricProgressiveList(double geometricFirstValue, double geometricSecondValue, int geometricLenthProgression)
        {
            this.geometricFirstValue = geometricFirstValue;
            this.geometricSecondValue = geometricSecondValue;
            this.geometricLenthProgression = geometricLenthProgression;
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.geometricLenthProgression; i++)
            {
                yield return this.geometricFirstValue * Math.Pow(this.geometricSecondValue, i);
            }
        }
    }
    class SimpleNumber : IEnumerable
    {
        int simpleValue;
        int currentValue;
        public SimpleNumber(int currentValue)
        {
            this.currentValue = currentValue;
        }
        private List<uint> SieveEratosthenes(uint n)
        {
            var numbers = new List<uint>();
            for (var i = 2u; i < n; i++)
            {
                numbers.Add(i);
            }
            for (var i = 0; i < numbers.Count; i++)
            {
                for (var j = 2u; j < n; j++)
                {
                    numbers.Remove(numbers[i] * j);
                }
            }
            return numbers;
        }
        public IEnumerator GetEnumerator()
        {
            var numbers = new List<int>();
            for (int i = 2; i < currentValue; i++)
            {
                numbers.Add(i);
            }
            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = 2; j < currentValue; j++)
                {
                    numbers.Remove(numbers[i] * j);
                    simpleValue = numbers[i];
                }
                yield return simpleValue;
            }
        }
    }
}



