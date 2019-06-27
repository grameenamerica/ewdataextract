using CsvHelper.Configuration.Attributes;
using System;

namespace EastWestDataExtract
{
    class transactionfile
    {

        [Name("AccountID")]
        public string _account_id { get; set; }

        [Name("Amount")]
        public decimal _amount { get; set; }

        [Name("Channel")]
        public string _channel { get; set; }

        [Name("Creation Date")]
        public Nullable<DateTime> _creation_date { get; set; }

        [Name("Entry Date")]
        public Nullable<DateTime> _entry_date { get; set; }

        [Name("ID")]
        public string _transaction_id { get; set; }

        [Name("Interest Amount")]
        public decimal _interest_amount { get; set; }

        [Name("Principal Amount")]
        public decimal _principal_amount { get; set; }

        [Name("Type")]
        public string _transaction_type { get; set; }

        [Name("Type Is Reversal")]
        public string _type_is_reversal { get; set; }

        [Name("Was Reversed")]
        public string _was_reversed { get; set; }

    }
}
