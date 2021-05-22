using System;
using System.Collections.Generic;
using System.Linq;
using IOTLink.Addon.AcerControl.Common;
using IOTLinkAPI.Helpers;

namespace IOTLink.Addon.AcerControl.Display
{
    public class DisplayController
    {
        public void Init()
        {
            // Usage of reverse engineered code below. It's A BIT ugly, but at least it works.
            DisplaysProvider.Init();
        }

        public void SetBrightness(int index, int level)
        {
            SelectDisplay(index);
            
            MCCSNative.Monitor_SET_Brightness((uint)level);
        }

        private void SelectDisplay(int selectedIndex)
        {
            var monitorDeviceNameList = DisplaysProvider.Monitor_DeviceName_List;
            var monitorCloneIndexList = DisplaysProvider.Monitor_CloneIndex_List;
            var monitorIsCloneList = DisplaysProvider.Monitor_Isclone_List;

            LoggerHelper.Verbose($"AcerControlAgent::DeviceNameList: {string.Join(", ",monitorDeviceNameList)}");
            LoggerHelper.Verbose($"AcerControlAgent::CloneIndexList: {string.Join(", ",monitorCloneIndexList)}");
            LoggerHelper.Verbose($"AcerControlAgent::IsCloneList: {string.Join(", ",monitorIsCloneList)}");
            
            var curr_Device_IsCloneMode = monitorIsCloneList[selectedIndex];
            var curr_Device_Clone_index = monitorCloneIndexList[selectedIndex];
            var curr_DeviceName = monitorDeviceNameList[selectedIndex];

        
            var MCCS_status = MCCSNative.init_MCCS(selectedIndex, curr_DeviceName, curr_Device_IsCloneMode, curr_Device_Clone_index);

            if (!MCCS_status)
            {
                throw new InitializationException("MCCSNative failed");
            }
        }
    }
}