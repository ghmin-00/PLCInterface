using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCInterface
{
    /// <summary>
    /// PLC ��� �Ŵ��� �������̽�
    /// </summary>
    public interface IPLC
    {
        /// <summary>
        /// PLC ��� ��Ʈ ���� ���� Ȯ��
        /// </summary>
        /// <returns></returns>
        bool IsOpen();

        /// <summary>
        /// PLC ��� ��Ʈ ����
        /// </summary>
        /// <returns>true: ����, false: ����</returns>
        bool Open();

        /// <summary>
        /// PLC ��� ��Ʈ ���� ����
        /// </summary>
        /// <returns>true: ����, false: ����</returns>
        bool Close();

        /// <summary>
        /// PLC ���� ���� Ȯ��
        /// </summary>
        /// <returns>true: ����, false: ����</returns>
        bool GetStatus();

        /// <summary>
        /// ���� ����̽� �ּҰ� �б�
        /// </summary>
        /// <param name="iDevice">���� ����̽� �ּ� �̸�</param>
        /// <param name="oValue">���� ����̽� ��</param>
        /// <param name="oErrCode">���� �ڵ�(�б� ���� ���� �� ���)</param>
        /// <returns>true: ����, false: ����</returns>
        bool ReadSingleDevice(string iDevice, out int oValue, out int oErrCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDevices"></param>
        /// <param name="oValues"></param>
        /// <param name="oErrCode"></param>
        /// <returns>true: ����, false: ����</returns>
        bool ReadIndividualDevices(string[] iDevices, out int[] oValues, out int oErrCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iStartDevice"></param>
        /// <param name="iDeviceCount"></param>
        /// <param name="oValues"></param>
        /// <param name="oErrCode"></param>
        /// <returns>true: ����, false: ����</returns>
        bool ReadContinuousDevices(string iStartDevice, int iDeviceCount, out int[] oValues, out int oErrCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDevice"></param>
        /// <param name="iValue"></param>
        /// <param name="oErrCode"></param>
        /// <returns>true: ����, false: ����</returns>
        bool WriteSingleDevice(string iDevice, int iValue, out int oErrCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDevices"></param>
        /// <param name="iValues"></param>
        /// <param name="oErrCode"></param>
        /// <returns>true: ����, false: ����</returns>
        bool WriteIndividualDevices(string[] iDevices, int[] iValues, out int oErrCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iStartDevice"></param>
        /// <param name="iDeviceCount"></param>
        /// <param name="iValues"></param>
        /// <param name="oErrCode"></param>
        /// <returns>true: ����, false: ����</returns>
        bool WriteContinuousDevices(string iStartDevice, int iDeviceCount, int[] iValues, out int oErrCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDeviceName"></param>
        /// <param name="iInterval"></param>
        /// <param name="oErrCode"></param>
        /// <returns></returns>
        bool SendPulseSignal(string iDeviceName, int iInterval, out int oErrCode);
    }
}