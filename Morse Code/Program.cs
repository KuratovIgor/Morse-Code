using System;
using System.IO;
using LibMorseCode;

namespace Morse_Code
{
    class Program
    {
        public static object CodePagesEncodingProvider { get; private set; }

        static void Main(string[] args)
        {
            string stringLetter = null, resultString = null;

            Console.WriteLine("Введите путь файла с текстом:");
            string path_sourse = Console.ReadLine();
            Console.WriteLine("Введите путь, по которому нужно записать результат:");
            string path_result = Console.ReadLine();

            try
            {
                using (StreamReader stream = File.OpenText(path_sourse))
                {
                    while (stream.Peek() != -1)
                    {
                        stringLetter += stream.ReadLine();
                        stringLetter += "\n";
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Not file!");
                Environment.Exit(0);
            }
            

            if (stringLetter == null)
            {
                Console.WriteLine("File is empty!");
                Environment.Exit(0);
            }

            Console.WriteLine("Choose language (russian, english, translit): [r/e/t]");
            string language = Console.ReadLine();

            if (language != "e" && language != "r" && language != "t")
            {
                Console.WriteLine("You can choose only r or e or t!");
            }
            else
            {
                if (language == "e") language = "en";
                if (language == "r") language = "rus";
                if (language == "t") language = "translit";

                MorseCode message = new MorseCode(language);
                message.NotifyError += ShowMessage;

                Console.WriteLine("Source string is Code or Leter? [c/l] ");
                char answer = Convert.ToChar(Console.ReadLine());

                if (answer == 'c')
                    resultString = message.Decode(stringLetter);
                else if (answer == 'l')
                    resultString = message.Code(stringLetter);
                else
                    Console.WriteLine("You can choose only c or l!");

                if (resultString != null)
                {
                    using (StreamWriter stream = new StreamWriter(path_result, false, System.Text.Encoding.UTF8))
                        stream.Write(resultString);

                    Console.WriteLine("The operation was successful, check the result file.");
                }
            }

            Console.ReadLine();
        }

        private static void ShowMessage(String textOfError)
        {
            Console.WriteLine(textOfError);
        }
    }
}
