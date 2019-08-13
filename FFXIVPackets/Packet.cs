using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FFXIV.Packets
{
    public class Packet
    {
        protected MemoryStream memoryStream;
        protected BinaryWriter writer;
        protected BinaryReader reader;
        public Byte[] RawBytes { get; set; }
        public UInt32 Source { get; set; }
        public Packet()
        {
        }
        public IEnumerable<Byte> Bytes
        {
            get
            {
                return ToArray().ToList();
            }
            set
            {
                RawBytes = value.ToArray();
                FromBytes(value.ToArray());
            }
        }
        public Int64 NumBytesRead { get; set; }
        public virtual void FromBytes(Byte[] Bytes)
        {
            memoryStream = new MemoryStream(Bytes);
            reader = new BinaryReader(memoryStream);
            var fields = GetType().GetFields().OrderBy(f => BaseCount(f.DeclaringType.BaseType)).ThenBy(f => f.MetadataToken);
            var numInList = 0;
            foreach (var f in fields)
            {
                if (reader.BaseStream.Position == reader.BaseStream.Length)
                    break;
                var fieldType = f.FieldType;
                if (f.FieldType.IsEnum)
                {
                    fieldType = Enum.GetUnderlyingType(f.FieldType);
                }
                if (fieldType.Equals(typeof(Byte)))
                    f.SetValue(this, reader.ReadByte());
                else if (fieldType.Equals(typeof(UInt16)))
                    f.SetValue(this, reader.ReadUInt16());
                else if (fieldType.Equals(typeof(UInt32)))
                    f.SetValue(this, reader.ReadUInt32());
                else if (fieldType.Equals(typeof(UInt64)))
                    f.SetValue(this, reader.ReadUInt64());
                else if (fieldType.Equals(typeof(Single)))
                    f.SetValue(this, reader.ReadSingle());
                else if (fieldType.Equals(typeof(String)))
                {
                    var hardcodeLength = (String)f.GetValue(this);
                    f.SetValue(this, Encoding.ASCII.GetString(reader.ReadBytes(hardcodeLength.Length)).Replace("\0", ""));
                    /*List<Byte> stringBytes = new List<Byte>();
                    Byte stringByte;
                    do
                    {
                        stringByte = reader.ReadByte();
                        stringBytes.Add(stringByte);
                    } while (stringByte != 0);
                    var finalString = stringBytes.Take(stringBytes.Count() - 1);
                    f.SetValue(this, Encoding.ASCII.GetString(finalString.ToArray()));*/
                }
                else if (fieldType.Equals(typeof(DateTime)))
                    f.SetValue(this, Utils.GetTimeFromUnix(reader.ReadUInt32()));
                //f.SetValue(this, DateTime.FromBinary((Int64)reader.ReadUInt64()));
                else if (fieldType.ToString().Contains("List"))
                {
                    dynamic list = Activator.CreateInstance(fieldType);
                    var listType = fieldType.GetGenericArguments()[0];
                    dynamic baseItem = Activator.CreateInstance(listType);
                    var baseItemLength = 1;
                    if (baseItem.GetType().IsSubclassOf(typeof(Packet)))
                        baseItemLength = baseItem.ToArray().Length;
                    else
                        baseItemLength = System.Runtime.InteropServices.Marshal.SizeOf(baseItem);
                    var origList = f.GetValue(this);
                    var listCount = numInList;
                    if (origList != null)
                    {
                        listCount = 0;
                        dynamic castedList = (IEnumerable)origList;
                        foreach (var o in castedList)
                            listCount++;
                    }
                    for (int i = 0; i < listCount; i++)
                    {
                        dynamic item = Activator.CreateInstance(listType);
                        if (baseItem.GetType().IsSubclassOf(typeof(Packet)))
                        {
                            List<Byte> itemBytes = new List<Byte>();
                            for (int j = 0; j < baseItemLength; j++)
                                itemBytes.Add(reader.ReadByte());
                            item.Bytes = itemBytes;
                        }
                        else
                        {
                            if (baseItemLength == 1)
                                item = reader.ReadByte();
                            else if (baseItemLength == 2)
                                item = reader.ReadUInt16();
                            else if (baseItemLength == 4)
                                item = reader.ReadUInt32();
                            else if (baseItemLength == 8)
                                item = reader.ReadUInt64();
                        }
                        list.Add(item);
                    }
                    f.SetValue(this, list);
                }
                else if (fieldType.IsSubclassOf(typeof(Packet)))
                {
                    dynamic item = Activator.CreateInstance(fieldType);
                    item.Bytes = Bytes.Skip((int)reader.BaseStream.Position).ToList();
                    f.SetValue(this, item);
                    reader.BaseStream.Position += item.ToArray().Length;
                }
                if (f.Name.Contains("Num") || f.Name.Contains("Length"))
                {
                    numInList = Convert.ToInt32(f.GetValue(this));
                }
            }
            NumBytesRead = reader.BaseStream.Position;
        }
        public Byte BaseCount(Type type)
        {
            Byte c = 0;
            while (type.BaseType != null)
            {
                c++;
                type = type.BaseType;
            }
            return c;
        }
        public virtual Byte[] ToArray()
        {
            memoryStream = new MemoryStream();
            writer = new BinaryWriter(memoryStream);
            var fields = GetType().GetFields().OrderBy(f => BaseCount(f.DeclaringType.BaseType)).ThenBy(f => f.MetadataToken);
            var lengthFieldStart = writer.BaseStream.Position;
            var lengthFieldLength = 2;
            foreach (var f in fields)
            {
                if (f.Name == "Length")
                {
                    lengthFieldStart = writer.BaseStream.Position;
                    if (f.FieldType == typeof(Byte))
                        lengthFieldLength = 1;
                }
                var val = f.GetValue(this);
                if (f.FieldType.IsEnum)
                {
                    if (f.Name == "PacketId")
                        val = Enum.Parse(f.FieldType, GetType().Name.Replace("C2S", "").Replace("S2C", ""));
                    val = Convert.ChangeType(val, Type.GetTypeCode(Enum.GetUnderlyingType(f.FieldType)));
                }
                if (val is Byte)
                    writer.Write((Byte)val);
                else if (val is Boolean)
                    writer.Write(Convert.ToByte(val));
                else if (val is UInt16)
                    writer.Write((UInt16)val);
                else if (val is UInt32)
                    writer.Write((UInt32)val);
                else if (val is Int16)
                    writer.Write((Int16)val);
                else if (val is Int32)
                    writer.Write((Int32)val);
                //else if (val.GetType().IsEnum)
                //    writer.Write((UInt32)val);
                else if (val is UInt64)
                    writer.Write((UInt64)val);
                else if (val is Single)
                    writer.Write((Single)val);
                //else if (val is Byte[])
                //    writer.Write((Byte[])val);
                else if (val is String)
                    writer.Write(Encoding.UTF8.GetBytes((String)val));
                else if (val is DateTime)
                    writer.Write((UInt32)((DateTime)val).GetUnixTime());
                // writer.Write(((DateTime)val).ToBinary());
                else if (f.FieldType.ToString().Contains("List"))
                {
                    dynamic list = (IEnumerable)val;
                    foreach (var o in list)
                    {
                        if (o.GetType().IsSubclassOf(typeof(Packet)))
                            writer.Write(o.ToArray());
                        else
                            writer.Write(o);
                    }
                }
                else if (f.FieldType.IsSubclassOf(typeof(Packet)) || f.FieldType == typeof(Packet))
                    writer.Write(((Packet)val).ToArray());
                else
                {
                    throw new Exception("need to parse this type : " + f.FieldType);
                }
            }
            Byte[] ByteArray = memoryStream.ToArray();
            if (fields.Count(f => f.Name == "Length") > 0)
            {
                Array.Copy(BitConverter.GetBytes((UInt16)ByteArray.Length), 0, ByteArray, lengthFieldStart, lengthFieldLength);
            }
            return ByteArray;
        }
        public override String ToString()
        {
            var fields = GetType().GetFields().OrderBy(f => BaseCount(f.DeclaringType.BaseType)).ThenBy(f => f.MetadataToken);
            var readable = "";
            foreach (var f in fields)
            {
                var val = f.GetValue(this);
                readable += f.Name + "(" + val?.GetType() + ") = " + val + "\n";
                //readable += "\t" + f.Name + "(" + val?.GetType() + ") = " + val + "\n";
            }
            return readable;
        }
        public static Byte[] ToPayload(List<Packet> packets)
        {
            var b = new List<Byte> { 0x52, 0x52, 0xA0, 0x41, 0xFF, 0x5D, 0x46, 0xE2, 0x7F, 0x2A, 0x64, 0x4D, 0x7B, 0x99, 0xC4, 0x75 };
            return b.ToArray();
        }
    }
}