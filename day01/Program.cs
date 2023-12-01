using System;
using System.Xml;
using System.Linq;
using System.Data;

namespace AOC_Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var entries = File.ReadAllLines(@"C:\Users\markz\repos\AdventOfCode2023\day01\input.txt");
            var ints = new int[entries.Length];
            for(int i = 0; i < entries.Length; i++)
            {
                ints[i] = getNumberAsString(replaceWordsWithDigits(entries[i]));
            }
            Console.WriteLine($"{ints.Sum()}");
            File.WriteAllLines(@".\output.txt", entries);
        }

        static string replaceWordsWithDigits(string str)
        {
            var repacementRules = new List<ReplacementRule>();
            repacementRules.AddRange(replaceFirstLetterOfDigit(str, "one",   '1'));
            repacementRules.AddRange(replaceFirstLetterOfDigit(str, "two",   '2'));
            repacementRules.AddRange(replaceFirstLetterOfDigit(str, "three", '3'));
            repacementRules.AddRange(replaceFirstLetterOfDigit(str, "four",  '4'));
            repacementRules.AddRange(replaceFirstLetterOfDigit(str, "five",  '5'));
            repacementRules.AddRange(replaceFirstLetterOfDigit(str, "six",   '6'));
            repacementRules.AddRange(replaceFirstLetterOfDigit(str, "seven", '7'));
            repacementRules.AddRange(replaceFirstLetterOfDigit(str, "eight", '8'));
            repacementRules.AddRange(replaceFirstLetterOfDigit(str, "nine",  '9'));

            str = applyReplacementRules(str, repacementRules);

            return str;
        }

        static string applyReplacementRules(string str, IList<ReplacementRule> ruleset)
        {
            var chars = str.ToCharArray();
            foreach (var rule in ruleset)
            {
                chars[rule.Index] = rule.Character;
            }
            return new string(chars);
        }

        static List<ReplacementRule> replaceFirstLetterOfDigit(string str, string search, char replace)
        {
            int i = 0;
            List<ReplacementRule> rules = new();
            var chars = str.ToCharArray();
            while ((i = str.IndexOf(search, i)) >= 0)
            {
                chars[i] = replace;
                str = new string(chars);
                rules.Add(new ReplacementRule(i, replace));
            }
            return rules;
        }

        static int getNumberAsString(string str)
        {
            var chrs = new char[2];
            for (int i = 0; i < str.Length; i++)
            {
                chrs[0] = str[i];
                if (int.TryParse(chrs[0].ToString(), out _))
                {
                    break;
                }
            }

            for (int i = str.Length - 1; i >= 0; i--)
            {
                chrs[1] = str[i];
                if (int.TryParse(chrs[1].ToString(), out _))
                {
                    break;
                }
            }

            return Convert.ToInt32(new string(chrs));
        }
    }

    struct ReplacementRule
    {
        public int Index {get;}
        public char Character {get;}
        public ReplacementRule(int index, char character)
        {
            Index = index;
            Character = character;
        }
    }
}