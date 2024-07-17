// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;

const string commandName = "wc";

Console.Write(">");
string? input = Console.ReadLine();

if (input is null)
{
    Console.WriteLine("Input cannot be empty");
}

var inputs = input?.Split([' ']);

if(inputs?.Length == 0)  
{
    Console.WriteLine("Command is not recognized");
    return;
}

var inCmdName = inputs?[0];
if (inCmdName != commandName)
{
    Console.WriteLine("Command is not recognized");
    return;
}

var inCmdLine = string.Empty;
var fileName = string.Empty;

if(inputs?.Length == 3)
{
    inCmdLine = inputs?[1] ?? "";
    fileName = inputs?[2] ?? "";
}
else if(inputs?.Length == 2)
{
    fileName = inputs?[1] ?? "";
}


switch(inCmdLine)
{
    case "-c":
        var size = CountBytes(fileName);
        Console.WriteLine(size + " bytes");
        break;
    case "-l":
        var lines = CountLines(fileName);
        Console.WriteLine(lines + " lines");
        break;
    case "-w":
        var words = CountWords(fileName);
        Console.WriteLine(words + " words");
        break;
    case "-m":
        var characters = CountCharacters(fileName);
        Console.WriteLine(characters + " characters");
        break;
    default:
        size = CountBytes(fileName);
        lines = CountLines(fileName);
        characters = CountCharacters(fileName);
        Console.WriteLine(size + " " + lines + " " + characters + " " + fileName);
        break;
}

static long CountBytes(string? fileName)
{
    if (fileName is null)
    {
        throw new NullReferenceException("No file is provided");
    }

    return new FileInfo(fileName).Length;
} 

static long CountLines(string? fileName)
{
    if (fileName is null)
    {
        throw new NullReferenceException("No file is provided");
    }

    return File.ReadLines(fileName).Count();
} 

static long CountWords(string? fileName)  
{
    if (fileName is null)
    {
        throw new NullReferenceException("No file is provided");
    }

    StreamReader sr = new(fileName);

      int counter = 0;
      string delim = " ,."; //maybe some more delimiters like ?! and so on
      string[]? fields = null;
      string? line = null;

      while(!sr.EndOfStream)
      {
         line = sr.ReadLine();//each time you read a line you should split it into the words
         line?.Trim();
         fields = line?.Split(delim.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
         counter+=fields!.Length; //and just add how many of them there is
      }
      sr.Close();

      return counter;
}

static long CountCharacters(string? fileName)
{
     if (fileName is null)
    {
        throw new NullReferenceException("No file is provided");
    }

    return File.ReadAllText(fileName).
    Count(c => c == ' ' ||
    (!char.IsControl(c) &&
     !char.IsWhiteSpace(c)));
}




