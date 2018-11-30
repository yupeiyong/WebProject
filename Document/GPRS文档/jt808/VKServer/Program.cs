using System;
using System.Text;
using System.Threading;
using Beetle.Express;
using Beetle.JT808;
using Beetle.JT808.Messages;
using Newtonsoft.Json;

namespace VKServer
{
    /// <summary>
    /// 微科测试项目
    /// </summary>
    class Program : IServerHandler
    {
        private static Beetle.Express.IServer _mServer;

        static void Main(string[] args)
        {
            Beetle.JT808.Serializes.SerializerFactory.Init();
            _mServer = ServerFactory.CreateTCP();
            Console.WriteLine("输入连接端口");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return;
            var port = 0;
            if (int.TryParse(input, out port))
            {
                Console.WriteLine("地址：" + _mServer.Host + ",端口：" + port);
                _mServer.Port = port;
                _mServer.Handler = new Program();
                _mServer.Open(false, false, false, 1, 1, "VK");
                SaveLogsService.WriteLog("VK server start!");
            }

            Console.ReadKey();
        }

        public void Connect(Beetle.Express.IServer server, Beetle.Express.ChannelConnectEventArgs e)
        {

        }

        public void Disposed(Beetle.Express.IServer server, Beetle.Express.ChannelEventArgs e)
        {

        }

        public void Error(Beetle.Express.IServer server, Beetle.Express.ErrorEventArgs e)
        {
            SaveLogsService.WriteLog(e.Error.Message);
        }

        public void Opened(Beetle.Express.IServer server)
        {

        }

        public void Receive(Beetle.Express.IServer server, Beetle.Express.ChannelReceiveEventArgs e)
        {
            IProtocolBuffer protocolbuffer;
            if (e.Channel.Tag == null)
            {
                protocolbuffer = ProtocolBufferPool.Default.Pop();
                e.Channel.Tag = protocolbuffer;
            }
            else
            {
                protocolbuffer = (IProtocolBuffer)e.Channel.Tag;
            }
            int offset = 0; int count = e.Data.Count;
            while (count > 0)
            {
                int readcout = protocolbuffer.Import(e.Data.Array, 0, count);
                if (readcout > 0)
                {
                    SendPosToServer.SubmitBase(protocolbuffer.ToString());
                    try
                    {
                        var message = MessageFactory.MessageFromBuffer(protocolbuffer);
                        if (message != null && message.Body != null)
                        {
                            SaveLogsService.WriteLog("Start : --------------------");
                            SaveLogsService.WriteLog("消息ID : " + message.ID);
                            SaveLogsService.WriteLog("终端SIM : " + message.SIM);
                            SaveMsg(message, server, e, protocolbuffer);
                            SaveLogsService.WriteLog("End : --------------------");
                        }
                    }
                    catch (Exception ex)
                    {
                        SaveLogsService.WriteLog("IProtocolBuffer : " + protocolbuffer);
                        SaveLogsService.WriteLog("errorInfo : " + ex.Message);
                        SaveLogsService.WriteLog("errorInfo : " + ex.StackTrace);
                    }
                    finally
                    {
                        ProtocolBufferPool.Default.Push(protocolbuffer);
                        protocolbuffer = ProtocolBufferPool.Default.Pop();
                    }
                    offset += readcout;
                    count -= readcout;
                }
            }
        }

        private void SaveMsg(Message message, Beetle.Express.IServer server, Beetle.Express.ChannelReceiveEventArgs e, IProtocolBuffer protocolbuffer)
        {
            //SaveLogsService.WriteLog("Body类名 : " + message.Packet);
            if (message.Body is ClientRegister)
            {
                var msg = message.GetBody<ClientRegister>();
                SaveLogsService.WriteLog("省ID : " + msg.Province);
                SaveLogsService.WriteLog("市县区ID : " + msg.City);
                SaveLogsService.WriteLog("Color : " + msg.Color);
                SaveLogsService.WriteLog("终端ID : " + msg.DeviceID);
                SaveLogsService.WriteLog("终端型号 : " + msg.DeviceNo);
                SaveLogsService.WriteLog("制造商 : " + msg.Provider);
                SaveLogsService.WriteLog("车辆颜色 : " + msg.Color);
                SendBackToReg(message, server, e, protocolbuffer);
            }
            else if (message.Body is ClientSignature)
            {
                var msg = message.GetBody<ClientSignature>();
                SaveLogsService.WriteLog("Signature : " + msg.Signature);
                SendBackToReg(message, server, e, protocolbuffer, 1);
            }
            else if (message.Body is ClientResponse)
            {
                var msg = message.GetBody<ClientResponse>();
                SaveLogsService.WriteLog("BussinessNO : " + msg.BussinessNO);
                SaveLogsService.WriteLog("ResultID : " + msg.ResultID);
                SaveLogsService.WriteLog("BussinessNO : " + msg.Result);
                SendBackToReg(message, server, e, protocolbuffer, 1);
            }
            else if (message.Body is ClientPostion)//坐标上报
            {
                var postion = message.GetBody<ClientPostion>();
                SaveLogsService.WriteLog("ID : " + message.ID);
                SaveLogsService.WriteLog("SIM : " + message.SIM);
                SaveLogsService.WriteLog("Latitude : " + postion.Latitude);
                SaveLogsService.WriteLog("Longitude : " + postion.Longitude);
                SaveLogsService.WriteLog("speed : " + postion.Speed);
                SaveLogsService.WriteLog("Time : " + postion.Time.ToString("yyyy-MM-dd HH:mm:ss"));
                SaveLogsService.WriteLog("Status : " + (postion.Status.ACC ? "启动" : "关闭"));
                var dto = new PostionDto
                {
                    Sim = message.SIM,
                    Latitude = postion.Latitude.ToString(),
                    Longitude = postion.Longitude.ToString(),
                    Direction = postion.Direction.ToString(),
                    Speed = postion.Speed.ToString(),
                    Time = postion.Time.ToString("yyyy-MM-dd HH:mm:ss"),
                    Status = postion.Status.ACC.ToString()
                };
                var result = SendPosToServer.SubmitPos(dto);
                if (result != "0")
                    SaveLogsService.WriteLog("Upload : " + result);
                SendBackToReg(message, server, e, protocolbuffer, 1);
            }
            else
            {
                //SendBackToReg(message, server, e, protocolbuffer, 1);
            }
        }

        private void SendBackToReg(Message message, Beetle.Express.IServer server, Beetle.Express.ChannelReceiveEventArgs e, IProtocolBuffer protocolbuffer, int typeToBack = 0)
        {
            e.Channel.Tag = protocolbuffer;
            var result = GetTyepToBack(message, typeToBack);
            Beetle.Express.Data data = new Beetle.Express.Data(result.Array, result.Length);
            data.Tag = result;
            server.Send(data, e.Channel);
        }

        private static IProtocolBuffer GetTyepToBack(Message message, int typeToBack)
        {
            IProtocolBuffer result = new ProtocolBuffer();
            if (typeToBack == 0)
            {
                var sig = SendPosToServer.QuerySig(message.SIM);
                //if (string.IsNullOrWhiteSpace(sig))
                //{
                //    SaveLogsService.WriteLog("注册失败，设备号 : " + message.SIM);
                //    result = MessageFactory.MessateToBuffer<ClientRegisterResponse>(
                //        message.BussinessNO, message.SIM, (m, b) =>
                //        {
                //            b.BusinessNO = message.BussinessNO;
                //            b.Result = 2; //数据库中无该终端
                //        });
                //}
                //else
                //{
                //    result = MessageFactory.MessateToBuffer<ClientRegisterResponse>(
                //        message.BussinessNO, message.SIM, (m, b) =>
                //        {
                //            b.BusinessNO = message.BussinessNO;
                //            b.Result = 0;//注册成功
                //            b.Signature = sig;
                //        });
                //    result.Postion = 0;
                //}
                result = MessageFactory.MessateToBuffer<ClientRegisterResponse>(
                    message.BussinessNO, message.SIM, (m, b) =>
                    {
                        b.BusinessNO = message.BussinessNO;
                        b.Result = 0x0;
                        b.Signature = "201811111111";
                    });
                result.Postion = 0;
            }
            else if (typeToBack == 1)
            {
                result = MessageFactory.MessateToBuffer<CenterResponseMessage>(
                    message.ID, message.SIM, (m, b) =>
                    {
                        b.No = message.BussinessNO;
                        b.ResultID = message.ID;
                        b.Result = ResultType.Success;
                    });
            }
            return result;
        }

        public void SendCompleted(Beetle.Express.IServer server, Beetle.Express.ChannelSendEventArgs e)
        {
            IProtocolBuffer buffer = (IProtocolBuffer)e.Data.Tag;
            ProtocolBufferPool.Default.Push(buffer);
        }
    }
}
