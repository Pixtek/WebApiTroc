namespace Infrastructure.EF.DbEntities;

public class DbHistoricArticle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string URLImage { get; set; }
    public DateTime PublicationDate { get; set; }
    public string CategoryName { get; set; }
    
    public string description { get; set; }
    
    public int IdUser { get; set; }
}