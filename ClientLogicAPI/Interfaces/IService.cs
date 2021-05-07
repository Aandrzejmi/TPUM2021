using System;
using System.Collections.Generic;
using System.Text;
using Client.DataAPI;

namespace Client.LogicAPI.Interfaces
{
    public interface IService
    {
        public bool ValidateModel(IModel _model);
    }
}
