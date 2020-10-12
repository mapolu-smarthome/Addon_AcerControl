using System;

namespace IOTLink.Addon.AcerControl.AcerDisplay
{
    public class Display
    {
        private DisplayInfoProvider _hard;

        public Display()
        {
            _hard = new DisplayInfoProvider();
        }

        public void Init()
        {
            var selectedIndex = 0;

            DisplayInfoProvider.Init();

            var Curr_DeviceName = _hard.Get_Monitor_DeviceName_List()[selectedIndex];
            var curr_Device_IsCloneMode = _hard.Get_Monitor_IsClone_List()[selectedIndex];
            var curr_Device_Clone_index = _hard.Get_Monitor_CloneIndex_List()[selectedIndex];
            var MCCS_status = MCCSNative.init_MCCS(selectedIndex, Curr_DeviceName, curr_Device_IsCloneMode, curr_Device_Clone_index);

            if (!MCCS_status)
            {
                throw new Exception();
            }
        }

        public void SetBrightness(int level)
        {
            MCCSNative.Monitor_SET_Brightness((uint)level);
        }
    }
}