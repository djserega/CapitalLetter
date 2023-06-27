using System.Text;

Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;

string[] exitWord = new string[3] { "q", "quit", "вихід" };
string[] prefixes = new string[9] { "фон", "зу", "де", "ля", "ді", "да", "дер", "дель", "ла" }; // тут можливе читання з cfg файлу, куди були б внесені можливі приставки


Greeting(exitWord);

string? userText = default;
do
{
    Console.Write("Введіть ПІБ (через пробіл): ");

    Console.ForegroundColor = ConsoleColor.DarkGray;

    userText = Console.ReadLine();

    Console.ForegroundColor = ConsoleColor.Gray;

    if (!string.IsNullOrEmpty(userText))
    {
        StringBuilder builderResult = new();

        string[] separatedText = userText.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        ProcessingEnteredText(prefixes, builderResult, separatedText);


        Console.Write($"Результат: ");

        Console.ForegroundColor = ConsoleColor.DarkGray;

        Console.WriteLine(builderResult);

        Console.ForegroundColor = ConsoleColor.Gray;

        Console.WriteLine();
    }

} while (!IsExitText(exitWord, userText));



static void Greeting(string[] exitWord)
{
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine("Вітання.");
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("Консольний додаток конвертує ПІБ написаний в різному регістрі в формат: Перша велика буква, наступні маленькі.");

    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine();
    Console.WriteLine($"Для виходу із додатку використовуйте: {string.Join(", ", exitWord)}");

    Console.WriteLine();
    Console.WriteLine();
}

static void ProcessingEnteredText(string[] prefixes, StringBuilder builderResult, string[] separatedText)
{
    foreach (string word in separatedText)
    {
        string toLowerWord = word.ToLower();

        if (prefixes.Contains(toLowerWord))
        {
            builderResult.Append($"{toLowerWord}");
        }
        else if (toLowerWord.Contains('-'))
        {
            string[] separatedWords = toLowerWord.Split('-');
            for (int i = 0; i < separatedWords.Length; i++)
            {
                ConvertWord(builderResult, separatedWords[i]);

                if (i != separatedWords.Length - 1)
                    builderResult.Append('-');
            }
        }
        else
        {
            ConvertWord(builderResult, toLowerWord);
        }
        builderResult.Append(' ');
    }
}

static void ConvertWord(StringBuilder builderResult, string toLowerWord)
{
    string firstLetter = toLowerWord[..1].ToUpper();
    string otherLetter = toLowerWord[1..];

    builderResult.Append($"{firstLetter}{otherLetter}");
}

static bool IsExitText(string[] exitWord, string? userText)
{
    if (string.IsNullOrEmpty(userText))
        return false;

    return exitWord.Contains(userText.ToLower());
}
