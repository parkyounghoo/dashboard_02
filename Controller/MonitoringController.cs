using KPC_Monitoring.Db;
using KPC_Monitoring.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace KPC_Monitoring.Controller
{
    public class MonitoringController
    {
        public DataSet Contact_Data(string date)
        {
            MonitoringDb db = new MonitoringDb();
            return db.getContact_Data(date);
        }

        public DataSet Private_Data(string date)
        {
            MonitoringDb db = new MonitoringDb();
            return db.getPrivate_Data(date);
        }

        public DataSet ETL_Data(string date)
        {
            MonitoringDb db = new MonitoringDb();
            return db.getETL_Data(date);
        }

        public DataSet Domain_Data(string date)
        {
            MonitoringDb db = new MonitoringDb();
            return db.getDomain_Data(date);
        }

        public DataSet Domain_Data_Total(string date)
        {
            MonitoringDb db = new MonitoringDb();
            return db.getDomain_Data_Total(date);
        }

        public DataSet Domain_Data_Chart(string date, string domainName, string gubun)
        {
            MonitoringDb db = new MonitoringDb();
            return db.getDomain_Data_Chart(date, domainName, gubun);
        }

        public DataSet Domain_Data_EDU(string date)
        {
            MonitoringDb db = new MonitoringDb();
            return db.getDomain_Data_EDU(date);
        }

        public DataSet Domain_Data_PROD(string date)
        {
            MonitoringDb db = new MonitoringDb();
            return db.getDomain_Data_PROD(date);
        }

        public DataSet Domain_Data_PRIV(string date)
        {
            MonitoringDb db = new MonitoringDb();
            return db.getDomain_Data_PRIV(date);
        }

        public List<MONI_DATA_MODEL> Sche_Status(string dbName, string tableName, string date)
        {
            MonitoringDb db = new MonitoringDb();
            DataSet ds = db.getSche_Status(dbName, tableName, date);

            List<MONI_DATA_MODEL> list = new List<MONI_DATA_MODEL>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                MONI_DATA_MODEL model = new MONI_DATA_MODEL();
                model.STATUS = StatusColor(dr["STEP_FLAG"].ToString());
                model.CREATE_DATE = dr["CREATE_DATE"].ToString();
                model.ERROR_LOG = dr["ERROR_LOG"].ToString();

                list.Add(model);
            }

            return list;
        }

        public List<MONI_DATA_MODEL> Private_Data_D(string date)
        {
            MonitoringDb db = new MonitoringDb();

            DataSet ds = db.getPrivate_Data_D(date);
            List<MONI_DATA_MODEL> list = new List<MONI_DATA_MODEL>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                MONI_DATA_MODEL model = new MONI_DATA_MODEL();
                model.TABLE_NAME = dr["TABLE_NAME"].ToString();
                model.TABLE_NAME_KOR = dr["TABLE_NAME_KOR"].ToString();
                model.ROWS = dr["ROWS"].ToString();
                model.TABLE_SIZE = dr["TABLE_SIZE"].ToString();
                model.TABLE_DESCRIPTION = dr["TABLE_DESCRIPTION"].ToString();
                model.CYCLE_NAME = dr["CYCLE_NAME"].ToString();

                list.Add(model);
            }

            return list;
        }

        public List<MONI_DATA_MODEL> ETL_Data_D(string date)
        {
            MonitoringDb db = new MonitoringDb();

            DataSet ds = db.getETL_Data_D(date);
            List<MONI_DATA_MODEL> list = new List<MONI_DATA_MODEL>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                MONI_DATA_MODEL model = new MONI_DATA_MODEL();
                model.STATUS_NAME = StatusName(dr["STEP_FLAG"].ToString());
                model.TABLE_NAME = dr["TABLE_NAME"].ToString();
                model.TABLE_NAME_KOR = dr["TABLE_NAME_KOR"].ToString();
                model.ROWS = dr["ROWS"].ToString();
                model.TABLE_SIZE = dr["TABLE_SIZE"].ToString();
                model.TABLE_DESCRIPTION = dr["TABLE_DESCRIPTION"].ToString();
                model.CYCLE_NAME = dr["CYCLE_NAME"].ToString();

                list.Add(model);
            }

            return list;
        }

        public List<MONI_DATA_MODEL> Contact_Data_D(string date)
        {
            MonitoringDb db = new MonitoringDb();

            DataSet ds = db.getContact_Data_D(date);
            List<MONI_DATA_MODEL> list = new List<MONI_DATA_MODEL>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                MONI_DATA_MODEL model = new MONI_DATA_MODEL();
                model.TABLE_NAME = dr["TABLE_NAME"].ToString();
                model.TABLE_NAME_KOR = dr["TABLE_NAME_KOR"].ToString();
                model.ROWS = dr["ROWS"].ToString();
                model.TABLE_SIZE = dr["TABLE_SIZE"].ToString();
                model.TABLE_DESCRIPTION = dr["TABLE_DESCRIPTION"].ToString();
                model.CYCLE_NAME = dr["CYCLE_NAME"].ToString();

                list.Add(model);
            }

            return list;
        }
        
        private string StatusColor(string status)
        {
            string color = "background-color: ";

            if (status == "Y")
            {
                color += "#338596";
            }
            else if (status == "N")
            {
                color += "#E7412D";
            }
            else if (status == "S")
            {
                color += "#FEA82B";
            }
            else
            {
                color += "#6C757D";
            }

            return color;
        }

        private string StatusName(string status)
        {
            string name = "";

            if (status == "Y")
            {
                name += "completed";
            }
            else if (status == "N")
            {
                name += "delayed";
            }
            else if (status == "S")
            {
                name += "on schedule";
            }
            else
            {
                name += "no operation";
            }

            return name;
        }
    }
}