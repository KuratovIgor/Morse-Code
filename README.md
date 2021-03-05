# Morse-Code library
Библиотека позволяет выполнять перевод сообщения на латинице в сообщение на азбуке Морзе и наоборот.
-----------
```
using System;

namespace LibMorseCode
{
    public static class MorseCode {
        private static String _string; //Переменная, хранящая сообщение
        //Массив, содержащий латинский алфавит и цифры
        private static char[] _arrayLetter = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
                                               'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2',
                                               '3', '4', '5', '6', '7', '8', '9', '0'};
        //Массив шифров из азбуки Морзе
        private static String[] _arrayMorse = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", 
                                                "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", 
                                                "..-", "...-", ".--", "-..-", "-.--", "--.", ".----", "..---", "...--",
                                                "....-", ".....", "-....", "--...", "---..", "----.", "-----"};

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
        private static bool IsLetter() {
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

        //Функция перевода сообщения на латинице в сообщение на азбуке Морзе
        public static String TranslateToMorse(String stringLetter) { 
            //stringLetter - введённое сообщение
            _string = stringLetter;
            
            //Если сообщение введено корректно, выполняем перевод
            if (IsLetter() == true) {
                _string = _string.Trim(); //Удаление пробелов спереди и в конце сообщения
                _string = _string.ToUpper(); 
                String resultString = "\0"; //Результирующая строка в виде сообщения на азбуке Морзе
                int length = _string.Length; //Длина сообщения
                int index = 0, //Индекс для итераций в циклах
                    indexFromArrays = 0, //Индекс для итераций по массивам латинского алфавита и азбуки Морзе
                    countSpaces = 0; //Количество пробелов

                while (index < length) {
                    //Перевод символа из сообщения в шифр из азбуки Морзе
                    while (indexFromArrays < _arrayLetter.Length) {
                        if (_string[index] == _arrayLetter[indexFromArrays]) {                         
                            resultString += _arrayMorse[indexFromArrays] + " ";
                            countSpaces = 0;
                            break;
                        }

                        indexFromArrays++;
                    }
                    
                    //Устранение лишних пробелов внутри сообщения
                    if (_string[index] == ' ') {
                        if (countSpaces > 0) {
                            index++;
                            indexFromArrays = 0;
                            continue;
                        }

                        resultString += "  ";
                        countSpaces++;
                    }

                    index++;
                    indexFromArrays = 0;
                }

                return resultString;
            }
            else {
                Console.WriteLine("Letter isn't correct!");
                return "\0";
            }
        }

        //Функция перевода сообщения на абуке Морзе в сообщение на латинице
        public static String TranslateToLetter(String stringMorse) {
            //stringLetter - введённое сообщение
            _string = stringMorse;

            //Если сообщение введено корректно (только '-' и '.'), выполняем перевод
            if (IsMorse() == true) {
                _string = _string.Trim(); //Удаление пробелов спереди и в конце сообщения
                String resultString = "\0"; //Результирующая строка в виде сообщения на азбуке Морзе
                String bufString = ""; //Строка для хранения шифров символов из азбуки Морзе
                int length = _string.Length; //Длина сообщения
                int index = 0, //Индекс для итераций в циклах
                    indexFromArray = 0, //Индекс для итераций по массивам латинского алфавита и азбуки Морзе
                    countSpaces = 0; //Количество пробелов
                bool isCorrect = false; //Выполняет проверки на правильность введённых кодов

                while (index < length) {
                    //Считываение шифра
                    if (_string[index] != ' ') 
                        bufString += _string[index];
                    else {
                        //Устранение лишних пробелов внутри сообщения
                        if (bufString == "") {
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
                            while (indexFromArray < _arrayMorse.Length) {
                                if (bufString == _arrayMorse[indexFromArray]) {
                                    isCorrect = true;
                                    resultString += _arrayLetter[indexFromArray];
                                    bufString = "";
                                    indexFromArray = 0;
                                    break;
                                }
                                
                                indexFromArray++;
                            }

                            //Если в сообщение содержится несуществующий шифр, выводим ошибку
                            if (isCorrect == false) {
                                Console.WriteLine("Letter isn't correct!");
                                return "\0";
                            }

                            isCorrect = false;
                        }
                    }
                    
                    index++;
                }

                //Перевод последнего шифра из сообщения на азбуке Морзе в символ на латинице
                while (indexFromArray < _arrayMorse.Length) {
                    if (bufString == _arrayMorse[indexFromArray]) {
                        isCorrect = true;
                        resultString += _arrayLetter[indexFromArray];
                        break;
                    }

                    indexFromArray++;
                }

                //Если в последнем шифре сообщения содержится несуществующий шифр, выводим ошибку
                if (isCorrect == false) {
                    Console.WriteLine("Letter isn't correct!");
                    return "\0";
                }

                return resultString;
            }
            else {
                Console.WriteLine("Letter isn't correct!");
                return "\0";
            }
        }
    }
}
```
