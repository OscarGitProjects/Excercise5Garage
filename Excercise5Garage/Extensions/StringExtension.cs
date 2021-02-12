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
        /// </summary>
        /// <param name="strChars">string med de tecken som vi vill kunna välja bland</param>
        /// <returns>Slumpmässigt valt tecken från string av tecken</returns>
        public static char RandomChar(this string strChars)
        {
            Random rand = new Random();

            int iRandomIndex = rand.Next(0, strChars.Length);
            char ch = strChars[iRandomIndex];

            return ch;
        }
    }
}
