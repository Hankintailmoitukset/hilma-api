# Hilma API meeting 23.1.2020 14:00-14:30 (EET)

## Participants
- HH (Innofactor)
- TT (Innofactor)
- EI (eKeiretsu)
- AR (Cloudia)
- JL (Mercell)
- JLM (Mercell)

## Topics
- New `hilma-api` github repo: https://github.com/Hankintailmoitukset/hilma-api
    - The place for API docs, meeting notes (participants, topics discussed and action points) and issues related to the Hilma API
    - For now ETS API users have to include request payload in bug reports. Hilma team will investigate if it is possible/reasonable to not include the request payload in the future. We will inform about any changes to the issue template in the upcoming API meetings.
- Q: Can we have the API changelog in hilma-api repo?
    - AP: Hilma team will include this in the development process.
- New tool to explore example notice contracts in Hilma: Navigate to a notice, hit F12 and add `?ets=1` to the URL. This will console output the notice in ETS format (non-public information nulled). ETS API users can use this to figure out the correct format for that notice type.
- Q: Will you keep hilma-migration repo in github up to date?
    - A: It is not guaranteed that it is up to date. It has been used as an internal tool for running migrations from old Hilma to new Hilma and it will not be used in the future.

## Known bugs

- API is responding with status code 500 without any clear error messages in certain scenarios due to validation. Hilma team will fix this asap so that reasonable error message is returned in such cases.