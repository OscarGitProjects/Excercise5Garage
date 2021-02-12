using System;

namespace Excercise5Garage.Extensions
{
    /// <summary>
    /// Extension metoder för string klassen
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Metoden kommer att returnera slumpmässig tecken från tecknen i strChars
        /// Om strChars är null eller en tom sträng returneras Char.MinValue
        /// </summary>
        /// <param name="strChars">string med de tecken som vi vill kunna välja bland</param>
        /// <returns>Slumpmässigt valt tecken från string av tecken. Om strChars är null eller en tom sträng returneras Char.MinValue</returns>
        public static char RandomChar(this string strChars)
        {
            char ch = Char.MinValue;
            Random rand = new Random();

            if (String.IsNullOrEmpty(strChars))
                return ch;

            int iRandomIndex = rand.Next(0, strChars.Length);
            ch = strChars[iRandomIndex];

            return ch;
        }
    }
}
