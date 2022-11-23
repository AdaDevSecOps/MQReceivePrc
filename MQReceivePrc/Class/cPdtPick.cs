using MQReceivePrc.Class.Standard;
using MQReceivePrc.Models.Config;
using MQReceivePrc.Models.PdtPick;
using MQReceivePrc.Models.Receive;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Class
{
    class cPdtPick
    {
        public bool C_PRCbUploadPdtPick(string ptQueueID, cmlRcvDataUpload poData, cmlShopDB poShopDB, ref string ptErrMsg)
        {
            cDataReader<cmlTCNTPdtPickHD> aoHD;
            cDataReader<cmlTCNTPdtPickHDCst> aoHDCst;
            cDataReader<cmlTCNTPdtPickHDDocRef> aoHDDocRef;
            cDataReader<cmlTCNTPdtPickDT> aoDT;
            cDataReader<cmlTCNTPdtPickDTSN> aoDTSN;
            cDataReader<cmlTCNTPdtPickDTDis> aoDTDis;
            //cDataReader<cmlTCNMPdtLocPickGrp> aoLocPickGrp;
            //cDataReader<cmlTCNMPdtLocPickGrp_L> aoLocPickGrpLng;

            StringBuilder oSql = new StringBuilder();
            cDatabase oDB = new cDatabase();
            int nRowAffect = 0;
            cmlTCNTPdtPick oPdtPick;
            SqlTransaction oTranscation;
            SqlConnection oConn;

            cSP oSP = new cSP();

            string tQueueID = "";
            string tTblPickHD = "";
            string tTblPickHDCst = "";
            string tTblPickHDDocRef = "";
            string tTblPickDT = "";
            string tTblPickDTSN = "";
            string tTblPickDTDis = "";
            //string tTblPickLocGrp = "";
            //string tTblPickLocGrp_L = "";

            try
            {
                if (poData == null) return false;
                if (string.IsNullOrEmpty(poData.ptData)) return false;
                oPdtPick = JsonConvert.DeserializeObject<cmlTCNTPdtPick>(poData.ptData);
                
                tQueueID = ptQueueID.Replace("-", "");

                tTblPickHD = "TPickHD" + tQueueID;
                tTblPickHDCst = "TPickHDCst" + tQueueID;
                tTblPickHDDocRef = "TPickHDDocRef" + tQueueID;
                tTblPickDT = "TPickDT" + tQueueID;
                tTblPickDTSN = "TPickDTSN" + tQueueID;
                tTblPickDTDis = "TPickDTDis" + tQueueID;
                //Create Temp
                #region Create Temp
                oDB.C_PRCxCreateDatabaseTmp("TCNTPdtPickHD", tTblPickHD, poData.ptConnStr, (int)poShopDB.nCommandTimeOut);
                oDB.C_PRCxCreateDatabaseTmp("TCNTPdtPickHDCst", tTblPickHDCst, poData.ptConnStr, (int)poShopDB.nCommandTimeOut);
                oDB.C_PRCxCreateDatabaseTmp("TCNTPdtPickHDDocRef", tTblPickHDDocRef, poData.ptConnStr, (int)poShopDB.nCommandTimeOut);
                oDB.C_PRCxCreateDatabaseTmp("TCNTPdtPickDT", tTblPickDT, poData.ptConnStr, (int)poShopDB.nCommandTimeOut);
                oDB.C_PRCxCreateDatabaseTmp("TCNTPdtPickDTSN", tTblPickDTSN, poData.ptConnStr, (int)poShopDB.nCommandTimeOut);
                oDB.C_PRCxCreateDatabaseTmp("TCNTPdtPickDTDis", tTblPickDTDis, poData.ptConnStr, (int)poShopDB.nCommandTimeOut);
                #endregion

                oConn = new SqlConnection(poData.ptConnStr);
                oConn.Open();

                oTranscation = oConn.BeginTransaction();

                //insert to DB
                if (oPdtPick.aoTCNTPdtPickHD != null)
                {
                    aoHD = new cDataReader<cmlTCNTPdtPickHD>(oPdtPick.aoTCNTPdtPickHD);

                    using (SqlBulkCopy oBulkCopy = new SqlBulkCopy(oConn, SqlBulkCopyOptions.Default, oTranscation))
                    {
                        foreach (string tColName in aoHD.ColumnNames)
                        {
                            oBulkCopy.ColumnMappings.Add(tColName, tColName);
                        }

                        oBulkCopy.BatchSize = 100;
                        oBulkCopy.DestinationTableName = "dbo." + tTblPickHD;

                        try
                        {
                            oBulkCopy.WriteToServer(aoHD);
                        }
                        catch (Exception oEx)
                        {
                            oTranscation.Rollback();
                            ptErrMsg = oEx.Message.ToString();
                            cFunction.C_LOGxKeepLogErr("BulkCopy/" + tTblPickHD + " " + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + " : " + oEx.Message.ToString(), "C_PRCbUploadPdtPick");
                            return false;
                        }
                    }
                }
                if (oPdtPick.aoTCNTPdtPickHDCst != null)
                {
                    aoHDCst = new cDataReader<cmlTCNTPdtPickHDCst>(oPdtPick.aoTCNTPdtPickHDCst);

                    using (SqlBulkCopy oBulkCopy = new SqlBulkCopy(oConn, SqlBulkCopyOptions.Default, oTranscation))
                    {
                        foreach (string tColName in aoHDCst.ColumnNames)
                        {
                            oBulkCopy.ColumnMappings.Add(tColName, tColName);
                        }

                        oBulkCopy.BatchSize = 100;
                        oBulkCopy.DestinationTableName = "dbo." + tTblPickHDCst;

                        try
                        {
                            oBulkCopy.WriteToServer(aoHDCst);
                        }
                        catch (Exception oEx)
                        {
                            oTranscation.Rollback();
                            ptErrMsg = oEx.Message.ToString();
                            cFunction.C_LOGxKeepLogErr("BulkCopy/" + tTblPickHDCst + " " + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + " : " + oEx.Message.ToString(), "C_PRCbUploadPdtPick");
                            return false;
                        }
                    }
                }
                if (oPdtPick.aoTCNTPdtPickHDDocRef != null)
                {
                    aoHDDocRef = new cDataReader<cmlTCNTPdtPickHDDocRef>(oPdtPick.aoTCNTPdtPickHDDocRef);

                    using (SqlBulkCopy oBulkCopy = new SqlBulkCopy(oConn, SqlBulkCopyOptions.Default, oTranscation))
                    {
                        foreach (string tColName in aoHDDocRef.ColumnNames)
                        {
                            oBulkCopy.ColumnMappings.Add(tColName, tColName);
                        }

                        oBulkCopy.BatchSize = 100;
                        oBulkCopy.DestinationTableName = "dbo." + tTblPickHDDocRef;

                        try
                        {
                            oBulkCopy.WriteToServer(aoHDDocRef);
                        }
                        catch (Exception oEx)
                        {
                            oTranscation.Rollback();
                            ptErrMsg = oEx.Message.ToString();
                            cFunction.C_LOGxKeepLogErr("BulkCopy/" + tTblPickHDDocRef + " " + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + " : " + oEx.Message.ToString(), "C_PRCbUploadPdtPick");
                            return false;
                        }
                    }
                }
                if (oPdtPick.aoTCNTPdtPickDT != null)
                {
                    aoDT = new cDataReader<cmlTCNTPdtPickDT>(oPdtPick.aoTCNTPdtPickDT);

                    using (SqlBulkCopy oBulkCopy = new SqlBulkCopy(oConn, SqlBulkCopyOptions.Default, oTranscation))
                    {
                        foreach (string tColName in aoDT.ColumnNames)
                        {
                            oBulkCopy.ColumnMappings.Add(tColName, tColName);
                        }

                        oBulkCopy.BatchSize = 100;
                        oBulkCopy.DestinationTableName = "dbo." + tTblPickDT;

                        try
                        {
                            oBulkCopy.WriteToServer(aoDT);
                        }
                        catch (Exception oEx)
                        {
                            oTranscation.Rollback();
                            ptErrMsg = oEx.Message.ToString();
                            cFunction.C_LOGxKeepLogErr("BulkCopy/" + tTblPickDT + " " + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + " : " + oEx.Message.ToString(), "C_PRCbUploadPdtPick");
                            return false;
                        }
                    }
                }
                if (oPdtPick.aoTCNTPdtPickDTSN != null)
                {
                    aoDTSN = new cDataReader<cmlTCNTPdtPickDTSN>(oPdtPick.aoTCNTPdtPickDTSN);

                    using (SqlBulkCopy oBulkCopy = new SqlBulkCopy(oConn, SqlBulkCopyOptions.Default, oTranscation))
                    {
                        foreach (string tColName in aoDTSN.ColumnNames)
                        {
                            oBulkCopy.ColumnMappings.Add(tColName, tColName);
                        }

                        oBulkCopy.BatchSize = 100;
                        oBulkCopy.DestinationTableName = "dbo." + tTblPickDTSN;

                        try
                        {
                            oBulkCopy.WriteToServer(aoDTSN);
                        }
                        catch (Exception oEx)
                        {
                            oTranscation.Rollback();
                            ptErrMsg = oEx.Message.ToString();
                            cFunction.C_LOGxKeepLogErr("BulkCopy/" + tTblPickDTSN + " " + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + " : " + oEx.Message.ToString(), "C_PRCbUploadPdtPick");
                            return false;
                        }
                    }
                }
                if (oPdtPick.aoTCNTPdtPickDTDis != null)
                {
                    aoDTDis = new cDataReader<cmlTCNTPdtPickDTDis>(oPdtPick.aoTCNTPdtPickDTDis);

                    using (SqlBulkCopy oBulkCopy = new SqlBulkCopy(oConn, SqlBulkCopyOptions.Default, oTranscation))
                    {
                        foreach (string tColName in aoDTDis.ColumnNames)
                        {
                            oBulkCopy.ColumnMappings.Add(tColName, tColName);
                        }

                        oBulkCopy.BatchSize = 100;
                        oBulkCopy.DestinationTableName = "dbo." + tTblPickDTDis;

                        try
                        {
                            oBulkCopy.WriteToServer(aoDTDis);
                        }
                        catch (Exception oEx)
                        {
                            oTranscation.Rollback();
                            ptErrMsg = oEx.Message.ToString();
                            cFunction.C_LOGxKeepLogErr("BulkCopy/" + tTblPickDTDis + " " + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + " : " + oEx.Message.ToString(), "C_PRCbUploadPdtPick");
                            return false;
                        }
                    }
                }
                oTranscation.Commit();

                oSql = new StringBuilder();
                oSql.AppendLine("BEGIN TRY");
                oSql.AppendLine("BEGIN TRANSACTION");
                //TCNTPdtPickHD
                oSql.AppendLine("   DELETE HD ");
                oSql.AppendLine("   FROM TCNTPdtPickHD HD WITH(ROWLOCK)");
                oSql.AppendLine("   WHERE HD.FTAgnCode = '"+ oPdtPick.aoTCNTPdtPickHD[0].FTAgnCode + "' AND HD.FTBchCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTBchCode + "' AND HD.FTXthDocNo = '" + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + "'");
                oSql.AppendLine();
                oSql.AppendLine("   INSERT INTO TCNTPdtPickHD");
                oSql.AppendLine("   SELECT * FROM " + tTblPickHD + " WITH(NOLOCK) ");
                oSql.AppendLine("   WHERE FTAgnCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTAgnCode + "' AND FTBchCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTBchCode + "' AND FTXthDocNo = '" + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + "'");
                oSql.AppendLine();
                //TCNTPdtPickHDCst
                oSql.AppendLine("   DELETE HDCst ");
                oSql.AppendLine("   FROM TCNTPdtPickHDCst HDCst WITH(ROWLOCK)");
                oSql.AppendLine("   WHERE HDCst.FTAgnCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTAgnCode + "' AND HDCst.FTBchCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTBchCode + "' AND HDCst.FTXthDocNo = '" + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + "'");
                oSql.AppendLine();
                oSql.AppendLine("   INSERT INTO TCNTPdtPickHDCst");
                oSql.AppendLine("   SELECT * FROM " + tTblPickHDCst + " WITH(NOLOCK) ");
                oSql.AppendLine("   WHERE FTAgnCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTAgnCode + "' AND FTBchCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTBchCode + "' AND FTXthDocNo = '" + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + "'");
                oSql.AppendLine();
                //TCNTPdtPickHDDocRef
                oSql.AppendLine("   DELETE HDDocRef ");
                oSql.AppendLine("   FROM TCNTPdtPickHDDocRef HDDocRef WITH(ROWLOCK) ");
                oSql.AppendLine("   WHERE HDDocRef.FTAgnCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTAgnCode + "' AND HDDocRef.FTBchCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTBchCode + "' AND HDDocRef.FTXthDocNo = '" + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + "'");
                oSql.AppendLine();
                oSql.AppendLine("   INSERT INTO TCNTPdtPickHDDocRef ");
                oSql.AppendLine("   SELECT * FROM " + tTblPickHDDocRef + " WITH(NOLOCK) ");
                oSql.AppendLine("   WHERE FTAgnCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTAgnCode + "' AND FTBchCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTBchCode + "' AND FTXthDocNo = '" + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + "'");
                oSql.AppendLine();
                //TCNTPdtPickDT
                oSql.AppendLine("   DELETE DT ");
                oSql.AppendLine("   FROM TCNTPdtPickDT DT WITH(ROWLOCK) ");
                oSql.AppendLine("   WHERE DT.FTAgnCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTAgnCode + "' AND DT.FTBchCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTBchCode + "' AND DT.FTXthDocNo = '" + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + "'");
                oSql.AppendLine();
                oSql.AppendLine("   INSERT INTO TCNTPdtPickDT ");
                oSql.AppendLine("   SELECT * FROM " + tTblPickDT + " WITH(NOLOCK) ");
                oSql.AppendLine("   WHERE FTAgnCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTAgnCode + "' AND FTBchCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTBchCode + "' AND FTXthDocNo = '" + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + "'");
                oSql.AppendLine();
                //TCNTPdtPickDTSN
                oSql.AppendLine("   DELETE DTSN ");
                oSql.AppendLine("   FROM TCNTPdtPickDTSN DTSN WITH(ROWLOCK) ");
                oSql.AppendLine("   WHERE DTSN.FTAgnCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTAgnCode + "' AND DTSN.FTBchCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTBchCode + "' AND DTSN.FTXthDocNo = '" + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + "'");
                oSql.AppendLine();
                oSql.AppendLine("   INSERT INTO TCNTPdtPickDTSN ");
                oSql.AppendLine("   SELECT * FROM " + tTblPickDTSN + " WITH(NOLOCK) ");
                oSql.AppendLine("   WHERE FTAgnCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTAgnCode + "' AND FTBchCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTBchCode + "' AND FTXthDocNo = '" + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + "'");
                oSql.AppendLine();
                //TCNTPdtPickDTDis
                oSql.AppendLine("   DELETE DTDis ");
                oSql.AppendLine("   FROM TCNTPdtPickDTDis DTDis WITH(ROWLOCK) ");
                oSql.AppendLine("   WHERE DTDis.FTAgnCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTAgnCode + "' AND DTDis.FTBchCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTBchCode + "' AND DTDis.FTXthDocNo = '" + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + "'");
                oSql.AppendLine();
                oSql.AppendLine("   INSERT INTO TCNTPdtPickDTDis ");
                oSql.AppendLine("   SELECT * FROM " + tTblPickDTDis + " WITH(NOLOCK) ");
                oSql.AppendLine("   WHERE FTAgnCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTAgnCode + "' AND FTBchCode = '" + oPdtPick.aoTCNTPdtPickHD[0].FTBchCode + "' AND FTXthDocNo = '" + oPdtPick.aoTCNTPdtPickHD[0].FTXthDocNo + "'");
                oSql.AppendLine();
                oSql.AppendLine("   COMMIT TRANSACTION");
                oSql.AppendLine("END TRY");
                oSql.AppendLine("BEGIN CATCH");
                oSql.AppendLine("   IF(@@TRANCOUNT > 0)");
                oSql.AppendLine("       ROLLBACK TRAN;");
                oSql.AppendLine("   THROW;");
                oSql.AppendLine("END CATCH");
                oDB.C_DATbExecuteNonQuery(poData.ptConnStr, oSql.ToString(), (int)poShopDB.nCommandTimeOut, out nRowAffect);

                return true;
            }
            catch (Exception oEx)
            {
                ptErrMsg = oEx.Message.ToString();
                cFunction.C_LOGxKeepLogErr(oEx.Message.ToString(), "C_PRCbUploadSale");
                return false;
            }
            finally
            {
                oSql = new StringBuilder();
                if (!string.IsNullOrEmpty(tTblPickHD))
                {
                    oSql.AppendLine("IF OBJECT_ID(N'" + tTblPickHD + "') IS NOT NULL BEGIN");
                    oSql.AppendLine("   DROP TABLE " + tTblPickHD);
                    oSql.AppendLine("END ");
                }
                if (!string.IsNullOrEmpty(tTblPickHDCst))
                {
                    oSql.AppendLine("IF OBJECT_ID(N'" + tTblPickHDCst + "') IS NOT NULL BEGIN");
                    oSql.AppendLine("   DROP TABLE " + tTblPickHDCst);
                    oSql.AppendLine("END ");
                }
                if (!string.IsNullOrEmpty(tTblPickHDDocRef))
                {
                    oSql.AppendLine("IF OBJECT_ID(N'" + tTblPickHDDocRef + "') IS NOT NULL BEGIN");
                    oSql.AppendLine("   DROP TABLE " + tTblPickHDDocRef);
                    oSql.AppendLine("END ");
                }
                if (!string.IsNullOrEmpty(tTblPickDT))
                {
                    oSql.AppendLine("IF OBJECT_ID(N'" + tTblPickDT + "') IS NOT NULL BEGIN");
                    oSql.AppendLine("   DROP TABLE " + tTblPickDT);
                    oSql.AppendLine("END ");
                }
                if (!string.IsNullOrEmpty(tTblPickDTSN))
                {
                    oSql.AppendLine("IF OBJECT_ID(N'" + tTblPickDTSN + "') IS NOT NULL BEGIN");
                    oSql.AppendLine("   DROP TABLE " + tTblPickDTSN);
                    oSql.AppendLine("END ");
                }
                if (!string.IsNullOrEmpty(tTblPickDTDis))
                {
                    oSql.AppendLine("IF OBJECT_ID(N'" + tTblPickDTDis + "') IS NOT NULL BEGIN");
                    oSql.AppendLine("   DROP TABLE " + tTblPickDTDis);
                    oSql.AppendLine("END ");
                }
                //if (!string.IsNullOrEmpty(tTblPickLocGrp))
                //{
                //    oSql.AppendLine("IF OBJECT_ID(N'" + tTblPickLocGrp + "') IS NOT NULL BEGIN");
                //    oSql.AppendLine("   DROP TABLE " + tTblPickLocGrp);
                //    oSql.AppendLine("END ");
                //}
                //if (!string.IsNullOrEmpty(tTblPickLocGrp_L))
                //{
                //    oSql.AppendLine("IF OBJECT_ID(N'" + tTblPickLocGrp_L + "') IS NOT NULL BEGIN");
                //    oSql.AppendLine("   DROP TABLE " + tTblPickLocGrp_L);
                //    oSql.AppendLine("END ");
                //}

                if (!string.IsNullOrEmpty(oSql.ToString()))
                {
                    new cDatabase().C_DATbExecuteNonQuery(poData.ptConnStr, oSql.ToString(), (int)poShopDB.nCommandTimeOut, out nRowAffect);
                }

                aoHD = null;
                aoHDCst = null;
                aoHDDocRef = null;
                aoDT = null;
                aoDTSN = null;
                //aoLocPickGrp = null;
                //aoLocPickGrpLng = null;
                aoDTDis = null;
                oSql = null;
                oPdtPick = null;
                oTranscation = null;
                oDB = null;
                oConn = null;
            }
        }
    }
}
