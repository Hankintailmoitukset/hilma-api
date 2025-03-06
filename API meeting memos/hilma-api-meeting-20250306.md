# Participants
- Diego Rönkkömäki-Tapia
- Laura Kivinkangas
- Joel Hallikainen
- Hjalmar Sedwall
- Thomas Hauken
- Baran Elis

## National eForms notices
- Test environment API now accepts E1, E3, and E4 notices with TED 1.13 specification
  - The write API endpoint has remained unchanged
  - National FI-fields are now supported. Hilma will publish documentation about the schema (xsm and JSON) soon.

## SDK 1.13 upgrade
- The following fields will not be supported in Hilma
    - **EED**: BT-810, BT-811, BT-812, BT-813, BT-814, BT-815
    - **REVIEW**: BT-783, BT-784, BT-785, BT-786, BT-787, BT-788, BT-789, BT-790, BT-791, BT-792, BT-793, BT-794, BT-795, BT-796, BT-797, BT-798
    - **IPI**: BT-684, BT-685, BT-686, BT-687, BT-688
    - **FSR**: BT-682, BT-681
	
	
- How does the rollout for SDK 1.13 look like for partners? 
	- Hilma will publish 1.13 on April 15th 4 PM UTC+2
    - Are we able to establish a date when partners and Hilma start supporting SDK 1.13 at the same time without the need for backwards compatibility grace period (SDK 1.10.2)?
		- Partners will discuss this internally but the goal is to have no grace period
