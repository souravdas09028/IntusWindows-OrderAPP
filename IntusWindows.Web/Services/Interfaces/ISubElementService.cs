using IntusWindows.Common.Models;

namespace IntusWindows.Web.Services.Interfaces
{
    public interface ISubElementService
    {
        Task<IEnumerable<SubElementDTO>> GetSubElements(int windowId);
        Task<bool> AddSubElement(SubElementDTO subElementDTO);
        Task<bool> EditSubElement(SubElementDTO subElementDTO);
        Task<bool> DeleteSubElement(SubElementDTO subElementDTO);
    }
}
