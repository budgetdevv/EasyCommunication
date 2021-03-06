﻿using EasyCommunication.Events.Client.EventArgs;
using EasyCommunication.SharedTypes;
using EasyCommunication.SharedTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Diagnostics;
using EasyCommunication;

namespace CommunicationTests.Events
{
    //Ports 9100 - 9149
    [Collection("Sequential")]
    public class EasyClientEvents
    {
        [Fact]
        public async Task TriggerConnectedToHost()
        {
            for (int i = 0; i < 1; i++)
            {
                int triggered = 0;
                var host = new EasyHost(2000, 9100, IPAddress.Loopback);
                host.Open();
                var client = new EasyClient(500);

                client.EventHandler.ConnectedToHost += delegate (ConnectedToHostEventArgs ev)
                {
                    triggered++;
                };
                client.ConnectToHost(IPAddress.Loopback, 9100);

                //Buffer for connection & event
                await Task.Delay(5);
                Assert.True(triggered == 1);
                client.DisconnectFromHost();
                host.Close();
            }
        }
        
        // In consistent results. Sadly.

        //[Fact]
        //public async Task TriggerDisconnectedFromHost()
        //{
        //    for (int i = 0; i < 1; i++)
        //    {
        //        int triggered = 0;
        //        var host = new EasyHost(2000, 9101, IPAddress.Loopback);
        //        host.Open();
        //        var client = new EasyClient(500);

        //        client.EventHandler.DisconnectedFromHost += delegate (DisconnectedFromHostEventArgs ev)
        //        {
        //            triggered++;
        //        };
        //        client.ConnectToHost(IPAddress.Loopback, 9101);
        //        await Task.Delay(5);
        //        client.DisconnectFromHost();

        //        //Buffer for event
        //        await Task.Delay(200);
        //        Debug.WriteLine(triggered);
        //        Assert.True(triggered == 1);
        //        host.Close();
        //    }
        //}
        [Fact]
        public async Task TriggerReceivedData()
        {
            for (int i = 0; i < 1; i++)
            {
                int triggered = 0;
                var host = new EasyHost(2000, 9102, IPAddress.Loopback);
                host.Open();
                var client = new EasyClient(500);

                client.EventHandler.ReceivedData += delegate (ReceivedDataEventArgs ev)
                {
                    triggered++;
                };
                client.ConnectToHost(IPAddress.Loopback, 9102);

                //Buffer for connection
                await Task.Delay(5);
                QueueStatus status = host.QueueData("Good evening there", host.ClientConnections.Keys.ToArray()[0], DataType.String);

                //Buffer for event
                await Task.Delay(50);

                Assert.True(triggered == 1);
                client.DisconnectFromHost();
                host.Close();
            }
        }
        [Fact]
        public async Task TriggerSendingData()
        {
            for (int i = 0; i < 1; i++)
            {
                int triggered = 0;
                var host = new EasyHost(2000, 9103, IPAddress.Loopback);
                host.Open();
                var client = new EasyClient(500);

                client.EventHandler.SendingData += delegate (SendingDataEventArgs ev)
                {
                    triggered++;
                };
                client.ConnectToHost(IPAddress.Loopback, 9103);
                QueueStatus status = client.QueueData("Good evening there", DataType.String);

                //Buffer for event
                await Task.Delay(25);

                Assert.True(triggered == 1);
                client.DisconnectFromHost();
                host.Close();
            }
        }
        [Fact]
        public async Task ReceivedDataIdentical()
        {
            for (int i = 0; i < 1; i++)
            {
                bool equal = false;
                var host = new EasyHost(1500, 9104, IPAddress.Loopback);
                host.Open();
                var client = new EasyClient(500);
                string data = "Good evening there";

                client.EventHandler.ReceivedData += delegate (ReceivedDataEventArgs ev)
                {
                    equal = (data == Encoding.UTF8.GetString(ev.ReceivedBuffer.Data));
                };

                client.ConnectToHost(IPAddress.Loopback, 9104);
                await Task.Delay(5);
                QueueStatus status = host.QueueData(data, host.ClientConnections.Keys.ToArray()[0], DataType.String);

                //Buffer for event
                await Task.Delay(1500);

                Assert.True(equal);
                client.DisconnectFromHost();
                host.Close();
            }
        }
    }
}
