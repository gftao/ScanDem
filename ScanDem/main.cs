using System;

namespace scandmo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //init config file 
            Conf conf = new Conf();
           // if (Hyscan.Init_conf(conf, System.AppDomain.CurrentDomain.BaseDirectory + "\\app.ini") != 0)
            if (Hyscan.Init_conf(conf, "./app.ini") != 0)
            {
                Console.WriteLine("配置文件错误,请检查配置文件!");
                return ;
            }
            Console.WriteLine("商户号："+conf.mcht_cd);
            //反扫支付
             Hyscan.T1131(conf);

        }
    }
}
