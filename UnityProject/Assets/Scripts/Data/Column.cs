using System;

public class Column
{
    public string Name;
    public string Property;
    public DataType Type;
    public bool IsPrimaryKey;
}
    
public enum DataType
{
    Int,
    String,
    Boolean
    
}

