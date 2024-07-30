namespace Vehicles.Tests.Infrastructure.Persistence
{
    public class VehicleServiceTests : BaseClassTests
    {
        #region Private variables

        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly IVehicleService _vehicleService;

        #endregion

        #region Constructors

        public VehicleServiceTests(ITestOutputHelper output) : base(output)
        {
            _vehicleRepositoryMock = new Mock<IVehicleRepository>(MockBehavior.Strict);
            _vehicleService = new VehicleService(Mapper, _vehicleRepositoryMock.Object);
        }

        #endregion

        #region GetAllVehiclesAsync

        [Fact]
        public async Task GetAllVehiclesAsync_GetAll_Successfully()
        {
            // Arrange   
            var dto = VehicleBuilder.VehicleDTO();
            dto.Id = 0;

            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)));

            // Act
            List<VehicleDTO> result = await _vehicleService.GetAllVehiclesAsync();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(VehicleBuilder.VehicleList(dto));

            _vehicleRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _vehicleRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetAllVehiclesAsync_GetAllNotBreak_ThrowsExceptionAsync()
        {
            // Arrange   
            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Throws(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _vehicleService.GetAllVehiclesAsync()).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetAllVehiclesAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }
        #endregion

        #region GetVehicleByIdAsync

        [Fact]
        public async Task GetVehicleByIdAsync_ValidVehicle_Successfully()
        {
            // Arrange   
            var dto = VehicleBuilder.VehicleDTO();
            dto.Id = 0;

            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)));

            // Act
            VehicleDTO result = await _vehicleService.GetVehicleByIdAsync(dto.Id);

            // Assert
            result.Should().NotBeNull();
            result.ModelId.Should().Be(dto.ModelId);
            result.Version.Should().Be(dto.Version);
            result.FuelType.Should().Be(dto.FuelType);
            result.Price.Should().Be(dto.Price);
            result.Mileage.Should().Be(dto.Mileage);
            result.Year.Should().Be(dto.Year);
            result.Color.Should().Be(dto.Color);
            result.Doors.Should().Be(dto.Doors);
            result.Transmission.Should().Be(dto.Transmission);
            result.EngineSize.Should().Be(dto.EngineSize);
            result.Power.Should().Be(dto.Power);
            result.Observations.Should().Be(dto.Observations);
            result.Opportunity.Should().Be(dto.Opportunity);
            result.VehicleImages.Should().NotBeNull().And.BeEmpty();

            _vehicleRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _vehicleRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetVehicleByIdAsync_VehicleNotFoundException_ThrowsExceptionAsync()
        {
            // Arrange   
            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryableEmpty()));

            // Act & Assert
            await FluentActions.Invoking(async () => await _vehicleService.GetVehicleByIdAsync(It.IsAny<long>())).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetVehicleByIdAsyncException} {DomainResources.VehicleNotFoundException}");
        }

        [Fact]
        public async Task GetVehicleByIdAsync_GetAllNotBreak_ThrowsExceptionAsync()
        {
            // Arrange
            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Throws(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _vehicleService.GetVehicleByIdAsync(It.IsAny<long>())).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetVehicleByIdAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region GetVehicleCountersAsync

        [Fact]
        public async Task GetVehicleCountersAsync_ValidValues_Successfully()
        {
            // Arrange
            var dto = VehicleBuilder.VehicleDTO();
            dto.Id = 0;

            _vehicleRepositoryMock.SetupSequence(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)))
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)))
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)));

            // Act
            VehicleCounterDTO result = await _vehicleService.GetVehicleCountersAsync();

            // Assert
            result.Should().NotBeNull();

            _vehicleRepositoryMock.Verify(repo => repo.GetAll(), Times.Exactly(3));
            _vehicleRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetVehicleCountersAsync_GetAllNotBreak_ThrowsExceptionAsync()
        {
            // Arrange
            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Throws(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _vehicleService.GetVehicleCountersAsync()).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetVehicleCountersAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region GetAllVehiclesWithYearComparisonAsync

        [Fact]
        public async Task GetAllVehiclesWithYearComparisonAsync_ValidValues_Successfully()
        {
            // Arrange
            var dto = VehicleBuilder.VehicleDTO();
            dto.Id = 0;

            _vehicleRepositoryMock.SetupSequence(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)))
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)));

            // Act
            ResponseCompleteStatisticDTO result = await _vehicleService.GetAllVehiclesWithYearComparisonAsync();

            // Assert
            result.Should().NotBeNull();

            _vehicleRepositoryMock.Verify(repo => repo.GetAll(), Times.Exactly(2));
            _vehicleRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetAllVehiclesWithYearComparisonAsync_GetAllNotBreak_ThrowsExceptionAsync()
        {
            // Arrange
            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Throws(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _vehicleService.GetAllVehiclesWithYearComparisonAsync()).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetAllVehiclesWithYearComparisonAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region GetAllVehiclesWithMonthComparisonAsync

        [Fact]
        public async Task GetAllVehiclesWithMonthComparisonAsync_ValidValues_Successfully()
        {
            // Arrange
            var dto = VehicleBuilder.VehicleDTO();
            dto.Id = 0;

            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)));

            // Act
            ResponseStatisticDTO result = await _vehicleService.GetAllVehiclesWithMonthComparisonAsync();

            // Assert
            result.Should().NotBeNull();

            _vehicleRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _vehicleRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetAllVehiclesWithMonthComparisonAsync_GetAllNotBreak_ThrowsExceptionAsync()
        {
            // Arrange
            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Throws(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _vehicleService.GetAllVehiclesWithMonthComparisonAsync()).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetAllVehiclesWithMonthComparisonAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region GetVehiclePieStatisticsAsync

        [Fact]
        public async Task GetVehiclePieStatisticsAsync_ValidValues_Successfully()
        {
            // Arrange
            var dto = VehicleBuilder.VehicleDTO();
            dto.Id = 0;

            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)));

            // Act
            PieStatisticDTO result = await _vehicleService.GetVehiclePieStatisticsAsync();

            // Assert
            result.Should().NotBeNull();

            _vehicleRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _vehicleRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetVehiclePieStatisticsAsync_GetAllNotBreak_ThrowsExceptionAsync()
        {
            // Arrange
            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Throws(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _vehicleService.GetVehiclePieStatisticsAsync()).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetVehiclePieStatisticsAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region AddVehicleAsync

        [Fact]
        public async Task AddVehicleAsync_ValidVehicle_Successfully()
        {
            // Arrange
            var vehicleImageDTO = VehicleImageBuilder.VehicleImageDTO();
            vehicleImageDTO.VehicleId = 0;
            vehicleImageDTO.Id = 0;
            vehicleImageDTO.IsMain = true;

            var dto = VehicleBuilder.VehicleDTO();
            dto.Id = 0;
            dto.VehicleImages = VehicleImageBuilder.VehicleImageDTOList(vehicleImageDTO);

            var vehicle = VehicleBuilder.Vehicle(dto);
            vehicle.SetVehicleImages(VehicleImageBuilder.VehicleImageList(vehicleImageDTO));

            _vehicleRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Vehicle>())).ReturnsAsync(VehicleBuilder.Vehicle(dto));

            // Act
            VehicleDTO result = await _vehicleService.AddVehicleAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.ModelId.Should().Be(dto.ModelId);
            result.Version.Should().Be(dto.Version);
            result.FuelType.Should().Be(dto.FuelType);
            result.Price.Should().Be(dto.Price);
            result.Mileage.Should().Be(dto.Mileage);
            result.Year.Should().Be(dto.Year);
            result.Color.Should().Be(dto.Color);
            result.Doors.Should().Be(dto.Doors);
            result.Transmission.Should().Be(dto.Transmission);
            result.EngineSize.Should().Be(dto.EngineSize);
            result.Power.Should().Be(dto.Power);
            result.Observations.Should().Be(dto.Observations);
            result.Opportunity.Should().Be(dto.Opportunity);
            result.VehicleImages.Should().BeEquivalentTo(dto.VehicleImages).And.NotBeEmpty();

            _vehicleRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Vehicle>()), Times.Once);
            _vehicleRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task AddVehicleAsync_AddAsyncNotBreak_ThrowsExceptionAsync()
        {
            // Arrange   
            var dto = VehicleBuilder.VehicleDTO();

            _vehicleRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Vehicle>()))
                .ThrowsAsync(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _vehicleService.AddVehicleAsync(dto)).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.AddVehicleAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region UpdateVehicleAsync

        [Fact]
        public async Task UpdateVehicleAsync_ValidVehicle_Successfully()
        {
            // Arrange
            var vehicleImageDTO = VehicleImageBuilder.VehicleImageDTO();
            vehicleImageDTO.VehicleId = 0;
            vehicleImageDTO.Id = 0;
            vehicleImageDTO.IsMain = true;

            var dto = VehicleBuilder.VehicleDTO();
            dto.Id = 0;
            dto.VehicleImages = VehicleImageBuilder.VehicleImageDTOList(vehicleImageDTO);

            var vehicle = VehicleBuilder.Vehicle(dto);
            vehicle.SetVehicleImages(VehicleImageBuilder.VehicleImageList(vehicleImageDTO));

            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)));

            _vehicleRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Vehicle>())).ReturnsAsync(VehicleBuilder.Vehicle(dto));

            // Act
            VehicleDTO result = await _vehicleService.UpdateVehicleAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.ModelId.Should().Be(dto.ModelId);
            result.Version.Should().Be(dto.Version);
            result.FuelType.Should().Be(dto.FuelType);
            result.Price.Should().Be(dto.Price);
            result.Mileage.Should().Be(dto.Mileage);
            result.Year.Should().Be(dto.Year);
            result.Color.Should().Be(dto.Color);
            result.Doors.Should().Be(dto.Doors);
            result.Transmission.Should().Be(dto.Transmission);
            result.EngineSize.Should().Be(dto.EngineSize);
            result.Power.Should().Be(dto.Power);
            result.Observations.Should().Be(dto.Observations);
            result.Opportunity.Should().Be(dto.Opportunity);
            result.VehicleImages.Should().BeEquivalentTo(dto.VehicleImages).And.NotBeEmpty();

            _vehicleRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _vehicleRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Vehicle>()), Times.Once);
            _vehicleRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task UpdateVehicleAsync_VehicleNotFoundException_ThrowsExceptionAsync()
        {
            // Arrange   
            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryableEmpty()));

            // Act & Assert
            await FluentActions.Invoking(async () => await _vehicleService.UpdateVehicleAsync(It.IsAny<VehicleDTO>())).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.UpdateVehicleAsyncException} {DomainResources.VehicleNotFoundException}");
        }

        [Fact]
        public async Task UpdateVehicleAsync_UpdateAsyncNotBreak_ThrowsExceptionAsync()
        {
            // Arrange   
            var dto = VehicleBuilder.VehicleDTO();
            dto.Id = 0;

            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)));

            _vehicleRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Vehicle>()))
                .ThrowsAsync(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _vehicleService.UpdateVehicleAsync(dto)).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.UpdateVehicleAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region DeleteVehiclesAsync

        [Fact]
        public async Task DeleteVehiclesAsync_ValidVehicle_Successfully()
        {
            // Arrange
            var dto = VehicleBuilder.VehicleDTO();
            dto.Id = 0;
            var vehicle = VehicleBuilder.Vehicle(dto);

            List<InternalBaseResponseDTO> responseMessageDTOs = InternalBaseBuilder.InternalBaseResponseDTOList(null, vehicle.Id);

            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)));

            _vehicleRepositoryMock.Setup(repo => repo.RemoveAsync(It.IsAny<Vehicle>())).ReturnsAsync(true);

            // Act
            List<InternalBaseResponseDTO> results = await _vehicleService.DeleteVehiclesAsync(new List<long>() { dto.Id });

            // Assert
            results.Should().NotBeEmpty();
            results.Should().BeEquivalentTo(responseMessageDTOs);

            _vehicleRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _vehicleRepositoryMock.Verify(repo => repo.RemoveAsync(It.IsAny<Vehicle>()), Times.Once);
            _vehicleRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteVehiclesAsync_InvalidVehicle_VehicleNotFoundException()
        {
            // Arrange
            string errorMessage = DomainResources.VehicleNotFoundException;
            List<InternalBaseResponseDTO> internalBaseResponseDTOs = InternalBaseBuilder.InternalBaseResponseDTOList(errorMessage);

            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryableEmpty()));

            // Act
            List<InternalBaseResponseDTO> results = await _vehicleService.DeleteVehiclesAsync(new List<long> { 0 });

            // Assert
            results.Should().NotBeEmpty();
            results.Should().BeEquivalentTo(internalBaseResponseDTOs);

            _vehicleRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _vehicleRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteVehiclesAsync_GetAll_DeleteVehiclesAsyncException()
        {
            // Arrange
            string errorMessage = DomainResources.DeleteVehiclesAsyncException;

            List<InternalBaseResponseDTO> responseMessageDTOs = InternalBaseBuilder.InternalBaseResponseDTOList(errorMessage);

            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Throws(new Exception());

            // Act
            List<InternalBaseResponseDTO> results = await _vehicleService.DeleteVehiclesAsync(new List<long> { 0 });

            // Assert
            results.Should().NotBeEmpty();
            results.Should().BeEquivalentTo(responseMessageDTOs);

            _vehicleRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _vehicleRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteVehiclesAsync_RemoveAsync_DeleteVehiclesAsyncException()
        {
            // Arrange
            var dto = VehicleBuilder.VehicleDTO();
            dto.Id = 0;
            var vehicle = VehicleBuilder.Vehicle(dto);
            string errorMessage = DomainResources.DeleteVehiclesAsyncException;

            List<InternalBaseResponseDTO> internalBaseResponseDTOs = InternalBaseBuilder.InternalBaseResponseDTOList(errorMessage, vehicle.Id);

            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Vehicle>(VehicleBuilder.IQueryable(dto)));

            _vehicleRepositoryMock.Setup(repo => repo.RemoveAsync(It.IsAny<Vehicle>())).ThrowsAsync(new Exception());

            // Act
            List<InternalBaseResponseDTO> results = await _vehicleService.DeleteVehiclesAsync(new List<long> { dto.Id });

            // Assert
            results.Should().NotBeEmpty();
            results.Should().BeEquivalentTo(internalBaseResponseDTOs);

            _vehicleRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _vehicleRepositoryMock.Verify(repo => repo.RemoveAsync(It.IsAny<Vehicle>()), Times.Once);
            _vehicleRepositoryMock.VerifyNoOtherCalls();
        }

        #endregion
    }
}
