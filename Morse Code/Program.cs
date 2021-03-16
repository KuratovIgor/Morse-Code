using System;
using LibMorseCode;

namespace Morse_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            String stringLetter;

            Console.WriteLine("Choose language: [r,e] ");
            char language = Convert.ToChar(Console.ReadLine());

            if (language != 'e' && language != 'r')
            {
                Console.WriteLine("You can choose only r or e!");
            }    
            else
            {
                Console.WriteLine("Choose Morse/Leter? [m/l] ");
                char answer = Convert.ToChar(Console.ReadLine());

                switch (answer)
                {
                    case 'm':
                        {
                            Console.Write("Enter morse-code: ");
                            stringLetter = Console.ReadLine();
                            if (language == 'e')
                                Console.WriteLine(MorseCode.TranslateToEnLetter(stringLetter));
                            if (language == 'r')
                                Console.WriteLine(MorseCode.TranslateToRusLetter(stringLetter));
                        }
                        break;
                    case 'l':
                        {
                            Console.Write("Enter letter: ");
                            stringLetter = Console.ReadLine();
                            if (language == 'e')
                                Console.WriteLine(MorseCode.TranslateToMorseEn(stringLetter));
                            if (language == 'r')
                                Console.WriteLine(MorseCode.TranslateToMorseRus(stringLetter));
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("You can choose only m or l!");
                        }
                        break;
                }
            }
        }
    }
}
