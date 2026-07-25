using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayern.CapstoneService.DAL
{
    public partial class BankDeposit
    {
        public const string TableName = "BankDeposit";
        public const string SortByColumn = DBColumns.BankDepositId;

        // static empty instance of this class (used in place of null where applicable).
        public static readonly BankDeposit Empty = new BankDeposit();

        #region Instance Properties

        public int BankDepositId { get; set; }
        public int CreatedByLoginUserId { get; set; }
        public DateTime CreatedUTCDateTime { get; set; }
        public bool VoidedFlag { get; set; }
        public int? VoidedByLoginUserId { get; set; }

        [XmlElementAttribute(IsNullable = false)]
        public string Description { get; set; }

        [XmlElementAttribute(IsNullable = true)]
        public string Note { get; set; }

        #endregion Instance Properties

        #region Column Names
        internal static class DBColumns
        {
            public const string BankDepositId = "BankDepositId";
            public const string CreatedByLoginUserId = "CreatedByLoginUserId";
            public const string CreatedUTCDateTime = "CreatedUTCDateTime";
            public const string VoidedFlag = "VoidedFlag";
            public const string Description = "Description";
        }
        #endregion Column Names
    }
}
