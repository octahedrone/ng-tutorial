﻿namespace Application.Playground;

public class AdventureStep
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<AdventureStepOption> Options { get; set; }
}