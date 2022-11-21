﻿namespace Infrastructure.EF.DbEntities;

public class DbArticle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string URLImage { get; set; }
    public DateTime PublicationDate { get; set; }
    public string CategoryName { get; set; }
    
    public int IdUser { get; set; }
}