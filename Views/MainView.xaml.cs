using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trabalho_final.Controller;

namespace Trabalho_final
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class Calculadora : Window
    {
       
       Conexao conexao = new Conexao();

       private string equation;
       
        public Calculadora()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            equation += button.Content;
            Display.Content = equation;
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {

        }

        private void Square(object sender, RoutedEventArgs e)
        {
            
        }

        private void Negation(object sender, RoutedEventArgs e)
        {
            
        }
        private void AddComma(object sender, RoutedEventArgs e)
        {
            
        }

        private void Erase(object sender, RoutedEventArgs e)
        {
            equation = equation.Substring(0, equation.Length - 1);
            Display.Content = equation;

        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            equation = "";
            Display.Content = equation;
        }

        private void Constant(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.Equals("e"))
            {
                equation += "2.71828";
            }
            else
            {
                equation += "3.14159";
            }
            Display.Content = equation;
        }

        public void MemoryClear(object sender, RoutedEventArgs e)
        {
            Display.Content = "clear";
        }

        public void MemoryPlus(object sender, RoutedEventArgs e)
        {
            Display.Content = "plus";
        }

        public void MemoryMinus(object sender, RoutedEventArgs e)
        {
            Display.Content = "minus";
        }

        public void MemorySave(object sender, RoutedEventArgs e)
        {
            Display.Content = "save";
        }

        private void Window_KeyDownPreview(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                // Lógica para números
                case Key.NumPad0: // 0
                    equation += "0";
                    break;
                case Key.NumPad1:  // 1
                    equation += "1";
                    break;
                case Key.NumPad2: // 2
                    equation += "2";
                    break;
                case Key.NumPad3: // 3
                    equation += "3";
                    break;
                case Key.NumPad4: // 4
                    equation += "4";
                    break;
                case Key.NumPad5: // 5
                    equation += "5";
                    break;
                case Key.NumPad6: // 6  
                    equation += "6";
                    break;
                case Key.NumPad7: // 7
                    equation += "7";
                    break;
                case Key.NumPad8: // 8
                    equation += "8";
                    break;
                case Key.NumPad9: // 9
                    equation += "9";
                    break;

                // Lógica para operadores
                case Key.Add: // +
                    equation += "+";
                    break;
                case Key.Subtract: // -
                    equation += "-";
                    break;
                case Key.Multiply: // *
                    equation += "*";
                    break;
                case Key.Divide: // /
                    equation += "/";
                    break;
                case Key.Enter: // =
                    equation += "=";
                    //EXEMPLO USO CONEXAO SQL SERVER -- WILL
                    conexao.getDBConnection("insert into historico_calc(dt_atualizacao, equacao, resultado) VALUES(SYSDATETIME(), '" + equation + "', '---');", "inserir");
                    break;

                // Lógica para outros
                case Key.P: // p
                    equation += "3.14159";
                    break;
                case Key.E: // e
                    equation += "2.71828";
                    break;
                case Key.Escape: // Esc
                    equation = "";
                    break;
                case Key.Delete: // Delete
                case Key.Back: // Backspace
                    equation = equation.Substring(0, equation.Length - 1);
                    break;
                case Key.S: // s
                    equation += "sin";
                    break;
                case Key.C: // c
                    equation += "cos";
                    break;
                case Key.L: // log
                    equation += "log";
                    break;
                case Key.Oem8: // ^
                    equation += "power";
                    break;
                case Key.OemComma: // ,
                    equation += ",";
                    break;
                case Key.OemBackslash: // \
                    equation += "-1";
                    break;
                case Key.D9: // "("
                    equation += "(";
                    break;
                case Key.D0: // "("
                    equation += ")";
                    break;
                case Key.Up: // ^
                    equation += "^";
                    break;
                
                default:
                    break;
            }
            Display.Content = equation;
        }

    }
}
