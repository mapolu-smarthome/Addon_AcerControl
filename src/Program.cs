using IOTLink.Addon.AcerControl.Display;

namespace IOTLink.Addon.AcerControl.Common
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DisplaysProvider.Init();

            SelectDisplay(1);
        }
        
        private static void SelectDisplay(int selectedIndex)
        {
            var monitorDeviceNameList = DisplaysProvider.Monitor_DeviceName_List;
            var monitorCloneIndexList = DisplaysProvider.Monitor_CloneIndex_List;
            var monitorIsCloneList = DisplaysProvider.Monitor_Isclone_List;

            var curr_Device_IsCloneMode = monitorIsCloneList[selectedIndex];

            var curr_Device_Clone_index = monitorCloneIndexList[selectedIndex];

            var Curr_DeviceName = monitorDeviceNameList[selectedIndex];

            var MCCS_status = MCCSNative.init_MCCS(selectedIndex, Curr_DeviceName, curr_Device_IsCloneMode, curr_Device_Clone_index);

            if (!MCCS_status)
            {
                throw new InitializationException("MCCSNative failed");
            }
        }
    }
}