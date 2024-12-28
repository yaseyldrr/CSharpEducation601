using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEducation601.Services
{
    public class MongoDBConnection
    {
        private IMongoDatabase _database;

        public MongoDBConnection()
        {
            // Constructor 
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("Db601Customer"); // oluşturulacak veritabanı adı

        }
        public IMongoCollection<BsonDocument> GetCustomersCollection()
        {
            return _database.GetCollection<BsonDocument>("Customers"); // customers adında bir collection oluşturulacak

        }


    }
}
