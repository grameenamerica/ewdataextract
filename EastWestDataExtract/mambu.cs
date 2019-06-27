using System;
using RestSharp;
using System.Net;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;

using System.Data;
using System.Data.OleDb;
// using System.Globalization;
using System.IO;


namespace EastWestDataExtract
{
    class mambu
    {
        string mambuUri = System.Configuration.ConfigurationSettings.AppSettings["MambuUrl"];
        string userId = System.Configuration.ConfigurationSettings.AppSettings["ApiUerId"];
        string password = System.Configuration.ConfigurationSettings.AppSettings["ApiPassword"];



        public string pullEWAccounst()

        {

            string mamburul, mambuUserid, mambuPW, payLoad;
            string eastWestAccounts = "";
            DateTime curDate;
            curDate = System.DateTime.Now;
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");


            LogFile lf = new LogFile();

            mamburul = mambuUri + "/api/loans/search?offset=0&limit=10";
            payLoad = "{\"filterConstraints\":[{\"filterSelection\":\"8a818fec63c590ad0163ccd35d776a0b\",\"filterElement\":\"EQUALS\",\"value\":\"East West Bank - Approved\",\"dataFieldType\":\"CUSTOM\"}]}";

            mambuUserid = userId;
            mambuPW = password;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient(mamburul);
            var request = new RestRequest(Method.POST);

            try
            {

                request.AddHeader("cache-control", "no-cache");
                client.Authenticator = new HttpBasicAuthenticator(mambuUserid, mambuPW);

                request.AddHeader("content-type", "application/json");

                request.AddParameter("application/json", payLoad, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                object con = response.Content;
                eastWestAccounts = con.ToString();

                if (eastWestAccounts != "")
                {
                    if (response.StatusCode.ToString() != "OK")

                    {
                        lf.writeLog("Function:mambu.pullEWAccounst. Date: " + DateTime.Now.ToString() + ". ", eastWestAccounts);
                        eastWestAccounts = "";
                    }
                }

                else

                {
                    lf.writeLog("Function:mambu.pullEWAccounst. Date: " + DateTime.Now.ToString() + ".", response.ErrorMessage.ToString());
                }

                return eastWestAccounts;
            }

            catch (System.Exception ex)
            {
                lf.writeLog("Function:mambu.pullEWAccounst. Operation Date: " + DateTime.Now.ToString() + ". ", ex.Message.ToString());
                return eastWestAccounts;
            }
        }

        public string getLoanCategory(string accountId)

        {
            string reqUrl, mambuUserid, mambuPW, responseString;

            LogFile lf = new LogFile();

            mambuUserid = userId;
            mambuPW = password;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                reqUrl = mambuUri + "/api/loans/" + accountId + "/custominformation/CAT";
                var client = new RestClient(reqUrl);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                client.Authenticator = new HttpBasicAuthenticator(mambuUserid, mambuPW);

                request.AddHeader("content-type", "application/json");
                IRestResponse response = client.Execute(request);

                object con = response.Content;
                responseString = con.ToString();

                if (responseString != "")
                {
                    if (response.StatusCode.ToString() == "OK")
                    {
                        JArray loanCat = JArray.Parse(responseString);
                        return loanCat[0]["value"].ToString();
                    }

                    else

                    {
                        lf.writeLog("Function:mambu.getLoanCategory. Account Id:"+accountId+". Error-", responseString);
                        return "";
                    }
                }

                else

                {
                    lf.writeLog("Function:mambu.getLoanCategory. Account Id:" + accountId + ". Error-", response.ErrorMessage.ToString());
                    return "";
                }
            }

            catch (System.Exception ex)

            {
                lf.writeLog("Function:mambu.getLoanCategory. Account Id:" + accountId + ". Error-", ex.Message.ToString());
                return "";
            }
        }

        public string getLoanPurpose(string accountId)

        {
            string reqUrl, mambuUserid, mambuPW, responseString;

            LogFile lf = new LogFile();

            mambuUserid = userId;
            mambuPW = password;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                reqUrl = mambuUri + "/api/loans/" + accountId + "/custominformation/PURP";
                var client = new RestClient(reqUrl);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                client.Authenticator = new HttpBasicAuthenticator(mambuUserid, mambuPW);

                request.AddHeader("content-type", "application/json");
                IRestResponse response = client.Execute(request);

                object con = response.Content;
                responseString = con.ToString();

                if (responseString != "")
                {
                    if (response.StatusCode.ToString() == "OK")
                    {
                        JArray loanCat = JArray.Parse(responseString);
                        return loanCat[0]["value"].ToString();
                    }

                    else

                    {
                        lf.writeLog("Function:mambu.getLoanPurpose. Account Id:" + accountId + ". Error-", responseString);
                        return "";
                    }
                }

                else

                {
                    lf.writeLog("Function:mambu.getLoanPurpose. Account Id:" + accountId + ". Error-", response.ErrorMessage.ToString());
                    return "";
                }
            }

            catch (System.Exception ex)

            {
                lf.writeLog("Function:mambu.getLoanPurpose. Account Id:" + accountId + ". Error-", ex.Message.ToString());
                return "";
            }
        }

        public string getLoanMonthlyIncome(string accountId)

        {
            string reqUrl, mambuUserid, mambuPW, responseString;

            LogFile lf = new LogFile();

            mambuUserid = userId;
            mambuPW = password;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                reqUrl = mambuUri + "/api/loans/" + accountId + "/custominformation/MOINC";
                var client = new RestClient(reqUrl);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                client.Authenticator = new HttpBasicAuthenticator(mambuUserid, mambuPW);

                request.AddHeader("content-type", "application/json");
                IRestResponse response = client.Execute(request);

                object con = response.Content;
                responseString = con.ToString();

                if (responseString != "")
                {
                    if (response.StatusCode.ToString() == "OK")
                    {
                        JArray loanCat = JArray.Parse(responseString);
                        return loanCat[0]["value"].ToString();
                    }

                    else

                    {
                        lf.writeLog("Function:mambu.getLoanMonthlyIncome. Account Id:" + accountId + ". Error-", responseString);
                        return "";
                    }
                }

                else

                {
                    lf.writeLog("Function:mambu.getLoanMonthlyIncome. Account Id:" + accountId + ". Error-", response.ErrorMessage.ToString());
                    return "";
                }
            }

            catch (System.Exception ex)

            {
                lf.writeLog("Function:mambu.getLoanMonthlyIncome. Account Id:" + accountId + ". Error-", ex.Message.ToString());
                return "";
            }
        }

        public string pullEWTransaction()

        {
            string mamburul, mambuUserid, mambuPW, payLoad, curDate;
            string eastWestTransaction = "";
            
            curDate = System.DateTime.Now.Date.ToString("yyyy-MM-dd");
            curDate = "2019-06-26";
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");


            LogFile lf = new LogFile();

            mamburul = mambuUri + "/api/loans/transactions/search?offset=0&limit=10";
            payLoad = "{\"filterConstraints\":[{\"filterSelection\":\"8a818fec63c590ad0163ccd35d776a0b\",\"filterElement\":\"EQUALS\",\"value\":\"East West Bank - Approved\",\"dataFieldType\":\"CUSTOM\"},{\"filterSelection\":\"ENTRY_DATE\",\"filterElement\":\"ON\",\"value\":\""+ curDate + "\"},{\"filterSelection\":\"EVENT\",\"filterElement\":\"EQUALS\",\"value\":\"REPAYMENT\"}]}";

            mambuUserid = userId;
            mambuPW = password;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient(mamburul);
            var request = new RestRequest(Method.POST);

            try
            {

                request.AddHeader("cache-control", "no-cache");
                client.Authenticator = new HttpBasicAuthenticator(mambuUserid, mambuPW);

                request.AddHeader("content-type", "application/json");

                request.AddParameter("application/json", payLoad, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                object con = response.Content;
                eastWestTransaction = con.ToString();

                if (eastWestTransaction != "")
                {
                    if (response.StatusCode.ToString() != "OK")

                    {
                        lf.writeLog("Function:mambu.pullEWTransaction. Date: " + DateTime.Now.ToString() + ". ", eastWestTransaction);
                        eastWestTransaction = "";
                    }
                }

                else

                {
                    lf.writeLog("Function:mambu.pullEWTransaction. Date: " + DateTime.Now.ToString() + ".", response.ErrorMessage.ToString());
                }

                return eastWestTransaction;
            }

            catch (System.Exception ex)
            {
                lf.writeLog("Function:mambu.pullEWTransaction. Operation Date: " + DateTime.Now.ToString() + ". ", ex.Message.ToString());
                return eastWestTransaction;
            }

        }

        public string getLoanId(string accountKey)

        {
            string reqUrl, mambuUserid, mambuPW, responseString;

            LogFile lf = new LogFile();

            mambuUserid = userId;
            mambuPW = password;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                reqUrl = mambuUri + "/api/loans/" + accountKey;
                var client = new RestClient(reqUrl);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                client.Authenticator = new HttpBasicAuthenticator(mambuUserid, mambuPW);

                request.AddHeader("content-type", "application/json");
                IRestResponse response = client.Execute(request);

                object con = response.Content;
                responseString = con.ToString();

                if (responseString != "")
                {
                    if (response.StatusCode.ToString() == "OK")
                    {
                        JObject accountDetails = JObject.Parse(responseString);
                        return accountDetails["id"].ToString();
                    }

                    else

                    {
                        lf.writeLog("Function:mambu.getLoanId. Account Id:" + accountKey + ". Error-", responseString);
                        return "";
                    }
                }

                else

                {
                    lf.writeLog("Function:mambu.getLoanId. Account Id:" + accountKey + ". Error-", response.ErrorMessage.ToString());
                    return "";
                }
            }

            catch (System.Exception ex)

            {
                lf.writeLog("Function:mambu.getLoanId. Account Id:" + accountKey + ". Error-", ex.Message.ToString());
                return "";
            }
        }

        public void readMemberId()

        {
            string strFilePath;
            strFilePath = @"C:\Mambu\Loans_06-26-2019.csv";
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    //for (int i = 0; i < headers.Length; i++)
                    //{
                    //dr[i] = rows[i];
                    //}

                    //dt.Rows.Add(dr);
                    dt.Rows.Add(rows[0]);
                }
            }

        }

    }
}
