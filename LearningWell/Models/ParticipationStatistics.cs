// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
using System;
using System.Collections.Generic;

public class Column
{
    public string Code { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
}

public class Comment
{
    public string Variable { get; set; }
    public string Value { get; set; }
    public string Comments { get; set; }
}

public class Data
{
    public List<string> Key { get; set; }
    public List<string> Values { get; set; }
}

public class Metadata
{
    public string Infofile { get; set; }
    public DateTime Updated { get; set; }
    public string Label { get; set; }
    public string Source { get; set; }
}

public class ParticipationStatistics
{
    public List<Column> Columns { get; set; }
    public List<Comment> Comments { get; set; }
    public List<Data> Data { get; set; }
    public List<Metadata> Metadata { get; set; }
}

