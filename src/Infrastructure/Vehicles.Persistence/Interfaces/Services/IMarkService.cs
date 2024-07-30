namespace Vehicles.Persistence.Interfaces.Services
{
    public interface IMarkService
    {
        Task<List<MarkDTO>> GetAllMarksAsync();
        Task<MarkDTO> GetMarkByIdAsync(long markId);
        Task<MarkDTO> AddMarkAsync(MarkDTO markDTO);
        Task<MarkDTO> UpdateMarkAsync(MarkDTO markDTO);
        Task<List<InternalBaseResponseDTO>> DeleteMarksAsync(List<long> marksIds);
    }
}
