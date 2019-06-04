using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace Ex3.Models
{
    public class SimulatorClient
    {

        static class Constants
        {
            public const int DefaultBufferSize = 256;
        }

        enum ClientStatus { running, standby };

        #region client members
        TcpClient _client;
        NetworkStream _stream;
        ClientStatus _status;
        string _ip;
        int _port;
        #endregion

        #region Singleton
        private static SimulatorClient instance = null;
        public static SimulatorClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SimulatorClient();
                    instance._status = ClientStatus.standby;
                    instance._client = new TcpClient();
                }
                return instance;
            }
        }
        #endregion

        public void Connect(string ip, int port)
        {
            if (_status == ClientStatus.running)
            {
                if (_ip == ip && _port == port)
                {
                    return;
                }
                else
                {
                    Disconnect();
                }
            }

            _ip = ip;
            _port = port;
            _client.Connect(ip, port);
            _stream = _client.GetStream();
            _status = ClientStatus.running;
        }

        public void Disconnect()
        {
            _stream.Close();
            _client.Close();
            _client = new TcpClient();
            _status = ClientStatus.standby;
        }

        public float[] GetDataFromSimulator(string query)
        {
            string[] requests = query.Split(',');
            float[] data = new float[requests.Length];

            for (int i = 0; i < requests.Length; i++)
            {
                //sending request for getting data from simulator
                string request = "get " + requests[i] + "\r\n";
                Byte[] buffer = System.Text.Encoding.ASCII.GetBytes(request);
                _stream.Write(buffer, 0, request.Length);

                buffer = new byte[Constants.DefaultBufferSize];
                string responseData = string.Empty;
                Int32 receivedBytesNumber = _stream.Read(buffer, 0, buffer.Length);
                responseData = System.Text.Encoding.ASCII.GetString(buffer, 0, receivedBytesNumber);

                data[i] = float.Parse((responseData.Split('\''))[1]);
            }

            return data;
        }

    }
}