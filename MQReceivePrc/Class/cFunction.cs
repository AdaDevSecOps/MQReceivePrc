using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using MQReceivePrc.Class;
using System.Data.Common;
using System.Net.Http;
using RabbitMQ.Client;
using System.Configuration;
using MQReceivePrc.Models.Receive;
using MQReceivePrc.Models.ResponseProgress;
using Newtonsoft.Json;

namespace MQReceivePrc.Class
{
    class cFunction
    {
        
        #region Encrypt - Decrypt
        private static TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();
        public string tCS_CNEncDec = "SOFTXada";
        public static string tVB_CNQueueName;
        //public string tCS_CNEncDec = "adasoft";
        private static byte[] TruncateHash(string ptkey, int piLength)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            // Hash the key.
            byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(ptkey);
            byte[] hash = sha1.ComputeHash(keyBytes);
            var oldHash = hash;
            hash = new byte[piLength - 1 + 1];

            // Truncate or pad the hash.
            if (oldHash != null)
                Array.Copy(oldHash, hash, Math.Min(piLength - 1 + 1, oldHash.Length));
            return hash;
        }
        public string SP_EncryptData(string ptPlaintext, string ptkey)
        {

            // Convert the plaintext string to a byte array.
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(ptPlaintext);

            // Initialize the crypto provider.
            TripleDes.Key = TruncateHash(ptkey, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize / 8);

            // Create the stream.
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the encoder to write to the stream.
            CryptoStream encStream = new CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();

            // Convert the encrypted stream to a printable string.
            return Convert.ToBase64String(ms.ToArray());
        }
        public string SP_DecryptData(string ptEncryptedtext, string ptkey)
        {

            // Convert the encrypted text string to a byte array.

            // Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

            // Initialize the crypto provider.
            TripleDes.Key = TruncateHash(ptkey, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize / 8);

            byte[] encryptedBytes;
            try
            {
                encryptedBytes = Convert.FromBase64String(ptEncryptedtext);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            // Create the stream.
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the decoder to write to the stream.
            CryptoStream decStream = new CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();

            // Convert the plaintext stream to a string.
            return System.Text.Encoding.Unicode.GetString(ms.ToArray());
        }
        #endregion

        public static void C_LOGxKeepLogErr(string ptMsg,string ptFunction)
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + tVB_CNQueueName + " " + ptFunction + " : Error = " + ptMsg);
                new cLog().C_WRTxLog(ptFunction, ptMsg);
            }
            catch(Exception oEx)
            {
                new cLog().C_WRTxLog("cFunction", "C_LOGxKeepLogErr : Error/ " + oEx.Message);
            }
        }

        /// <summary>
        /// Arm 65-08-22
        /// Write Queue Process Monitor
        /// </summary>
        /// <param name="ptQueueID"></param>
        /// <param name="ptMessage"></param>
        public static void C_LOGxConsoleLogMonitor(string ptQueueID, string ptMessage, string ptFunction = "", bool pbAlwLogFile = false)
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " [" + ptQueueID + "] " + ptMessage);
                if (pbAlwLogFile) new cLog().C_WRTxLogMonitor(ptFunction, ptMessage, pbAlwLogFile);
            }
            catch (Exception oEx)
            {
                new cLog().C_WRTxLog("cFunction", "C_LOGxConsoleLogMonitor : Error/" + oEx.Message); //*Arm 65-06-16
            }
        }

        public static void C_PRCxMQResponsce(string ptResMQ,string ptDocNo,string ptUser, string ptProg ,out string ptErrMsg)
        {
            ConnectionFactory oFactory;
            cFunction oFunc = new cFunction();
            string tQueueName = ptResMQ + "_" + ptDocNo + "_" + ptUser;
            try
            {
                oFactory = new ConnectionFactory();
                oFactory.HostName = cMQReceiver.oC_Config.oC_RabbitMQ.tMQHostName;
                oFactory.UserName = cMQReceiver.oC_Config.oC_RabbitMQ.tMQUserName;
                oFactory.Password = cMQReceiver.oC_Config.oC_RabbitMQ.tMQPassword;
                oFactory.VirtualHost = cMQReceiver.oC_Config.oC_RabbitMQ.tMQVirtualHost;
                using (var oConn = oFactory.CreateConnection())
                {
                    using (var oChannel = oConn.CreateModel())
                    {
                        //string tJson = JsonConvert.SerializeObject(ptProg);
                        var body = Encoding.UTF8.GetBytes(ptProg);
                        oChannel.QueueDeclare(tQueueName, true, false, false, null);
                        oChannel.BasicPublish("", tQueueName, false, null, body);
                        Console.WriteLine("Response Queues : " + tQueueName + " Progress " + ptProg + " %");

                        ptErrMsg = "";
                    }
                }
                

            }
            catch (Exception oEx)
            {
                ptErrMsg = oEx.Message.ToString();
            }
            finally
            {
                oFactory = null;
            }
        }

        /// <summary>
        /// *Arm 65-08-23
        /// Response Progress
        /// </summary>
        /// <param name="pnProg"></param>
        /// <param name="ptDocNo"></param>
        /// <param name="ptStatus">Status 0:Wait, 1:Success, 2:false</param>
        /// <param name="ptUser"></param>
        /// <param name="ptErrMsg"></param>
        /// <returns></returns>
        public static void C_PRCxResProgress(string ptResMQ, int pnProg, string ptDocNo, string ptStatus, string ptUser, string ptErrMsg = "")
        {
            cmlResProgress oData = new cmlResProgress();
            string tQueueName = "";
            string tErrMsg = "";
            try
            {
                tQueueName = ptResMQ + "_" + ptDocNo + "_" + ptUser;
                oData.rnProg = pnProg;
                oData.rtDocNo = ptDocNo;
                oData.rtMsgError = ptErrMsg;
                if (pnProg == 100)
                {
                    oData.rtStatus = ptStatus;  //0:Wait, 1:Success, 2:false
                }
                else
                {
                    oData.rtStatus = "0";   //0:Wait, 1:Success, 2:false
                }
                string tMsgJson = JsonConvert.SerializeObject(oData);
                cFunction.C_PRCxMQPublish(tQueueName, tMsgJson, out tErrMsg, true);
                
            }
            catch (Exception oEx)
            {
                ptErrMsg = oEx.Message.ToString();
                cFunction.C_LOGxKeepLogErr(ptErrMsg, "C_PRCbResProgress");
            }
            finally
            {
                oData = null;
            }
        }

        //public static void C_PRCxMQPublish(string ptQueueName, string ptMessage, out string ptErrMsg) //*Arm 65-09-02 -Comment Code
        public static void C_PRCxMQPublish(string ptQueueName, string ptMessage, out string ptErrMsg, bool pbDurable = false) //*Arm 65-09-02 -[CR-Oversea] เพิ่ม pbDurable
        {
            ConnectionFactory oFactory;
            cFunction oFunc = new cFunction();
            string tQueueName = ptQueueName;
            try
            {
                oFactory = new ConnectionFactory();
                oFactory.HostName = cMQReceiver.oC_Config.oC_RabbitMQ.tMQHostName;
                oFactory.UserName = cMQReceiver.oC_Config.oC_RabbitMQ.tMQUserName;
                oFactory.Password = cMQReceiver.oC_Config.oC_RabbitMQ.tMQPassword;
                oFactory.VirtualHost = cMQReceiver.oC_Config.oC_RabbitMQ.tMQVirtualHost;
                using (var oConn = oFactory.CreateConnection())
                {
                    using (var oChannel = oConn.CreateModel())
                    {
                        var body = Encoding.UTF8.GetBytes(ptMessage);
                        //oChannel.QueueDeclare(tQueueName, false, false, false, null);  //*Arm 65-09-02 -Comment Code
                        oChannel.QueueDeclare(tQueueName, pbDurable, false, false, null); //*Arm 65-09-02 -[CR-Oversea]
                        oChannel.BasicPublish("", tQueueName, false, null, body);
                        ptErrMsg = "";
                    }
                }
            }
            catch (Exception oEx)
            {
                ptErrMsg = oEx.Message.ToString();
            }
            finally
            {
                oFactory = null;
            }
        }

        /// <summary>
        /// *Arm 63-02-25
        /// Publish Message to Exchange
        /// </summary>
        /// <param name="ptExchange">Exchange Name</param>
        /// <param name="ptRoute">Routing</param>
        /// <param name="ptExchangeMode">direct,fanout,topic,headers</param>
        /// <param name="ptMessage">Message to send out</param>
        /// <param name="ptErrMsg">Error Message</param>
        
        //*Net 63-02-27 แก้ให้เหมือนกับ ver statdose
        public static void C_PRCxMQPublishExchange(string ptExchange, string ptRoute, string ptExchangeMode, string ptMessage, out string ptErrMsg)
        {
            ConnectionFactory oFactory;
            cFunction oFunc = new cFunction();
            string tExchangeName = ptExchange;
            string tRoute = ptRoute;
            string tExchangeMode = ptExchangeMode;
            try
            {
                oFactory = new ConnectionFactory();
                oFactory.HostName = cMQReceiver.oC_Config.oC_RabbitMQ.tMQHostName;
                oFactory.UserName = cMQReceiver.oC_Config.oC_RabbitMQ.tMQUserName;
                oFactory.Password = cMQReceiver.oC_Config.oC_RabbitMQ.tMQPassword;
                oFactory.VirtualHost = cMQReceiver.oC_Config.oC_RabbitMQ.tMQVirtualHost;
                using (var oConn = oFactory.CreateConnection())
                {
                    using (var oChannel = oConn.CreateModel())
                    {
                        var body = Encoding.UTF8.GetBytes(ptMessage);
                        oChannel.ExchangeDeclare(tExchangeName, tExchangeMode, false, false, null);
                        oChannel.BasicPublish(tExchangeName, ptRoute, false, null, body);
                        ptErrMsg = "";
                    }
                }
            }
            catch (Exception oEx)
            {
                ptErrMsg = oEx.Message.ToString();
            }
            finally
            {
                oFactory = null;
            }
        }


        /// <summary>
        /// Clear Memory
        /// </summary>
        public void C_CLExMemory()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception oEx)
            { }
        }
    }
}
