using Isis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Dashboard
{
    [QS.Fx.Reflection.ComponentClass("1`1", "Dashboard", "This is a dashboard for Operator's console")]
    public partial class Form1  : QS.Fx.Component.Classes.UI, QS.Fx.Interface.Classes.ICheckpointedCommunicationChannelClient<QS.Fx.Value.Classes.IText, QS.Fx.Value.Classes.IText>
    {

       
        private Dictionary<String, PictureBox> generatorControlMap;
        private Dictionary<String, List<LineControl>> lineControlMap;
        private Dictionary<String, BusControl> busControlMap;
        private String previousAffectListString = String.Empty;

        Timer animationTimer = new Timer();
        System.Threading.Timer updateThreadingTimer;

        private Timer timerRealTimeData = new Timer();
        Size size = new Size(10, 10);
        static Size initSize;
        static Size tempSize;
        bool showInfoPanel = false;


        delegate void stringArg(string who, string val);

        public Form1(QS.Fx.Object.IContext _mycontext,
            [QS.Fx.Reflection.Parameter("channel", QS.Fx.Reflection.ParameterClass.Value)] 
            QS.Fx.Object.IReference<QS.Fx.Object.Classes.ICheckpointedCommunicationChannel<QS.Fx.Value.Classes.IText, QS.Fx.Value.Classes.IText>>
                _channel)
            : base(_mycontext)
        {

            /***** Isis-2 *****/

            IsisSystem.Start();
            myGroup = Isis.Group.Lookup("Circuit");
            if (myGroup == null)
                myGroup = new Group("Circuit");

            myGroup.Handlers[UPDATE] += (stringArg)delegate(string name, string val)
            {
                lock (this)
                {
                    string _new_text = val;
                    if (!_new_text.Equals(this._text))
                    {
                        this._text = _new_text;
                        // _Refresh();
                    }

                }
            };
            myGroup.Join();


            InitializeComponent();
            panel1.MinimumSize = size;
            initSize = panel1.Size;
            this.Click += Form1_Click;
            animationTimer.Tick += new EventHandler(timer1_Tick); // Everytime timer ticks, timer_Tick will be called
            animationTimer.Interval = (1) * (1);             // Timer will tick every 2 milliseconds
            animationTimer.Enabled = false; // Enable the timer 


            // Set Circuit Timer
            updateThreadingTimer = new System.Threading.Timer(new System.Threading.TimerCallback(this.ThreadingTimer_Tick), null,
                            10000, System.Threading.Timeout.Infinite);
            

            generatorControlMap = new Dictionary<String, PictureBox>();
            lineControlMap = new Dictionary<String, List<LineControl>>();
            busControlMap = new Dictionary<String, BusControl>();


            this.BackColor = Color.Yellow;           
            XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
            xmlDoc.Load("C:\\liveobjects\\libraries\\4D12F33758B74DAFBDE0D17E298AD01E\\1\\Data\\Component.xml");

            // Get elements
            XmlNodeList components = xmlDoc.GetElementsByTagName("component");
            float xSizeFactor = 1382 / 804;
            float ySizeFactor = 744 / 498;
            
            for (int i = 0; i < components.Count; i++)
            {
                Control control = null;
                if (Convert.ToString(components[i].Attributes[1].Value).Equals("Generator"))
                {
                    String id = components[i].Attributes[0].Value;
                    int x = Convert.ToInt32(components[i].ChildNodes[0].Attributes[0].InnerText);
                    int y = Convert.ToInt32(components[i].ChildNodes[1].Attributes[0].InnerText);
                    int rotation = Convert.ToInt32(components[i].ChildNodes[2].Attributes[0].InnerText);
                    control = AddGeneratorControl(id, x *xSizeFactor, y *ySizeFactor, rotation);
                }
                else if (Convert.ToString(components[i].Attributes[1].Value).Equals("Bus"))
                {
                    String id = components[i].Attributes[0].Value;
                    int x1 =  Convert.ToInt32(components[i].ChildNodes[0].Attributes[0].InnerText);
                    int y1 =  Convert.ToInt32(components[i].ChildNodes[1].Attributes[0].InnerText);
                    int x2 =  Convert.ToInt32(components[i].ChildNodes[2].Attributes[0].InnerText);
                    int y2 =  Convert.ToInt32(components[i].ChildNodes[3].Attributes[0].InnerText);
                    bool isArrow = Convert.ToString(components[i].ChildNodes[4].Attributes[0].InnerText).Equals("line-arrow");
                    control = AddBusControl(id, x1*xSizeFactor, y1 *ySizeFactor, x2 *xSizeFactor, y2 *ySizeFactor,isArrow);
                    control.Click += Bus_Click;          
                }
                else if (Convert.ToString(components[i].Attributes[1].Value).Equals("Line"))
                {
                    String id = components[i].ChildNodes[4].Attributes[0].Value;
                    int x1 =  Convert.ToInt32(components[i].ChildNodes[0].Attributes[0].InnerText);
                    int y1 =  Convert.ToInt32(components[i].ChildNodes[1].Attributes[0].InnerText);
                    int x2 =  Convert.ToInt32(components[i].ChildNodes[2].Attributes[0].InnerText);
                    int y2 =  Convert.ToInt32(components[i].ChildNodes[3].Attributes[0].InnerText);
                     control = AddLineControl(id, x1 *xSizeFactor, y1 *ySizeFactor, x2 *xSizeFactor, y2*ySizeFactor);
                }

                if(control != null)
                    this.Controls.Add(control);
            }


            this._channelendpoint_circuit = _mycontext.DualInterface<
                QS.Fx.Interface.Classes.ICheckpointedCommunicationChannel<QS.Fx.Value.Classes.IText, QS.Fx.Value.Classes.IText>,
                QS.Fx.Interface.Classes.ICheckpointedCommunicationChannelClient<QS.Fx.Value.Classes.IText, QS.Fx.Value.Classes.IText>>(this);

            if (_channel != null)
            {
                QS.Fx.Object.Classes.ICheckpointedCommunicationChannel<QS.Fx.Value.Classes.IText, QS.Fx.Value.Classes.IText> _channelproxy =
                    _channel.Dereference(_mycontext);

                QS.Fx.Endpoint.Classes.IDualInterface<
                    QS.Fx.Interface.Classes.ICheckpointedCommunicationChannelClient<QS.Fx.Value.Classes.IText, QS.Fx.Value.Classes.IText>,
                        QS.Fx.Interface.Classes.ICheckpointedCommunicationChannel<QS.Fx.Value.Classes.IText, QS.Fx.Value.Classes.IText>>
                            _otherendpoint =
                                _channelproxy.Channel;

                this._channelconnection_circuit = ((QS.Fx.Endpoint.Classes.IEndpoint)this._channelendpoint_circuit).Connect(_otherendpoint);
            }

            

            
        }

        private void ThreadingTimer_Tick(object state)
        {
            lock (this)
            {
               // MessageBox.Show("Update Threading Circuit Timer!");
                String affectedListString = GetServerResponseForOperation(Constants.OPERATION_AFFECTEDLIST);

                if (!affectedListString.Equals(String.Empty))
                    UpdateAffectedLines(affectedListString);

                previousAffectListString = affectedListString;

                updateThreadingTimer = new System.Threading.Timer(new System.Threading.TimerCallback(this.ThreadingTimer_Tick), null,
                           Constants.UPDATE_CIRCUIT_INTERVAL, System.Threading.Timeout.Infinite);
            }
                      
        }

       

        private void UpdateAffectedLines(string affectedListString)
        {
            String[] affectedLines = affectedListString.Split(Constants.SEPARATOR_DELIMITER.ToCharArray());
            String[] previousAffectedLines = previousAffectListString.Split(Constants.SEPARATOR_DELIMITER.ToCharArray());
            // Iterate each previous affect lines and make it of original color
            foreach (String lineId in previousAffectedLines)
            {
                if (this.lineControlMap.ContainsKey(lineId))
                {
                    foreach (LineControl lineControl in lineControlMap[lineId])
                        lineControl.UpdateLine(Constants.NORMAL_LINE_COLOR);
                }

            }


            // Iterate for each affected lines
            foreach(String lineId in affectedLines)
            {
                if (this.lineControlMap.ContainsKey(lineId))
                {
                    foreach (LineControl lineControl in lineControlMap[lineId])
                        lineControl.UpdateLine(Constants.WARNING_LINE_COLOR);
                }
                    
            }

            
        }


        #region LDO
        private string _text = string.Empty;
        private bool _editing;
        private DateTime _lastchanged;
        private System.Threading.Timer _timer;

        private bool primary = false;
        private int normal = 0;
        private Random rnd;
        Group myGroup;
        int UPDATE = 1;

        private const int EditingTimeoutInMilliseconds1 = 1000;
        private const int EditingTimeoutInMilliseconds2 = 100;
        private KeyPressEventArgs e;

        [QS.Fx.Base.Inspectable("channelendpoint")]
        private QS.Fx.Endpoint.Internal.IDualInterface<
            QS.Fx.Interface.Classes.ICheckpointedCommunicationChannel<QS.Fx.Value.Classes.IText, QS.Fx.Value.Classes.IText>,
            QS.Fx.Interface.Classes.ICheckpointedCommunicationChannelClient<QS.Fx.Value.Classes.IText, QS.Fx.Value.Classes.IText>>
                _channelendpoint_circuit;

        [QS.Fx.Base.Inspectable("channelconnection")]
        private QS.Fx.Endpoint.IConnection _channelconnection_circuit;

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;
            /*
            for (int i = 0; i < labels.Length; i++)
            {
                this.labels[i].Left = size_x[i] * control.Size.Width / 804;
                this.labels[i].Top = size_y[i] * control.Size.Height / 498;
                try
                {
                    float size = Math.Min(control.Size.Width, control.Size.Height) * 0.03F;
                    this.labels[i].Font = new System.Drawing.Font("Microsoft Sans Serif", size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
                catch (Exception d)
                {
                }
            }
             */
        }

        QS.Fx.Value.Classes.IText QS.Fx.Interface.Classes.ICheckpointedCommunicationChannelClient<QS.Fx.Value.Classes.IText, QS.Fx.Value.Classes.IText>.Checkpoint()
        {
            return new QS.Fx.Value.UnicodeText(this._text);
        }

        void QS.Fx.Interface.Classes.ICheckpointedCommunicationChannelClient<QS.Fx.Value.Classes.IText, QS.Fx.Value.Classes.IText>.Initialize(QS.Fx.Value.Classes.IText _checkpoint)
        {
            lock (this)
            {
                string _new_text = (_checkpoint != null) ? _checkpoint.Text : null;
                if (_new_text == null)
                {
                    _new_text = string.Empty;
                    primary = true;
                    rnd = new Random();
                }
                if (!_new_text.Equals(this._text))
                {
                    this._text = _new_text;
                }
                //MessageBox.Show("Calling refresh");
                _Refresh();
            }
        }

        void QS.Fx.Interface.Classes.ICheckpointedCommunicationChannelClient<QS.Fx.Value.Classes.IText, QS.Fx.Value.Classes.IText>.Receive(QS.Fx.Value.Classes.IText _message)
        {
            /* lock (this)
             {
                 string _new_text = _message.Text;
                 if (!_new_text.Equals(this._text))
                 {
                     this._text = _new_text;
                     _Refresh();
                 }
             }*/
        }


        private void _Refresh()
        {
            lock (this)
            {

            }
        }

        private void _textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            lock (this)
            {
                if (_editing)
                    _lastchanged = DateTime.Now;
                else
                {
                    _editing = true;
                    _lastchanged = DateTime.Now;
                    _timer = new System.Threading.Timer(new System.Threading.TimerCallback(this._TimerCallback), null,
                        EditingTimeoutInMilliseconds1, System.Threading.Timeout.Infinite);
                }
            }
        }

        private void _TimerCallback(object o)
        {
            lock (this)
            {
                if (_editing)
                {
                    int _remaining_milliseconds =
                        EditingTimeoutInMilliseconds1 - ((int)Math.Floor((DateTime.Now - _lastchanged).TotalMilliseconds));
                    if (_remaining_milliseconds > EditingTimeoutInMilliseconds2)
                        _timer = new System.Threading.Timer(new System.Threading.TimerCallback(this._TimerCallback), null,
                            _remaining_milliseconds, System.Threading.Timeout.Infinite);
                    else
                    {
                        if (InvokeRequired)
                            BeginInvoke(new System.Threading.TimerCallback(this._TimerCallback), new object[] { null });
                        else
                        {
                            _editing = false;
                            string _text = this._text;
                            this.Log(_text);
                        }
                    }
                }
                _Refresh();
            }
        }

        private void timerRealTimeData_Tick(object sender, System.EventArgs e)
        {
            if (normal == 0)
            {
                normal = 1;
                this.timerRealTimeData.Interval = 60000;
            }
            else if (normal == 1)
            {
                normal = 2;
                this.timerRealTimeData.Interval = 30000;
            }
            else if (normal == 2)
            {
                normal = 0;
                this.timerRealTimeData.Interval = 30000;
            }
        }

        #endregion

        #region Server Communication

        public String GetServerResponseForOperation(String operation)
        {
      
            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];
            String affectedListString = String.Empty;

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5300);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(operation);

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    Console.WriteLine("after send");
                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    affectedListString = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                   // MessageBox.Show(operation + ": " + affectedListString);
                    Console.ReadLine();
                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();


                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return affectedListString;
        }

        #endregion

        #region Click Handler for Panel
        private void Bus_Click(object sender, EventArgs e)
        {

            BusControl busControl = sender as BusControl;
            if (busControl != null)
            {
                String busDetailsString = GetServerResponseForOperation(Constants.OPERATION_BUSDETAILS + Constants.SEPARATOR_DELIMITER + busControl.getBusNo());

                if (!busDetailsString.Equals(String.Empty))
                {
                    setPanelValues(busControl, busDetailsString);
                    animationTimer.Enabled = true;
                    tempSize = size;
                    showInfoPanel = true;
                    animationTimer.Start();

                }
            }

            

        }

        #endregion

        #region Panel Animation

        void Form1_Click(object sender, EventArgs e)
        {
            showInfoPanel = false;
            animationTimer.Start();
        }

     

        private void InfoPanel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

       

        private void setPanelValues(BusControl busControl, string busDetailsString)
        {
            busControl.setBusDetails(busDetailsString);
            this.busIdValue.Text = busControl.getBusNo();
            this.busNameValue.Text = busControl.getBusName();
            this.busTypeValue.Text = busControl.getBusType();
            this.busAreaNumberValue.Text = busControl.getAreaNumber();
            this.busVoltageValue.Text = busControl.getBusVoltage();
            this.busBaseKiloVoltageValue.Text = busControl.getBusBaseKiloVoltage();
            this.busPhaseAngleValue.Text = busControl.getVoltagePhaseAngle();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (showInfoPanel)
            {
                if (tempSize.Height < initSize.Height && tempSize.Width < initSize.Width)
                {
                    tempSize.Height += (initSize.Height / 100) * 5;
                    tempSize.Width += (initSize.Width / 100) * 5;
                    panel1.Height = tempSize.Height;
                    panel1.Width = tempSize.Width;
                    panel1.Show();
                }
                else
                {
                    animationTimer.Stop();
                    panel1.Visible = true;
                    panel1.Size = initSize;
                    panel1.Show();
                }
            }
            else
            {
                if (tempSize.Height > 0 && tempSize.Width > 0)
                {
                    tempSize.Height -= (initSize.Height / 100) * 5;
                    tempSize.Width -= (initSize.Width / 100) * 5;
                    panel1.Height = tempSize.Height;
                    panel1.Width = tempSize.Width;
                    panel1.Show();
                }
                else
                {
                    animationTimer.Stop();
                    panel1.Hide();
                }

            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            showInfoPanel = false;
            animationTimer.Start();
        }

        #endregion

        #region Add Controls

        private Control AddBusControl(String id, float x1, float y1, float x2, float y2, bool isArrow)
        {

            BusControl busControl = new BusControl(id, x1, y1, x2, y2);
            busControl.Location = new Point((int)x1,(int) y1);
            this.busControlMap.Add(id, busControl);

            if (isArrow)
            {
                PictureBox arrowElement = AddArrowToBus();
                arrowElement.Location = new Point((int)(x1+x2)/2,(int) y1+1);
                this.Controls.Add(arrowElement);
            }

            return busControl;
        }

        private PictureBox AddArrowToBus()
        {
            PictureBox arrowBox = new PictureBox();
            arrowBox.Image = new Bitmap("C:\\liveobjects\\libraries\\4D12F33758B74DAFBDE0D17E298AD01E\\1\\Data\\arrow.png");
            arrowBox.Size = new System.Drawing.Size(10, 27);
            
            return arrowBox;
        }

        private Control AddLineControl(String id, float x1, float y1, float x2, float y2)
        {
            LineControl lineControl = new LineControl(id, x1, y1, x2, y2);
            lineControl.Location = new Point((int)x1, (int)y1);
            if (!this.lineControlMap.ContainsKey(id))
            {
                List<LineControl> lineControlList = new List<LineControl>();
                lineControlList.Add(lineControl);
                this.lineControlMap.Add(id, lineControlList);
            }else
            {
                List<LineControl> lineControlList = this.lineControlMap[id];
                lineControlList.Add(lineControl);               

            }
                
          
            return lineControl;
        }

        private PictureBox AddGeneratorControl(String id, float x, float y, int rotation)
        {
            PictureBox pictureBox = new PictureBox();

            if (rotation == 0)
                pictureBox.Image = new Bitmap("C:\\liveobjects\\libraries\\4D12F33758B74DAFBDE0D17E298AD01E\\1\\Data\\generator.png");
            else
                pictureBox.Image = new Bitmap("C:\\liveobjects\\libraries\\4D12F33758B74DAFBDE0D17E298AD01E\\1\\Data\\generator_rotated180.png");

            pictureBox.Location = new Point((int)x, (int)y);

            pictureBox.Size = new Size(34, 75);

            this.generatorControlMap.Add(id, pictureBox);

            return pictureBox;
        }

        #endregion

 

      


    }
}
