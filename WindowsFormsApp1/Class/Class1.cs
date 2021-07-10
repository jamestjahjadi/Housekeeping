using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1.Class
{
    class backEndHouseKeeping
    {
       
        public int id { get; set; }
        public string ParameterYear { get; set; }
        public string Message { get; set; }

        public bool Checked { get; set; }

        // Connection with Config
        static string conn = ConfigurationManager.ConnectionStrings["connBackup"].ConnectionString;


        public DataTable Select ()
        {
            //database connection
            SqlConnection backUpConn = new SqlConnection(conn);
            DataTable dt = new DataTable();
            try
            {
                // selecting sql query 
                string sql = "select * from [BackupHistory]";

                //creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, backUpConn);

                // creating SQL Data adapter using CMD 
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                backUpConn.Open();
                adapter.Fill(dt);
            }
            catch (Exception)
            {

            }
            finally
            {
                backUpConn.Close();
            }
            return dt;
        }

       



        // BACKUP CLASS

        public bool Backup(backEndHouseKeeping BE)
        {
            bool isSuccess = false;
            string config = ConfigurationManager.AppSettings["BackUpFile"];
            string pathFolder = ConfigurationManager.AppSettings["PathFolder"];
            string database = ConfigurationManager.AppSettings["databaseName"];
            var time = DateTime.Now;
            string formattedTime = time.ToString("yyyyMMdd_hhmmss");
            string nameDb = "Activo_" + formattedTime;
            SqlConnection backUpConn = new SqlConnection(conn);
            try
            {
                string master = "use master";

                // create db
                string createDB = "create database " + nameDb;

                // restore database
                string restore = "RESTORE DATABASE " + nameDb + " FROM  DISK =  N'" + pathFolder + nameDb + ".bak' WITH  FILE = 1, MOVE N'" + database +"_Data' TO N'" + config + nameDb + ".mdf',  MOVE N'" + database +"_Log' TO N'" + config + nameDb + ".ldf',  NOUNLOAD, REPLACE, STATS = 5";

                // back-up database
                string backupDb = "BACKUP DATABASE [" + database + "] TO DISK = N'" + pathFolder + nameDb + ".bak' WITH NOFORMAT, NOINIT, SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                // delete DB existing 
                string sqlExisting = "delete from [" + database +"].[dbo].[TransactionAttachments] where YEAR(SCreateDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlAssetAccessory = "delete from [" + database +"].[dbo].[AssetAccessory] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlAssetAdditionalCost = "delete from [" + database +"].[dbo].[AssetAdditionalCost] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlAssetCostAllocation = "delete from [" + database +"].[dbo].[AssetCostAllocation ] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlAssetGeolocation = "delete from [" + database +"].[dbo].[AssetGeolocation] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlAssetMaintenance = "delete from [" + database +"].[dbo].[AssetMaintenance] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlAssetPartialPayment = "delete from [" + database +"].[dbo].[AssetPartialPayment ] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlDepLastUsedAppraisal = "delete from [" + database +"].[dbo].[DepLastUsedAppraisal] where YEAR(PeriodYear) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlGLGenerateManual = "delete from [" + database +"].[dbo].[GLGenerateManual] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlJurnalBatch = "delete from [" + database +"].[dbo].[JurnalBatch] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlJurnalBatchDetail = "delete from [" + database +"].[dbo].[JurnalBatchDetail] where YEAR(ReturnDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqljurnalrevaluationasset = "delete from [" + database +"].[dbo].[jurnalrevaluationasset] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlassets = "delete from [" + database +"].[dbo].[Assets] where YEAR(SCreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlsystemlog = "delete from [" + database +"].[dbo].[systemlog] where YEAR(SCreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlHistRunTaskMail = "delete from [" + database +"].[dbo].[HistRunTaskMail] where YEAR(SubmittedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlComment = "delete from [" + database +"].[dbo].[Comment] where YEAR(DCreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlThread = "delete from [" + database +"].[dbo].[Thread] where YEAR(DCreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlJurnalDisposalAsset = "delete from [" + database +"].[dbo].[JurnalDisposalAsset] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlAssetDisposal = "delete from [" + database +"].[dbo].[AssetDisposal ] where YEAR(DCreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlAssetReverseDisposal = "delete from [" + database +"].[dbo].[AssetReverseDisposal] where YEAR(DCreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlJurnalmovementAsset = "delete from [" + database +"].[dbo].[JurnalmovementAsset] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlAssetTransferDetail = "delete from [" + database +"].[dbo].[AssetTransferDetail] where YEAR(DPurchaseDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlAssetTransfer = "delete from [" + database +"].[dbo].[AssetTransfer] where YEAR(DCreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlHistTransactionDrafts = "delete from [" + database +"].[dbo].[HistTransactionDrafts] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlBarcodingLog = "delete from [" + database +"].[dbo].[BarcodingLog ] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlDepRequest = "delete from [" + database +"].[dbo].[DepRequest ] where YEAR(CreatedDate) <= YEAR(GETDATE())-" + BE.ParameterYear + ";";
                string sqlDepAssetUnDepreciatedReport = "delete from [" + database +"].[dbo].[DepAssetUnDepreciatedReport] where DepPeriodId in (select DepPeriodId from [" + database +"].[dbo].[DepPeriod] where NPeriodYear <= YEAR(GETDATE())-" + BE.ParameterYear + ");";
                string sqlDepAssetDetail = "delete from [" + database +"].[dbo].[DepAssetDetail] where DepPeriodId in (select DepPeriodId from [" + database +"].[dbo].[DepPeriod] where NPeriodYear <= YEAR(GETDATE())-" + BE.ParameterYear + ")";
                string sqlDepAssetDetailAllocation = "delete from [" + database +"].[dbo].[DepAssetDetailAllocation] where DepPeriodId in (select DepPeriodId from [" + database +"].[dbo].[DepPeriod] where NPeriodYear <= YEAR(GETDATE())-" + BE.ParameterYear + ")";
                string sqlRequestDep = "delete from [" + database +"].[dbo].[DepRequest] where DepPeriodId in (select DepPeriodId from [" + database +"].[dbo].[DepPeriod] where NPeriodYear <= YEAR(GETDATE())-" + BE.ParameterYear + ")";
                string sqlDepPeriod = "delete from [" + database +"].[dbo].[DepPeriod] where NPeriodYear<=YEAR(GETDATE())-" + BE.ParameterYear + "";
                string sqlDepAccumulation = "delete from [" + database +"].[dbo].[DepAccumulation] where YEAR(calculateddate)<=YEAR(GETDATE())-" + BE.ParameterYear + "";
                string sqlCreateUser = "delete from [" + database +"].[dbo].[TransactionAttachments] where SCreateUser is NULL";
                string sqlHistRunWorkflow = "delete from [" + database +"].[dbo].[HistRunWorkflow] where RunWorkflowID in (select RunWorkflowID from [" + database +"].[dbo].[HistRunTask] where MsgSystem = 'Completed Approved' and YEAR(sumitteddate) <= YEAR(GETDATE())-" + BE.ParameterYear + ")";


                // SQLCOMMAND 
                SqlCommand cmdMaster = new SqlCommand(master, backUpConn);
                SqlCommand cmdBackup = new SqlCommand(backupDb, backUpConn);
                cmdBackup.CommandTimeout = 1500;

                SqlCommand cmd3 = new SqlCommand(restore, backUpConn);
                cmd3.CommandTimeout = 1500;
                SqlCommand cmdCreate = new SqlCommand(createDB, backUpConn);


                SqlCommand cmd4 = new SqlCommand(sqlExisting, backUpConn);
                SqlCommand cmdAsset = new SqlCommand(sqlAssetAccessory, backUpConn);
                SqlCommand cmdAssetAdditionalCost = new SqlCommand(sqlAssetAdditionalCost, backUpConn);
                SqlCommand cmdBEAssetCostAllocation = new SqlCommand(sqlAssetCostAllocation, backUpConn);
                SqlCommand cmdsqlGeolocation = new SqlCommand(sqlAssetGeolocation, backUpConn);
                SqlCommand cmdSqlAssetMaintenance = new SqlCommand(sqlAssetMaintenance, backUpConn);
                SqlCommand cmdSqlAssetPartialPayment = new SqlCommand(sqlAssetPartialPayment, backUpConn);
                SqlCommand cmdsqlDepLastUsedAppraisal = new SqlCommand(sqlDepLastUsedAppraisal, backUpConn);
                SqlCommand cmdSqlJurnalBatch = new SqlCommand(sqlJurnalBatch, backUpConn);
                SqlCommand cmdsqlJurnalBatchDetail = new SqlCommand(sqlJurnalBatchDetail, backUpConn);
                SqlCommand cmdsqljurnalrevaluationasset = new SqlCommand(sqljurnalrevaluationasset, backUpConn);
                SqlCommand cmdsqlassets = new SqlCommand(sqlassets, backUpConn);
                SqlCommand cmdsqlsystemlog = new SqlCommand(sqlsystemlog, backUpConn);
                cmdsqlsystemlog.CommandTimeout = 3600;
                SqlCommand cmdsqlHistRunTaskMail = new SqlCommand(sqlHistRunTaskMail, backUpConn);
                SqlCommand cmdsqlComment = new SqlCommand(sqlComment, backUpConn);
                SqlCommand cmdsqlThread = new SqlCommand(sqlThread, backUpConn);
                SqlCommand cmdsqlJurnalDisposalAsset = new SqlCommand(sqlJurnalDisposalAsset, backUpConn);
                SqlCommand cmdsqlAssetDisposal = new SqlCommand(sqlAssetDisposal, backUpConn);
                SqlCommand cmdsqlAssetReverseDisposal = new SqlCommand(sqlAssetReverseDisposal, backUpConn);
                SqlCommand cmdsqlJurnalmovementAsset = new SqlCommand(sqlJurnalmovementAsset, backUpConn);
                SqlCommand cmdsqlAssetTransferDetail = new SqlCommand(sqlAssetTransferDetail, backUpConn);
                SqlCommand cmdsqlAssetTransfer = new SqlCommand(sqlAssetTransfer, backUpConn);
                SqlCommand cmdsqlHistTransactionDrafts = new SqlCommand(sqlHistTransactionDrafts, backUpConn);
                SqlCommand cmdsqlBarcodingLog = new SqlCommand(sqlBarcodingLog, backUpConn);
                SqlCommand cmdsqlDepRequest = new SqlCommand(sqlDepRequest, backUpConn);
                SqlCommand cmdsqlDepAssetUnDepreciatedReport = new SqlCommand(sqlDepAssetUnDepreciatedReport, backUpConn);
                SqlCommand cmdsqlDepAssetDetail = new SqlCommand(sqlDepAssetDetail, backUpConn);
                SqlCommand cmdsqlDepAssetDetailAllocation = new SqlCommand(sqlDepAssetDetailAllocation, backUpConn);
                SqlCommand cmdsqlRequestDep = new SqlCommand(sqlRequestDep, backUpConn);
                SqlCommand cmdsqlDepPeriod = new SqlCommand(sqlDepPeriod, backUpConn);
                SqlCommand cmdsqlDepAccumulation = new SqlCommand(sqlDepAccumulation, backUpConn);
                SqlCommand cmdsqlCreateUser = new SqlCommand(sqlCreateUser, backUpConn);
                SqlCommand cmdsqlHistRunWorkflow = new SqlCommand(sqlHistRunWorkflow, backUpConn);

              

                // Adding Parameter
                cmd4.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdAsset.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdAssetAdditionalCost.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdBEAssetCostAllocation.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlGeolocation.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdSqlAssetMaintenance.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdSqlAssetPartialPayment.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlDepLastUsedAppraisal.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdSqlJurnalBatch.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlJurnalBatchDetail.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqljurnalrevaluationasset.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlassets.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlsystemlog.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlHistRunTaskMail.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlComment.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlThread.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlJurnalDisposalAsset.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlAssetDisposal.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlAssetReverseDisposal.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlJurnalmovementAsset.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlAssetTransferDetail.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlHistTransactionDrafts.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlBarcodingLog.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlDepRequest.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlDepAssetUnDepreciatedReport.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlDepAssetDetail.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlDepAssetDetailAllocation.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlRequestDep.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlDepPeriod.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlDepAccumulation.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmdsqlHistRunWorkflow.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);


                // OPEN CONNECTION
                backUpConn.Open();

                if (BE.Checked == true)
                {
                    cmdMaster.ExecuteNonQuery();
                    cmdBackup.ExecuteNonQuery();
                }
                else
                {
                    cmdMaster.ExecuteNonQuery();
                    cmdBackup.ExecuteNonQuery();
                    // CREATE DB
                    cmdCreate.ExecuteNonQuery();
                    // RESTORE DB 
                    cmd3.ExecuteNonQuery();
                }

                cmd4.ExecuteNonQuery();
                cmdAsset.ExecuteNonQuery();
                cmdAssetAdditionalCost.ExecuteNonQuery();
                cmdBEAssetCostAllocation.ExecuteNonQuery();
                cmdsqlGeolocation.ExecuteNonQuery();
                cmdSqlAssetMaintenance.ExecuteNonQuery();
                cmdSqlAssetPartialPayment.ExecuteNonQuery();
                cmdsqlDepLastUsedAppraisal.ExecuteNonQuery();
                cmdSqlJurnalBatch.ExecuteNonQuery();
                cmdsqlJurnalBatchDetail.ExecuteNonQuery();
                cmdsqljurnalrevaluationasset.ExecuteNonQuery();
                cmdsqlassets.ExecuteNonQuery();
                cmdsqlsystemlog.ExecuteNonQuery(); 
                cmdsqlHistRunTaskMail.ExecuteNonQuery();
                cmdsqlComment.ExecuteNonQuery();
                cmdsqlThread.ExecuteNonQuery();
                cmdsqlJurnalDisposalAsset.ExecuteNonQuery();
                cmdsqlAssetDisposal.ExecuteNonQuery();
                cmdsqlAssetReverseDisposal.ExecuteNonQuery();
                cmdsqlJurnalmovementAsset.ExecuteNonQuery();
                cmdsqlAssetTransferDetail.ExecuteNonQuery();
                cmdsqlAssetTransfer.ExecuteNonQuery();
                cmdsqlHistTransactionDrafts.ExecuteNonQuery();
                cmdsqlBarcodingLog.ExecuteNonQuery();
                cmdsqlDepRequest.ExecuteNonQuery();
                cmdsqlDepAssetUnDepreciatedReport.ExecuteNonQuery();
                cmdsqlDepAssetDetail.ExecuteNonQuery();
                cmdsqlDepAssetDetailAllocation.ExecuteNonQuery();
                cmdsqlRequestDep.ExecuteNonQuery();
                cmdsqlDepPeriod.ExecuteNonQuery();
                cmdsqlDepAccumulation.ExecuteNonQuery();
                cmdsqlCreateUser.ExecuteNonQuery();

               


                // BACKUP DB
                int rows = cmdsqlHistRunWorkflow.ExecuteNonQuery();

                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false; 
                }

            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                if (isSuccess == false)
                {
                    System.Windows.Forms.MessageBox.Show(sqlException.Message);
                }
                else
                {
                    isSuccess = true;
                }
            }
            finally
            {
                backUpConn.Close(); 
            }
            return isSuccess;
        }

        public bool  Insert (backEndHouseKeeping BE)
        {
            // default value 
            bool isSuccess = false;
            
            //Connect Database 
            SqlConnection insconn = new SqlConnection(conn);
            
            try
            {
                //query insert data 
                
                string insertSql = "INSERT INTO BackupHistory (ParameterYear, [Message], RequestDate) values (@ParameterYear , @Message, CURRENT_TIMESTAMP);";

                //SQL Command 
               
                SqlCommand cmd = new SqlCommand(insertSql, insconn);
                
                //Parameter add data
                cmd.Parameters.AddWithValue("@ParameterYear", BE.ParameterYear);
                cmd.Parameters.AddWithValue("@Message", BE.Message);



                //Open Connection 
                insconn.Open();
                
                int rows = cmd.ExecuteNonQuery();
                
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }


            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                if (isSuccess == false)
                {
                    System.Windows.Forms.MessageBox.Show(sqlException.Message);
                }
                else
                {
                    isSuccess = true;
                }
            }
            finally
            {
                insconn.Close();
            }
            return isSuccess;
        }
    }
}
