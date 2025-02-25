<?xml version="1.0" encoding="UTF-8"?>

<!-- Generated by the Hilma Schematron Generator -->

<pattern id="FI-validation-E3" xmlns="http://purl.oclc.org/dsdl/schematron">
<rule context="/*/cac:TenderingProcess[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-105-Procedure-1" role="ERROR" test="count(cbc:ProcedureCode) &gt; 0">rule|text|FI-E3-BT-105-Procedure-1</assert>
<assert id="FI-E3-BT-88-Procedure-1" role="ERROR" test="count(cbc:Description) &gt; 0 or not(cbc:ProcedureCode/normalize-space(text()) = ('oth-mult','oth-single'))">rule|text|FI-E3-BT-88-Procedure-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingProcess[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-109-Lot-1" role="ERROR" test="count(cac:FrameworkAgreement/cbc:Justification) &gt; 0 or not(cac:ContractingSystem[cbc:ContractingSystemTypeCode/@listName='framework-agreement']/cbc:ContractingSystemTypeCode/normalize-space(text()) = ('fa-mix','fa-w-rc','fa-wo-rc')) or not((boolean(for $T in (current-date()) return ($T + xs:dayTimeDuration(../cac:ProcurementProject/cac:PlannedPeriod/cbc:EndDate/xs:date(text()) - ../cac:ProcurementProject/cac:PlannedPeriod/cbc:StartDate/xs:date(text())) > $T + xs:yearMonthDuration('P4Y')))) or (boolean(for $T in (current-date()) return ($T + (for $F in ../cac:ProcurementProject/cac:PlannedPeriod/cbc:DurationMeasure return (if ($F/@unitCode='WEEK') then xs:dayTimeDuration(concat('P', $F/number() * 7, 'D')) else if ($F/@unitCode='DAY') then xs:dayTimeDuration(concat('P', $F/number(), 'D')) else if ($F/@unitCode='YEAR') then xs:yearMonthDuration(concat('P', $F/number(), 'Y')) else if ($F/@unitCode='MONTH') then xs:yearMonthDuration(concat('P', $F/number(), 'M')) else ())) > $T + xs:yearMonthDuration('P4Y')))))">rule|text|FI-E3-BT-109-Lot-1</assert>
<assert id="FI-E3-BT-109-Lot-2" role="ERROR" test="count(cac:FrameworkAgreement/cbc:Justification) = 0 or (cac:ContractingSystem[cbc:ContractingSystemTypeCode/@listName='framework-agreement']/cbc:ContractingSystemTypeCode/normalize-space(text()) = ('fa-mix','fa-w-rc','fa-wo-rc')) or ((boolean(for $T in (current-date()) return ($T + xs:dayTimeDuration(../cac:ProcurementProject/cac:PlannedPeriod/cbc:EndDate/xs:date(text()) - ../cac:ProcurementProject/cac:PlannedPeriod/cbc:StartDate/xs:date(text())) > $T + xs:yearMonthDuration('P4Y')))) or (boolean(for $T in (current-date()) return ($T + (for $F in ../cac:ProcurementProject/cac:PlannedPeriod/cbc:DurationMeasure return (if ($F/@unitCode='WEEK') then xs:dayTimeDuration(concat('P', $F/number() * 7, 'D')) else if ($F/@unitCode='DAY') then xs:dayTimeDuration(concat('P', $F/number(), 'D')) else if ($F/@unitCode='YEAR') then xs:yearMonthDuration(concat('P', $F/number(), 'Y')) else if ($F/@unitCode='MONTH') then xs:yearMonthDuration(concat('P', $F/number(), 'M')) else ())) > $T + xs:yearMonthDuration('P4Y')))))">rule|text|FI-E3-BT-109-Lot-2</assert>
<assert id="FI-E3-BT-113-Lot-1" role="ERROR" test="count(cac:FrameworkAgreement/cbc:MaximumOperatorQuantity) &gt; 0 or not(cac:ContractingSystem[cbc:ContractingSystemTypeCode/@listName='framework-agreement']/cbc:ContractingSystemTypeCode/normalize-space(text()) = ('fa-mix','fa-w-rc','fa-wo-rc'))">rule|text|FI-E3-BT-113-Lot-1</assert>
<assert id="FI-E3-BT-113-Lot-2" role="ERROR" test="count(cac:FrameworkAgreement/cbc:MaximumOperatorQuantity) = 0 or (cac:ContractingSystem[cbc:ContractingSystemTypeCode/@listName='framework-agreement']/cbc:ContractingSystemTypeCode/normalize-space(text()) = ('fa-mix','fa-w-rc','fa-wo-rc'))">rule|text|FI-E3-BT-113-Lot-2</assert>
<assert id="FI-E3-BT-17-Lot-1" role="ERROR" test="count(cbc:SubmissionMethodCode[@listName='esubmission']) &gt; 0">rule|text|FI-E3-BT-17-Lot-1</assert>
<assert id="FI-E3-BT-52-Lot-1" role="ERROR" test="count(cbc:CandidateReductionConstraintIndicator) &gt; 0 or ../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = 'open' or ../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = 'oth-single' or ../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = 'neg-wo-call'">rule|text|FI-E3-BT-52-Lot-1</assert>
<assert id="FI-E3-BT-52-Lot-2" role="ERROR" test="count(cbc:CandidateReductionConstraintIndicator) = 0 or ../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = 'open' or ../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = 'oth-single' or ../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = 'neg-wo-call'">rule|text|FI-E3-BT-52-Lot-2</assert>
<assert id="FI-E3-BT-745-Lot-1" role="ERROR" test="count(cac:ProcessJustification/cbc:Description) &gt; 0 or cbc:SubmissionMethodCode[@listName='esubmission']/normalize-space(text()) = 'not-allowed'">rule|text|FI-E3-BT-745-Lot-1</assert>
</rule>
<rule context="/*/cac:ContractingParty[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-11-Procedure-Buyer-1" role="ERROR" test="count(cac:ContractingPartyType/cbc:PartyTypeCode[@listName='buyer-legal-type']) &gt; 0">rule|text|FI-E3-BT-11-Procedure-Buyer-1</assert>
</rule>
<rule context="/*/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/efext:EformsExtension/efac:Changes/efac:Change/efac:ChangedSection[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-13716-notice-1" role="ERROR" test="count(efbc:ChangedSectionIdentifier) &gt; 0 or not(../../efbc:ChangedNoticeIdentifier)">rule|text|FI-E3-BT-13716-notice-1</assert>
<assert id="FI-E3-BT-13716-notice-2" role="ERROR" test="count(efbc:ChangedSectionIdentifier) = 0 or (../../efbc:ChangedNoticeIdentifier)">rule|text|FI-E3-BT-13716-notice-2</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms/cac:CallForTendersDocumentReference[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-14-Lot-1" role="ERROR" test="count(cbc:DocumentType) &gt; 0">rule|text|FI-E3-BT-14-Lot-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Part']/cac:TenderingTerms/cac:CallForTendersDocumentReference[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-14-Part-1" role="ERROR" test="count(cbc:DocumentType) &gt; 0">rule|text|FI-E3-BT-14-Part-1</assert>
</rule>
<rule context="/*/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/efext:EformsExtension/efac:Changes/efac:Change[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-141_a_-notice-1" role="ERROR" test="count(efbc:ChangeDescription) &gt; 0 or not(efac:ChangedSection/efbc:ChangedSectionIdentifier)">rule|text|FI-E3-BT-141(a)-notice-1</assert>
<assert id="FI-E3-BT-141_a_-notice-2" role="ERROR" test="count(efbc:ChangeDescription) = 0 or (efac:ChangedSection/efbc:ChangedSectionIdentifier)">rule|text|FI-E3-BT-141(a)-notice-2</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Part']/cac:ProcurementProject[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-23-Part-1" role="ERROR" test="count(cbc:ProcurementTypeCode[@listName='contract-nature']) &gt; 0">rule|text|FI-E3-BT-23-Part-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Part']/cac:ProcurementProject/cac:MainCommodityClassification[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-262-Part-1" role="ERROR" test="count(cbc:ItemClassificationCode) &gt; 0">rule|text|FI-E3-BT-262-Part-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/efext:EformsExtension/efac:SelectionCriteria[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-40-Lot-1" role="ERROR" test="count(efbc:SecondStageIndicator) &gt; 0 or (not(cbc:TendererRequirementTypeCode)) or not(../../../../../../cac:TenderingProcess/cbc:CandidateReductionConstraintIndicator = true()) or not(../../../../../../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = ('comp-dial','innovation','neg-w-call','oth-mult','restricted'))">rule|text|FI-E3-BT-40-Lot-1</assert>
<assert id="FI-E3-BT-40-Lot-2" role="ERROR" test="count(efbc:SecondStageIndicator) = 0 or ((cbc:TendererRequirementTypeCode)) or (../../../../../../cac:TenderingProcess/cbc:CandidateReductionConstraintIndicator = true()) or (../../../../../../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = ('comp-dial','innovation','neg-w-call','oth-mult','restricted'))">rule|text|FI-E3-BT-40-Lot-2</assert>
<assert id="FI-E3-BT-750-Lot-1" role="ERROR" test="count(cbc:Description) &gt; 0 or not(cbc:TendererRequirementTypeCode)">rule|text|FI-E3-BT-750-Lot-1</assert>
<assert id="FI-E3-BT-750-Lot-2" role="ERROR" test="count(cbc:Description) = 0 or (cbc:TendererRequirementTypeCode)">rule|text|FI-E3-BT-750-Lot-2</assert>
</rule>
<rule context="/*/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/efext:EformsExtension/efac:Organizations/efac:Organization/efac:Company/cac:PartyLegalEntity[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-501-Organization-Company-1" role="ERROR" test="count(cbc:CompanyID) &gt; 0">rule|text|FI-E3-BT-501-Organization-Company-1</assert>
</rule>
<rule context="/*/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/efext:EformsExtension/efac:Organizations/efac:Organization/efac:Company[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-506-Organization-Company-1" role="ERROR" test="count(cac:Contact/cbc:ElectronicMail) &gt; 0 or (cac:PartyIdentification/cbc:ID/normalize-space(text()) = ../../../../../../../cac:ContractingParty/cac:Party/cac:PartyIdentification/cbc:ID/normalize-space(text()))">rule|text|FI-E3-BT-506-Organization-Company-1</assert>
<assert id="FI-E3-BT-507-Organization-Company-1" role="ERROR" test="count(cac:PostalAddress/cbc:CountrySubentityCode) &gt; 0 or not(cac:PostalAddress/cac:Country/cbc:IdentificationCode/normalize-space(text()) = ('ALB','AUT','BEL','BGR','CHE','CYP','CZE','DEU','DNK','ESP','EST','FIN','FRA','GRC','HRV','HUN','IRL','ISL','ITA','LIE','LTU','LUX','LVA','MKD','MLT','MNE','NLD','NOR','POL','PRT','ROU','SRB','SVK','SVN','SWE','TUR','XKX'))">rule|text|FI-E3-BT-507-Organization-Company-1</assert>
<assert id="FI-E3-BT-507-Organization-Company-2" role="ERROR" test="count(cac:PostalAddress/cbc:CountrySubentityCode) = 0 or (cac:PostalAddress/cac:Country/cbc:IdentificationCode/normalize-space(text()) = ('ALB','AUT','BEL','BGR','CHE','CYP','CZE','DEU','DNK','ESP','EST','FIN','FRA','GRC','HRV','HUN','IRL','ISL','ITA','LIE','LTU','LUX','LVA','MKD','MLT','MNE','NLD','NOR','POL','PRT','ROU','SRB','SVK','SVN','SWE','TUR','XKX'))">rule|text|FI-E3-BT-507-Organization-Company-2</assert>
</rule>
<rule context="/*/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/efext:EformsExtension/efac:Organizations/efac:Organization/efac:Company/cac:PostalAddress[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-513-Organization-Company-1" role="ERROR" test="count(cbc:CityName) &gt; 0">rule|text|FI-E3-BT-513-Organization-Company-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms/cac:AwardingTerms/cac:AwardingCriterion/cac:SubordinateAwardingCriterion[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-539-Lot-1" role="ERROR" test="count(cbc:AwardingCriterionTypeCode[@listName='award-criterion-type']) &gt; 0">rule|text|FI-E3-BT-539-Lot-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='LotsGroup']/cac:TenderingTerms/cac:AwardingTerms/cac:AwardingCriterion[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-539-LotsGroup-1" role="ERROR" test="count(cac:SubordinateAwardingCriterion/cbc:AwardingCriterionTypeCode[@listName='award-criterion-type']) &gt; 0 or not(../../../cbc:ID)">rule|text|FI-E3-BT-539-LotsGroup-1</assert>
<assert id="FI-E3-BT-539-LotsGroup-2" role="ERROR" test="count(cac:SubordinateAwardingCriterion/cbc:AwardingCriterionTypeCode[@listName='award-criterion-type']) = 0 or (../../../cbc:ID)">rule|text|FI-E3-BT-539-LotsGroup-2</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-60-Lot-1" role="ERROR" test="count(cbc:FundingProgramCode[@listName='eu-funded']) &gt; 0">rule|text|FI-E3-BT-60-Lot-1</assert>
<assert id="FI-E3-BT-769-Lot-1" role="ERROR" test="count(cbc:MultipleTendersCode) = 0">rule|text|FI-E3-BT-769-Lot-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingProcess/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/efext:EformsExtension/efac:InterestExpressionReceptionPeriod[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-630_d_-Lot-1" role="ERROR" test="count(cbc:EndDate) = 0">rule|text|FI-E3-BT-630(d)-Lot-1</assert>
<assert id="FI-E3-BT-630_t_-Lot-1" role="ERROR" test="count(cbc:EndTime) = 0">rule|text|FI-E3-BT-630(t)-Lot-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingProcess/cac:EconomicOperatorShortList[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-661-Lot-1" role="ERROR" test="count(cbc:LimitationDescription) &gt; 0 or ../../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = 'open' or ../../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = 'oth-single' or ../../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = 'neg-wo-call'">rule|text|FI-E3-BT-661-Lot-1</assert>
<assert id="FI-E3-BT-661-Lot-2" role="ERROR" test="count(cbc:LimitationDescription) = 0 or ../../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = 'open' or ../../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = 'oth-single' or ../../../cac:TenderingProcess/cbc:ProcedureCode/normalize-space(text()) = 'neg-wo-call'">rule|text|FI-E3-BT-661-Lot-2</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms/cac:TendererQualificationRequest[not(cbc:CompanyLegalFormCode)][not(cac:SpecificTendererRequirement/cbc:TendererRequirementTypeCode[@listName='missing-info-submission'])][not(cac:SpecificTendererRequirement/cbc:TendererRequirementTypeCode[@listName='selection-criteria-source'])]/cac:SpecificTendererRequirement[cbc:TendererRequirementTypeCode/@listName='reserved-procurement'][$noticeSubType = 'E3']">
<assert id="FI-E3-BT-71-Lot-1" role="ERROR" test="count(cbc:TendererRequirementTypeCode) &gt; 0">rule|text|FI-E3-BT-71-Lot-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Part']/cac:TenderingTerms/cac:TendererQualificationRequest[not(cbc:CompanyLegalFormCode)][not(cac:SpecificTendererRequirement/cbc:TendererRequirementTypeCode[@listName='missing-info-submission'])][not(cac:SpecificTendererRequirement/cbc:TendererRequirementTypeCode[@listName='selection-criteria-source'])]/cac:SpecificTendererRequirement[cbc:TendererRequirementTypeCode/@listName='reserved-procurement'][$noticeSubType = 'E3']">
<assert id="FI-E3-BT-71-Part-1" role="ERROR" test="count(cbc:TendererRequirementTypeCode) &gt; 0">rule|text|FI-E3-BT-71-Part-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms/cac:ContractExecutionRequirement[cbc:ExecutionRequirementCode/@listName='reserved-execution'][$noticeSubType = 'E3']">
<assert id="FI-E3-BT-736-Lot-1" role="ERROR" test="count(cbc:ExecutionRequirementCode) &gt; 0">rule|text|FI-E3-BT-736-Lot-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Part']/cac:TenderingTerms/cac:ContractExecutionRequirement[cbc:ExecutionRequirementCode/@listName='reserved-execution'][$noticeSubType = 'E3']">
<assert id="FI-E3-BT-736-Part-1" role="ERROR" test="count(cbc:ExecutionRequirementCode) &gt; 0">rule|text|FI-E3-BT-736-Part-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms/cac:RequiredFinancialGuarantee[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-751-Lot-1" role="ERROR" test="count(cbc:GuaranteeTypeCode[@listName='tender-guarantee-required']) &gt; 0">rule|text|FI-E3-BT-751-Lot-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/efext:EformsExtension/efac:SelectionCriteria/efac:CriterionParameter[efbc:ParameterCode/@listName='number-weight'][$noticeSubType = 'E3']">
<assert id="FI-E3-BT-752-Lot-ThresholdNumber-1" role="ERROR" test="count(/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/efext:EformsExtension/efac:SelectionCriteria/efac:CriterionParameter[efbc:ParameterCode/@listName='number-threshold']/efbc:ParameterNumeric) &gt; 0 or (efbc:ParameterCode or ../efac:CriterionParameter[efbc:ParameterCode/@listName='number-threshold']/efbc:ParameterCode)">rule|text|FI-E3-BT-752-Lot-ThresholdNumber-1</assert>
</rule>
<rule context="/*/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/efext:EformsExtension/efac:Changes[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-758-notice-1" role="ERROR" test="count(efbc:ChangedNoticeIdentifier) &gt; 0">rule|text|FI-E3-BT-758-notice-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingProcess/cac:ContractingSystem[cbc:ContractingSystemTypeCode/@listName='framework-agreement'][$noticeSubType = 'E3']">
<assert id="FI-E3-BT-765-Lot-1" role="ERROR" test="count(cbc:ContractingSystemTypeCode) &gt; 0">rule|text|FI-E3-BT-765-Lot-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Part']/cac:TenderingProcess/cac:ContractingSystem[cbc:ContractingSystemTypeCode/@listName='framework-agreement'][$noticeSubType = 'E3']">
<assert id="FI-E3-BT-765-Part-1" role="ERROR" test="count(cbc:ContractingSystemTypeCode) &gt; 0">rule|text|FI-E3-BT-765-Part-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingProcess/cac:ContractingSystem[cbc:ContractingSystemTypeCode/@listName='dps-usage'][$noticeSubType = 'E3']">
<assert id="FI-E3-BT-766-Lot-1" role="ERROR" test="count(cbc:ContractingSystemTypeCode) &gt; 0">rule|text|FI-E3-BT-766-Lot-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Part']/cac:TenderingProcess/cac:ContractingSystem[cbc:ContractingSystemTypeCode/@listName='dps-usage'][$noticeSubType = 'E3']">
<assert id="FI-E3-BT-766-Part-1" role="ERROR" test="count(cbc:ContractingSystemTypeCode) &gt; 0">rule|text|FI-E3-BT-766-Part-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingProcess/cac:AuctionTerms[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-767-Lot-1" role="ERROR" test="count(cbc:AuctionConstraintIndicator) &gt; 0">rule|text|FI-E3-BT-767-Lot-1</assert>
</rule>
<rule context="/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms/cac:Language[$noticeSubType = 'E3']">
<assert id="FI-E3-BT-97-Lot-1" role="ERROR" test="count(cbc:ID) &gt; 0">rule|text|FI-E3-BT-97-Lot-1</assert>
</rule>
<rule context="/*/hilma:NationalExtension/hilma:ProcurementProject[$noticeSubType = 'E3']">
<assert id="FI-E3-FI-11-1" role="ERROR" test="count(hilma:ApplicableLegislation) &gt; 0">rule|text|FI-E3-FI-11-1</assert>
<assert id="FI-E3-FI-30-1" role="ERROR" test="count(hilma:PredictedValueConstraintIndicator) &gt; 0">rule|text|FI-E3-FI-30-1</assert>
<assert id="FI-E3-FI-20-1" role="ERROR" test="count(hilma:IsEUApplicableConstraintIndicator) &gt; 0">rule|text|FI-E3-FI-20-1</assert>
</rule>
</pattern>
