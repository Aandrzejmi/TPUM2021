using Client.LogicAPI;
using Client.LogicAPI.DTOs;
using Client.LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunicationAPI.Models;
using static CommunicationAPI.Serialization;

namespace Client.App.Commands
{
    class RefreshDataCommand : ICommand
    {
        public event Action OnExecute;

        public event EventHandler CanExecuteChanged;
        private IConnectionService _connectionService = Logic.CreateConnectionService();

        public bool IsActive { get; set; } = false;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var task = Task.Run(async () =>
            {
                await _connectionService.CreateConnection();
                var task1 = _connectionService.SendTask(Serialize(new CSendRequest()
                    { Type = typeof(CProduct).ToString(), RequestedID = null }));
                var task2 = _connectionService.SendTask(Serialize(new CSendRequest()
                    { Type = typeof(COrder).ToString(), RequestedID = null }));
                var task3 = _connectionService.SendTask(Serialize(new CSendRequest()
                    { Type = typeof(CEvidenceEntry).ToString(), RequestedID = null }));
                var task4 = _connectionService.SendTask(Serialize(new CSendRequest()
                    { Type = typeof(CClient).ToString(), RequestedID = null }));
                await task1;
                await task2;
                await task3;
                await task4;
            });
        }
    }
}
