namespace Vehicles.Tests.Infrastructure.Persistence
{
    public class ModelServiceTests : BaseClassTests
    {
        #region Private variables

        private readonly Mock<IModelRepository> _modelRepositoryMock;
        private readonly IModelService _modelService;

        #endregion

        #region Constructors

        public ModelServiceTests(ITestOutputHelper output) : base(output)
        {
            _modelRepositoryMock = new Mock<IModelRepository>(MockBehavior.Strict);
            _modelService = new ModelService(Mapper, _modelRepositoryMock.Object);
        }

        #endregion

        #region GetAllModelsAsync

        [Fact]
        public async Task GetAllModelsAsync_GetAll_Successfully()
        {
            // Arrange   
            var dto = ModelBuilder.ModelDTO();
            dto.Id = 0;

            _modelRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Model>(ModelBuilder.IQueryable(dto)));

            // Act
            List<ModelDTO> result = await _modelService.GetAllModelsAsync();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(ModelBuilder.ModelDTOList(dto));

            _modelRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _modelRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetAllModelsAsync_GetAllNotBreak_ThrowsExceptionAsync()
        {
            // Arrange   
            _modelRepositoryMock.Setup(x => x.GetAll())
                .Throws(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _modelService.GetAllModelsAsync()).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetAllModelsAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }
        #endregion

        #region GetModelByIdAsync

        [Fact]
        public async Task GetModelByIdAsync_ValidModel_Successfully()
        {
            // Arrange   
            var dto = ModelBuilder.ModelDTO();
            dto.Id = 0;

            _modelRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Model>(ModelBuilder.IQueryable(dto)));

            // Act
            ModelDTO result = await _modelService.GetModelByIdAsync(dto.Id);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);
            result.MarkId.Should().Be(dto.MarkId);

            _modelRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _modelRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetModelByIdAsync_ModelNotFoundException_ThrowsExceptionAsync()
        {
            // Arrange   
            _modelRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Model>(ModelBuilder.IQueryableEmpty()));

            // Act & Assert
            await FluentActions.Invoking(async () => await _modelService.GetModelByIdAsync(It.IsAny<long>())).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetModelByIdAsyncException} {DomainResources.ModelNotFoundException}");
        }

        [Fact]
        public async Task GetModelByIdAsync_GetAllNotBreak_ThrowsExceptionAsync()
        {
            // Arrange
            _modelRepositoryMock.Setup(x => x.GetAll())
                .Throws(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _modelService.GetModelByIdAsync(It.IsAny<long>())).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetModelByIdAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region GetModelsByMarkIdAsync

        [Fact]
        public async Task GetModelsByMarkIdAsync_GetAll_Successfully()
        {
            // Arrange   
            var dto = ModelBuilder.ModelDTO();
            dto.Id = 0;

            _modelRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Model>(ModelBuilder.IQueryable(dto)));

            // Act
            List<ModelDTO> result = await _modelService.GetModelsByMarkIdAsync(dto.MarkId);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(ModelBuilder.ModelDTOList(dto));

            _modelRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _modelRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetModelsByMarkIdAsync_GetAllNotBreak_ThrowsExceptionAsync()
        {
            // Arrange   
            _modelRepositoryMock.Setup(x => x.GetAll())
                .Throws(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _modelService.GetModelsByMarkIdAsync(It.IsAny<long>())).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetModelsByMarkIdAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }
        #endregion

        #region AddModelAsync

        [Fact]
        public async Task AddModelAsync_ValidModel_Successfully()
        {
            // Arrange   
            var dto = ModelBuilder.ModelDTO();

            _modelRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Model>(ModelBuilder.IQueryableEmpty()));

            _modelRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Model>())).ReturnsAsync(ModelBuilder.Model(dto));

            // Act
            ModelDTO result = await _modelService.AddModelAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);
            result.MarkId.Should().Be(dto.MarkId);

            _modelRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _modelRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Model>()), Times.Once);
            _modelRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task AddModelAsync_ModelAlreadyExists_ThrowsExceptionAsync()
        {
            // Arrange   
            var dto = ModelBuilder.ModelDTO();

            _modelRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Model>(ModelBuilder.IQueryable(dto)));

            // Act & Assert
            await FluentActions.Invoking(async () => await _modelService.AddModelAsync(dto)).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.AddModelAsyncException} {DomainResources.ModelAlreadyExistsException}");
        }

        [Fact]
        public async Task AddModelAsync_AddAsyncNotBreak_ThrowsExceptionAsync()
        {
            // Arrange   
            var dto = ModelBuilder.ModelDTO();

            _modelRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Model>(ModelBuilder.IQueryableEmpty()));

            _modelRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Model>()))
                .ThrowsAsync(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _modelService.AddModelAsync(dto)).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.AddModelAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region UpdateModelAsync

        [Fact]
        public async Task UpdateModelAsync_ValidModel_Successfully()
        {
            // Arrange   
            var dto = ModelBuilder.ModelDTO();

            _modelRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(ModelBuilder.Model(dto));

            _modelRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Model>(ModelBuilder.IQueryableEmpty()));

            _modelRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Model>())).ReturnsAsync(ModelBuilder.Model(dto));

            // Act
            ModelDTO result = await _modelService.UpdateModelAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);
            result.MarkId.Should().Be(dto.MarkId);

            _modelRepositoryMock.Verify(repo => repo.FindByIdAsync(It.IsAny<long>()), Times.Once);
            _modelRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _modelRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Model>()), Times.Once);
            _modelRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task UpdateModelAsync_ModelNotFoundException_ThrowsExceptionAsync()
        {
            // Arrange   
            var dto = ModelBuilder.ModelDTO();

            _modelRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(() => null!);

            // Act & Assert
            await FluentActions.Invoking(async () => await _modelService.UpdateModelAsync(dto)).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.UpdateModelAsyncException} {DomainResources.ModelNotFoundException}");
        }

        [Fact]
        public async Task UpdateModelAsync_ModelAlreadyExistsException_ThrowsExceptionAsync()
        {
            // Arrange   
            var dto = ModelBuilder.ModelDTO();

            _modelRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>()))!.ReturnsAsync(ModelBuilder.Model(dto));

            _modelRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Model>(ModelBuilder.IQueryable(dto)));

            // Act & Assert
            await FluentActions.Invoking(async () => await _modelService.UpdateModelAsync(dto)).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.UpdateModelAsyncException} {DomainResources.ModelAlreadyExistsException}");
        }

        [Fact]
        public async Task UpdateModelAsync_UpdateAsyncNotBreak_ThrowsExceptionAsync()
        {
            // Arrange   
            var dto = ModelBuilder.ModelDTO();

            _modelRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(ModelBuilder.Model(dto));

            _modelRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Model>(ModelBuilder.IQueryableEmpty()));

            _modelRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Model>()))
                .ThrowsAsync(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _modelService.UpdateModelAsync(dto)).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.UpdateModelAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region DeleteModelsAsync

        [Fact]
        public async Task DeleteModelsAsync_ValidModel_Successfully()
        {
            // Arrange
            var dto = ModelBuilder.ModelDTO();
            dto.Id = 0;
            var model = ModelBuilder.Model(dto);
            List<InternalBaseResponseDTO> internalBaseResponseDTOs = InternalBaseBuilder.InternalBaseResponseDTOList(null, model.Id);

            _modelRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(model);

            _modelRepositoryMock.Setup(repo => repo.RemoveAsync(It.IsAny<Model>())).ReturnsAsync(true);

            // Act
            List<InternalBaseResponseDTO> results = await _modelService.DeleteModelsAsync(new List<long> { dto.Id });

            // Assert
            results.Should().NotBeEmpty();
            results.Should().BeEquivalentTo(internalBaseResponseDTOs);

            _modelRepositoryMock.Verify(repo => repo.FindByIdAsync(It.IsAny<long>()), Times.Once);
            _modelRepositoryMock.Verify(repo => repo.RemoveAsync(It.IsAny<Model>()), Times.Once);
            _modelRepositoryMock.VerifyNoOtherCalls();

        }

        [Fact]
        public async Task DeleteModelsAsync_InvalidModel_ModelNotFoundException()
        {
            // Arrange
            Model? model = null;
            string errorMessage = DomainResources.ModelNotFoundException;
            List<InternalBaseResponseDTO> internalBaseResponseDTOs = InternalBaseBuilder.InternalBaseResponseDTOList(errorMessage);

            _modelRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>()))!.ReturnsAsync(model);

            // Act
            List<InternalBaseResponseDTO> results = await _modelService.DeleteModelsAsync(new List<long> { 0 });

            // Assert
            results.Should().NotBeEmpty();
            results.Should().BeEquivalentTo(internalBaseResponseDTOs);

            _modelRepositoryMock.Verify(repo => repo.FindByIdAsync(It.IsAny<long>()), Times.Once);
            _modelRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteModelsAsync_FindByIdAsync_DeleteModelsAsyncException()
        {
            // Arrange
            string errorMessage = DomainResources.DeleteModelsAsyncException;

            List<InternalBaseResponseDTO> internalBaseResponseDTOs = InternalBaseBuilder.InternalBaseResponseDTOList(errorMessage);

            _modelRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ThrowsAsync(new Exception());

            // Act
            List<InternalBaseResponseDTO> results = await _modelService.DeleteModelsAsync(new List<long> { 0 });

            // Assert
            results.Should().NotBeEmpty();
            results.Should().BeEquivalentTo(internalBaseResponseDTOs);

            _modelRepositoryMock.Verify(repo => repo.FindByIdAsync(It.IsAny<long>()), Times.Once);
            _modelRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteModelsAsync_RemoveAsync_DeleteModelsAsyncException()
        {
            // Arrange
            var dto = ModelBuilder.ModelDTO();
            dto.Id = 0;
            var model = ModelBuilder.Model(dto);
            string errorMessage = DomainResources.DeleteModelsAsyncException;

            List<InternalBaseResponseDTO> internalBaseResponseDTOs = InternalBaseBuilder.InternalBaseResponseDTOList(errorMessage, model.Id);

            _modelRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(model);

            _modelRepositoryMock.Setup(repo => repo.RemoveAsync(It.IsAny<Model>())).ThrowsAsync(new Exception());

            // Act
            List<InternalBaseResponseDTO> results = await _modelService.DeleteModelsAsync(new List<long> { dto.Id });

            // Assert
            results.Should().NotBeEmpty();
            results.Should().BeEquivalentTo(internalBaseResponseDTOs);

            _modelRepositoryMock.Verify(repo => repo.FindByIdAsync(It.IsAny<long>()), Times.Once);
            _modelRepositoryMock.Verify(repo => repo.RemoveAsync(It.IsAny<Model>()), Times.Once);
            _modelRepositoryMock.VerifyNoOtherCalls();
        }
        #endregion
    }
}
