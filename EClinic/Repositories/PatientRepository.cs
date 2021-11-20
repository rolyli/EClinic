using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly FilterDefinitionBuilder<Patient> filterBuilder = Builders<Patient>.Filter;

        
        // Dependency injection
        public PatientRepository(IMongoClient mongoClient) 
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            patientCollection = database.GetCollection<Patient>(collectionName);
        }

        public async Task CreatePatientAsync(Patient patient) {
            await patientCollection.InsertOneAsync(patient);
        }
        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            return await patientCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Patient> GetPatientAsync(Guid id)
        {
            var filter = filterBuilder.Eq(patient => patient.Id, id);
            return await patientCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<Patient> GetPatientAsync(string username)
        {
            var filter = filterBuilder.Eq(patient => patient.Username, username);
            return await patientCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            var filter = filterBuilder.Eq(existingPatient => existingPatient.Id, patient.Id);
            await patientCollection.ReplaceOneAsync(filter, patient);
        }
        public async Task DeletePatientAsync(Guid id)
        {
            var filter = filterBuilder.Eq(patient => patient.Id, id);
            await patientCollection.DeleteOneAsync(filter);
        }
    }
}