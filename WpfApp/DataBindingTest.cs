using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        Person person = new Person { Name = "Salman", Age = 26 };

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string message = person.Name + " is " + person.Age;
            MessageBox.Show(message);
        }
    }


    public class Person
    {
        private string nameValue;

        public string Name
        {
            get => nameValue;
            set => nameValue = value;
        }

        private double ageValue;

        public double Age
        {
            get => ageValue;
            set => ageValue = value;
        }
    }
}
