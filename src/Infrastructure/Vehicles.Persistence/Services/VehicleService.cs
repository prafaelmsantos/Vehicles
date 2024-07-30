namespace Vehicles.Persistence.Services
{
    public class VehicleService : IVehicleService
    {
        #region Private variables

        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;

        #endregion

        #region Constructors
        public VehicleService(IMapper mapper, IVehicleRepository vehicleRepository)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
        }

        #endregion

        #region Public methods

        public async Task<List<VehicleDTO>> GetAllVehiclesAsync()
        {
            try
            {
                List<Vehicle> vehicles = await _vehicleRepository
                    .GetAll()
                    .Include(x => x.VehicleImages)
                    .Include(x => x.Model)
                    .ThenInclude(x => x.Mark)
                    .OrderBy(x => x.Id)
                    .AsNoTracking()
                    .ToListAsync();

                return _mapper.Map<List<VehicleDTO>>(vehicles);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.GetAllVehiclesAsyncException} {ex.Message}");
            }
        }

        public async Task<VehicleDTO> GetVehicleByIdAsync(long vehicleId)
        {
            try
            {
                Vehicle? vehicle = await _vehicleRepository
                    .GetAll()
                    .Where(x => x.Id == vehicleId)
                    .Include(x => x.VehicleImages)
                    .Include(x => x.Model)
                    .ThenInclude(x => x.Mark)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                vehicle.ThrowIfNull(() => throw new Exception(DomainResources.VehicleNotFoundException));

                return _mapper.Map<VehicleDTO>(vehicle);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.GetVehicleByIdAsyncException} {ex.Message}");
            }
        }

        public async Task<VehicleCounterDTO> GetVehicleCountersAsync()
        {
            try
            {
                var salesVehiclesByMonth = await GetCountersValuesAsync(true, true);
                var salesVehicles = await GetCountersValuesAsync(true);
                var stockVehicles = await GetCountersValuesAsync();

                return new()
                {
                    TotalSalesMonth = new()
                    {
                        Units = salesVehiclesByMonth.Item1,
                        Values = salesVehiclesByMonth.Item2
                    },
                    TotalSales = new()
                    {
                        Units = salesVehicles.Item1,
                        Values = salesVehicles.Item2
                    },
                    TotalStock = new()
                    {
                        Units = stockVehicles.Item1,
                        Values = stockVehicles.Item2
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.GetVehicleCountersAsyncException} {ex.Message}");
            }
        }

        public async Task<ResponseCompleteStatisticDTO> GetAllVehiclesWithYearComparisonAsync()
        {
            try
            {
                var currentStatisticsDTO = await GetStatisticsByYearAsync(DateTime.UtcNow.Year);
                var lastStatisticsDTO = await GetStatisticsByYearAsync(DateTime.UtcNow.Year - 1);

                var values = GetValuesWithYearComparison(lastStatisticsDTO, currentStatisticsDTO);

                return new()
                {
                    Statistics = currentStatisticsDTO,
                    LastStatistics = lastStatisticsDTO,
                    Value = values.Item1,
                    ValuePerc = values.Item2
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.GetAllVehiclesWithYearComparisonAsyncException} {ex.Message}");
            }
        }

        public async Task<ResponseStatisticDTO> GetAllVehiclesWithMonthComparisonAsync()
        {
            try
            {
                var statisticsDTO = await GetStatisticsByYearAsync(DateTime.UtcNow.Year);

                var values = GetValuesWithMonthComparison(statisticsDTO);

                return new()
                {
                    Statistics = statisticsDTO,
                    Value = values.Item1,
                    ValuePerc = values.Item2
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.GetAllVehiclesWithMonthComparisonAsyncException} {ex.Message}");
            }
        }

        public async Task<PieStatisticDTO> GetVehiclePieStatisticsAsync()
        {
            try
            {
                var pieValues = await GetPieStatisticValuesAsync();
                return new() { TotalSales = pieValues.Item1, TotalStock = pieValues.Item2 };
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.GetVehiclePieStatisticsAsyncException} {ex.Message}");
            }
        }

        public async Task<VehicleDTO> AddVehicleAsync(VehicleDTO vehicleDTO)
        {
            try
            {
                Vehicle vehicle = new(vehicleDTO.ModelId, vehicleDTO.Version, vehicleDTO.FuelType, vehicleDTO.Price, vehicleDTO.Mileage,
                    vehicleDTO.Year, vehicleDTO.Color, vehicleDTO.Doors, vehicleDTO.Transmission, vehicleDTO.EngineSize, vehicleDTO.Power,
                    vehicleDTO.Observations, vehicleDTO.Opportunity, vehicleDTO.Sold, vehicleDTO.SoldDate);

                vehicle = SetVehicleImages(vehicleDTO, vehicle);

                await _vehicleRepository.AddAsync(vehicle);

                return _mapper.Map<VehicleDTO>(vehicle);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.AddVehicleAsyncException} {ex.Message}");
            }
        }

        public async Task<VehicleDTO> UpdateVehicleAsync(VehicleDTO vehicleDTO)
        {
            try
            {
                Vehicle? vehicle = await _vehicleRepository
                    .GetAll()
                    .Where(x => x.Id == vehicleDTO.Id)
                    .Include(x => x.VehicleImages)
                    .FirstOrDefaultAsync();

                vehicle.ThrowIfNull(() => throw new Exception(DomainResources.VehicleNotFoundException));

                vehicle.Update(vehicleDTO.ModelId, vehicleDTO.Version, vehicleDTO.FuelType, vehicleDTO.Price, vehicleDTO.Mileage,
                    vehicleDTO.Year, vehicleDTO.Color, vehicleDTO.Doors, vehicleDTO.Transmission, vehicleDTO.EngineSize, vehicleDTO.Power,
                    vehicleDTO.Observations, vehicleDTO.Opportunity, vehicleDTO.Sold, vehicleDTO.SoldDate);

                vehicle = SetVehicleImages(vehicleDTO, vehicle);

                await _vehicleRepository.UpdateAsync(vehicle);

                return _mapper.Map<VehicleDTO>(vehicle);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.UpdateVehicleAsyncException} {ex.Message}");
            }
        }

        public async Task<List<InternalBaseResponseDTO>> DeleteVehiclesAsync(List<long> vehiclesIds)
        {
            return await DeleteAsync(vehiclesIds);
        }

        #endregion

        #region Private methods

        private async Task<(int, double)> GetCountersValuesAsync(bool sold = false, bool byMonth = false)
        {
            var vehicles = await _vehicleRepository
                    .GetAll()
                    .Where(x => x.Sold == sold)
                    .AsNoTracking()
                    .ToListAsync();

            if (byMonth)
            {
                vehicles = vehicles.Where(x => x.SoldDate?.Month == DateTime.UtcNow.Month).ToList();
            }

            return (vehicles.Count, vehicles.Select(x => x.Price).Sum());
        }

        private async Task<List<StatisticDTO>> GetStatisticsByYearAsync(int year)
        {
            List<Vehicle> vehicles = await _vehicleRepository
                   .GetAll()
                   .Where(x => x.SoldDate != null && x.SoldDate.Value.Year == year)
                   .AsNoTracking()
                   .ToListAsync();

            List<StatisticDTO> statisticsDTO = new();

            var monthList = Enum.GetValues(typeof(MONTH)).Cast<MONTH>().ToList();

            monthList.ForEach(month =>
                statisticsDTO.Add(new()
                {
                    Month = month,
                    Year = year,
                    Value = vehicles.Where(x => x.Sold && x.SoldDate != null && x.SoldDate.Value.Month == (int)month).Select(x => x.Price).Sum()
                }));

            return statisticsDTO;
        }

        private static (double, double) GetValuesWithYearComparison(List<StatisticDTO> lastStatisticsDTO, List<StatisticDTO> currentStatisticsDTO)
        {
            double lastValue = lastStatisticsDTO.Select(x => x.Value).Sum();
            double currentValue = currentStatisticsDTO.Select(x => x.Value).Sum();
            var valuePerc = lastValue != 0 ? (double)(currentValue - lastValue) / lastValue * 100 : currentValue != 0 ? 100 : 0;

            return (currentValue, Math.Round(valuePerc, 1));
        }

        private static (double, double) GetValuesWithMonthComparison(List<StatisticDTO> statisticsDTO)
        {
            double currentMonthValue = statisticsDTO.Where(x => (int)x.Month == DateTime.UtcNow.Month)?.Select(x => x.Value)?.Sum() ?? 0;
            double lastMonthValue = statisticsDTO.Where(x => (int)x.Month == DateTime.UtcNow.Month - 1)?.Select(x => x.Value)?.Sum() ?? 0;
            var valuePerc = lastMonthValue != 0 ? (double)(currentMonthValue - lastMonthValue) / lastMonthValue * 100 : currentMonthValue != 0 ? 100 : 0;

            return (currentMonthValue, Math.Round(valuePerc, 1));
        }

        private async Task<(double, double)> GetPieStatisticValuesAsync()
        {
            var vehicles = await _vehicleRepository
                   .GetAll()
                   .AsNoTracking()
                   .ToListAsync();

            var vehiclesSoldCount = vehicles.Where(x => x.Sold).Count();
            var vehiclesCount = vehicles.Where(x => !x.Sold).Count();
            double vehiclesSum = vehiclesSoldCount + vehiclesCount;

            var vehiclesSoldPerc = vehiclesSum != 0 ? vehiclesSoldCount / vehiclesSum * 100 : 0;
            var vehiclesPerc = vehiclesSum != 0 ? vehiclesCount / vehiclesSum * 100 : 0;

            return (Math.Round(vehiclesSoldPerc, 1), Math.Round(vehiclesPerc, 1));
        }

        private static Vehicle SetVehicleImages(VehicleDTO vehicleDTO, Vehicle vehicle)
        {
            List<VehicleImage> vehicleImages = new();
            vehicleDTO.VehicleImages.ForEach(x => vehicleImages.Add(new VehicleImage(x.Url)));
            vehicleImages.FirstOrDefault()?.SetIsMain();
            vehicle.SetVehicleImages(vehicleImages);

            return vehicle;
        }

        private async Task<List<InternalBaseResponseDTO>> DeleteAsync(List<long> vehiclesIds)
        {
            List<InternalBaseResponseDTO> internalBaseResponseDTOs = new();

            foreach (var vehicleId in vehiclesIds)
            {
                InternalBaseResponseDTO internalBaseResponseDTO = new() { Id = vehicleId, Success = false };
                try
                {
                    Vehicle? vehicle = await _vehicleRepository
                        .GetAll()
                        .Where(x => x.Id == vehicleId)
                        .Include(x => x.Model)
                        .ThenInclude(x => x.Mark)
                        .FirstOrDefaultAsync();

                    if (vehicle is not null)
                    {
                        internalBaseResponseDTO.Success = await _vehicleRepository.RemoveAsync(vehicle);
                    }
                    else
                    {
                        internalBaseResponseDTO.ErrorMessage = DomainResources.VehicleNotFoundException;
                    }
                }
                catch (Exception)
                {
                    internalBaseResponseDTO.ErrorMessage = DomainResources.DeleteVehiclesAsyncException;
                }

                internalBaseResponseDTOs.Add(internalBaseResponseDTO);
            }

            return internalBaseResponseDTOs;
        }
        #endregion
    }
}
