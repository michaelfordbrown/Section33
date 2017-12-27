using System;
using System.Collections.Generic;
using UtilityLibraries;

class Program
{
    public static string BoxTable(List<WordCount> CountTable)
    {
        string Result = "";


        Result = "========================\n";
        int i = 0;
        while ((i < CountTable.Count) && (i < 5))
        {
            Result += "| ";
            Result += string.Format("{0, -15}", CountTable[i].word);
            Result += string.Format(" {0, -5}|\n",CountTable[i].count);
            i++;
        }
        Result += "========================\n";
        return Result;
    }
    static void Main(string[] args)
    {
        string FilePath = "Test Document.txt";

        Console.WriteLine(FilePath.DisplayTopFive());

        string strPhrase = "Marley was dead: to begin with.  There is no doubt whatever about that.  The register of his burial was signed by the clergyman, the clerk, the undertaker, and the chief mourner.  Scrooge signed it: and Scrooge's name was good upon 'Change, for anything he chose to put his hand to.  Old Marley was as dead as a door-nail. \n\nMind!I don't mean to say that I know, of my own knowledge, what there is particularly dead about a door-nail.  I might have been inclined, myself, to regard a coffin-nail as the deadest piece of ironmongery in the trade.  But the wisdom of our ancestors is in the simile; and my unhallowed hands shall not disturb it, or the Country's done for.  You will therefore permit me to repeat, emphatically, that Marley was as dead as a door - nail.\n\nScrooge knew he was dead ? Of course he did.How could it be otherwise ? Scrooge and he were partners for I don't know how many years.  Scrooge was his sole executor, his sole administrator, his sole assign, his sole residuary legatee, his sole friend and sole mourner.  And even Scrooge was not so dreadfully cut up by the sad event, but that he was an excellent man of business on the very day of the funeral, and solemnised it with an undoubted bargain.\n\nThe mention of Marley's funeral brings me back to the point I started from.  There is no doubt that Marley was dead.  This must be distinctly understood, or nothing wonderful can come of the story I am going to relate.  If we were not perfectly convinced that Hamlet's Father died before the play began, there would be nothing more remarkable in his taking a stroll at night, in an easterly wind, upon his own ramparts, than there would be in any other middle - aged gentleman rashly turning out after dark in a breezy spot-- say Saint Paul's Churchyard for instance -- literally to astonish his son's weak mind.";

        var MWords = strPhrase.MatchWords();
        var CResult = MWords.CountLettersAndWords();
        var LSort = CResult.Item1.SortByValue();
        var WSort = CResult.Item2.SortByValue();

        Console.WriteLine("{0}\n{1}",BoxTable(LSort), BoxTable(WSort));

        Console.ReadKey();
    }
}