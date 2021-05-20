using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace LibMorseCode
{
    public class MorseCode
    {
        //Адреса файлов со словарями
        private const string PATH_EN = @"C:\Users\kurat\source\repos\Morse Code\LibMorseCode\EnglishDictionary.txt"; //Константа, хранящая адрес английского словаря
        private const string PATH_RUS = @"C:\Users\kurat\source\repos\Morse Code\LibMorseCode\RussianDictionary.txt"; //Константа, хранящая адрес русского словаря
        private const string PATH_TRANSLIT = @"C:\Users\kurat\source\repos\Morse Code\LibMorseCode\Translit.txt"; //Константа, хранящая адрес словаря с транслитом

        private string _language; //Язык сообщения
        private char _symbol; //Символ, который нужно перевести
        private string _shifr; //Шифр кода Морзе, который нужно перевести
       
        //Словарь алфавита
        private Dictionary<string, char> alphaviteDictionary = new Dictionary<string, char> { };
        //Словарь кода 
        private Dictionary<char, string> codeDictionary = new Dictionary<char, string> { };

        public delegate void ErrorMessage(string textOfError);
        public event ErrorMessage NotifyError;
        
        public MorseCode (string language)
        {
            _language = language;

            if (_language != "en" && _language != "rus" && _language != "translit")
                Console.WriteLine("Language isn't correct!");

            //В зависимости от того, какой язык был введен, создаются словари
            if (_language == "en")
                CreateDictionary(PATH_EN);

            if (_language == "rus")
                CreateDictionary(PATH_RUS);

            if (_language == "translit")
                CreateDictionary(PATH_TRANSLIT);
        }

        //Функция создания словаря
        private void CreateDictionary (string path)
        {
            string lineFromFile;

            using (StreamReader fileStream = File.OpenText(path)) 
            {
                while (fileStream.Peek() != -1) 
                {
                    lineFromFile = fileStream.ReadLine();

                    //Запись данных в словарь
                    if (_language != "translit")
                        alphaviteDictionary.Add(lineFromFile.Substring(1, lineFromFile.Length - 1), lineFromFile[0]);

                    codeDictionary.Add(lineFromFile[0], lineFromFile.Substring(1, lineFromFile.Length - 1));
                }
            }
        }

        //Функция проверки коррекстности ввода шифра
        private bool IsCode()
        { 
            //Сообщение должно содержать только символы '-', '.' и пробел
            if (_shifr[0] == '-' || _shifr[0] == '.' || _shifr[0] == ' ' || _shifr[0] == '\n')
                return true;
            
            return false;
        }

        //Функция проверки коррекстности ввода сообщения 
        private bool IsLetter() 
        {
            //Сообщение должно содержать только буквы латинского или русского алфавита и цифры
            if (_language == "en" && 
                   ((_symbol >= 'A' && _symbol <= 'Z') ||
                    (_symbol >= 'a' && _symbol <= 'z') ||
                    (_symbol >= '0' && _symbol <= '9') ||
                    (_symbol == ' ')))
                    return true;

            if (_language == "rus" &&
                   ((_symbol >= 'А' && _symbol <= 'Я') ||
                    (_symbol >= 'а' && _symbol <= 'я') ||
                    (_symbol >= '0' && _symbol <= '9') ||
                    (_symbol == ' ')))
                    return true;

            if (_language == "translit" && (codeDictionary.ContainsKey(_symbol) || _symbol == ' ')) 
                return true;
            
            return false;
        }

        //Функция шифрования сообщения 
        public string Code(char stringLetter)
        {
            _symbol = stringLetter;
            _symbol = Convert.ToChar(Convert.ToString(_symbol).ToUpper()); //Перевод символа в верхний регистр

            if (IsLetter())
            {
                //Перевод символа в шифр 
                if (codeDictionary.ContainsKey(_symbol))
                {
                    if (_language != "translit")
                        return codeDictionary[_symbol] + " ";
                    
                    return codeDictionary[_symbol];
                }                  
                
                if (_symbol == '\n')
                    return "\n";

                //Если на вход попал пробел, возвращаем больший отступ между шифрами
                if (_symbol == ' ')
                    return "   ";

                return null;             
            }

            return null;
        }

        //Функция расшифровки кода
        public char Decode(string shifr) 
        {
            _shifr = shifr;

            //Если сообщение введено корректно (только '-' и '.'), выполняем перевод
            if (IsCode())
            {
                if (_shifr[0] == ' ')
                    return ' ';

                if (_shifr == "\n")
                    return '\n';

                //Перевод шифра в символ
                if (alphaviteDictionary.ContainsKey(_shifr))
                    return alphaviteDictionary[_shifr];

                if (NotifyError != null)
                    NotifyError("Invalid code!");
                else
                    Console.WriteLine("Invalid code!");

                return '\0';
            }
          
            return '\0';
        }
    }
}