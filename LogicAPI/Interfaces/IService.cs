using System;
using System.Collections.Generic;
using System.Text;
using DataAPI;

namespace LogicAPI.Interfaces
{
    public interface IService
    {
        public bool ValidateModel(IModel _model);
    }
}
