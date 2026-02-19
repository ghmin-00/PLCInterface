using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCInterface
{
    /// <summary>
    /// PLC 통신 매니저 인터페이스
    /// </summary>
    public interface IPLC
    {
        /// <summary>
        /// PLC 통신 객체 초기화
        /// </summary>
        /// <returns></returns>
        bool Initialize();

        /// <summary>
        /// PLC 통신 포트 연결 상태 확인
        /// </summary>
        /// <returns></returns>
        bool IsOpen();

        /// <summary>
        /// PLC 통신 포트 연결
        /// </summary>
        /// <returns>true: 성공, false: 실패</returns>
        bool Open();

        /// <summary>
        /// PLC 통신 포트 연걸 해제
        /// </summary>
        /// <returns>true: 성공, false: 실패</returns>
        bool Close();

        /// <summary>
        /// PLC 현재 상태 확인
        /// </summary>
        /// <returns>true: 성공, false: 실패</returns>
        bool GetStatus();

        /// <summary>
        /// 단일 디바이스 주소값 읽기
        /// </summary>
        /// <param name="iDevice">읽을 디바이스 주소 이름</param>
        /// <param name="oValue">읽은 디바이스 값</param>
        /// <param name="oErrCode">에러 코드(읽기 성공 실패 일 경우)</param>
        /// <returns>true: 성공, false: 실패</returns>
        bool ReadSingleDevice(string iDevice, out int oValue, out int oErrCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDevices"></param>
        /// <param name="oValues"></param>
        /// <param name="oErrCode"></param>
        /// <returns>true: 성공, false: 실패</returns>
        bool ReadIndividualDevices(string[] iDevices, out int[] oValues, out int oErrCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iStartDevice"></param>
        /// <param name="iDeviceCount"></param>
        /// <param name="oValues"></param>
        /// <param name="oErrCode"></param>
        /// <returns>true: 성공, false: 실패</returns>
        bool ReadContinuousDevices(string iStartDevice, int iDeviceCount, out int[] oValues, out int oErrCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDevice"></param>
        /// <param name="iValue"></param>
        /// <param name="oErrCode"></param>
        /// <returns>true: 성공, false: 실패</returns>
        bool WriteSingleDevice(string iDevice, int iValue, out int oErrCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDevices"></param>
        /// <param name="iValues"></param>
        /// <param name="oErrCode"></param>
        /// <returns>true: 성공, false: 실패</returns>
        bool WriteIndividualDevices(string[] iDevices, int[] iValues, out int oErrCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iStartDevice"></param>
        /// <param name="iDeviceCount"></param>
        /// <param name="iValues"></param>
        /// <param name="oErrCode"></param>
        /// <returns>true: 성공, false: 실패</returns>
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