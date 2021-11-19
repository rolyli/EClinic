using System.Collections.Generic;
using EClinic.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EClinic.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private const string databaseName = "eclinic";
        private const string collectionName = "patient";
        private readonly IMongoCollection<Patient> patientCollection;
        
        // Dependency injection
        public PatientRepository(IMongoClient mongoClient) 
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            patientCollection = database.GetCollection<Patient>(collectionName);
        }

        public void CreatePatient(Patient patient) {
            patientCollection.InsertOne(patient);
        }
        public IEnumerable<Patient> GetPatients()
        {
            return patientCollection.Find(new BsonDocument()).ToList();
        }
    }
}