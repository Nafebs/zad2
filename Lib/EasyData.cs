using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base36Converter;
using System.Windows;

namespace Lib
{
    public class EasyData
    {
        List<int[]> ints = new List<int[]>();
        List<string> strings = new List<string>();
        public string aaa;

        public EasyData(string filename)
        {
            StreamReader streamReader = new StreamReader(filename);
            string data = streamReader.ReadToEnd();
            streamReader.Close();

            foreach (string line in data.Split('\n'))
            {
                if (string.IsNullOrEmpty(line)) continue;
                
                string[] splited = line.Split(';');
                string number = splited[0];
                string name = splited[1];

                if (name == "0")
                {
                    name = "Не используется";
                }

                int[] nums = new int[2];
                string[] nspl = number.Split('-');
                for (int i = 0; i < nums.Length; i++)
                {
                    nums[i] = (int)Converter.decode(nspl[0]);
                }

                if (nums[0] > nums[1])
                {
                    int temp = nums[0];
                    nums[0] = nums[1];
                    nums[1] = temp;
                }

                Console.WriteLine($"[{nums[0]}, {nums[1]}] - {name}");

                ints.Add(nums);
                strings.Add(name);
            }
        }

        public string get(int number)
        {
            int ind = -1;
            for (int i = 0; i < ints.Count; i++)
            {
                int[] values = ints[i];
                
                if (number >= values[0] && number <= values[1])
                {
                    ind = i;
                }
            }

            if (ind == -1)
                return "Число не попадает в заданные границы";

            return strings[ind];
        }
    }
    
    public class VinCalculator
    {
        static EasyData countries;
        static EasyData сontinents;
        static bool inited = false;

        public static void init(string path)
        {
            if (inited) return;
            
            countries = new EasyData(path + "\\countries.txt");
            сontinents = new EasyData(path + "\\сontinents.txt");
            inited = true;
        }

        public static bool checkVin(string vinNumber)
        {
            if (!inited)
                throw new Exception("Нужно инициализировать пути для файлов");

            if (vinNumber.Length != 17)
                return false;
            
            var result = 0;
            var index = 0;
            var checkDigit = 0;
            var checkSum = 0;
            var weight = 0;
            
            foreach (char c in vinNumber.ToLower().ToCharArray())
            {
                index++;
                string character = c.ToString();
                
                if (char.IsNumber(c))
                    result = int.Parse(character);
                
                else
                {
                    switch (character)
                    {
                        case "a":
                        case "j":
                            result = 1;
                            break;
                        
                        case "b":
                        case "k":
                        case "s":
                            result = 2;
                            break;
                        
                        case "c":
                        case "l":
                        case "t":
                            result = 3;
                            break;
                        
                        case "d":
                        case "m":
                        case "u":
                            result = 4;
                            break;
                        
                        case "e":
                        case "n":
                        case "v":
                            result = 5;
                            break;
                        
                        case "f":
                        case "w":
                            result = 6;
                            break;
                        
                        case "g":
                        case "p":
                        case "x":
                            result = 7;
                            break;
                        
                        case "h":
                        case "y":
                            result = 8;
                            break;
                        
                        case "r":
                        case "z":
                            result = 9;
                            break;
                    }
                }

                if (index >= 1 && index <= 7 || index == 9)
                    weight = 9 - index;
                
                else if (index == 8)
                    weight = 10;
                
                else if (index >= 10 && index <= 17)
                    weight = 19 - index;
                
                if (index == 9)
                    checkDigit = character == "x" ? 10 : result;
                
                checkSum += (result * weight);
            }

            return checkSum % 11 == checkDigit;
        }

        public static string getVINCountry(string vinNumber)
        {
            if (!checkVin(vinNumber))
            {
                throw new Exception("Введен не правильный номер машины");
            }

            string[] n = new string[2];

            n[0] = сontinents.get((int)Converter.decode(vinNumber[0].ToString()));
            n[1] = countries.get((int)Converter.decode(vinNumber.Substring(1, 2)));

            return $"{n[0]} - {n[1]}";
        }

        public static int getTransortYear(string vinNumber)
        {
            if (!checkVin(vinNumber))
            {
                throw new Exception("Введен не правильный номер машины");
            }

            return 1;
        }
    }
}
