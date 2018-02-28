using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WeatherApp
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void getWeather()
        {
            try
            {
                string url = "http://api.openweathermap.org/data/2.5/weather?q=" + textBox1.Text + "&APPID=c1f32bdc6fce77f9c867c084032ece1a&units=metric";
                var json = new WebClient().DownloadString(url);
                var values = JObject.Parse(json);
                var temp = values["main"]["temp"].ToString();
                if (Double.Parse(temp) >= 0)
                {
                    temp = "+" + temp;
                }
                label3.Text = "Температура воздуха сейчас " + temp.ToString() + " C";
                label4.Text = "Скорость ветра " + values["wind"]["speed"] + " м/c";
                foreach (var w in values["weather"])
                {
                    if (w["main"].ToString() == "Clouds")
                    {
                        label5.Text = "Облачно, серо, пасмурно :(";
                    }
                    else if (w["main"].ToString() == "Clear")
                    {
                        label5.Text = "На улице ясно, облаков почти нет...";
                    }
                    else if (w["main"].ToString() == "Snow")
                    {
                        label5.Text = "Идет снег, можно играть в снежки! :)";
                    }
                    else if (w["main"].ToString() == "Extreme")
                    {
                        label5.Text = "Даже не вздумайте сегодня выходить из дома!";
                    }
                    if (Double.Parse(temp) >= 30)
                    {
                        label6.Text = "На улице очень жарко, прохладительные напитки приветствуются!";
                    }
                    else if (Double.Parse(temp) >= 20 && Double.Parse(temp) < 30)
                    {
                        label6.Text = "На улице очень тепло, даже немного жарко!";
                    }
                    else if (Double.Parse(temp) >= 10 && Double.Parse(temp) < 20)
                    {
                        label6.Text = "На улице тепло, шапка не нужна :)";
                    }
                    else if (Double.Parse(temp) > 0 && Double.Parse(temp) < 10)
                    {
                        label6.Text = "За окном очень прохладно! Будьте осторожны :)";
                    }
                    else if (Double.Parse(temp) > -15 && Double.Parse(temp) <= 0)
                    {
                        label6.Text = "За окном мороз, но на работу/учебу/прогулку идти можно!";
                    }
                    else if (Double.Parse(temp) <= -15 && Double.Parse(temp) >= -30)
                    {
                        label6.Text = "За окном страшный мороз, не выходите на улицу без необходимости!";
                    }
                }
                if (values["weather"][0]["main"].ToString() == "Rain")
                {
                    label7.Text = "Нужно захватить с собой зонт!";
                }
                else
                {
                    label7.Text = "Зонт можно не брать!";
                }
                pictureBox2.Load("http://openweathermap.org/img/w/" + values["weather"][0]["icon"].ToString() + ".png");
            }
            catch
            {
                label3.Text = "Произошла ошибка, попробуйте снова...";
                label4.Text = "";
                label5.Text = "";
                label6.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getWeather();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getWeather();
            }
        }
    }
}
