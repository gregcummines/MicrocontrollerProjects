using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;
using ZedGraph;
using Pop3;

using ChickenCoopBaseStation.Commands;

namespace ChickenCoopBaseStation
{
    public partial class MainForm : Form
    {
        static EventWaitHandle _waitHandle = new AutoResetEvent(false);
        private static TimeSpan _timeToUpdate;
        private static object _dataUpdate = new object();
        private CoopData _coopData;
        private GraphPane m_temperatureGraphPane;
        private GraphPane m_lightGraphPane;
        LineItem _averageLightCurve;
        LineItem _instantLightCurve;
        LineItem _coopTempCurve;
        LineItem _waterTempCurve;
        private ErrorStatusForm _errorForm;

        private int _foodLevelLowLastReading = 0;


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            panelCommunicationsStatus.BackColor = Color.Red;
            m_temperatureGraphPane = zgTemp.GraphPane;
            m_lightGraphPane = zgLight.GraphPane;
            CreateTemperatureLineGraph();
            CreateLightLineGraph();
            UpdateStatus();
            tmrUpdateStatus.Start();
            tmrBlinkerLowFoodLevel.Start();
            //timerCheckEmail.Start();
            //timerCheckEmail_Tick(null, null);
        }

        private delegate void UpdateStatsDelegate(string s);
        private void UpdateStats(string s)
        {
            // textBox1.Text = s;
            // Do NOT do this, as we are on a different thread.

            // Check if we need to call BeginInvoke.
            if (this.InvokeRequired)
            {
                // Pass the same function to BeginInvoke,
                // but the call would come on the correct
                // thread and InvokeRequired will be false.
                this.BeginInvoke(new UpdateStatsDelegate(UpdateStats),
                                                 new object[] { s });

                return;
            }
            // TODO: do work here
        }

        private void CreateTemperatureLineGraph()
        {
            //clear if anything exists.            
            m_temperatureGraphPane.CurveList.Clear();
            m_temperatureGraphPane.XAxis.Type = AxisType.Date;
            m_temperatureGraphPane.XAxis.Scale.MajorUnit = DateUnit.Hour;
            m_temperatureGraphPane.XAxis.Scale.MajorStep = 10;

            // Set the titles and axis labels
            m_temperatureGraphPane.Title.Text = "Coop Statistics";
            m_temperatureGraphPane.XAxis.Title.Text = "Time";
            m_temperatureGraphPane.YAxis.Title.Text = "Data";

            // Generate a blue curve with Star symbols
            //_lightCurve = m_graphPane.AddCurve("Light", new PointPairList(), Color.Blue, SymbolType.Diamond);
            _waterTempCurve = m_temperatureGraphPane.AddCurve("WaterTemp", new PointPairList(), Color.Violet, SymbolType.None);
            _waterTempCurve.Line.Width = 2;
            _coopTempCurve = m_temperatureGraphPane.AddCurve("CoopTemp", new PointPairList(), Color.Red, SymbolType.None);
            _coopTempCurve.Line.Width = 2;

            // Calculate the Axis Scale Ranges
            zgTemp.AxisChange();
        }

        private void CreateLightLineGraph()
        {
            //clear if anything exists.            
            m_lightGraphPane.CurveList.Clear();
            m_lightGraphPane.XAxis.Type = AxisType.Date;
            m_lightGraphPane.XAxis.Scale.MajorUnit = DateUnit.Hour;
            m_lightGraphPane.XAxis.Scale.MajorStep = 10;

            // Set the titles and axis labels
            m_lightGraphPane.Title.Text = "Coop Statistics";
            m_lightGraphPane.XAxis.Title.Text = "Time";
            m_lightGraphPane.YAxis.Title.Text = "Data";

            // Generate a blue curve with Star symbols
            //_lightCurve = m_graphPane.AddCurve("Light", new PointPairList(), Color.Blue, SymbolType.Diamond);
            _averageLightCurve = m_lightGraphPane.AddCurve("AverageLight", new PointPairList(), Color.Violet, SymbolType.None);
            _averageLightCurve.Line.Width = 2;
            _instantLightCurve = m_lightGraphPane.AddCurve("InstantLight", new PointPairList(), Color.Red, SymbolType.None);
            _instantLightCurve.Line.Width = 2;

            // Calculate the Axis Scale Ranges
            zgTemp.AxisChange();
        }

        private void UpdateGraphs()
        {
            zgLight.AxisChange();
            zgLight.Invalidate();
            zgTemp.AxisChange();
            zgTemp.Invalidate();
        }

        //private void CreateBarGraph()
        //{

        //    //clear if anything exists.            
        //    m_graphPane.CurveList.Clear();

        //    string _graphTitle = "Coop Statistics", _xTitle = "Time", _yTitle = "Data";

        //    // Set the titles and axis labels
        //    m_graphPane.Title.Text = _graphTitle;
        //    m_graphPane.XAxis.Title.Text = _xTitle;
        //    m_graphPane.YAxis.Title.Text = _yTitle;

        //    BarItem myCurve = m_graphPane.AddBar("Light", new PointPairList(), Color.Blue);
        //    Color[] colors = { Color.Red, Color.Yellow, Color.Green, Color.Blue, Color.Purple };
        //    myCurve.Bar.Fill = new Fill(colors);
        //    myCurve.Bar.Fill.Type = FillType.GradientByZ;

        //    myCurve.Bar.Fill.RangeMin = 0;
        //    myCurve.Bar.Fill.RangeMax = 4;

        //    // Tell ZedGraph to calculate the axis ranges
        //    zgTemp.AxisChange();
        //}


        private void timer1_Tick(object sender, EventArgs e)
        {

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            try
            {
                lock (_dataUpdate)
                {
                    DateTime dateTimeStart = DateTime.Now;

                    _coopData = ChickenCoopWirelessProtocol.Instance.GetAllStatistics();
                    if (_coopData != null)
                    {
                        lblWaterTemp.Text = (_coopData.WaterTemperature != CoopData.InvalidData) ? _coopData.WaterTemperature.ToString("F2") + "F" : "Unknown";
                        lblCoopTemp.Text = (_coopData.CoopTemperature != CoopData.InvalidData) ? _coopData.CoopTemperature.ToString("F2") + "F" : "Unknown";
                        lblWaterSetTemp.Text = (_coopData.WaterTemperatureSetPoint != CoopData.InvalidData) ? _coopData.WaterTemperatureSetPoint.ToString("F0") + "F" : "Unknown";
                        lblAvgLightLevel.Text = (_coopData.AverageLightReading != CoopData.InvalidData) ? _coopData.AverageLightReading.ToString() : "Unknown";
                        lblInstantLightLevel.Text = (_coopData.InstantLightReading != CoopData.InvalidData) ? _coopData.InstantLightReading.ToString() : "Unknown";
                        lblCoopDateTime.Text = _coopData.CoopDateTime.ToString();
                        lblDaylightBulbOn.Text = _coopData.CoopLightOn.ToString();
                        lblDoorState.Text = _coopData.DoorState.ToString();
                        lblFoodLevel.Text = _coopData.FoodLevelLow == 0 ? "OK" : "Low";

                        if (_foodLevelLowLastReading != _coopData.FoodLevelLow)
                        {
                            lblFoodLevelLastChanged.Text = DateTime.Now.ToString();
                            _foodLevelLowLastReading = _coopData.FoodLevelLow;
                        }

                        lblWaterHeaterOn.Text = _coopData.WaterHeaterOn.ToString();

                        DateTime dateTimeEnd = DateTime.Now;

                        lblLastUpdated.Text = dateTimeEnd.ToString();

                        _timeToUpdate = dateTimeEnd - dateTimeStart;
                        lblTimeToUpdate.Text = _timeToUpdate.Milliseconds.ToString() + "mS";

                        LogData();

                        double x = (double)new XDate(dateTimeEnd);

                        //m_graphPane.CurveList["Light"].AddPoint(x,Convert.ToDouble(_coopData.AverageLightReading));
                        if (_coopData.WaterTemperature != CoopData.InvalidData)
                            m_temperatureGraphPane.CurveList["WaterTemp"].AddPoint(x, _coopData.WaterTemperature);
                        if (_coopData.CoopTemperature != CoopData.InvalidData)
                            m_temperatureGraphPane.CurveList["CoopTemp"].AddPoint(x, _coopData.CoopTemperature);
                        if (_coopData.AverageLightReading != CoopData.InvalidData) ;
                        m_lightGraphPane.CurveList["AverageLight"].AddPoint(x, Convert.ToDouble(_coopData.AverageLightReading));
                        if (_coopData.InstantLightReading != CoopData.InvalidData)
                            m_lightGraphPane.CurveList["InstantLight"].AddPoint(x, Convert.ToDouble(_coopData.InstantLightReading));
                        UpdateGraphs();

                        statusLabel.Text = "Updated data at " + dateTimeEnd.ToString();

                        UpdateErrorStatus();

                        //if (_errorForm != null)
                        //{
                        //    _errorForm.Close();
                        //    _errorForm = null;
                        //}
                        panelCommunicationsStatus.BackColor = Color.Green;
                    }
                    else
                    {
                        statusLabel.Text = "Failure communicating with Remote Chicken Coop Server...";
                        panelCommunicationsStatus.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception exc)
            {
                string error = "Failing updating data..." + exc.Message;
                Debug.Print(error);
                statusLabel.Text = error;
                panelCommunicationsStatus.BackColor = Color.Red;
                //if ((_errorForm == null) || (!_errorForm.Created))
                //{
                //    _errorForm = new ErrorStatusForm();
                //    _errorForm.Error = "Failure getting data from coop: \n\r" + exc.Message;
                //    _errorForm.ShowDialog(this);
                //}
            }
        }

        private void UpdateErrorStatus()
        {
            if (_coopData.FoodLevelLow == 1)
            {
                tmrBlinkerLowFoodLevel.Start();
                AlertService.AlertMe("Food level is low.");
            }
            else
            {
                tmrBlinkerLowFoodLevel.Stop();
                lblFoodLevelWarning.Visible = false;
            }
        }

        private void LogData()
        {
            DateTime dateTime = DateTime.Now;
            string fileName = "AutomaticChickenCoopV4DataLogFile_" + dateTime.Month.ToString() + dateTime.Day.ToString() + dateTime.Year.ToString() + ".txt";

            const string header = "DateTime,CoopTemperature,WaterTemperature,WaterHeaterOn,DoorState,InstantLightLevel,AverageLightLevel,FoodLevel,CoopDaylightBulbOn\r\n";
            byte[] data = null;

            string dataToFile = dateTime.ToString() + "," +
                lblCoopTemp.Text + "," +
                lblWaterTemp.Text + "," +
                lblWaterHeaterOn.Text + "," +
                lblDoorState.Text + "," +
                lblInstantLightLevel.Text + "," +
                lblAvgLightLevel.Text + "," +
                lblFoodLevel.Text + "," +
                lblDaylightBulbOn.Text + "\r\n";

            string fileToWrite = fileName;
            FileStream dataWriter = null;

            if (!File.Exists(fileToWrite))
            {
                dataWriter = new FileStream(fileToWrite, FileMode.OpenOrCreate, FileAccess.Write);

                data = Encoding.UTF8.GetBytes(header);
                dataWriter.Write(data, 0, data.Length);
                dataWriter.Flush();
            }
            else
            {
                dataWriter = new FileStream(fileToWrite, FileMode.Append, FileAccess.Write);
            }

            data = Encoding.UTF8.GetBytes(dataToFile);
            dataWriter.Write(data, 0, data.Length);
            dataWriter.Flush();
            dataWriter.Close();
        }

        private void tmrBlinkerLowFoodLevel_Tick(object sender, EventArgs e)
        {
            if (_coopData != null)
            {
                if (_coopData.FoodLevelLow == 1)
                {
                    lblFoodLevelWarning.Visible = !lblFoodLevelWarning.Visible;
                }
            }
        }

        private void timerCheckEmail_Tick(object sender, EventArgs e)
        {
            Pop3.Pop3MimeClient DemoClient = new Pop3.Pop3MimeClient("pop.gmail.com", 995, true, "gregcummines@gmail.com", "lr680nq");

            DemoClient.Trace += new TraceHandler(Console.WriteLine);
            DemoClient.ReadTimeout = 60000; //give pop server 60 seconds to answer

            //establish connection
            DemoClient.Connect();

            //get mailbox stats
            int numberOfMailsInMailbox, mailboxSize;
            DemoClient.GetMailboxStats(out numberOfMailsInMailbox, out mailboxSize);

            //get at most the xx first emails
            RxMailMessage mm;
            int downloadNumberOfEmails;
            int maxDownloadEmails = 400;
            if (numberOfMailsInMailbox < maxDownloadEmails)
            {
                downloadNumberOfEmails = numberOfMailsInMailbox;
            }
            else
            {
                downloadNumberOfEmails = maxDownloadEmails;
            }
            for (int i = downloadNumberOfEmails; i > 1; i--)
            {
                DemoClient.GetEmail(i, out mm);
                if (mm == null)
                {
                    Console.WriteLine("Email " + i.ToString() + " cannot be displayed.");
                }
                else
                {
                    
                    string mailStructure = mm.MailStructure();

                    //int pos = mailStructure.IndexOf

                    if (mailStructure.Contains("ChickenCoopCommand6x7y"))
                    {

                        DemoClient.DeleteEmail(i);        
                    }
                }

                
            }

            ////uncomment the following code if you want to write the raw text of the emails to a file.
            //string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //string Email;
            //DemoClient.IsCollectRawEmail = true;
            //string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Emails From POP3 Server.TXT";
            //using (StreamWriter sw = File.CreateText(fileName))
            //{
            //    for (int i = 1; i <= maxDownloadEmails; i++)
            //    {
            //        sw.WriteLine("Email: " + i.ToString() + "\n===============\n\n");
            //        DemoClient.GetRawEmail(i, out Email);
            //        sw.WriteLine(Email);
            //        sw.WriteLine();
            //    }
            //    sw.Close();
            //}

            //close connection
            DemoClient.Disconnect();
        }

        public static bool SMSResponse(string message)
        {
            return false;
            //DateTime dateTime = DateTime.Now;
            //string fileToWrite = "AutomaticChickenCoop_AlertMessageSent_" + dateTime.Month.ToString() + dateTime.Day.ToString() + dateTime.Year.ToString() + ".txt";
            //FileStream dataWriter = null;

            //if (File.Exists(fileToWrite))
            //    return false;

            //try
            //{
            //    string subjectOfMessage = "Automatic Chicken Coop Alert!";
            //    string to = "6125999506@vtext.com";
            //    //string to = "gregcummines@gmail.com";
            //    using (MailMessage mailMsg = new MailMessage("gregcummines@gmail.com", to))
            //    {
            //        mailMsg.Subject = subjectOfMessage;
            //        mailMsg.IsBodyHtml = false;
            //        mailMsg.Body = message;

            //        using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            //        {
            //            //client.Host = "smtp.gmail.com";
            //            client.Credentials = new NetworkCredential("gregcummines@gmail.com", "lr680nq");
            //            client.EnableSsl = true;
            //            client.Send(mailMsg);

            //            if (!File.Exists(fileToWrite))
            //            {
            //                dataWriter = new FileStream(fileToWrite, FileMode.OpenOrCreate, FileAccess.Write);

            //                byte[] data = Encoding.UTF8.GetBytes(subjectOfMessage);
            //                dataWriter.Write(data, 0, data.Length);
            //                dataWriter.Flush();
            //            }
            //        }
            //    }

            //    to = "6123821854@vtext.com";
            //    //string to = "gregcummines@gmail.com";
            //    using (MailMessage mailMsg = new MailMessage("gregcummines@gmail.com", to))
            //    {
            //        mailMsg.Subject = subjectOfMessage;
            //        mailMsg.IsBodyHtml = false;
            //        mailMsg.Body = message;

            //        using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            //        {
            //            //client.Host = "smtp.gmail.com";
            //            client.Credentials = new NetworkCredential("gregcummines@gmail.com", "lr680nq");
            //            client.EnableSsl = true;
            //            client.Send(mailMsg);
            //        }
            //    }
            //}
            //catch (SmtpException)
            //{
            //    // handle exception here
            //}
            //return true;
        }
    }
}
