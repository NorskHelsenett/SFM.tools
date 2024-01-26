# Multidose examples from SFM BASIS API
## Version 3.9 is now in test, and there is still some adjustements to come.

The example files in this folder provide examples on changes that have been estabished between version 2.12 used by two systems in production today, and the soon-to-be released version 3.9 where information on PLL/Myltidose has been refactored. The essence of this is that what is called `sectionPLLinfo` now contains one entry for each type of M25 message.
Documented in Simplifier: https://simplifier.net/guide/SFMAPIBasis/Multidose-info?version=current 

The information on multidose responsibility may be reveiled by parsing a structure. Depending on the actual use, information for display may be found on higher level than the structured information itself:


Shown below is the section entry:
```
                {
                  "title": "PllInfo",
                  "code": {
                    "coding": [
                      {
                        "system": "http://ehelse.no/fhir/CodeSystem/sfm-section-types",
                        "code": "sectionPLLinfo",
                        "display": "PLL Info"
                      }
                    ]
                  },
                  "text": {
                    "status": "generated",
                    "div": "<xhtml:div xmlns:xhtml=\"http://www.w3.org/1999/xhtml\">Info read from M25</xhtml:div>"
                  },
                  "entry": [
                    {
                      "reference": "urn:uuid:aad7def2-3273-40f4-ae4d-833d0a6e340c",
                      "type": "http://ehelse.no/fhir/StructureDefinition/sfm-PLL-info",
                      "display": "M25.1"
                    },
                    {
                      "reference": "urn:uuid:2f83430d-579f-4ef2-96e1-90ff28948dd6",
                      "type": "http://ehelse.no/fhir/StructureDefinition/sfm-PLL-info",
                      "display": "M25.2"
                    },
                    {
                      "reference": "urn:uuid:621a9b1d-21c5-4eab-9114-915819146705",
                      "type": "http://ehelse.no/fhir/StructureDefinition/sfm-PLL-info",
                      "display": "M25.3"
                    }
                  ]
                },
```
Each of these are identified with code:
```
              "code": {
                "coding": [
                  {
                    "system": "http://ehelse.no/fhir/CodeSystem/sfm-message-type",
                    "code": "M25.1",
                    "display": "PLL"
                  }
                ]
              },
```

In each of these it is possible to find timestamp, sender, multidose responsible actors and more:
```
            "resource": {
              "resourceType": "Basic",
              "id": "8ae003f5-221d-4ad8-a936-60e46fd535b3",
              "meta": {
                "profile": [
                  "http://ehelse.no/fhir/StructureDefinition/sfm-PLL-info"
                ]
              },
              "extension": [
                {
                  "extension": [
                    {
                      "url": "substitute",
                      "valueBoolean": false
                    },
                    {
                      "url": "PLLdate",
                      "valueDateTime": "2023-12-08T13:54:37.7688651+01:00"
                    },
                    {
                      "url": "relatedparties",
                      "valueReference": {
                        "reference": "urn:uuid:94eac548-4c93-43e9-9e55-fe344ef19725",
                        "type": "http://ehelse.no/fhir/StructureDefinition/sfm-PractitionerRole",
                        "display": "JOHAN PSA JENSEN, HPR: 431001110, Tverrådalskyrkja PLO Institusjon, HER: 8140453, M:Multidoseansvarlig lege"
                      }
                    },
                    {
                      "url": "relatedparties",
                      "valueReference": {
                        "reference": "urn:uuid:aaed83a5-c4db-43ec-bfb0-d85e5a19236a",
                        "type": "http://ehelse.no/fhir/StructureDefinition/sfm-PractitionerRole",
                        "display": "Vitusapotek Test Turing, HER: 8135176, +4711223344, A:Ansvarlig farmasøyt"
                      }
                    }
                  ],
                  "url": "http://ehelse.no/fhir/StructureDefinition/sfm-pllInformation"
                }
              ],
              "identifier": [
                {
                  "use": "usual",
                  "value": "e6c9577d-df6b-4eaa-8d1a-23421bc8c816"
                }
              ],
              "code": {
                "coding": [
                  {
                    "system": "http://ehelse.no/fhir/CodeSystem/sfm-message-type",
                    "code": "M25.1",
                    "display": "PLL"
                  }
                ]
              },
              "subject": {
                "reference": "urn:uuid:e8119b94-c9cd-4b2e-804a-aa96ca8cf2a3",
                "type": "http://ehelse.no/fhir/StructureDefinition/sfm-Patient",
                "display": "Troy Hammer"
              },
              "author": {
                "reference": "urn:uuid:4d095026-c5f1-479f-a783-58a68a77bb7c",
                "type": "http://ehelse.no/fhir/StructureDefinition/sfm-PractitionerRole",
                "display": "JOHAN PSA JENSEN, HPR: 431001110, Østmarka Legekontor, HER: 8093556"
              }

```
The ´display´ fields above may be shown to the user for display purpouse.

When following the reference to the practitionerRoles, the details may be found down in the Practitioner and/or Organziation resources, example here for the multidose responsible doctor:
```
"resource": {
              "resourceType": "Practitioner",
              "id": "1935277754",
              "meta": {
                "profile": [
                  "http://ehelse.no/fhir/StructureDefinition/sfm-Practitioner"
                ]
              },
              "identifier": [
                {
                  "use": "official",
                  "type": {
                    "coding": [
                      {
                        "system": "http://hl7.no/fhir/NamingSystem/HPR",
                        "code": "HPR-nummer"
                      }
                    ]
                  },
                  "system": "urn:oid:2.16.578.1.12.4.1.4.4",
                  "value": "431001110"
                },
                {
                  "use": "official",
                  "type": {
                    "coding": [
                      {
                        "system": "http://hl7.no/fhir/NamingSystem/FNR",
                        "code": "FNR-nummer"
                      }
                    ]
                  },
                  "system": "urn:oid:2.16.578.1.12.4.1.4.1",
                  "value": "17056600574"
                }
              ],
              "active": true,
              "name": [
                {
                  "extension": [
                    {
                      "url": "http://hl7.no/fhir/StructureDefinition/no-basis-middlename",
                      "valueString": "PSA"
                    }
                  ],
                  "use": "official",
                  "family": "JENSEN",
                  "given": [
                    "JOHAN"
                  ]
                }
              ],
              "gender": "male",
              "birthDate": "1966-05-17"
            }
```
The "rare" case of having multidose responsibility even if no M25 messages are available will be represented with a pllInfo with code "M9.12" (will be available in version 3.10):
* paper multidose (will be replaced by e-multidose)
* failure in registration, a multidose doctor is registered, but no M25.1 is sent


```
              "code": {
                "coding": [
                  {
                    "system": "http://ehelse.no/fhir/CodeSystem/sfm-message-type",
                    "code": "M9.12",
                    "display": "PLL"
                  }
                ]
              },
```

