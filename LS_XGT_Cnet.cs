using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Globalization;
using System.Threading;

namespace PLCInterface
{
    class XGT_Cnet : IPLC
    {
        private SerialPort _serialPort;
        private int _station = 0;

        public Dictionary<int, string> CNetError;

        public XGT_Cnet(int iStation, string iPortName, int iBaudrate, int iDatabits, Parity iParity, StopBits iStopBits)
        {
            if (iStation > 255)
                return;
            _station = iStation;

            _serialPort = new SerialPort();
            _serialPort.PortName = iPortName;
            _serialPort.BaudRate = iBaudrate;
            _serialPort.DataBits = iDatabits;
            _serialPort.Parity = iParity;
            _serialPort.StopBits = iStopBits;
            _serialPort.ReadTimeout = 100;
            _serialPort.WriteTimeout = 100;

            InitError();
        }

        private void InitError()
        {
            CNetError = new Dictionary<int, string>();
            // xgt error
            CNetError[0x0003] = "Number of blocks exceeded :: Number of blocks exceeds 16 at Individual Read/Write Request";
            CNetError[0x0004] = "Variable length error :: Variable Length exceeds the max. size of 16";
            CNetError[0x0007] = "Data type error :: Other data type than X,B,W,D,L received";
            CNetError[0x0011] = "Data error :: In case % is unavailable to start with || Variable’s area value wrong || Other value is written for Bit Write than 00 or 01";
            CNetError[0x0090] = "Monitor execution error :: Unregistered monitor execution requested";
            CNetError[0x0190] = "Monitor execution error :: Reg. No. range exceeded";
            CNetError[0x0290] = "Monitor reg. Error :: Reg. No. range exceeded";
            CNetError[0x1132] = "Device memory error :: Other letter than applicable device is input";
            CNetError[0x1232] = "Data size error :: Request exceeds the max range of 60 Words to read or write at a time. 0";
            CNetError[0x1234] = "Extra frame error :: Unnecessary details exist as added";
            CNetError[0x1332] = "Data type discordant :: All the blocks shall be requested of the identical data type in the case of Individual Read/Write";
            CNetError[0x1432] = "Data value error :: Data value unavailable to convert to Hex";
            CNetError[0x7132] = "Variable request area exceeded :: Request exceeds the area each device supports";
            // custom error
            CNetError[0x9000] = "Long Word 값을 Int 형으로 받아올 수 없음.";
        }

        public bool IsOpen()
        {
            return _serialPort.IsOpen;
        }

        public bool Open()
        {
            if (!_serialPort.IsOpen)
                _serialPort.Open();

            if (_serialPort.IsOpen)
                return true;
            else
                return false;
        }

        public bool Close()
        {
            _serialPort.Close();

            if (!_serialPort.IsOpen)
                return true;
            else
                return false;
        }

        public bool GetStatus()
        {
            return true;
        }

        public bool ReadSingleDevice(string iDevice, out int oValue, out int oErrCode)
        {
            string[] iDevices = { iDevice };
            int[] oValues = { 0 };
            oValue = oValues[0];
            if (!ReadIndividualDevices(iDevices, out oValues, out oErrCode))
                return false;
            oValue = oValues[0];
            return true;
        }

        public bool ReadSingleDevice(string iDevice, out long oValue, out int oErrCode)
        {
            string[] iDevices = { iDevice };
            long[] oValues = { 0 };
            oValue = oValues[0];
            if (!ReadIndividualDevices(iDevices, out oValues, out oErrCode))
                return false;
            oValue = oValues[0];
            return true;

        }

        public bool ReadIndividualDevices(string[] iDevices, out int[] oValues, out int oErrCode)
        {
            oValues = new int[iDevices.Length];
            oErrCode = 0;
            if (iDevices[0].Substring(2, 1) == "L")
            {
                oErrCode = 0x9000;
                return false;
            }
            bool result = ReadIndividualDevices(iDevices, out long[] oLongValues, out oErrCode);
            if (!result)
                return result;
            for (int i = 0; i < iDevices.Length; i++)
            {
                oValues[i] = Convert.ToInt32(oLongValues[i]);
            }
            return result;
        }

        public bool ReadIndividualDevices(string[] iDevices, out long[] oValues, out int oErrCode)
        {
            //출력 결과 초기화
            oErrCode = 0;
            oValues = new long[iDevices.Length];
            Array.Clear(oValues, 0, oValues.Length);

            //읽을 주소 개수 확인
            int deviceCnt = iDevices.Length;
            if (deviceCnt > 16 || deviceCnt < 1)
            {
                oErrCode = -1; // 디바이스 개수 초과 에러 리턴
                return false;
            }

            //읽을 주소 타입 체크 및 통일성 확인 (첫번째 타입과 다른 타입이 있는 경우 에러 리턴)
            string refDeviceType = iDevices[0].Substring(2, 1);
            foreach (string device in iDevices)
                if (device.Substring(2, 1) != refDeviceType)
                {
                    oErrCode = -2;
                    return false; // 디바이스 타입 불일치 에러 리턴
                }

            //읽을 주소 타입, Byte Size 지정
            int byteSize = 0; // byte size
            switch (refDeviceType)
            {
                case "X":
                    byteSize = 1;
                    break;
                case "B":
                    byteSize = 1;
                    break;
                case "W":
                    byteSize = 2;
                    break;
                case "D":
                    byteSize = 4;
                    break;
                case "L":
                    byteSize = 8;
                    break;
                default: //잘못된 타입 입력
                    oErrCode = -3;
                    return false;
            }

            //전체 주소 길이 계산
            int sendLength = 0;
            sendLength += 1; //header
            sendLength += 2; //station number
            sendLength += 1; //command
            sendLength += 2; //command type
            sendLength += 2; //number of blocks
            foreach (string dev in iDevices)
            {
                sendLength += 2;           //device length
                sendLength += dev.Length;  //device name
            }
            sendLength += 1; //tail
            sendLength += 2; //bcc

            //송신할 패킷 초기화
            byte[] send = new byte[sendLength];
            int idx = 0;

            //--------------------packet-------------------- 
            send[idx++] = (byte)FrameHeader.ENQ;

            string strStation = string.Format("{0:X2}", _station);
            send[idx++] = Convert.ToByte(strStation[0]); //station number
            send[idx++] = Convert.ToByte(strStation[1]);

            send[idx++] = 0x52; //주 명령어 (R)
            send[idx++] = 0x53; //명령어 타입 (S)
            send[idx++] = 0x53; //명령어 타입 (S)

            //블록 수
            string strDevCnt = string.Format("{0:X2}", deviceCnt);
            send[idx++] = Convert.ToByte((char)strDevCnt[0]);
            send[idx++] = Convert.ToByte((char)strDevCnt[1]);

            //각 블록 디바이스 이름 길이 / 디바이스 이름 입력
            foreach (string device in iDevices)
            {
                string strDevLen = string.Format("{0:X2}", device.Length);
                send[idx++] = Convert.ToByte((char)strDevLen[0]);
                send[idx++] = Convert.ToByte((char)strDevLen[1]);
                byte[] bDevice = Encoding.ASCII.GetBytes(device);
                for (int i = 0; i < device.Length; i++)
                {
                    send[idx++] = bDevice[i];
                }
            }
            send[idx++] = (byte)FrameHeader.EOT;

            //byte checksum 계산
            byte bcc = 0;
            foreach (byte b in send)
            {
                bcc = (byte)(bcc + b);
            }
            //계산된 byte를 ascii 코드로 변환 (ex, 0x36 -> 3을 0x33 으로, 6을 0x36으로)
            byte[] encBcc = Encoding.ASCII.GetBytes(string.Format("{0:X2}, ", bcc));
            send[idx++] = encBcc[0];
            send[idx++] = encBcc[1];

            //--------------------send packet-------------------- 
            _serialPort.DiscardInBuffer();
            _serialPort.Write(send, 0, send.Length);

            //--------------------recv packet--------------------
            int recvACKLength = 9 + deviceCnt * (2 + byteSize * 2);
            int recvNAKLength = 11;
            byte[] recv = new byte[Math.Max(recvACKLength, recvNAKLength)];
            Array.Clear(recv, 0, recv.Length);
            int idxRecv = 0;
            try
            {
                while (true)
                {
                    byte[] buf = new byte[recv.Length];
                    _serialPort.Read(buf, 0, buf.Length);
                    foreach (byte b in buf)
                    {
                        if (b != 0x00) recv[idxRecv++] = b;
                        else break;
                    }
                }
            }
            catch
            {
                if (idxRecv != recvACKLength && idxRecv != recvNAKLength)
                {
                    oErrCode = -4;
                    return false; // 타임 아웃 에러 리턴}
                }
            }
            ////--------------------출력 테스트--------------------
            //string str = "";
            //foreach (byte b in recv)
            //{
            //    str = str + string.Format("0x{0:X2} \r\n", b);
            //}
            ////MessageBox.Show(str);

            //--------------------결과 출력--------------------
            if (recv[0] == (byte)FrameHeader.ACK)
            {
                int startIdx = 8;
                for (int i = 0; i < deviceCnt; i++)
                {
                    byte[] temp = new byte[byteSize * 2];
                    Array.Clear(temp, 0, temp.Length);
                    Array.Copy(recv, startIdx + 2, temp, 0, byteSize * 2);
                    string strValue = Encoding.ASCII.GetString(temp);
                    long value = 0;
                    if (long.TryParse(strValue, NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out value))
                        oValues[i] = value;
                    else
                    {
                        oErrCode = -5;
                        return false;
                    }

                    startIdx = startIdx + 2 + byteSize * 2;
                }
                return true;
            }
            else if (recv[0] == (byte)FrameHeader.NAK)
            {
                byte[] temp = new byte[4];
                Array.Clear(temp, 0, temp.Length);
                Array.Copy(recv, 6, temp, 0, 4);
                string strErrCode = Encoding.ASCII.GetString(temp);
                //oValues[0] = Convert.ToInt64(strValue, 16); //에러코드를 0번 결과에 입력
                oErrCode = Convert.ToInt32(strErrCode, 16); //에러코드 입력
                return false; // PLC 응답 에러 리턴
            }
            else
            {
                oErrCode = -6;
                return false; // 원인 불명 에러 리턴
            }
        }

        public bool ReadContinuousDevices(string iStartDevice, int iDeviceCount, out int[] oValues, out int oErrCode)
        {
            oValues = new int[iDeviceCount];
            oErrCode = 0;
            if (iStartDevice.Substring(2, 1) == "L")
            {
                oErrCode = 0x9000;
                return false;
            }
            bool result = ReadContinuousDevices(iStartDevice, iDeviceCount, out long[] oLongValues, out oErrCode);
            if (!result)
                return result;
            for (int i = 0; i < iDeviceCount; i++)
            {
                oValues[i] = Convert.ToInt32(oLongValues[i]);
            }
            return result;
        }

        public bool ReadContinuousDevices(string iStartDevice, int iDeviceCount, out long[] oValues, out int oErrCode)
        {
            //출력 결과 초기화
            oErrCode = 0;
            oValues = new long[iDeviceCount];
            Array.Clear(oValues, 0, oValues.Length);

            //읽을 주소 개수 확인
            if (iDeviceCount > 16 || iDeviceCount < 1)
            {
                oErrCode = -1; // 디바이스 개수 초과 에러 리턴
                return false;
            }

            //읽을 주소 타입, Byte Size 지정
            int byteSize = 0; // byte size
            switch (iStartDevice.Substring(2, 1))
            {
                case "X":
                    oErrCode = -3;
                    return false;
                case "B":
                    byteSize = 1;
                    break;
                case "W":
                    byteSize = 2;
                    break;
                case "D":
                    byteSize = 4;
                    break;
                case "L":
                    byteSize = 8;
                    break;
                default: //잘못된 타입 입력
                    oErrCode = -3;
                    return false;
            }

            //전체 주소 길이 계산
            int sendLength = 0;
            sendLength += 1; //header
            sendLength += 2; //station number
            sendLength += 1; //command
            sendLength += 2; //command type
            sendLength += 2; //device length
            sendLength += iStartDevice.Length; //device name
            sendLength += 2; //number of data
            sendLength += 1; //tail
            sendLength += 2; //bcc

            //송신할 패킷 초기화
            byte[] send = new byte[sendLength];
            int idx = 0;

            //--------------------packet-------------------- 
            send[idx++] = (byte)FrameHeader.ENQ; //frame header

            string strStation = string.Format("{0:X2}", _station);
            send[idx++] = Convert.ToByte(strStation[0]); //station number
            send[idx++] = Convert.ToByte(strStation[1]);

            send[idx++] = 0x52; //주 명령어 (R)
            send[idx++] = 0x53; //명령어 타입 (S)
            send[idx++] = 0x42; //명령어 타입 (B)

            //읽기 시작 변수 길이
            string strDevLength = string.Format("{0:X2}", iStartDevice.Length);
            send[idx++] = Convert.ToByte((char)strDevLength[0]);
            send[idx++] = Convert.ToByte((char)strDevLength[1]);

            //읽기 시작 변수 이름
            byte[] bDevice = Encoding.ASCII.GetBytes(iStartDevice);
            for (int i = 0; i < iStartDevice.Length; i++)
            {
                send[idx++] = bDevice[i];
            }

            //읽을 변수 개수
            string strDevCnt = string.Format("{0:X2}", iDeviceCount);
            send[idx++] = Convert.ToByte((char)strDevCnt[0]);
            send[idx++] = Convert.ToByte((char)strDevCnt[1]);

            send[idx++] = (byte)FrameHeader.EOT;

            //byte checksum 계산
            byte bcc = 0;
            foreach (byte b in send)
            {
                bcc = (byte)(bcc + b);
            }
            //계산된 byte를 ascii 코드로 변환 (ex, 0x36 -> 3을 0x33 으로, 6을 0x36으로)
            byte[] encBcc = Encoding.ASCII.GetBytes(string.Format("{0:X2}, ", bcc));
            send[idx++] = encBcc[0];
            send[idx++] = encBcc[1];

            //--------------------send packet-------------------- 
            _serialPort.DiscardInBuffer();
            _serialPort.Write(send, 0, send.Length);

            //--------------------recv packet--------------------
            int recvACKLength = 9 + iDeviceCount * (2 + byteSize * 2);
            int recvNAKLength = 11;
            byte[] recv = new byte[Math.Max(recvACKLength, recvNAKLength)];
            Array.Clear(recv, 0, recv.Length);
            int idxRecv = 0;
            try
            {
                while (true)
                {
                    byte[] buf = new byte[recv.Length];
                    _serialPort.Read(buf, 0, buf.Length);
                    foreach (byte b in buf)
                    {
                        if (b != 0x00) recv[idxRecv++] = b;
                        else break;
                    }
                }
            }
            catch
            {
                if (idxRecv != recvACKLength && idxRecv != recvNAKLength)
                {
                    oErrCode = -4;
                    return false; // 타임 아웃 에러 리턴}
                }
            }
            ////--------------------출력 테스트--------------------
            //string str = "";
            //foreach (byte b in recv)
            //{
            //    str = str + string.Format("0x{0:X2} \r\n", b);
            //}
            ////MessageBox.Show(str);

            //--------------------결과 출력--------------------
            if (recv[0] == (byte)FrameHeader.ACK)
            {
                int startIdx = 8;
                for (int i = 0; i < iDeviceCount; i++)
                {
                    //데이터 개수(크기) 계산 --> byteSize 변수 사용
                    //byte[] temp = new byte[2];
                    //Array.Clear(temp, 0, temp.Length);
                    //Array.Copy(recv, startIdx, temp, 0, 2);
                    //string strDataNum = Encoding.ASCII.GetString(temp);
                    //byte dataNum = Convert.ToByte(strDataNum, 16);

                    byte[] temp = new byte[byteSize * 2];
                    Array.Clear(temp, 0, temp.Length);
                    Array.Copy(recv, startIdx + 2, temp, 0, byteSize * 2);
                    string strValue = Encoding.ASCII.GetString(temp);
                    long value = 0;
                    if (long.TryParse(strValue, NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out value))
                        oValues[i] = value;
                    else
                    {
                        oErrCode = -5;
                        return false;
                    }

                    startIdx = startIdx + 2 + byteSize * 2;
                }
                return true;
            }
            else if (recv[0] == (byte)FrameHeader.NAK)
            {
                byte[] temp = new byte[4];
                Array.Clear(temp, 0, temp.Length);
                Array.Copy(recv, 6, temp, 0, 4);
                string strValue = Encoding.ASCII.GetString(temp);
                //oValues[0] = Convert.ToInt64(strValue, 16); //에러코드를 0번 결과에 입력
                oErrCode = Convert.ToInt32(strValue, 16); //에러코드 입력
                return false; // PLC 응답 에러 리턴
            }
            else
            {
                oErrCode = -6;
                return false; // 원인 불명 에러 리턴
            }
        }

        public bool WriteSingleDevice(string iDevice, int iValue, out int oErrCode)
        {
            string[] iDevices = { iDevice };
            int[] iValues = { iValue };
            if (!WriteIndividualDevices(iDevices, iValues, out oErrCode))
                return false;
            return true;
        }

        public bool WriteSingleDevice(string iDevice, long iValue, out int oErrCode)
        {
            string[] iDevices = { iDevice };
            long[] iValues = { iValue };
            if (!WriteIndividualDevices(iDevices, iValues, out oErrCode))
                return false;
            return true;
        }

        public bool WriteIndividualDevices(string[] iDevices, int[] iValues, out int oErrCode)
        {
            long[] iLongValues = new long[iDevices.Length];
            oErrCode = 0;
            if (iDevices[0].Substring(2, 1) == "L")
            {
                oErrCode = 0x9000;
                return false;
            }
            for (int i = 0; i < iDevices.Length; i++)
            {
                iLongValues[i] = Convert.ToInt64(iValues[i]);
            }
            bool result = WriteIndividualDevices(iDevices, iLongValues, out oErrCode);
            if (!result)
                return result;
            return result;
        }

        public bool WriteIndividualDevices(string[] iDevices, long[] iValues, out int oErrCode)
        {
            //출력 결과 초기화
            oErrCode = 0;

            //읽을 주소 개수 확인
            int deviceCnt = iDevices.Length;
            if (deviceCnt > 16 || deviceCnt < 1)
            {
                oErrCode = -1; // 디바이스 개수 초과 에러 리턴
                return false;
            }

            //읽을 주소 타입 체크 및 통일성 확인 (첫번째 타입과 다른 타입이 있는 경우 에러 리턴)
            string refDeviceType = iDevices[0].Substring(2, 1);
            foreach (string device in iDevices)
                if (device.Substring(2, 1) != refDeviceType)
                {
                    oErrCode = -2;
                    return false; // 디바이스 타입 불일치 에러 리턴
                }

            //읽을 주소 타입, Byte Size 지정
            int byteSize = 0; // byte size
            switch (refDeviceType)
            {
                case "X":
                    byteSize = 1;
                    break;
                case "B":
                    byteSize = 1;
                    break;
                case "W":
                    byteSize = 2;
                    break;
                case "D":
                    byteSize = 4;
                    break;
                case "L":
                    byteSize = 8;
                    break;
                default: //잘못된 타입 입력
                    oErrCode = -3;
                    return false;
            }

            //전체 주소 길이 계산
            int sendLength = 0;
            sendLength += 1; //header
            sendLength += 2; //station number
            sendLength += 1; //command
            sendLength += 2; //command type
            sendLength += 2; //number of blocks
            foreach (string dev in iDevices)
            {
                sendLength += 2;           //device length
                sendLength += dev.Length;  //device name
                sendLength += byteSize;    //data
            }
            sendLength += 1; //tail
            sendLength += 2; //bcc

            //송신할 패킷 초기화
            byte[] send = new byte[sendLength];
            int idx = 0;

            //--------------------packet-------------------- 
            send[idx++] = (byte)FrameHeader.ENQ;

            string strStation = string.Format("{0:X2}", _station);
            send[idx++] = Convert.ToByte(strStation[0]); //station number
            send[idx++] = Convert.ToByte(strStation[1]);

            send[idx++] = 0x57; //주 명령어 (W)
            send[idx++] = 0x53; //명령어 타입 (S)
            send[idx++] = 0x53; //명령어 타입 (S)

            //블록 수
            string strDevCnt = string.Format("{0:X2}", deviceCnt);
            send[idx++] = Convert.ToByte((char)strDevCnt[0]);
            send[idx++] = Convert.ToByte((char)strDevCnt[1]);

            //각 블록 디바이스 이름 길이 / 디바이스 이름 / 데이터 입력
            for (int i = 0; i < deviceCnt; i++)
            {
                //디바이스 이름 길이
                string strDev = string.Format("{0:X2}", iDevices[i].Length);
                send[idx++] = Convert.ToByte((char)strDev[0]);
                send[idx++] = Convert.ToByte((char)strDev[1]);

                //디바이스 이름
                byte[] bDevice = Encoding.ASCII.GetBytes(iDevices[i]);
                foreach (byte b in bDevice)
                {
                    send[idx++] = b;
                }

                //데이터
                byte[] tempDataBytes = BitConverter.GetBytes(iValues[i]);
                string strBytes = "";
                for (int j = 0; j < byteSize; j++)
                {
                    strBytes = strBytes + string.Format("{0:X2}", tempDataBytes[byteSize - 1 - j]);
                }

                byte[] bData = Encoding.ASCII.GetBytes(strBytes);
                foreach (byte b in bData)
                {
                    send[idx++] = b;
                }
            }
            //테일
            send[idx++] = (byte)FrameHeader.EOT;

            //byte checksum 계산
            byte bcc = 0;
            foreach (byte b in send)
            {
                bcc = (byte)(bcc + b);
            }
            //계산된 byte를 ascii 코드로 변환 (ex, 0x36 -> 3을 0x33 으로, 6을 0x36으로)
            byte[] encBcc = Encoding.ASCII.GetBytes(string.Format("{0:X2}, ", bcc));
            send[idx++] = encBcc[0];
            send[idx++] = encBcc[1];

            //--------------------send packet-------------------- 
            _serialPort.DiscardInBuffer();
            _serialPort.Write(send, 0, send.Length);

            //--------------------recv packet--------------------
            int recvAckLen = 7;
            int recvNakLen = 11;
            byte[] recv = new byte[Math.Max(recvAckLen, recvNakLen)];
            Array.Clear(recv, 0, recv.Length);
            int idxRecv = 0;
            try
            {
                while (true)
                {
                    byte[] buf = new byte[recv.Length];
                    _serialPort.Read(buf, 0, buf.Length);
                    foreach (byte b in buf)
                    {
                        if (b != 0x00) recv[idxRecv++] = b;
                        else break;
                    }
                }
            }
            catch
            {
                if (idxRecv != recvAckLen && idxRecv != recvNakLen)
                {
                    oErrCode = -4;
                    return false; // 타임 아웃 에러 리턴
                }
            }

            ////--------------------출력 테스트--------------------
            //string str = "";
            //foreach (byte b in recv)
            //{
            //    str = str + string.Format("0x{0:X2} \r\n", b);
            //}
            ////MessageBox.Show(str);

            //--------------------결과 출력--------------------
            if (recv[0] == (byte)FrameHeader.ACK)
                return true;
            else if (recv[0] == (byte)FrameHeader.NAK)
            {
                //에러코드 출력
                byte[] temp = new byte[4];
                Array.Clear(temp, 0, temp.Length);
                Array.Copy(recv, 6, temp, 0, 4);
                string strValue = Encoding.ASCII.GetString(temp);
                //oValues[0] = Convert.ToInt64(strValue, 16); //에러코드를 0번 결과에 입력
                oErrCode = Convert.ToInt32(strValue, 16); //에러코드 입력
                return false;
            }
            else
            {
                oErrCode = -6;
                return false; // 원인 불명 에러 리턴
            }
        }

        public bool WriteContinuousDevices(string iStartDevice, int iDeviceCount, int[] iValues, out int oErrCode)
        {
            long[] iLongValues = new long[iDeviceCount];
            oErrCode = 0;
            if (iStartDevice.Substring(2, 1) == "L")
            {
                oErrCode = 0x9000;
                return false;
            }
            for (int i = 0; i < iDeviceCount; i++)
            {
                iLongValues[i] = Convert.ToInt64(iValues[i]);
            }
            bool result = WriteContinuousDevices(iStartDevice, iDeviceCount, iLongValues, out oErrCode);
            if (!result)
                return result;
            return result;
        }

        public bool WriteContinuousDevices(string iStartDevice, int iDeviceCount, long[] iValues, out int oErrCode)
        {
            //출력 결과 초기화
            oErrCode = 0;

            //읽을 주소 개수 확인
            int deviceCnt = iDeviceCount;
            if (deviceCnt > 16 || deviceCnt < 1)
            {
                oErrCode = -1; // 디바이스 개수 초과 에러 리턴
                return false;
            }

            //쓸 주소 타입, Byte Size 지정
            int byteSize = 0; // byte size
            switch (iStartDevice.Substring(2, 1))
            {
                case "X":
                    oErrCode = -3;
                    return false;
                case "B":
                    byteSize = 1;
                    break;
                case "W":
                    byteSize = 2;
                    break;
                case "D":
                    byteSize = 4;
                    break;
                case "L":
                    byteSize = 8;
                    break;
                default: //잘못된 타입 입력
                    oErrCode = -3;
                    return false;
            }

            //전체 주소 길이 계산
            int sendLength = 0;
            sendLength += 1; //header
            sendLength += 2; //station number
            sendLength += 1; //command
            sendLength += 2; //command type
            sendLength += 2; //device length
            sendLength += iStartDevice.Length; //device name
            sendLength += 2; //number of data
            for (int i = 0; i < iDeviceCount; i++)
            {
                sendLength += byteSize * 2; //data
            }
            sendLength += 1; //tail
            sendLength += 2; //bcc

            //송신할 패킷 초기화
            byte[] send = new byte[sendLength];
            int idx = 0;

            //--------------------packet-------------------- 
            send[idx++] = (byte)FrameHeader.ENQ;

            string strStation = string.Format("{0:X2}", _station);
            send[idx++] = Convert.ToByte(strStation[0]); //station number
            send[idx++] = Convert.ToByte(strStation[1]);

            send[idx++] = 0x57; //주 명령어 (W)
            send[idx++] = 0x53; //명령어 타입 (S)
            send[idx++] = 0x42; //명령어 타입 (B)

            //디바이스 이름 길이
            string strDev = string.Format("{0:X2}", iStartDevice.Length);
            send[idx++] = Convert.ToByte((char)strDev[0]);
            send[idx++] = Convert.ToByte((char)strDev[1]);

            //디바이스 이름
            byte[] bDevice = Encoding.ASCII.GetBytes(iStartDevice);
            foreach (byte b in bDevice)
            {
                send[idx++] = b;
            }

            //블록 수
            string strDevCnt = string.Format("{0:X2}", deviceCnt);
            send[idx++] = Convert.ToByte((char)strDevCnt[0]);
            send[idx++] = Convert.ToByte((char)strDevCnt[1]);

            //각 블록 디바이스 이름 길이 / 디바이스 이름 / 데이터 입력
            for (int i = 0; i < deviceCnt; i++)
            {
                //데이터
                byte[] tempDataBytes = BitConverter.GetBytes(iValues[i]);
                string strBytes = "";
                for (int j = 0; j < byteSize; j++)
                {
                    strBytes = strBytes + string.Format("{0:X2}", tempDataBytes[byteSize - 1 - j]);
                }

                byte[] bData = Encoding.ASCII.GetBytes(strBytes);
                foreach (byte b in bData)
                {
                    send[idx++] = b;
                }
            }
            //테일
            send[idx++] = (byte)FrameHeader.EOT;

            //byte checksum 계산
            byte bcc = 0;
            foreach (byte b in send)
            {
                bcc = (byte)(bcc + b);
            }
            //계산된 byte를 ascii 코드로 변환 (ex, 0x36 -> 3을 0x33 으로, 6을 0x36으로)
            byte[] encBcc = Encoding.ASCII.GetBytes(string.Format("{0:X2}, ", bcc));
            send[idx++] = encBcc[0];
            send[idx++] = encBcc[1];

            //--------------------send packet-------------------- 
            _serialPort.DiscardInBuffer();
            _serialPort.Write(send, 0, send.Length);

            //--------------------recv packet--------------------
            int recvAckLen = 7;
            int recvNakLen = 11;
            byte[] recv = new byte[Math.Max(recvAckLen, recvNakLen)];
            Array.Clear(recv, 0, recv.Length);
            int idxRecv = 0;
            try
            {
                while (true)
                {
                    byte[] buf = new byte[recv.Length];
                    _serialPort.Read(buf, 0, buf.Length);
                    foreach (byte b in buf)
                    {
                        if (b != 0x00) recv[idxRecv++] = b;
                        else break;
                    }
                }
            }
            catch
            {
                if (idxRecv != recvAckLen && idxRecv != recvNakLen)
                {
                    oErrCode = -4;
                    return false; // 타임 아웃 에러 리턴
                }
            }

            ////--------------------출력 테스트--------------------
            //string str = "";
            //foreach (byte b in recv)
            //{
            //    str = str + string.Format("0x{0:X2} \r\n", b);
            //}
            ////MessageBox.Show(str);

            //--------------------결과 출력--------------------
            if (recv[0] == (byte)FrameHeader.ACK)
                return true;
            else if (recv[0] == (byte)FrameHeader.NAK)
            {
                //에러코드 출력
                byte[] temp = new byte[4];
                Array.Clear(temp, 0, temp.Length);
                Array.Copy(recv, 6, temp, 0, 4);
                string strValue = Encoding.ASCII.GetString(temp);
                //oValues[0] = Convert.ToInt64(strValue, 16); //에러코드를 0번 결과에 입력
                oErrCode = Convert.ToInt32(strValue, 16); //에러코드 입력
                return false;
            }
            else
            {
                oErrCode = -6;
                return false; // 원인 불명 에러 리턴
            }
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

        private enum FrameHeader
        {
            /** <summary>요구 프레임 시작 코드</summary> */
            ENQ = 0x05,
            /** <summary>ACK 응답 프레임 시작 코드</summary> */
            ACK = 0x06,
            /** <summary>NAK 응답 프레임 시작 코드</summary> */
            NAK = 0x15,
            /** <summary>요구 프레임 마감 코드</summary> */
            EOT = 0x04,
            /** <summary>응답 프레임 마감 코드</summary> */
            ETX = 0x03
        }
    }
}