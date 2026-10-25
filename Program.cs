using System;
using System.Collections.Generic;

namespace ComicBookStore;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        Console.WriteLine("==================================================");
        Console.WriteLine("    חנות ספרי קומיקס - מערכת בדיקות אוטומטית    ");
        Console.WriteLine("==================================================\n");

        ComicStore store = new ComicStore();

        int passed = 0;
        int total = 7;

        Console.WriteLine("תוצאות הרצת בדיקות היחידה המובנות:");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine(string.Format("{0,-10} | {1,-35} | {2}", "מספר משימה", "שם המשימה", "סטטוס"));
        Console.WriteLine("--------------------------------------------------");

        // בדיקה 1
        bool test1 = TestTask1(store);
        PrintRow("משימה 1", "הוספה/עדכון מלאי (AddOrUpdateComic)", test1);
        if (test1) passed++;

        // בדיקה 2
        bool test2 = TestTask2(store);
        PrintRow("משימה 2", "עדכון מלאי (UpdateStock)", test2);
        if (test2) passed++;

        // בדיקה 3
        bool test3 = TestTask3(store);
        PrintRow("משימה 3", "חיפוש לפי גיבור (SearchByHero)", test3);
        if (test3) passed++;

        // בדיקה 4
        bool test4 = TestTask4(store);
        PrintRow("משימה 4", "בדיקת זמינות מלאי (IsInStock)", test4);
        if (test4) passed++;

        // בדיקה 5
        bool test5 = TestTask5(store);
        PrintRow("משימה 5", "יצירת מילון מלאי (GetStockDictionary)", test5);
        if (test5) passed++;

        // בדיקה 6
        bool test6 = TestTask6(store);
        PrintRow("משימה 6", "חיפוש כמות במילון (GetQuantityFromDictionary)", test6);
        if (test6) passed++;

        // בדיקה 7
        bool test7 = TestTask7(store);
        PrintRow("משימה 7", "גיבור עם הכי הרבה ספרים (GetHeroWithMostComics)", test7);
        if (test7) passed++;

        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"\nסיכום בדיקות: {passed} / {total} משימות עברו בהצלחה.");
        if (passed < total)
        {
            Console.WriteLine("חלק מהמשימות עדיין לא מומשו כראוי. בהצלחה בפתרון!");
        }
        else
        {
            Console.WriteLine("כל הכבוד! כל המשימות עברו בהצלחה! 🏆");
        }

        Console.WriteLine("\nלחץ על מקש כלשהו ליציאה...");
        Console.ReadKey();
    }

    static void PrintRow(string taskId, string taskName, bool isSuccess)
    {
        string status = isSuccess ? "[V] עבר תקין" : "[X] נכשל / לא מומש";
        Console.ForegroundColor = isSuccess ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(string.Format("{0,-10} | {1,-35} | {2}", taskId, taskName, status));
        Console.ResetColor();
    }

    static bool TestTask1(ComicStore store)
    {
        int initialCount = store.Inventory.Count;
        store.AddOrUpdateComic(new ComicBook("Batman: Hush", 2002, "Batman", 5));
        bool addedNew = store.Inventory.Count == initialCount + 1;
        
        store.AddOrUpdateComic(new ComicBook("Batman: Hush", 2002, "Batman", 3));
        var hushBook = store.Inventory.Find(c => c.Title == "Batman: Hush");
        bool updatedQuantity = hushBook != null && hushBook.StockQuantity == 8;

        return addedNew && updatedQuantity;
    }

    static bool TestTask2(ComicStore store)
    {
        bool successUpdate = store.UpdateStock("The Amazing Spider-Man #300", 15);
        var book = store.Inventory.Find(c => c.Title == "The Amazing Spider-Man #300");
        bool failUpdate = store.UpdateStock("Non Existing Comic", 10);

        return successUpdate && !failUpdate && book != null && book.StockQuantity == 15;
    }

    static bool TestTask3(ComicStore store)
    {
        List<ComicBook> batmanBooks = store.SearchByHero("Batman");
        return batmanBooks != null && batmanBooks.Count >= 2 && batmanBooks.All(b => b.Hero == "Batman");
    }

    static bool TestTask4(ComicStore store)
    {
        bool inStock = store.IsInStock("The Amazing Spider-Man #300"); // 15 or > 0
        bool outOfStock = store.IsInStock("Batman: The Dark Knight Returns"); // stock = 0
        bool nonExisting = store.IsInStock("Fake Comic");

        return inStock && !outOfStock && !nonExisting;
    }

    static bool TestTask5(ComicStore store)
    {
        var dict = store.GetStockDictionary();
        if (dict == null || dict.Count != store.Inventory.Count) return false;
        
        return dict.ContainsKey("The Amazing Spider-Man #300") && dict["The Amazing Spider-Man #300"] == store.Inventory.Find(c => c.Title == "The Amazing Spider-Man #300")?.StockQuantity;
    }

    static bool TestTask6(ComicStore store)
    {
        int q1 = store.GetQuantityFromDictionary("Superman: Action Comics #1000");
        int q2 = store.GetQuantityFromDictionary("Non Existing Book");

        return q1 == 12 && q2 == -1;
    }

    static bool TestTask7(ComicStore store)
    {
        string topHero = store.GetHeroWithMostComics();
        return topHero == "Spider-Man";
    }
}
