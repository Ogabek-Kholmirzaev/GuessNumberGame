using System.Runtime.CompilerServices;
using TaskWebAPI.Data;
using TaskWebAPI.Entities;

namespace TaskWebAPI.Services;

public static class CalculationService
{
    public static int GenerateSecretNumber()
    {
        var numbers = new List<int>();

        for (var i = 1000; i < 10000; i++)
        {
            if(IsAllDigitsDifferent(i))
                numbers.Add(i);
        }

        return numbers[new Random().Next(numbers.Count)];
    }

    public static bool IsAllDigitsDifferent(int number)
    {
        var numberString = number.ToString();
        var chars = numberString.ToCharArray();

        Array.Sort(chars);
        
        var sortedNumberString = new string(chars);

        for (var i = 1; i < sortedNumberString.Length; i++)
        {
            if (sortedNumberString[i] == sortedNumberString[i - 1]) 
                return false;
        }

        return true;
    }

    public static string CheckGuessNumber(int secretNumber, int guessNumber)
    {
        var m = 0;
        var p = 0;
        var secretNumberString = secretNumber.ToString();
        var guessNumberString = guessNumber.ToString();

        for (var i = 0; i < secretNumberString.Length; i++)
        {
            if (secretNumberString[i] == guessNumberString[i])
            {
                p++;
                continue;
            }

            for (var j = 0; j < guessNumberString.Length; j++)
            {
                if (secretNumberString[i] == guessNumberString[j])
                {
                    m++;
                    continue;
                }
            }
        }

        return $"M:{m}; P:{p}";
    }
}