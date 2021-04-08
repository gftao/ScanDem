using System;
using System.IO;
using scandmo;
using System.Net;
using Newtonsoft.Json;

namespace scandmo
{
    public class Hyscan
    {

        public static void T1131(Conf conf)
        {

            MsgHead msghead = new MsgHead();
            msghead.init();
            msg_body scan_body = new msg_body();
            scan_body.prod_cd = conf.prod_cd;
            scan_body.ins_id_cd = conf.ins_id_cd;
            scan_body.mcht_cd = conf.mcht_cd;
            scan_body.term_id = conf.term_id;

            scan_body.biz_cd = "0000007";

            scan_body.tran_cd = "1131";
            scan_body.tran_amt = "1";
            scan_body.order_id = "202104070000001";
            scan_body.qr_code_info = new qr_code();
            scan_body.qr_code_info.auth_code = "付款条码";//付款条码
            scan_body.qr_code_info.scance = "TERM";

            msghead.msg_body = JsonConvert.SerializeObject(scan_body);//json 转换
            msghead.signature = hyrsa.RSAUtil.RSASign(msghead.msg_body, "pra.key");//签名

            string temp = JsonConvert.SerializeObject(msghead);//
            Console.WriteLine("请求报文:" + temp);

            string html;
            // https post
            try
            {
                html = hyhttp.HttpsPost(conf.url, temp, conf.timeout);
                //html = temp;
            }
            catch (WebException webEx)
            {
                if (webEx.Status == WebExceptionStatus.Timeout)
                {
                    //发起查询
                    //T5091(conf, rc, rs, "1");
                    return;

                }
                else
                {
                    //rs.resp_cd = "P6";
                    //rs.resp_nm = "后台通讯出错";
                    return;
                }

            }

            //解析结构体
            MsgHead jo = JsonConvert.DeserializeObject<MsgHead>(html);
            if (jo == null)
            {
                //rs.resp_cd = "P9";
                //rs.resp_nm = "读取应答出错";
                return;
            }
            string sign_data = jo.signature;
            string sign_srcdata = jo.msg_body;

            //验证签名
            bool sign = hyrsa.RSAUtil.Verify(sign_srcdata, sign_data, "pub.key");
            if (!sign)
            {
                //rs.resp_cd = "P5";
                //rs.resp_nm = "验证签名失败";
                return;
            }

            //解析响应报文
            msg_body_ans body_ans = JsonConvert.DeserializeObject<msg_body_ans>(sign_srcdata);
            Console.Write("1131" + "body_ans: " + body_ans + "sign_srcdata" + sign_srcdata);

            Console.Write("resp_cd:" + body_ans.resp_cd);
            Console.Write("resp_cd:" + body_ans.resp_msg);
            Console.Write("resp_cd:" + body_ans.order_id);

            return;

        }

        public  static int Init_conf(Conf conf, string iniFilePath)
        {
            Console.WriteLine("配置文件：", iniFilePath);
            if (File.Exists(iniFilePath))
            {
                Console.WriteLine("读取配置文件");     
                conf.mcht_cd = OperateIniFile.ReadIniData("scan", "mcht_cd", iniFilePath);
                conf.term_id = OperateIniFile.ReadIniData("scan", "term_id",  iniFilePath);
                conf.prod_cd = OperateIniFile.ReadIniData("scan", "prod_cd", iniFilePath);
                conf.url = OperateIniFile.ReadIniData("scan", "url", iniFilePath);
                conf.mcht_nm = OperateIniFile.ReadIniData("scan", "mcht_nm", iniFilePath);
                conf.ins_id_cd = OperateIniFile.ReadIniData("scan", "ins_id_cd", iniFilePath);
                conf.wxchat = OperateIniFile.ReadIniData("scan", "wxchat", iniFilePath);
                conf.timeout = OperateIniFile.ReadIniData("scan", "time_out", iniFilePath);
            }
            else
            {
                return -1;
            }

            return 0;
        }

    }
}
