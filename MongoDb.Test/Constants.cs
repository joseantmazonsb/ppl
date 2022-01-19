namespace MongoDb.Test {
    public static class Constants {
        public const string ConnectionString = "mongodb://root:root@" 
                                               + "localhost:27017" 
                                               + "/test/" 
                                               + "?authSource=admin";
    }
}