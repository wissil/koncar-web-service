using KoncarWebService.Data;

namespace KoncarWebService.Models
{
    public interface IDataObject
    {
        IDTObject ToDto();
    }
}
