using System;
using Newtonsoft.Json.Linq;
using CsvHelper;
using System.Collections.Generic;
using System.IO;

namespace EastWestDataExtract
{
    class utility

    {

        public void EWLoanFile(string account_list)

        {
            string member_id, account_id, account_state, account_sub_state, monic, cat, purp, product, file_name;
            int arrear_position, days_in_arrears, account_count;
            decimal interest_rate, principal_balance, total_balance, interest_due, interest_paid, last_payment_amount, loan_amount, interest_balance;
            decimal principal_due, principal_paid, total_due, total_paid, disburse_amount;
            object checkJsNode;

            LogFile lgf = new LogFile();

            try
            {

                DateTime? locked_date = null, closed_date = null, last_set_to_arrears_date = null;
                DateTime activation_date, approval_date, expected_maturity_date, first_repayment_date;

                file_name = "C:\\mambu\\" + "Loans " + System.DateTime.Today.ToString("MM-dd-yyyy") + ".csv";


                JArray account_details = JArray.Parse(account_list);
                account_count = account_details.Count;

                List<loanfile> loanRecords = new List<loanfile>();

                mambu m = new mambu();


                for (int i = 0; i < account_count; i++)

                {
                    member_id = account_details[i]["accountHolderKey"].ToString();
                    account_id = account_details[i]["id"].ToString();
                    account_state = account_details[i]["accountState"].ToString();
                    product = account_details[i]["loanName"].ToString();

                    checkJsNode = account_details[i]["accountsubState"];

                    if (checkJsNode != null)

                    {
                        account_sub_state = account_details[i]["accountsubState"].ToString();
                    }

                    else

                    {
                        account_sub_state = null;
                    }


                    checkJsNode = account_details[i]["lastSetToArrearsDate"];

                    if (checkJsNode != null)

                    {
                        last_set_to_arrears_date = TimeZoneInfo.ConvertTimeToUtc(System.Convert.ToDateTime(account_details[i]["lastSetToArrearsDate"].ToString())).Date;
                        arrear_position = (System.DateTime.Today - Convert.ToDateTime(account_details[i]["lastSetToArrearsDate"].ToString())).Days + 1;
                    }

                    else

                    {
                        last_set_to_arrears_date = null;
                        arrear_position = 0;
                    }


                    days_in_arrears = arrear_position;
                    interest_rate = System.Convert.ToDecimal(account_details[i]["interestRate"].ToString());
                    principal_balance = System.Convert.ToDecimal(account_details[i]["principalBalance"].ToString());
                    interest_balance = System.Convert.ToDecimal(account_details[i]["interestBalance"].ToString());

                    total_balance = principal_balance + interest_balance;

                    interest_due = System.Convert.ToDecimal(account_details[i]["interestDue"].ToString());
                    interest_paid = System.Convert.ToDecimal(account_details[i]["interestPaid"].ToString());
                    last_payment_amount = System.Convert.ToDecimal(account_details[i]["interestPaid"].ToString());// TBD
                    loan_amount = System.Convert.ToDecimal(account_details[i]["loanAmount"].ToString());
                    principal_due = System.Convert.ToDecimal(account_details[i]["principalDue"].ToString());
                    principal_paid = System.Convert.ToDecimal(account_details[i]["principalPaid"].ToString());
                    total_due = principal_due + interest_due;
                    total_paid = principal_paid + interest_paid;
                    disburse_amount = System.Convert.ToDecimal(account_details[i]["loanAmount"].ToString());


                    activation_date = System.Convert.ToDateTime(account_details[i]["creationDate"].ToString()).Date;
                    approval_date = System.Convert.ToDateTime(account_details[i]["approvedDate"].ToString()).Date;


                    checkJsNode = account_details[i]["closeDate"];

                    if (checkJsNode != null)

                    {
                        closed_date = System.Convert.ToDateTime(account_details[i]["closeDate"].ToString()).Date;
                    }
                    else
                    {
                        closed_date = null;
                    }

                    //var timeUtc = DateTime.UtcNow;

                    var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

                    expected_maturity_date = System.Convert.ToDateTime(account_details[i]["approvedDate"].ToString()).Date;//TBD
                    first_repayment_date = TimeZoneInfo.ConvertTimeToUtc(System.Convert.ToDateTime(account_details[i]["disbursementDetails"]["firstRepaymentDate"].ToString())).Date;//TBD

                    checkJsNode = account_details[i]["lastLockedDate"];

                    if (checkJsNode != null)
                    {
                        locked_date = System.Convert.ToDateTime(account_details[i]["lastLockedDate"].ToString()).Date;
                    }
                    else
                    {
                        locked_date = null;
                    }

                    cat = m.getLoanCategory(account_id);
                    purp = m.getLoanPurpose(account_id);
                    monic = m.getLoanMonthlyIncome(account_id);

                    loanfile lf = new loanfile();

                    lf._member_id = member_id;
                    lf._account_id = account_id;
                    lf._arrear_position = arrear_position;
                    lf._interest_rate = interest_rate;
                    lf._principal_balance = principal_balance;
                    lf._total_balance = total_balance;
                    lf._account_state = account_state;
                    lf._account_sub_state = account_sub_state;
                    lf._activation_date = activation_date;
                    lf._approval_date = approval_date;
                    lf._closed_date = closed_date;
                    lf._days_in_arrears = days_in_arrears;
                    lf._disbursed_amount = disburse_amount;
                    lf._expected_maturity_date = expected_maturity_date;
                    lf._first_repayment_date = first_repayment_date;
                    lf._interest_due = interest_due;
                    lf._interest_paid = interest_paid;
                    lf._last_payment_date = System.DateTime.Now;
                    lf._last_set_to_arrears_date = last_set_to_arrears_date;
                    lf._loan_amount = loan_amount;
                    lf._locked_date = locked_date;
                    lf._principal_due = principal_due;
                    lf._principal_paid = principal_paid;
                    lf._total_due = total_due;
                    lf._total_paid = total_paid;
                    lf._product = product;
                    lf._monic = monic;
                    lf._cat = cat;
                    lf._purp = purp;

                    loanRecords.Add(lf);

                }



                using (var writer = new StreamWriter(file_name))
                using (var csv = new CsvWriter(writer))

                {
                    csv.WriteRecords(loanRecords);
                    writer.Close();
                }
            }

            catch (System.Exception ex)

            {
                lgf.writeLog("utility.EWLoanFile. Operation Date: " + System.DateTime.Today.Date.ToString("MM-dd-yyyy") + ". ", ex.Message.ToString());
            }
        }

        public void EWTransaction(string transaction_list)

        {
            string account_id, channel, id, transaction_type, type_is_reversal, was_reversed, file_name,reversal_key;
            decimal amount, interest_amount, principal_amount;
            int transaction_count;

            DateTime? creation_date = null,entry_date=null;
            object checkJsNode;
            List<transactionfile> transactionrecords = new List<transactionfile>();
            
            LogFile lgf = new LogFile();

            file_name = "C:\\mambu\\" + "Transaction " + System.DateTime.Today.ToString("MM-dd-yyyy") + ".csv";

            JArray transaction_details = JArray.Parse(transaction_list);
            transaction_count = transaction_details.Count;

            List<transactionfile> loanRecords = new List<transactionfile>();

            mambu m = new mambu();

            try
            {
                for (int i = 0; i < transaction_count; i++)

                {
                    account_id = m.getLoanId(transaction_details[i]["parentAccountKey"].ToString());
                    amount= System.Convert.ToDecimal(transaction_details[i]["amount"].ToString());

                    checkJsNode = transaction_details[i]["details"]["transactionChannel"];

                    if (checkJsNode != null)

                    {
                        channel = transaction_details[i]["details"]["transactionChannel"]["name"].ToString();
                    }

                    else

                    {
                        channel ="";
                    }
                    creation_date = TimeZoneInfo.ConvertTimeToUtc(System.Convert.ToDateTime(transaction_details[i]["creationDate"].ToString())).Date;
                    entry_date = TimeZoneInfo.ConvertTimeToUtc(System.Convert.ToDateTime(transaction_details[i]["entryDate"].ToString())).Date;
                    id= transaction_details[i]["transactionId"].ToString();
                    interest_amount= System.Convert.ToDecimal(transaction_details[i]["interestPaid"].ToString());
                    principal_amount = System.Convert.ToDecimal(transaction_details[i]["principalPaid"].ToString());
                    transaction_type = transaction_details[i]["type"].ToString();

                    checkJsNode = transaction_details[i]["reversalTransactionKey"];

                    if (checkJsNode != null)

                    {
                        reversal_key = transaction_details[i]["reversalTransactionKey"].ToString();
                        type_is_reversal = "";
                        was_reversed = "YES";
                    }

                    else

                    {
                        type_is_reversal = "";
                        was_reversed = "";
                    }
                    transactionfile tf = new transactionfile();
                    tf._account_id = account_id;
                    tf._amount = amount;
                    tf._channel = channel;
                    tf._creation_date = creation_date;
                    tf._entry_date = entry_date;
                    tf._transaction_id = id;
                    tf._interest_amount = interest_amount;
                    tf._principal_amount = principal_amount;
                    tf._transaction_type = transaction_type;
                    tf._type_is_reversal = type_is_reversal;
                    tf._was_reversed = was_reversed;
                    transactionrecords.Add(tf);

                }

                using (var writer = new StreamWriter(file_name))
                using (var csv = new CsvWriter(writer))
                {
                    csv.WriteRecords(transactionrecords);
                    writer.Close();
                }
            }
            catch (System.Exception ex)

            {
                lgf.writeLog("utility.EWTransaction. Operation Date: " + System.DateTime.Today.Date.ToString("MM-dd-yyyy") + ". ", ex.Message.ToString());
            }
        }
    }
}

