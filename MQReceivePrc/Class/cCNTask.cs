using MQReceivePrc.Class.Standard;
using MQReceivePrc.Models.AdaTask;
using MQReceivePrc.Models.Config;
using MQReceivePrc.Models.Receive;
using MQReceivePrc.Models.WebService.Request.ExchangeRate;
using MQReceivePrc.Models.WebService.Response;
using MQReceivePrc.Models.WebService.Response.ExchangeRate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Class
{
    public class cCNTask
    {
		private string tC_QueueID;
		public bool C_PRCbAdaTask(string ptQueueID, cmlRcvData poRcvData, cmlShopDB poShopDB, ref string ptErrMsg)
		{
			bool bStaPrc = false;
			try
			{
				//1.ตรวจสอบข้อมูล
				if (poRcvData == null)
				{
					ptErrMsg = "Data is null";
					return false;
				}
				if (poShopDB == null)
				{
					ptErrMsg = "Conecction is null";
					return false;
				}

				tC_QueueID = ptQueueID;

				switch (poRcvData.ptFunction.ToUpper())
				{
					case "SYNCEXCHANGERATE": 
						cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Sync data Exchange Rate Start...", MethodBase.GetCurrentMethod().Name, true);
						bStaPrc = C_PRCbSyncExchangeRate(poRcvData, ref ptErrMsg);
						break;
					//case "BESTSELLER":
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Best Seller Start...", MethodBase.GetCurrentMethod().Name, true);
					//	bStaPrc = C_PRCbBestSeller(poRcvData, ref ptErrMsg);
					//	break;
					//case "STOCKMONTHEND": //*Arm 65-07-21 -ยกมาจาก AdaBigC
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Stock Month End Start...", MethodBase.GetCurrentMethod().Name, true);
					//	bStaPrc = C_PRCbStockMonthEnd(poRcvData, ref ptErrMsg);
					//	break;
					//case "STOCKMONTHENDDAILY": //*Arm 65-07-21 -ยกมาจาก AdaBigC
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Stock Month End Daily Start...", MethodBase.GetCurrentMethod().Name, true);
					//	bStaPrc = C_PRCbMonthEndDaily(poRcvData, ref ptErrMsg);
					//	break;
					//case "CLEARPMTEXPIRED": //*Arm 65-07-21 -ยกมาจาก AdaBigC
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Clear PMT Expired Start...", MethodBase.GetCurrentMethod().Name, true);
					//	bStaPrc = C_PRCbClearPMTExpired(poRcvData, ref ptErrMsg);
					//	break;
					//case "CLEARPRICEEXPIRE":
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Clear Price Expire Start...", MethodBase.GetCurrentMethod().Name, true);
					//	bStaPrc = C_PRCbClearPriceExpire(poRcvData, ref ptErrMsg);
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Clear Price Expire end...", MethodBase.GetCurrentMethod().Name, true);
					//	break;
					//case "DBBACKUP": //*Arm 65-07-21 -ยกมาจาก AdaBigC
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process DB BackUp Start...", MethodBase.GetCurrentMethod().Name, true);
					//	bStaPrc = C_PRCbDbBackup(poRcvData, ref ptErrMsg);
					//	break;
					//case "CLEARSTKBOOK": //*Arm 65-07-21 -ยกมาจาก AdaBigC
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Clear Stock Booking Start...", MethodBase.GetCurrentMethod().Name, true);
					//	bStaPrc = C_PRCbClearStkBooking(poRcvData, ref ptErrMsg);
					//	break;
					//case "PURGEANDBACKUP":  //*Arm 65-07-21 -ยกมาจาก AdaFitAuto
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Purge Start...", MethodBase.GetCurrentMethod().Name, true);
					//	bStaPrc = C_PRCbPurgeAdj(poRcvData, ref ptErrMsg);
					//	break;
					//case "PURGEBYTASK":  //*Arm 65-07-21 -ยกมาจาก AdaFitAuto
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Purge By Task Start...", MethodBase.GetCurrentMethod().Name, true);
					//	bStaPrc = C_PRCbPrgByTask(poRcvData, ref ptErrMsg);
					//	break;
					//case "PURGELOG":  //*Arm 65-07-21 -ยกมาจาก AdaFitAuto
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Purge Log Start...", MethodBase.GetCurrentMethod().Name, true);
					//	bStaPrc = C_PRCbPrgLog(poRcvData, ref ptErrMsg);
					//	break;
					//case "CLEARCREDIT":   //*Arm 65-07-21 -ยกมาจาก AdaFitAuto
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Clear Credit Start...", MethodBase.GetCurrentMethod().Name, true);
					//	bStaPrc = C_PRCbClearCredit(poRcvData, ref ptErrMsg);
					//	break;
					//case "UPLOADLOGFILE": //*Arm 65-06-15 Request Upload Log File
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Upload Log File Start...", MethodBase.GetCurrentMethod().Name, true);
					//	bStaPrc = C_PRCbRequestUploadLogFile(poRcvData, ref ptErrMsg);
					//	cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Process Upload Log File end...", MethodBase.GetCurrentMethod().Name, true);
					//	break;
					default:
						cFunction.C_LOGxConsoleLogMonitor(ptQueueID, "Unknown Function", MethodBase.GetCurrentMethod().Name, true);
						bStaPrc = false;
						break;
				}

			}
			catch (Exception oEx)
			{
				ptErrMsg = oEx.Message.ToString();
				cFunction.C_LOGxKeepLogErr(oEx.Message.ToString(), "cCNTask/C_PRCbCenterTask");
			}
			return bStaPrc;
		}

		/// <summary>
		/// *Arm 65-08-22 -[CR-Oversea]
		/// Sync Exchange Rate
		/// </summary>
		/// <param name="ptErrMsg"></param>
		/// <returns></returns>
		public bool C_PRCbSyncExchangeRate(cmlRcvData poRcvData, ref string ptErrMsg)
        {
			StringBuilder oSql = new StringBuilder();
			cDatabase oDB = new cDatabase();
			cmlReqTaskExchangeRate oReqTask = new cmlReqTaskExchangeRate();
			cmlReqExchangeRate oReqExchangeRateDwn;
			cmlResItem<cmlResExchangeRateDwn> oResItem;
			cClientService oCall;
			HttpResponseMessage oRep;

			string tTblRateTmp = "";
			string tTblRate = "";
			string tTblRate_L = "";
			string tAgnCode = "";
			string tUsrCode = "";
			string tJsonReq = "";
			string tResponse = "";
			string tAPI2CNAda = "";
			string tPath = "/Rate/Download";
			string tAPIHeader = "X-Api-Key";
			string tAgnKeyAPI = "12345678-1111-1111-1111-123456789410";
			int nRowEff = 0;
			bool bStaPrc = false;
			try
            {
				ptErrMsg = "";

				if (poRcvData == null) return false;

				oReqExchangeRateDwn = new cmlReqExchangeRate();
				oReqExchangeRateDwn.paoRate = new List<cmlReqInfoExchangeRate>();

				switch (poRcvData.ptSource)
                {
					case "AdaStoreBack": //Manual จาก จากหลังบ้าน
						if (string.IsNullOrEmpty(poRcvData.ptData))
						{
							cFunction.C_PRCxResProgress("RESEXCHANGERATE", 100, tAgnCode, "2", tUsrCode, "Require Parameter ptAgnCode and ptUsrCode");
							return false;
						}

						oReqTask = JsonConvert.DeserializeObject<cmlReqTaskExchangeRate>(poRcvData.ptData);

						if (oReqTask != null && !string.IsNullOrEmpty(oReqTask.ptAgnCode) && !string.IsNullOrEmpty(oReqTask.ptUsrCode))
						{
							tAgnCode = oReqTask.ptAgnCode;
							tUsrCode = oReqTask.ptUsrCode;
							cFunction.C_PRCxResProgress("RESEXCHANGERATE", 10, tAgnCode, "0", tUsrCode);
						}
                        else
                        {
							//ไม่ส่ง parameter มา
							cFunction.C_PRCxResProgress("RESEXCHANGERATE", 100, tAgnCode, "2", tUsrCode, "Require Parameter ptAgnCode and ptUsrCode");
							return false;
						}
						break;

					default:
						//* Request ทั้งหมด ไม่ต้องส่ง parameter
						break;
				}

				//Get Request Prameter for send to api
				oSql.Clear();
				oSql.AppendLine("SELECT SPC.FTFmtCode AS ptFmtCode, '' AS ptRteIsoCode ");
				oSql.AppendLine("FROM TCNMFmtRteSpc SPC WITH(NOLOCK) ");
				oSql.AppendLine("INNER JOIN TFNSFmtURL_L FMTL WITH(NOLOCK) ON SPC.FTFmtCode = FMTL.FTFmtCode ");
				oSql.AppendLine("WHERE FMTL.FTFmtStaUse = '1' and FMTL.FTFmtType = '1' and SPC.FTFspStaUse = '1' ");
				if (!string.IsNullOrEmpty(tAgnCode))
                {
					oSql.AppendLine("AND SPC.FTAgnCode = '" + tAgnCode + "' ");
				}
				oSql.AppendLine("GROUP BY SPC.FTFmtCode ");
				oReqExchangeRateDwn.paoRate = oDB.C_GETaDataQuery<cmlReqInfoExchangeRate>(cVB.tVB_ConnStr, oSql.ToString(), cVB.nVB_CmdTime);

				if (oReqExchangeRateDwn != null && oReqExchangeRateDwn.paoRate != null && oReqExchangeRateDwn.paoRate.Count > 0)
                {
					switch (poRcvData.ptSource)
					{
						case "AdaStoreBack": //Manual จาก จากหลังบ้าน
							cFunction.C_PRCxResProgress("RESEXCHANGERATE", 30, tAgnCode, "0", tUsrCode);
							break;

						default:
							//* Request ทั้งหมด ไม่ต้องส่ง parameter
							break;
					}

					tJsonReq = JsonConvert.SerializeObject(oReqExchangeRateDwn);

					//*Get URL API2CNAda
					oSql.Clear();
					oSql.AppendLine("SELECT TOP 1 ISNULL(FTUrlAddress,'') AS FTUrlAddress  ");
					oSql.AppendLine("FROM TCNTUrlObject WITH(NOLOCK)  ");
					oSql.AppendLine("WHERE FTUrlRefID = 'CENTER' AND FNUrlType = '16' ");
					tAPI2CNAda = oDB.C_DAToExecuteQuery<string>(cVB.tVB_ConnStr, oSql.ToString(), cVB.nVB_CmdTime);

					if (!string.IsNullOrEmpty(tAPI2CNAda))
					{
						oCall = new cClientService(tAPIHeader, tAgnKeyAPI);
						oRep = new HttpResponseMessage();

						try
                        {
							oRep = oCall.C_POSToInvoke(tAPI2CNAda + tPath, tJsonReq);
						}
						catch(Exception oEx)
                        {
							throw new Exception(oEx.Message);
                        }

						//Respose Progress
						switch (poRcvData.ptSource)
						{
							case "AdaStoreBack": //Manual จาก จากหลังบ้าน
								cFunction.C_PRCxResProgress("RESEXCHANGERATE", 50, tAgnCode, "0", tUsrCode);
								break;

							default:
								//* Request ทั้งหมด ไม่ต้องส่ง parameter
								break;
						}

						if (oRep.StatusCode == System.Net.HttpStatusCode.OK)
						{
							tResponse = oRep.Content.ReadAsStringAsync().Result;
							oResItem = new cmlResItem<cmlResExchangeRateDwn>();
							oResItem = JsonConvert.DeserializeObject<cmlResItem<cmlResExchangeRateDwn>>(tResponse);

							switch (oResItem.rtCode)
							{
								case "001":
									if (oResItem != null && oResItem.roItem != null && oResItem.roItem.raoRate != null && oResItem.roItem.raoRate.Count > 0)
                                    {
										tTblRateTmp = "TTmpExchangeRate" + tC_QueueID.Replace("-", "");

										if (C_INSxInsertExchangeRate2Tmp(tTblRateTmp, oResItem.roItem.raoRate)) //เอาข้อมูล Exchange Rate ลง Temp ก่อน
                                        {
											//*Arm 65-09-10
											oSql.Clear();
											oSql.AppendLine("SELECT TOP 1 FNLngID FROM TSysLanguage WITH(NOLOCK) WHERE FTLngStaUse = '1' AND FTLngStaLocal = '1' ");
											int nLngID = oDB.C_DAToExecuteQuery<int>(cVB.tVB_ConnStr, oSql.ToString(), cVB.nVB_CmdTime);
											if (nLngID == 0) { nLngID = 1; }

											//Get Config
											oSql.Clear();
											oSql.AppendLine("SELECT CASE WHEN ISNULL(FTSysStaUsrValue,'') != '' THEN FTSysStaUsrValue ELSE FTSysStaDefValue END AS FTSysStaUsrValue ");
											oSql.AppendLine("FROM TSysConfig WITH(NOLOCK) ");
											oSql.AppendLine("WHERE FTSysCode = 'ADecPntSavRte' ");
											oSql.AppendLine("AND FTSysApp = 'CN' ");
											int nSavRte = oDB.C_DAToExecuteQuery<int>(cVB.tVB_ConnStr, oSql.ToString(), cVB.nVB_CmdTime);
											if (nSavRte == 0) { nSavRte = 10; }
											//++++++++++++++

										    //Update Exchange Rate
											oSql.Clear();
											oSql.AppendLine("UPDATE RTE WITH(ROWLOCK) ");
											//oSql.AppendLine("SET RTE.FCRteLastRate = ROUND(ECR.FCRteRate, " + nSavRte + ", 1) ");
											//oSql.AppendLine(", RTE.FCRteRate = CASE WHEN ISNULL(CTY.FTCtyStaCtrlRate,'2') = '2' THEN ROUND(ECR.FCRteRate, " + nSavRte + ", 1) ELSE ROUND(RTE.FCRteRate, " + nSavRte + ", 1) END ");
											//*Arm 65-11-05
											oSql.AppendLine("SET RTE.FCRteLastRate = ROUND((ECR.FCRteRate/RteLoc.FCRteRate), " + nSavRte + ", 1) ");
											oSql.AppendLine(", RTE.FCRteRate = CASE WHEN ISNULL(CTY.FTCtyStaCtrlRate,'2') = '2' THEN ROUND((ECR.FCRteRate/RteLoc.FCRteRate), " + nSavRte + ", 1) ELSE ROUND(RTE.FCRteRate, " + nSavRte + ", 1) END ");
											//++++++++++++++
											oSql.AppendLine(", RTE.FDRteLastUpdOn = ECR.FDLastUpdOn ");
											oSql.AppendLine(", RTE.FDLastUpdOn = GETDATE() ");
											oSql.AppendLine(", RTE.FTLastUpdBy = 'MQReceivePrc' ");
											oSql.AppendLine("FROM TCNMFmtRteSpc SPC WITH(NOLOCK) ");
											oSql.AppendLine("INNER JOIN TFNSFmtURL_L FMTL WITH(NOLOCK) ON SPC.FTFmtCode = FMTL.FTFmtCode ");
											oSql.AppendLine("INNER JOIN " + tTblRateTmp + " ECR WITH(NOLOCK) ON SPC.FTFmtCode = ECR.FTFmtCode ");
											oSql.AppendLine("INNER JOIN TFNMRate RTE ON SPC.FTAgnCode = RTE.FTAgnCode AND RTE.FTRteIsoCode = ECR.FTRteIsoCode ");
											oSql.AppendLine("INNER JOIN TCNMAgency AGN WITH(NOLOCK) ON SPC.FTAgnCode = AGN.FTAgnCode ");
											oSql.AppendLine("INNER JOIN TCNMCountry CTY WITH(NOLOCK) ON AGN.FTCtyCode = CTY.FTCtyCode AND CTY.FTCtyStaUse = '1' ");
											//*Arm 65-11-05
											oSql.AppendLine("LEFT JOIN (SELECT FTAgnCode, FTRteCode, RTE.FTRteIsoCode, ECR.FCRteRate FROM TFNMRate RTE WITH(NOLOCK) ");
											oSql.AppendLine("			INNER JOIN " + tTblRateTmp + " ECR WITH(NOLOCK) ON RTE.FTRteIsoCode = ECR.FTRteIsoCode ");
											oSql.AppendLine("			WHERE FTRteStaLocal = '1') RteLoc ");
											oSql.AppendLine("	ON RteLoc.FTAgnCode = SPC.FTAgnCode ");
											//+++++++++++++
											oSql.AppendLine("WHERE FMTL.FTFmtStaUse = '1' and FMTL.FTFmtType = '1' and SPC.FTFspStaUse = '1' ");
											if (!string.IsNullOrEmpty(tAgnCode))
											{
												oSql.AppendLine("AND SPC.FTAgnCode = '" + tAgnCode + "' ");
											}
											oDB.C_DATbExecuteNonQuery(cVB.tVB_ConnStr, oSql.ToString(), cVB.nVB_CmdTime, out nRowEff);

											//Insert Exchange Rate
											oSql.Clear();
											oSql.AppendLine("SELECT DISTINCT COUNT(SPC.FTAgnCode) ");
											oSql.AppendLine("FROM TCNMFmtRteSpc SPC WITH(NOLOCK) ");
											oSql.AppendLine("INNER JOIN TFNSFmtURL_L FMTL WITH(NOLOCK) ON SPC.FTFmtCode = FMTL.FTFmtCode ");
											oSql.AppendLine("INNER JOIN " + tTblRateTmp + " ECR WITH(NOLOCK) ON SPC.FTFmtCode = ECR.FTFmtCode ");
											oSql.AppendLine("INNER JOIN TCNMAgency AGN WITH(NOLOCK) ON SPC.FTAgnCode = AGN.FTAgnCode ");
											oSql.AppendLine("INNER JOIN TCNMCountry CTY WITH(NOLOCK) ON AGN.FTCtyCode = CTY.FTCtyCode AND CTY.FTCtyStaUse = '1' ");
											oSql.AppendLine("LEFT JOIN TFNMRate RTE WITH(NOLOCK) ON SPC.FTAgnCode = RTE.FTAgnCode AND RTE.FTRteIsoCode = ECR.FTRteIsoCode ");
											oSql.AppendLine("WHERE FMTL.FTFmtStaUse = '1' and FMTL.FTFmtType = '1' and SPC.FTFspStaUse = '1' ");
											oSql.AppendLine("AND ISNULL(RTE.FTRteCode,'') = '' ");
											if (!string.IsNullOrEmpty(tAgnCode))
											{
												oSql.AppendLine("AND SPC.FTAgnCode = '" + tAgnCode + "' ");
											}
											int nCnt = oDB.C_DAToExecuteQuery<int>(cVB.tVB_ConnStr, oSql.ToString(), cVB.nVB_CmdTime);

											if (nCnt > 0)
											{
												tTblRate = "TFNMRate" + tC_QueueID.Replace("-", "");
												tTblRate_L = "TFNMRate_L" + tC_QueueID.Replace("-", "");

												oDB.C_PRCxCreateDatabaseTmp("TFNMRate", tTblRate, cVB.tVB_ConnStr, cVB.nVB_CmdTime);
												oDB.C_PRCxCreateDatabaseTmp("TFNMRate_L", tTblRate_L, cVB.tVB_ConnStr, cVB.nVB_CmdTime);

												oSql.Clear();
												oSql.AppendLine("INSERT INTO " + tTblRate + " (FTAgnCode, FTRteCode, FCRteRate, FCRteLastRate, FCRteFraction ");
												oSql.AppendLine(", FTRteType, FTRteTypeChg, FTRteSign, FTRteIsoCode, FTRteStaLocal ");
												oSql.AppendLine(", FTRteStaAlwChange, FTRteStaUse, FDRteLastUpdOn ");
												oSql.AppendLine(", FDLastUpdOn, FTLastUpdBy, FDCreateOn, FTCreateBy) ");
												oSql.AppendLine("SELECT DISTINCT SPC.FTAgnCode AS FTAgnCode, ECR.FTRteIsoCode AS FTRteCode ");
												oSql.AppendLine(", CASE WHEN ISNULL(CTY.FTCtyStaCtrlRate,'2') = '2' THEN ROUND(ECR.FCRteRate, " + nSavRte + ", 1) ELSE 0 END AS FCRteRate ");
												oSql.AppendLine(", ROUND(ECR.FCRteRate, " + nSavRte + ", 1) AS FCRteLastRate, 0 AS FCRteFraction ");
												oSql.AppendLine(", '1' AS FTRteType, NULL AS FTRteTypeChg, '' AS FTRteSign, ECR.FTRteIsoCode AS FTRteIsoCode, '2' AS FTRteStaLocal ");
												oSql.AppendLine(", '2' AS FTRteStaAlwChange, '2' AS FTRteStaUse, ECR.FDLastUpdOn AS FDRteLastUpdOn ");
												oSql.AppendLine(", GETDATE() AS FDLastUpdOn, 'MQReceivePrc' AS FTLastUpdBy, GETDATE() AS FDCreateOn, 'MQReceivePrc' AS FTCreateBy ");
												oSql.AppendLine("FROM TCNMFmtRteSpc SPC WITH(NOLOCK) ");
												oSql.AppendLine("INNER JOIN TFNSFmtURL_L FMTL WITH(NOLOCK) ON SPC.FTFmtCode = FMTL.FTFmtCode ");
												oSql.AppendLine("INNER JOIN " + tTblRateTmp + " ECR WITH(NOLOCK) ON SPC.FTFmtCode = ECR.FTFmtCode ");
												oSql.AppendLine("INNER JOIN TCNMAgency AGN WITH(NOLOCK) ON SPC.FTAgnCode = AGN.FTAgnCode ");
												oSql.AppendLine("INNER JOIN TCNMCountry CTY WITH(NOLOCK) ON AGN.FTCtyCode = CTY.FTCtyCode AND CTY.FTCtyStaUse = '1' ");
												oSql.AppendLine("LEFT JOIN TFNMRate RTE WITH(NOLOCK) ON SPC.FTAgnCode = RTE.FTAgnCode AND RTE.FTRteIsoCode = ECR.FTRteIsoCode ");
												oSql.AppendLine("WHERE FMTL.FTFmtStaUse = '1' and FMTL.FTFmtType = '1' and SPC.FTFspStaUse = '1' ");
												oSql.AppendLine("AND ISNULL(RTE.FTRteCode,'') = '' ");
												if (!string.IsNullOrEmpty(tAgnCode))
												{
													oSql.AppendLine("AND SPC.FTAgnCode = '" + tAgnCode + "' ");
												}
												oDB.C_DATbExecuteNonQuery(cVB.tVB_ConnStr, oSql.ToString(), cVB.nVB_CmdTime, out nRowEff);

												if(nRowEff > 0)
                                                {
													oSql.Clear();
													oSql.AppendLine("INSERT INTO " + tTblRate_L + " (FTAgnCode, FTRteCode, FNLngID, FTRteName, FTRteShtName, FTRteNameText, FTRteDecText) ");
													//oSql.AppendLine("SELECT FTAgnCode, FTRteCode, 1 AS FNLngID, FTRteIsoCode AS FTRteName, '' AS FTRteShtName, '' AS FTRteNameText, '' AS FTRteDecText ");
													//oSql.AppendLine("FROM " + tTblRate + " RTE WITH(NOLOCK) ");
													//*Arm 65-09-10
													oSql.AppendLine("SELECT DISTINCT RTE.FTAgnCode, RTE.FTRteCode, " + nLngID + " AS FNLngID, ISNULL(RTEL.FTRteIsoName, RTE.FTRteIsoCode) AS FTRteName ");
													oSql.AppendLine(", '' AS FTRteShtName, '' AS FTRteNameText, '' AS FTRteDecText ");
													oSql.AppendLine("FROM " + tTblRate + " RTE WITH(NOLOCK) ");
													oSql.AppendLine("LEFT JOIN TCNSRate_L RTEL WITH(NOLOCK) ON RTE.FTRteIsoCode = RTEL.FTRteIsoCode ");
													//+++++++++++++
													oDB.C_DATbExecuteNonQuery(cVB.tVB_ConnStr, oSql.ToString(), cVB.nVB_CmdTime, out nRowEff);
												}

												oSql.Clear();
												oSql.AppendLine("BEGIN TRY");
												oSql.AppendLine("BEGIN TRANSACTION");
												oSql.AppendLine("	INSERT INTO TFNMRate (FTAgnCode, FTRteCode, FCRteRate, FCRteLastRate, FCRteFraction ");
												oSql.AppendLine("	, FTRteType, FTRteTypeChg, FTRteSign, FTRteIsoCode, FTRteStaLocal ");
												oSql.AppendLine("	, FTRteStaAlwChange, FTRteStaUse, FDRteLastUpdOn ");
												oSql.AppendLine("	, FDLastUpdOn, FTLastUpdBy, FDCreateOn, FTCreateBy) ");
												oSql.AppendLine("	SELECT FTAgnCode, FTRteCode, FCRteRate, FCRteLastRate, FCRteFraction ");
												oSql.AppendLine("	, FTRteType, FTRteTypeChg, FTRteSign, FTRteIsoCode, FTRteStaLocal ");
												oSql.AppendLine("	, FTRteStaAlwChange, FTRteStaUse, FDRteLastUpdOn ");
												oSql.AppendLine("	, FDLastUpdOn, FTLastUpdBy, FDCreateOn, FTCreateBy ");
												oSql.AppendLine("	FROM " + tTblRate + " WITH(NOLOCK) ");
												oSql.AppendLine(" ");
												oSql.AppendLine("	INSERT INTO TFNMRate_L (FTAgnCode, FTRteCode, FNLngID, FTRteName, FTRteShtName, FTRteNameText, FTRteDecText) ");
												oSql.AppendLine("	SELECT FTAgnCode, FTRteCode, FNLngID, FTRteName, FTRteShtName, FTRteNameText, FTRteDecText ");
												oSql.AppendLine("	FROM " + tTblRate_L + " WITH(NOLOCK) ");
												oSql.AppendLine("   COMMIT TRANSACTION");
												oSql.AppendLine("END TRY");
												oSql.AppendLine("BEGIN CATCH");
												oSql.AppendLine("   IF(@@TRANCOUNT > 0)");
												oSql.AppendLine("       ROLLBACK TRAN;");
												oSql.AppendLine("END CATCH");
												oDB.C_DATbExecuteNonQuery(cVB.tVB_ConnStr, oSql.ToString(), cVB.nVB_CmdTime, out nRowEff);
											}
										}
                                    }

									bStaPrc = true;
									break;

								case "800":
									new cLog().C_WRTxLogMonitor("cCNTask", "C_PRCbSyncExchangeRate : (" + oResItem.rtCode + ")" + oResItem.rtDesc, cVB.bVB_AlwKeepLog);
									bStaPrc = true;
									break;

								default:

									//Respose Progress
									switch (poRcvData.ptSource)
									{
										case "AdaStoreBack":
											cFunction.C_PRCxResProgress("RESEXCHANGERATE", 100, oReqTask.ptAgnCode, "2", oReqTask.ptUsrCode, "Fail " + oResItem.rtCode + ":"+ oResItem.rtDesc);
											break;
									}
									new cLog().C_WRTxLogMonitor("cCNTask", "C_PRCbSyncExchangeRate : Fail (" + oResItem.rtCode + ")" + oResItem.rtDesc, cVB.bVB_AlwKeepLog);
									bStaPrc = false;
									break;
							}
						}
                        else
                        {
							new cLog().C_WRTxLogMonitor("cCNTask", "C_PRCbSyncExchangeRate : Fail StatusCode = " + oRep.StatusCode, cVB.bVB_AlwKeepLog);
							bStaPrc = false;
							
							//Respose Progress
							switch (poRcvData.ptSource)
							{
								case "AdaStoreBack":
									cFunction.C_PRCxResProgress("RESEXCHANGERATE", 100, oReqTask.ptAgnCode, "2", oReqTask.ptUsrCode, "API2CNAda Error " + oRep.StatusCode);
									break;
							}
						}
					}
                    else
                    {
						//Not found URL
						new cLog().C_WRTxLogMonitor("cCNTask", "C_PRCbSyncExchangeRate : Not found url API2CNAda.", cVB.bVB_AlwKeepLog);
						bStaPrc = false;

						//Respose Progress
						switch (poRcvData.ptSource)
						{
							case "AdaStoreBack":
								cFunction.C_PRCxResProgress("RESEXCHANGERATE", 100, oReqTask.ptAgnCode, "2", oReqTask.ptUsrCode, "Not found url API2CNAda.");
								break;
						}
					}
				}
                else
                {
					//*ไม่พบ format rate code
					new cLog().C_WRTxLogMonitor("cCNTask", "C_PRCbSyncExchangeRate : Not found Format Rate Code", cVB.bVB_AlwKeepLog);
					switch (poRcvData.ptSource)
					{
						case "AdaStoreBack": //Manual จาก จากหลังบ้าน
							cFunction.C_PRCxResProgress("RESEXCHANGERATE", 100, tAgnCode, "2", tUsrCode, "Not found Format Rate Code.");
							break;

						default:
							//* Request ทั้งหมด ไม่ต้องส่ง parameter
							break;
					}
					bStaPrc = false;
				}


				if (bStaPrc)
				{
					switch (poRcvData.ptSource)
					{
						case "AdaStoreBack": //Manual จาก จากหลังบ้าน
							cFunction.C_PRCxResProgress("RESEXCHANGERATE", 100, tAgnCode, "1", tUsrCode);
							break;

						default:
							//* Request ทั้งหมด ไม่ต้องส่ง parameter
							break;
					}
				}

			}
			catch (Exception oEx)
			{
				bStaPrc = false;
				ptErrMsg = oEx.Message.ToString();
				cFunction.C_LOGxKeepLogErr(oEx.Message.ToString(), "cCNTask/C_PRCbCenterTask");

				switch (poRcvData.ptSource)
				{
					case "AdaStoreBack":
						cFunction.C_PRCxResProgress("RESEXCHANGERATE", 100, oReqTask.ptAgnCode, "1", oReqTask.ptUsrCode, oEx.Message.ToString());
						break;
				}
			}
            finally
            {
				oSql.Clear();
				if (!string.IsNullOrEmpty(tTblRateTmp))
				{
					oSql.AppendLine("IF OBJECT_ID(N'" + tTblRateTmp + "') IS NOT NULL");
					oSql.AppendLine("   DROP TABLE " + tTblRateTmp);
				}
				if (!string.IsNullOrEmpty(tTblRate))
				{
					oSql.AppendLine("IF OBJECT_ID(N'" + tTblRate + "') IS NOT NULL");
					oSql.AppendLine("   DROP TABLE " + tTblRate);
				}
				if (!string.IsNullOrEmpty(tTblRate_L))
				{
					oSql.AppendLine("IF OBJECT_ID(N'" + tTblRate_L + "') IS NOT NULL");
					oSql.AppendLine("   DROP TABLE " + tTblRate_L);
				}
				if (!string.IsNullOrEmpty(oSql.ToString()))
				{
					oDB.C_DATbExecuteNonQuery(cVB.tVB_ConnStr, oSql.ToString(), cVB.nVB_CmdTime, out nRowEff);
				}

				oCall = null;
				oRep = null;
				oSql = null;
				oDB = null;
				oResItem = null;
				oReqExchangeRateDwn = null;
            }

			return bStaPrc;
		}

		public bool C_INSxInsertExchangeRate2Tmp(string ptTblRateTmp, List<cmlResInfoExchangeRate> paoExchangeRate)
        {
			StringBuilder oSql = new StringBuilder();
			cDatabase oDB = new cDatabase();
			SqlTransaction oTranscation;
			SqlConnection oConn;

			List<cmlExchangeRateTmp> aoExchangeRateTmp;
			cDataReader<cmlExchangeRateTmp> oExchangeRateTmp;

			int nRowEff = 0;
			try
            {
				if (string.IsNullOrEmpty(ptTblRateTmp)) return false;

				oSql.Clear();
				oSql.AppendLine("IF NOT EXISTS(SELECT name FROM sys.tables WHERE OBJECT_ID = object_id('" + ptTblRateTmp + "')) BEGIN ");
				oSql.AppendLine("CREATE TABLE [dbo].[" + ptTblRateTmp + "]( ");
				oSql.AppendLine("	[FTFmtCode] [varchar](5) NOT NULL, ");
				oSql.AppendLine("	[FTRteIsoCode] [varchar](10) NOT NULL, ");
				oSql.AppendLine("	[FCRteRate] [numeric](18, 10) NULL, ");
				oSql.AppendLine("	[FDLastUpdOn] [datetime] NULL, ");
				oSql.AppendLine("	[FTLastUpdBy] [varchar](20) NULL, ");
				oSql.AppendLine("	[FDCreateOn] [datetime] NULL, ");
				oSql.AppendLine("	[FTCreateBy] [varchar](20) NULL ");
				oSql.AppendLine(" ) ON [PRIMARY] ");
				oSql.AppendLine("END ");
				oSql.AppendLine("TRUNCATE TABLE " + ptTblRateTmp + " ");
				oDB.C_DATbExecuteNonQuery(cVB.tVB_ConnStr, oSql.ToString(), cVB.nVB_CmdTime, out nRowEff);

				aoExchangeRateTmp = new List<cmlExchangeRateTmp>();
				aoExchangeRateTmp = paoExchangeRate.Select(oItem => new cmlExchangeRateTmp()
				{
					FTFmtCode = oItem.rtFmtCode,
					FTRteIsoCode = oItem.rtRteIsoCode,
					FCRteRate = oItem.rcRteRate,
					FDLastUpdOn = oItem.rdLastUpdOn,
					FTLastUpdBy = oItem.rtLastUpdBy,
					FDCreateOn = oItem.rdCreateOn,
					FTCreateBy = oItem.rtCreateBy
				}).ToList();

				oExchangeRateTmp = new cDataReader<cmlExchangeRateTmp>(aoExchangeRateTmp);
				oConn = new SqlConnection(cVB.tVB_ConnStr);
				oConn.Open();
				oTranscation = oConn.BeginTransaction();

				using (SqlBulkCopy oBulkCopy = new SqlBulkCopy(oConn, SqlBulkCopyOptions.Default, oTranscation))
				{
					foreach (string tColName in oExchangeRateTmp.ColumnNames)
					{
						oBulkCopy.ColumnMappings.Add(tColName, tColName);
					}

					oBulkCopy.BatchSize = 100;
					oBulkCopy.DestinationTableName = "dbo." + ptTblRateTmp;
					try
					{
						oBulkCopy.WriteToServer(oExchangeRateTmp);
					}
					catch (Exception oEx)
					{
						cFunction.C_LOGxKeepLogErr("BulkCopy / ExchangeRateTmp : " + oEx.Message.ToString(), "cCNTask /C_INSxInsertExchangeRate2Tmp");
					}
				}
				oTranscation.Commit();
				return true;
			}
			catch(Exception oEx)
            {
				cFunction.C_LOGxKeepLogErr(oEx.Message.ToString(), "cCNTask/C_INSxInsertExchangeRate2Tmp");
				return false;
            }
            finally
            {
				oSql = null;
				oDB = null;
				oConn = null;
				oTranscation = null;
				aoExchangeRateTmp = null;
				oExchangeRateTmp = null;
				paoExchangeRate = null;
            }
        }
	}
}
