namespace Vehicles.Persistence.Services
{
    public class MarkService : IMarkService
    {
        #region Private variables

        private readonly IMapper _mapper;
        private readonly IMarkRepository _markRepository;

        #endregion

        #region Constructors
        public MarkService(IMapper mapper, IMarkRepository markRepository)
        {
            _mapper = mapper;
            _markRepository = markRepository;
        }
        #endregion

        #region Public methods

        public async Task<List<MarkDTO>> GetAllMarksAsync()
        {
            try
            {
                List<Mark> marks = await _markRepository
                    .GetAll()
                    .OrderBy(x => x.Id)
                    .AsNoTracking()
                    .ToListAsync();

                return _mapper.Map<List<MarkDTO>>(marks);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.GetAllMarksAsyncException} {ex.Message}");
            }
        }

        public async Task<MarkDTO> GetMarkByIdAsync(long markId)
        {
            try
            {
                Mark? mark = await _markRepository.FindByIdAsync(markId);

                mark.ThrowIfNull(() => throw new Exception(DomainResources.MarkNotFoundException));

                return _mapper.Map<MarkDTO>(mark);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.GetMarkByIdAsyncException} {ex.Message}");
            }
        }

        public async Task<MarkDTO> AddMarkAsync(MarkDTO markDTO)
        {
            try
            {
                MarkExistsAsync(markDTO).Result
                    .Throw(() => throw new Exception(DomainResources.MarkAlreadyExistsException))
                    .IfTrue();

                Mark mark = new(markDTO.Name);

                mark = await _markRepository.AddAsync(mark);

                return _mapper.Map<MarkDTO>(mark);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.AddMarkAsyncException} {ex.Message}");
            }
        }

        public async Task<MarkDTO> UpdateMarkAsync(MarkDTO markDTO)
        {
            try
            {
                Mark? mark = await _markRepository.FindByIdAsync(markDTO.Id);

                mark.ThrowIfNull(() => throw new Exception(DomainResources.MarkNotFoundException));

                MarkExistsAsync(markDTO).Result
                    .Throw(() => throw new Exception(DomainResources.MarkAlreadyExistsException))
                    .IfTrue();

                mark.SetName(markDTO.Name);

                mark = await _markRepository.UpdateAsync(mark);

                return _mapper.Map<MarkDTO>(mark);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.UpdateMarkAsyncException} {ex.Message}");
            }
        }

        public async Task<List<InternalBaseResponseDTO>> DeleteMarksAsync(List<long> marksIds)
        {
            return await DeleteAsync(marksIds);
        }

        #endregion

        #region Private methods

        private async Task<bool> MarkExistsAsync(MarkDTO markDTO)
        {
            return await _markRepository
                    .GetAll()
                    .AnyAsync(x => x.Id != markDTO.Id && x.Name.Trim().ToLower() == markDTO.Name.ToLower());
        }

        private async Task<List<InternalBaseResponseDTO>> DeleteAsync(List<long> marksIds)
        {
            List<InternalBaseResponseDTO> internalBaseResponseDTOs = new();

            foreach (long markId in marksIds)
            {
                InternalBaseResponseDTO internalBaseResponseDTO = new() { Id = markId, Success = false };
                try
                {
                    Mark? mark = await _markRepository.FindByIdAsync(markId);

                    if (mark is not null)
                    {
                        internalBaseResponseDTO.Success = await _markRepository.RemoveAsync(mark);
                    }
                    else
                    {
                        internalBaseResponseDTO.ErrorMessage = DomainResources.MarkNotFoundException;
                    }
                }
                catch (Exception)
                {
                    internalBaseResponseDTO.ErrorMessage = DomainResources.DeleteMarksAsyncException;
                }

                internalBaseResponseDTOs.Add(internalBaseResponseDTO);
            }

            return internalBaseResponseDTOs;
        }

        #endregion
    }
}
