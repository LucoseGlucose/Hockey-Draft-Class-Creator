using System;
using System.IO;
using System.Collections.Generic;

namespace DraftClassGenerator
{
    class Program
    {
        static void Main()
        {
            string[] nationalities;
            string[] positions;
            string[] firstNamePaths;
            string[] lastNamePaths;
            string folderPath = "C:/Users/LJorg/source/repos/ConsoleApp1/Resource Files/";

            if (File.Exists(folderPath + "Nationalities.txt")) nationalities = File.ReadAllLines(folderPath + "Nationalities.txt");
            else throw new FileNotFoundException();

            if (File.Exists(folderPath + "Positions.txt")) positions = File.ReadAllLines(folderPath + "Positions.txt");
            else throw new FileNotFoundException();

            if (File.Exists(folderPath + "First Name Paths.txt")) firstNamePaths = File.ReadAllLines(folderPath + "First Name Paths.txt");
            else throw new FileNotFoundException();

            if (File.Exists(folderPath + "Last Name Paths.txt")) lastNamePaths = File.ReadAllLines(folderPath + "Last Name Paths.txt");
            else throw new FileNotFoundException();

            string[] variableInfo;
            if (File.Exists(folderPath + "Variables.txt")) variableInfo = File.ReadAllLines(folderPath + "Variables.txt");
            else throw new FileNotFoundException();

            int players = int.Parse(variableInfo[0]);
            string seperator = variableInfo[1];
            int baseWeight = int.Parse(variableInfo[2]);
            int baseHeight = int.Parse(variableInfo[3]);
            float heightWeightImpact = float.Parse(variableInfo[4]);
            int randHeightDeviation = int.Parse(variableInfo[5]);
            int randWeightDeviation = int.Parse(variableInfo[6]);
            float heightMultiplier = float.Parse(variableInfo[7]);

            Random rand = new Random();
            List<string> outputList = new List<string>();
            for (int i = 0; i < players; i++)
            {
                string nation = nationalities[rand.Next(0, nationalities.Length)];

                string[] possibleFirsts = GetNamesFromNation(nation, firstNamePaths, nationalities);
                string firstName = possibleFirsts[rand.Next(0, possibleFirsts.Length)];

                string[] possibleLasts = GetNamesFromNation(nation, lastNamePaths, nationalities);
                string lastName = possibleLasts[rand.Next(0, possibleLasts.Length)];

                string position = positions[rand.Next(0, positions.Length)];

                float heightInches = baseHeight + rand.Next(-randHeightDeviation, randHeightDeviation);
                string heightFormatted = ((int)(heightInches / 12)).ToString() + "," + (heightInches % 12).ToString();

                float weight = baseWeight + rand.Next(-randWeightDeviation, randWeightDeviation);
                weight += (heightInches * heightMultiplier - weight) * heightWeightImpact;

                outputList.Add((i + 1).ToString() + ". " + firstName + " " + lastName + seperator + position + seperator + nation + seperator +
                    heightFormatted + " ft" + seperator + weight.ToString() + " lb");
            }

            File.WriteAllLines(folderPath + "Output.txt", outputList.ToArray());

            foreach (string player in outputList) { Console.WriteLine(player); } 
            Console.ReadKey();
        }
        public static string[] GetNamesFromNation(string nation, string[] nationNamePaths, string[] nationalities)
        {
            int index = 0;
            for (int i = 0; i < nationalities.Length; i++)
            {
                if (nationalities[i] == nation) { index = i; break; }
            }

            string[] possibleNames;
            if (File.Exists(nationNamePaths[index])) possibleNames = File.ReadAllLines(nationNamePaths[index]);
            else throw new FileNotFoundException();

            return possibleNames;
        }
    }
}
