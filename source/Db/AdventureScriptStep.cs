﻿namespace Db;

public class AdventureScriptStep
{
    public int Id { get; set; }
    public int AdventureScriptId { get; set; }
    public int? ParentStepId { get; set; }
    public string Text { get; set; }
}