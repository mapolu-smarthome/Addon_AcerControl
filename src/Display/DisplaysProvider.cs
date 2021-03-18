using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace IOTLink.Addon.AcerControl.Display
{
	public class DisplaysProvider
	{
		public struct DEVMODE
		{
			private const int CCHDEVICENAME = 32;

			private const int CCHFORMNAME = 32;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmDeviceName;

			public short dmSpecVersion;

			public short dmDriverVersion;

			public short dmSize;

			public short dmDriverExtra;

			public int dmFields;

			public int dmPositionX;

			public int dmPositionY;

			public int dmDisplayOrientation;

			public int dmDisplayFixedOutput;

			public short dmColor;

			public short dmDuplex;

			public short dmYResolution;

			public short dmTTOption;

			public short dmCollate;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmFormName;

			public short dmLogPixels;

			public int dmBitsPerPel;

			public int dmPelsWidth;

			public int dmPelsHeight;

			public int dmDisplayFlags;

			public int dmDisplayFrequency;

			public int dmICMMethod;

			public int dmICMIntent;

			public int dmMediaType;

			public int dmDitherType;

			public int dmReserved1;

			public int dmReserved2;

			public int dmPanningWidth;

			public int dmPanningHeight;
		}

		[Flags]
		public enum DisplayDeviceStateFlags
		{
			AttachedToDesktop = 0x1,
			MultiDriver = 0x2,
			ActiveDevice = 0x3,
			PrimaryDevice = 0x4,
			MirroringDriver = 0x8,
			VGACompatible = 0x10,
			Removable = 0x20,
			ModesPruned = 0x8000000,
			Remote = 0x4000000,
			Disconnect = 0x2000000
		}

		public struct DISPLAY_DEVICE
		{
			[MarshalAs(UnmanagedType.U4)]
			public int cb;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string DeviceName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string DeviceString;

			[MarshalAs(UnmanagedType.U4)]
			public DisplayDeviceStateFlags StateFlags;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string DeviceID;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string DeviceKey;
		}

		public static short monitor_num;

		public static List<string> Monitor_DeviceID_List = new List<string>();

		public static List<string> Monitor_ModelName_List = new List<string>();

		public static List<string> Monitor_screen_resolution_List = new List<string>();

		public static List<string> Monitor_SN_num_List = new List<string>();

		public static List<string> Monitor_DeviceName_List = new List<string>();

		public static List<int> Monitor_Isclone_List = new List<int>();

		public static List<int> Monitor_CloneIndex_List = new List<int>();

		public static List<string> Monitor_DriverID_List = new List<string>();

		public static List<string> Monitor_FullPath_DriverID_List = new List<string>();

		private const string ACER_ID = "04 72";

		private const int TAG_ACER_PRODUCT_ID = 0;

		private const int TAG_CURRENT_REFRESH_RATE = 1;

		private const int TAG_OVERCLOCK_REFRESH_RATE = 2;

		private const int DEFAULT_REFRESH_RATE = 60;

		private const int MAX_EDID_SIZE = 384;

		private const int EDID_DEFAULT_SIZE = 128;

		public static int Current_Refresh_Rate = 60;

		public static int Overclock_Refresh_Rate = 60;

		private static byte[] Read_EDID_Block = new byte[384];

		private static char[] EDID_SPILIT_SPACE = new char[1]
		{
			' '
		};

		public const int ENUM_CURRENT_SETTINGS = -1;

		public List<string> Get_Monitor_FullPath_DeviceID_List()
		{
			return Monitor_FullPath_DriverID_List;
		}

		public List<string> Get_Monitor_DeviceID_List()
		{
			return Monitor_DeviceID_List;
		}

		public List<string> Get_Monitor_ModelName_List()
		{
			return Monitor_ModelName_List;
		}

		public List<string> Get_Monitor_SN_num_List()
		{
			return Monitor_SN_num_List;
		}

		public List<string> Get_Monitor_screen_resolution_List()
		{
			return Monitor_screen_resolution_List;
		}

		public List<string> Get_Monitor_DeviceName_List()
		{
			return Monitor_DeviceName_List;
		}

		public List<int> Get_Monitor_IsClone_List()
		{
			return Monitor_Isclone_List;
		}

		public List<int> Get_Monitor_CloneIndex_List()
		{
			return Monitor_CloneIndex_List;
		}

		public List<string> Get_Monitor_DriverID_List()
		{
			return Monitor_DriverID_List;
		}

		public static void AcerReset_AllList()
		{
			Monitor_DeviceID_List.Clear();
			Monitor_ModelName_List.Clear();
			Monitor_SN_num_List.Clear();
			Monitor_screen_resolution_List.Clear();
			Monitor_DeviceName_List.Clear();
			Monitor_Isclone_List.Clear();
			Monitor_CloneIndex_List.Clear();
			Monitor_DriverID_List.Clear();
			Monitor_FullPath_DriverID_List.Clear();
			Current_Refresh_Rate = 60;
			Overclock_Refresh_Rate = 60;
			Array.Clear(Read_EDID_Block, 0, Read_EDID_Block.Length);
		}

		[DllImport("Monitor_MCCS_API.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool GetCurrentDisplayConfig();

		[DllImport("user32.dll")]
		private static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

		[DllImport("user32.dll")]
		private static extern bool EnumDisplaySettings(string lpDevice, int iModeNum, ref DEVMODE lpDevMode);

		public static string AcerListDisplaySetting(string lpDevice)
		{
			DEVMODE lpDevMode = default(DEVMODE);
			if (EnumDisplaySettings(lpDevice, -1, ref lpDevMode))
			{
				return lpDevMode.dmPelsWidth + "x" + lpDevMode.dmPelsHeight + "@" + lpDevMode.dmDisplayFrequency + " Hz";
			}
			return "n/a";
		}

		public static void Init()
		{
			char[] separator = new char[1]
			{
				'\\'
			};
			string text = null;
			int num = 0;
			bool flag = false;
			monitor_num = 0;
			AcerReset_AllList();
			DISPLAY_DEVICE lpDisplayDevice = default(DISPLAY_DEVICE);
			lpDisplayDevice.cb = Marshal.SizeOf(lpDisplayDevice);
			try
			{
				for (uint num2 = 0u; EnumDisplayDevices(null, num2, ref lpDisplayDevice, 0u); num2++)
				{
					if (lpDisplayDevice.StateFlags.HasFlag(DisplayDeviceStateFlags.AttachedToDesktop))
					{
						DISPLAY_DEVICE lpDisplayDevice2 = default(DISPLAY_DEVICE);
						lpDisplayDevice2.cb = Marshal.SizeOf(lpDisplayDevice2);
						for (uint num3 = 0u; EnumDisplayDevices(lpDisplayDevice.DeviceName, num3, ref lpDisplayDevice2, 0u); num3++)
						{
							if (lpDisplayDevice2.StateFlags.HasFlag(DisplayDeviceStateFlags.ActiveDevice))
							{
								text = AcerListDisplaySetting(lpDisplayDevice.DeviceName);
								Monitor_screen_resolution_List.Add(text);
								Monitor_DeviceName_List.Add(lpDisplayDevice.DeviceName);
								string[] array = lpDisplayDevice2.DeviceID.Split(separator);
								Monitor_FullPath_DriverID_List.Add(lpDisplayDevice2.DeviceID);
								Monitor_DeviceID_List.Add(array[1]);
								Monitor_DriverID_List.Add(array[3]);
								monitor_num++;
								Monitor_Isclone_List.Add(0);
								Monitor_CloneIndex_List.Add(0);
								if (monitor_num > 1)
								{
									num = 0;
									flag = false;
									for (int i = 0; i < monitor_num - 1; i++)
									{
										if (string.Compare(Monitor_DeviceName_List[i], lpDisplayDevice.DeviceName, ignoreCase: false) == 0)
										{
											Monitor_Isclone_List[i] = 1;
											Monitor_Isclone_List[monitor_num - 1] = 1;
											num++;
											flag = true;
										}
									}
									if (flag)
									{
										Monitor_CloneIndex_List[monitor_num - 1] = num;
									}
								}
							}
							lpDisplayDevice2.cb = Marshal.SizeOf(lpDisplayDevice2);
						}
					}
					lpDisplayDevice.cb = Marshal.SizeOf(lpDisplayDevice);
				}
				for (int j = 0; j < monitor_num; j++)
				{
					AcerGetEDID_Parser(Monitor_DeviceID_List[j], Monitor_DriverID_List[j]);
				}
			}
			catch (Exception)
			{
			}
        }

		public static string IntToHex(int value)
		{
			return $"{value:X}";
		}

		public static void AcerGetEDID_Parser(string Connect_MonitorID, string Connect_DriverID)
		{
			string text = null;
			string text2 = null;
			char[] separator = new char[1]
			{
				'\\'
			};
			RegistryKey registryKey = Registry.LocalMachine;
			bool flag = false;
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum\\DISPLAY");
			}
			catch
			{
				flag = true;
			}
			if (!flag && registryKey != null)
			{
				string[] subKeyNames = registryKey.GetSubKeyNames();
				foreach (string text3 in subKeyNames)
				{
					if (string.Compare(text3, Connect_MonitorID, ignoreCase: false) != 0)
					{
						continue;
					}
					RegistryKey registryKey2 = registryKey.OpenSubKey(text3);
					if (registryKey2 == null)
					{
						continue;
					}
					string[] subKeyNames2 = registryKey2.GetSubKeyNames();
					foreach (string name in subKeyNames2)
					{
						RegistryKey registryKey3 = registryKey2.OpenSubKey(name);
						if (registryKey3 == null)
						{
							continue;
						}
						string[] subKeyNames3 = registryKey3.GetSubKeyNames();
						string[] array = registryKey3.GetValue("Driver").ToString().Split(separator);
						if (string.Compare(Connect_DriverID, array[1], ignoreCase: false) != 0 || !subKeyNames3.Contains("Device Parameters"))
						{
							continue;
						}
						RegistryKey registryKey4 = registryKey3.OpenSubKey("Device Parameters");
						new string(new char[4]
						{
							'\0',
							'\0',
							'\0',
							'ÿ'
						});
						new string(new char[4]
						{
							'\0',
							'\0',
							'\0',
							'ü'
						});
						string text4 = null;
						byte[] array2 = registryKey4.GetValue("EDID", null) as byte[];
						if (array2 == null)
						{
							return;
						}
						byte[] array3 = new byte[4];
						for (int k = 54; k < 109; k += 18)
						{
							StringBuilder stringBuilder = new StringBuilder();
							Buffer.BlockCopy(array2, k, array3, 0, 4);
							Array.Reverse(array3);
							if (BitConverter.ToInt32(array3, 0).Equals(255))
							{
								for (int l = k + 5; array2[l] != 10 && l < k + 18; l++)
								{
									stringBuilder.Append((char)array2[l]);
								}
								text = stringBuilder.ToString();
								stringBuilder.Clear();
							}
							if (text == null)
							{
								StringBuilder stringBuilder2 = new StringBuilder();
								for (int m = 12; m <= 15; m++)
								{
									text4 = Convert.ToString(array2[m], 16);
									text4 = text4.PadLeft(2, '0');
									stringBuilder2.Append(text4);
								}
								text = stringBuilder2.ToString();
								stringBuilder2.Clear();
							}
							if (BitConverter.ToInt32(array3, 0).Equals(252))
							{
								for (int n = k + 5; array2[n] != 10 && n < k + 18; n++)
								{
									stringBuilder.Append((char)array2[n]);
								}
								text2 = stringBuilder.ToString();
								stringBuilder.Clear();
							}
						}
						if (text == null)
						{
							text = "n/a";
						}
						if (text2 == null)
						{
							text2 = Connect_MonitorID;
						}
						if (text2.Contains("Acer "))
						{
							text2 = text2.Replace("Acer ", "");
						}
						else if (text2.Contains("ACER "))
						{
							text2 = text2.Replace("ACER ", "");
						}
						Monitor_SN_num_List.Add(text);
						Monitor_ModelName_List.Add(text2);
					}
				}
			}
			if (text == null)
			{
				text = "n/a";
				Monitor_SN_num_List.Add(text);
			}
			if (text2 == null)
			{
				text2 = Connect_MonitorID;
				Monitor_ModelName_List.Add(text2);
			}
		}

		public static bool AcerSetEDID_OVERRIDE(string Connect_MonitorID, string Connect_DriverID)
		{
			char[] separator = new char[1]
			{
				'\\'
			};
			if (Read_EDID_Block.Length % 128 != 0 && Read_EDID_Block.Length >= 128)
			{
				return false;
			}
			int num = Read_EDID_Block.Length / 128;
			byte[] array = new byte[128]
			{
				0,
				255,
				255,
				255,
				255,
				255,
				255,
				0,
				13,
				175,
				35,
				23,
				0,
				0,
				0,
				0,
				2,
				21,
				1,
				4,
				149,
				38,
				21,
				120,
				2,
				209,
				245,
				147,
				93,
				89,
				144,
				38,
				29,
				80,
				84,
				0,
				0,
				0,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				29,
				54,
				128,
				160,
				112,
				56,
				30,
				64,
				46,
				30,
				36,
				0,
				126,
				215,
				16,
				0,
				0,
				24,
				0,
				0,
				0,
				5,
				0,
				116,
				139,
				128,
				80,
				112,
				56,
				151,
				65,
				8,
				64,
				6,
				0,
				0,
				0,
				0,
				0,
				254,
				0,
				67,
				77,
				73,
				10,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				0,
				0,
				0,
				254,
				0,
				78,
				49,
				55,
				51,
				72,
				72,
				70,
				45,
				69,
				50,
				49,
				32,
				32,
				0,
				57
			};
			byte[] array2 = new byte[128];
			byte[] array3 = new byte[128];
			for (int i = 0; i < 128; i++)
			{
				if (num > 0)
				{
					array[i] = Read_EDID_Block[i];
				}
				if (num > 1)
				{
					array2[i] = Read_EDID_Block[i + 128];
				}
				if (num > 2)
				{
					array3[i] = Read_EDID_Block[i + 128 * (num - 1)];
				}
			}
			RegistryKey registryKey = Registry.LocalMachine;
			bool flag = false;
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum\\DISPLAY", writable: true);
			}
			catch
			{
				registryKey.Close();
				flag = true;
			}
			if (!flag && registryKey != null)
			{
				string[] subKeyNames = registryKey.GetSubKeyNames();
				foreach (string text in subKeyNames)
				{
					if (string.Compare(text, Connect_MonitorID, ignoreCase: false) != 0)
					{
						continue;
					}
					RegistryKey registryKey2 = registryKey.OpenSubKey(text, writable: true);
					if (registryKey2 != null)
					{
						string[] subKeyNames2 = registryKey2.GetSubKeyNames();
						foreach (string name in subKeyNames2)
						{
							RegistryKey registryKey3 = registryKey2.OpenSubKey(name, writable: true);
							if (registryKey3 != null)
							{
								string[] subKeyNames3 = registryKey3.GetSubKeyNames();
								string[] array4 = registryKey3.GetValue("Driver").ToString().Split(separator);
								if (string.Compare(Connect_DriverID, array4[1], ignoreCase: false) == 0 && subKeyNames3.Contains("Device Parameters"))
								{
									RegistryKey registryKey4 = registryKey3.OpenSubKey("Device Parameters", writable: true);
									if (registryKey4 != null)
									{
										byte[] array5 = registryKey4.GetValue("EDID", null) as byte[];
										if (array5 == null)
										{
											return false;
										}
										RegistryKey registryKey5 = registryKey4.CreateSubKey("EDID_OVERRIDE", writable: true);
										if (registryKey5 != null)
										{
											if (num > 0)
											{
												registryKey5.SetValue("0", array5);
											}
											if (num > 1)
											{
												registryKey5.SetValue("1", array2);
											}
											if (num > 2)
											{
												registryKey5.SetValue("2", array3);
											}
											registryKey4.Close();
											registryKey3.Close();
											registryKey2.Close();
											registryKey5.Close();
											return true;
										}
									}
								}
							}
							registryKey3.Close();
						}
					}
					registryKey2.Close();
				}
			}
			return false;
		}

		public static bool AcerDeleteEDID_OVERRIDE(string Connect_MonitorID, string Connect_DriverID)
		{
			char[] separator = new char[1]
			{
				'\\'
			};
			RegistryKey registryKey = Registry.LocalMachine;
			bool flag = false;
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum\\DISPLAY", writable: true);
			}
			catch
			{
				registryKey.Close();
				flag = true;
			}
			if (!flag && registryKey != null)
			{
				string[] subKeyNames = registryKey.GetSubKeyNames();
				foreach (string text in subKeyNames)
				{
					if (string.Compare(text, Connect_MonitorID, ignoreCase: false) != 0)
					{
						continue;
					}
					RegistryKey registryKey2 = registryKey.OpenSubKey(text, writable: true);
					if (registryKey2 != null)
					{
						string[] subKeyNames2 = registryKey2.GetSubKeyNames();
						foreach (string name in subKeyNames2)
						{
							RegistryKey registryKey3 = registryKey2.OpenSubKey(name, writable: true);
							if (registryKey3 != null)
							{
								string[] subKeyNames3 = registryKey3.GetSubKeyNames();
								string[] array = registryKey3.GetValue("Driver").ToString().Split(separator);
								if (string.Compare(Connect_DriverID, array[1], ignoreCase: false) == 0 && subKeyNames3.Contains("Device Parameters"))
								{
									RegistryKey registryKey4 = registryKey3.OpenSubKey("Device Parameters", writable: true);
									if (registryKey4 != null && registryKey4.OpenSubKey("EDID_OVERRIDE", writable: true) != null)
									{
										registryKey4.DeleteSubKeyTree("EDID_OVERRIDE");
										registryKey4.Close();
										registryKey3.Close();
										registryKey2.Close();
										return true;
									}
								}
							}
							registryKey3.Close();
						}
					}
					registryKey2.Close();
				}
			}
			return false;
		}

		public static bool AcerReadEDIDfromFile(string model_name)
		{
			string path = "Overclock\\" + model_name + ".dat";
			try
			{
				using StreamReader streamReader = new StreamReader(path);
				int num = 0;
				int num2 = 0;
				if (streamReader != null)
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						if (num == 0 && string.Compare(text, "04 72", ignoreCase: true) != 0)
						{
							return false;
						}
						if (num == 1)
						{
							string[] array = text.Split(EDID_SPILIT_SPACE);
							Current_Refresh_Rate = Convert.ToByte(array[0], 16) + Convert.ToByte(array[1], 16);
						}
						else if (num == 2)
						{
							string[] array2 = text.Split(EDID_SPILIT_SPACE);
							Overclock_Refresh_Rate = Convert.ToByte(array2[0], 16) + Convert.ToByte(array2[1], 16);
						}
						else if (num > 2)
						{
							string[] array3 = text.Split(EDID_SPILIT_SPACE);
							for (int i = 0; i < array3.Length; i++)
							{
								Read_EDID_Block[num2] = Convert.ToByte(array3[i], 16);
								num2++;
								if (num2 > 384)
								{
									return true;
								}
							}
						}
						num++;
					}
					streamReader.Close();
					if (num2 % 128 != 0)
					{
						return false;
					}
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
			return false;
		}
	}
}
