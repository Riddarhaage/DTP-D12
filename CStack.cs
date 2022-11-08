using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MolkFreeCalc
{
    /// <summary>
    /// Is essentially a RPN-calculator with four registers X, Y, Z, T
    /// like the HP RPN calculators.Numeric values are entered in the entry
    /// string by adding digits and one comma.For test purposes the method
    /// RollSetX can be used instead. Operations can be performed on the
    /// calculator preferrably by using one of the methods
    /// <list type="number">
    /// <item>BinOp - merges X and Y into X via an operation and rolls down
    /// the stack</item>
    /// <item>Unop - operates X and puts the result in X with overwrite</item>
    /// <item>Nilop - adds a known constant on the stack and rolls up the stack</item>
    /// </list>
    /// </summary>
    public class CStack
    {
        public double X, Y, Z, T;
        public string entry;
        /// <summary>
        /// create a new stack and init X, Y, Z, T and the text entry
        /// <param>--</param>
        /// </summary>
        public CStack()
        {
            X = Y = Z = T = 0;
            entry = "";
        }
        /// <summary>
        /// construct a string to write out in a stack view
        /// <param>--</param>
        /// </summary>
        /// <returns>the string containing the values T, Z, Y, X with newlines
        /// between them/returns>
        public string StackString()
        {
            return $"{T}\n{Z}\n{Y}\n{X}\n{entry}";
        }
        /// <summary>
        /// Set X with overwrite
        /// </summary>
        /// <returns>--</returns>
        /// <param name="newX">double newX - the name value to put in X</param>
        public void SetX(double newX)
        {
            X = newX;
        }
        /// <summary>
        /// add a digit to the entry string
        /// </summary>
        /// <param name="digit">String digit - the candidate digit to add at the end of
        /// the string</param>
        /// <returns>--</returns>
        /// \bug {if the string digit does not contain a parseable integer, nothing
        /// is added to the entry}
        public void EntryAddNum(string digit)
        {
            int val;
            if (int.TryParse(digit, out val))
            {
                entry = entry + val;
            }
        }
        /// <summary>
        /// adds a comma to the entry string
        /// <param>--</param>
        /// <returns>--</returns>
        /// \bug {if the entry string already contains a comma, nothing is added
        /// to the entry}
        /// </summary>
        public void EntryAddComma()
        {
            if (entry.IndexOf(",") == -1)
                entry = entry + ",";
        }
        /// <summary>
        /// changes the sign of the entry string
        /// <param>--</param>
        /// <returns>--</returns>
        /// <remarks>if the first char is already a '-' it is exchanged for a '+',
        /// if it is a '+' it is changed to a '-', otherwise a '-' is just added
        /// first</remarks>
        /// </summary>
        public void EntryChangeSign()
        {
            char[] cval = entry.ToCharArray();
            if (cval.Length > 0)
            {
                switch (cval[0])
                {
                    case '+': cval[0] = '-'; entry = new string(cval); break;
                    case '-': cval[0] = '+'; entry = new string(cval); break;
                    default: entry = '-' + entry; break;
                }
            }
            else
            {
                entry = '-' + entry;
            }
        }
        /// <summary>
        /// convers the entry to a double and puts it into X
        /// <remarks>the entry is cleared after a successful operation</remarks>
        /// </summary>
        public void Enter()
        {
            if (entry != "")
            {
                RollSetX(double.Parse(entry));
                entry = "";
            }
        }
        /// <summary>
        /// drops the value of X, and rolls down
        /// <remarks>Z gets the value of T</remarks>
        /// </summary>
        public void Drop()
        {
            X = Y; Y = Z; Z = T;
        }
        /// <summary>
        /// replaces the value of X, and rolls down
        /// <remarks>Z gets the value of T</remarks>
        /// </summary>
        /// \note {this is used when applying binary operations consuming
        /// X and Y and putting the result in X, while rolling down the
        /// stack}
        /// 
        /// <param name="newX">double newX - the new value to assign to X</param>
        public void DropSetX(double newX)
        {
            X = newX; Y = Z; Z = T;
        }
        /// <summary>
        /// evaluates a binary operation
        /// </summary>
        /// <param name="op">string op - the binary operation retrieved from the
        /// GUI buttons</param>
        /// <remarks>the stack is rolled down</remarks>
        public void BinOp(string op)
        {
            switch (op)
            {
                case "+": DropSetX(Y + X); break;
                case "−": DropSetX(Y - X); break;
                case "×": DropSetX(Y * X); break;
                case "÷": DropSetX(Y / X); break;
                case "yˣ": /* NYI: Power */ break;
                case "ˣ√y": /* NYI: Xth Root */ break;
            }
        }
        /* METHOD: Unop
         * PURPOSE: evaluates a unary operation
         * PARAMETERS: string op - the unary operation retrieved from the
         *   GUI buttons
         * RETURNS: --
         * FEATURES: the stack is not moved, X is replaced by the result of
         *   the operation
         */
        public void Unop(string op)
        {
            switch (op)
            {
                // Powers & Logarithms:
                case "x²": SetX(X * X); break;
                case "√x": SetX(Math.Sqrt(X)); break;
                case "log x": /* NYI: 10th Logarithm */ break;
                case "ln x": /* NYI: Natural Logarithm */ break;
                case "10ˣ": /* NYI: Exponent of 10 */ break;
                case "eˣ": /* NYI: Exponent of e */ break;

                // Trigonometry:
                case "sin": SetX(Math.Sin(X)); break;
                case "cos": /* NYI: Cosine */ break;
                case "tan": /* NYI: Tangent */ break;
                case "sin⁻¹": /* NYI: Arc Sine */ break;
                case "cos⁻¹": /* NYI: Arc Cosine */ break;
                case "tan⁻¹": /* NYI: Arc Tangent */ break;
            }
        }
        /// <summary>
        /// evaluates a "nilary operation" (insertion of a constant)
        /// </summary>
        /// <param name="op">string op - the nilary operation (name of the constant)
        /// retrieved from the GUI buttons</param>
        /// <remarks>the stack is rolled up, X is preserved in Y that is preserved in
        ///   Z that is preserved in T, T is erased</remarks>
        public void Nilop(string op)
        {
            switch (op)
            {
                case "π": RollSetX(Math.PI); break;
                case "e": /* NYI: e Constant */ break;
            }
        }
        /// <summary>
        /// rolls the tack up
        /// </summary>
        public void Roll()
        {
            double tmp = T;
            T = Z; Z = Y; Y = X; X = tmp;
        }
        /// <summary>
        /// rolls the stack up and puts a new value in X
        /// </summary>
        /// <remarks>T is dropped</remarks>

        public void RollSetX(double newX)
        {
            T = Z; Z = Y; Y = X; X = newX;
        }
        public void Setvar(string op)
        {
        }
        public void GetVar(string op)
        {

        }
    }
}
