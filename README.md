# SportyWarsaw

Sporty Warsaw is a social app for organizing sports events in Warsaw. 

It was developed for the [Business Intelligence Hackathon API (BIHAPI)](http://www.bihapi.pl) contest. The data about sports facilities in the application comes from an open data set provided by the City of Warsaw.

This repository contains the backend for Sporty Warsaw. The solution consists of three projects:
* SportyWarsaw.Domain - domain model definition (EF6 Code First) and code for downloading and transforming data from the BIHAPI servers
* SportyWarsaw.WebApi - RESTful API service, contains all core logic of the application
* SportyWarsaw.Tests - unit tests (only for parsing data at the moment)

## Clients
* [Android client](http://github.com/mchudy/sportywarsaw.android)

## License
```
Copyright 2016 Marcin Chudy, Jan Kierzkowski

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
```
