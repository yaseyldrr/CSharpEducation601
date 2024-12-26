using CSharpEducation601.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEducation601.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        { 
            var connection = new MongoDBConnection(); // db ye bağlanma
            var customerCollection = connection.GetCustomerCollection(); // tabloya bağlanma

            var document = new BsonDocument
            {
                {"CustomerName", customer.CustomerName},
                {"CustomerSurname", customer.CustomerSurname},
                {"CustomerCity", customer.CustomerCity},
                {"CustomerBalance", customer.CustomerBalance},
                {"CustomerPurchase", customer.CustomerPurchase}
            }; // paramtereleri belirleme

            customerCollection.InsertOne(document); // ekleme işlemi
        }
    }
}
