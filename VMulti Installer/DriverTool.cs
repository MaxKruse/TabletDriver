using System;
using System.Collections.Generic;
using System.Text;

namespace VMulti_Installer
{
    internal static class DriverTool
    {
        /// <summary>
        /// Checks if a device is detected.
        /// </summary>
        /// <param name="deviceName">The device's name</param>
        /// <returns></returns>
        public static bool DeviceConnected(string deviceName)
        {
            IList<string> allDevices = GetAll();
            return allDevices.Contains(deviceName);
        }

        //Name:     GetAll
        //Inputs:   none
        //Outputs:  string array
        //Errors:   This method may throw the following errors.
        //          Failed to enumerate device tree!
        //          Invalid handle!
        //Remarks:  This is code I cobbled together from a number of newsgroup threads
        //          as well as some C++ stuff I translated off of MSDN.  Seems to work.
        //          The idea is to come up with a list of devices, same as the device
        //          manager does.  Currently it uses the actual "system" names for the
        //          hardware.  It is also possible to use hardware IDs.  See the docs
        //          for SetupDiGetDeviceRegistryProperty in the MS SDK for more details.
        public static string[] GetAll()
        {
            List<string> HWList = new List<string>();
            try
            {
                Guid myGUID = Guid.Empty;
                IntPtr hDevInfo = Native.SetupDiGetClassDevs(ref myGUID, 0, IntPtr.Zero, Native.DIGCF_ALLCLASSES | Native.DIGCF_PRESENT);

                if (hDevInfo.ToInt32() == Native.INVALID_HANDLE_VALUE)
                    throw new Exception("Invalid Handle");

                Native.SP_DEVINFO_DATA DeviceInfoData;
                DeviceInfoData = new Native.SP_DEVINFO_DATA();
                DeviceInfoData.cbSize = 28;
                //is devices exist for class
                DeviceInfoData.devInst = 0;
                DeviceInfoData.classGuid = Guid.Empty;
                DeviceInfoData.reserved = 0;
                uint i;
                StringBuilder DeviceName = new StringBuilder("");
                DeviceName.Capacity = Native.MAX_DEV_LEN;
                for (i = 0; Native.SetupDiEnumDeviceInfo(hDevInfo, i, DeviceInfoData); i++)
                {
                    //Declare vars
                    while (!Native.SetupDiGetDeviceRegistryProperty(hDevInfo, DeviceInfoData, Native.SPDRP_DEVICEDESC, 0, DeviceName, Native.MAX_DEV_LEN, IntPtr.Zero))
                    { }
                    HWList.Add(DeviceName.ToString());
                }
                Native.SetupDiDestroyDeviceInfoList(hDevInfo);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to enumerate device tree!", ex);
            }
            return HWList.ToArray();
        }
    }
}
