//using System.Collections.Generic;
//using System;
//using System.Linq;
//using System.Reflection;
//using Mono.Data.Sqlite;

//public abstract class SQLRepository<T> : IRepository<T>
//    where T : class, IRepositoryEntry, new()
//{
//    private const string ConnectionString = "URI=file:Daemon.db";
//    private Type _typeOfT = typeof(T);
//    private Column _primaryKeyColumn;

//    protected Column PrimaryKeyColumn
//    {
//        get
//        {
//            if (_primaryKeyColumn == null)
//                _primaryKeyColumn = Columns.First(col => col.IsPrimaryKey);
//            return _primaryKeyColumn;
//        }
//    }

//    protected abstract List<Column> LoadColumns();

//    private List<Column> _columns;

//    private List<Column> Columns
//    {
//        get
//        {
//            if (_columns == null)
//                _columns = LoadColumns();

//            return _columns;
//        }
//    }

//    protected abstract string TableName
//    {
//        get;
//    }

//    public T GetByID(int id)
//    {
//        return GetBySQL(string.Format("WHERE {0} = {1}", PrimaryKeyColumn.Name, id)).FirstOrDefault();
//    }

//    public void Save(T instance)
//    {
//        if (instance.ID == 0)
//            Insert(instance);
//        else
//            Update(instance);
//    }

//    public void Update(T instance)
//    {
//        List<Column> columns = Columns;
//        SqliteConnection dbcon = new SqliteConnection(ConnectionString);
//        dbcon.Open();
//        try
//        {
//            using (SqliteCommand dbcmd = dbcon.CreateCommand())
//            {
//                List<string> columnUpdates = new List<string>();

//                for (int index = 0; index < columns.Count; index++)
//                {
//                    Column column = columns[index];
//                    if (column.IsPrimaryKey)
//                        continue;
//                    PropertyInfo propertyInfo = _typeOfT.GetProperty(column.Property);
//                    object propertyValue = propertyInfo.GetValue(instance, null);
//                    if (propertyValue == null)
//                        columnUpdates.Add(string.Format(" {0} = NULL ", column.Name));
//                    else
//                        columnUpdates.Add(string.Format(" {0} = '{1}' ", column.Name, propertyValue.ToString()));
//                }

//                dbcmd.CommandText = string.Format("UPDATE {0} SET {1} WHERE {2} = {3}", TableName, string.Join(",", columnUpdates.ToArray()), PrimaryKeyColumn.Name, instance.ID);
//                dbcmd.ExecuteNonQuery();
//            }
//        }
//        finally
//        {
//            dbcon.Close();
//        }
//    }

//    public void Insert(T instance)
//    {
//        List<Column> columns = Columns;
//        SqliteConnection dbcon = new SqliteConnection(ConnectionString);
//        dbcon.Open();
//        try
//        {
//            using (SqliteCommand dbcmd = dbcon.CreateCommand())
//            {
//                List<string> columnNames = new List<string>();
//                List<string> columnValues = new List<string>();

//                for (int index = 0; index < columns.Count; index++)
//                {
//                    Column column = columns[index];
//                    if (column.IsPrimaryKey)
//                        continue;

//                    columnNames.Add(column.Name);

//                    PropertyInfo propertyInfo = _typeOfT.GetProperty(column.Property);
//                    object propertyValue = propertyInfo.GetValue(instance, null);
//                    if (propertyValue == null)
//                        columnValues.Add("NULL");
//                    else
//                    {
//                        columnValues.Add("'" + propertyValue.ToString().Replace("'", "''") + "'");
//                    }
//                }

//                dbcmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", TableName, string.Join(",", columnNames.ToArray()), string.Join(",", columnValues.ToArray()));
//                dbcmd.ExecuteNonQuery();
//            }
//        }
//        finally
//        {
//            dbcon.Close();
//        }
//    }

//    public void Delete(T instance)
//    {
//        SqliteConnection dbcon = new SqliteConnection(ConnectionString);
//        dbcon.Open();
//        try
//        {
//            using (SqliteCommand dbcmd = dbcon.CreateCommand())
//            {
//                dbcmd.CommandText = string.Format("DELETE FROM {0} WHERE {1} = {2}", TableName, PrimaryKeyColumn.Name, instance.ID);
//                dbcmd.ExecuteNonQuery();
//            }
//        }
//        finally
//        {
//            dbcon.Close();
//        }
//    }

//    public void DeleteAll()
//    {
//        SqliteConnection dbcon = new SqliteConnection(ConnectionString);
//        dbcon.Open();
//        try
//        {
//            using (SqliteCommand dbcmd = dbcon.CreateCommand())
//            {
//                dbcmd.CommandText = string.Format("DELETE FROM {0}", TableName);
//                dbcmd.ExecuteNonQuery();
//            }
//        }
//        finally
//        {
//            dbcon.Close();
//        }
//    }

//    public List<T> GetBySQL(string SQL)
//    {
//        List<T> ts = new List<T>();

//        List<Column> columns = Columns;

//        string columnsToQuery = String.Join(", ", columns.Select(Column => Column.Name).ToArray());
//        SqliteConnection dbcon = new SqliteConnection(ConnectionString);
//        dbcon.Open();

//        try
//        {
//            using (SqliteCommand dbcmd = dbcon.CreateCommand())
//            {
               

//                dbcmd.CommandText = "SELECT " + columnsToQuery + " FROM " + TableName + " " + SQL;

//                SqliteDataReader reader = dbcmd.ExecuteReader();

//                while (reader.Read())
//                {
//                    T t = new T();
//                    for (int index = 0; index < columns.Count; index++)
//                    {
//                        Column column = columns[index];

//                        PropertyInfo propertyInfo = _typeOfT.GetProperty(column.Property);

//                        if (reader.IsDBNull(index))
//                        {
//                            propertyInfo.SetValue(t, null, null);
//                        }
//                        else
//                        {
//                            switch (column.Type)
//                            {

//                                case DataType.Int:
//                                    propertyInfo.SetValue(t, reader.GetInt32(index), null);
//                                    break;
//                                case DataType.String:
//                                    propertyInfo.SetValue(t, reader.GetString(index), null);
//                                    break;
//                                case DataType.Boolean:
//                                    propertyInfo.SetValue(t, reader.GetBoolean(index), null);
//                                    break;
//                            }
//                        }
//                    }
//                    ts.Add(t);
//                }

//                reader.Close();
//            }
//        }
//        finally
//        {
//            dbcon.Close();
//        }
//        return ts;
//    }

//    public List<T> GetAll()
//    {
//        return GetBySQL("");
//    }

//}