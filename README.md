# BaggageAutomation
Repository for KSU Fall 2022 Hackathon - Airport Luggage Automated Retrieval
This repository serves as a home for our team's solution to airport carousels.
We currently have the solution implemented as a WPF project in Visual Studio 2022,
with hopes to move it to a web-based project given the time. The general flow of the
program at the moment is for the purpose of demonstrating the concept in a short period
of time. QR codes are generated for checked in luggage as the user arrives at the airport,
containting each a GUID, a reserved physical storage location in their destination airport,
the name of the airline, and the destination airport. These QR codes are then detected and
deserialized from a static image, in the real world this would be a camera that exists at
the destination airport, passing images of each item of luggage to the program. The luggage 
is then taken to its reserved storage location, waiting for it's owner to scan their ticket
at the luggage-pickup kiosk, upon which the luggage will be delivered via conveyor belt to
the kiosk.
