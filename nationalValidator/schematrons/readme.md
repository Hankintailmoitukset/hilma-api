# Schematrons
## About
The schematron files in this directory are generated automatically (if not stated otherwise) based on data provided 
based on `nationalfields.json` and `nationalrules.json`. See readme.md in nationalValidator directory for more indepth information.

## Usage
We use these schematrons in conjunction with TED-OP schematrons (this can be clearly seen in the `hilma-validation.sch`) to generate the stylesheets used to validate XML.

## Deviation From TED-OP
hilma-validation.sch deviates from TED-OP's validation. hilma-validation.sch splits the validation into two stages.
Which is implemented using schematron phases. This decision was made to reduce file size and increase performance.

### Shared Stage (Schematron Phase)
This shared phase is a collection of all files that are duplicated across every phase. This stage is run at the beginning of every
validation request

### Notice Specific Stage (Schematron Phase)
The notice subtype is used to select the needed phase.

### Merging
This approach produces two reports (shared and notice subtype) which are merged before being returned.
