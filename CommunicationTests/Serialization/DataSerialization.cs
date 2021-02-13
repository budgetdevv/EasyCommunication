﻿using EasyCommunication.Serialization;
using EasyCommunication.SharedTypes;
using ProtoBuf;
using System.Drawing;
using System.Threading.Tasks;
using Xunit;

namespace CommunicationTests.Serialization
{
    public class DataSerialization
    {
        [Fact]
        public async Task SerializeShort()
        {
            byte[] expected = new byte[] { (byte)DataType.Short, 2, 0, 0, 0, 2, 0 };
            byte[] result = SerializationHelper.GetBuffer((short)2, DataType.Short);

            Assert.Equal(expected, result);
        }
        [Fact]
        public async Task SerializeInteger()
        {
            byte[] expected = new byte[] { (byte)DataType.Int, 4, 0, 0, 0, 2, 0, 0, 0 };
            byte[] result = SerializationHelper.GetBuffer(2, DataType.Int);

            Assert.Equal(expected, result);
        }
        [Fact]
        public async Task SerializeLong()
        {
            byte[] expected = new byte[] { (byte)DataType.Long, 8, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0 };
            byte[] result = SerializationHelper.GetBuffer((long)2, DataType.Long);

            Assert.Equal(expected, result);
        }
        [Fact]
        public async Task SerializeFloat()
        {
            byte[] expected = new byte[] { (byte)DataType.Float, 4, 0, 0, 0, 205, 204, 140, 63 };
            byte[] result = SerializationHelper.GetBuffer(1.1f, DataType.Float);

            Assert.Equal(expected, result);
        }
        [Fact]
        public async Task SerializeDouble()
        {
            byte[] expected = new byte[] { (byte)DataType.Double, 8, 0, 0, 0, 154, 153, 153, 153, 153, 153, 241, 63 };
            byte[] result = SerializationHelper.GetBuffer(1.1d, DataType.Double);

            Assert.Equal(expected, result);
        }
        [Fact]
        public async Task SerializeBool()
        {
            byte[] expected = new byte[] { (byte)DataType.Bool, 1, 0, 0, 0, 1 };
            byte[] result = SerializationHelper.GetBuffer(true, DataType.Bool);

            Assert.Equal(expected, result);
        }
        [Fact]
        public async Task SerializeString()
        {
            byte[] expected = new byte[] { (byte)DataType.String, 5, 0, 0, 0, 72, 101, 108, 108, 111 };
            byte[] result = SerializationHelper.GetBuffer("Hello", DataType.String);

            Assert.Equal(expected, result);
        }
        [Fact]
        public async Task SerializeJsonString()
        {
            Point test = new Point(1, 1);
            byte[] expected = new byte[] { (byte)DataType.JsonString, 6, 0, 0, 0, 34, 49, 44, 32, 49, 34 };
            byte[] result = SerializationHelper.GetBuffer(test, DataType.JsonString);

            Assert.Equal(expected, result);
        }
        [Fact]
        public async Task SerializeProtoBuf()
        {
            ProtoBufType test = new ProtoBufType(1, 1);
            byte[] expected = new byte[] { (byte)DataType.ProtoBuf, 4, 0, 0, 0, 8, 1, 16, 1 };
            byte[] result = SerializationHelper.GetBuffer(test, DataType.ProtoBuf);

            Assert.Equal(expected, result);
        }
        [Fact]
        public async Task SerializeByteArray()
        {
            byte[] data = new byte[] { 1, 1, 1 };
            byte[] expected = new byte[] { (byte)DataType.ByteArray, 3, 0, 0, 0, 1, 1, 1 };
            byte[] result = SerializationHelper.GetBuffer(data, DataType.ByteArray);

            Assert.Equal(expected, result);
        }
    }
    [ProtoContract]
    internal class ProtoBufType
    {
        [ProtoMember(1)] public int Num1 { get; set; }
        [ProtoMember(2)] public int Num2 { get; set; }
        public ProtoBufType(int num1, int num2)
        {
            Num1 = num1;
            Num2 = num2;
        }
    }
}
