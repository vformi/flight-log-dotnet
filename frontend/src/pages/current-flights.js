import {BackendService} from '../services/backend-service';
import {inject} from 'aurelia-framework';

@inject(BackendService)
export class CurrentFlights {

  constructor(backendService) {
    this.backendService = backendService;
    this.flightsInAir = [];
    this.flightToLandWithTime = null;
    this.landingTime = null;
  }

  activate(params) {
    this.backendService.getFlightsInAir().then(
      data => {
        this.flightsInAir = data;
      }
    );
  }

  landNow(flight) {
    this.landFlight(flight, new Date());
  };

  landAtSelectedTime(){
    this.landFlight(this.flightToLandWithTime, this.landingTime);
    this.cancelLandingWithTime();
  }

  landFlight(flight, time) {
    this.backendService.landFlight(flight.id, time).then(
      () => {
        // remove flight
        let index = this.flightsInAir.indexOf(flight);
        if (index !== -1) {
          this.flightsInAir.splice(index, 1);
        }
      }
    );
  }

  openLandingWithTime(flight) {
    this.flightToLandWithTime = flight;
    this.landingTime = new Date();
    console.log("Landing time ",this.landingTime)
  }

  cancelLandingWithTime(){
    this.flightToLandWithTime = null;
    this.landingTime = null;
  }

}
