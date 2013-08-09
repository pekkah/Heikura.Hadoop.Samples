namespace MapReduce.Xml
{
    public class Configuration
    {
        public string ClusterUrl { get; set; }

        public string ClusterUserName { get; set; }

        public string ClusterPassword { get; set; }

        public string HadoopUserName { get; set; }

        public string BlobStorageUrl { get; set; }

        public string BlobStorageKey { get; set; }

        public string BlobContainerName { get; set; }

        public bool CreateContainerIfMissing { get; set; }

        public string InputPath { get; set; }

        public string OutputPath { get; set; }
    }
}