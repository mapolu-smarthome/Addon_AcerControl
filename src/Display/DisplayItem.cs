namespace IOTLink.Addon.AcerControl.Display
{
    public class DisplayItem
    {
        private readonly DisplaysProvider _displaysProvider;

        public DisplayItem()
        {
            _displaysProvider = new DisplaysProvider();
        }

        public void Init()
        {
            const int selectedIndex = 0;

            // Usage of reverse engineered code below. It's A BIT ugly, but at least it works.

            DisplaysProvider.Init();

            var Curr_DeviceName = _displaysProvider.Get_Monitor_DeviceName_List()[selectedIndex];
            var curr_Device_IsCloneMode = _displaysProvider.Get_Monitor_IsClone_List()[selectedIndex];
            var curr_Device_Clone_index = _displaysProvider.Get_Monitor_CloneIndex_List()[selectedIndex];

            var MCCS_status = MCCSNative.init_MCCS(selectedIndex, Curr_DeviceName, curr_Device_IsCloneMode, curr_Device_Clone_index);

            if (!MCCS_status)
            {
                throw new InitializationException("MCCSNative failed");
            }
        }

        public void SetBrightness(int level)
        {
            MCCSNative.Monitor_SET_Brightness((uint)level);
        }

        public uint GetBrightness()
        {
            uint current = 0, _ = 0;

            MCCSNative.Monitor_GET_Brightness(ref _, ref current, ref _);

            return current;
        }
    }
}