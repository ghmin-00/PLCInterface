
namespace PLCInterface
{
    partial class FormPLCMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDisconnectPLC = new System.Windows.Forms.Button();
            this.btnConnectPLC = new System.Windows.Forms.Button();
            this.cbProtocol = new System.Windows.Forms.ComboBox();
            this.cbPLC = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnReadWrite1 = new System.Windows.Forms.Button();
            this.rbWrite1 = new System.Windows.Forms.RadioButton();
            this.rbRead1 = new System.Windows.Forms.RadioButton();
            this.rtbName1 = new System.Windows.Forms.RichTextBox();
            this.rtbValue1 = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDelete2 = new System.Windows.Forms.Button();
            this.dgvMultiReadWrite = new System.Windows.Forms.DataGridView();
            this.colName2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReset2 = new System.Windows.Forms.Button();
            this.btnReadWrite2 = new System.Windows.Forms.Button();
            this.rbWrite2 = new System.Windows.Forms.RadioButton();
            this.rbRead2 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnResetHeatLineInfo = new System.Windows.Forms.Button();
            this.btnWriteHeatLineInfo = new System.Windows.Forms.Button();
            this.btnMonitoringStop = new System.Windows.Forms.Button();
            this.btnDelete3 = new System.Windows.Forms.Button();
            this.btnReset3 = new System.Windows.Forms.Button();
            this.btnMonitoringStart = new System.Windows.Forms.Button();
            this.dgvMonitoring = new System.Windows.Forms.DataGridView();
            this.colName3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue3Dec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue3Hex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue3Bin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiReadWrite)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonitoring)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDisconnectPLC);
            this.groupBox1.Controls.Add(this.btnConnectPLC);
            this.groupBox1.Controls.Add(this.cbProtocol);
            this.groupBox1.Controls.Add(this.cbPLC);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10, 11, 10, 10);
            this.groupBox1.Size = new System.Drawing.Size(320, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PLC 연결";
            // 
            // btnDisconnectPLC
            // 
            this.btnDisconnectPLC.Location = new System.Drawing.Point(210, 55);
            this.btnDisconnectPLC.Margin = new System.Windows.Forms.Padding(0);
            this.btnDisconnectPLC.Name = "btnDisconnectPLC";
            this.btnDisconnectPLC.Size = new System.Drawing.Size(100, 20);
            this.btnDisconnectPLC.TabIndex = 5;
            this.btnDisconnectPLC.Text = "PLC 끊기";
            this.btnDisconnectPLC.UseVisualStyleBackColor = true;
            this.btnDisconnectPLC.Click += new System.EventHandler(this.btnDisconnectPLC_Click);
            // 
            // btnConnectPLC
            // 
            this.btnConnectPLC.Location = new System.Drawing.Point(210, 25);
            this.btnConnectPLC.Margin = new System.Windows.Forms.Padding(0);
            this.btnConnectPLC.Name = "btnConnectPLC";
            this.btnConnectPLC.Size = new System.Drawing.Size(100, 20);
            this.btnConnectPLC.TabIndex = 4;
            this.btnConnectPLC.Text = "PLC 연결";
            this.btnConnectPLC.UseVisualStyleBackColor = true;
            this.btnConnectPLC.Click += new System.EventHandler(this.btnConnectPLC_Click);
            // 
            // cbProtocol
            // 
            this.cbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProtocol.FormattingEnabled = true;
            this.cbProtocol.Location = new System.Drawing.Point(80, 55);
            this.cbProtocol.Margin = new System.Windows.Forms.Padding(0);
            this.cbProtocol.Name = "cbProtocol";
            this.cbProtocol.Size = new System.Drawing.Size(120, 20);
            this.cbProtocol.TabIndex = 3;
            // 
            // cbPLC
            // 
            this.cbPLC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPLC.FormattingEnabled = true;
            this.cbPLC.Location = new System.Drawing.Point(80, 25);
            this.cbPLC.Margin = new System.Windows.Forms.Padding(0);
            this.cbPLC.Name = "cbPLC";
            this.cbPLC.Size = new System.Drawing.Size(120, 20);
            this.cbPLC.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(10, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "프로토콜";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "PLC 종류";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnReadWrite1);
            this.groupBox2.Controls.Add(this.rbWrite1);
            this.groupBox2.Controls.Add(this.rbRead1);
            this.groupBox2.Controls.Add(this.rtbName1);
            this.groupBox2.Controls.Add(this.rtbValue1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(10, 105);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10, 11, 10, 10);
            this.groupBox2.Size = new System.Drawing.Size(320, 85);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "단일 디바이스/레지스터 읽기/쓰기";
            // 
            // btnReadWrite1
            // 
            this.btnReadWrite1.Location = new System.Drawing.Point(210, 55);
            this.btnReadWrite1.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadWrite1.Name = "btnReadWrite1";
            this.btnReadWrite1.Size = new System.Drawing.Size(100, 20);
            this.btnReadWrite1.TabIndex = 8;
            this.btnReadWrite1.Text = "읽기";
            this.btnReadWrite1.UseVisualStyleBackColor = true;
            this.btnReadWrite1.Click += new System.EventHandler(this.btnReadWrite1_Click);
            // 
            // rbWrite1
            // 
            this.rbWrite1.AutoSize = true;
            this.rbWrite1.Location = new System.Drawing.Point(260, 27);
            this.rbWrite1.Name = "rbWrite1";
            this.rbWrite1.Size = new System.Drawing.Size(33, 16);
            this.rbWrite1.TabIndex = 7;
            this.rbWrite1.TabStop = true;
            this.rbWrite1.Text = "W";
            this.rbWrite1.UseVisualStyleBackColor = true;
            this.rbWrite1.CheckedChanged += new System.EventHandler(this.rbWrite1_CheckedChanged);
            // 
            // rbRead1
            // 
            this.rbRead1.AutoSize = true;
            this.rbRead1.Location = new System.Drawing.Point(215, 27);
            this.rbRead1.Name = "rbRead1";
            this.rbRead1.Size = new System.Drawing.Size(31, 16);
            this.rbRead1.TabIndex = 6;
            this.rbRead1.TabStop = true;
            this.rbRead1.Text = "R";
            this.rbRead1.UseVisualStyleBackColor = true;
            this.rbRead1.CheckedChanged += new System.EventHandler(this.rbRead1_CheckedChanged);
            // 
            // rtbName1
            // 
            this.rtbName1.Location = new System.Drawing.Point(80, 25);
            this.rtbName1.Margin = new System.Windows.Forms.Padding(0);
            this.rtbName1.Multiline = false;
            this.rtbName1.Name = "rtbName1";
            this.rtbName1.Size = new System.Drawing.Size(120, 20);
            this.rtbName1.TabIndex = 5;
            this.rtbName1.Text = "";
            // 
            // rtbValue1
            // 
            this.rtbValue1.BackColor = System.Drawing.SystemColors.Window;
            this.rtbValue1.Location = new System.Drawing.Point(80, 55);
            this.rtbValue1.Margin = new System.Windows.Forms.Padding(0);
            this.rtbValue1.Multiline = false;
            this.rtbValue1.Name = "rtbValue1";
            this.rtbValue1.Size = new System.Drawing.Size(120, 20);
            this.rtbValue1.TabIndex = 4;
            this.rtbValue1.Text = "";
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(10, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "주소 값";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(10, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "주소 이름";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDelete2);
            this.groupBox3.Controls.Add(this.dgvMultiReadWrite);
            this.groupBox3.Controls.Add(this.btnReset2);
            this.groupBox3.Controls.Add(this.btnReadWrite2);
            this.groupBox3.Controls.Add(this.rbWrite2);
            this.groupBox3.Controls.Add(this.rbRead2);
            this.groupBox3.Location = new System.Drawing.Point(340, 10);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(10, 11, 10, 10);
            this.groupBox3.Size = new System.Drawing.Size(320, 180);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "멀티 디바이스/레지스터 읽기/쓰기";
            // 
            // btnDelete2
            // 
            this.btnDelete2.Location = new System.Drawing.Point(210, 118);
            this.btnDelete2.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelete2.Name = "btnDelete2";
            this.btnDelete2.Size = new System.Drawing.Size(100, 20);
            this.btnDelete2.TabIndex = 13;
            this.btnDelete2.Text = "지우기";
            this.btnDelete2.UseVisualStyleBackColor = true;
            this.btnDelete2.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvMultiReadWrite
            // 
            this.dgvMultiReadWrite.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMultiReadWrite.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName2,
            this.colValue2});
            this.dgvMultiReadWrite.Location = new System.Drawing.Point(10, 25);
            this.dgvMultiReadWrite.Margin = new System.Windows.Forms.Padding(0);
            this.dgvMultiReadWrite.Name = "dgvMultiReadWrite";
            this.dgvMultiReadWrite.RowHeadersWidth = 20;
            this.dgvMultiReadWrite.RowTemplate.Height = 23;
            this.dgvMultiReadWrite.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMultiReadWrite.Size = new System.Drawing.Size(190, 145);
            this.dgvMultiReadWrite.TabIndex = 12;
            // 
            // colName2
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.colName2.DefaultCellStyle = dataGridViewCellStyle3;
            this.colName2.Frozen = true;
            this.colName2.HeaderText = "이름";
            this.colName2.Name = "colName2";
            this.colName2.Width = 75;
            // 
            // colValue2
            // 
            this.colValue2.Frozen = true;
            this.colValue2.HeaderText = "값";
            this.colValue2.Name = "colValue2";
            // 
            // btnReset2
            // 
            this.btnReset2.Location = new System.Drawing.Point(210, 148);
            this.btnReset2.Margin = new System.Windows.Forms.Padding(0);
            this.btnReset2.Name = "btnReset2";
            this.btnReset2.Size = new System.Drawing.Size(100, 20);
            this.btnReset2.TabIndex = 11;
            this.btnReset2.Text = "리셋";
            this.btnReset2.UseVisualStyleBackColor = true;
            this.btnReset2.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnReadWrite2
            // 
            this.btnReadWrite2.Location = new System.Drawing.Point(210, 55);
            this.btnReadWrite2.Margin = new System.Windows.Forms.Padding(0);
            this.btnReadWrite2.Name = "btnReadWrite2";
            this.btnReadWrite2.Size = new System.Drawing.Size(100, 20);
            this.btnReadWrite2.TabIndex = 11;
            this.btnReadWrite2.Text = "읽기";
            this.btnReadWrite2.UseVisualStyleBackColor = true;
            this.btnReadWrite2.Click += new System.EventHandler(this.btnReadWrite2_Click);
            // 
            // rbWrite2
            // 
            this.rbWrite2.AutoSize = true;
            this.rbWrite2.Location = new System.Drawing.Point(260, 27);
            this.rbWrite2.Name = "rbWrite2";
            this.rbWrite2.Size = new System.Drawing.Size(33, 16);
            this.rbWrite2.TabIndex = 10;
            this.rbWrite2.TabStop = true;
            this.rbWrite2.Text = "W";
            this.rbWrite2.UseVisualStyleBackColor = true;
            this.rbWrite2.CheckedChanged += new System.EventHandler(this.rbWrite2_CheckedChanged);
            // 
            // rbRead2
            // 
            this.rbRead2.AutoSize = true;
            this.rbRead2.Location = new System.Drawing.Point(215, 27);
            this.rbRead2.Name = "rbRead2";
            this.rbRead2.Size = new System.Drawing.Size(31, 16);
            this.rbRead2.TabIndex = 9;
            this.rbRead2.TabStop = true;
            this.rbRead2.Text = "R";
            this.rbRead2.UseVisualStyleBackColor = true;
            this.rbRead2.CheckedChanged += new System.EventHandler(this.rbRead2_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnResetHeatLineInfo);
            this.groupBox4.Controls.Add(this.btnWriteHeatLineInfo);
            this.groupBox4.Controls.Add(this.btnMonitoringStop);
            this.groupBox4.Controls.Add(this.btnDelete3);
            this.groupBox4.Controls.Add(this.btnReset3);
            this.groupBox4.Controls.Add(this.btnMonitoringStart);
            this.groupBox4.Controls.Add(this.dgvMonitoring);
            this.groupBox4.Location = new System.Drawing.Point(10, 200);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox4.MaximumSize = new System.Drawing.Size(650, 235);
            this.groupBox4.MinimumSize = new System.Drawing.Size(650, 235);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(10, 11, 10, 10);
            this.groupBox4.Size = new System.Drawing.Size(650, 235);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "디바이스/레지스터 모니터링";
            // 
            // btnResetHeatLineInfo
            // 
            this.btnResetHeatLineInfo.Location = new System.Drawing.Point(540, 123);
            this.btnResetHeatLineInfo.Margin = new System.Windows.Forms.Padding(0);
            this.btnResetHeatLineInfo.Name = "btnResetHeatLineInfo";
            this.btnResetHeatLineInfo.Size = new System.Drawing.Size(100, 20);
            this.btnResetHeatLineInfo.TabIndex = 19;
            this.btnResetHeatLineInfo.Text = "가열선 리셋";
            this.btnResetHeatLineInfo.UseVisualStyleBackColor = true;
            this.btnResetHeatLineInfo.Click += new System.EventHandler(this.btnResetHeatLineInfo_Click);
            // 
            // btnWriteHeatLineInfo
            // 
            this.btnWriteHeatLineInfo.Location = new System.Drawing.Point(540, 88);
            this.btnWriteHeatLineInfo.Margin = new System.Windows.Forms.Padding(0);
            this.btnWriteHeatLineInfo.Name = "btnWriteHeatLineInfo";
            this.btnWriteHeatLineInfo.Size = new System.Drawing.Size(100, 20);
            this.btnWriteHeatLineInfo.TabIndex = 18;
            this.btnWriteHeatLineInfo.Text = "가열선 쓰기";
            this.btnWriteHeatLineInfo.UseVisualStyleBackColor = true;
            this.btnWriteHeatLineInfo.Click += new System.EventHandler(this.btnWriteHeatLineInfo_Click);
            // 
            // btnMonitoringStop
            // 
            this.btnMonitoringStop.Location = new System.Drawing.Point(540, 55);
            this.btnMonitoringStop.Margin = new System.Windows.Forms.Padding(0);
            this.btnMonitoringStop.Name = "btnMonitoringStop";
            this.btnMonitoringStop.Size = new System.Drawing.Size(100, 20);
            this.btnMonitoringStop.TabIndex = 17;
            this.btnMonitoringStop.Text = "종료";
            this.btnMonitoringStop.UseVisualStyleBackColor = true;
            this.btnMonitoringStop.Click += new System.EventHandler(this.btnMonitoringStop_Click);
            // 
            // btnDelete3
            // 
            this.btnDelete3.Location = new System.Drawing.Point(540, 175);
            this.btnDelete3.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelete3.Name = "btnDelete3";
            this.btnDelete3.Size = new System.Drawing.Size(100, 20);
            this.btnDelete3.TabIndex = 16;
            this.btnDelete3.Text = "지우기";
            this.btnDelete3.UseVisualStyleBackColor = true;
            this.btnDelete3.Click += new System.EventHandler(this.btnDelete3_Click);
            // 
            // btnReset3
            // 
            this.btnReset3.Location = new System.Drawing.Point(540, 205);
            this.btnReset3.Margin = new System.Windows.Forms.Padding(0);
            this.btnReset3.Name = "btnReset3";
            this.btnReset3.Size = new System.Drawing.Size(100, 20);
            this.btnReset3.TabIndex = 15;
            this.btnReset3.Text = "리셋";
            this.btnReset3.UseVisualStyleBackColor = true;
            this.btnReset3.Click += new System.EventHandler(this.btnReset3_Click);
            // 
            // btnMonitoringStart
            // 
            this.btnMonitoringStart.Location = new System.Drawing.Point(540, 25);
            this.btnMonitoringStart.Margin = new System.Windows.Forms.Padding(0);
            this.btnMonitoringStart.Name = "btnMonitoringStart";
            this.btnMonitoringStart.Size = new System.Drawing.Size(100, 20);
            this.btnMonitoringStart.TabIndex = 14;
            this.btnMonitoringStart.Text = "시작";
            this.btnMonitoringStart.UseVisualStyleBackColor = true;
            this.btnMonitoringStart.Click += new System.EventHandler(this.btnMonitoringStart_Click);
            // 
            // dgvMonitoring
            // 
            this.dgvMonitoring.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMonitoring.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName3,
            this.colValue3Dec,
            this.colValue3Hex,
            this.colValue3Bin});
            this.dgvMonitoring.Location = new System.Drawing.Point(10, 25);
            this.dgvMonitoring.Margin = new System.Windows.Forms.Padding(0);
            this.dgvMonitoring.Name = "dgvMonitoring";
            this.dgvMonitoring.RowHeadersWidth = 20;
            this.dgvMonitoring.RowTemplate.Height = 23;
            this.dgvMonitoring.Size = new System.Drawing.Size(520, 200);
            this.dgvMonitoring.TabIndex = 13;
            // 
            // colName3
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.colName3.DefaultCellStyle = dataGridViewCellStyle4;
            this.colName3.Frozen = true;
            this.colName3.HeaderText = "이름";
            this.colName3.Name = "colName3";
            this.colName3.Width = 75;
            // 
            // colValue3Dec
            // 
            this.colValue3Dec.Frozen = true;
            this.colValue3Dec.HeaderText = "값(Dec)";
            this.colValue3Dec.Name = "colValue3Dec";
            this.colValue3Dec.ReadOnly = true;
            // 
            // colValue3Hex
            // 
            this.colValue3Hex.Frozen = true;
            this.colValue3Hex.HeaderText = "값(Hex)";
            this.colValue3Hex.Name = "colValue3Hex";
            this.colValue3Hex.ReadOnly = true;
            // 
            // colValue3Bin
            // 
            this.colValue3Bin.Frozen = true;
            this.colValue3Bin.HeaderText = "값(Bin)";
            this.colValue3Bin.Name = "colValue3Bin";
            this.colValue3Bin.ReadOnly = true;
            this.colValue3Bin.Width = 250;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.Location = new System.Drawing.Point(699, 226);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox1.Multiline = false;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(303, 20);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1013, 225);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 20);
            this.button1.TabIndex = 9;
            this.button1.Text = "읽기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormPLCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 446);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormPLCMain";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Remote PLC";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiReadWrite)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonitoring)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnectPLC;
        private System.Windows.Forms.ComboBox cbProtocol;
        private System.Windows.Forms.ComboBox cbPLC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox rtbValue1;
        private System.Windows.Forms.RichTextBox rtbName1;
        private System.Windows.Forms.RadioButton rbRead1;
        private System.Windows.Forms.RadioButton rbWrite1;
        private System.Windows.Forms.Button btnReadWrite1;
        private System.Windows.Forms.DataGridView dgvMultiReadWrite;
        private System.Windows.Forms.Button btnReset2;
        private System.Windows.Forms.Button btnReadWrite2;
        private System.Windows.Forms.RadioButton rbWrite2;
        private System.Windows.Forms.RadioButton rbRead2;
        private System.Windows.Forms.Button btnDelete2;
        private System.Windows.Forms.DataGridView dgvMonitoring;
        private System.Windows.Forms.Button btnMonitoringStop;
        private System.Windows.Forms.Button btnDelete3;
        private System.Windows.Forms.Button btnReset3;
        private System.Windows.Forms.Button btnMonitoringStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue3Dec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue3Hex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue3Bin;
        private System.Windows.Forms.Button btnDisconnectPLC;
        private System.Windows.Forms.Button btnResetHeatLineInfo;
        private System.Windows.Forms.Button btnWriteHeatLineInfo;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
    }
}

