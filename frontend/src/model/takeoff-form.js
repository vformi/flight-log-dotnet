import {Flight} from "./flight";

export class TakeoffForm {

  constructor() {
    this.takeoffTime = null;
    this.task = null;
    this.towplane = new Flight();
    this.glider = new Flight();
    this.withoutGlider = false;
  }

}
