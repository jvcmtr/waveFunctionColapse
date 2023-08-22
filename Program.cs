using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using waveFunctionColapse1;

List<Drawable> TILES1 = new()
{   // ░▒▓█
    new Drawable(" ", " ░█"),
    new Drawable("░", " ░▒"),
    new Drawable("▒", "░▒▓"),
    new Drawable("▓", "▒▓█"),
    new Drawable("█", " ▓█")
};

List<Drawable> TILES2 = new()
{ 
    new Drawable(" ", "░█"),
    new Drawable("░", " ░▓"),
    new Drawable("▓", "░▓█"),
    new Drawable("█", "▓█")
};

// TEST HERE !!!
List<Drawable> drawableList = TILES1;
int[] DIMENTIONS = new int[] { 50, 15 };

Console.BackgroundColor = ConsoleColor.Black;
Console.ForegroundColor = ConsoleColor.White;


//______________________________ KEEP DRAWING
# region Loop
/*
int[] dimentions = new int[]{ DIMENTIONS[0], 3 };
int total = dimentions[0] * dimentions[1];
Grid map = new(dimentions);
map.Init(drawableList);

while (true)
{
    map.Run();

    Cell[] LastRow = new Cell[dimentions[0]];
    for(int i = 0; i < dimentions[0]; i++)
    {
        LastRow[i] = map.cells[(total - dimentions[0]) + i];
    }

    map = new(dimentions);
    map.Init(drawableList, LastRow);      
}
*/
#endregion


//______________________________ DEBUG DEFAULT LOOP
#region Debug
bool exit = false;
int count = 0;
int targetCount = 0;
int completed = 0;
while (!exit)
{
    #region CommandControll
    string s = "";
    int i = 0;
    if (completed >= targetCount)
    {
        s = Console.ReadLine();
    }

    if (int.TryParse(s, out i))
    {
        targetCount += i;
    }

    switch (s)
    {
        case "clear":
            Console.Clear();
            break;

        case "exit":
        case "quit":
            goto exitStatements;
            break;

        case "w":
        case "width":
            Console.Write("New Width : ");
            string w = Console.ReadLine();
            if (int.TryParse(w, out i))
                DIMENTIONS[0] = i;
            break;

        case "h":
        case "height":
            Console.Write("New Width : ");
            string h = Console.ReadLine();
            if (int.TryParse(h, out i))
                DIMENTIONS[1] = i;
            break;

        default:
            break;
    }
    #endregion

    Grid map = new(DIMENTIONS);
    map.Init(drawableList);
    while (!map.Run())
    {
        count++;
        map = new(DIMENTIONS);
        map.Init(drawableList);
    }

    Console.WriteLine();
    completed++;
    count++;
}

exitStatements:
    Console.WriteLine($"\n\n __________________________________________________________________ \n DONE ");
    Console.WriteLine($"Runs:\t{count}");
    Console.WriteLine($"Sucess:\t{completed}");
    Console.WriteLine($"Fail:\t{count - completed}\n");
    Console.WriteLine($"SUCESS RATE:\t{((float)completed / (float)count) * 100} %");
    // depending on diferent tile configurations the build can fail (AKA have a configuration in witch no tile can satisfy the matching conditions)
#endregion



Console.ReadKey();

