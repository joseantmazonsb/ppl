namespace PluggablePersistenceLayer.MongoDb.Builders {
    /// <summary>
    /// Specific options for a MongoDb driver.
    /// </summary>
    public class MongoDbDriverOptions {
        /// <summary>
        /// If set to <c>false</c>, you not will get an error when attempting to use transactions
        /// if the database is not configured as a Replica Set. Disabling this feature may
        /// lead to troubles understanding your own code, specially when rolling back transactions.
        /// </summary>
        public bool FailIfTransactionsNotSupported {
            get => Strict || _failIfTransactionsNotSupported;
            set => _failIfTransactionsNotSupported = value;
        }
        private bool _failIfTransactionsNotSupported = true;

        /// <summary>
        /// If set to <c>false</c>, an error will be thrown if you attempt to perform an operation
        /// within a non registered dataset, even though the inner MongoDb provider
        /// allows such things.
        /// </summary>
        public bool AllowAnyDataset {
            get => !Strict && _allowAnyDataset;
            set => _allowAnyDataset = value;
        }
        private bool _allowAnyDataset;

        /// <summary>
        /// Be even stricter than <c>Strict</c>.
        /// </summary>
        public bool Pedantic { get; set; }

        /// <summary>
        /// Apply best practices for this framework.
        /// </summary>
        /// <remarks>Overrides other options.</remarks>
        public bool Strict {
            get => !Pedantic && _strict;
            set => _strict = value;
        }
        private bool _strict;

        public MongoDbDriverOptions() {
            Strict = true;
        }
    }
}