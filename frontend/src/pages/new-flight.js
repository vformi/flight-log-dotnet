import {BackendService} from '../services/backend-service';
import {inject} from 'aurelia-framework';
import {TakeoffForm} from "../model/takeoff-form";

@inject(BackendService)
export class CurrentFlights {

  constructor(backendService) {
    this.backendService = backendService;

    this.airplanes = [];
    this.clubMembers = [];

    this.takeoffForm = new TakeoffForm();
  }

  activate(params) {
    this.backendService.getClubAirplanes().then(
      data => {
        this.airplanes = data;
      }
    );

    this.backendService.getClubMembers().then(
      data => {
        this.clubMembers = data;
      }
    );
  }

  takeoff() {
    this.backendService.takeoff(this.takeoffForm)
      .then(() => {
        alert('Start letu byl zaznamenán');
        this.takeoffForm = new TakeoffForm();
      })
      .catch(error => {
        console.log(error);
        alert('Start letu se nepodařilo zaznamenat');
      });
  }

}
