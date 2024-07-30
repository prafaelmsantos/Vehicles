namespace Vehicles.Persistence.Interfaces.Services
{
    public interface IModelService
    {
        Task<List<ModelDTO>> GetAllModelsAsync();
        Task<ModelDTO> GetModelByIdAsync(long modelId);
        Task<List<ModelDTO>> GetModelsByMarkIdAsync(long markId);
        Task<ModelDTO> AddModelAsync(ModelDTO modelDTO);
        Task<ModelDTO> UpdateModelAsync(ModelDTO modelDTO);
        Task<List<InternalBaseResponseDTO>> DeleteModelsAsync(List<long> modelsIds);
    }
}
