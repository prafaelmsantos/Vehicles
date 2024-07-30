namespace Vehicles.Persistence.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<List<VehicleDTO>> GetAllVehiclesAsync();
        Task<VehicleDTO> GetVehicleByIdAsync(long vehicleId);
        Task<VehicleCounterDTO> GetVehicleCountersAsync();
        Task<ResponseCompleteStatisticDTO> GetAllVehiclesWithYearComparisonAsync();
        Task<ResponseStatisticDTO> GetAllVehiclesWithMonthComparisonAsync();
        Task<PieStatisticDTO> GetVehiclePieStatisticsAsync();
        Task<VehicleDTO> AddVehicleAsync(VehicleDTO vehicleDTO);
        Task<VehicleDTO> UpdateVehicleAsync(VehicleDTO vehicleDTO);
        Task<List<InternalBaseResponseDTO>> DeleteVehiclesAsync(List<long> vehiclesIds);
    }
}
