# National Validator
___
## Important

National Validator uses SDK 1.13 to validate E1, E3 and E4. This is a transitional step,
and we reserve the right to make modifications to workflows and documentation with minimal notice.

Documentation contained within this directory is provided in the hopes to facilitate constructive feedback and ease transition of external users.
___
### Clarification
- `fields.json`: Is a reference to TED-OP fields.json.
- `national validator`: Hilma's self-hosted XML validator.
- `national rules`: Hilma's national rules.
- `national fields`: Hilma's national fields.
- `national notices`: E1, E3, E4 notice subtypes.

### Current Usage
National validator is current serving a role to facilitate the submission of only E1, E3 and E4 notices, referred to as `national notices`.
These national notices currently live outside the SDK 1.10 that is used Hilma, as the national notices use SDK 1.13 ruleset. 

This migration to a national validator is the first step in a process of unifying our validation logic.

<mark>The information below will affect only the national notices until further notice.</mark>

### About
The national validator is Hilma's self-hosted XML validator. The national validator unifies our validation logic into an
easily maintainable and transparent set of rules (nationalRules.json and nationalFields.json).

### How does it work
The Validator uses TED-OP schematron files as a base and appends Finland national rules on top.
This process consists of multiple parts:
- `ted-field`: original field extracted from fields.json
- `national-rule`: ruleset extracted from nationalRules.json
- `national-field`: field extracted from nationalFields.json

This process consists of multiple stages:
- Each `ted-field` gets a `national-field` merged into it.
- Each `national-field` get appended to fields.json

This process results in an end product referred to as `enriched-fields`.
an `enriched-field` is a `ted-field` that has undergone some mutation via the above described process.

After the `enriched-fields` have been produced they are parsed into schematron and then compiled into xsl stylesheets.

### Notice Structure & National Specific fields 
As noted above, we are enriching the fields.json and the result of this means we need to communicate the new expected data structure to our users.
To achieve this we are 

#### xsd structure
You can find the enriched xsd in this same directory, the enriched xsd is TED-OP xsd with the national fields appended to it.

### National Rules & Fields

#### nationalRules.json
nationalRules.json is a list of objects condensed version of TED-OP fields.json field object:
````
[ 
    {
        "id" : < ID of fields matching fields.json >,
        "forbidden" : {
            "constraints" : 
            [ 
                {
                "noticeTypes" : < Array of String >,
                "severity" : "ERROR",
                "value" : true
                },¨
                ....
            ],
        },
        "mandatory" : {
            "constraints" : 
            [ 
                {
                  "noticeTypes" : < Array of String >,
                  "severity" : "ERROR",
                  "value" : true
                },
                ....
            ],
        }
        ....
]
````

#### nationalFields.json
nationalFields.json is an implementation of TED-OP fields.json, see TED-OP field.json for example field object.
