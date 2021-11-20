using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EClinic.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EClinic.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private const string databaseName = "eclinic";
        private const string collectionName = "appointment";
        private readonly IMongoCollection<Appointment> appointmentCollection;
        private readonly FilterDefinitionBuilder<Appointment> filterBuilder = Builders<Appointment>.Filter;

        
        // Dependency injection
        public AppointmentRepository(IMongoClient mongoClient) 
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            appointmentCollection = database.GetCollection<Appointment>(collectionName);
        }


        public async Task CreateAppointmentAsync(Appointment appointment) {
            await appointmentCollection.InsertOneAsync(appointment);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            return await appointmentCollection.Find(new BsonDocument()).ToListAsync();
        }

        // Overloaded from GetAppointmentsAsync
        // Returns all matching entires where Appointment.DoctorId = Id
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(Guid id)
        {
            var filter = filterBuilder.Eq(appointment => appointment.DoctorId, id);
            return await appointmentCollection.Find(filter).ToListAsync();
        }

        public async Task<Appointment> GetAppointmentAsync(Guid id)
        {
            var filter = filterBuilder.Eq(appointment => appointment.Id, id);
            return await appointmentCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            var filter = filterBuilder.Eq(existingAppointment => existingAppointment.Id, appointment.Id);
            await appointmentCollection.ReplaceOneAsync(filter, appointment);
        }
        public async Task DeleteAppointmentAsync(Guid id)
        {
            var filter = filterBuilder.Eq(appointment => appointment.Id, id);
            await appointmentCollection.DeleteOneAsync(filter);
        }
    }
}