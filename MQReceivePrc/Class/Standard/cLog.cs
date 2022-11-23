using MQReceivePrc.Class.Standard;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace MQReceivePrc.Class
{
    public class cLog
    {
        private cSP oC_CNSP;

        #region Constructor

        public cLog()
        {
            try
            {
                oC_CNSP = new cSP();
            }
            catch (Exception oEx) { C_WRTxLog("cLog", "cLog " + oEx.Message); }
        }

        #endregion End Constructor

        /// <summary>
        /// Write log
        /// </summary>
        /// <param name="ptForm"></param>
        /// <param name="ptFunction"></param>
        public void C_WRTxLog(string ptForm, string ptFunction)
        {
            string tPath;

            try
            {
                tPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName + @"\Log";
                if (!Directory.Exists(tPath))
                    Directory.CreateDirectory(tPath);

                tPath += @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                if (!File.Exists(tPath))
                    File.Create(tPath).Dispose();

                using (StreamWriter oOutputFile = new StreamWriter(tPath, true))
                {
                    oOutputFile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " : Form (" + ptForm + ") > " + ptFunction);
                    oOutputFile.Dispose();
                }
            }
            catch (Exception oEx) { Debug.WriteLine(oEx.ToString()); }
            finally
            {
                tPath = null;
                ptForm = null;
                ptFunction = null;
                //oC_CNSP.SP_CLExMemory();
            }
        }

        /// <summary>
        /// *Arm 65-08-24
        /// Write Log Monitor File
        /// Format : "HH:mm:ss : From (${ptForm}) > ${ptMessage}"
        /// </summary>
        /// <param name="ptForm"></param>
        /// <param name="ptMessage"></param>
        public void C_WRTxLogMonitor(string ptForm, string ptMessage, bool pbAlwKeepLog)
        {
            string tPath;

            try
            {
                if (pbAlwKeepLog == false) return;

                tPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName + @"\Log";
                if (!Directory.Exists(tPath))
                    Directory.CreateDirectory(tPath);

                tPath += @"\Monitor_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                if (!File.Exists(tPath))
                    File.Create(tPath).Dispose();

                using (StreamWriter oOutputFile = new StreamWriter(tPath, true))
                {
                    oOutputFile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " : Form (" + ptForm + ") > " + ptMessage);
                    oOutputFile.Close();
                    oOutputFile.Dispose();
                }
            }
            catch (Exception oEx) { Debug.WriteLine(oEx.ToString()); }
            finally
            {
                tPath = null;
                ptForm = null;
                ptMessage = null;
            }
        }
    }
}
