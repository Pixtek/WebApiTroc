namespace Domain;

public class Article
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string URLImage { get; set; }
    public DateTime PublicationDate { get; set; }
    public string CategoryName { get; set; }
    
    public string description { get; set; }
    
    public int IdUser { get; set; }
    
    
    public bool IsInCategory(string categoryName)
    {
        return CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase);
    }
}