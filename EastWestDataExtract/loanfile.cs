using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWestDataExtract
{
    public class loanfile

    {
        [Name("Account HolderId")]
        public string _member_id {get; set;}

        [Name("Account ID")]
        public string _account_id { get; set; }

        [Name("Arrears Position")]
        public int _arrear_position { get; set; }

        [Name("Interest Rate")]
        public decimal _interest_rate { get; set; }

        [Name("Principal Balance")]
        public decimal _principal_balance { get; set; }

        [Name("Total Balance")]
        public decimal _total_balance { get; set; }

        [Name("Account State")]
        public string _account_state { get; set; }

        [Name("Account Sub-State")]
        public string _account_sub_state { get; set; }

        [Name("Activation Date")]
        public Nullable<DateTime>_activation_date { get; set; }

        [Name("Approval Date")]
        public DateTime _approval_date { get; set; }

        [Name("Closed Date")]
        public Nullable<DateTime> _closed_date { get; set; }

        [Name("Days In Arrears")]
        public int _days_in_arrears { get; set; }

        [Name("Disbursed Amount")]
        public decimal _disbursed_amount { get; set; }

        [Name("Expected Maturity Date")]
        public DateTime _expected_maturity_date { get; set; }

        [Name("First Repayment Date")]
        public DateTime _first_repayment_date { get; set; }

        [Name("Interest Due")]
        public decimal _interest_due { get; set; }

        [Name("Interest Paid")]
        public decimal _interest_paid { get; set; }

        [Name("Last Payment Amount")]
        public decimal _last_payment_amount { get; set; }

        [Name("Last Payment Date")]
        public DateTime _last_payment_date { get; set; }

        [Name("Last Set to Arrears Date")]
        public Nullable<DateTime> _last_set_to_arrears_date { get; set; }

        [Name("Loan Amount")]
        public decimal _loan_amount { get; set; }

        [Name("Locked Date")]
        public Nullable<DateTime> _locked_date { get; set; }

        [Name("Principal Due")]
        public decimal _principal_due { get; set; }

        [Name("Principal Paid")]
        public decimal _principal_paid { get; set; }

        [Name("Product")]
        public string _product { get; set; }
        
        [Name("Total Due")]
        public decimal _total_due { get; set; }

        [Name("Total Paid")]
        public decimal _total_paid { get; set; }

        [Name("MONIC")]
        public string _monic { get; set; }

        [Name("CAT")]
        public string _cat { get; set; }

        [Name("PURP")]
        public string _purp { get; set; }

    }
}
