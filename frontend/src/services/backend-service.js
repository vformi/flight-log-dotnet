import {inject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';


@inject(HttpClient)
export class BackendService {

  constructor(httpClient) {
    this.httpClient = httpClient;
  }

  getFlightsInAir() {
    return this.httpClient.fetch('flight/inAir')
      .then(response => response.json())
      .catch(error => {
        console.log(error);
        alert("Cannot fetch flights in the air. Backend connection error.");
      });
  }

  getClubAirplanes() {
    return this.httpClient.fetch('airplane')
      .then(response => response.json())
      .catch(error => {
        console.log(error);
        alert("Cannot fetch club airplanes. Backend connection error.");

      });
  }

  getClubMembers() {
    return this.httpClient.fetch('user')
      .then(response => response.json())
      .catch(error => {
        console.log(error);
        alert("Cannot fetch club members. Backend connection error.");
      });
  }

  landFlight(flightId, landingTime) {
    let landCommand = {
      flightId: flightId,
      landingTime: landingTime
    };

    return this.httpClient.fetch('flight/land', {
      method: 'post',
      body: json(landCommand)
    }).catch(error => {
      console.log(error);
      alert('Cannot land flight.');
      throw "CannotLandException"
    });
  }

  takeoff(form) {
    let request = {
      "takeoffTime": this.getTakeoffTimeFromForm(form.takeoffTime),
      "task": form.task,
      "towplane": this.getFlightFromForm(form.towplane),
      "glider": this.getFlightFromForm(form.glider)
    };

    console.debug(json(request));
    return this.httpClient.fetch('flight/takeoff', {
      method: 'post',
      body: json(request)
    });
  }

  getFlightFromForm(flight) {
    if (flight.getAirplane() == null) {
      return null;
    } else {
      return {
        "airplane": flight.getAirplane(),
        "pilot": flight.getPilot(),
        "copilot": flight.getCopilot(),
        "note": flight.note
      }
    }
  }

  getTakeoffTimeFromForm(takeoffTime) {
    console.log(takeoffTime);
    if (takeoffTime) {
      return takeoffTime;
    } else {
      return new Date();
    }
  }

  getFlightsForReport(){
    return this.httpClient.fetch('flight/report')
      .then(response => response.json())
      .catch(error => {
        console.log(error);
        alert("Cannot fetch flights for the report. Backend connection error.");
      });
  }

  getFlightExportUrl() {
    return this.httpClient.baseUrl + 'flight/export';
  }

}
