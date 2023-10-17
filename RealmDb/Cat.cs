using MongoDB.Bson;
using Realms;
using System.Collections.Generic;

public partial class Cat : IEmbeddedObject
{
   /* [PrimaryKey]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();*/
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
    public string Breed { get; set; } = string.Empty;
    
}


public partial class CatList : IRealmObject
{
    [PrimaryKey]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    public IList<Cat> Items { get;  }
}

