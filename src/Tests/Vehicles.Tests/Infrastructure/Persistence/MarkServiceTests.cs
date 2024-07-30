namespace Vehicles.Tests.Infrastructure.Persistence
{
    public class MarkServiceTests : BaseClassTests
    {
        #region Private variables

        private readonly Mock<IMarkRepository> _markRepositoryMock;
        private readonly IMarkService _markService;

        #endregion

        #region Constructors

        public MarkServiceTests(ITestOutputHelper output) : base(output)
        {
            _markRepositoryMock = new Mock<IMarkRepository>(MockBehavior.Strict);
            _markService = new MarkService(Mapper, _markRepositoryMock.Object);
        }

        #endregion

        #region GetAllMarksAsync

        [Fact]
        public async Task GetAllMarksAsync_GetAll_Successfully()
        {
            // Arrange   
            var dto = MarkBuilder.MarkDTO();
            dto.Id = 0;

            _markRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Mark>(MarkBuilder.IQueryable(dto)));

            // Act
            List<MarkDTO> result = await _markService.GetAllMarksAsync();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().BeEquivalentTo(MarkBuilder.MarkDTOList(dto));

            _markRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _markRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetAllMarksAsync_GetAllNotBreak_ThrowsExceptionAsync()
        {
            // Arrange   
            _markRepositoryMock.Setup(x => x.GetAll())
                .Throws(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _markService.GetAllMarksAsync()).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetAllMarksAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }
        #endregion

        #region GetMarkByIdAsync

        [Fact]
        public async Task GetMarkByIdAsync_ValidMark_Successfully()
        {
            // Arrange   
            var dto = MarkBuilder.MarkDTO();

            _markRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(MarkBuilder.Mark(dto));

            // Act
            MarkDTO result = await _markService.GetMarkByIdAsync(dto.Id);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);

            _markRepositoryMock.Verify(repo => repo.FindByIdAsync(It.IsAny<long>()), Times.Once);
            _markRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetMarkByIdAsync_MarkNotFoundException_ThrowsExceptionAsync()
        {
            // Arrange   
            _markRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(() => null!);

            // Act & Assert
            await FluentActions.Invoking(async () => await _markService.GetMarkByIdAsync(It.IsAny<long>())).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetMarkByIdAsyncException} {DomainResources.MarkNotFoundException}");
        }

        [Fact]
        public async Task GetMarkByIdAsync_FindByIdAsyncNotBreak_ThrowsExceptionAsync()
        {
            // Arrange
            _markRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>()))
                .ThrowsAsync(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _markService.GetMarkByIdAsync(It.IsAny<long>())).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.GetMarkByIdAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region AddMarkAsync

        [Fact]
        public async Task AddMarkAsync_ValidMark_Successfully()
        {
            // Arrange   
            var dto = MarkBuilder.MarkDTO();

            _markRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Mark>(MarkBuilder.IQueryableEmpty()));

            _markRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Mark>())).ReturnsAsync(MarkBuilder.Mark(dto));

            // Act
            MarkDTO result = await _markService.AddMarkAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);

            _markRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _markRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Mark>()), Times.Once);
            _markRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task AddMarkAsync_MarkAlreadyExists_ThrowsExceptionAsync()
        {
            // Arrange   
            var dto = MarkBuilder.MarkDTO();

            _markRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Mark>(MarkBuilder.IQueryable(dto)));

            // Act & Assert
            await FluentActions.Invoking(async () => await _markService.AddMarkAsync(dto)).Should()
                .ThrowAsync<Exception>()
                 .WithMessage($"{DomainResources.AddMarkAsyncException} {DomainResources.MarkAlreadyExistsException}");
        }

        [Fact]
        public async Task AddMarkAsync_AddAsyncNotBreak_ThrowsExceptionAsync()
        {
            // Arrange   
            var dto = MarkBuilder.MarkDTO();

            _markRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Mark>(MarkBuilder.IQueryableEmpty()));

            _markRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Mark>()))
                .ThrowsAsync(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _markService.AddMarkAsync(dto)).Should()
                .ThrowAsync<Exception>()
                 .WithMessage($"{DomainResources.AddMarkAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region UpdateMarkAsync

        [Fact]
        public async Task UpdateMarkAsync_ValidMark_Successfully()
        {
            // Arrange   
            var dto = MarkBuilder.MarkDTO();
            var mark = MarkBuilder.Mark(dto);

            _markRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(mark);

            _markRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Mark>(MarkBuilder.IQueryableEmpty()));

            _markRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Mark>())).ReturnsAsync(mark);

            // Act
            MarkDTO result = await _markService.UpdateMarkAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);

            _markRepositoryMock.Verify(repo => repo.FindByIdAsync(It.IsAny<long>()), Times.Once);
            _markRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _markRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Mark>()), Times.Once);
            _markRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task UpdateMarkAsync_MarkNotFoundException_ThrowsExceptionAsync()
        {
            // Arrange   
            var dto = MarkBuilder.MarkDTO();

            _markRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(() => null!);

            // Act & Assert
            await FluentActions.Invoking(async () => await _markService.UpdateMarkAsync(dto)).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.UpdateMarkAsyncException} {DomainResources.MarkNotFoundException}");
        }

        [Fact]
        public async Task UpdateMarkAsync_MarkAlreadyExistsException_ThrowsExceptionAsync()
        {
            // Arrange   
            var dto = MarkBuilder.MarkDTO();

            _markRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(MarkBuilder.Mark(dto));

            _markRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Mark>(MarkBuilder.IQueryable(dto)));

            // Act & Assert
            await FluentActions.Invoking(async () => await _markService.UpdateMarkAsync(dto)).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.UpdateMarkAsyncException} {DomainResources.MarkAlreadyExistsException}");
        }

        [Fact]
        public async Task UpdateMarkAsync_UpdateAsyncNotBreak_ThrowsExceptionAsync()
        {
            // Arrange   
            var dto = MarkBuilder.MarkDTO();

            _markRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(MarkBuilder.Mark(dto));

            _markRepositoryMock.Setup(x => x.GetAll())
                .Returns(new TestAsyncEnumerable<Mark>(MarkBuilder.IQueryableEmpty()));

            _markRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Mark>()))
                .ThrowsAsync(new Exception(ExceptionBuilder.ExceptionMessage));

            // Act & Assert
            await FluentActions.Invoking(async () => await _markService.UpdateMarkAsync(dto)).Should()
                .ThrowAsync<Exception>()
                .WithMessage($"{DomainResources.UpdateMarkAsyncException} {ExceptionBuilder.ExceptionMessage}");
        }

        #endregion

        #region DeleteMarksAsync

        [Fact]
        public async Task DeleteMarksAsync_ValidMark_Successfully()
        {
            // Arrange
            var dto = MarkBuilder.MarkDTO();
            dto.Id = 0;
            var mark = MarkBuilder.Mark(dto);
            List<InternalBaseResponseDTO> internalBaseResponseDTOs = InternalBaseBuilder.InternalBaseResponseDTOList(null, mark.Id);

            _markRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(mark);

            _markRepositoryMock.Setup(repo => repo.RemoveAsync(It.IsAny<Mark>())).ReturnsAsync(true);

            // Act
            List<InternalBaseResponseDTO> results = await _markService.DeleteMarksAsync(new List<long> { dto.Id });

            // Assert
            results.Should().NotBeEmpty();
            results.Should().BeEquivalentTo(internalBaseResponseDTOs);

            _markRepositoryMock.Verify(repo => repo.FindByIdAsync(It.IsAny<long>()), Times.Once);
            _markRepositoryMock.Verify(repo => repo.RemoveAsync(It.IsAny<Mark>()), Times.Once);
            _markRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteMarksAsync_InvalidMark_MarkNotFoundException()
        {
            // Arrange
            string errorMessage = DomainResources.MarkNotFoundException;
            List<InternalBaseResponseDTO> internalBaseResponseDTOs = InternalBaseBuilder.InternalBaseResponseDTOList(errorMessage);

            _markRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>()))!.ReturnsAsync(() => null!);

            // Act
            List<InternalBaseResponseDTO> results = await _markService.DeleteMarksAsync(new List<long> { 0 });

            // Assert
            results.Should().NotBeEmpty();
            results.Should().BeEquivalentTo(internalBaseResponseDTOs);

            _markRepositoryMock.Verify(repo => repo.FindByIdAsync(It.IsAny<long>()), Times.Once);
            _markRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteMarksAsync_FindByIdAsync_DeleteMarksAsyncException()
        {
            // Arrange
            string errorMessage = DomainResources.DeleteMarksAsyncException;

            List<InternalBaseResponseDTO> internalBaseResponseDTOs = InternalBaseBuilder.InternalBaseResponseDTOList(errorMessage);

            _markRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ThrowsAsync(new Exception());

            // Act
            List<InternalBaseResponseDTO> results = await _markService.DeleteMarksAsync(new List<long> { 0 });

            // Assert
            results.Should().NotBeEmpty();
            results.Should().BeEquivalentTo(internalBaseResponseDTOs);

            _markRepositoryMock.Verify(repo => repo.FindByIdAsync(It.IsAny<long>()), Times.Once);
            _markRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteMarksAsync_RemoveAsync_DeleteMarksAsyncException()
        {
            // Arrange
            var dto = MarkBuilder.MarkDTO();
            dto.Id = 0;
            var mark = MarkBuilder.Mark(dto);
            string errorMessage = DomainResources.DeleteMarksAsyncException;

            List<InternalBaseResponseDTO> responseMessageDTOs = InternalBaseBuilder.InternalBaseResponseDTOList(errorMessage, mark.Id);

            _markRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<long>())).ReturnsAsync(mark);

            _markRepositoryMock.Setup(repo => repo.RemoveAsync(It.IsAny<Mark>())).ThrowsAsync(new Exception());

            // Act
            List<InternalBaseResponseDTO> results = await _markService.DeleteMarksAsync(new List<long> { dto.Id });

            // Assert
            results.Should().NotBeEmpty();
            results.Should().BeEquivalentTo(responseMessageDTOs);

            _markRepositoryMock.Verify(repo => repo.FindByIdAsync(It.IsAny<long>()), Times.Once);
            _markRepositoryMock.Verify(repo => repo.RemoveAsync(It.IsAny<Mark>()), Times.Once);
            _markRepositoryMock.VerifyNoOtherCalls();
        }

        #endregion
    }
}
