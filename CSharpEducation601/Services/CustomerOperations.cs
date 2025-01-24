using CSharpEducation601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
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
            var customerCollection = connection.GetCustomersCollection(); // tabloya bağlanma

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


        public List<Customer> GetAllCustomer()
        {

            var connection = new MongoDBConnection();
            var customerCollection = connection.GetCustomersCollection();
            var customers = customerCollection.Find(new BsonDocument()).ToList(); // mongodb de verileri getirme komutu
            List<Customer> customerList = new List<Customer>();

            foreach (var c in customers)
            { // 
                customerList.Add(new Customer()
                {
                    CustomerId = c["_id"].ToString(),
                    CustomerBalance = decimal.Parse(c["CustomerBalance"].ToString()),
                    CustomerCity = c["CustomerCity"].ToString(),
                    CustomerName = c["CustomerName"].ToString(),
                    CustomerPurchase = int.Parse(c["CustomerPurchase"].ToString()),
                    CustomerSurname = c["CustomerSurname"].ToString()
                });

            }
            return customerList;
        }



        public void DeleteCustomer(string id)
        {
            var connection = new MongoDBConnection();
            var customerCollection = connection.GetCustomersCollection();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            customerCollection.DeleteOne(filter); 
        }


        public void UpdateCustomer(Customer customer)
        {
            var connection = new MongoDBConnection();
            var customerCollection = connection.GetCustomersCollection();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customer.CustomerId)); // filter değişkeni 
            var update = Builders<BsonDocument>.Update // Ayarla
                .Set("CustomerName", customer.CustomerName)
                .Set("CustomerSurname", customer.CustomerSurname)
                .Set("CustomerCity", customer.CustomerCity)
                .Set("CustomerBalance", customer.CustomerBalance)
                .Set("CustomerPurchase", customer.CustomerPurchase);

            customerCollection.UpdateOne(filter, update);
        }



        public Customer GetCustomerById(string id)
        {
            var connection = new MongoDBConnection();
            var customerCollection = connection.GetCustomersCollection();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var customer = customerCollection.Find(filter).FirstOrDefault();
            return new Customer()
            {
                CustomerBalance = decimal.Parse(customer["CustomerBalance"].ToString()),
                CustomerCity = customer["CustomerCity"].ToString(),
                CustomerName = customer["CustomerName"].ToString(),
                CustomerPurchase = int.Parse(customer["CustomerPurchase"].ToString()),
                CustomerSurname = customer["CustomerSurname"].ToString(),
                CustomerId = id
            };
        }
    }
        }
