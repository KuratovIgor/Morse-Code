# Morse-Code
Основная программа осуществления перевода сообщения в азбуку Морзе. Работа программы осуществляется через файлы, поэтому перед запуском стоит записать исходные данные в файл SourseFile.
---------------------------
```
using System;
using LibMorseCode;
using System.IO;

namespace Morse_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            String stringLetter = "";
            String resultString = "";

            string pathSourse = @"C:\Users\kurat\source\repos\Разработка ПМ\Morse Code\SourseString.txt";
            string pathNew = @"C:\Users\kurat\source\repos\Разработка ПМ\Morse Code\Result.txt";

            Console.WriteLine("Choose language: [r,e] ");
            char language;
            try
            {
                language = Convert.ToChar(Console.ReadLine());

                if (language != 'e' && language != 'r')
                {
                    Console.WriteLine("You can choose only r or e!");
                }
                else
                {
                    Console.WriteLine("Source string is Morse or Leter? [m/l] ");
                    char answer = Convert.ToChar(Console.ReadLine());

                    switch (answer)
                    {
                        case 'm':
                            {
                                using (StreamReader stream = File.OpenText(pathSourse))
                                    stringLetter = stream.ReadLine();

                                if (stringLetter != null)
                                {
                                    if (language == 'e')
                                        resultString = MorseCode.TranslateToEnLetter(stringLetter);
                                    if (language == 'r')
                                        resultString = MorseCode.TranslateToRusLetter(stringLetter);

                                    using (StreamWriter stream = new StreamWriter(pathNew, false, System.Text.Encoding.UTF8))
                                        stream.Write(resultString);

                                    Console.WriteLine("The operation was successful! Check the file!");
                                }
                                else
                                {
                                    Console.WriteLine("File is empty!");
                                }
                            }
                            break;
                        case 'l':
                            {
                                using (StreamReader stream = File.OpenText(pathSourse))
                                    stringLetter = stream.ReadLine();

                                if (stringLetter != null)
                                {
                                    if (language == 'e')
                                        resultString = MorseCode.TranslateToMorseEn(stringLetter);
                                    if (language == 'r')
                                        resultString = MorseCode.TranslateToMorseRus(stringLetter);

                                    using (StreamWriter stream = new StreamWriter(pathNew, false, System.Text.Encoding.UTF8))
                                        stream.Write(resultString);

                                    Console.WriteLine("The operation was successful! Check the file!");
                                }
                                else
                                {
                                    Console.WriteLine("File is empty!");
                                }
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
