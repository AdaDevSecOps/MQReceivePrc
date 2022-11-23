using MQReceivePrc.Class.Standard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQReceivePrc.Class
{
	public class cRate
	{
		public static string C_GETtIsoName(string ptConStr, string ptAgnCode, string ptIsoCode)
		{
			StringBuilder oSql;
			cDatabase oDB;
			string tIsoName = "";
			try
			{
				if (String.IsNullOrEmpty(ptIsoCode)) return "";

				oDB = new cDatabase();
				oSql = new StringBuilder();
				oSql.Clear();
				oSql.AppendLine($"SELECT RTEL.FTRteName ");
				oSql.AppendLine($"FROM TFNMRate RTE WITH(NOLOCK) ");
				oSql.AppendLine($"LEFT JOIN TFNMRate_L RTEL WITH(NOLOCK) ON ");
				oSql.AppendLine($"	  RTE.FTRteCode = RTEL.FTRteCode AND RTEL.FNLngID = {cVB.nVB_Language} ");
				oSql.AppendLine($"WHERE RTE.FTRteIsoCode = '{ptIsoCode}' ");
				oSql.AppendLine($"	  AND ( ISNULL(RTE.FTAgnCode,'') = '' OR ISNULL(RTE.FTAgnCode,'') = '{ptAgnCode}' ) ");
				oSql.AppendLine($"ORDER BY ISNULL(RTE.FTAgnCode,'') ");

				tIsoName = oDB.C_DAToExecuteQuery<string>(ptConStr, oSql.ToString(), cVB.nVB_CmdTime);

				return tIsoName;
			}
			catch (Exception oEx)
			{
				new cLog().C_WRTxLog("cRate", "C_GETtIsoName : " + oEx.Message);
			}
			return "";
		}
		public static string C_GETtIsoCode(string ptConStr, string ptAgnCode, string ptRteCode)
		{
			StringBuilder oSql;
			cDatabase oDB;
			string tIsoCode = "";
			try
			{
				if (String.IsNullOrEmpty(ptRteCode)) return "";

				oDB = new cDatabase();
				oSql = new StringBuilder();
				oSql.Clear();
				oSql.AppendLine($"SELECT RTE.FTRteIsoCode ");
				oSql.AppendLine($"FROM TFNMRate RTE WITH(NOLOCK) ");
				oSql.AppendLine($"WHERE RTE.FTRteCode = '{ptRteCode}' ");
				oSql.AppendLine($"	  AND ( ISNULL(RTE.FTAgnCode,'') = '' OR ISNULL(RTE.FTAgnCode,'') = '{ptAgnCode}' ) ");
				oSql.AppendLine($"ORDER BY ISNULL(RTE.FTAgnCode,'') ");

				tIsoCode = oDB.C_DAToExecuteQuery<string>(ptConStr, oSql.ToString(), cVB.nVB_CmdTime);

				return tIsoCode;
			}
			catch (Exception oEx)
			{
				new cLog().C_WRTxLog("cRate", "C_GETtIsoCode : " + oEx.Message);
			}
			return "";
		}

		public static decimal C_PRCcConvertIsoCode(string ptConStr, string ptAgnCode, string ptIsoFrm, string ptIsoTo, decimal pcAmtFrm)
		{
			StringBuilder oSql;
			cDatabase oDB;
			DataTable oDbTblRate;
			decimal cIsoRteFrm = 0;
			decimal cIsoRteTo = 0;
			decimal cAmtTo = 0;
			try
			{
				if (String.IsNullOrEmpty(ptIsoFrm)) return pcAmtFrm;
				if (String.IsNullOrEmpty(ptIsoTo)) return pcAmtFrm;

				oDB = new cDatabase();
				oSql = new StringBuilder();
				oDbTblRate = new DataTable();

				oSql.Clear();
				oSql.AppendLine($"SELECT ");
				oSql.AppendLine($"(SELECT TOP 1 ISNULL(FCRteRate, 0) FROM TFNMRate WHERE FTRteIsoCode = '{ptIsoFrm}' ");
				oSql.AppendLine($"    AND (ISNULL(FTAgnCode,'') = '' OR ISNULL(FTAgnCode,'') = '{ptAgnCode}' ) ");
				oSql.AppendLine($"    ORDER BY ISNULL(FTAgnCode,'') DESC ) AS FCIsoRteFrm");
				oSql.AppendLine($", (SELECT TOP 1 ISNULL(FCRteRate, 0) FROM TFNMRate WHERE FTRteIsoCode = '{ptIsoTo}' ");
				oSql.AppendLine($"    AND (ISNULL(FTAgnCode,'') = '' OR ISNULL(FTAgnCode,'') = '{ptAgnCode}' ) ");
				oSql.AppendLine($"    ORDER BY ISNULL(FTAgnCode,'') DESC ) AS FCIsoRteTo");
				oDbTblRate = oDB.C_DAToExecuteQuery(ptConStr, oSql.ToString(), cVB.nVB_CmdTime);
				if (oDbTblRate == null || oDbTblRate.Rows.Count == 0) return pcAmtFrm;

				if (oDbTblRate.Rows[0].Field<decimal>("FCIsoRteFrm") == 0) return pcAmtFrm;
				if (oDbTblRate.Rows[0].Field<decimal>("FCIsoRteTo") == 0) return pcAmtFrm;

				cIsoRteFrm = oDbTblRate.Rows[0].Field<decimal>("FCIsoRteFrm");
				cIsoRteTo = oDbTblRate.Rows[0].Field<decimal>("FCIsoRteTo");

				cAmtTo = (pcAmtFrm * cIsoRteTo) / cIsoRteFrm;

				return cAmtTo;
			}
			catch (Exception oEx)
			{
				new cLog().C_WRTxLog("cRate", "C_GETtRateName : " + oEx.Message);
			}
			return pcAmtFrm;
		}


		/// <summary>
		/// Check Diff Iso Rate Local and Contry
		/// </summary>
		/// <returns>true:Diff false:Same or Default</returns>
		public static bool C_CHKbDiffIsoRate(string ptIsoRteLocal, string ptIsoRteCty) //Net 65-09-03
		{
			try
			{
				if (String.IsNullOrEmpty(ptIsoRteLocal)) return false;
				if (String.IsNullOrEmpty(ptIsoRteCty)) return false;

				if (ptIsoRteCty == ptIsoRteLocal) return false;

				return true;
			}
			catch (Exception oEx)
			{
				new cLog().C_WRTxLog("cRate", "C_CHKbDiffIsoRate : " + oEx.Message);
			}
			return false;
		}
	}
}
