namespace Vehicles.Persistence.Services
{
    public class ModelService : IModelService
    {
        #region Private variables

        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepository;

        #endregion

        #region Constructors

        public ModelService(IMapper mapper, IModelRepository modelRepository)
        {
            _mapper = mapper;
            _modelRepository = modelRepository;
        }

        #endregion

        #region Public methods

        public async Task<List<ModelDTO>> GetAllModelsAsync()
        {
            try
            {
                List<Model> models = await _modelRepository
                    .GetAll()
                    .Include(x => x.Mark)
                    .OrderBy(x => x.Id)
                    .AsNoTracking()
                    .ToListAsync();

                return _mapper.Map<List<ModelDTO>>(models);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.GetAllModelsAsyncException} {ex.Message}");
            }
        }

        public async Task<ModelDTO> GetModelByIdAsync(long modelId)
        {
            try
            {
                Model? model = await _modelRepository
                    .GetAll()
                    .Where(x => x.Id == modelId)
                    .Include(x => x.Mark)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                model.ThrowIfNull(() => throw new Exception(DomainResources.ModelNotFoundException));

                return _mapper.Map<ModelDTO>(model);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.GetModelByIdAsyncException} {ex.Message}");
            }
        }

        public async Task<List<ModelDTO>> GetModelsByMarkIdAsync(long markId)
        {
            try
            {
                List<Model> models = await _modelRepository
                    .GetAll()
                    .Where(x => x.MarkId == markId)
                    .OrderBy(x => x.Id)
                    .AsNoTracking()
                    .ToListAsync();

                return _mapper.Map<List<ModelDTO>>(models);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.GetModelsByMarkIdAsyncException} {ex.Message}");
            }
        }

        public async Task<ModelDTO> AddModelAsync(ModelDTO modelDTO)
        {
            try
            {
                ModelExistsAsync(modelDTO).Result
                    .Throw(() => throw new Exception(DomainResources.ModelAlreadyExistsException))
                    .IfTrue();

                Model model = new(modelDTO.Name, modelDTO.MarkId);

                await _modelRepository.AddAsync(model);

                return _mapper.Map<ModelDTO>(model);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.AddModelAsyncException} {ex.Message}");
            }
        }

        public async Task<ModelDTO> UpdateModelAsync(ModelDTO modelDTO)
        {
            try
            {
                Model? model = await _modelRepository.FindByIdAsync(modelDTO.Id);

                model.ThrowIfNull(() => throw new Exception(DomainResources.ModelNotFoundException));

                ModelExistsAsync(modelDTO).Result
                    .Throw(() => throw new Exception(DomainResources.ModelAlreadyExistsException))
                    .IfTrue();

                model.UpdateModel(modelDTO.Name, modelDTO.MarkId);

                await _modelRepository.UpdateAsync(model);

                return _mapper.Map<ModelDTO>(model);
            }
            catch (Exception ex)
            {
                throw new Exception($"{DomainResources.UpdateModelAsyncException} {ex.Message}");
            }
        }

        public async Task<List<InternalBaseResponseDTO>> DeleteModelsAsync(List<long> modelsIds)
        {
            return await DeleteAsync(modelsIds);
        }

        #endregion

        #region Private methods

        private async Task<bool> ModelExistsAsync(ModelDTO modelDTO)
        {
            return await _modelRepository
                    .GetAll()
                    .AnyAsync(x => x.Id != modelDTO.Id && x.Name.Trim().ToLower() == modelDTO.Name.ToLower());
        }

        private async Task<List<InternalBaseResponseDTO>> DeleteAsync(List<long> modelsIds)
        {
            List<InternalBaseResponseDTO> internalBaseResponseDTOs = new();

            foreach (var markId in modelsIds)
            {
                InternalBaseResponseDTO internalBaseResponseDTO = new() { Id = markId, Success = false };
                try
                {
                    Model? model = await _modelRepository.FindByIdAsync(markId);

                    if (model is not null)
                    {
                        internalBaseResponseDTO.Success = await _modelRepository.RemoveAsync(model);
                    }
                    else
                    {
                        internalBaseResponseDTO.ErrorMessage = DomainResources.ModelNotFoundException;
                    }
                }
                catch (Exception)
                {
                    internalBaseResponseDTO.ErrorMessage = DomainResources.DeleteModelsAsyncException;
                }

                internalBaseResponseDTOs.Add(internalBaseResponseDTO);
            }

            return internalBaseResponseDTOs;
        }

        #endregion
    }
}
