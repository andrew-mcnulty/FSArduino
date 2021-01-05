using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.IO.Ports;

namespace FSArduino
{
    public partial class Form1 : Form
    {
        SimConnectManager manager;
        Timer timer;
        SerialPort serial;

        public Form1()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            //byte[] buff = new byte[1];
            //buff[0] = GetLEDStatuses();
            //serial.Write(buff, 0, 1);
        }

        private byte GetLEDStatuses()
        {
            if (manager != null)
            {
                Task t = new Task(() => {
                    manager.RefreshData(SimConnectManager.DEFINITION.LedStatus);

                    while (!manager.DataFresh)
                    {

                    }
                });

                t.Start();
                
                byte status = 0;

                status += manager.LEDData.AP > 0.0f ? (byte)SimConnectManager.LEDS.AP : (byte)0;
                status += manager.LEDData.Nav > 0.0f ? (byte)SimConnectManager.LEDS.Nav : (byte)0;
                status += manager.LEDData.VNav > 0.0f ? (byte)SimConnectManager.LEDS.VNav : (byte)0;
                status += manager.LEDData.Loc > 0.0f ? (byte)SimConnectManager.LEDS.Loc : (byte)0;
                status += manager.LEDData.Appr > 0.0f ? (byte)SimConnectManager.LEDS.Appr : (byte)0;
                status += manager.LEDData.Flc > 0.0f ? (byte)SimConnectManager.LEDS.FLC : (byte)0;

                return status;

            }
            return 0;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (serial == null)
                {
                    serial = new SerialPort();
                    serial.PortName = "COM3";
                    serial.BaudRate = 9600;
                    serial.Parity = Parity.None;
                    serial.DataBits = 8;
                    serial.StopBits = StopBits.One;
                    serial.Handshake = Handshake.None;

                    serial.Open();
                }

                if (manager == null)
                {
                    manager = new SimConnectManager(base.Handle);
                    ErrorBox.DataSource = manager.errors;

                    manager.errors.ListChanged += Errors_ListChanged;

                    if (!manager.IsConnected)
                    {
                        manager.Connect();

                        byte[] buff = new byte[1];
                        buff[0] = GetLEDStatuses();
                        serial.Write(buff, 0, 1);

                        timer.Start();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (serial != null)
                {
                    serial = null;
                }

                if (manager != null)
                {
                    manager = null;
                }
            }
            
        }

        private void Errors_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            if (ErrorBox != null)
            {
                ErrorBox.TopIndex = 0;
            }   
        }

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == 0x402)
            {
                if (manager != null)
                {
                    manager.ReceiveMessage();
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        private void APMaster_Click(object sender, EventArgs e)
        {
            try
            {
                manager.SendEvent(SimConnectManager.EVENTS.ApMaster);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
        }

        private void NavButton_Click(object sender, EventArgs e)
        {
            try
            {
                manager.SendEvent(SimConnectManager.EVENTS.ApNav);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
