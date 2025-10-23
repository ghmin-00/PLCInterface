using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCInterface
{
    /// <summary>
    /// HiCurve <-> PC 간 지속적으로 주고받는 데이터 정리
    /// </summary>
    public class PLCAddressMap
    {
        /// <summary>
        /// PLC 모니터링 값 변수 이름 - 주소 쌍 Dictionary
        /// </summary>        
        public Dictionary<ReadData, string> MonitoringNameAddress { get; set; }
        public Dictionary<WriteData, string> UpdatingNameAddress { get; set; }

        /// <summary>
        /// PLC 모니터링 값 주소 - 값 쌍 Dictionary
        /// </summary>
        public Dictionary<string, short> MonitoringAddressValue { get; set; }
        public Dictionary<string, short> UpdatingAddressValue { get; set; }

        public PLCAddressMap()
        {
            Init();
        }

        private void Init()
        {
            MonitoringNameAddress = new Dictionary<ReadData, string>();
            UpdatingNameAddress = new Dictionary<WriteData, string>();
            MonitoringAddressValue = new Dictionary<string, short>();
            UpdatingAddressValue = new Dictionary<string, short>();

            // PLC 통신 상태
            MonitoringNameAddress[ReadData.bComOK] = "SM400";

            // 안전 관련 상태 --> 00_MAIN 프로그램에 연결됨
            MonitoringNameAddress[ReadData.bDoorOpen] = "D7000.0";
            MonitoringNameAddress[ReadData.bShockSensor] = "D7000.1";
            MonitoringNameAddress[ReadData.bRailAxisLimitL] = "D7000.2";
            MonitoringNameAddress[ReadData.bRailAxisLimitH] = "D7000.3";
            MonitoringNameAddress[ReadData.bRailAxisOriginPin] = "D7000.4";

            // 시스템 상태 --> 00_MAIN 프로그램에 연결됨
            MonitoringNameAddress[ReadData.bSystemOK] = "D7010.0";
            MonitoringNameAddress[ReadData.bSystemRun] = "D7010.1";
            MonitoringNameAddress[ReadData.bSystemEmergencyStop] = "D7010.2";
            MonitoringNameAddress[ReadData.bHeaterOK] = "D7010.3";
            MonitoringNameAddress[ReadData.bHeaterRun] = "D7010.4";
            MonitoringNameAddress[ReadData.bChillerOK] = "D7010.5";
            MonitoringNameAddress[ReadData.bChillerRun] = "D7010.6";
            MonitoringNameAddress[ReadData.bRobotProgramError] = "D7010.E";
            MonitoringNameAddress[ReadData.bSysOverallError] = "D7010.F";
            MonitoringNameAddress[ReadData.wRobotErrorCode] = "D7011";
            MonitoringNameAddress[ReadData.wRobotErrorSubInfo] = "D7012";
            MonitoringNameAddress[ReadData.wOverallErrorCode] = "D7013";

            // 로봇 상태 --> 10_ROBOT 프로그램에 연결됨
            MonitoringNameAddress[ReadData.bRobotModeManual] = "D7020.0";
            MonitoringNameAddress[ReadData.bRobotModeAuto] = "D7020.1";
            MonitoringNameAddress[ReadData.bRobotModeRemote] = "D7020.2";
            MonitoringNameAddress[ReadData.bRobotMotorOn] = "D7020.8";
            MonitoringNameAddress[ReadData.bRobotReadyOK] = "D7020.9";//X804
            MonitoringNameAddress[ReadData.bRobotRunning] = "D7020.A";
            MonitoringNameAddress[ReadData.bRobotMoving] = "D7020.B";
            MonitoringNameAddress[ReadData.bRobotStopHold] = "D7020.C";
            MonitoringNameAddress[ReadData.bRobotEmergencyStop] = "D7020.D";
            MonitoringNameAddress[ReadData.bRobotProgramEnd] = "D7020.E";
            MonitoringNameAddress[ReadData.bRobotOverallError] = "D7020.F";
            MonitoringNameAddress[ReadData.wRobotToolNum] = "D7021";
            MonitoringNameAddress[ReadData.wRobotStatus] = "D7022";
            MonitoringNameAddress[ReadData.wRobotSpeed] = "D7023";
            MonitoringNameAddress[ReadData.dLRobotMotorOnDay] = "D7024";
            MonitoringNameAddress[ReadData.dHRobotMotorOnDay] = "D7025";
            MonitoringNameAddress[ReadData.dLRobotMotorOnMs] = "D7026";
            MonitoringNameAddress[ReadData.dHRobotMotorOnMs] = "D7027";
            MonitoringNameAddress[ReadData.dLRobotRunningTimeOnDay] = "D7028";
            MonitoringNameAddress[ReadData.dHRobotRunningTimeOnDay] = "D7029";
            MonitoringNameAddress[ReadData.dLRobotRunningTimeOnMs] = "D7030";
            MonitoringNameAddress[ReadData.dHRobotRunningTimeOnMs] = "D7031";

            // 로봇 작업 상태 --> 10_ROBOT 프로그램에 연결됨
            MonitoringNameAddress[ReadData.bRobotWait] = "D7039.0";
            MonitoringNameAddress[ReadData.bRobotMoveInstPose] = "D7039.1";
            MonitoringNameAddress[ReadData.bRobotInstrumentation] = "D7039.2";
            MonitoringNameAddress[ReadData.bRobotUpdateHeatingLines] = "D7039.3";
            MonitoringNameAddress[ReadData.bRobotMoveHeatingPose] = "D7039.4";
            MonitoringNameAddress[ReadData.bRobotHeating] = "D7039.5";
            MonitoringNameAddress[ReadData.bRobotMoveHomePose] = "D7039.6";
            MonitoringNameAddress[ReadData.bRobotJobFinished] = "D7039.7";
            MonitoringNameAddress[ReadData.bRobotCaptureRequest] = "D7039.8";//X82B
            MonitoringNameAddress[ReadData.bRobotInstFinished] = "D7039.9";//D7039.9 or X81F
            MonitoringNameAddress[ReadData.bRobotUpdateRequest] = "D7039.A";


            // 가열선 정보 (작업 중) --> 10_ROBOT 프로그램에 연결됨
            MonitoringNameAddress[ReadData.wOverallHeatingLinesNumber] = "D7040";
            MonitoringNameAddress[ReadData.wCurrentHeatingLineNumber] = "D7041";
            MonitoringNameAddress[ReadData.wHeatingType] = "D7042";
            MonitoringNameAddress[ReadData.wHeatingPower] = "D7043";
            MonitoringNameAddress[ReadData.dLHeatingWidth] = "D7044";
            MonitoringNameAddress[ReadData.dHHeatingWidth] = "D7045";
            MonitoringNameAddress[ReadData.dLHeatingSpeed] = "D7046";
            MonitoringNameAddress[ReadData.dHHeatingSpeed] = "D7047";
            MonitoringNameAddress[ReadData.dLStartPointX] = "D7048";
            MonitoringNameAddress[ReadData.dHStartPointX] = "D7049";
            MonitoringNameAddress[ReadData.dLStartPointY] = "D7050";
            MonitoringNameAddress[ReadData.dHStartPointY] = "D7051";
            MonitoringNameAddress[ReadData.dLStartPointZ] = "D7052";
            MonitoringNameAddress[ReadData.dHStartPointZ] = "D7053";
            MonitoringNameAddress[ReadData.dLEndPointX] = "D7054";
            MonitoringNameAddress[ReadData.dHEndPointX] = "D7055";
            MonitoringNameAddress[ReadData.dLEndPointY] = "D7056";
            MonitoringNameAddress[ReadData.dHEndPointY] = "D7057";
            MonitoringNameAddress[ReadData.dLEndPointZ] = "D7058";
            MonitoringNameAddress[ReadData.dHEndPointZ] = "D7059";

            // 로봇 자세 --> 10_ROBOT 프로그램에 연결됨
            MonitoringNameAddress[ReadData.dLRobotPoseX] = "D7060"; //D7060
            MonitoringNameAddress[ReadData.dHRobotPoseX] = "D7061";
            MonitoringNameAddress[ReadData.dLRobotPoseY] = "D7062";
            MonitoringNameAddress[ReadData.dHRobotPoseY] = "D7063";
            MonitoringNameAddress[ReadData.dLRobotPoseZ] = "D7064";
            MonitoringNameAddress[ReadData.dHRobotPoseZ] = "D7065";
            MonitoringNameAddress[ReadData.dLRobotPoseRX] = "D7066";
            MonitoringNameAddress[ReadData.dHRobotPoseRX] = "D7067";
            MonitoringNameAddress[ReadData.dLRobotPoseRY] = "D7068";
            MonitoringNameAddress[ReadData.dHRobotPoseRY] = "D7069";
            MonitoringNameAddress[ReadData.dLRobotPoseRZ] = "D7070";
            MonitoringNameAddress[ReadData.dHRobotPoseRZ] = "D7071";
            MonitoringNameAddress[ReadData.dLRobotPoseA1] = "D7072";
            MonitoringNameAddress[ReadData.dHRobotPoseA1] = "D7073";
            MonitoringNameAddress[ReadData.dLRobotPoseS] = "D7074";
            MonitoringNameAddress[ReadData.dHRobotPoseS] = "D7075";
            MonitoringNameAddress[ReadData.dLRobotPoseH] = "D7076";
            MonitoringNameAddress[ReadData.dHRobotPoseH] = "D7077";
            MonitoringNameAddress[ReadData.dLRobotPoseV] = "D7078";
            MonitoringNameAddress[ReadData.dHRobotPoseV] = "D7079";
            MonitoringNameAddress[ReadData.dLRobotPoseR2] = "D7080";
            MonitoringNameAddress[ReadData.dHRobotPoseR2] = "D7081";
            MonitoringNameAddress[ReadData.dLRobotPoseB] = "D7082";
            MonitoringNameAddress[ReadData.dHRobotPoseB] = "D7083";
            MonitoringNameAddress[ReadData.dLRobotPoseR1] = "D7084";
            MonitoringNameAddress[ReadData.dHRobotPoseR1] = "D7085";

            MonitoringNameAddress[ReadData.wRobotProgramNumber] = "D7090";
            MonitoringNameAddress[ReadData.wRobotStepNumber] = "D7091";
            MonitoringNameAddress[ReadData.wRobotFunctionNumber] = "D7092";
            MonitoringNameAddress[ReadData.wRobotMainNumber] = "D7093";

            // 카메라 조작 상태
            //dictRecvNameAddress["bPanoramaMode1"] = "D7100.0";
            //dictRecvNameAddress["bPanoramaMode2"] = "D7100.1";
            //dictRecvNameAddress["bPanoramaMode3"] = "D7100.2";
            //dictRecvNameAddress["bPTZMode1"] = "D7100.3";
            //dictRecvNameAddress["bPTZMode2"] = "D7100.4";
            //dictRecvNameAddress["bPTZMode1"] = "D7100.5";
            //dictRecvNameAddress["bPTZZoomIn"] = "D7100.6";
            //dictRecvNameAddress["bPTZZoomOut"] = "D7100.7";
            //dictRecvNameAddress["bPTZHome"] = "D7100.8";

            //한휘영 수정
            MonitoringNameAddress[ReadData.bPanoramaMode] = "D7201";
            MonitoringNameAddress[ReadData.bPTZMode] = "D7211";
            MonitoringNameAddress[ReadData.bPTZZoom] = "D7212";
            MonitoringNameAddress[ReadData.bPTZHome] = "D7213";


            // 센서 상태
            MonitoringNameAddress[ReadData.bLJX8300Sensor1Ready] = "D7101.0";
            MonitoringNameAddress[ReadData.bLJX8300Sensor2Ready] = "D7101.1";
            MonitoringNameAddress[ReadData.bIL600Sensor1Ready] = "D7101.2";
            MonitoringNameAddress[ReadData.bIL600Sensor2Ready] = "D7101.3";
            MonitoringNameAddress[ReadData.bIL600Sensor3Ready] = "D7101.4";
            MonitoringNameAddress[ReadData.bIL600Sensor4Ready] = "D7101.5";
            MonitoringNameAddress[ReadData.bIL600Sensor5Ready] = "D7101.6";
            MonitoringNameAddress[ReadData.bIL300Sensor1Ready] = "D7101.7";
            MonitoringNameAddress[ReadData.bIL300Sensor2Ready] = "D7101.8";
            MonitoringNameAddress[ReadData.bThermalSensorReady] = "D7101.9";

            // 센서 값
            MonitoringNameAddress[ReadData.dLLJX8300P1Height] = "D7110";
            MonitoringNameAddress[ReadData.dHLJX8300P1Height] = "D7111";
            MonitoringNameAddress[ReadData.dLLJX8300P1Angle] = "D7112";
            MonitoringNameAddress[ReadData.dHLJX8300P1Angle] = "D7113";

            MonitoringNameAddress[ReadData.dLLJX8300P2Height] = "D7114";
            MonitoringNameAddress[ReadData.dHLJX8300P2Height] = "D7115";
            MonitoringNameAddress[ReadData.dLLJX8300P2Angle] = "D7116";
            MonitoringNameAddress[ReadData.dHLJX8300P2Angle] = "D7117";

            MonitoringNameAddress[ReadData.dLLJX8300P3Height] = "D7118";
            MonitoringNameAddress[ReadData.dHLJX8300P3Height] = "D7119";
            MonitoringNameAddress[ReadData.dLLJX8300P3Angle] = "D7120";
            MonitoringNameAddress[ReadData.dHLJX8300P3Angle] = "D7121";

            MonitoringNameAddress[ReadData.dLLJX8300P4Height] = "D7122";
            MonitoringNameAddress[ReadData.dHLJX8300P4Height] = "D7123";
            MonitoringNameAddress[ReadData.dLLJX8300P4Angle] = "D7124";
            MonitoringNameAddress[ReadData.dHLJX8300P4Angle] = "D7125";

            MonitoringNameAddress[ReadData.dLLJX8300P5Height] = "D7126";
            MonitoringNameAddress[ReadData.dHLJX8300P5Height] = "D7127";
            MonitoringNameAddress[ReadData.dLLJX8300P5Angle] = "D7128";
            MonitoringNameAddress[ReadData.dHLJX8300P5Angle] = "D7129";

            MonitoringNameAddress[ReadData.dLLJX8300P6Height] = "D7130";
            MonitoringNameAddress[ReadData.dHLJX8300P6Height] = "D7131";
            MonitoringNameAddress[ReadData.dLLJX8300P6Angle] = "D7132";
            MonitoringNameAddress[ReadData.dHLJX8300P6Angle] = "D7133";

            MonitoringNameAddress[ReadData.dLLJX8300P7Height] = "D7134";
            MonitoringNameAddress[ReadData.dHLJX8300P7Height] = "D7135";
            MonitoringNameAddress[ReadData.dLLJX8300P7Angle] = "D7136";
            MonitoringNameAddress[ReadData.dHLJX8300P7Angle] = "D7137";

            MonitoringNameAddress[ReadData.dLLJX8300P8Height] = "D7138";
            MonitoringNameAddress[ReadData.dHLJX8300P8Height] = "D7139";
            MonitoringNameAddress[ReadData.dLLJX8300P8Angle] = "D7140";
            MonitoringNameAddress[ReadData.dHLJX8300P8Angle] = "D7141";

            MonitoringNameAddress[ReadData.dLLJX8300P9Height] = "D7142";
            MonitoringNameAddress[ReadData.dHLJX8300P9Height] = "D7143";
            MonitoringNameAddress[ReadData.dLLJX8300P9Angle] = "D7144";
            MonitoringNameAddress[ReadData.dHLJX8300P9Angle] = "D7145";

            MonitoringNameAddress[ReadData.dLLJX8300P10Height] = "D7146";
            MonitoringNameAddress[ReadData.dHLJX8300P10Height] = "D7147";
            MonitoringNameAddress[ReadData.dLLJX8300P10Angle] = "D7148";
            MonitoringNameAddress[ReadData.dHLJX8300P10Angle] = "D7149";

            MonitoringNameAddress[ReadData.dLIL600AHeight] = "D7170"; // D7170
            MonitoringNameAddress[ReadData.dHIL600AHeight] = "D7171";
            MonitoringNameAddress[ReadData.dLIL600BHeight] = "D7172";
            MonitoringNameAddress[ReadData.dHIL600BHeight] = "D7173";
            MonitoringNameAddress[ReadData.dLIL600CHeight] = "D7174";
            MonitoringNameAddress[ReadData.dHIL600CHeight] = "D7175";
            MonitoringNameAddress[ReadData.dLIL600DHeight] = "D7176";
            MonitoringNameAddress[ReadData.dHIL600DHeight] = "D7177";
            MonitoringNameAddress[ReadData.dLIL600EHeight] = "D7178";
            MonitoringNameAddress[ReadData.dHIL600EHeight] = "D7179";

            MonitoringNameAddress[ReadData.dLIL300AHeight] = "D7180";
            MonitoringNameAddress[ReadData.dHIL300AHeight] = "D7181";
            MonitoringNameAddress[ReadData.dLIL300BHeight] = "D7182";
            MonitoringNameAddress[ReadData.dHIL300BHeight] = "D7183";

            MonitoringNameAddress[ReadData.dLTemperature] = "D7184";
            MonitoringNameAddress[ReadData.dHTemperature] = "D7185";



            // 모든 주소 값에 빈 값 넣기
            foreach (var value in MonitoringNameAddress)
            {
                MonitoringAddressValue[value.Value] = 0;
            }

            // 쓰기 주소값 정리
            // 카메라 3종 상태
            UpdatingNameAddress[WriteData.bPanoramaCameraReady] = "D7500.0";
            UpdatingNameAddress[WriteData.bPTZCameraReady] = "D7500.1";
            UpdatingNameAddress[WriteData.bMotionCameraReady] = "D7500.2";

            // 작업 요청 신호
            UpdatingNameAddress[WriteData.bInstrumentationStart] = "D7510.0";
            UpdatingNameAddress[WriteData.bMotionCamCaptureFinished] = "D7510.1";
            UpdatingNameAddress[WriteData.bHeatingLineUpdating] = "D7510.2";
            UpdatingNameAddress[WriteData.bHeatingLineUpdateStart] = "D7510.3";
            UpdatingNameAddress[WriteData.bInstrumentationType] = "D7510.4";// 수동가열: 0, 자동가열: 1
            UpdatingNameAddress[WriteData.bHeatingLineUpdated] = "D7510.5";//개별 가열선 업데이트 완료 신호
            UpdatingNameAddress[WriteData.bHeatingStart] = "D7510.6";//가열 시작
            UpdatingNameAddress[WriteData.bHeatingType] = "D7510.7";//가열 타입

            // 가열선 정보 업데이트 영역
            UpdatingNameAddress[WriteData.wOverallHeatingLinesNumber] = "D7550";
            UpdatingNameAddress[WriteData.wCurrentHeatingLineNumber] = "D7551";
            UpdatingNameAddress[WriteData.wHeatingType] = "D7552";
            UpdatingNameAddress[WriteData.wHeatingPower] = "D7553";
            UpdatingNameAddress[WriteData.dLHeatingWidth] = "D7554";
            UpdatingNameAddress[WriteData.dHHeatingWidth] = "D7555";
            UpdatingNameAddress[WriteData.dLHeatingSpeed] = "D7556";
            UpdatingNameAddress[WriteData.dHHeatingSpeed] = "D7557";
            UpdatingNameAddress[WriteData.dLStartPointX] = "D7558";
            UpdatingNameAddress[WriteData.dHStartPointX] = "D7559";
            UpdatingNameAddress[WriteData.dLStartPointY] = "D7560";
            UpdatingNameAddress[WriteData.dHStartPointY] = "D7561";
            UpdatingNameAddress[WriteData.dLStartPointZ] = "D7562";
            UpdatingNameAddress[WriteData.dHStartPointZ] = "D7563";
            UpdatingNameAddress[WriteData.dLEndPointX] = "D7564";
            UpdatingNameAddress[WriteData.dHEndPointX] = "D7565";
            UpdatingNameAddress[WriteData.dLEndPointY] = "D7566";
            UpdatingNameAddress[WriteData.dHEndPointY] = "D7567";
            UpdatingNameAddress[WriteData.dLEndPointZ] = "D7568";
            UpdatingNameAddress[WriteData.dHEndPointZ] = "D7569";
            UpdatingNameAddress[WriteData.dLTriHeatingHorizontalAngle] = "D7570";
            UpdatingNameAddress[WriteData.dHTriHeatingHorizontalAngle] = "D7571";

            // 이미지 코너 데이터 전달
            UpdatingNameAddress[WriteData.dLImageCorner1X] = "D7600";
            UpdatingNameAddress[WriteData.dHImageCorner1X] = "D7601";
            UpdatingNameAddress[WriteData.dLImageCorner1Y] = "D7602";
            UpdatingNameAddress[WriteData.dHImageCorner1Y] = "D7603";
            UpdatingNameAddress[WriteData.dLImageCorner2X] = "D7604";
            UpdatingNameAddress[WriteData.dHImageCorner2X] = "D7605";
            UpdatingNameAddress[WriteData.dLImageCorner2Y] = "D7606";
            UpdatingNameAddress[WriteData.dHImageCorner2Y] = "D7607";
            UpdatingNameAddress[WriteData.dLImageCorner3X] = "D7608";
            UpdatingNameAddress[WriteData.dHImageCorner3X] = "D7609";
            UpdatingNameAddress[WriteData.dLImageCorner3Y] = "D7610";
            UpdatingNameAddress[WriteData.dHImageCorner3Y] = "D7611";
            UpdatingNameAddress[WriteData.dLImageCorner4X] = "D7612";
            UpdatingNameAddress[WriteData.dHImageCorner4X] = "D7613";
            UpdatingNameAddress[WriteData.dLImageCorner4Y] = "D7614";
            UpdatingNameAddress[WriteData.dHImageCorner4Y] = "D7615";

            // 계측곡 코너 데이터 전달
            UpdatingNameAddress[WriteData.dLInstrCorner1X] = "D7620";
            UpdatingNameAddress[WriteData.dHInstrCorner1X] = "D7621";
            UpdatingNameAddress[WriteData.dLInstrCorner1Y] = "D7622";
            UpdatingNameAddress[WriteData.dHInstrCorner1Y] = "D7623";
            UpdatingNameAddress[WriteData.dLInstrCorner2X] = "D7624";
            UpdatingNameAddress[WriteData.dHInstrCorner2X] = "D7625";
            UpdatingNameAddress[WriteData.dLInstrCorner2Y] = "D7626";
            UpdatingNameAddress[WriteData.dHInstrCorner2Y] = "D7627";
            UpdatingNameAddress[WriteData.dLInstrCorner3X] = "D7628";
            UpdatingNameAddress[WriteData.dHInstrCorner3X] = "D7629";
            UpdatingNameAddress[WriteData.dLInstrCorner3Y] = "D7630";
            UpdatingNameAddress[WriteData.dHInstrCorner3Y] = "D7631";
            UpdatingNameAddress[WriteData.dLInstrCorner4X] = "D7632";
            UpdatingNameAddress[WriteData.dHInstrCorner4X] = "D7633";
            UpdatingNameAddress[WriteData.dLInstrCorner4Y] = "D7634";
            UpdatingNameAddress[WriteData.dHInstrCorner4Y] = "D7635";

            // 모든 주소 값에 빈 값 넣기
            foreach (var value in UpdatingNameAddress)
            {
                UpdatingAddressValue[value.Value] = 0;
            }
        }

        public Int32 GetValueFromName(ReadData name)
        {
            return MonitoringAddressValue[MonitoringNameAddress[name]];
        }

        public ReadData GetNameFromAddress(string address)
        {
            foreach (var pair in MonitoringNameAddress)
            {
                if (pair.Value == address)
                    return pair.Key;
            }
            return ReadData.Unknown;
        }
    }

    public enum ReadData
    {
        Unknown,
        // PLC 통신 상태
        /// <summary>
        /// PLC 통신 상태
        /// <para>PLC Address: SM400</para>
        /// </summary>
        bComOK,

        // 안전 관련 상태
        /// <summary>
        /// 안전문 열림 신호
        /// <para>PLC Address: D7000.0</para>
        /// </summary>
        bDoorOpen,
        /// <summary>
        /// 충돌 센서 신호
        /// <para>PLC Address: D7000.1</para>
        /// </summary>
        bShockSensor,
        /// <summary>
        /// 레일 하한 리밋 센서 신호
        /// <para>PLC Address: D7000.2</para>
        /// </summary>
        bRailAxisLimitL,
        /// <summary>
        /// 레일 상한 리밋 센서 신호
        /// <para>PLC Address: D7000.3</para>
        /// </summary>
        bRailAxisLimitH,
        /// <summary>
        /// 레일 원점핀 센서 신호
        /// <para>PLC Address: D7000.4</para>
        /// </summary>
        bRailAxisOriginPin,

        // 시스템 상태
        /// <summary>
        /// 시스템 상태
        /// <para>PLC Address: D7010.0</para>
        /// </summary>
        bSystemOK,
        /// <summary>
        /// 시스템 동작 중
        /// <para>PLC Address: D7010.1</para>
        /// </summary>
        bSystemRun,
        /// <summary>
        /// 시스템 비상 정지
        /// <para>PLC Address: D7010.2</para>
        /// </summary>
        bSystemEmergencyStop,
        /// <summary>
        /// 가열기 전원 상태
        /// <para>PLC Address: D7010.3</para>
        /// </summary>
        bHeaterOK,
        /// <summary>
        /// 가열기 동작 중
        /// <para>PLC Address: D7010.4</para>
        /// </summary>
        bHeaterRun,
        /// <summary>
        /// 냉각기 전원 상태
        /// <para>PLC Address: D7010.5</para>
        /// </summary>
        bChillerOK,
        /// <summary>
        /// 냉각기 동작 중
        /// <para>PLC Address: D7010.6</para>
        /// </summary>
        bChillerRun,
        /// <summary>
        /// 로봇 프로그램 이상
        /// <para>PLC Address: D7010.E</para>
        /// </summary>
        bRobotProgramError,
        /// <summary>
        /// 시스템 종합 이상
        /// <para>PLC Address: D7010.F</para>
        /// </summary>
        bSysOverallError,

        /// <summary>
        /// 로봇 에러 코드
        /// <para>PLC Address: D7011</para>
        /// </summary>
        wRobotErrorCode,
        /// <summary>
        /// 로봇 에러 보조 정보
        /// <para>PLC Address: D7012</para>
        /// </summary>
        wRobotErrorSubInfo,
        /// <summary>
        /// 시스템 종합 에러 코드
        /// <para>PLC Address: D7013</para>
        /// </summary>
        wOverallErrorCode,

        // 로봇 상태
        /// <summary>
        /// 로봇 동작 모드 - 수동
        /// <para>PLC Address: D7020.0</para>
        /// </summary>
        bRobotModeManual,
        /// <summary>
        /// 로봇 동작 모드 - 자동
        /// <para>PLC Address: D7020.1</para>
        /// </summary>
        bRobotModeAuto,
        /// <summary>
        /// 로봇 동작 모드 - 원격
        /// <para>PLC Address: D7020.2</para>
        /// </summary>
        bRobotModeRemote,
        /// <summary>
        /// 로봇 모터 ON 
        /// <para>PLC Address: D7020.8</para>
        /// </summary>
        bRobotMotorOn,
        /// <summary>
        /// 로봇 준비 OK 
        /// <para>PLC Address: D7020.9</para>
        /// </summary>
        bRobotReadyOK,
        /// <summary>
        /// 로봇 기동중
        /// <para>PLC Address: D7020.A</para>
        /// </summary>
        bRobotRunning,
        /// <summary>
        /// 로봇 동작중
        /// <para>PLC Address: D7020.B</para>
        /// </summary>
        bRobotMoving,
        /// <summary>
        /// 로봇 일시 정지
        /// <para>PLC Address: D7020.C</para>
        /// </summary>
        bRobotStopHold,
        /// <summary>
        /// 로봇 비상 정지
        /// <para>PLC Address: D7020.D</para>
        /// </summary>
        bRobotEmergencyStop,
        /// <summary>
        /// 로봇 프로그램 END
        /// <para>PLC Address: D7020.E</para>
        /// </summary>
        bRobotProgramEnd,
        /// <summary>
        /// 로봇 종합 이상
        /// <para>PLC Address: D7020.F</para>
        /// </summary>
        bRobotOverallError,

        /// <summary>
        /// 로봇 TOOL 번호
        /// <para>PLC Address: D7021</para>
        /// </summary>
        wRobotToolNum,
        /// <summary>
        /// 로봇 상태
        /// <para>PLC Address: D7022</para>
        /// </summary>
        wRobotStatus,
        /// <summary>
        /// 로봇 프로그램 재생 속도
        /// <para>PLC Address: D7023</para>
        /// </summary>
        wRobotSpeed,
        /// <summary>
        /// 로봇 모터 ON 시간 (day)(low word)
        /// <para>PLC Address: D7024</para>
        /// </summary>
        dLRobotMotorOnDay,
        /// <summary>
        /// 로봇 모터 ON 시간 (day)(high word)
        /// <para>PLC Address: D7025</para>
        /// </summary>
        dHRobotMotorOnDay,
        /// <summary>
        /// 로봇 모터 ON 시간 (ms)(low word)
        /// <para>PLC Address: D7026</para>
        /// </summary>
        dLRobotMotorOnMs,
        /// <summary>
        /// 로봇 모터 ON 시간 (ms)(high word)
        /// <para>PLC Address: D7027</para>
        /// </summary>
        dHRobotMotorOnMs,
        /// <summary>
        /// 가동 시간 (day)(low word)
        /// <para>PLC Address: D7028</para>
        /// </summary>
        dLRobotRunningTimeOnDay,
        /// <summary>
        /// 가동 시간 ON (day)(high word)
        /// <para>PLC Address: D7029</para>
        /// </summary>
        dHRobotRunningTimeOnDay,
        /// <summary>
        /// 가동 시간 ON (ms)(low word)
        /// <para>PLC Address: D7030</para>
        /// </summary>
        dLRobotRunningTimeOnMs,
        /// <summary>
        /// 가동 시간 ON (ms)(high word)
        /// <para>PLC Address: D7031</para>
        /// </summary>
        dHRobotRunningTimeOnMs,

        // 로봇 작업 상태
        /// <summary>
        /// 로봇작업상태: 작업 대기 중
        /// <para>PLC Address: D7039.0</para>
        /// </summary>
        bRobotWait,
        /// <summary>
        /// 로봇작업상태: 초기 계측 위치 이동 중
        /// <para>PLC Address: D7039.1</para>
        /// </summary>
        bRobotMoveInstPose,
        /// <summary>
        /// 로봇작업상태: 부재 계측 중
        /// <para>PLC Address: D7039.2</para>
        /// </summary>
        bRobotInstrumentation,
        /// <summary>
        /// 로봇작업상태: 가열선 정보 업데이트 중
        /// <para>PLC Address: D7039.3</para>
        /// </summary>
        bRobotUpdateHeatingLines,
        /// <summary>
        /// 로봇작업상태: 초기 가열 위치 이동 중
        /// <para>PLC Address: D7039.4</para>
        /// </summary>
        bRobotMoveHeatingPose,
        /// <summary>
        /// 로봇작업상태: 가열 중
        /// <para>PLC Address: D7039.5</para>
        /// </summary>
        bRobotHeating,
        /// <summary>
        /// 로봇작업상태: 홈 자세 복귀 중
        /// <para>PLC Address: D7039.6</para>
        /// </summary>
        bRobotMoveHomePose,
        /// <summary>
        /// 로봇작업상태: 작업 완료(3초 활성화)
        /// <para>PLC Address: D7039.7</para>
        /// </summary>
        bRobotJobFinished,
        /// <summary>
        /// 로봇작업상태: 모션캠 촬영 요청
        /// <para>PLC Address: D7039.8</para>
        /// </summary>
        bRobotCaptureRequest,
        /// <summary>
        /// 로봇작업상태: 계측 프로그램 종료
        /// <para>PLC Address: D7039.9</para>
        /// </summary>
        bRobotInstFinished,
        /// <summary>
        /// 로봇작업상태: 가열선 업데이트 요청
        /// <para>PLC Address: D7039.A</para>
        /// </summary>
        bRobotUpdateRequest,

        // 가열선 정보
        /// <summary>
        /// 가열선정보: 가열선 개수
        /// <para>PLC Address: D7040</para>
        /// </summary>
        wOverallHeatingLinesNumber,
        /// <summary>
        /// 가열선정보: 가열선 번호
        /// <para>PLC Address: D7041</para>
        /// </summary>
        wCurrentHeatingLineNumber,
        /// <summary>
        /// 가열선정보: 가열선 종류
        /// <para>PLC Address: D7042</para>
        /// </summary>
        wHeatingType,
        /// <summary>
        /// 가열선정보: 가열 세기
        /// <para>단위: 0.01%</para>
        /// <para>PLC Address: D7043</para>
        /// </summary>
        wHeatingPower,
        /// <summary>
        /// 가열선정보: 가열 폭 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7044</para>
        /// </summary>
        dLHeatingWidth,
        /// <summary>
        /// 가열선정보: 가열 폭 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7045</para>
        /// </summary>
        dHHeatingWidth,
        /// <summary>
        /// 가열선정보: 가열 속도 (low word)
        /// <para>단위: 0.001cm/min</para>
        /// <para>PLC Address: D7046</para>
        /// </summary>
        dLHeatingSpeed,
        /// <summary>
        /// 가열선정보: 가열 속도 (high word)
        /// <para>단위: 0.001cm/min</para>
        /// <para>PLC Address: D7047</para>
        /// </summary>
        dHHeatingSpeed,
        /// <summary>
        /// 가열선정보: 시작점 X 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7048</para>
        /// </summary>
        dLStartPointX,
        /// <summary>
        /// 가열선정보: 시작점 X 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7049</para>
        /// </summary>
        dHStartPointX,
        /// <summary>
        /// 가열선정보: 시작점 Y 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7050</para>
        /// </summary>
        dLStartPointY,
        /// <summary>
        /// 가열선정보: 시작점 Y 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7051</para>
        /// </summary>
        dHStartPointY,
        /// <summary>
        /// 가열선정보: 시작점 Z 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7052</para>
        /// </summary>
        dLStartPointZ,
        /// <summary>
        /// 가열선정보: 시작점 Z 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7053</para>
        /// </summary>
        dHStartPointZ,
        /// <summary>
        /// 가열선정보: 끝점 X 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7054</para>
        /// </summary>
        dLEndPointX,
        /// <summary>
        /// 가열선정보: 끝점 X 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7055</para>
        /// </summary>
        dHEndPointX,
        /// <summary>
        /// 가열선정보: 끝점 Y 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7056</para>
        /// </summary>
        dLEndPointY,
        /// <summary>
        /// 가열선정보: 끝점 Y 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7057</para>
        /// </summary>
        dHEndPointY,
        /// <summary>
        /// 가열선정보: 끝점 Z 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7058</para>
        /// </summary>
        dLEndPointZ,
        /// <summary>
        /// 가열선정보: 끝점 Z 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7059</para>
        /// </summary>
        dHEndPointZ,

        // 로봇 자세
        /// <summary>
        /// 로봇자세: X (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7060</para>
        /// </summary>
        dLRobotPoseX,
        /// <summary>
        /// 로봇자세: X (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7061</para>
        /// </summary>
        dHRobotPoseX,
        /// <summary>
        /// 로봇자세: Y (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7062</para>
        /// </summary>
        dLRobotPoseY,
        /// <summary>
        /// 로봇자세: Y (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7063</para>
        /// </summary>
        dHRobotPoseY,
        /// <summary>
        /// 로봇자세: Z (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7064</para>
        /// </summary>
        dLRobotPoseZ,
        /// <summary>
        /// 로봇자세: Z (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7065</para>
        /// </summary>
        dHRobotPoseZ,
        /// <summary>
        /// 로봇자세: RX (low word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7066</para>
        /// </summary>
        dLRobotPoseRX,
        /// <summary>
        /// 로봇자세: RX (high word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7067</para>
        /// </summary>
        dHRobotPoseRX,
        /// <summary>
        /// 로봇자세: RY (low word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7068</para>
        /// </summary>
        dLRobotPoseRY,
        /// <summary>
        /// 로봇자세: RY (high word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7069</para>
        /// </summary>
        dHRobotPoseRY,
        /// <summary>
        /// 로봇자세: RZ (low word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7070</para>
        /// </summary>
        dLRobotPoseRZ,
        /// <summary>
        /// 로봇자세: RZ (high word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7071</para>
        /// </summary>
        dHRobotPoseRZ,
        /// <summary>
        /// 로봇자세: A1 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7072</para>
        /// </summary>
        dLRobotPoseA1,
        /// <summary>
        /// 로봇자세: A1 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7073</para>
        /// </summary>
        dHRobotPoseA1,
        /// <summary>
        /// 로봇자세: S (low word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7074</para>
        /// </summary>
        dLRobotPoseS,
        /// <summary>
        /// 로봇자세: S (high word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7075</para>
        /// </summary>
        dHRobotPoseS,
        /// <summary>
        /// 로봇자세: H (low word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7076</para>
        /// </summary>
        dLRobotPoseH,
        /// <summary>
        /// 로봇자세: H (high word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7077</para>
        /// </summary>
        dHRobotPoseH,
        /// <summary>
        /// 로봇자세: V (low word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7078</para>
        /// </summary>
        dLRobotPoseV,
        /// <summary>
        /// 로봇자세: V (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7079</para>
        /// </summary>
        dHRobotPoseV,
        /// <summary>
        /// 로봇자세: R2 (low word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7080</para>
        /// </summary>
        dLRobotPoseR2,
        /// <summary>
        /// 로봇자세: R2 (high word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7081</para>
        /// </summary>
        dHRobotPoseR2,
        /// <summary>
        /// 로봇자세: B (low word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7082</para>
        /// </summary>
        dLRobotPoseB,
        /// <summary>
        /// 로봇자세: B (high word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7083</para>
        /// </summary>
        dHRobotPoseB,
        /// <summary>
        /// 로봇자세: R1 (low word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7084</para>
        /// </summary>
        dLRobotPoseR1,
        /// <summary>
        /// 로봇자세: R1 (high word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7085</para>
        /// </summary>
        dHRobotPoseR1,

        /// <summary>
        /// 로봇 프로그램 번호
        /// <para>PLC Address: D7090</para>
        /// </summary>
        wRobotProgramNumber,
        /// <summary>
        /// 로봇 스텝 번호
        /// <para>PLC Address: D7091</para>
        /// </summary>
        wRobotStepNumber,
        /// <summary>
        /// 로봇 펑션 번호
        /// <para>PLC Address: D7092</para>
        /// </summary>
        wRobotFunctionNumber,
        /// <summary>
        /// 로봇 메인 프로그램 번호
        /// <para>PLC Address: D7093</para>
        /// </summary>
        wRobotMainNumber,

        // 카메라 조작 상태
        /// <summary>
        /// 파노라마 카메라 모드 (1:어안렌즈,2:2단,3:4개뷰)
        /// <para>PLC Address: D7201</para>
        /// </summary>
        bPanoramaMode,
        /// <summary>
        /// PTZ 카메라 모드(1:자동,2:수동)
        /// <para>PLC Address: D7211</para>
        /// </summary>
        bPTZMode,
        /// <summary>
        /// PTZ 카메라 ZOOM
        /// <para>PLC Address: D7212</para>
        /// </summary>
        bPTZZoom,
        /// <summary>
        ///  PTZ 카메라 홈
        /// <para>PLC Address: D7213</para>
        /// </summary>
        bPTZHome,

        // 센서 상태
        /// <summary>
        /// 
        /// </summary>
        bLJX8300Sensor1Ready,
        /// <summary>
        /// 
        /// </summary>
        bLJX8300Sensor2Ready,
        /// <summary>
        /// 
        /// </summary>
        bIL600Sensor1Ready,
        /// <summary>
        /// 
        /// </summary>
        bIL600Sensor2Ready,
        /// <summary>
        /// 
        /// </summary>
        bIL600Sensor3Ready,
        /// <summary>
        /// 
        /// </summary>
        bIL600Sensor4Ready,
        /// <summary>
        /// 
        /// </summary>
        bIL600Sensor5Ready,
        /// <summary>
        /// 
        /// </summary>
        bIL300Sensor1Ready,
        /// <summary>
        /// 
        /// </summary>
        bIL300Sensor2Ready,
        /// <summary>
        /// 
        /// </summary>
        bThermalSensorReady,

        // 센서 값
        dLLJX8300P1Height,
        dHLJX8300P1Height,
        dLLJX8300P1Angle,
        dHLJX8300P1Angle,
        dLLJX8300P2Height,
        dHLJX8300P2Height,
        dLLJX8300P2Angle,
        dHLJX8300P2Angle,
        dLLJX8300P3Height,
        dHLJX8300P3Height,
        dLLJX8300P3Angle,
        dHLJX8300P3Angle,
        dLLJX8300P4Height,
        dHLJX8300P4Height,
        dLLJX8300P4Angle,
        dHLJX8300P4Angle,
        dLLJX8300P5Height,
        dHLJX8300P5Height,
        dLLJX8300P5Angle,
        dHLJX8300P5Angle,
        dLLJX8300P6Height,
        dHLJX8300P6Height,
        dLLJX8300P6Angle,
        dHLJX8300P6Angle,
        dLLJX8300P7Height,
        dHLJX8300P7Height,
        dLLJX8300P7Angle,
        dHLJX8300P7Angle,
        dLLJX8300P8Height,
        dHLJX8300P8Height,
        dLLJX8300P8Angle,
        dHLJX8300P8Angle,
        dLLJX8300P9Height,
        dHLJX8300P9Height,
        dLLJX8300P9Angle,
        dHLJX8300P9Angle,
        dLLJX8300P10Height,
        dHLJX8300P10Height,
        dLLJX8300P10Angle,
        dHLJX8300P10Angle,

        /// <summary>
        /// 자세 제어 센서 1 - 거리 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7170</para>
        /// </summary>
        dLIL600AHeight,
        /// <summary>
        /// 자세 제어 센서 1 - 거리 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7171</para>
        /// </summary>
        dHIL600AHeight,
        /// <summary>
        /// 자세 제어 센서 2 - 거리 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7172</para>
        /// </summary>
        dLIL600BHeight,
        /// <summary>
        /// 자세 제어 센서 2 - 거리 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7173</para>
        /// </summary>
        dHIL600BHeight,
        /// <summary>
        /// 자세 제어 센서 3 - 거리 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7174</para>
        /// </summary>
        dLIL600CHeight,
        /// <summary>
        /// 자세 제어 센서 3 - 거리 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7175</para>
        /// </summary>
        dHIL600CHeight,
        /// <summary>
        /// 자세 제어 센서 4 - 거리 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7176</para>
        /// </summary>
        dLIL600DHeight,
        /// <summary>
        /// 자세 제어 센서 4 - 거리 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7177</para>
        /// </summary>
        dHIL600DHeight,
        /// <summary>
        /// 자세 제어 센서 5 - 거리 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7178</para>
        /// </summary>
        dLIL600EHeight,
        /// <summary>
        /// 자세 제어 센서 5 - 거리 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7179</para>
        /// </summary>
        dHIL600EHeight,
        /// <summary>
        /// 코일 측정 센서 1 - 거리 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7180</para>
        /// </summary>
        dLIL300AHeight,
        /// <summary>
        /// 코일 측정 센서 1 - 거리 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7181</para>
        /// </summary>
        dHIL300AHeight,
        /// <summary>
        /// 코일 측정 센서 2 - 거리 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7182</para>
        /// </summary>
        dLIL300BHeight,
        /// <summary>
        /// 코일 측정 센서 2 - 거리 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7183</para>
        /// </summary>
        dHIL300BHeight,
        /// <summary>
        /// 온도 센서 1 - 온도 (low word)
        /// <para>단위: 0.001℃</para>
        /// <para>PLC Address: D7184</para>
        /// </summary>
        dLTemperature,
        /// <summary>
        /// 온도 센서 1 - 온도 (high word)
        /// <para>단위: 0.001℃</para>
        /// <para>PLC Address: D7185</para>
        /// </summary>
        dHTemperature
    }

    public enum WriteData
    {
        Unknown,
        // 카메라 3종 상태
        /// <summary>
        /// 파노라마 카메라 상태
        /// <para>PLC Address: D7500.0</para>
        /// </summary>
        bPanoramaCameraReady,
        /// <summary>
        /// PTZ 카메라 상태
        /// <para>PLC Address: D7500.1</para>
        /// </summary>
        bPTZCameraReady,
        /// <summary>
        /// Motion 카메라 상태
        /// <para>PLC Address: D7500.2</para>
        /// </summary>
        bMotionCameraReady,

        // 작업 요청 신호
        /// <summary>
        /// 계측 시작 요청
        /// <para>PLC Address: D7510.0</para>
        /// </summary>
        bInstrumentationStart,
        /// <summary>
        /// 현재 위치 계측 완료 신호
        /// <para>PLC Address: D7510.1</para>
        /// </summary>
        bMotionCamCaptureFinished,
        /// <summary>
        /// 가열선 정보 업데이트 중
        /// <para>PLC Address: D7510.2</para>
        /// </summary>
        bHeatingLineUpdating,
        /// <summary>
        /// 가열 업데이트 시작 요청
        /// <para>PLC Address: D7510.3</para>
        /// </summary>
        bHeatingLineUpdateStart,
        /// <summary>
        /// 부재 계측 타입 (0: 수동, 1: 자동)
        /// <para>PLC Address: D7510.4</para>
        /// </summary>
        bInstrumentationType,
        /// <summary>
        /// 가열선 단일 업데이트 완료
        /// <para>PLC Address: D7510.5</para>
        /// </summary>
        bHeatingLineUpdated,
        /// <summary>
        /// 부재 가열 시작 요청
        /// <para>PLC Address: D7510.6</para>
        /// </summary>
        bHeatingStart,
        /// <summary>
        ///  부재 가열 타입 (0: 수동, 1: 자동)
        /// <para>PLC Address: D7510.7</para>
        /// </summary>
        bHeatingType,

        // 가열선 정보 업데이트 영역
        /// <summary>
        /// 가열선정보: 가열선 개수
        /// <para>PLC Address: D7550</para>
        /// </summary>
        wOverallHeatingLinesNumber,
        /// <summary>
        /// 가열선정보: 가열선 번호
        /// <para>PLC Address: D7551</para>
        /// </summary>
        wCurrentHeatingLineNumber,
        /// <summary>
        /// 가열선정보: 가열선 종류
        /// <para>PLC Address: D7552</para>
        /// </summary>
        wHeatingType,
        /// <summary>
        /// 가열선정보: 가열 세기
        /// <para>단위: 0.01%</para>
        /// <para>PLC Address: D7553</para>
        /// </summary>
        wHeatingPower,
        /// <summary>
        /// 가열선정보: 가열 폭 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7554</para>
        /// </summary>
        dLHeatingWidth,
        /// <summary>
        /// 가열선정보: 가열 폭 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7555</para>
        /// </summary>
        dHHeatingWidth,
        /// <summary>
        /// 가열선정보: 가열 속도 (low word)
        /// <para>단위: 0.001cm/min</para>
        /// <para>PLC Address: D7556</para>
        /// </summary>
        dLHeatingSpeed,
        /// <summary>
        /// 가열선정보: 가열 속도 (high word)
        /// <para>단위: 0.001cm/min</para>
        /// <para>PLC Address: D7557</para>
        /// </summary>
        dHHeatingSpeed,
        /// <summary>
        /// 가열선정보: 시작점 X 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7558</para>
        /// </summary>
        dLStartPointX,
        /// <summary>
        /// 가열선정보: 시작점 X 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7559</para>
        /// </summary>
        dHStartPointX,
        /// <summary>
        /// 가열선정보: 시작점 Y 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7560</para>
        /// </summary>
        dLStartPointY,
        /// <summary>
        /// 가열선정보: 시작점 Y 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7561</para>
        /// </summary>
        dHStartPointY,
        /// <summary>
        /// 가열선정보: 시작점 Z 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7562</para>
        /// </summary>
        dLStartPointZ,
        /// <summary>
        /// 가열선정보: 시작점 Z 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7563</para>
        /// </summary>
        dHStartPointZ,
        /// <summary>
        /// 가열선정보: 끝점 X 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7564</para>
        /// </summary>
        dLEndPointX,
        /// <summary>
        /// 가열선정보: 끝점 X 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7565</para>
        /// </summary>
        dHEndPointX,
        /// <summary>
        /// 가열선정보: 끝점 Y 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7566</para>
        /// </summary>
        dLEndPointY,
        /// <summary>
        /// 가열선정보: 끝점 Y 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7567</para>
        /// </summary>
        dHEndPointY,
        /// <summary>
        /// 가열선정보: 끝점 Z 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7568</para>
        /// </summary>
        dLEndPointZ,
        /// <summary>
        /// 가열선정보: 끝점 Z 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7569</para>
        /// </summary>
        dHEndPointZ,
        /// <summary>
        /// 가열선정보: 삼각가열 수평이동 각도 (low word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7570</para>
        /// </summary>
        dLTriHeatingHorizontalAngle,
        /// <summary>
        /// <summary>
        /// 가열선정보: 삼각가열 수평이동 각도 (high word)
        /// <para>단위: 0.001deg</para>
        /// <para>PLC Address: D7571</para>
        /// </summary>
        dHTriHeatingHorizontalAngle,

        // 계측 전 이미지 선택하여 계산한 꼭지점 정보
        /// <summary>
        /// 이미지 꼭지점 1 X 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7600</para>
        /// </summary>
        dLImageCorner1X,
        /// <summary>
        /// 이미지 꼭지점 1 X 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7601</para>
        /// </summary>
        dHImageCorner1X,
        /// <summary>
        /// 이미지 꼭지점 1 Y 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7602</para>
        /// </summary>
        dLImageCorner1Y,
        /// <summary>
        /// 이미지 꼭지점 1 Y 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7603</para>
        /// </summary>
        dHImageCorner1Y,
        /// <summary>
        /// 이미지 꼭지점 2 X 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7604</para>
        /// </summary>
        dLImageCorner2X,
        /// <summary>
        /// 이미지 꼭지점 2 X 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7605</para>
        /// </summary>
        dHImageCorner2X,
        /// <summary>
        /// 이미지 꼭지점 2 Y 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7606</para>
        /// </summary>
        dLImageCorner2Y,
        /// <summary>
        /// 이미지 꼭지점 2 Y 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7607</para>
        /// </summary>
        dHImageCorner2Y,
        /// <summary>
        /// 이미지 꼭지점 3 X 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7608</para>
        /// </summary>
        dLImageCorner3X,
        /// <summary>
        /// 이미지 꼭지점 3 X 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7609</para>
        /// </summary>
        dHImageCorner3X,
        /// <summary>
        /// 이미지 꼭지점 3 Y 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7610</para>
        /// </summary>
        dLImageCorner3Y,
        /// <summary>
        /// 이미지 꼭지점 3 Y 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7611</para>
        /// </summary>
        dHImageCorner3Y,
        /// <summary>
        /// 이미지 꼭지점 4 X 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7612</para>
        /// </summary>
        dLImageCorner4X,
        /// <summary>
        /// 이미지 꼭지점 4 X 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7613</para>
        /// </summary>
        dHImageCorner4X,
        /// <summary>
        /// 이미지 꼭지점 4 Y 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7614</para>
        /// </summary>
        dLImageCorner4Y,
        /// <summary>
        /// 이미지 꼭지점 4 Y 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7615</para>
        /// </summary>
        dHImageCorner4Y,

        // 계측 후 실제 계측 데이터 기반 꼭지점 정보
        /// <summary>
        /// 계측 꼭지점 1 X 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7620</para>
        /// </summary>
        dLInstrCorner1X,
        /// <summary>
        /// 계측 꼭지점 1 X 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7621</para>
        /// </summary>
        dHInstrCorner1X,
        /// <summary>
        /// 계측 꼭지점 1 Y 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7622</para>
        /// </summary>
        dLInstrCorner1Y,
        /// <summary>
        /// 계측 꼭지점 1 Y 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7623</para>
        /// </summary>
        dHInstrCorner1Y,
        /// <summary>
        /// 계측 꼭지점 2 X 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7624</para>
        /// </summary>
        dLInstrCorner2X,
        /// <summary>
        /// 계측 꼭지점 2 X 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7625</para>
        /// </summary>
        dHInstrCorner2X,
        /// <summary>
        /// 계측 꼭지점 2 Y 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7626</para>
        /// </summary>
        dLInstrCorner2Y,
        /// <summary>
        /// 계측 꼭지점 2 Y 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7627</para>
        /// </summary>
        dHInstrCorner2Y,
        /// <summary>
        /// 계측 꼭지점 3 X 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7628</para>
        /// </summary>
        dLInstrCorner3X,
        /// <summary>
        /// 계측 꼭지점 3 X 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7629</para>
        /// </summary>
        dHInstrCorner3X,
        /// <summary>
        /// 계측 꼭지점 3 Y 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7630</para>
        /// </summary>
        dLInstrCorner3Y,
        /// <summary>
        /// 계측 꼭지점 3 Y 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7631</para>
        /// </summary>
        dHInstrCorner3Y,
        /// <summary>
        /// 계측 꼭지점 4 X 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7632</para>
        /// </summary>
        dLInstrCorner4X,
        /// <summary>
        /// 계측 꼭지점 4 X 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7633</para>
        /// </summary>
        dHInstrCorner4X,
        /// <summary>
        /// 계측 꼭지점 4 Y 좌표 (low word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7634</para>
        /// </summary>
        dLInstrCorner4Y,
        /// <summary>
        /// 계측 꼭지점 4 Y 좌표 (high word)
        /// <para>단위: 0.001mm</para>
        /// <para>PLC Address: D7635</para>
        /// </summary>
        dHInstrCorner4Y,
    }
}
