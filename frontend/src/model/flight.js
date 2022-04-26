import {Airplane} from "./airplane";
import {Person} from "./person";

export class Flight {

  constructor() {
    this.airplane = new Airplane();
    this.pilot = new Person();
    this.copilot = null;
    this.note = null;
  }

  getAirplane() {
    if (this.airplane) {
      return this.airplane.getAirplane();
    } else {
      return null;
    }
  }

  getPilot() {
    if (this.pilot) {
      return this.pilot.getPerson();
    } else {
      return null;
    }
  }

  getCopilot() {
    if (this.copilot) {
      return this.copilot;
    } else {
      return null;
    }
  }
}
