# Data migration
Tools in this folder support the migration of data into SFM.

```SFM.DataImporter.Client.zip``` contains the uploader that parses the predefined enctypted data for an installation, including organizations, personell, patients and patient data.

`FmSfmExportImportFormatDefinition.xsd` contains the formal XML specification for the format used in the different file components.

`export-crypto.cs` contains example code for content encryption of data for import to SFM.

The folder `examples` contains some examples for patients, organization and Index

Tools for export from FM is included in the FM distribution. Note that there need to be corresponding versions on FM and the epxorter, and that an FM update may be necessary prior to export. 

See the SFM documentation for details.

