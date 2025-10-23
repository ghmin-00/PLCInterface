using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Media.Media3D;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace PLCInterface
{
    /// <summary>
    /// 이 클래스는 무조건 상속 받아서 사용
    /// PLC와 기본적으로 통신 하는 기능은 PLCManager 클래스에 구현
    /// 이벤트 함수 또는 별도 기능으로 PLC 와 통신하는 코드는 상속받은 자식 클래스에서 구현
    /// </summary>
    public class PLCManager
    {
        private Timer _timerMonitoring;
        private Timer _timerUpdating;
        private bool _isMonitoringRunning = false;
        private bool _isUpdatingRunning = false;
        public bool IsMonitoringRunning { get { return _isMonitoringRunning; } }
        public bool IsUpdatingRunning { get { return _isUpdatingRunning; } }
        private Dictionary<string, short> _monitoringPairs;
        private Dictionary<string, short> _updatingPairs;


        public event Action MonitoringValueChanged;

        public IPLC PLC { get; set; }
        public PLCAddressMap PLCAddressMap { get; set; }
        public int Interval { get; set; }

        // PLCAddressMap 대체할 객체임
        public PLCDataList PLCDataList { get; set; }

        #region PLCManager 기본 기능
        public PLCManager()
        {
            Interval = 100;
        }

        public void Init()
        {
            if (PLC == null || PLCAddressMap == null) return;

            _monitoringPairs = new Dictionary<string, short>(PLCAddressMap.MonitoringAddressValue);
            _updatingPairs = new Dictionary<string, short>(PLCAddressMap.UpdatingAddressValue);

            _timerMonitoring = new Timer(TimerTickMonitoring, null, 0, Interval);
            _timerUpdating = new Timer(TimerTickUpdating, null, 0, Interval);
        }

        private void TimerTickMonitoring(object state)
        {
            Dictionary<string, short> tempPairs = new Dictionary<string, short>(_monitoringPairs);
            if (_isMonitoringRunning)
            {
                Dictionary<string, short> result = MonitoringSingleTick(tempPairs);
                if (result != null)
                {
                    _monitoringPairs = new Dictionary<string, short>(result);
                    MonitoringValueChanged?.Invoke();
                }
            }
        }

        private void TimerTickUpdating(object state)
        {
            if (_isUpdatingRunning)
            {
                UpdatingSingleTick(_updatingPairs);
            }
        }

        private Dictionary<string, short> MonitoringSingleTick(Dictionary<string, short> iPairs)
        {
            int count = iPairs.Count;
            string[] devices = new string[count];
            int[] values = new int[count];

            int idx = 0;
            foreach (KeyValuePair<string, short> pair in iPairs)
            {
                devices[idx++] = pair.Key;
            }

            if (!PLC.ReadIndividualDevices(devices, out values, out int errRecv))
            {
                MonitoringStop();
                return null;
            }

            idx = 0;
            foreach (int value in values)
            {
                iPairs[devices[idx++]] = (short)value;
            }

            return iPairs;
        }

        private void UpdatingSingleTick(Dictionary<string, short> iPairs)
        {
            int count = iPairs.Count;
            string[] devices = new string[count];
            int[] values = new int[count];

            int idx = 0;
            foreach (KeyValuePair<string, short> pair in iPairs)
            {
                devices[idx] = pair.Key;
                values[idx++] = pair.Value;
            }

            if (!PLC.WriteIndividualDevices(devices, values, out int errRecv))
            {
                UpdatingStop();
                return;
            }
        }

        /// <summary>
        /// 모니터링 디바이스 리스트 세팅
        /// </summary>
        /// <param name="iPairs"></param>
        public void SetMonitoringDeviceList(Dictionary<string, short> iPairs)
        {
            _monitoringPairs = iPairs;
        }

        /// <summary>
        /// 업데이트 디바이스 리스트 세팅
        /// </summary>
        /// <param name="iPairs"></param>
        public void SetUpdateDeviceList(Dictionary<string, short> iPairs)
        {
            _updatingPairs = iPairs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsRunning()
        {
            return _isMonitoringRunning || _isUpdatingRunning;
        }

        public int isRunning()
        {
            if (!_isMonitoringRunning && !_isUpdatingRunning)
            {
                return 0; // 둘다 실행 중이 아닐 때
            }
            else if (_isMonitoringRunning && !_isUpdatingRunning)
            {
                return 1; // 모니터링만 실행 중 일 때
            }
            else if (!_isMonitoringRunning && _isUpdatingRunning)
            {
                return 2; // 업데이트만 실행 중 일 때
            }
            else
            {
                return 3; // 둘다 실행 중 일 때
            }
        }

        public void MonitoringStart()
        {
            _isMonitoringRunning = true;
        }

        public void MonitoringStop()
        {
            _isMonitoringRunning = false;
        }

        public void UpdatingStart()
        {
            _isUpdatingRunning = true;
        }

        public void UpdatingStop()
        {
            _isUpdatingRunning = false;
        }

        public int[] GetMonitoringData(ReadData[] names, out int[] values, out int errCode)
        {
            values = new int[names.Length];
            errCode = 0;
            string[] devices = new string[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                devices[i] = PLCAddressMap.MonitoringNameAddress[names[i]];
                values[i] = _monitoringPairs[devices[i]];
            }

            return values;
        }

        public int GetSingleMonitoringData(ReadData name)
        {
            return _monitoringPairs[PLCAddressMap.MonitoringNameAddress[name]];
        }

        public int SetUpdatingData(WriteData[] names, short[] values)
        {
            for (int i = 0; i < names.Length; i++)
            {
                _updatingPairs[PLCAddressMap.UpdatingNameAddress[names[i]]] = values[i];
            }
            return _updatingPairs[PLCAddressMap.UpdatingNameAddress[names[0]]];
        }

        public int SetSingleUpdatingData(WriteData name, short value)
        {
            _updatingPairs[PLCAddressMap.UpdatingNameAddress[name]] = value;
            return _updatingPairs[PLCAddressMap.UpdatingNameAddress[name]];
        }

        public int GetSingleUpdatingData(WriteData name)
        {
            return _updatingPairs[PLCAddressMap.UpdatingNameAddress[name]];
        }
        #endregion
    }
}
