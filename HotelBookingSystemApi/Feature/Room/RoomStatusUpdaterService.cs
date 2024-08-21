using HotelBookingSystem.DBService.Model;

namespace HotelBookingSystemApi.Feature.Room
{
    public class RoomStatusUpdaterService:BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RoomStatusUpdaterService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await UpdateRoomStatusesAsync();
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Adjust timing as needed
            }
        }
        private async Task UpdateRoomStatusesAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var currentDate = DateTime.UtcNow;

                var roomsToUpdate =  dbContext.TblRooms
                 .Where(r => r.Status != "Available" &&
                             dbContext.TblBookings.Any(b => b.RoomId == r.RoomId 
                             && b.CheckOutDate < currentDate))
                 .ToList();

                ;

                foreach (var room in roomsToUpdate)
                {
                    room.Status = "Available";
                }

                await dbContext.SaveChangesAsync();
            }
        }


    }
}
