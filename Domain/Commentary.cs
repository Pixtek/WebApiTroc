﻿namespace Domain;

public class Commentary
{
    public int Id { get; set; }
    public short Note { get; set; }
    public string Nom { get; set; }
    public string Message { get; set; }
    public int Id_User { get; set; }
    
}