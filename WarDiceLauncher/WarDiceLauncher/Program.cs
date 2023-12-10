internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("\nHello, welcome to the game!");
        string diceOfAttack = string.Empty;
        string diceOfDefese = string.Empty;
        int numberDiceOfAttack;
        int numberDiceOfDefese;

        while (!int.TryParse(diceOfAttack, out numberDiceOfAttack))
        {
            Console.WriteLine("Number of attack dice:\n");
            diceOfAttack = Console.ReadLine();

        }
        while (!int.TryParse(diceOfDefese, out numberDiceOfDefese))
        {
            Console.WriteLine("Number of defese dice:\n");
            diceOfDefese = Console.ReadLine();
        }
        int length = numberDiceOfAttack > numberDiceOfDefese ? numberDiceOfAttack : numberDiceOfDefese;

        (Array attack, Array defense) dices = RollTheDice(numberDiceOfAttack, numberDiceOfDefese);
        OrderDices(dices);
        int[,] result = CheckResult(dices, length);
        (int countAttacks, int countDefenses, string[] dicesResult) result2 = ResultToString(result, length);
        
        PrintResult(result2);

        Console.WriteLine("\nNew GAME? (y - Yes)");
        string input = Console.ReadLine();
        if (input == "y")
            Main(args);
    }

    private static (Array attack, Array defense) RollTheDice(int numberDiceOfAttack, int numberDiceOfDefese)
    {
        int[] attack = new int[numberDiceOfAttack];
        int[] defense = new int[numberDiceOfDefese];
        
        for (int i = 0; i < numberDiceOfAttack; i++)
            attack[i] = new Random().Next(1, 7);
        for (int i = 0; i < numberDiceOfDefese; i++)
            defense[i] = new Random().Next(1, 7);
        
        return (attack, defense);
    }

    private static void OrderDices((Array attack, Array defense) dices)
    {
        Array.Sort(dices.attack);
        Array.Reverse(dices.attack);
        Array.Sort(dices.defense);
        Array.Reverse(dices.defense);
    }

    private static int[,] CheckResult((Array attack, Array defense) dices, int length)
    {
        int[,] result = new int[length, 2];
        for (int i = 0; i < length; i++)
        {
            if (dices.attack.Length > i)
                result[i, 0] = (int)dices.attack.GetValue(i);
            else
                result[i, 0] = 0;
        }
        for (int i = 0; i < length; i++)
        {
            if (dices.defense.Length > i)
                result[i, 1] = (int)dices.defense.GetValue(i);
            else
                result[i, 1] = 0;
        }
        return result;
    }

    private static (int countAttacks, int countDefenses, string[] dicesResult) ResultToString(int[,] dices, int length)
    {
        int countAttacks = 0;
        int countDefenses = 0;
        string[] dicesResult = new string[length];

        for (int i = 0; i < length; i++)
        {
            int? diceAttack = dices[i,0];
            int? diceDefense = dices[i,1];

            if (diceAttack == 0 || diceDefense == 0)
            {
                dicesResult[i] = diceAttack + "  ...  " + diceDefense;
            }
            else if (diceAttack > diceDefense)
            {
                countAttacks++;
                dicesResult[i] = diceAttack + "   >   " + diceDefense;
            }
            else if (diceAttack < diceDefense)
            {
                countDefenses++;
                dicesResult[i] = diceAttack + "   <   " + diceDefense;
            }
            else if (diceAttack == diceDefense)
            {
                countDefenses++;
                dicesResult[i] = diceAttack + "   ==  " + diceDefense;
            }
        }
        return (countAttacks, countDefenses, dicesResult);
    }

    private static void PrintResult((int countAttacks, int countDefenses, string[] dicesResult) result2)
    {
        Console.WriteLine("\nAttac\tDefese");
        for (int i = 0; i < result2.dicesResult.Length; i++)
        {
            Console.WriteLine("  " + result2.dicesResult[i]);
        }
        Console.WriteLine("\nkilled: " + result2.countAttacks);
        Console.WriteLine("Died: " + result2.countDefenses);
    }
}