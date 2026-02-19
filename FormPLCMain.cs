using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLCInterface;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace PLCInterface
{
    public partial class FormPLCMain : Form
    {
        IPLC _plc;
        private BasePLCInterface _plcManager;

        private Dictionary<string, short> _heatingLineInfo;
        private string[] _deviceName = new string[11];
        private int[,] _heatingInfo = new int[10, 11];

        public FormPLCMain()
        {
            InitializeComponent();

            // 이벤트 연결
            cbPLC.SelectedIndexChanged += CbPLC_SelectedValueChanged;

            InitUI();

            InitTestInfo();
        }

        private void CbPLC_SelectedValueChanged(object sender, EventArgs e)
        {
            cbProtocol.Items.Clear();
            if (cbPLC.SelectedItem.ToString() == "LS")
            {
                cbProtocol.Items.Add("Cnet");
                cbProtocol.Items.Add("FEnet");
                cbProtocol.SelectedItem = cbProtocol.Items[0];
            }
            else if (cbPLC.SelectedItem.ToString() == "MITHUBISHI")
            {
                cbProtocol.Items.Add("MXComponent");
                cbProtocol.SelectedItem = cbProtocol.Items[0];
            }
        }

        private void InitUI()
        {
            cbPLC.Items.Clear();

            cbPLC.Items.Add("LS");
            cbPLC.Items.Add("MITHUBISHI");

            cbPLC.SelectedItem = cbPLC.Items[0];

            rbRead1.Checked = true;
            rbRead2.Checked = true;
        }

        private void InitTestInfo()
        {
            _heatingLineInfo = new Dictionary<string, short> { };
            _heatingLineInfo["D5920"] = 0;
            _heatingLineInfo["D5921"] = 0;
            _heatingLineInfo["D5922"] = 0;
            _heatingLineInfo["D5923"] = 0;
            _heatingLineInfo["D5924"] = 0;
            _heatingLineInfo["D5925"] = 0;
            _heatingLineInfo["D5926"] = 0;
            _heatingLineInfo["D5927"] = 0;
            _heatingLineInfo["D5928"] = 0;
            _heatingLineInfo["D5929"] = 0;
            _heatingLineInfo["D5930"] = 0;

            for (int i = 0; i < 10; i++)
            {
                _heatingInfo[i, 0] = 10;
                _heatingInfo[i, 1] = i + 1;
                _heatingInfo[i, 2] = 100 + i;
                _heatingInfo[i, 3] = 200 + i;
                _heatingInfo[i, 4] = 300 + i;
                _heatingInfo[i, 5] = 400 + i;
                _heatingInfo[i, 6] = 500 + i;
                _heatingInfo[i, 7] = 600 + i;
                _heatingInfo[i, 8] = 700 + i;
                _heatingInfo[i, 9] = 800 + i;
                _heatingInfo[i, 10] = 900 + i;
            }
        }

        // 값 받아서 컨트롤 업데이트 하는 함수 넣기
        public void UpdateUI(Dictionary<string, short> iPairs)
        {
            foreach (KeyValuePair<string, short> kevValue in iPairs)
            {
                string name = kevValue.Key;
                int value = kevValue.Value;
                UdpateDataGridViewRow(name, value);
            }

            dgvMonitoring.Update();
        }

        private void UdpateDataGridViewRow(string name, int value)
        {
            DataGridViewRow currentRow = null;
            // 찾기
            int idx = 0;
            foreach (DataGridViewRow row in dgvMonitoring.Rows)
            {
                if (idx == dgvMonitoring.RowCount - 1)
                    break;
                if (row.Cells[0].Value.ToString() == name)
                    currentRow = row;
                idx++;
            }
            if (currentRow == null) return;
            currentRow.Cells[1].Value = value;
            currentRow.Cells[2].Value = value.ToString("X4");
            currentRow.Cells[3].Value = Convert.ToString(value, 2).PadLeft(16, '0');
        }

        /********************************** 여기서부터 컨트롤 이벤트 함수 **********************************/
        private void btnConnectPLC_Click(object sender, EventArgs e)
        {
            if (_plc != null && _plc.IsOpen())
            {
                MessageBox.Show($"기존에 연결된 PLC가 존재: {_plc.GetType()}");
                return;
            }

            string plc = cbPLC.Text;
            string protocol = cbProtocol.Text;
            FormPLCConnect formConnect = new FormPLCConnect(plc, protocol);
            formConnect.ValueReturned += new EventHandler<FormPLCConnect.ValueEventArgs>(FormConnect_ValueReturned);

            formConnect.ShowDialog();
        }

        private void btnDisconnectPLC_Click(object sender, EventArgs e)
        {
            if (_plc == null || !_plc.IsOpen())
                return;

            if (_plcManager == null) return;

            //_plcManager.MonitoringValueChanged -= _plcMonitor_ValueChanged;

            _plc.Close();
        }

        private void FormConnect_ValueReturned(object sender, FormPLCConnect.ValueEventArgs e)
        {
            _plc = e.ReturnedValue;

            _plcManager = new BasePLCInterface();
            _plcManager.PLC = _plc;
            //_plcManager.MonitoringValueChanged += _plcMonitor_ValueChanged;
        }

        private void _plcMonitor_ValueChanged(Dictionary<string, short> pairs)
        {
            if (InvokeRequired)
            {
                // UI 스레드에서 실행되도록 Invoke 호출
                this.Invoke((Action)(() => UpdateUI(pairs)));
            }
            else
            {
                UpdateUI(pairs);
            }
        }

        private void rbRead1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRead1.Checked)
            {
                rbWrite1.Checked = false;
                rtbValue1.ReadOnly = true;
                rtbValue1.Text = "";
                btnReadWrite1.Text = "읽기";
            }
        }

        private void rbWrite1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWrite1.Checked)
            {
                rbRead1.Checked = false;
                rtbValue1.ReadOnly = false;
                rtbValue1.Text = "";
                btnReadWrite1.Text = "쓰기";
            }
        }

        private void btnReadWrite1_Click(object sender, EventArgs e)
        {
            if (_plc == null || !_plc.IsOpen())
            {
                MessageBox.Show("PLC 연결 상태 확인");
                return;
            }

            string name = rtbName1.Text;
            string value = rtbValue1.Text;

            int valueRead = 0;
            int err = 0;
            bool result = false;
            if (rbRead1.Checked)
            {
                result = _plc.ReadSingleDevice(name, out valueRead, out err);
                rtbValue1.Text = valueRead.ToString();
            }
            else if (rbWrite1.Checked)
            {
                if (!int.TryParse(value, out int valueWrite))
                {
                    MessageBox.Show("잘못된 쓰기 값");
                    return;
                }
                result = _plc.WriteSingleDevice(name, valueWrite, out err);
            }

            if (!result) MessageBox.Show("PLC 응답 없음");
        }

        private void rbRead2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRead2.Checked)
            {
                rbWrite2.Checked = false;
                btnReadWrite2.Text = "읽기";

                foreach (DataGridViewRow row in dgvMultiReadWrite.Rows)
                {
                    row.Cells[1].Value = null;
                }
                dgvMultiReadWrite.Columns[1].ReadOnly = true;
            }
        }

        private void rbWrite2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWrite2.Checked)
            {
                rbRead2.Checked = false;
                btnReadWrite2.Text = "쓰기";

                foreach (DataGridViewRow row in dgvMultiReadWrite.Rows)
                {
                    row.Cells[1].Value = null;
                }
                dgvMultiReadWrite.Columns[1].ReadOnly = false;
            }
        }

        private void btnReadWrite2_Click(object sender, EventArgs e)
        {
            if (_plc == null || !_plc.IsOpen())
            {
                MessageBox.Show("PLC 연결 상태 확인");
                return;
            }

            // 요청 보내기
            int count = dgvMultiReadWrite.RowCount - 1;
            string[] name = new string[count];
            int[] value = new int[count];
            int err = 0;
            bool result = false;

            int idx = 0;
            if (rbRead2.Checked)
            {
                idx = 0;
                foreach (DataGridViewRow row in dgvMultiReadWrite.Rows)
                {
                    if (idx == count) break;
                    name[idx++] = row.Cells[0].Value.ToString();
                }
                result = _plc.ReadIndividualDevices(name, out value, out err);
                idx = 0;
                foreach (DataGridViewRow row in dgvMultiReadWrite.Rows)
                {
                    if (idx == count) break;
                    row.Cells[1].Value = value[idx++];
                }
            }
            else if (rbWrite2.Checked)
            {
                idx = 0;
                foreach (DataGridViewRow row in dgvMultiReadWrite.Rows)
                {
                    if (idx == count) break;
                    name[idx] = row.Cells[0].Value.ToString();
                    if (!int.TryParse(row.Cells[1].Value.ToString(), out value[idx]))
                    {
                        MessageBox.Show($"잘못된 쓰기 값: {idx + 1} 번째 줄");
                        return;
                    }
                    idx++;
                }
                result = _plc.WriteIndividualDevices(name, value, out err);
                idx = 0;
            }

            if (!result) MessageBox.Show("PLC 응답 없음");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMultiReadWrite.SelectedRows.Count == 0) return;

            List<int> rowIndex = new List<int>();
            foreach (DataGridViewRow row in dgvMultiReadWrite.SelectedRows)
            {
                rowIndex.Add(row.Index);
            }

            foreach (int i in rowIndex)
            {
                if (i >= dgvMultiReadWrite.RowCount - 1)
                    continue;
                dgvMultiReadWrite.Rows.RemoveAt(i);
            }

            this.Update();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dgvMultiReadWrite.Rows.Clear();
            this.Update();
        }

        private void btnMonitoringStart_Click(object sender, EventArgs e)
        {
            if (_plc == null || !_plc.IsOpen())
            {
                MessageBox.Show("PLC 연결 상태 확인");
                return;
            }

            Dictionary<string, short> pairs = new Dictionary<string, short>();

            int count = dgvMonitoring.RowCount - 1;

            int idx = 0;
            foreach (DataGridViewRow row in dgvMonitoring.Rows)
            {
                if (idx == count) break;
                pairs[row.Cells[0].Value.ToString()] = 0;
                idx++;
            }

            //_plcManager.SetMonitoringDeviceList(pairs);
            _plcManager.MonitoringStart();

            dgvMonitoring.ReadOnly = true;
        }

        private void btnMonitoringStop_Click(object sender, EventArgs e)
        {
            _plcManager.MonitoringStop();
            dgvMonitoring.ReadOnly = false;
        }

        private void btnDelete3_Click(object sender, EventArgs e)
        {
            if (dgvMonitoring.SelectedRows.Count == 0) return;

            List<int> rowIndex = new List<int>();
            foreach (DataGridViewRow row in dgvMonitoring.SelectedRows)
            {
                rowIndex.Add(row.Index);
            }

            foreach (int i in rowIndex)
            {
                if (i >= dgvMonitoring.RowCount - 1)
                    continue;
                dgvMonitoring.Rows.RemoveAt(i);
            }

            this.Update();
        }

        private void btnReset3_Click(object sender, EventArgs e)
        {
            dgvMonitoring.Rows.Clear();
            this.Update();
        }

        private void btnWriteHeatLineInfo_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(ThreadFunctionWriteHeatLines);
            thread.Start();
        }

        private void btnResetHeatLineInfo_Click(object sender, EventArgs e)
        {
            string[] names = new string[11];
            int[] values = new int[11];
            for (int j = 0; j < 11; j++)
            {
                names[j] = $"D{5920 + j}";
                values[j] = 0;
            }

            _plc.WriteContinuousDevices("D5920", 1080, new int[1080], out int err);
        }

        private void ThreadFunctionWriteHeatLines(object sender)
        {
            _plc.WriteSingleDevice("D5900", 2, out int err1);

            for (int i = 0; i < 10; i++)
            {
                string[] names = new string[11];
                int[] values = new int[11];

                for (int j = 0; j < 11; j++)
                {
                    names[j] = $"D{5920 + j}";
                    values[j] = _heatingInfo[i, j];
                }

                _plc.WriteIndividualDevices(names, values, out int err2);

                Thread.Sleep(1000);
            }

            _plc.WriteSingleDevice("D5900", 0, out int err3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _plc.ReadContinuousDevices("D7060", 14, out int[] values, out int eer);
            double[] pose = new double[7];
            for (int i = 0; i < 7; i++)
            {
                short low = (short)values[2 * i];
                short high = (short)values[2 * i + 1];

                int value1000 = ((high & 0xFFFF) << 16) | (low & 0xFFFF);
                pose[i] = (double)value1000 / 1000.0;
            }
            richTextBox1.Text = $"{pose[0]:f3},{pose[1]:f3},{pose[2]:f3},{pose[3]:f3},{pose[4]:f3},{pose[5]:f3}";
        }
    }
}
