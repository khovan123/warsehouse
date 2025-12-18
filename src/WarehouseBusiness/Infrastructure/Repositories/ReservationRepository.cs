using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
  public class ReservationRepository : IReservationRepository
  {
    private readonly IMongoCollection<Reservation> _reservation;

    public ReservationRepository(MongoDbContext context)
    {
      _reservation = context.Reservations;
    }
    public async Task<List<Reservation>> GetAll(CancellationToken ct)
    {
      var filter = Builders<Reservation>.Filter.Empty;
      return await _reservation.Find(filter).ToListAsync(ct);
    }

    public async Task<Reservation> GetById(string id, CancellationToken ct)
    {
      var f = Builders<Reservation>.Filter;
      var filter = f.And(
        f.Eq(m => m.Id, id)
      );
      return await _reservation.Find(filter).FirstOrDefaultAsync(ct);
    }
  }
}