using System;
using System.Collections.Generic;
using System.Text;
using Server.DataAPI;

namespace Server.LogicAPI.Interfaces
{
    public interface IService
    {
        public bool ValidateModel(IModel _model);
    }
}
