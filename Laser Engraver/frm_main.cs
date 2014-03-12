using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
namespace Laser_Engraver
{
    public enum eMode { CONNECTED, DISCONNECTED, RUNNING, FEEDHOLD, CYCLESTART, FINISHED, ABORTED, WAITING, READY, LOADING, SOFTRESET, INACTIVE };

    public partial class frm_main : Form
    {
        public eMode currentMode;
        private eMode specialMode;
        private Bitmap target_img;
        private System.Timers.Timer TXLEDoff;
        private System.Timers.Timer RXLEDoff;
        private System.Timers.Timer PortListUpdate;
        private volatile bool waitingOnACK;
        private volatile bool cancelled;

        private bool toolchange;
        private bool feedHold;
        private bool gettingSettings;
        private bool statusUpdates;
        private bool useGrblOnly;
        private bool GrblReportsInches;
        private bool _keepReading;

        private Stopwatch sw;
        private string executingLine;
        private List<string> Settings;

        Thread _readThread;

        public delegate void TransmitLEDDelegate();
        TransmitLEDDelegate TX_LED;

        public delegate void ReceiveLEDDelegate();
        ReceiveLEDDelegate RX_LED;
        public delegate void UpdateConsoleDelegate(string str);
        UpdateConsoleDelegate UpdateConsole;
        public delegate void ThreadFinishActionsDelegate();
        ThreadFinishActionsDelegate FinishActions;

        public delegate void UpdateGUIThreadDelegate(int i);
        UpdateGUIThreadDelegate UpdateGUIAction;

        public delegate void UpdatePositionLEDSDelegate(string str);
        UpdatePositionLEDSDelegate UpdatePositionLEDSAction;
        public frm_main()
        {
            InitializeComponent();
            setMode(eMode.DISCONNECTED);

            TX_LED = new TransmitLEDDelegate(TransmitLED);
            RX_LED = new ReceiveLEDDelegate(ReceiveLED);
            TXLEDoff = new System.Timers.Timer(50);
            TXLEDoff.Elapsed += TXLEDoffElapsed;
            RXLEDoff = new System.Timers.Timer(50);
            RXLEDoff.Elapsed += RXLEDoffElapsed;
            Settings = new List<string>();
            UpdateConsole = new UpdateConsoleDelegate(UpdateConsoleListBox);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getSerialPortList();

        }
        //串口连接
        //---------------------
        #region 串口方法

        private void StartReading()
        {
            if (!_keepReading)
            {
                _keepReading = true;
                _readThread = new Thread(ReadPort);
                _readThread.Start();
            }
        }

        private void StopReading()
        {
            if (_keepReading)
            {
                _keepReading = false;
                _readThread.Join();	//block until exits
                _readThread = null;
            }
        }

        private void ReadPort()
        {
            while (_keepReading)
            {
                if (comPort.IsOpen)
                {
                    //Invoke(UpdateConsole, "Reading...");
                    //byte[] readBuffer = new byte[comPort.ReadBufferSize + 1];
                    //try
                    //{
                    //    // If there are bytes available on the serial port,
                    //    // Read returns up to "count" bytes, but will not block (wait)
                    //    // for the remaining bytes. If there are no bytes available
                    //    // on the serial port, Read will block until at least one byte
                    //    // is available on the port, up until the ReadTimeout milliseconds
                    //    // have elapsed, at which time a TimeoutException will be thrown.
                    //    int count = comPort.Read(readBuffer, 0, comPort.ReadBufferSize);
                    //    String SerialIn = System.Text.Encoding.ASCII.GetString(readBuffer, 0, count);
                    try
                    {

                        while (comPort.BytesToRead > 0)
                        {
                            string SerialIn = comPort.ReadLine();
                            Invoke(UpdateConsole, SerialIn);
                            Invoke(RX_LED);
                        }
                    }
                    catch (TimeoutException) { }
                }
                else
                {
                    TimeSpan waitTime = new TimeSpan(0, 0, 0, 0, 50);
                    Thread.Sleep(waitTime);
                }
            }
        }

        public void Send(string data)
        {
            if (comPort.IsOpen)
            {
                Thread.Sleep(50);
                Invoke(TX_LED);
                comPort.Write(data + "\n");
            }
        }
        /// <summary>
        /// 获取串口列表
        /// </summary>
        private void getSerialPortList()
        {
            comList.Items.Clear();
            comList.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if (comList.Items.Count == 0)
            {
                comList.Items.Add("NOPORTS");
            }
            // choose first available index
            comList.SelectedIndex = 0;

        }
        private void connect()
        {
            if (comList.Text == "NOPORTS")
            {
                MessageBox.Show(
                    "没有任何设备连接此计算机.\n\n" +
                    "1. Connect Grbl controller\n" +
                    "2. Wait a few seconds\n" +
                    "3. Press 'OK'\n" +
                    "4. Choose COM port in dropdown box in statusbar\n" +
                    "5. Retry connect\n",
                    "Serial",
                    MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                getSerialPortList();
                return;
            }

            comPort.PortName = comList.Text;
            comPort.BaudRate = 9600;
            comPort.DtrEnable = false;
            comPort.NewLine = "\n";
            try
            {
                // open port, prod for a reponse within 500 ms
                comPort.Open();
                StartReading();
                comPort.ReadTimeout = 1000;
                Send(" ");
                setMode(eMode.CONNECTED);
                //comPort.ReadTimeout = -1;
                
            }

            catch (IOException)
            {
                setMode(eMode.DISCONNECTED);
                MessageBox.Show(String.Format("{0} does not exist", comPort.PortName),
                                "Serial Port",
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);


            }
            catch (UnauthorizedAccessException)
            {
                setMode(eMode.DISCONNECTED);
                MessageBox.Show(String.Format("{0} already in use", comPort.PortName),
                               "Serial Port",
                               MessageBoxButtons.OK, MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            }

        }

        private void disconnect()
        {

            comPort.Close();
            setMode(eMode.DISCONNECTED);

        }
        private void clearSerialBuffers()
        {
            if (comPort.IsOpen)
            {
                comPort.DiscardInBuffer();
                comPort.DiscardOutBuffer();
                comPort.Close();
            }
        }

        private void WriteSerial(string cmd)
        {
            if (comPort.IsOpen)
            {
                comPort.Write(cmd);
            }
        }
        private void waitForReset()
        {
            setMode(eMode.WAITING);

            // delay for bootloader timeout
            for (int i = 3; i > 0; i--)
            {
                lb_connectStatu.Text =
                    string.Format("等待重启 ...{0}秒", i);
                Application.DoEvents();
                Thread.Sleep(1000);
            }

            setMode(eMode.READY);
        }
        private void ComPortDataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string ACK = string.Empty;

            // status interval timer and ThreadedCommunication each will trigger this
            // only allow one at a time 
            lock (this)
            {
                // empty buffer by reading all received lines
                while (comPort.BytesToRead > 0)
                {

                    if (cancelled == true)
                    {
                        return;
                    }

                    if (comPort.IsOpen)
                        ACK = comPort.ReadLine();

                    // test cases for responses back from GRbl
                    Invoke(UpdateConsole, ACK);

                    // normal response
                    if (ACK.ToUpper().Trim() == "OK")
                    {
                        // strobe RX LED (only on affirm ACKs, not status queries)
                        Invoke(RX_LED);
                        if (specialMode == eMode.FEEDHOLD)
                        {
                            // swallow the first OK sent by the command on resume
                            waitingOnACK = true;
                        }
                        else
                        {
                            waitingOnACK = false;
                        }
                    }
                    // status update
                    else if (ACK.ToUpper().StartsWith("MPOS"))
                    {
                        // show the machine/world position on 7 segment displays
                        Invoke(UpdatePositionLEDSAction, ACK);
                    }
                    else if (ACK.StartsWith("'$x=value'"))
                    {
                        // break out of loop getting setting values
                        gettingSettings = false;
                    }
                    // response to a setting query
                    else if (ACK.StartsWith("$"))
                    {
                        // accumulate responses
                        // add to list in GetSettings()
                        Settings.Add(ACK);
                    }
                    // tool change
                    else if (toolchange)
                    {
                        executingLine = "Manual tool change :\n" + executingLine;

                        MessageBox.Show(executingLine, "Manual intervention required",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button1,
                                        MessageBoxOptions.DefaultDesktopOnly);

                        waitingOnACK = false;
                    }
                    // Grbl unsupported statement or error
                    else if (ACK.ToUpper().StartsWith("ERROR"))
                    {
                        executingLine = "Unknown or unsupported gcode execution attempt: " + executingLine;
                        executingLine += "\nDo you want to ABORT this run?";

                        DialogResult res = MessageBox.Show(executingLine, ACK,
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Error,
                                            MessageBoxDefaultButton.Button2,
                                            MessageBoxOptions.DefaultDesktopOnly);
                        if (res == DialogResult.Yes)
                        {
                            cancelled = true;
                        }
                        waitingOnACK = false;
                    }
                }
                Application.DoEvents();
            }
        }
        #endregion
        //状态
        #region 系统状态
        private void setMode(eMode newMode)
        {
            switch (newMode)
            {
                case eMode.CONNECTED:
                    currentMode = eMode.CONNECTED;
                    //pnlControl.Enabled = true;
                    //btnConnect.Enabled = false;
                    //cbxComPort.Enabled = false;
                    //customPanel1.Enabled = true;
                    //tabControl1.Enabled = true;
                    //cbxComPort.Enabled = false;
                    //btnConnect.BackColor = System.Drawing.Color.LightGreen;
                    //btnDisconnect.BackColor = System.Drawing.Color.Coral;
                    //btnDisconnect.Enabled = true;
                    //btnMDIExecute.Enabled = true;
                    //btnZminus.Enabled = true;
                    //btnZplus.Enabled = true;
                    //btnYminus.Enabled = true;
                    //btnYplus.Enabled = true;
                    //btnXminus.Enabled = true;
                    //btnXplus.Enabled = true;
                    //btnReset.Enabled = true;
                    //btnRun.Enabled = true;
                    //lblMode.BackColor = System.Drawing.Color.LightGreen;
                    lb_connectStatu.Text = "已连接";
                    btn_connect.Text = "断开";
                    //Cursor = Cursors.Default;
                    break;
                case eMode.DISCONNECTED:
                    currentMode = eMode.DISCONNECTED;
                    //pnlControl.Enabled = true;
                    //btnDisconnect.Enabled = false;
                    //btnDisconnect.BackColor = System.Drawing.Color.DarkGray;
                    //btnConnect.Enabled = true;
                    //btnConnect.BackColor = System.Drawing.Color.DarkGray;
                    //btnRun.Enabled = false;
                    //btnMDIExecute.Enabled = false;
                    //btnZminus.Enabled = false;
                    //btnZplus.Enabled = false;
                    //btnYminus.Enabled = false;
                    //btnYplus.Enabled = false;
                    //btnXminus.Enabled = false;
                    //btnXplus.Enabled = false;
                    //btnReset.Enabled = false;
                    //btnZeroAll.Enabled = false;
                    //btnZeroX.Enabled = false;
                    //btnZeroY.Enabled = false;
                    //btnZeroZ.Enabled = false;
                    //cbxComPort.Enabled = true;
                    //lblMode.BackColor = System.Drawing.Color.Khaki;
                    lb_connectStatu.Text = "未连接";
                    btn_connect.Text = "连接";
                    //Cursor = Cursors.Default;
                    break;
                case eMode.RUNNING:
                    //listBoxGcode.SelectedIndex = 0;
                    //currentMode = eMode.RUNNING;
                    //specialMode = eMode.CYCLESTART;
                    //Cursor = Cursors.AppStarting;
                    //workThread = new Thread(ThreadedCommunication);
                    //Progress.Minimum = 0;
                    //Progress.Maximum = gcode.Count;
                    //btnLoad.Enabled = false;
                    //btnDisconnect.Enabled = false;
                    //lblMode.BackColor = System.Drawing.Color.Gainsboro;
                    //lblMode.Text = "RUNNING";
                    //btnFeedHold.BackColor = System.Drawing.Color.Khaki;
                    //btnFeedHold.Text = "Feed Hold";
                    //feedHold = false;
                    //btnReset.Enabled = false;
                    //btnZeroAll.Enabled = false;
                    //btnZeroX.Enabled = false;
                    //btnZeroY.Enabled = false;
                    //btnZeroZ.Enabled = false;
                    //btnFeedHold.Enabled = true;
                    //btnCancel.Enabled = true;
                    //btnRun.Enabled = false;
                    //lblElapsedTime.Text = "00:00:00";
                    //waitingOnACK = true;
                    //workThread.Start();
                    //sw.Reset();
                    //sw.Start();
                    //timerStatusQuery.Enabled = true;
                    break;
                case eMode.FINISHED:
                    //currentMode = eMode.FINISHED;
                    //btnDisconnect.Enabled = true;
                    //btnReset.Enabled = true;
                    //btnLoad.Enabled = true;
                    //btnZeroAll.Enabled = true;
                    //btnZeroX.Enabled = true;
                    //btnZeroY.Enabled = true;
                    //btnZeroZ.Enabled = true;
                    //btnFeedHold.Enabled = false;
                    //btnCancel.Enabled = false;
                    //Progress.Value = 0;
                    //lblRX.BackColor = System.Drawing.Color.DarkGray;
                    //btnRun.Enabled = true;
                    //lblMode.BackColor = System.Drawing.Color.Chartreuse;
                    //lblMode.Text = "FINISHED";
                    //MessageBox.Show("Normal Completion", "Run completed",
                    //            MessageBoxButtons.OK,
                    //            MessageBoxIcon.Information,
                    //            MessageBoxDefaultButton.Button1,
                    //            MessageBoxOptions.DefaultDesktopOnly);
                    //Cursor = Cursors.Default;
                    break;
                case eMode.ABORTED:
                    //currentMode = eMode.ABORTED;
                    //sw.Stop();
                    //cancelled = true;
                    //waitingOnACK = false;
                    //terminateThread();
                    //timerStatusQuery.Enabled = false;
                    //lblMode.BackColor = System.Drawing.Color.Salmon;
                    //lblRX.BackColor = System.Drawing.Color.DarkGray;
                    //lblMode.Text = "ABORTED";
                    //Progress.Value = 0;
                    //Cursor = Cursors.Default;
                    //btnRun.Enabled = true;
                    //btnReset.Enabled = true;
                    //btnLoad.Enabled = true;
                    //btnDisconnect.Enabled = true;
                    //btnZeroAll.Enabled = true;
                    //btnZeroX.Enabled = true;
                    //btnZeroY.Enabled = true;
                    //btnZeroZ.Enabled = true;
                    //btnFeedHold.Enabled = false;
                    //btnCancel.Enabled = false;
                    //Cursor = Cursors.Default;

                    //MessageBox.Show("Cancel has been requested", "Run aborted",
                    //                MessageBoxButtons.OK,
                    //                MessageBoxIcon.Hand,
                    //                MessageBoxDefaultButton.Button1,
                    //                MessageBoxOptions.DefaultDesktopOnly);
                    //cancelled = false;
                    break;
                case eMode.WAITING:
                    //currentMode = eMode.WAITING;
                    //Cursor = Cursors.WaitCursor;
                    //lblMode.BackColor = System.Drawing.Color.Yellow;
                    break;
                case eMode.READY:
                    //currentMode = eMode.READY;
                    //lblMode.BackColor = System.Drawing.Color.Gainsboro;
                    //lblMode.Text = "READY";
                    //Cursor = Cursors.Default;
                    break;
                case eMode.LOADING:
                    //Cursor = Cursors.AppStarting;
                    //pnlControl.Enabled = false;
                    //btnRun.Enabled = false;
                    //listBoxGcode.Items.Clear();
                    //this.Refresh();
                    //lblMode.BackColor = System.Drawing.Color.SkyBlue;
                    //lblMode.Text = "LOADING";
                    break;
                case eMode.SOFTRESET:
                    //currentMode = eMode.SOFTRESET;
                    //lblMode.BackColor = System.Drawing.Color.SkyBlue;
                    //lblMode.Text = "SOFT RESET";
                    break;
                case eMode.FEEDHOLD:
                    // transient mode, don't update currentmode
                    //specialMode = eMode.FEEDHOLD;
                    //lblMode.BackColor = System.Drawing.Color.Orange;
                    //lblMode.Text = "FEED HOLD";
                    //btnFeedHold.BackColor = System.Drawing.Color.Orange;
                    //btnFeedHold.Text = "Cycle Start";
                    break;
                case eMode.CYCLESTART:
                    // transient  mode, don't update currentmode
                    //specialMode = eMode.CYCLESTART;
                    //lblMode.BackColor = System.Drawing.Color.Gainsboro;
                    //lblMode.Text = "RUNNING";
                    //btnFeedHold.BackColor = System.Drawing.Color.Khaki;
                    //btnFeedHold.Text = "Feed Hold";
                    break;
            }
        }

        #endregion

        #region 状态指示灯

        private void TransmitLED()
        {
            lb_TX.BackColor = System.Drawing.Color.LightGreen;
            TXLEDoff.Enabled = true;
        }


        private void TXLEDoffElapsed(object sender, EventArgs e)
        {
            TXLEDoff.Enabled = false;
            lb_TX.BackColor = System.Drawing.Color.DarkGray;
        }

        private void ReceiveLED()
        {
            RXLEDoff.Enabled = true;
            lb_RX.BackColor = System.Drawing.Color.Khaki;
        }

        private void RXLEDoffElapsed(object sender, EventArgs e)
        {
            RXLEDoff.Enabled = false;
            lb_RX.BackColor = System.Drawing.Color.DarkGray;
        }

        #endregion
        private void UpdateConsoleListBox(string str)
        {
            if (!str.ToUpper().Contains("OK"))
                listBox1.Items.Add(str);
            this.listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = imgFileDialog.ShowDialog();
            {
                if (result == DialogResult.OK)
                {
                    tb_filename.Text = imgFileDialog.FileName;
                    Bitmap bmp = new Bitmap(imgFileDialog.FileName);
                    frm_img imgForm = new frm_img(bmp);
                    imgForm.caller = this;
                    imgForm.Show();
                }
            }
        }
        /// <summary>
        /// 窗口重画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_main_Resize(object sender, EventArgs e)
        {
            comList.Location = new Point(comList.Location.X, this.Height - 63);
            btn_connect.Location = new Point(btn_connect.Location.X, this.Height - 64);
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (currentMode != eMode.CONNECTED)
            {
                try
                {
                    connect();
                    if (!comPort.IsOpen)
                        return;

                    setMode(eMode.CONNECTED);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    setMode(eMode.DISCONNECTED);
                    disconnect();
                }
            }
            else
            {
                setMode(eMode.DISCONNECTED);
                disconnect();
            }
        }

       
     

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lb_laserspeed.Text = (track_laserSpeed.Value * 100).ToString();
        }

        private void track_LaserPower_Scroll(object sender, EventArgs e)
        {
            lb_laserpower.Text = track_LaserPower.Value.ToString();
        }

       
        public void SetTargetImage(Bitmap bmp,List<string> gcode)
        {
            target_img = bmp;
            pictureBox1.Image = bmp;
            foreach (string gc in gcode)
            {
                listBox2.Items.Add(gc);
            }
            
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Send(comboBox1.Text);
                Invoke(UpdateConsole, ">_ " + comboBox1.Text);
                comboBox1.Text = "";
            }
        }

        private void frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopReading();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Send("M03\r");
            }
            else
            {
                Send("M05\r");
            }
        }

        private void comList_DropDown(object sender, EventArgs e)
        {
            getSerialPortList();
        }

        private void btn_output_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            //设置文件保存类型
            sf.Filter = "nc文件|*.nc|所有文件|*.*";
            //如果用户没有输入扩展名，自动追加后缀
            sf.AddExtension = true;
            //设置标题
            sf.Title = "保存Gcode";
            //如果用户点击了保存按钮
            if (sf.ShowDialog() == DialogResult.OK)
            {
                //实例化一个文件流--->与写入文件相关联
                FileStream fs = new FileStream(sf.FileName, FileMode.Create);
                //获得字节数组
                byte[] data ;
                int offset=0;
                for( int i=0;i<listBox2.Items.Count;i++)
                {
                    data = new UTF8Encoding().GetBytes(listBox2.Items[i].ToString()+"\n");
                    fs.Write(data, 0, data.Length);
                    
                }
                //开始写入
                
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();

            }
        }
    }
}
