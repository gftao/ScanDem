using System;
namespace scandmo
{
  
      public class MsgHead
        {
            public string version { get; set; }
            public string encodeing { get; set; }
            public string sign_method { get; set; }
            public string signature { get; set; }
            public string msg_body { get; set; }


            public void init()
            {
                this.version = "1.0.1";
                this.encodeing = "UTF-8";
                this.sign_method = "01";
            }

        };


        public class msg_body_ans
        {
            public string prod_cd { get; set; }
            public string biz_cd { get; set; }
            public string tran_cd { get; set; }
            public string mcht_cd { get; set; }
            public string tran_dt_tm { get; set; }
            public string ins_id_cd { get; set; }
            public string order_id { get; set; }
            public string tran_amt { get; set; }
            public string term_id { get; set; }
            public qr_code_ans qr_code_info { get; set; }

            public string resp_cd { get; set; }
            public string resp_msg { get; set; }
            public string orig_resp_cd { get; set; }
            public string orig_resp_msg { get; set; }
            public string sys_order_id { get; set; }
            public string orig_sys_order_id { get; set; }

            //public qr_code_info qr_code_info { get; set; }
            //public string liq_time { get; set; }
            //public string channel_id { get; set; }
            //public string pay_url { get; set; }
        }


        public class msg_body
        {
            public string prod_cd { get; set; }
            public string biz_cd { get; set; }
            public string tran_cd { get; set; }
            public string mcht_cd { get; set; }
            public string tran_dt_tm { get; set; }
            public string ins_id_cd { get; set; }
            public string order_id { get; set; }
            public string tran_amt { get; set; }
            public string term_id { get; set; }
            public qr_code qr_code_info { get; set; }
            public string orig_sys_order_id { get; set; }
        }

        public class qr_code_ans
        {
            public string auth_code { get; set; }
            public string scance { get; set; }
            public string channel_id { get; set; }
            public string qr_code { get; set; }
            public string cash_amt { get; set; }

        }
        public class qr_code
        {
            public string auth_code { get; set; }
            public string scance { get; set; }
        }
 
        public class Conf
        {
            public string mcht_cd;
            public string term_id;
            public string prod_cd;
            public string url;
            public string mcht_nm;
            public string ins_id_cd;
            public string wxchat;
            public string timeout;
        }

}
