# Morse-Code library
Библиотека позволяет выполнять перевод сообщения на латинице в сообщение на азбуке Морзе и наоборот.
-----------
```
using System;
using System.Collections;
using System.Collections.Generic;

namespace LibMorseCode
{
    public static class MorseCode {
        private static String _string; //Переменная, хранящая сообщение
        //Словарь азбуки Морзе для латинского алфавита
        private static Dictionary<char, string> dictionaryOfMorseEn = new Dictionary<char, string> {
            {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."}, {'F', "..-."},
            {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"}, {'K', "-.-"}, {'L', ".-.."},
            {'M', "--"}, {'N', "-."}, {'O', "---"}, {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."},
            {'S', "..."}, {'T', "-"}, {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"},
            {'Y', "-.--"}, {'Z', "--."}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"},
            {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."}, {'0', "-----"}
        };
        //Словарь азбуки Морзе для русского алфавита
        private static Dictionary<char, string> dictionaryOfMorseRus = new Dictionary<char, string> {
            {'А', ".-"}, {'Б', "-..."}, {'В', ".--"}, {'Г', "--."}, {'Д', "-.."}, {'Е', "."}, 
            {'Ж', "...-"}, {'З', "--.."}, {'И', ".."}, {'Й', ".---"}, {'К', "-.-"}, {'Л', ".-.."}, 
            {'М', "--"}, {'Н', "-."}, {'О', "---"}, {'П', ".--."}, {'Р', ".-."}, {'С', "..."}, 
            {'Т', "-"}, {'У', "..-"}, {'Ф', "..-."}, {'Х', "...."}, {'Ц', "-.-."}, {'Ч', "---."},
            {'Ш', "----"}, {'Щ', "--.-"}, {'Ъ', ".--.-."}, {'Ы', "-.--"}, {'Ь', "-..-"}, {'Э', "...-..."},
            {'Ю', "..--"}, {'Я', ".-.-"}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"},
            {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."}, {'0', "-----"}
        };
        //Словарь латинского алфавита
        private static Dictionary<string, char> dictionaryOfEnglishAlph = new Dictionary<string, char> {
            {".-", 'A'}, {"-...", 'B'}, {"-.-.", 'C'}, {"-..", 'D'}, {".", 'E'}, {"..-.", 'F'}, 
            {"--.", 'G'}, {"....", 'H'}, {"..", 'I'}, {".---", 'J'}, {"-.-", 'K'}, {".-..", 'L'},
            {"--", 'M'}, {"-.", 'N'}, {"---", 'O'}, {".--.", 'P'}, {"--.-", 'Q'}, {".-.", 'R'},
            {"...", 'S'}, {"-", 'T'}, {"..-", 'U'}, {"...-", 'V'}, {".--", 'W'}, {"-..-", 'X'}, 
            {"-.--", 'Y'}, {"", 'Z'}, {".----", '1'}, {"..---", '2'}, {"...--", '3'}, {"....-", '4'},
            {".....", '5'}, {"-....", '6'}, {"--...", '7'}, {"---..", '8'}, {"----.", '9'}, {"-----", '0'}
        };
        //Словарь русского алфавита
        private static Dictionary<string, char> dictionaryOfRussianAlph = new Dictionary<string, char> {
            {".-", 'А'}, {"-...", 'Б'}, {".--", 'В'}, {"--.", 'Г'}, {"-..", 'Д'}, {".", 'Е'},
            {"...-", 'Ж'}, {"--..", 'З'}, {"..", 'И'}, {".---", 'Й'}, {"-.-", 'К'}, {".-..", 'Л'},
            {"--", 'М'}, {"-.", 'Н'}, {"---", 'О'}, {".--.", 'П'}, {".-.", 'Р'}, {"...", 'С'},
            {"-", 'Т'}, {"..-", 'У'}, {"..-.", 'Ф'}, {"....", 'Х'}, {"-.-.", 'Ц'}, {"---.", 'Ч'},
            {"----", 'Ш'}, {"--.-", 'Щ'}, {".--.-.", 'Ъ'}, {"-.--", 'Ы'}, {"-..-", 'Ь'}, {"...-...", 'Э'},
            {"..--", 'Ю'}, {".-.-", 'Я'}, {".----", '1'}, {"..---", '2'}, {"...--", '3'}, {"....-", '4'},
            {".....", '5'}, {"-....", '6'}, {"--...", '7'}, {"---..", '8'}, {"----.", '9'}, {"-----", '0'}
        };

        //Функция проверки коррекстности ввода сообщения на азбуке Морзе
        private static bool IsMorse() {
            int length = _string.Length; //Длина сообщения
            int index = 0;

            //Сообщение должно содержать только символы '-', '.' и пробел
            while (index < length) {
                if (_string[index] == '-' || _string[index] == '.' || _string[index] == ' ')
                    index++;
                else
                    return false;
            }

            return true;
        }

        //Функция проверки коррекстности ввода сообщения на латинице
        private static bool IsEnLetter() {
            int length = _string.Length; //Длина сообщения
            int index = 0;

            //Сообщение должно содержать только буквы латинского алфавита и цифры
            while (index < length) {
                if ((_string[index] >= 'A' && _string[index] <= 'Z') ||
                    (_string[index] >= 'a' && _string[index] <= 'z') ||
                    (_string[index] >= '0' && _string[index] <= '9') ||
                    _string[index] == ' ')
                    index++;
                else
                    return false;
            }

            return true;
        }

        //Функция проверки коррекстности ввода сообщения на латинице
        private static bool IsRusLetter()
        {
            int length = _string.Length; //Длина сообщения
            int index = 0;

            //Сообщение должно содержать только буквы латинского алфавита и цифры
            while (index < length)
            {
                if ((_string[index] >= 'А' && _string[index] <= 'Я') ||
                    (_string[index] >= 'а' && _string[index] <= 'я') ||
                    (_string[index] >= '0' && _string[index] <= '9') ||
                    _string[index] == ' ')
                    index++;
                else
                    return false;
            }

            return true;
        }

        //Функция перевода сообщения на латинице в сообщение на азбуке Морзе
        public static String TranslateToMorseEn(String stringLetter) { 
            //stringLetter - введённое сообщение
            _string = stringLetter;
            
            //Если сообщение введено корректно, выполняем перевод
            if (IsEnLetter() == true) {
                _string = _string.Trim(); //Удаление пробелов спереди и в конце сообщения
                _string = _string.ToUpper(); 
                String resultString = "\0"; //Результирующая строка в виде сообщения на азбуке Морзе
                int length = _string.Length; //Длина сообщения
                int index = 0, //Индекс для итераций в циклах
                    countSpaces = 0; //Количество пробелов

                while (index < length) {
                    //Перевод символа из сообщения в шифр из азбуки Морзе
                    if (_string[index] != ' ') {
                        resultString += dictionaryOfMorseEn[_string[index]];
                        resultString += " ";
                        countSpaces = 0;
                    }
                    //Устранение лишних пробелов внутри сообщения
                    else {
                        if (countSpaces > 0) {
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
            else {
                Console.WriteLine("Letter isn't correct!");
                return "\0";
            }
        }

        //Функция перевода сообщения на русском языке в сообщение на азбуке Морзе
        public static String TranslateToMorseRus(String stringLetter)
        {
            //stringLetter - введённое сообщение
            _string = stringLetter;

            //Если сообщение введено корректно, выполняем перевод
            if (IsRusLetter() == true)
            {
                _string = _string.Trim(); //Удаление пробелов спереди и в конце сообщения
                _string = _string.ToUpper();
                String resultString = "\0"; //Результирующая строка в виде сообщения на азбуке Морзе
                int length = _string.Length; //Длина сообщения
                int index = 0, //Индекс для итераций в циклах
                    countSpaces = 0; //Количество пробелов

                while (index < length)
                {
                    //Перевод символа из сообщения в шифр из азбуки Морзе
                    if (_string[index] != ' ')
                    {
                        resultString += dictionaryOfMorseRus[_string[index]];
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
                Console.WriteLine("Letter isn't correct!");
                return "\0";
            }
        }

        //Функция перевода сообщения на абуке Морзе в сообщение на латинице
        public static String TranslateToEnLetter(String stringMorse) {
            //stringLetter - введённое сообщение
            _string = stringMorse;

            //Если сообщение введено корректно (только '-' и '.'), выполняем перевод
            if (IsMorse() == true) {
                _string = _string.Trim(); //Удаление пробелов спереди и в конце сообщения
                String resultString = "\0"; //Результирующая строка в виде сообщения на азбуке Морзе
                String shifr = ""; //Строка для хранения шифров символов из азбуки Морзе
                int length = _string.Length; //Длина сообщения
                int index = 0, //Индекс для итераций в циклах
                    countSpaces = 0; //Количество пробелов

                while (index < length) {
                    //Считываение шифра
                    if (_string[index] != ' ')
                        shifr += _string[index];
                    else {
                        //Устранение лишних пробелов внутри сообщения
                        if (shifr == "") {
                            if (countSpaces > 0) {
                                index++;
                                continue;
                            }

                            resultString += " ";
                            countSpaces++;
                        }
                        else {
                            countSpaces = 0;

                            //Перевод шифра из сообщения на азбуке Морзе в символ на латинице
                            try {
                                resultString += dictionaryOfEnglishAlph[shifr];
                                shifr = "";
                            }
                            //Если в сообщение содержится несуществующий шифр, выводим ошибку
                            catch (Exception) {
                                Console.WriteLine("Letter isn't correct!");
                                return "\0";
                                throw;
                            }
                        }
                    }
                    
                    index++;
                }

                try {
                    resultString += dictionaryOfEnglishAlph[shifr];
                    shifr = "";
                }
                //Если в сообщение содержится несуществующий шифр, выводим ошибку
                catch (Exception) {
                    Console.WriteLine("Letter isn't correct!");
                    return "\0";
                    throw;
                }

                return resultString;
            }
            else {
                Console.WriteLine("Letter isn't correct!");
                return "\0";
            }
        }

        //Функция перевода сообщения на абуке Морзе в сообщение на русском языке
        public static String TranslateToRusLetter(String stringMorse)
        {
            //stringLetter - введённое сообщение
            _string = stringMorse;

            //Если сообщение введено корректно (только '-' и '.'), выполняем перевод
            if (IsMorse() == true)
            {
                _string = _string.Trim(); //Удаление пробелов спереди и в конце сообщения
                String resultString = "\0"; //Результирующая строка в виде сообщения на азбуке Морзе
                String shifr = ""; //Строка для хранения шифров символов из азбуки Морзе
                int length = _string.Length; //Длина сообщения
                int index = 0, //Индекс для итераций в циклах
                    countSpaces = 0; //Количество пробелов

                while (index < length)
                {
                    //Считываение шифра
                    if (_string[index] != ' ')
                        shifr += _string[index];
                    else
                    {
                        //Устранение лишних пробелов внутри сообщения
                        if (shifr == "")
                        {
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

                            //Перевод шифра из сообщения на азбуке Морзе в символ на латинице
                            try
                            {
                                resultString += dictionaryOfRussianAlph[shifr];
                                shifr = "";
                            }
                            //Если в сообщение содержится несуществующий шифр, выводим ошибку
                            catch (Exception)
                            {
                                Console.WriteLine("Letter isn't correct!");
                                return "\0";
                                throw;
                            }
                        }
                    }

                    index++;
                }

                try
                {
                    resultString += dictionaryOfRussianAlph[shifr];
                    shifr = "";
                }
                //Если в сообщение содержится несуществующий шифр, выводим ошибку
                catch (Exception)
                {
                    Console.WriteLine("Letter isn't correct!");
                    return "\0";
                    throw;
                }

                return resultString;
            }
            else
            {
                Console.WriteLine("Letter isn't correct!");
                return "\0";
            }
        }
    }
}
```
