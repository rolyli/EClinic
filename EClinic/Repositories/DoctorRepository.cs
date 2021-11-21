using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EClinic.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EClinic.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private const string databaseName = "eclinic";
        private const string collectionName = "doctor";
        private readonly IMongoCollection<Doctor> doctorCollection;
        private readonly FilterDefinitionBuilder<Doctor> filterBuilder = Builders<Doctor>.Filter;

        
        // Dependency injection
        public DoctorRepository(IMongoClient mongoClient) 
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            doctorCollection = database.GetCollection<Doctor>(collectionName);
        }

        public async Task CreateDoctorAsync(Doctor doctor) {
            await doctorCollection.InsertOneAsync(doctor);
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsAsync()
        {
            return await doctorCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Doctor> GetDoctorAsync(Guid id)
        {
            var filter = filterBuilder.Eq(doctor => doctor.Id, id);
            return await doctorCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            var filter = filterBuilder.Eq(existingDoctor => existingDoctor.Id, doctor.Id);
            await doctorCollection.ReplaceOneAsync(filter, doctor);
        }
        public async Task DeleteDoctorAsync(Guid id)
        {
            var filter = filterBuilder.Eq(doctor => doctor.Id, id);
            await doctorCollection.DeleteOneAsync(filter);
        }

        public async Task<Doctor> GetDoctorAsync(string username)
        {
            var filter = filterBuilder.Eq(doctor => doctor.Username, username);
            return await doctorCollection.Find(filter).SingleOrDefaultAsync();
        }
    }
}