using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace LibMorseCode
{
    public class MorseCode
    {
        //Адреса файлов со словарями
        private const String PATH_EN = @"C:\Users\kurat\source\repos\Разработка ПМ\Morse Code\EnglishDictionary.txt"; //Константа, хранящая адрес английского словаря
        private const String PATH_RUS = @"C:\Users\kurat\source\repos\Разработка ПМ\Morse Code\RussianDictionary.txt"; //Константа, хранящая адрес русского словаря
       
        private String _string; //Переменная, хранящая сообщение
        private String _language; //Язык сообщения
       
        //Словарь алфавита
        private Dictionary<string, char> alphaviteDictionary = new Dictionary<string, char> { };
        //Словарь кода Морзе
        private Dictionary<char, string> morseDictionary = new Dictionary<char, string> { };

        //Делегат для вывода сообщений об ошибках
        public delegate void ErrorMessage(String textOfError);
        ErrorMessage _errorMessage;
        
        //Конструктор
        public MorseCode (String language)
        {
            _language = language;
            String mesFromFile = null, morseCode = null;
            int index = 2, length;

            //В зависимости от того, какой язык был введен, создаются словари
            switch (_language)
            {
               case "en":
                    {
                        using (StreamReader fileStream = File.OpenText(PATH_EN)) //Создание потока на чтение
                        {
                            while (mesFromFile != "///") // "///" - конец файла
                            {
                                mesFromFile = fileStream.ReadLine(); //Чтение строки из файла

                                if (mesFromFile != "///")
                                {
                                    length = mesFromFile.Length;

                                    //Разделение ключа и значения
                                    while (index < length)
                                    {
                                        morseCode += mesFromFile[index];
                                        index++;
                                    }

                                    //Запись данных в словари
                                    alphaviteDictionary.Add(morseCode, mesFromFile[0]);
                                    morseDictionary.Add(mesFromFile[0], morseCode);
                                }

                                index = 2;
                                morseCode = "";
                            }
                        }
                    }break;
                case "rus":
                    {
                        using (StreamReader fileStream = File.OpenText(PATH_RUS)) 
                        {
                            while (mesFromFile != "///")
                            {
                                mesFromFile = fileStream.ReadLine();

                                if (mesFromFile != "///")
                                {
                                    length = mesFromFile.Length;

                                    while (index < length)
                                    {
                                        morseCode += mesFromFile[index];
                                        index++;
                                    }

                                    alphaviteDictionary.Add(morseCode, mesFromFile[0]);
                                    morseDictionary.Add(mesFromFile[0], morseCode);
                                }

                                index = 2;
                                morseCode = "";
                            }
                        }
                    }
                    break;
                default:
                    {
                        Console.WriteLine("Language isn't correct!");
                    }break;
            }
        }

        //Функция получения метода с выводом ошибки
        public void RegisterErrors(ErrorMessage errorMessage)
        {
            _errorMessage = errorMessage;
        }

        //Функция проверки коррекстности ввода сообщения на азбуке Морзе
        private bool IsMorse()
        {
            int length = _string.Length; //Длина сообщения
            int index = 0;

            //Сообщение должно содержать только символы '-', '.' и пробел
            while (index < length)
            {
                if (_string[index] == '-' || _string[index] == '.' || _string[index] == ' ')
                    index++;
                else
                    return false;
            }

            return true;
        }

        //Функция проверки коррекстности ввода сообщения на латинице
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
                        (_string[index] >= '0' && _string[index] <= '9') ||
                        (_string[index] == ',' || _string[index] == '.') ||
                        (_string[index] == '?' || _string[index] == '!') ||
                        _string[index] == ' ')
                        index++;
                    else
                        return false;
                }
            }
            if (_language == "rus")
            {
                while (index < length)
                {
                    if ((_string[index] >= 'А' && _string[index] <= 'Я') ||
                        (_string[index] >= 'а' && _string[index] <= 'я') ||
                        (_string[index] >= '0' && _string[index] <= '9') ||
                        (_string[index] == ',' || _string[index] == '.') ||
                        (_string[index] == '?' || _string[index] == '!') ||
                        _string[index] == ' ')
                        index++;
                    else
                        return false;
                }
            }
            
            return true;
        }

        //Функция шифрования сообщения в код Морзе
        public String TranslateToMorse(String stringLetter)
        { 
            //stringLetter - введённое сообщение
            _string = stringLetter;
            
            //Если сообщение введено корректно, выполняем перевод
            if (IsLetter() == true)
            {
                _string = _string.Trim(); //Удаление пробелов спереди и в конце сообщения
                _string = _string.ToUpper(); //Перевод всех символов сообщения в верхний регистр

                String resultString = null; //Результирующая строка в виде сообщения на азбуке Морзе
                int length = _string.Length; //Длина сообщения
                int index = 0, //Индекс для итераций в циклах
                    countSpaces = 0; //Количество пробелов

                while (index < length)
                {
                    //Перевод символа из сообщения в шифр на азбуке Морзе
                    if (_string[index] != ' ' && _string[index] != ',' && _string[index] != '.' &&
                        _string[index] != '!' && _string[index] != '?')
                    {
                        resultString += morseDictionary[_string[index]];
                        resultString += " ";
                        countSpaces = 0;
                    }
                    //Устранение лишних пробелов внутри сообщения
                    else
                    {
                        if (countSpaces > 0) 
                        {
                            index++;
                            continue;
                        }

                        resultString += "  ";
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

        //Функция расшифровки кода Морзе
        public String TranslateFromMorse(String stringMorse) 
        {
            //stringLetter - введённое сообщение
            _string = stringMorse;

            //Если сообщение введено корректно (только '-' и '.'), выполняем перевод
            if (IsMorse() == true) 
            {
                _string = _string.Trim(); //Удаление пробелов спереди и в конце сообщения

                String resultString = null; //Результирующая строка в виде сообщения на азбуке Морзе
                String shifr = null; //Строка для хранения шифров символов из азбуки Морзе
                int length = _string.Length; //Длина сообщения
                int index = 0, //Индекс для итераций в циклах
                    countSpaces = 0; //Количество пробелов

                while (index < length)
                {
                    //Считываение шифра
                    if (_string[index] != ' ')
                        shifr += _string[index];
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

                            //Перевод шифра из азбуки Морзе в символ на латинице
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
                                throw;
                            }
                        }
                    }
                    
                    index++;
                }

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
                    throw;
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