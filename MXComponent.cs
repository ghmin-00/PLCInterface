using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace PLCInterface
{
    /*
    MX Component 기본 함수 설명
    기본형과(GetDevice) 2가 붙은 형태(GetDevice2)가 있음
    GetDevice  : int   자료형 결과 리턴 (32 bits, 2 words)
    GetDevice2 : short 자료형 결과 리턴 (16 bits, 1 word)
     */

    public class MXComponent : IPLC
    {
        //private ActUtlTypeLib.ActUtlType _actUtlType;
        private ActUtlType64Lib.ActUtlType64 _actUtlType;
        private int _stationNumber { get; set; }
        public Dictionary<int, string> error;
        private bool _connected = false;

        public MXComponent(int iLogicalStationNumber)
        {
            _stationNumber = iLogicalStationNumber;

            _actUtlType = new ActUtlType64Lib.ActUtlType64();
            _actUtlType.ActLogicalStationNumber = _stationNumber;

            InitError();
        }

        private void InitError()
        {
            error = new Dictionary<int, string>();
            error[0x00000000] = "정상 종료";
            error[0x01010002] = "타임아웃에러";
        }

        public bool IsOpen()
        {
            // 상시 ON/OFF 특수 릴레이 값을 읽어서 PLC 연결상태 확인
            string[] devices = new string[2];
            devices[0] = "SM400"; // 상시 ON  특수 릴레이
            devices[1] = "SM401"; // 상시 OFF 특수 릴레이
            int[] values = new int[2];
            int errCode = 0;

            if (!_connected)
                return false;

            bool result = ReadIndividualDevices(devices, out values, out errCode);

            if (!result)
            {
                this.Close();
                return false;
            }

            if (values[0] == 1 && values[1] == 0)
                return true;
            else
            {
                this.Close();
                return false;
            }
        }

        public bool Open()
        {
            int result = _actUtlType.Open();
            if (result == 0)
            {
                _connected = true;
                return true;
            }
            return false;
        }

        public bool Close()
        {
            int result = _actUtlType.Close();
            if (result == 0)
            {
                _connected = false;
                return true;
            }
            return false;
        }

        public bool GetStatus()
        {
            return true;
        }

        public bool ReadSingleDevice(string iDevice, out int oValue, out int oErrCode)
        {
            oValue = 0;
            oErrCode = _actUtlType.GetDevice(iDevice, out oValue);
            try
            {
            }
            catch
            {
                oErrCode = -1;
            }
            if (oErrCode == 0) return true;
            return false;
        }

        public bool ReadIndividualDevices(string[] iDevices, out int[] oValues, out int oErrCode)
        {
            oValues = new int[iDevices.Length];
            Array.Clear(oValues, 0, oValues.Length);
            string iDeviceList = iDevices[0];
            for (int i = 1; i < iDevices.Length; i++)
            {
                iDeviceList = iDeviceList + "\n" + iDevices[i];
            }
            try
            {
                oErrCode = _actUtlType.ReadDeviceRandom(iDeviceList, iDevices.Length, out oValues[0]);
            }
            catch 
            {
                oErrCode = -1;
            }
            if (oErrCode == 0) return true;
            return false;
        }

        public bool ReadContinuousDevices(string iStartDevice, int iDeviceCount, out int[] oValues, out int oErrCode)
        {
            oValues = new int[iDeviceCount];
            Array.Clear(oValues, 0, iDeviceCount);
            try
            {
                oErrCode = _actUtlType.ReadDeviceBlock(iStartDevice, iDeviceCount, out oValues[0]);
            }
            catch
            {
                oErrCode = -1;
            }
            if (oErrCode == 0) return true;
            return false;
        }

        public bool WriteSingleDevice(string iDevice, int iValue, out int oErrCode)
        {
            try
            {
                oErrCode = _actUtlType.SetDevice(iDevice, iValue);
            }
            catch
            {
                oErrCode = -1;
            }
            if (oErrCode == 0) return true;
            return false;
        }

        public bool WriteIndividualDevices(string[] iDevices, int[] iValues, out int oErrCode)
        {
            string iDeviceList = iDevices[0];
            for (int i = 1; i < iDevices.Length; i++)
            {
                iDeviceList = iDeviceList + "\n" + iDevices[i];
            }
            try
            {
                oErrCode = _actUtlType.WriteDeviceRandom(iDeviceList, iDevices.Length, ref iValues[0]);
            }
            catch
            {
                oErrCode = -1;
            }
            if (oErrCode == 0) return true;
            return false;
        }

        public bool WriteContinuousDevices(string iStartDevice, int iDeviceCount, int[] iValues, out int oErrCode)
        {
            try
            {
                oErrCode = _actUtlType.WriteDeviceBlock(iStartDevice, iDeviceCount, ref iValues[0]);
            }
            catch
            {
                oErrCode = -1;
            }
            if (oErrCode == 0) return true;
            return false;
        }

        public bool SendPulseSignal(string iDeviceName, int iInterval, out int oErrCode)
        {
            oErrCode = 0;
            if (!WriteSingleDevice(iDeviceName, 1, out oErrCode))
                return false;
            Thread.Sleep(iInterval);
            if (!WriteSingleDevice(iDeviceName, 0, out oErrCode))
                return false;
            return true;
        }
    }
}
