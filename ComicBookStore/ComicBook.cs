namespace ComicBookStore;

/// <summary>
/// מייצג חוברת קומיקס בחנות
/// </summary>
public class ComicBook
{
    public string Title { get; set; }
    public int Year { get; set; }
    public string Hero { get; set; }
    public int StockQuantity { get; set; }

    public ComicBook(string title, int year, string hero, int stockQuantity)
    {
        Title = title;
        Year = year;
        Hero = hero;
        StockQuantity = stockQuantity;
    }

    public override string ToString()
    {
        return $"[קומיקס] {Title} ({Year}) | גיבור: {Hero} | במלאי: {StockQuantity}";
    }
}
