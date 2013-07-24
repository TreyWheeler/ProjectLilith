
using System.Collections.Generic;

public abstract class SQLRepository<T> : IRepository<T>
    where T : class
{
    
    
    public T GetByID ()
    {
        
           
               // dbcmd.CommandText = "Insert into employee values ('Trey', 'Wheeler')";
                
              //  dbcmd.ExecuteNonQuery();
             //   dbcmd.CommandText = "select * from employee";
                
             //   IDataReader reader = dbcmd.ExecuteReader();
        
//        
//         while(reader.Read())
//                {
//                    string FirstName = reader.GetString(0);
//                    string LastName = reader.GetString(1);
//                    
//                    Debug.Log(FirstName + " " + LastName);
//                }                
//                
//                reader.Close();
        
        // TODO
        
        return null;
    }
    
    public void Save (T instance)
    {
        // TODO
    }
    
    public List<T> GetBySQL(string SQL)
    {
        // TODO
        
        return null;
    }
}