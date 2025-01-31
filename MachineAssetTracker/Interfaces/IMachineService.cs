using MachineAssetTracker.Models;

namespace MachineAssetTracker.Interfaces
{
    public interface IMachineService
    {
        public List<Machine> GetAll();
        public void InsertMachine(Machine machineAsset);
        public void UpdateMachineDetails(string Id,Machine machineAsset);
        public void DeleteMachine(string id);
        public Machine GetMachineById(string id);
    }
}
