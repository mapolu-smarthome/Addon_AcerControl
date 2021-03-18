using System.Runtime.InteropServices;

namespace IOTLink.Addon.AcerControl.Display
{
	public class MCCSNative
	{
		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool init_MCCS(int monitor_index, [MarshalAs(UnmanagedType.LPWStr)] string DeviceName, int IsClone, int clone_index);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FreePhysicalMonitor();

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool acer_init_windows_evenet_hook(uint Current_DisplayMode);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void acer_unhook_windows_event();

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool init_AppSync();

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool acer_set_app_string([MarshalAs(UnmanagedType.LPWStr)] string add_app, int display_mode, int add_mode_select);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool acer_delete_AppfromList([MarshalAs(UnmanagedType.LPWStr)] string delete_app, int app_index);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void print_App_list();

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void acer_query_monitor_model_name();

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_SET_Brightness(uint brtightness);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_SET_Contrast(uint contrast);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_SET_Rgain(uint Rgain);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_SET_Ggain(uint Ggain);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_SET_Bgain(uint Bgain);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_SET_Saturation(uint saturation);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_SET_SPK_Volume(uint vol_level);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_SET_VCP_code(byte VCP_code, uint level);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_Gamma(uint gamma_index);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_ColorTemp(uint colortemp_mode);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_BlueLight(uint bluelight_level);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_BlackBoost(uint blackboost_level);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_DisplayMode(uint Monitor_Display_Mode);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_OD(uint OD_level);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_AimType(uint AimType);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_PowerIndicator(uint PowerIndicator);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_PowerKey(uint PowerKey);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_KeyLock(uint KeyLock);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_RefreshRate(uint RefreshRate);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_FactoryReset();

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_PowerModeControl(uint PowerMode);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_ColorSpace(uint ColorSpace);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Set_Calibration_Mode(uint Calibration_Mode);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_Brightness(ref uint Min, ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_Contrast(ref uint Min, ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_Rgain(ref uint Min, ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_Ggain(ref uint Min, ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_Bgain(ref uint Min, ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_Saturation(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_test(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_SPK_Volume(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Get_VCP_code(byte VCP_code, ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_Gamma(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_ColorTemp(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_BlueLight(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_BlackBoost(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_DisplayMode(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_OD(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_AimType(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_PowerIndicator(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_PowerKey(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_KeyLock(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_RefreshRate(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Query_ModelCategory(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Query_SupportDisplayMode(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Get_VCP_Support_List();

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_Decode_VCP_String(ref bool OD_Support, ref bool Refresh_Rate_Num_Support, ref bool SPK_VOL_Support);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool Monitor_Get_SN(ref uint Cur, ref uint Max, int read_num, int DelayTime);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int Acer_get_Monitor_index();

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Monitor_GET_ColorSpace(ref uint Cur, ref uint Max);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Acer_Set_Screen_Rotation([MarshalAs(UnmanagedType.LPWStr)] string FullPath_Device_ID);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Acer_Get_Screen_Position([MarshalAs(UnmanagedType.LPWStr)] string FullPath_Device_ID, ref int screen_x, ref int screen_y);

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Acer_Set_Screen_Position([MarshalAs(UnmanagedType.LPWStr)] string FullPath_Device_ID, int screen_x, int screen_y);
	}
}
