using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808.Messages
{

    [MessageType(ID = 0x0008)]
    public class CenterResponse
    {
        [UInt16Handler]
        public ushort BussinessNO { get; set; }
        [UInt16Handler]
        public ushort ResultID { get; set; }
        [ByteHandler]
        public ResultType Result { get; set; }
    }

    /// <summary>
    /// 平台通用应答
    /// </summary>
    [MessageType(ID = 0x8001)]
    public class CenterResponseMessage
    {
        /// <summary>
        /// 应答流水号 (对应的终端消息的流水号)
        /// </summary>
        [UInt16Handler]
        public ushort No { get; set; }
        /// <summary>
        /// 应答 ID  (对应的终端消息的 ID )
        /// </summary>
        [UInt16Handler]
        public ushort ResultID { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        [ByteHandler]
        public ResultType Result { get; set; }
    }
}
