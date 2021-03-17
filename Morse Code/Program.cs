using System;
using LibMorseCode;
using System.IO;

namespace Morse_Code
{
    class Program
    {
        delegate void ErrorMessage(String message);

        private static void ShowMessage(String textOfError)
        {
            Console.WriteLine(textOfError);
        }

        static void Main(string[] args)
        {
            const string pathSourse = @"C:\Users\kurat\source\repos\Разработка ПМ\Morse Code\SourseString.txt";
            const string pathNew = @"C:\Users\kurat\source\repos\Разработка ПМ\Morse Code\Result.txt";

            string stringLetter = "";
            string resultString = "";
            ErrorMessage errorMes = ShowMessage;

            Console.WriteLine("Choose language: [r,e] ");
            string language = Console.ReadLine();

            if (language != "e" && language != "r")
            {
                errorMes("You can choose only r or e!");
                Environment.Exit(0);
            }

            MorseCode message;
            if (language == "e")
                message = new MorseCode("en");
            else
                message = new MorseCode("rus");

            message.RegisterErrors(new MorseCode.ErrorMessage(ShowMessage));

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
                            resultString = message.TranslateFromMorse(stringLetter);

                            using (StreamWriter stream = new StreamWriter(pathNew, false, System.Text.Encoding.UTF8))
                                stream.Write(resultString);

                            if (resultString != "\0")
                                Console.WriteLine("The operation was successful! Check the file!");
                        }
                        else
                        {
                            errorMes("File is empty!");
                        }
                    }
                    break;
                case 'l':
                    {
                        using (StreamReader stream = File.OpenText(pathSourse))
                            stringLetter = stream.ReadLine();

                        if (stringLetter != null)
                        {
                            resultString = message.TranslateToMorse(stringLetter);

                            using (StreamWriter stream = new StreamWriter(pathNew, false, System.Text.Encoding.UTF8))
                                stream.Write(resultString);

                            if (resultString != "\0")
                                Console.WriteLine("The operation was successful! Check the file!");
                        }
                        else
                        {
                            errorMes("File is empty!");
                        }
                    }
                    break;
                default:
                    {
                        errorMes("You can choose only m or l!");
                    }
                    break;
            }
        }
    }
}
