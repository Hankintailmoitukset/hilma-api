# Participants
- Jarkko Sikiö
- Aleksandar Zezovksi
- Diego Rönkkömäki-Tapia
- Mikko Hagman
- Hjalmar Sedwall
- Johan Adamsson
- Joose Huopio
- Laura Kivikangas
- Olli Surakka

# Topics
## API Response change
- As Hilma is moving towards full Schematron validation, both the errors-field and tedValidationReport-field in the API response will both be deprecated. In the future, all validation results will be in validationReport-field which will have the same SVRL format (Schematron Validation Report Language) as the current tedValidationReport-field and it will be generated using Hilma's own Schematron validator.

## Remarks about the timetable
- It may be possible for Hilma partners to start using SDK 1.13 during February. We will follow-up on this in later API meetings
	- SDK 1.13 is believed to be too ambitious, 1.12 should be ok
	- Go-live with SDK 1.13 in the end of February would be achievable
- Common go-live with E1/E3/E4 eForms on 15.1. comes too soon,  possible date would be somewhere end of Q1
- 6.6.2025 seems to be quite a fast schedule for stopping the publication of other national notices in old-style format. The 1 month transition period raises concern but needs follow-up in later API meetings.
	- Depends on the non-TED notice type specifications, Hansel will estimate when the specifications will be ready.
- How exactly will DPS procurement notice's corrigendum notices be made after 8.1.2026 for DPS's that run e.g. from 2023 to 2028?
	- Joose & Petteri will talk about this