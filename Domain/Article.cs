﻿namespace Domain;

public class Article
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public string CategoryName { get; set; }
    public string Name { get; set; }
    public string URLImage { get; set; }
    public DateTime PublicationDate { get; set; }
}