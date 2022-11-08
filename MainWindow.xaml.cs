using System;
using System.Collections.Generic;
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

namespace MolkFreeCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CStack cs = new CStack();
        public MainWindow()
        {
            InitializeComponent();
            LetterField.Text = "T:\nZ:\nY:\nX:\n▹";
            UpdateNumberField();
        }
        private void UpdateNumberField()
        {
            NumberField.Text = cs.StackString();
        }
        /// <summary>
        /// add number(s) button(s)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumBtn(object sender, RoutedEventArgs e)
        {
            Button B = sender as Button;
            cs.EntryAddNum(B.Content.ToString());
            UpdateNumberField();
        }
        /// <summary>
        /// put a comma between numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumComma(object sender, RoutedEventArgs e)
        {
            cs.EntryAddComma();
            UpdateNumberField();
        }
        /// <summary>
        /// changes input number from '-' to '+' or the other way around
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumCHS(object sender, RoutedEventArgs e)
        {
            cs.EntryChangeSign();
            UpdateNumberField();
        }
        /// <summary>
        /// Enter button, takes the value and assign it to X
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterBtnClick(object sender, RoutedEventArgs e)
        {
            cs.Enter();
            UpdateNumberField();
        }
        /// <summary>
        /// Controls buttons operations '+', '-', '/', '*'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpBtn_binop(object sender, RoutedEventArgs e)
        {
            string op = (sender as Button).Content.ToString();
            cs.BinOp(op);
            UpdateNumberField();
        }
        /// <summary>
        /// power and logarithm buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpBtn_unop(object sender, RoutedEventArgs e)
        {
            string op = (sender as Button).Content.ToString();
            cs.Unop(op);
            UpdateNumberField();
        }
        /// <summary>
        /// Buttons for nilary operations(e and π)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpBtn_nilop(object sender, RoutedEventArgs e)
        {
            string op = (sender as Button).Content.ToString();
            cs.Nilop(op);
            UpdateNumberField();
        }
        /// <summary>
        /// Clears the number entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clr_Btn(object sender, RoutedEventArgs e)
        {
            cs.entry = "";
            UpdateNumberField();
        }
        /// <summary>
        /// Clear X
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clx_Btn(object sender, RoutedEventArgs e)
        {
            cs.X = 0;
            UpdateNumberField();
        }
        /// <summary>
        /// Clears Y, Z and T
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clst_Btn(object sender, RoutedEventArgs e)
        {
            cs.Y = cs.Z = cs.T = 0;
            UpdateNumberField();
        }
        /// <summary>
        /// Switch place of X and Y values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpBtn_swap(object sender, RoutedEventArgs e)
        {
            double tmp = cs.X; cs.X = cs.Y; cs.Y = tmp;
            UpdateNumberField();
        }
        /// <summary>
        /// new value to X and the former values roll up the stack.
        /// T is dropped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpBtn_dup(object sender, RoutedEventArgs e)
        {
            cs.RollSetX(cs.X);
            UpdateNumberField();
        }
        /// <summary>
        /// Values roll down the stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpBtn_drop(object sender, RoutedEventArgs e)
        {
            cs.Drop();
            UpdateNumberField();
        }
        /// <summary>
        /// Roll the numbers up the stack.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpBtn_roll(object sender, RoutedEventArgs e)
        {
            cs.Roll();
            UpdateNumberField();
        }
        /// <summary>
        /// Button(s) to store values. NYI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpBtn_sto(object sender, RoutedEventArgs e)
        {
            string op = (sender as Button).Content.ToString();
            // cs.SetVar(op); /// \todo NYI!
        }
        /// <summary>
        /// Button to retrieve stored values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpBtn_rcl(object sender, RoutedEventArgs e)
        {
            string op = (sender as Button).Content.ToString();
            cs.GetVar(op);
        }
    }
}
