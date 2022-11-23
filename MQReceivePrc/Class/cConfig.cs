using Microsoft.Extensions.Configuration;
using MQReceivePrc.Class.Standard;
using MQReceivePrc.Models.Config;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace MQReceivePrc.Class
{
    public class cConfig
    {
        public cmlRabbitMQ oC_RabbitMQ;
        public cmlConsolidateDB oC_ConsolidateDB;
        public cmlFoodCourtDB oC_BchFoodCourtDB;
        public cmlFoodCourtDB oC_HQFoodCourtDB;
        public cmlShopDB oC_ShopDB;

        /// <summary>
        /// Load config from App.config
        /// </summary>
        /// 
        /// <param name="ptMsgErr">out Message error.</param>
        /// 
        /// <returns>
        /// true : load config success.<br/>
        /// flase : load config false.
        /// </returns>
        public bool C_CFGbLoadConfig(out string ptMsgErr)
        {
            //NameValueCollection oRabbitMQCfg, oKeyCfg, oConsolidateDBCfg, oBchFCDB, oHQFCDB, oShopDB;
            //*Ton 2021-09-09
            Dictionary<string, string> oRabbitMQCfg = new Dictionary<string, string>();
            Dictionary<string, string> oKeyCfg = new Dictionary<string, string>();
            Dictionary<string, string> oConsolidateDBCfg = new Dictionary<string, string>();
            Dictionary<string, string> oBchFCDB = new Dictionary<string, string>();
            Dictionary<string, string> oHQFCDB = new Dictionary<string, string>();
            Dictionary<string, string> oShopDB = new Dictionary<string, string>();

            cSecurity oSecurity;
            cMS oMsg;
            int nConTme, nComTme;

            try
            {
                //// load configuration from App.cofig
                //oRabbitMQCfg = (NameValueCollection)ConfigurationManager.GetSection("rabbitMQSettings");
                //oKeyCfg = (NameValueCollection)ConfigurationManager.GetSection("keySettings");
                //oConsolidateDBCfg = (NameValueCollection)ConfigurationManager.GetSection("consolidateDBSetting");
                //oBchFCDB = (NameValueCollection)ConfigurationManager.GetSection("bchFCDBSetting");
                //oHQFCDB = (NameValueCollection)ConfigurationManager.GetSection("hqFCDBSetting");
                //oShopDB = (NameValueCollection)ConfigurationManager.GetSection("shopDBSetting");

                //*Ton 2021-09-09 load configuration from appsettings.json
                var oConfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                    .AddEnvironmentVariables().Build();
                oConfig.GetSection("rabbitMQSettings").Bind(oRabbitMQCfg);
                oConfig.GetSection("keySettings").Bind(oKeyCfg);
                oConfig.GetSection("consolidateDBSetting").Bind(oConsolidateDBCfg);
                oConfig.GetSection("bchFCDBSetting").Bind(oBchFCDB);
                oConfig.GetSection("hqFCDBSetting").Bind(oHQFCDB);
                oConfig.GetSection("shopDBSetting").Bind(oShopDB);
                //*********************************************************************************************
                
                //foreach(string key in oRabbitMQCfg.Keys)
                //{
                //    string tEnvKey = $"ENV_{key}";
                //    string tEnvValue = Environment.GetEnvironmentVariable(tEnvKey);
                //    if (!string.IsNullOrEmpty(tEnvValue))
                //    {
                //        oRabbitMQCfg[key] = tEnvValue;
                //    }
                //}

                oMsg = new cMS();
                if (oRabbitMQCfg == null)
                {
                    ptMsgErr = string.Format(oMsg.tMS_CfgNotFound, "rabbit MQ");
                    return false;
                }

                if (oKeyCfg == null)
                {
                    ptMsgErr = string.Format(oMsg.tMS_CfgNotFound, "key");
                    return false;
                }

                if (oConsolidateDBCfg == null)
                {
                    ptMsgErr = string.Format(oMsg.tMS_CfgNotFound, "consolidate database");
                    return false;
                }

                if (oShopDB == null)
                {
                    ptMsgErr = string.Format(oMsg.tMS_CfgNotFound, "shop");
                    return false;
                }

                // set configuration.
                oSecurity = new cSecurity();

                // rabbit MQ.
                oC_RabbitMQ = new cmlRabbitMQ();
                //string mas = oSecurity.C_DATtTripleDESEncryptData("AdaPos5.0Master", oKeyCfg["Key"]); //mas = "oklV+QiMfR0lAF3vadbETvGhv/aKzEWBJpCW+iyWFfM="
                //string doc= oSecurity.C_DATtTripleDESEncryptData("AdaPos5.0Doc", oKeyCfg["Key"]);//doc = "oklV+QiMfR0lAF3vadbEThiIETfkaBEsp3wmnym0gww="
                //string sal = oSecurity.C_DATtTripleDESEncryptData("AdaPos5.0Sale", oKeyCfg["Key"]);//sal = "oklV+QiMfR0lAF3vadbETokcKP/zYuJ3mm1LkIgvyn8="
                //string rpt = oSecurity.C_DATtTripleDESEncryptData("AdaPos5.0Report", oKeyCfg["Key"]);//rpt = "oklV+QiMfR0lAF3vadbETihd4iLKuoMN+7y5PbbwUqE="
                //var yyy = oSecurity.C_DATtTripleDESDecryptData(aaa, oKeyCfg["Key"].Trim());

                oC_RabbitMQ.tMQHostName = oSecurity.C_DATtTripleDESDecryptData(oRabbitMQCfg["HostName"].Trim(), oKeyCfg["Key"].Trim());
                oC_RabbitMQ.tMQUserName = oSecurity.C_DATtTripleDESDecryptData(oRabbitMQCfg["UserName"].Trim(), oKeyCfg["Key"].Trim());
                oC_RabbitMQ.tMQPassword = oSecurity.C_DATtTripleDESDecryptData(oRabbitMQCfg["Password"].Trim(), oKeyCfg["Key"].Trim());
                oC_RabbitMQ.tMQVirtualHost = oSecurity.C_DATtTripleDESDecryptData(oRabbitMQCfg["VirtualHost"].Trim(), oKeyCfg["Key"].Trim());
                oC_RabbitMQ.tMQListQueue = oRabbitMQCfg["ListQueue"].Trim();

                // consolidate database.
                oC_ConsolidateDB = new cmlConsolidateDB();
                oC_ConsolidateDB.tServer = oSecurity.C_DATtTripleDESDecryptData(oConsolidateDBCfg["Server"].Trim(), oKeyCfg["Key"].Trim());
                oC_ConsolidateDB.tUser = oSecurity.C_DATtTripleDESDecryptData(oConsolidateDBCfg["User"].Trim(), oKeyCfg["Key"].Trim());
                oC_ConsolidateDB.tPassword = oSecurity.C_DATtTripleDESDecryptData(oConsolidateDBCfg["Password"].Trim(), oKeyCfg["Key"].Trim());
                oC_ConsolidateDB.tDatabase = oSecurity.C_DATtTripleDESDecryptData(oConsolidateDBCfg["Database"].Trim(), oKeyCfg["Key"].Trim());
                oC_ConsolidateDB.tAuthenMode = oConsolidateDBCfg["AuthenMode"].Trim();
                int.TryParse(oConsolidateDBCfg["ConnectTimeOut"].Trim(), out nConTme);
                int.TryParse(oConsolidateDBCfg["CommandTimeOut"].Trim(), out nComTme);
                oC_ConsolidateDB.nConnectTimeOut = nConTme;
                oC_ConsolidateDB.nCommandTimeOut = nComTme;

                // source food court database.
                if (oBchFCDB != null)
                {
                    if (!string.IsNullOrEmpty(oBchFCDB["Server"].Trim()))
                    {
                        oC_BchFoodCourtDB = new cmlFoodCourtDB();
                        oC_BchFoodCourtDB.tServer = oSecurity.C_DATtTripleDESDecryptData(oBchFCDB["Server"].Trim(), oKeyCfg["Key"].Trim());
                        oC_BchFoodCourtDB.tUser = oSecurity.C_DATtTripleDESDecryptData(oBchFCDB["User"].Trim(), oKeyCfg["Key"].Trim());
                        oC_BchFoodCourtDB.tPassword = oSecurity.C_DATtTripleDESDecryptData(oBchFCDB["Password"].Trim(), oKeyCfg["Key"].Trim());
                        oC_BchFoodCourtDB.tDatabase = oSecurity.C_DATtTripleDESDecryptData(oBchFCDB["Database"].Trim(), oKeyCfg["Key"].Trim());
                        oC_BchFoodCourtDB.tAuthenMode = oBchFCDB["AuthenMode"].Trim();
                        int.TryParse(oBchFCDB["ConnectTimeOut"].Trim(), out nConTme);
                        int.TryParse(oBchFCDB["CommandTimeOut"].Trim(), out nComTme);
                        oC_BchFoodCourtDB.nConnectTimeOut = nConTme;
                        oC_BchFoodCourtDB.nCommandTimeOut = nComTme;
                    }
                }

                // destination food court database.
                if (oHQFCDB != null)
                {
                    if (!string.IsNullOrEmpty(oHQFCDB["Server"].Trim()))
                    {
                        oC_HQFoodCourtDB = new cmlFoodCourtDB();
                        oC_HQFoodCourtDB.tServer = oSecurity.C_DATtTripleDESDecryptData(oHQFCDB["Server"].Trim(), oKeyCfg["Key"].Trim());
                        oC_HQFoodCourtDB.tUser = oSecurity.C_DATtTripleDESDecryptData(oHQFCDB["User"].Trim(), oKeyCfg["Key"].Trim());
                        oC_HQFoodCourtDB.tPassword = oSecurity.C_DATtTripleDESDecryptData(oHQFCDB["Password"].Trim(), oKeyCfg["Key"].Trim());
                        oC_HQFoodCourtDB.tDatabase = oSecurity.C_DATtTripleDESDecryptData(oHQFCDB["Database"].Trim(), oKeyCfg["Key"].Trim());
                        oC_HQFoodCourtDB.tAuthenMode = oHQFCDB["AuthenMode"].Trim();
                        int.TryParse(oHQFCDB["ConnectTimeOut"].Trim(), out nConTme);
                        int.TryParse(oHQFCDB["CommandTimeOut"].Trim(), out nComTme);
                        oC_HQFoodCourtDB.nConnectTimeOut = nConTme;
                        oC_HQFoodCourtDB.nCommandTimeOut = nComTme;
                    }
                }

                // shop.
                oC_ShopDB = new cmlShopDB();
                oC_ShopDB.tAuthenMode = oShopDB["AuthenMode"].Trim();
                int.TryParse(oShopDB["ConnectTimeOut"].Trim(), out nConTme);
                int.TryParse(oShopDB["CommandTimeOut"].Trim(), out nComTme);
                oC_ShopDB.nConnectTimeOut = nConTme;
                oC_ShopDB.nCommandTimeOut = nComTme;
                oC_ShopDB.tUrlAPI2FNWallet = oSecurity.C_DATtTripleDESDecryptData(oShopDB["URLAIP2FNWallet"].Trim(), oKeyCfg["Key"].Trim());
                oC_ShopDB.nRecordPerRound = Convert.ToInt32((string.IsNullOrEmpty(oShopDB["RecordPerRound"].Trim())?"25":oShopDB["RecordPerRound"].Trim()));    //*Em 62-03-21
                oC_ShopDB.tUrlAPI2PSMaster = oShopDB["URLAIP2PSMaster"].Trim(); //*Em 62-10-14
                oC_ShopDB.tServer = oShopDB["Server"].Trim();   //*Em 62-10-15
                oC_ShopDB.tUser = oShopDB["User"].Trim();   //*Em 62-10-15
                oC_ShopDB.tPassword = oSecurity.C_DATtTripleDESDecryptData(oShopDB["Password"].Trim(), oKeyCfg["Key"].Trim());   //*Em 62-10-15
                oC_ShopDB.tDatabase = oShopDB["Database"].Trim();   //*Em 62-10-15

                //foreach (PropertyInfo info in oC_ShopDB.GetType().GetProperties())
                //{
                //    string tEnvKey = $"ENV_{info.Name}";
                //    string tEnvValue = Environment.GetEnvironmentVariable(tEnvKey);
                //    if (!string.IsNullOrEmpty(tEnvValue))
                //    {
                //        info.SetValue(oC_ShopDB, tEnvValue);
                //    }
                //}

                cVB.oVB_ShopDB = oC_ShopDB; //*Em 62-10-24
                cVB.tVB_ConnStr = new cDatabase().C_GETtConnectString(cVB.oVB_ShopDB.tServer, cVB.oVB_ShopDB.tUser, cVB.oVB_ShopDB.tPassword, cVB.oVB_ShopDB.tDatabase, (int)cVB.oVB_ShopDB.nConnectTimeOut, cVB.oVB_ShopDB.tAuthenMode);
                cVB.nVB_CmdTime = (int)cVB.oVB_ShopDB.nCommandTimeOut;
                cVB.tVB_BchCode = new cSP().SP_GETtLocalBch(cVB.tVB_ConnStr, cVB.nVB_CmdTime);
                cVB.tVB_BchHQ = new cSP().SP_GETtBchHQ(cVB.tVB_ConnStr, cVB.nVB_CmdTime);   //*Arm 63-01-07

                //*Arm 63-03-31
                if(oShopDB["StaUseCentralized"].Trim() =="1" )
                {
                    cVB.bVB_StaUseCentralized = true;
                }
                else
                {
                    cVB.bVB_StaUseCentralized = false;
                }
                //+++++++++++++++

                //*Arm 63-08-04
                if (oShopDB["StaAlwSendAdaLink"].Trim() == "1")
                {
                    cVB.bVB_StaAlwSendAdaLink = true;
                }
                else
                {
                    cVB.bVB_StaAlwSendAdaLink = false;
                }
                //+++++++++++++

                //*Arm 65-08-24 -สถานะการเก็บ Log Monitor 1:อนุญาต, 2:ไม่อนุญาต
                if (oShopDB["AlwKeepLogMonitor"].Trim() == "1")
                {
                    cVB.bVB_AlwKeepLog = true;
                }
                else
                {
                    cVB.bVB_AlwKeepLog = false;
                }
                //+++++++++++++

                ptMsgErr = "";
                return true;
            }
            catch (Exception oExn)
            {
                ptMsgErr = oExn.Message;
            }

            finally
            {
                oRabbitMQCfg = null;
                oKeyCfg = null;
                oConsolidateDBCfg = null;
                oBchFCDB = null;
                oHQFCDB = null;
                oSecurity = null;
                oMsg = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

            return false;
        }
    }
}
