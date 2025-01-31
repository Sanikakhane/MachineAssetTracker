using MachineAssetTracker.Data;
using MachineAssetTracker.Interfaces;
using MachineAssetTracker.Models;

namespace MachineAssetTracker.Services
{
    public class MachineService : IMachineService
    {
        private MachineData _machineData = new MachineData();

        public void DeleteMachine(string id)
        {
            _machineData.DeleteMachine(id);
        }

        public List<Machine> GetAll()
        {
           return _machineData.GetAll();
        }

        public Machine GetMachineById(string id)
        {
            return _machineData.GetMachineById(id);
        }

        public void InsertMachine(Machine machineAsset)
        {
            _machineData.InsertMachineWithAssets(machineAsset, machineAsset.Assets);
        }

        public void UpdateMachineDetails(string Id,Machine machineAsset)
        {
            _machineData.UpdateMachine(Id, machineAsset);
        }
    }
}
