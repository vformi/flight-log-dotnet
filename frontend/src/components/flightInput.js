export class FlightInput {

  activate(model){
    console.log(model);
    this.flight = model.flight;
    this.title = model.title;
    this.allowNone = model.allowNone;
  }
}
