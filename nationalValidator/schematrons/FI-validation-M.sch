<?xml version="1.0" encoding="UTF-8"?>
<!-- Manual validation rules -->
<pattern id="FI-validation-M" xmlns="http://purl.oclc.org/dsdl/schematron">
    <rule context="/*" >
        <assert test="count(hilma:NationalExtension/hilma:ProcurementProject) or not($noticeSubType = ('E1', 'E3', 'E4'))">rule|text|FI-0001</assert>
    </rule>
</pattern>