Heikura.Hadoop.Samples
======================

Hadoop (HDInsight) samples

# Usage

 - Build the binaries
 - Use SensorDataGenerator to generate sample xml files: SensorDataGenerator 1000 outdir
 - Upload sample data to blob storage associated with your HDInsight cluster
 - Configure the default.yaml with your HDInsight details
 - Execute MapReduce.Xml.exe locally 
 
MapReduce.Xml.exe is configured to automatically upload itself to the HDInsight cluster and execute itself there.

