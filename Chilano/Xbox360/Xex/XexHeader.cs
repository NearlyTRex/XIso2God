﻿namespace Chilano.Xbox360.Xex
{
    using Chilano.Xbox360.IO;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class XexHeader : Dictionary<XexInfoFields, XexInfoField>
    {
        public XexHeader(CBinaryReader br)
        {
            base.Clear();
            try
            {
                br.Seek(0L, SeekOrigin.Begin);
                if (Encoding.ASCII.GetString(br.ReadBytes(4)) == "XEX2")
                {
                    br.Seek(4L, SeekOrigin.Begin);
                    base.Add(XexInfoFields.ModuleFlags, new XexModuleFlags(br.ReadUInt32()));
                    br.Seek(8L, SeekOrigin.Begin);
                    base.Add(XexInfoFields.CodeOffset, new XexCodeOffset(br.ReadUInt32()));
                    br.Seek(0x10L, SeekOrigin.Begin);
                    base.Add(XexInfoFields.CertifcateOffset, new XexCertifcateOffset(br.ReadUInt32()));
                    br.Seek(20L, SeekOrigin.Begin);
                    uint num = br.ReadUInt32();
                    for (int i = 0; i < num; i++)
                    {
                        uint num3 = BitConverter.ToUInt32(br.ReadBytes(4), 0);
                        if (num3 == BitConverter.ToUInt32(XexResourceInfo.Signature, 0))
                        {
                            base.Add(XexInfoFields.ResourceInfo, new XexResourceInfo(br.ReadUInt32()));
                        }
                        else if (num3 == BitConverter.ToUInt32(XexCompressionInfo.Signature, 0))
                        {
                            base.Add(XexInfoFields.CompressionInfo, new XexCompressionInfo(br.ReadUInt32()));
                        }
                        else if (num3 == BitConverter.ToUInt32(XexExecutionInfo.Signature, 0))
                        {
                            base.Add(XexInfoFields.ExecutionInfo, new XexExecutionInfo(br.ReadUInt32()));
                        }
                        else if (num3 == BitConverter.ToUInt32(XexBaseFileFormat.Signature, 0))
                        {
                            base.Add(XexInfoFields.BaseFileFormat, new XexBaseFileFormat(br.ReadUInt32()));
                        }
                        else if (num3 == BitConverter.ToUInt32(XexBaseFileTimestamp.Signature, 0))
                        {
                            base.Add(XexInfoFields.BaseFileTimestamp, new XexBaseFileTimestamp(br.ReadUInt32()));
                        }
                        else if (num3 == BitConverter.ToUInt32(XexOriginalName.Signature, 0))
                        {
                            base.Add(XexInfoFields.OriginalName, new XexOriginalName(br.ReadUInt32()));
                        }
                        else if (num3 == BitConverter.ToUInt32(XexRatingsInfo.Signature, 0))
                        {
                            base.Add(XexInfoFields.RatingsInfo, new XexRatingsInfo(br.ReadUInt32()));
                        }
                        else if (num3 == BitConverter.ToUInt32(XexModuleFlags.Signature, 0))
                        {
                            base.Add(XexInfoFields.SystemFlags, new XexModuleFlags(br.ReadUInt32()));
                        }
                        else
                        {
                            br.ReadUInt32();
                        }
                    }
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("EndOfStreamException when trying to read XEX file header.");
            }
        }
    }
}

