using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Beetle.JT808.Messages
{
    [MessageType(ID = 0x0100)]
    public class ClientRegister 
    {
        [UInt16Handler]
        public ushort Province { get; set; }
        [UInt16Handler]
        public ushort City { get; set; }
        [ASCIIHandler(5)]
        public string Provider { get; set; }
        [ASCIIHandler(8)]
        public string DeviceNumber { get; set; }
        [ASCIIHandler(7)]
        public string DeviceID { get; set; }
        [ByteHandler]
        public byte Color { get; set; }
        [GBKHandler]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 终端型号 
        /// </summary>
        [ASCIIHandler(8)]
        public string DeviceNo { get; set; }
    }

    /// <summary>
    /// 终端注册应答 
    /// </summary>
    [MessageType(ID = 0x8100)]
    public class RegisterResponseMessage
    {
        /// <summary>
        /// 应答流水号 
        /// </summary>
        [UInt16Handler]
        public ushort No { get; set; }
        /// <summary>
        /// 结果 
        /// </summary>
        [ByteHandler]
        public RegisterResult Result { get; set; }
        /// <summary>
        /// 鉴权码 
        /// </summary>
        [GBKHandler]
        public string Signature { get; set; }
    }

    [MessageType(NoBody = true, ID = 0x0003)]
    public class RegisterCancelMessage
    {
    }

    public enum RegisterResult : byte
    {
        成功 = 0,
        车辆已被注 = 1,
        数据库中无该车辆 = 2,
        终端已被注册 = 3,
        数据库中无该终端 = 4,
    }
}
