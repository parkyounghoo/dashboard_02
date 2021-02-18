using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPC_Monitoring.Model
{
    public class MONI_Model
    {
        public string TABLE_1 { get; set; }
        public string TABLE_1_YN { get; set; }
        public string TABLE_2 { get; set; }
        public string TABLE_2_YN { get; set; }
        public string TABLE_3 { get; set; }
        public string TABLE_3_YN { get; set; }
        public string TABLE_4 { get; set; }
        public string TABLE_4_YN { get; set; }
    }

    public class BATCH_LIST_Model
    {
        public string API_NAME { get; set; } //API 명
        public string API_Purpose { get; set; } //사용목적
        public string API_Data_Collection { get; set; } //데이터수집처
        public string API_Data_Service { get; set; } //제공기관
        public string TABLE_NAME { get; set; } //테이블 명
        public string API_Status { get; set; } //상태
        public string ERROR_LOG { get; set; } //에러로그
    }

    public class MONI_TAB_1
    {
        public string dataCount { get; set; }
        public string tableCount { get; set; }
        public string complateY { get; set; }
        public string complateN { get; set; }
    }

    public class MONI_DATA_MODEL
    {
        public string STATUS { get; set; }
        public string STATUS_NAME { get; set; }
        public string TABLE_NAME { get; set; }
        public string TABLE_NAME_KOR { get; set; }
        public string ROWS { get; set; }
        public string TABLE_SIZE { get; set; }
        public string TABLE_DESCRIPTION { get; set; }
        public string CYCLE_NAME { get; set; }
        public string ERROR_LOG { get; set; }
        public string CREATE_DATE { get; set; }
    }
}