import {BackendService} from '../services/backend-service';
import {inject} from 'aurelia-framework';
import moment from 'moment';

@inject(BackendService)
export class Report {

  constructor(backendService) {
    this.backendService = backendService;
    this.flightReport = [];
  }

  activate(params) {
    this.reportUrl = this.backendService.getFlightExportUrl();
    this.backendService.getFlightsForReport()
      .then(data => {
        this.flightReport = data;
      });

  }

  getFormattedFlightDuration(flight) {
    if (flight == null) {
      return "";
    }
    if (flight.landingTime == null) {
      return "--";
    }
    let duration = moment.duration(moment(flight.landingTime).diff(flight.takeoffTime));
    return moment.utc(duration.asMilliseconds()).format("H°mm'");
  }
}
