# Hyperway
<p align="center">
  <img width="180" height="180" src="https://skynet.sediva.it/img/peppol/hyperway-logo.png">
</p>

This repository contains the [PEPPOL](http://www.peppol.eu/) Access Point, named **Hyperway**,
inspired on [OXALIS](https://github.com/difi/oxalis) written in JAVA.

Hyperway is a C# PEPPOL implementation written to enhance OXALIS performance keeping cross-platform support.

Hyperway is written in .NET Standard 2.0, supports .NET Core 2.0 and runs on Linux, Windows and Mac.

The Hyperway system can be used used as a complete standalone PEPPOL solution or as an API component from your own code.

It comes with a basic command line tool for sending messages (```hyperway-standalone```) capable of sending multiple files.

## Roadmap
v. 1.0 - WIP
* Complete porting of [OXALIS 4.0.1](https://github.com/difi/oxalis) (Inbound, Outbound and Standlone components)
* Complete porting of [VEFA PEPPOL](https://github.com/difi/vefa-peppol) 
  * Common - Data model for PEPPOL functionality.
  * Evidence - Implementation of ETSI REM Evidence.
  * ICD - Handling of ICDs as used in PEPPOL.
  * Lookup - Functionality for looking up participants in PEPPOL.
  * Mode - Feature to configure a PEPPOL application based on a PEPPOL certificate.
  * Publisher - Generic implementation of SMP interface.
  * SBDH - Optimized library for handling of envelope.
  * Security - Security features for PEPPOL.
* Complete porting of [certvalidator](https://github.com/difi/certvalidator) a certificate validator for X.509 certificates
* Refactoring to reflect C# naming conventions, configuration remapping, xml serialization model


## Components
* [Zipkin](https://zipkin.io/) instrumentation
* [BouncyCastle](https://www.bouncycastle.org/) for crypto and security services
* [Autofac](https://autofac.org/) as inversion of control container
* [log4net](https://logging.apache.org/log4net/release/features.html) for logging


## Installation
...

## License
hyperway is an open source project supported by Massive Dynamic Technology under BSD license.

## Troubleshooting
...

## Build from source
Note that the Hyperway "head" revision on *master* branch is often in "flux" and should be considered a "nightly build".
The official releases are tagged and may be downloaded by clicking on [Tags](https://github.com/massivex/hyperway/tags).

* make sure [Visual Studio 2017 15.7.4](http://www.visualstudio.com/) is installed


## Securing
...
