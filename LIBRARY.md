# Morse-Code library
Библиотека позволяет выполнять перевод сообщения на английском или русском языках в сообщение на азбуке Морзе и наоборот. 
-----------
```
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace LibMorseCode
{
    public class MorseCode
    {
        //Адреса файлов со словарями
        private const string PATH_EN = @"C:\Users\kurat\source\repos\Разработка ПМ\Morse Code\EnglishDictionary.txt"; //Константа, хранящая адрес английского словаря
        private const string PATH_RUS = @"C:\Users\kurat\source\repos\Разработка ПМ\Morse Code\RussianDictionary.txt"; //Константа, хранящая адрес русского словаря
        private const string PATH_TRANSLIT = @"C:\Users\kurat\source\repos\Разработка ПМ\Morse Code\Translit.txt";

        private string _string; //Переменная, хранящая сообщение
        private string _language; //Язык сообщения
       
        //Словарь алфавита
        private Dictionary<string, char> alphaviteDictionary = new Dictionary<string, char> { };
        //Словарь кода 
        private Dictionary<char, string> codeDictionary = new Dictionary<char, string> { };

        //Делегат для вывода сообщений об ошибках
        public delegate void ErrorMessage(String textOfError);
        ErrorMessage _errorMessage;
        
        //Конструктор
        public MorseCode (String language)
        {
            _language = language;

            //В зависимости от того, какой язык был введен, создаются словари
            switch (_language)
            {
               case "en":
                        CreateDictionary(PATH_EN);
                    break;
                case "rus":
                        CreateDictionary(PATH_RUS);
                    break;
                case "translit":
                        CreateDictionary(PATH_TRANSLIT);
                    break;
                default:
                        Console.WriteLine("Language isn't correct!");
                    break;
            }
        }

        //Функция создания словаря
        private void CreateDictionary (string path)
        {
            String mesFromFile = null, code = null;
            int index = 2, length;

            using (StreamReader fileStream = File.OpenText(path)) //Создание файлового потока для чтения
            {
                while (mesFromFile != "///") // /// - конец словаря
                {
                    mesFromFile = fileStream.ReadLine();

                    if (mesFromFile != "///")
                    {
                        length = mesFromFile.Length;
                         
                        //Раздаление ключа и значения словаря
                        while (index < length)
                        {
                            code += mesFromFile[index];
                            index++;
                        }

                        //Запись данных в словарь
                        if (_language != "translit")
                            alphaviteDictionary.Add(code, mesFromFile[0]);

                        codeDictionary.Add(mesFromFile[0], code);
                    }

                    index = 2;
                    code = null;
                }
            }
        }

        //Функция получения метода с выводом ошибки
        public void RegisterErrors(ErrorMessage errorMessage)
        {
            _errorMessage = errorMessage;
        }

        //Функция проверки коррекстности ввода шифра
        private bool IsCode()
        {
            int length = _string.Length; //Длина сообщения
            int index = 0;

            //Сообщение должно содержать только символы '-', '.' и пробел
            while (index < length)
            {
                if (_string[index] == '-' || _string[index] == '.' || _string[index] == ' ' || _string[index] == '\n')
                    index++;
                else
                    return false;
            }

            return true;
        }

        //Функция проверки коррекстности ввода сообщения 
        private bool IsLetter() 
        {
            int length = _string.Length; //Длина сообщения
            int index = 0;

            //Сообщение должно содержать только буквы латинского или русского алфавита и цифры
            if (_language == "en")
            {
                while (index < length)
                {
                    if ((_string[index] >= 'A' && _string[index] <= 'Z') ||
                        (_string[index] >= 'a' && _string[index] <= 'z') ||
                        (_string[index] >= '0' && _string[index] <= '9'))
                        return true;

                    index++;
                }
            }
            if (_language == "rus")
            {
                while (index < length)
                {
                    if ((_string[index] >= 'А' && _string[index] <= 'Я') ||
                        (_string[index] >= 'а' && _string[index] <= 'я') ||
                        (_string[index] >= '0' && _string[index] <= '9'))
                        return true;

                    index++;
                }
            }
            if (_language == "translit")
            {
                string temp = null;
                while (index < length)
                {
                    try
                    {
                        temp = codeDictionary[_string[index]];
                        return true;
                    }
                    catch (Exception) { }

                    index++;
                }
            }
            
            return false;
        }

        //Функция шифрования сообщения 
        public String Code(String stringLetter)
        { 
            //stringLetter - введённое сообщение
            _string = stringLetter;

            _string = _string.Trim(); //Удаление пробелов спереди и в конце сообщения
            _string = _string.ToUpper(); //Перевод всех символов сообщения в верхний регистр

            //Если сообщение введено корректно, выполняем перевод
            if (IsLetter() == true)
            {
                String resultString = null; //Результирующая строка в виде шифра
                int length = _string.Length; //Длина сообщения
                int index = 0, //Индекс для итераций в циклах
                    countSpaces = 0; //Количество пробелов

                while (index < length)
                {
                    //Перевод символа из сообщения в шифр 
                    if (_string[index] != ' ')
                    {
                        try
                        {
                            resultString += codeDictionary[_string[index]];

                            if (_language != "translit") 
                                resultString += " ";

                            countSpaces = 0;
                        }
                        //Если встречается символ переноса строки, этот символ заносится в результирующую строку
                        catch (Exception) 
                        {
                            if (_string[index] == '\n')
                                resultString += "\n";
                        }
                    }
                    //Устранение лишних пробелов внутри сообщения
                    else
                    {
                        if (countSpaces > 0)
                        {
                            index++;
                            continue;
                        }

                        if (_language != "translit")
                            resultString += "  ";
                        else
                            resultString += " ";

                        countSpaces++;
                    }

                    index++;
                }

                return resultString;
            }
            else
            {
                if (_errorMessage != null)
                    _errorMessage("Letter isn't correct!");
                else
                    Console.WriteLine("Letter isn't correct!");

                return null;
            }
        }

        //Функция расшифровки кода
        public String Decode(String stringMorse) 
        {
            //stringLetter - введённое сообщение
            _string = stringMorse;

            //Если сообщение введено корректно (только '-' и '.'), выполняем перевод
            if (IsCode() == true) 
            {
                _string = _string.Trim(); //Удаление пробелов спереди и в конце сообщения

                String resultString = null; //Результирующая строка в виде сообщения на азбуке Морзе
                String shifr = null; //Строка для хранения шифров 
                int length = _string.Length; //Длина сообщения
                int index = 0, //Индекс для итераций в циклах
                    countSpaces = 0; //Количество пробелов

                while (index < length)
                {
                    //Считываение шифра
                    if (_string[index] != ' ')
                    {
                        shifr += _string[index];

                        //Если встречается символ переноса строки, этот символ заносится в результирующую строку
                        if (shifr == "\n")
                        {
                            resultString += "\n";
                            shifr = null;
                        }
                    }
                    else {
                        //Устранение лишних пробелов внутри сообщения
                        if (shifr == null) {
                            if (countSpaces > 0)
                            {
                                index++;
                                continue;
                            }

                            resultString += " ";
                            countSpaces++;
                        }
                        else
                        {
                            countSpaces = 0;

                            //Перевод шифра в символ на латинице
                            try 
                            {
                                resultString += alphaviteDictionary[shifr];
                                shifr = null;
                            }
                            //Если в сообщение содержится несуществующий шифр, выводим ошибку
                            catch (Exception)
                            {
                                if (_errorMessage != null)
                                    _errorMessage("Letter isn't correct!");
                                else
                                    Console.WriteLine("Letter isn't correct!");

                                return null;
                            }
                        }
                    }
                    
                    index++;
                }

                //Считывание последнего шифра в записи
                try
                {
                    resultString += alphaviteDictionary[shifr];
                    shifr = null;
                }
                //Если в сообщение содержится несуществующий шифр, выводим ошибку
                catch (Exception)
                {                    
                    if (_errorMessage != null)
                        _errorMessage("Letter isn't correct!");
                    else
                        Console.WriteLine("Letter isn't correct!");

                    return null;
                }
                    
                return resultString;
            }
            else 
            {
                if (_errorMessage != null)
                    _errorMessage("Letter isn't correct!");
                else
                    Console.WriteLine("Letter isn't correct!");

                return null;
            }
        }
    }
}
```
