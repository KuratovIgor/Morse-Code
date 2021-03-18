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
            const string PATH_SOURSE = @"C:\Users\kurat\source\repos\Разработка ПМ\Morse Code\SourseString.txt";
            const string PATH_RESULT = @"C:\Users\kurat\source\repos\Разработка ПМ\Morse Code\Result.txt";

            string stringLetter = null, resultString = null;

            using (StreamReader stream = File.OpenText(PATH_SOURSE))
                stringLetter = stream.ReadLine();

            if (stringLetter == null)
            {
                Console.WriteLine("File is empty!");
                Environment.Exit(0);
            }

            Console.WriteLine("Choose language: [r,e] ");
            string language = Console.ReadLine();

            if (language != "e" && language != "r")
            {
                Console.WriteLine("You can choose only r or e!");
            } 
            else
            {
                if (language == "e") language = "en";
                if (language == "r") language = "rus";

                MorseCode message = new MorseCode(language);
                message.RegisterErrors(new MorseCode.ErrorMessage(ShowMessage));

                Console.WriteLine("Source string is Morse or Leter? [m/l] ");
                char answer = Convert.ToChar(Console.ReadLine());

                if (answer == 'm')
                    resultString = message.TranslateFromMorse(stringLetter);
                else if (answer == 'l')
                    resultString = message.TranslateToMorse(stringLetter);
                else
                    Console.WriteLine("You can choose only m or l!");

                if (resultString != null)
                {
                    using (StreamWriter stream = new StreamWriter(PATH_RESULT, false, System.Text.Encoding.UTF8))
                        stream.Write(resultString);

                    Console.WriteLine("The operation was successful, check the result file.");
                }
            }
        }

        private static void ShowMessage(String textOfError)
        {
            Console.WriteLine(textOfError);
        }
    }
}
```
