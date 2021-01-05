using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;

namespace FSArduino
{
    public class SimConnectManager
    {
        #region ENUMS
        public enum DEFINITION
        {
            LedStatus = 0
        };

        public enum REQUEST
        {
            Dummy = 0
        };

        public enum EVENTS
        {
            ApMaster, //Toggles autopilot
            ApNav,
        }

        public enum LEDS
        {
            AP = 1,
            Nav = 2,
            VNav = 4,
            Loc = 8,
            Appr = 16,
            FLC = 32,
        }

        public enum NOTIFICATION_GROUPS
        {
            Group0
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Data
        {
            public double AP;
            public double Nav;
            public double VNav;
            public double Loc;
            public double Appr;
            public double Flc;
        }

        public static Dictionary<EVENTS, string> EVENTS_TO_EVENTID = new Dictionary<EVENTS, string>()
        {
        {EVENTS.ApMaster, "AP_MASTER"},
        {EVENTS.ApNav, "AP_NAV1_HOLD"},
        };
        #endregion

        private IntPtr m_hWnd;
        public const int WM_USER_SIMCONNECT = 0x0402;
        private SimConnect m_oSimConnect;

        private bool isConnected = false;

        public SimConnectManager(IntPtr handle)
        {
            m_hWnd = handle;
            errors = new BindingList<string>();       
        }

        public bool IsConnected
        {
            get { return isConnected; }
        }

        public BindingList<string> errors;

        public Data LEDData;

        public bool DataFresh;

        public void Connect()
        {
            try
            {
                m_oSimConnect = new SimConnect("Arduino", m_hWnd, WM_USER_SIMCONNECT, null, 0);

                /// Catch a simobject data request
                m_oSimConnect.OnRecvEvent += new SimConnect.RecvEventEventHandler(OnRecvEventHandler);
                m_oSimConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(OnOpenHandler);
                m_oSimConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(OnRecvExceptionHandler);
                m_oSimConnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(OnRecvSimobjectDataBytype);


                m_oSimConnect.AddToDataDefinition(DEFINITION.LedStatus, "AUTOPILOT MASTER", "Bool", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                m_oSimConnect.AddToDataDefinition(DEFINITION.LedStatus, "AUTOPILOT NAV1 LOCK", "Bool", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                m_oSimConnect.AddToDataDefinition(DEFINITION.LedStatus, "AUTOPILOT GLIDESLOPE HOLD", "Bool", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                m_oSimConnect.AddToDataDefinition(DEFINITION.LedStatus, "AUTOPILOT NAV1 LOCK", "Bool", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                m_oSimConnect.AddToDataDefinition(DEFINITION.LedStatus, "AUTOPILOT APPROACH HOLD", "Bool", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                m_oSimConnect.AddToDataDefinition(DEFINITION.LedStatus, "AUTOPILOT GLIDESLOPE HOLD", "Bool", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

                m_oSimConnect.RegisterDataDefineStruct<Data>(DEFINITION.LedStatus);

                m_oSimConnect.RequestDataOnSimObjectType(REQUEST.Dummy, DEFINITION.LedStatus, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);

                isConnected = true;
                Log("Connected");
                Console.WriteLine("Connected");
            }
            catch (COMException ex)
            {
                Console.WriteLine("Connection to KH failed: " + ex.Message);
            }
        }

        private void OnRecvExceptionHandler(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            Log("Exception");
        }

        private void OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            if((DEFINITION)data.dwDefineID == DEFINITION.LedStatus)
            {
                LEDData = (Data)data.dwData[0];
                DataFresh = true;
            }         
        }

        public void RefreshData(DEFINITION def)
        {
            DataFresh = false;
            m_oSimConnect.RequestDataOnSimObjectType(REQUEST.Dummy, def, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
        }

        public void SendEvent(EVENTS e)
        {
            m_oSimConnect.TransmitClientEvent(1, e, 0, NOTIFICATION_GROUPS.Group0, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
            Log("Sent " + EVENTS_TO_EVENTID[e]);
        }

        public void SubscribeToEvent(EVENTS e)
        {
            m_oSimConnect.MapClientEventToSimEvent(e, EVENTS_TO_EVENTID[e]);
            m_oSimConnect.AddClientEventToNotificationGroup(NOTIFICATION_GROUPS.Group0, e, false);

        }

        public void ReceiveMessage()
        {
            m_oSimConnect.ReceiveMessage();
        }

        private void OnOpenHandler(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            m_oSimConnect.SetNotificationGroupPriority(NOTIFICATION_GROUPS.Group0, SimConnect.SIMCONNECT_GROUP_PRIORITY_HIGHEST);
        }

        private void OnRecvEventHandler(SimConnect sender, SIMCONNECT_RECV_EVENT data)
        {
            Log("Received: " + data.dwData);
        }

        private void OnRecvExceptionHandler(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            Log("Received Exception: " + data.dwException);
        }

        private void Log(string text)
        {
            errors.Insert(0, DateTime.Now.ToString() + ": " + text);
        }
    }
}
