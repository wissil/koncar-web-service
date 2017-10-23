using System;

using KoncarWebService.App_Persistence;

namespace KoncarWebService.Models
{
    public interface IBusinessModel<T> where T : IDataModel
    {
        T ToDataModel();
    }
}
