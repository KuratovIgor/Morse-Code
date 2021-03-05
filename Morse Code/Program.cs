using System;
using LibMorseCode;

namespace Morse_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            String stringLetter;

            Console.WriteLine("Choose Morse/Leter? [m/l] ");
            char answer = Convert.ToChar(Console.ReadLine());

            switch (answer) {
                case 'm': {
                        Console.Write("Enter morse-code: ");
                        stringLetter = Console.ReadLine();
                        Console.WriteLine(MorseCode.TranslateToLetter(stringLetter));
                }break;
                case 'l': {
                        Console.Write("Enter letter: ");
                        stringLetter = Console.ReadLine();
                        Console.WriteLine(MorseCode.TranslateToMorse(stringLetter));
                }break;
                default: {
                        Console.WriteLine("You can choose only m or l! ");
                }break;
            }
        }
    }
}
