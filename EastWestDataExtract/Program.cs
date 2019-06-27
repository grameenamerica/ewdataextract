using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWestDataExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            string accountList,trxDetails;
            mambu m = new mambu();
            utility ut = new utility();

//            accountList=m.pullEWAccounst();
//            ut.EWLoanFile(accountList);

            //trxDetails = m.pullEWTransaction();
            //ut.EWTransaction(trxDetails);

            m.readMemberId();

        }
    }
}
