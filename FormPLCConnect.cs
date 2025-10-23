using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLCInterface;

namespace PLCInterface
{
    public partial class FormPLCConnect : Form
    {
        IPLC _plc;
        private string _plcType;
        private string _protocolType;

        private ComboBox _cbComPort = new ComboBox();
        private ComboBox _cbBaudrate = new ComboBox();
        private TextBox _tbStationNumber = new TextBox();
        private TextBox _tbIP = new TextBox();
        private TextBox _tbPort = new TextBox();

        public event EventHandler<ValueEventArgs> ValueReturned;

        public FormPLCConnect(string iPLCType, string iProtocolType)
        {
            InitializeComponent();
            _plcType = iPLCType;
            _protocolType = iProtocolType;  
            InitUI();
        }

        private void InitUI()
        {
            if (_plcType == "LS")
            {
                if (_protocolType == "Cnet")
                {
                    InitControl_Serial();
                }
                else if (_protocolType == "FEnet")
                {
                    InitControl_Etherent();
                }
            }
            else if (_plcType == "MITHUBISHI")
            {
                if (_protocolType == "MXComponent")
                {
                    InitControl_MXComponent();
                }
            }
        }

        private void InitControl_MXComponent()
        {
            label1.Text = "국번 No.";
            label2.Text = "";

            _tbStationNumber.ForeColor = SystemColors.ControlText;
            _tbStationNumber.Location = new Point(80, 10);
            _tbStationNumber.Margin = new Padding(0);
            _tbStationNumber.Name = "_tbStationNumber";
            _tbStationNumber.Size = new Size(120, 20);
            _tbStationNumber.TabIndex = 0;
            _tbStationNumber.TextAlign = HorizontalAlignment.Left;

            Controls.Add(_tbStationNumber);

            Update();
        }

        private void InitControl_Etherent()
        {
            label1.Text = "IP";
            label2.Text = "Port";

            _tbIP.ForeColor = SystemColors.ControlText;
            _tbIP.Location = new Point(80, 10);
            _tbIP.Margin = new Padding(0);
            _tbIP.Name = "_tbIP";
            _tbIP.Size = new Size(120, 20);
            _tbIP.TabIndex = 0;
            _tbIP.TextAlign = HorizontalAlignment.Left;

            _tbPort.ForeColor = SystemColors.ControlText;
            _tbPort.Location = new Point(80, 40);
            _tbPort.Margin = new Padding(0);
            _tbPort.Name = "_tbPort";
            _tbPort.Size = new Size(120, 20);
            _tbPort.TabIndex = 1;
            _tbPort.TextAlign = HorizontalAlignment.Left;

            Controls.Add(_tbIP);
            Controls.Add(_tbPort);

            Update();
        }

        private void InitControl_Serial()
        {
            label1.Text = "IP";
            label2.Text = "Port";

            _cbComPort.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbComPort.FormattingEnabled = true;
            _cbComPort.Location = new Point(80, 10);
            _cbComPort.Margin = new Padding(0);
            _cbComPort.Name = "cbPLC";
            _cbComPort.Size = new Size(120, 20);
            _cbComPort.TabIndex = 0;

            _cbBaudrate.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbBaudrate.FormattingEnabled = true;
            _cbBaudrate.Location = new Point(80, 40);
            _cbBaudrate.Margin = new Padding(0);
            _cbBaudrate.Name = "cbPLC";
            _cbBaudrate.Size = new Size(120, 20);
            _cbBaudrate.TabIndex = 1;

            Controls.Add(_cbComPort);
            Controls.Add(_cbBaudrate);

            _cbComPort.Items.Add("COM1");
            _cbComPort.Items.Add("COM2");
            _cbComPort.Items.Add("COM3");
            _cbComPort.Items.Add("COM4");
            _cbComPort.Items.Add("COM5");
            _cbComPort.Items.Add("COM6");
            _cbComPort.Items.Add("COM7");
            _cbComPort.Items.Add("COM8");
            _cbComPort.Items.Add("COM9");
            _cbComPort.Items.Add("COM10");
            _cbComPort.SelectedItem = _cbComPort.Items[0];

            _cbBaudrate.Items.Add("1200");
            _cbBaudrate.Items.Add("4800");
            _cbBaudrate.Items.Add("9600");
            _cbBaudrate.Items.Add("115200");
            _cbBaudrate.SelectedItem = _cbBaudrate.Items[0];

            Update();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_plcType == "LS")
            {
                if (_protocolType == "Cnet")
                {
                    
                }
                else if (_protocolType == "FEnet")
                {
                    
                }
            }
            else if (_plcType == "MITHUBISHI")
            {
                if (_protocolType == "MXComponent")
                {
                    if (!int.TryParse(_tbStationNumber.Text, out int stationNumber))
                    {
                        MessageBox.Show("국번 확인 필요");
                        return;
                    }
                    _plc = new MXComponent(stationNumber);
                    if (_plc.Open())
                    {
                        MessageBox.Show("연결 성공");
                        ValueReturned?.Invoke(this, new ValueEventArgs(_plc));
                        this.Close();
                    }
                    else 
                    {
                        MessageBox.Show("연결 실패");
                    }
                }
            }
        }
        public class ValueEventArgs : EventArgs
        {
            public IPLC ReturnedValue { get; }

            public ValueEventArgs(IPLC _iPLC)
            {
                ReturnedValue = _iPLC;
            }
        }
    }
}
