using System;
using System.Collections.Generic;

namespace square_9
{
    class IntToWord
    {
        //Dictionary of number words for 0-9
        public static Dictionary<char, String> onesWords = new Dictionary<char, String>(){
                {'0', ""},
                {'1',"One"},
                {'2', "Two"},
                {'3', "Three"},
                {'4', "Four"},
                {'5', "Five"},
                {'6', "Six"},
                {'7', "Seven"},
                {'8', "Eight"},
                {'9', "Nine"}
        };

        //Dictionary of number words for 10-19
        public static Dictionary<char, String> teensWords = new Dictionary<char, String>(){
                {'0', "Ten"},
                {'1', "Eleven"},
                {'2', "Twelve"},
                {'3', "Thirteen"},
                {'4', "Fourteen"},
                {'5', "Fifteen"},
                {'6', "Sixteen"},
                {'7', "Seventeen"},
                {'8', "Eighteen"},
                {'9', "Nineteen"}
        };

        //Dictionary of number words for 20-90
        public static Dictionary<char, String> tensWords = new Dictionary<char, String>(){
                {'2', "Twenty"},
                {'3', "Thirty"},
                {'4', "Forty"},
                {'5', "Fifty"},
                {'6', "Sixty"},
                {'7', "Seventy"},
                {'8', "Eighty"},
                {'9', "Ninety"}
            };

        //Array of place value words for Hundred, Thousand, etc.
        public static String[] placeValueWords = new String[] {
                "",
                "",
                " Hundred",
                " Thousand",
                "",
                " Hundred",
                " Million",
                "",
                " Hundred",
                " Billion"
            };

        //Main Loop
        //Reads input (assumes valid input)
        //Converts input, returns word form
        //Loops until "done" is received
        static void Main(string[] args)
        {
            Console.WriteLine("Please input an integer: ");
            String value = Console.ReadLine();

            while (value != "done")
            {
                Console.WriteLine(ConvertToWords(value));

                Console.WriteLine("Please input an integer (Type done to quit): ");
                value = Console.ReadLine();
            }
        }

        //Takes user string
        //Increments through string by character, building the output 
        //by calling helper methods
        static String ConvertToWords(String userInput)
        {
            //ouput string
            String wordForm = "";

            //adjusts length to last index of input
            int count = userInput.Length - 1;

            //bool to track if place value label is needed, default set by looking at length of input
            //set to true if input = 1000+
            bool labelNeeded = (userInput.Length >= 3);

            //increments to length of input
            for (int x = 0; x < userInput.Length; x++)
            {
                //Checks to see if looking at the tens place
                //N % 3 == 2 means the tens place of any tuple (hundred, tens, ones)
                if ((count + 1) % 3 == 2)
                {
                    //switch case to check for special cases (0 tens or 10-19)
                    switch (userInput[x])
                    {
                        //0 tens: get the ones word, skip the ones count, increase x counter
                        case '0':
                            count--;
                            break;

                        //10-19: send ones place to teens dictionary, skip the ones count, increase x counter
                        case '1':
                            Console.WriteLine("One - Getting the teens");
                            wordForm += getTeensWord(userInput[x + 1]) + getPlaceValueWord(count - 1) + " ";
                            count -= 2;
                            x++;
                            labelNeeded = true;
                            break;

                        //20 - 90: get word from tens dictionary, decrease count
                        default:
                            Console.WriteLine("Two - Getting the ones");
                            wordForm += getTensWord(userInput[x]) + getPlaceValueWord(count) + " ";
                            count--;
                            labelNeeded = true;
                            break;
                    }
                }
                //handles the hundreds place
                else if ((count + 1) % 3 == 0)
                {
                    if (userInput[x] == '0')
                    {
                        wordForm += getOnesWord(userInput[x]) + " ";
                        count--;
                        labelNeeded = false;
                    }
                    else
                    {
                        wordForm += getOnesWord(userInput[x]) + getPlaceValueWord(count) + " ";
                        count--;
                        labelNeeded = true;
                    }
                }
                else
                {
                    if (labelNeeded)
                    {
                        wordForm += getOnesWord(userInput[x]) + getPlaceValueWord(count) + " ";
                        count--;
                        labelNeeded = false;
                    }
                    else
                    {
                        wordForm += getOnesWord(userInput[x]) + " ";
                        count--;
                    }
                }
            }

            //return word form of user input
            return wordForm;

            //returns word for ones & hundreds place
            String getOnesWord(char index)
            {
                if (index == '0')
                {
                    return "";
                }
                return onesWords[index];
            }

            //returns word for teens (10-19)
            String getTeensWord(char index)
            {
                return teensWords[index];
            }

            //returns word for tens place
            String getTensWord(char index)
            {
                return tensWords[index];
            }

            //returns word from place value array
            String getPlaceValueWord(int index)
            {
                return placeValueWords[index];
            }
        }
    }
}
