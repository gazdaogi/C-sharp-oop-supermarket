using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security;

namespace ZadatakSupermarketi
{
    public partial class Form1 : Form
    {
        //private String file;
        private Dictionary<String, int> gomex = new Dictionary<String,int>();
        private Dictionary<String, int> idea = new Dictionary<String, int>();
        private Dictionary<String, int> maxi = new Dictionary<String, int>();
        private Dictionary<String, int> perSu = new Dictionary<String, int>();
        private Dictionary<String, int> univer = new Dictionary<String, int>();

        public Form1()
        {
            InitializeComponent();
            button1.Click += new EventHandler(SelectButton_Click);
        }

        //open file dialog
        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var sr = new StreamReader(openFileDialog1.FileName);
                    String file = sr.ReadToEnd();
                    string[] stringSeparators = new string[] { "\r\n" };
                    string[] lines = file.Split(stringSeparators, StringSplitOptions.None);

                    foreach (String line in lines)
                    {
                        if (line.Equals(""))
                        {
                            break;
                        }

                        String[] lineParts = line.Split(' ');
                        String market = lineParts[0];
                        String fruit = lineParts[1];
                        int price = Convert.ToInt32(lineParts[2]);
                        
                        if(market.Equals("Gomex"))
                        {
                            this.gomex.Add(fruit,price);
                        }
                        else if (market.Equals("Idea"))
                        {
                            this.idea.Add(fruit,price);
                        }
                        else if (market.Equals("Maxi"))
                        {
                            this.maxi.Add(fruit,price);
                        }
                        else if (market.Equals("UniverExport"))
                        {
                            this.univer.Add(fruit,price);
                        }
                        else //PerSu
                        {
                            this.perSu.Add(fruit,price);
                        }

                        this.dataGridView1.Rows.Add(market,fruit,price);
                    }

                    label1.Text = "Datoteka " + openFileDialog1.FileName;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show("Security error.\n\nError message: {ex.Message}\n\n" +
                    "Details:\n\n{ex.StackTrace}");
                }
            }
        }

        //najpovoljniji supermarket
        private void button3_Click(object sender, EventArgs e)
        {
            int fruitsSumGomex = 0;
            int fruitsSumPerSu = 0;
            int fruitsSumIdea = 0;
            int fruitsSumMaxi = 0;
            int fruitsSumUniver = 0;

            foreach(int price in gomex.Values)
            {
                fruitsSumGomex += price;
            }
            foreach (int price in idea.Values)
            {
                fruitsSumIdea += price;
            }
            foreach (int price in perSu.Values)
            {
                fruitsSumPerSu += price;
            }
            foreach (int price in univer.Values)
            {
                fruitsSumUniver += price;
            }
            foreach (int price in maxi.Values)
            {
                fruitsSumMaxi += price;
            }

            int minValue = fruitsSumGomex;
            String marketWithMinValue = "Gomex";

            if (fruitsSumIdea < minValue)
            {
                minValue = fruitsSumIdea;
                marketWithMinValue = "Idea";
            }
            if (fruitsSumMaxi < minValue)
            {
                minValue = fruitsSumMaxi;
                marketWithMinValue = "Maxi";
            }
            if (fruitsSumPerSu < minValue)
            {
                minValue = fruitsSumPerSu;
                marketWithMinValue = "PerSu";
            }
            if (fruitsSumUniver < minValue)
            {
                minValue = fruitsSumUniver;
                marketWithMinValue = "Univer";
            }

            MessageBox.Show("Najpovoljniji supermarket je " + marketWithMinValue
                + ", sa ukupnom cenom proizvoda " + minValue);


        }

        //prosecne vrednosti voca
        private void button2_Click(object sender, EventArgs e)
        {
            double avgBananas = this.getAveragePriceOfFruit("Banane");
            double avgLemons = this.getAveragePriceOfFruit("Limun");
            double avgApples = this.getAveragePriceOfFruit("Jabuke");
            double avgOranges = this.getAveragePriceOfFruit("Pomorandze");
            double avgPineapple = this.getAveragePriceOfFruit("Ananas");

            String msg = "Prosecna cena banana: " + avgBananas;
            msg += Environment.NewLine;
            msg += "Prosecna cena pomorandzi: " + avgOranges;
            msg += Environment.NewLine;
            msg += "Prosecna cena ananasa: " + avgPineapple;
            msg += Environment.NewLine;
            msg += "Prosecna cena jabuka: " + avgApples;
            msg += Environment.NewLine;
            msg += "Prosecna cena limuna: " + avgLemons;
            msg += Environment.NewLine;

            MessageBox.Show(msg);
        }

        private double getAveragePriceOfFruit(String fruitName)
        {
            int sumOfFruit = 0;
            sumOfFruit += this.gomex[fruitName];
            sumOfFruit += this.idea[fruitName];
            sumOfFruit += this.maxi[fruitName];
            sumOfFruit += this.univer[fruitName];
            sumOfFruit += this.perSu[fruitName];
            double avgFruit = (double)sumOfFruit / 5;

            return avgFruit;
        }



    }
}
