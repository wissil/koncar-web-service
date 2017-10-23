using System;
using KoncarWebService.Models;

namespace KoncarWebService.App_Persistence
{
    public interface IDataModel
    {
        T ToBusinessModel() where T : IDataModel;
    }
}
